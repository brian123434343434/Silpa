using SILPA.AccesoDatos.Salvoconducto;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.LogicaNegocio.Aprovechamiento;
using SILPA.AccesoDatos.BPMProcess;
using System.IO;
using System.Xml.Serialization;
using SILPA.LogicaNegocio.BPMProcessL;
using System.Configuration;
using SoftManagement.Log;
using SILPA.AccesoDatos.Generico;
using System.Data;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class SalvoconductoNew
    {
        private SalvoconductoNewDalc vSalvoconductoNewDalc;
        private EspecimenNewDalc vEspecimenNewDalc;
        private EstadoSalvoconductoDalc vEstadoSalvoconductoDalc;
        private RutaDalc vRutaDalc;
        private TransporteDalc vTransporteDalc;
        
        public SalvoconductoNew()
        {
            vSalvoconductoNewDalc = new SalvoconductoNewDalc();
            vEspecimenNewDalc = new EspecimenNewDalc();
            vEstadoSalvoconductoDalc = new EstadoSalvoconductoDalc();
            vRutaDalc = new RutaDalc();
            vTransporteDalc = new TransporteDalc();
        }
        
        //public void CrearSolicitudSalvoconducto(ref SalvoconductoNewIdentity vSalvoconductoNewIdentity)
        public int CrearSolicitudSalvoconducto(ref SalvoconductoNewIdentity vSalvoconductoNewIdentity)
        {
            int SalvoconductoId = 0;
            try
            {
                string strNumeroVital = "";
                vSalvoconductoNewIdentity.EstadoID = Convert.ToInt32(EstadoSalvoconducto.Solicitud);
                SalvoconductoId = vSalvoconductoNewDalc.CrearSolicitudSalvoconducto(ref vSalvoconductoNewIdentity);
                vEspecimenNewDalc.EliminarEspeciesSalvoconducto(SalvoconductoId);
                foreach (EspecimenNewIdentity especie in vSalvoconductoNewIdentity.LstEspecimen)
                {
                    especie.SalvocoductoID = SalvoconductoId;
                    vEspecimenNewDalc.CrearEspecieSalvoconducto(especie);
                }
                foreach (TransporteNewIdentity transporte in vSalvoconductoNewIdentity.LstTransporte)
                {
                    transporte.SalvoconductoID = SalvoconductoId;
                    vTransporteDalc.InsertarTransporteSalvoconducto(transporte);
                }
                // consultamos el origen y el destino ruta del salvoconducto
                if (vSalvoconductoNewIdentity.LstRuta.Count > 0)
                {
                    // obtenemos el primer elemento de la lista de rutas.
                    var rutaOrigen = vSalvoconductoNewIdentity.LstRuta.First();
                    var rutaDestino = vSalvoconductoNewIdentity.LstRuta.Last();
                    // insertarmos el origen y destino del salvoconducto
                    RutaDalc objRutaDalc = new RutaDalc();
                    int rutaID = objRutaDalc.InsertarOrigenDestinoSalvoconducto(rutaOrigen, rutaDestino, vSalvoconductoNewIdentity.SalvoconductoID);

                    // removemos la primer y ultima ruta
                    var lstRutaDesplazamiento = vSalvoconductoNewIdentity.LstRuta;
                    foreach (RutaEntity iRuta in lstRutaDesplazamiento)
                    {
                        iRuta.RutaID = rutaID;
                        objRutaDalc.InsertarRutaDesplazamientoSalconducto(iRuta);
                    }
                }

                //jmartinez Salvocondcuto Fase 2
                if (vSalvoconductoNewIdentity.LstSalvoconductoAnterior != null && vSalvoconductoNewIdentity.LstSalvoconductoAnterior.Count == 0)
                {
                    foreach (var salvoconductoAnterior in vSalvoconductoNewIdentity.LstSalvoconductoAnterior)
                    {
                        vSalvoconductoNewDalc.InsertarSalvoconductoAnterior(SalvoconductoId, salvoconductoAnterior.SalvoconductoID, salvoconductoAnterior.Detalle);
                    }
                }


                //JACOSTA 20170512. SE AGREGA LA FUNCIONALIDAD PARA AGREGAR VARIOS APROVECHAMIENTOS
                //jmartinez Salvocondcuto Fase 2
                if (vSalvoconductoNewIdentity.LstAprovechamientoOrigen != null && vSalvoconductoNewIdentity.LstAprovechamientoOrigen.Count > 0)
                {
                    foreach (var aprovechamientoOrigen in vSalvoconductoNewIdentity.LstAprovechamientoOrigen)
                    {
                        vSalvoconductoNewDalc.InsertarAprovechamientoOrigen(SalvoconductoId, aprovechamientoOrigen.AprovechamientoID, aprovechamientoOrigen.Detalle);
                    }
                }
                // insertamos los datos del transporte y transportador

                vSalvoconductoNewIdentity.SalvoconductoID = SalvoconductoId;
                if (SalvoconductoId > 0)
                {

                    //Registrar en VITAL
                    strNumeroVital = RegistroVITAL(vSalvoconductoNewIdentity, SalvoconductoId);

                    //Verificar que se obtenga el numero VITAL
                    if (!string.IsNullOrEmpty(strNumeroVital))
                    {
                        //Cargar numero vital al registro
                        vSalvoconductoNewIdentity.NumeroVitalTramite = strNumeroVital;
                        // si se genera el numero vital se realiza el envio del correo informando que se ha creado una nueva solicitud
                        EnviarCorreoNuevaSolicitud(ref vSalvoconductoNewIdentity);
                    }
                    else
                    {
                        //REalizar reverso de la transaccion
                        this.vSalvoconductoNewDalc.EliminarSalvoconducto(SalvoconductoId);

                        //Generar excepcion
                        throw new Exception("No se pudo obtener el número vital");
                    }



                }
                else
                {
                    throw new Exception("No se pudo obtener el identificador del registro");
                }
            }
            catch (Exception exc)
            {
                //REalizar reverso de la transaccion
                this.vSalvoconductoNewDalc.EliminarSalvoconducto(SalvoconductoId);
                throw new Exception("No se pudo crear la solicitud.");
            }
            return SalvoconductoId;
        }
        public SalvoconductoNewIdentity ConsultaSalvoconductoXSalvoconductoId(int pSalvoconductoId)
        {
            return vSalvoconductoNewDalc.ConsultaSalvoconductoXSalvoconductoID(pSalvoconductoId);
        }
        public List<SalvoconductoNewIdentity> ListaSalvoconducto(DateTime? pFechaInicioSol, DateTime? pFechaFinSol, int? pAutoridadID, int? pSolicitanteID, int? pEstadoID, int? pTipoSalvoconducto,int? pClaseRecursoID, int? pAutoridadAmbientalID , string pNumeroSalvoconducto)
        {
            return vSalvoconductoNewDalc.ListaSalvoconducto(pFechaInicioSol, pFechaFinSol, pAutoridadID, pSolicitanteID, pEstadoID, pTipoSalvoconducto, pClaseRecursoID, pAutoridadAmbientalID, pNumeroSalvoconducto);
        }
        public List<SalvoconductoNewIdentity> ListaSalvoconducto(int? pAutoridadID, int? pSolicitanteID, int? pEstadoID, int? pTipoSalvoconductoID, int pClaseRecursoID)
        {
            return vSalvoconductoNewDalc.ListaSalvoconducto(pAutoridadID, pSolicitanteID, pEstadoID, pTipoSalvoconductoID, pClaseRecursoID);
        }
        public List<EstadoSalvoconductoIdentity> ListaEstadoSalvoconducto()
        {
            return vEstadoSalvoconductoDalc.ListaEstadoSalvoconducto();
        }
        public void CargarSalvoconducto(ref SalvoconductoNewIdentity pSalvoconductoNewIdentity)
        {
            vSalvoconductoNewDalc.CargarSalvoconducto(ref pSalvoconductoNewIdentity);
            vEspecimenNewDalc.EliminarEspeciesSalvoconducto(pSalvoconductoNewIdentity.SalvoconductoID);
            if (pSalvoconductoNewIdentity.LstEspecimen != null)
            {
                foreach (EspecimenNewIdentity especie in pSalvoconductoNewIdentity.LstEspecimen)
                {
                    especie.SalvocoductoID = pSalvoconductoNewIdentity.SalvoconductoID;
                    vEspecimenNewDalc.CrearEspecieSalvoconducto(especie);
                }
            }
        }
        public void ActualizarRutaArchivoSaldo(int salvoconductoID, string rutaArchivo)
        {
            vSalvoconductoNewDalc.ActualizarRutaArchivoSaldo(salvoconductoID, rutaArchivo);
        }
        public void EliminarSalvoconducto(int pSalvoconductoID)
        {
            vSalvoconductoNewDalc.EliminarSalvoconducto(pSalvoconductoID);
        }
        public List<EspecimenNewIdentity> ListaEspecieSalvoconducto(int pSalvoconductoID)
        {
            return vEspecimenNewDalc.ListaEspecieSalvoconducto(pSalvoconductoID);
        }
        public List<TipoRuta> ListaTipoRuta()
        {
            return vRutaDalc.ListaTipoRuta();
        }
        public string RegistroVITAL(SalvoconductoNewIdentity objSalvoconductoIdentity, int p_intSalvoconductoID)
        {
            BpmProcessLn objProceso = new BpmProcessLn();
            string strNumeroVital = "";

            try
            {
                try
                {
                    //Crear proceso
                    objProceso = new BpmProcessLn();
                    //strNumeroVital = objProceso.crearProceso(ConfigurationManager.AppSettings["RIDClientID"].ToString(),
                    //                                         Convert.ToInt64(ConfigurationManager.AppSettings["RIDFormID"]),
                    //                                         Convert.ToInt64(p_intSolicitanteID),
                    //                                         this.CrearXmlVital(p_intSolicitanteID, p_intSalvoconductoID));
                    strNumeroVital = objSalvoconductoIdentity.AutoridadEmisoraID.ToString() + p_intSalvoconductoID.ToString().PadLeft(7, '0') + (new Random().Next(6000000, 6099999)).ToString() + objSalvoconductoIdentity.SolicitanteID.ToString(); 
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "SolicitudSalvoconducto :: RegistroVital -> Error Inesperado: " + exc.Message + " " + exc.InnerException);

                }

                //Varificar que se halla obtenido el numero vital
                if (!string.IsNullOrEmpty(strNumeroVital))
                {
                    //Actualiza en base de datos el número vital
                    this.vSalvoconductoNewDalc.ActualizarNumeroVitalRegistro(p_intSalvoconductoID, strNumeroVital);
                }

            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "RegistroRID :: RegistroVital -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
            }

            return strNumeroVital;
        }
        /// <summary>
        /// Crear el XML requerido para crear formulario en Vital
        /// </summary>
        /// <param name="p_intSolicitanteID">int con el identificador del silicitante</param>
        /// <param name="p_intRegistroID">int con el identficador del registro</param>
        /// <returns>string con el XML</returns>
        private string CrearXmlVital(int p_intSolicitanteID, int p_intRegistroID)
        {
            List<ValoresIdentity> objValoresList = new List<ValoresIdentity>();
            objValoresList.Add(CargarValores(1, "Bas", p_intRegistroID.ToString(), 1, new Byte[1]));
            objValoresList.Add(CargarValores(2, "Bas", p_intSolicitanteID.ToString(), 1, new Byte[1]));
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer serializador = new XmlSerializer(typeof(List<ValoresIdentity>));
            serializador.Serialize(memoryStream, objValoresList);
            string xml = System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
            return xml;
        }
        /// <summary>
        /// Crear objeto identity para cargar en XML
        /// </summary>
        /// <param name="pintId">int con el ID</param>
        /// <param name="p_strGrupo">string con el nombre del grupo</param>
        /// <param name="p_strValor">string con el valor</param>
        /// <param name="p_intOrden">int con el orden</param>
        /// <param name="p_objArchivo">Arreglo de bytes con archivo</param>
        /// <returns></returns>
        private ValoresIdentity CargarValores(int pintId, string p_strGrupo, string p_strValor, int p_intOrden, Byte[] p_objArchivo)
        {
            ValoresIdentity objValores = new ValoresIdentity();
            objValores.Id = pintId;
            objValores.Grupo = p_strGrupo;
            objValores.Valor = p_strValor;
            objValores.Orden = p_intOrden;
            objValores.Archivo = p_objArchivo;
            return objValores;
        }
        public String EmitirSalvoconducto(int ID_AUT_AMBIENTAL, int SALVOCONDUCTO_ID, DateTime FEC_EXPEDICION, DateTime FEC_INI_VIGENCIA, DateTime FEC_FIN_VIGENCIA, string USUARIO, List<RutaEntity> RUTA_DESPLAZAMIENTO, string CODIGO_SEGURIDAD)
        {
            String MENSAJE = "";
            int CONSECUTIVO = 0;
            RutaDalc objRutaDalc = new RutaDalc();
            List<NumeracionSalvoconducto> AsignarConsecutivo = new List<NumeracionSalvoconducto>();
            AsignarConsecutivo = vSalvoconductoNewDalc.ConsecutivoSalvoconductoDalc(ID_AUT_AMBIENTAL, SALVOCONDUCTO_ID);
            MENSAJE = AsignarConsecutivo[0].MENSAJE.ToString();
            CONSECUTIVO =  Convert.ToInt32(AsignarConsecutivo[0].CONSECUTIVO);
            

            if (CONSECUTIVO > 0)
            {
                vSalvoconductoNewDalc.EmitirSalvoconductoDalc(CONSECUTIVO, SALVOCONDUCTO_ID, FEC_EXPEDICION, FEC_INI_VIGENCIA, FEC_FIN_VIGENCIA, USUARIO, CODIGO_SEGURIDAD);
                var lstRutaDesplazamiento = RUTA_DESPLAZAMIENTO;
                if (RUTA_DESPLAZAMIENTO.Where(x => x.Estado == true).Count() >= 1)
                {
                    foreach (RutaEntity iRuta in RUTA_DESPLAZAMIENTO)
                    {
                        objRutaDalc.InsertarRutaDesplazamientoSalconducto(iRuta);
                    }
                }
                Formularios.CrearFormularios clsCrearFormularios = new Formularios.CrearFormularios();
                //string nombreArchivo = clsCrearFormularios.GenerarSalvoconductoPDF(SALVOCONDUCTO_ID);
                //vSalvoconductoNewDalc.ActualizarRutaArchivoSaldo(SALVOCONDUCTO_ID, nombreArchivo);
                SalvoconductoNewIdentity vSalvoconductoNewIdentity = ConsultaSalvoconductoXSalvoconductoId(SALVOCONDUCTO_ID);
                if (vSalvoconductoNewIdentity.TipoSalvoconductoID == 1)// solo se registran los salvoconductos de movilizacion
                {
                    WSIntegracion_SUNL_IDEAM.RegistroSalvoconducto clsRegistroSalvoconductoIDEAM = new WSIntegracion_SUNL_IDEAM.RegistroSalvoconducto();
                    clsRegistroSalvoconductoIDEAM.RegistrarSalvoconductoIDEAM(vSalvoconductoNewIdentity);
                }
                EnviarCorreoEmisionSalvoconducto(ref vSalvoconductoNewIdentity);
            }
            return MENSAJE;
        }

        #region jmartinez cuando la serie este a punto de vencerse se envia correo a la autoridad ambiental, al emisor del salvoconducto, y al MADS
        public void EnviarCorreoVencimientoSerieSUNL(string usuario, string str_mensaje)
        {
            PersonaDalc per = new PersonaDalc();
            PersonaIdentity p = new PersonaIdentity();
            string CorreoUsuario = string.Empty;
            string CorreoNombreUsuario = string.Empty;
            string CorreoAutoridadAmbiental = string.Empty;
            string NombreAutoridadAmbiental = string.Empty;
            string CorreoMADS = string.Empty;
            string CorreoSoporte = string.Empty;

            int autID = 0;
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;
            //obtengo datos de la persona
            p = per.BuscarPersonaByUserId(usuario);
            CorreoUsuario = p.CorreoElectronico;
            //CorreoNombreUsuario = p.PrimerApellido + " " + p.SegundoApellido + " " + p.PrimerNombre + " " + p.SegundoNombre;
           

            //obtengo datos del correo autoridad ambiental
            string autNombre = per.ObtenerAutoridadPorPersona(long.Parse(usuario), out autID);
            AutoridadAmbientalDalc objAutoridad = new AutoridadAmbientalDalc();
            DataSet _dsDatos_AA = objAutoridad.ListarAutoridadAmbiental(autID);
            if (_dsDatos_AA.Tables[0].Rows.Count > 0)
            {
                CorreoAutoridadAmbiental = _dsDatos_AA.Tables[0].Rows[0]["AUT_CORREO_SALVOCONDUCTO"].ToString();
                NombreAutoridadAmbiental = _dsDatos_AA.Tables[0].Rows[0]["AUT_NOMBRE"].ToString();
                CorreoNombreUsuario = NombreAutoridadAmbiental;
            }

            //obtengo datos del correo autoridad ambiental
            objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
            CorreoMADS = objParametrizacion.ObtenerValorParametroGeneral(-1, "Correo_MADS_para_vencimiento_series");
            CorreoSoporte = objParametrizacion.ObtenerValorParametroGeneral(-1, "Correo_SOPORTE_para_vencimiento_series");

            //enviar correo al usuario
            if (!string.IsNullOrEmpty(CorreoUsuario) && !string.IsNullOrEmpty(CorreoNombreUsuario) && !string.IsNullOrEmpty(str_mensaje) && !string.IsNullOrEmpty(CorreoMADS))
            {
                EnviarCorreoVencimientoSerieSUNL(CorreoUsuario, string.Empty, CorreoNombreUsuario, str_mensaje, " Por favor remitir solicitud al MADS para nueva asignación, al correo electrónico " + CorreoMADS);
            }
            
            //enviar correo a la autoridad ambiental
            if (!string.IsNullOrEmpty(CorreoAutoridadAmbiental) && !string.IsNullOrEmpty(NombreAutoridadAmbiental) && !string.IsNullOrEmpty(str_mensaje) && !string.IsNullOrEmpty(CorreoMADS))
            { 
                EnviarCorreoVencimientoSerieSUNL(CorreoAutoridadAmbiental, string.Empty, NombreAutoridadAmbiental, str_mensaje, " Por favor remitir solicitud al MADS para nueva asignación, al correo electrónico " + CorreoMADS);
            }

            //enviar correo al MADS
            if (!string.IsNullOrEmpty(CorreoMADS) && !string.IsNullOrEmpty(NombreAutoridadAmbiental) && !string.IsNullOrEmpty(CorreoNombreUsuario) && !string.IsNullOrEmpty(str_mensaje))
            {
                EnviarCorreoVencimientoSerieSUNL(CorreoMADS, NombreAutoridadAmbiental, CorreoNombreUsuario, str_mensaje, string.Empty);
                //enviar correo a soporte SUNL
                EnviarCorreoVencimientoSerieSUNL(CorreoSoporte, NombreAutoridadAmbiental, CorreoNombreUsuario, str_mensaje, string.Empty);
            }
        }

        public void EnviarCorreoVencimientoSerieSUNL(string CorreoPara, string token_aut_amb_nombre, string token_usuario, string token_pje, string token_MailMads)
        {
            ICorreo.Correo objCorreo = null;
            try
            {
                if (CorreoPara != string.Empty)
                {//Enviar el correo
                    objCorreo = new ICorreo.Correo();
                    objCorreo.EnviarCorreoVencimientoSerieSUNL(CorreoPara, token_aut_amb_nombre, token_usuario, token_pje, token_MailMads);
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Salvoconducto :: EnviarCorreoVencimientoSerieSUNL -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
            }

        }


        #endregion

        public string GenerarPDFSalvoconducto(int SalvocondcutoID)
        {
            Formularios.CrearFormularios clsCrearFormularios = new Formularios.CrearFormularios();
            string nombreArchivo = clsCrearFormularios.GenerarSalvoconductoPDF(SalvocondcutoID);
            if (!string.IsNullOrEmpty(nombreArchivo))
            {
                vSalvoconductoNewDalc.ActualizarRutaArchivoSaldo(SalvocondcutoID, nombreArchivo);
            }
            return nombreArchivo;
        }

        public void RechazarSalvoconducto(int SALVOCONDUCTO_ID, string MOTIVO_RECHAZO, string USUARIO)
        {
            vSalvoconductoNewDalc.RechazarSalvoconductoDalc(SALVOCONDUCTO_ID, MOTIVO_RECHAZO, USUARIO);
            SalvoconductoNewIdentity vSalvoconductoNewIdentity = ConsultaSalvoconductoXSalvoconductoId(SALVOCONDUCTO_ID);
            EnviarCorreoRechazoSalvoconducto(ref vSalvoconductoNewIdentity);
        }
        /// <summary>
        /// Envia correo electronico informando a la autoridad ambiental correspondiente.
        /// </summary>
        /// <param name="vSalvoconductoNewIdentity"></param>
        private void EnviarCorreoNuevaSolicitud(ref SalvoconductoNewIdentity vSalvoconductoNewIdentity)
        {
            ICorreo.Correo objCorreo = null;
            try
            {
                string correoSolicitante = string.Empty;
                AutoridadAmbientalIdentity autoridad = new AutoridadAmbientalIdentity();
                AutoridadAmbientalDalc objAutoridad = new AutoridadAmbientalDalc();
                PersonaDalc objpersonaDalc = new PersonaDalc();
                autoridad.IdAutoridad = vSalvoconductoNewIdentity.AutoridadEmisoraID;
                objAutoridad.ObtenerAutoridadById(ref autoridad);

                correoSolicitante = objpersonaDalc.ObtenerEmailFuncionarioByApplicationUserID(vSalvoconductoNewIdentity.SolicitanteID.ToString());
                if(autoridad.CorreoSalvoconducto != string.Empty && correoSolicitante != string.Empty)
                {//Enviar el correo
                    objCorreo = new ICorreo.Correo();
                    objCorreo.EnviarCorreoNuevaSolicitudAutoridadAmbiental(autoridad.CorreoSalvoconducto, correoSolicitante, ref vSalvoconductoNewIdentity, autoridad.Nombre);
                }
            }
            catch (Exception exc)
            {

                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Salvoconducto :: EnviarCorreoNuevaSolicitud -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
            }
        }

        private void EnviarCorreoEmisionSalvoconducto(ref SalvoconductoNewIdentity vSalvoconductoNewIdentity)
        {
            ICorreo.Correo objCorreo = null;
            try
            {
                string correoSolicitante = string.Empty;
                PersonaDalc objpersonaDalc = new PersonaDalc();
                correoSolicitante = objpersonaDalc.ObtenerEmailFuncionarioByApplicationUserID(vSalvoconductoNewIdentity.SolicitanteID.ToString());
                if (correoSolicitante != string.Empty)
                {//Enviar el correo
                    objCorreo = new ICorreo.Correo();
                    objCorreo.EnviarCorreoEmisionSalvoconducto(correoSolicitante, ref vSalvoconductoNewIdentity);
                }
            }
            catch (Exception exc)
            {

                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Salvoconducto :: EnviarCorreoEmisionSalvoconducto -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
            }
        }

        private void EnviarCorreoRechazoSalvoconducto(ref SalvoconductoNewIdentity vSalvoconductoNewIdentity)
        {
            ICorreo.Correo objCorreo = null;
            try
            {
                string correoSolicitante = string.Empty;
                PersonaDalc objpersonaDalc = new PersonaDalc();
                correoSolicitante = objpersonaDalc.ObtenerEmailFuncionarioByApplicationUserID(vSalvoconductoNewIdentity.SolicitanteID.ToString());
                if (correoSolicitante != string.Empty)
                {//Enviar el correo
                    objCorreo = new ICorreo.Correo();
                    objCorreo.EnviarCorreoRechazoSalvoconducto(correoSolicitante, ref vSalvoconductoNewIdentity);
                }
            }
            catch (Exception exc)
            {

                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Salvoconducto :: EnviarCorreoRechazoSalvoconducto -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
            }
        }
        private void EnviarCorreoBloqueoSalvoconducto(ref SalvoconductoNewIdentity vSalvoconductoNewIdentity)
        {
            ICorreo.Correo objCorreo = null;
            try
            {
                //correo solicitante
                string correoSolicitante = string.Empty;
                PersonaDalc objpersonaDalc = new PersonaDalc();
                AutoridadAmbientalIdentity autoridad = new AutoridadAmbientalIdentity();
                AutoridadAmbientalDalc objAutoridad = new AutoridadAmbientalDalc();

                correoSolicitante = objpersonaDalc.ObtenerEmailFuncionarioByApplicationUserID(vSalvoconductoNewIdentity.SolicitanteID.ToString());
                autoridad.IdAutoridad = vSalvoconductoNewIdentity.AutoridadEmisoraID;
                objAutoridad.ObtenerAutoridadById(ref autoridad);

                if (correoSolicitante != string.Empty)
                {
                    objCorreo = new ICorreo.Correo();
                    objCorreo.EnviarCorreBloqueoSalvoconducto(correoSolicitante, ref vSalvoconductoNewIdentity);
                }

                //correo Autoridad Ambiental
                if (autoridad.CorreoSalvoconducto != string.Empty)
                {
                    objCorreo = new ICorreo.Correo();
                    objCorreo.EnviarCorreBloqueoSalvoconducto(autoridad.CorreoSalvoconducto, ref vSalvoconductoNewIdentity);
                }
            }
            catch (Exception exc)
            {

                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Salvoconducto :: EnviarCorreoRechazoSalvoconducto -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
            }
        }

        public bool GrabarBloqueoSalvoconducto(int SALVOCONDUCTO_ID, int TIPO_BLOQUEO_ID, string USUARIO, int autoridadCambiaEstado)
        {
            bool respuesta;
            SalvoconductoNewIdentity vSalvoconductoNewIdentity = ConsultaSalvoconductoXSalvoconductoId(SALVOCONDUCTO_ID);
            respuesta = vSalvoconductoNewDalc.GrabarBloqueoSalvoconducto(SALVOCONDUCTO_ID, TIPO_BLOQUEO_ID, USUARIO, autoridadCambiaEstado);
            if (respuesta == true)
            {
                EnviarCorreoBloqueoSalvoconducto(ref vSalvoconductoNewIdentity);
            }
            return respuesta;
        }
        public void GrabarDesbloqueoSalvoconducto(int salvoconductoID, string motivoDesbloqueo, DateTime fechaDesbloqueo, string usuarioDesbloquea, int autoridadDesbloquea, byte[] soporteDesbloqueo)
        {
            vSalvoconductoNewDalc.GrabarDesbloqueoSalvoconducto(salvoconductoID, motivoDesbloqueo, fechaDesbloqueo, usuarioDesbloquea, autoridadDesbloquea, soporteDesbloqueo);
        }
        /// <summary>
        /// Verificar si el numero de salvoconducto existe en la base de datos
        /// </summary>
        /// <param name="NumeroSalvoconducto"></param>
        /// <returns></returns>
        public bool VerificarNumeroSalvoconducto(string NumeroSalvoconducto)
        {
            return vSalvoconductoNewDalc.VerificarNumeroSalvoconducto(NumeroSalvoconducto);
        }


        public DataSet ValidarSalvoconducto(int SalvoconductoId)
        {
            DataSet ds = new DataSet();
            ds = vSalvoconductoNewDalc.ValidarSalvoconductoDalc(SalvoconductoId);
            return ds;
        }


    }

}
