using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Comunicacion;
using System.Data;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.DAA;
using SILPA.LogicaNegocio.Comunicacion;
using SILPA.AccesoDatos.Generico;
using System.Xml.Serialization;
using System.Xml;
using Silpa.Workflow;
using SoftManagement.Log;
using SILPA.LogicaNegocio.BPMProcessL;
using SILPA.AccesoDatos.BPMProcess;
using SILPA.AccesoDatos.Correspondencia;
namespace SILPA.LogicaNegocio.Comunicacion
{
    public class ComunicacionEE
    {
        public ComunicacionEEType ComEEIdentity;

        public Configuracion objConfiguracion;

        /// <summary>
        /// 
        /// Constructor
        /// </summary>
        public ComunicacionEE()
        {
            objConfiguracion = new Configuracion();
        }


        public void SolicitarComunicacion(ComunicacionEEType ComEEIdentity, ref RadicacionDocumento objRadicacionSolicitud, bool respuesta) 
        {
            /// carga la autoridad ambiental:  
            //AutoridadAmbiental objAutoridad = new AutoridadAmbiental(ComEEIdentity.Id_AA_Destino);
            NumeroSilpaDalc num = new NumeroSilpaDalc();

            objRadicacionSolicitud._objRadDocIdentity.NumeroRadicacionDocumento = string.Empty;
            objRadicacionSolicitud._objRadDocIdentity.ActoAdministrativo = string.Empty;
            //objRadicacion._objRadDocIdentity.NombreFormulario = null;
            objRadicacionSolicitud._objRadDocIdentity.IdSolicitante = objConfiguracion.IdUserComunicacionEE.ToString();
            //objRadicacion._objRadDocIdentity.IdRadicacion = intIdRadicacion;
            objRadicacionSolicitud._objRadDocIdentity.IdAA = ComEEIdentity.Id_AA_Destino;
            //objRadicacion._objRadDocIdentity.FechaSolicitud = ComEEIdentity.FechaDoc;
            objRadicacionSolicitud._objRadDocIdentity.NumeroSilpa = num.ProcessInstance(ComEEIdentity.numSilpa);
            objRadicacionSolicitud._objRadDocIdentity.NumeroVITALCompleto = ComEEIdentity.numSilpa;

            List<String> lstNombres = new List<string>();
            List<Byte[]> lstBytes = new List<byte[]>();

            foreach (documentoAdjuntoType doc in ComEEIdentity.ListaDocumentoAdjuntoType.ListaDocumento)
            {
                lstNombres.Add(doc.nombreArchivo);
                lstBytes.Add(doc.bytes);
            }

            if (lstNombres.Count>0)
            {
                objRadicacionSolicitud.SetAdjuntos(lstNombres, lstBytes);
            }
            
            /// Genera el registro de solicitud de radicación de documento
            //objRadicacionSolicitud.RadicarDocumento();
            objRadicacionSolicitud.RadicarDocumentoEE(ComEEIdentity.Id_AA_Origen, respuesta, ComEEIdentity.Id_AA_Destino);

            //
            //if (respuesta)
            //{
                //SolicitudDAAEIA solDAA = new SolicitudDAAEIA();
                //solDAA.ObtenerSolicitud(null, Convert.ToInt64(objRadicacionSolicitud._objRadDocIdentity.NumeroSilpa));
                //solDAA.Identity.IdRadicacion = objRadicacionSolicitud._objRadDocIdentity.IdRadicacion;
                //solDAA.InsertarSolicitudEE();
           // }
        }

        /// <summary>
        /// carga los valores del formulario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="grupo"></param>
        /// <param name="valor"></param>
        /// <param name="orden"></param>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public ValoresIdentity CargarValor(int id, string grupo, string valor, int orden, Byte[] archivo)
        {
            ValoresIdentity objValores = new ValoresIdentity();
            objValores.Id = id;
            objValores.Grupo = grupo;
            objValores.Valor = valor;
            objValores.Orden = orden;
            objValores.Archivo = archivo;
            return objValores;
        }

        /// <summary>
        /// hava:
        /// Modificado para permitir la segunda radicación en la AA
        /// </summary>
        /// <param name="strXmlDatos"></param>
        /// <param name="bRespuesta"></param>
        public string EnviarComunicacionEE(string strXmlDatos, bool bRespuesta)
        {
            ComEEIdentity = new ComunicacionEEType();
            ComEEIdentity = (ComunicacionEEType)ComEEIdentity.Deserializar(strXmlDatos);
            string NumeroVital = "";
            long perid = -1;
            string nombreArchivo = string.Empty;
            Byte[] bytesArchivo;
            // si solicita informacion adicional a una AA
            if (ComEEIdentity.SolicitudInfoAA)
            {
                List<ValoresIdentity> objValoresList = new List<ValoresIdentity>();
                Byte[] bytes = new byte[0];
                string ValoresXML = string.Empty;
                BpmProcessLn objBPMProcess = new BpmProcessLn();

                CorrespondenciaSilpaDalc correspondencia = new CorrespondenciaSilpaDalc();

                Int32 codigo_aa = correspondencia.VerificarAutoridadAmbiental(ComEEIdentity.Str_AA_Destino);
                ComEEIdentity.Id_AA_Destino = codigo_aa;
                
                perid = ComEEIdentity.ObtenerFuncionarioInciaProceso(ComEEIdentity.Id_AA_Destino, this.objConfiguracion);
                long idAA = (long)ComEEIdentity.Id_AA_Destino;
                long TramiteId = ComEEIdentity.ObtenerTipoTramiteIniciaProceso(bRespuesta, this.objConfiguracion);
                /*
                objValoresList.Add(this.CargarValor(1, "Bas", ComEEIdentity.Id_AA_Destino.ToString(), 1, new Byte[1]));
                objValoresList.Add(this.CargarValor(2, "Bas", nombreArchivo, 2, bytes));
                
                objValoresList.Add(this.CargarValor(3, "Bas", "1", 3, new Byte[1]));
                objValoresList.Add(this.CargarValor(4, "Bas", "Proceso Comunicacion", 4, new Byte[1]));                
                 */

                                    /*
                                     * 2	2958	Bas	Tipo de trámite
                    3	2959	Bas	Número vital padre
                    4	2960	Bas	Nombre del trámite padre
                    5	2961	Bas	Expediente del trámite padre
                    6	2962	Bas	AUTORIDAD AMBIENTAL
                    7	1767	List1	Adjuntar Documento
                    8	1784	List1	Nº de Radicado
                    9	2874	List1	Descripción
                                     */

                ///
                SolicitudDAAEIA sol = new SolicitudDAAEIA();
                sol.ConsultarSolicitudNumeroSILPA(ComEEIdentity.numSilpa);

                string tipoTramite = string.Empty;
                string nombreTipoTramite = string.Empty;
                string nombreSolicitante = string.Empty;

                sol.ObtenerTipoTramite(ComEEIdentity.numSilpa, out tipoTramite, out nombreTipoTramite, out nombreSolicitante);

                /// Identificador del tipo de trámite
                objValoresList.Add(this.CargarValor(1, "Bas", nombreSolicitante, 1, new Byte[1]));
                // Tipo de trámite:
                objValoresList.Add(this.CargarValor(2, "Bas", tipoTramite, 1, new Byte[1]));
                //Número vital padre
                objValoresList.Add(this.CargarValor(3, "Bas", ComEEIdentity.numSilpa, 1, new Byte[1]));
                //Nombre del trámite padre  pendiente--
                objValoresList.Add(this.CargarValor(4, "Bas", nombreTipoTramite, 1, new Byte[1]));
                //Expediente del trámite padre
                objValoresList.Add(this.CargarValor(5, "Bas", ComEEIdentity.numExpediente, 1, new Byte[1]));
                //Autoridad Ambiental - ok
                objValoresList.Add(this.CargarValor(6, "Bas", ComEEIdentity.Id_AA_Destino.ToString(), 1, new Byte[1]));

                if (ComEEIdentity.ListaDocumentoAdjuntoType.ListaDocumento != null)
                {
                    for (int i = 1; i <= ComEEIdentity.ListaDocumentoAdjuntoType.ListaDocumento.Length; i++) 
                    {
                        nombreArchivo = ComEEIdentity.ListaDocumentoAdjuntoType.ListaDocumento[i - 1].nombreArchivo;
                        bytesArchivo = ComEEIdentity.ListaDocumentoAdjuntoType.ListaDocumento[i - 1].bytes;

                        // iterar por la lista 
                        //Adjuntar Documento
                        objValoresList.Add(this.CargarValor(7, "List1", nombreArchivo, i, bytesArchivo));
                        //Nº de Radicado
                        objValoresList.Add(this.CargarValor(8, "List1", "", i, new Byte[1]));
                        //descripcion
                        objValoresList.Add(this.CargarValor(9, "List1", "Proceso Comunicacion", i, new Byte[1]));
                        objValoresList.Add(this.CargarValor(10, "List1", "", i, new Byte[1]));
                        objValoresList.Add(this.CargarValor(11, "List1", "", i, new Byte[1]));
                    }
                }

                XmlSerializador ser = new XmlSerializador();
                ValoresXML = ser.serializar(objValoresList);

                 NumeroVital = objBPMProcess.crearProcesoAutoridad(TramiteId, perid, idAA, ValoresXML);
                  
                //System.IO.File.WriteAllText(@"E:\vital\ValoresXML.txt", ValoresXML + ">>: idAA: " + idAA.ToString() + " perid: " + perid.ToString() + " TramiteId :" + TramiteId.ToString() + " numeroVital  :" + numeroVital);
                
                ComEEIdentity.NumeroVitalSolicitaInfoAA = NumeroVital;

                ComEEIdentity.RegistrarComunicacion(ComEEIdentity.Id_AA_Origen, int.Parse(ComEEIdentity.esperarRespuesta.ToString()), this.objConfiguracion);                
               
                //ComEEIdentity.NumeroVitalSolicitaInfoAA = numeroVital;
                //ComEEIdentity.RegistrarComunicacion(ComEEIdentity.Id_AA_Destino, int.Parse(ComEEIdentity.esperarRespuesta.ToString()), this.objConfiguracion);
           }
            else 
            {   ///  codigo original de ComunicacionEE 
                //Se radica el documento
                RadicacionDocumento objRadicacionSolicitud = new RadicacionDocumento();
                NumeroSilpaDalc num = new NumeroSilpaDalc();

                SolicitarComunicacion(ComEEIdentity, ref objRadicacionSolicitud, bRespuesta);

                // Se complementa la ubicacion de los archivos con el filetraffic  
                if (ComEEIdentity.ListaDocumentoAdjuntoType.ListaDocumento != null)
                {
                    for (int i = 0; i < ComEEIdentity.ListaDocumentoAdjuntoType.ListaDocumento.Length; i++)
                    {
                        nombreArchivo = objRadicacionSolicitud._objRadDocIdentity.LstNombreDocumentoAdjunto[i];
                        ComEEIdentity.ListaDocumentoAdjuntoType.ListaDocumento[i].nombreArchivo = nombreArchivo;
                    }
                }
                // oficio, acto, concepto, resolución
                //Si la comunicacion es para Entidad Externa
                if (ComEEIdentity.EntidadExterna != null && ComEEIdentity.EntidadExterna.Length > 0)
                {
                    //Si se debe esperar respuesta de la entidad
                    EsperarRespuestaIdentity _respuesta = new EsperarRespuestaIdentity();
                    _respuesta.idRespuesta = 0;
                    //07-jun-2010 - aegb
                    if (ComEEIdentity.esperarRespuesta != string.Empty)
                        //if (Convert.ToBoolean(ComEEIdentity.esperarRespuesta))
                        if (Convert.ToBoolean(int.Parse(ComEEIdentity.esperarRespuesta)))
                        {
                            EsperarRespuestaDalc _objRespuesta = new EsperarRespuestaDalc();
                            _respuesta.numSilpa = ComEEIdentity.numSilpa;
                            _respuesta.numExpediente = ComEEIdentity.numExpediente;
                            _objRespuesta.InsertarEsperarRespuesta(ref _respuesta);
                        }

                    foreach (EntidadExternaType EET in ComEEIdentity.EntidadExterna)
                    {
                        ICorreo.Correo.EnviarComunicacionEE(ComEEIdentity, EET, bRespuesta);

                        EsperarRespuestaEntidadIdentity _respuestaEntidad = new EsperarRespuestaEntidadIdentity();
                        EsperarRespuestaEntidadDalc _objRespuestaEntidad = new EsperarRespuestaEntidadDalc();

                        //Si se debe esperar respuesta de la entidad
                        if (_respuesta.idRespuesta > 0)
                        {
                            _respuestaEntidad.idRespuesta = _respuesta.idRespuesta;
                            _respuestaEntidad.nombre = EET.nombre;
                            _respuestaEntidad.correoElectronico = EET.correoElectronico;
                            _objRespuestaEntidad.InsertarEsperarRespuesta(ref _respuestaEntidad);
                        }

                        //Si envia la respuesta a la comunicacion                     
                        if (bRespuesta)
                        {
                            //07-jul-2010 - aegb
                            _respuestaEntidad.IdEntidad = ComEEIdentity.Id_AA_Origen;
                            _respuestaEntidad.NumSilpa = ComEEIdentity.numSilpa;
                            _objRespuestaEntidad.ActualizarEsperarRespuesta(ref _respuestaEntidad);
                        }
                    }

                    /// Si es de respuesta entonces avanza la tarea en el bpm:
                    //if (bRespuesta) 
                    //{
                    string condicion = string.Empty;
                    if (ComEEIdentity.Id_TipoDocumento != -1)
                    {
                        // Se busca la condición dependiendo del tipo de documento asociado
                        TipoDocumentoDalc tipoDocDalc = new TipoDocumentoDalc();
                        condicion = tipoDocDalc.ObtenerCondicionTipoDocumento(ComEEIdentity.Id_TipoDocumento);
                    }

                    SMLog.Escribir(Severidad.Informativo, String.Format("EnviarComunicacionEE Id_TipoDocumento = {0} Condicion {1} NumSilpa {2}", ComEEIdentity.Id_TipoDocumento, condicion, ComEEIdentity.numSilpa));

                    /// si hay condición avanza la tarea
                    if (condicion != string.Empty)
                    {

                        NumeroSilpaDalc numvital = new NumeroSilpaDalc();
                        string strProcessinstance = numvital.ProcessInstance(ComEEIdentity.numSilpa);

                        // si existe el processInstance  se finaliza
                        if (!String.IsNullOrEmpty(strProcessinstance))
                        {
                            ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
                            string strMensaje = servicioWorkflow.ValidarActividadActual(Convert.ToInt64(strProcessinstance),objConfiguracion.UserFinaliza,(long)ActividadSilpa.RegistrarInformacionDocumento);
                            if (string.IsNullOrEmpty(strMensaje))
                                servicioWorkflow.FinalizarTarea(Convert.ToInt64(strProcessinstance), ActividadSilpa.RegistrarInformacionDocumento, objConfiguracion.UserFinaliza, condicion);
                            else
                                //JNS 20190822 se escribe mensaje de error
                                SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "BPM - Validacion :: ComunicacionEE::EnviarComunicacionEE - bRespuesta: " + bRespuesta.ToString() + " - strXmlDatos: \n" + (!string.IsNullOrEmpty(strXmlDatos) ? strXmlDatos : "null") + "\n\n Error: " + strMensaje, "BPM_VAL_CON");
                        }
                    }
                    //}
                }
                else
                {

                    List<PersonaIdentity> _listaPersona = new List<PersonaIdentity>();
                    PersonaDalc _objPersonaDalc = new PersonaDalc();

                    _listaPersona = _objPersonaDalc.BuscarPersonaNumeroVITAL(ComEEIdentity.numSilpa);

                    //Se envia el correo por cada persona o solicitante
                    foreach (PersonaIdentity _objPersona in _listaPersona)
                    {
                        ICorreo.Correo.EnviarComunicacionSol(ComEEIdentity, _objPersona);
                    }
                }
            }
            return NumeroVital;

        //    ComEEIdentity = new ComunicacionEEType();
        //    ComEEIdentity = (ComunicacionEEType)ComEEIdentity.Deserializar(strXmlDatos);
           
        //    //Se radica el documento
        //    RadicacionDocumento objRadicacionSolicitud = new RadicacionDocumento();
        //    NumeroSilpaDalc num = new NumeroSilpaDalc();

        //    SolicitarComunicacion(ComEEIdentity, ref objRadicacionSolicitud, bRespuesta);

        //    // Se complementa la ubicacion de los archivos con el filetraffic  
        //    if(ComEEIdentity.ListaDocumentoAdjuntoType.ListaDocumento!=null)
        //    {
        //        for (int i = 0; i < ComEEIdentity.ListaDocumentoAdjuntoType.ListaDocumento.Length;i++ )
        //        {
        //            string nombreArchivo = objRadicacionSolicitud._objRadDocIdentity.LstNombreDocumentoAdjunto[i];
        //            ComEEIdentity.ListaDocumentoAdjuntoType.ListaDocumento[i].nombreArchivo = nombreArchivo;
        //        }
        //    }
        //    // oficio, acto, concepto, resolución
        //    //Si la comunicacion es para Entidad Externa
        //    if (ComEEIdentity.EntidadExterna != null && ComEEIdentity.EntidadExterna.Length > 0)
        //    {
        //        //Si se debe esperar respuesta de la entidad
        //        EsperarRespuestaIdentity _respuesta = new EsperarRespuestaIdentity();
        //        _respuesta.idRespuesta = 0;
        //        //07-jun-2010 - aegb
        //        if (ComEEIdentity.esperarRespuesta != string.Empty)
        //            //if (Convert.ToBoolean(ComEEIdentity.esperarRespuesta))
        //            if (Convert.ToBoolean(int.Parse(ComEEIdentity.esperarRespuesta)))
        //            {
        //                EsperarRespuestaDalc _objRespuesta = new EsperarRespuestaDalc();
        //                _respuesta.numSilpa = ComEEIdentity.numSilpa;
        //                _respuesta.numExpediente = ComEEIdentity.numExpediente;
        //                _objRespuesta.InsertarEsperarRespuesta(ref _respuesta);
        //            }                    

        //        foreach (EntidadExternaType EET in ComEEIdentity.EntidadExterna)
        //        {
        //            ICorreo.Correo.EnviarComunicacionEE(ComEEIdentity, EET, bRespuesta);

        //            EsperarRespuestaEntidadIdentity _respuestaEntidad = new EsperarRespuestaEntidadIdentity();
        //            EsperarRespuestaEntidadDalc _objRespuestaEntidad = new EsperarRespuestaEntidadDalc();
                       
        //            //Si se debe esperar respuesta de la entidad
        //            if (_respuesta.idRespuesta > 0)
        //            {
        //                _respuestaEntidad.idRespuesta = _respuesta.idRespuesta;
        //                _respuestaEntidad.nombre = EET.nombre;
        //                _respuestaEntidad.correoElectronico = EET.correoElectronico;
        //                _objRespuestaEntidad.InsertarEsperarRespuesta(ref _respuestaEntidad);
        //            }

        //            //Si envia la respuesta a la comunicacion                     
        //            if (bRespuesta)
        //            {
        //                //07-jul-2010 - aegb
        //               _respuestaEntidad.IdEntidad = ComEEIdentity.Id_AA_Origen;
        //                _respuestaEntidad.NumSilpa = ComEEIdentity.numSilpa;
        //                _objRespuestaEntidad.ActualizarEsperarRespuesta(ref _respuestaEntidad);
        //            }
        //        }

        //        /// Si es de respuesta entonces avanza la tarea en el bpm:
        //        //if (bRespuesta) 
        //        //{
        //            string condicion = string.Empty;
        //            if (ComEEIdentity.Id_TipoDocumento != -1)
        //            {
        //                // Se busca la condición dependiendo del tipo de documento asociado
        //                TipoDocumentoDalc tipoDocDalc = new TipoDocumentoDalc();
        //                condicion = tipoDocDalc.ObtenerCondicionTipoDocumento(ComEEIdentity.Id_TipoDocumento);
        //            }

        //            SMLog.Escribir(Severidad.Informativo,String.Format("EnviarComunicacionEE Id_TipoDocumento = {0} Condicion {1} NumSilpa {2}", ComEEIdentity.Id_TipoDocumento, condicion, ComEEIdentity.numSilpa));

        //            /// si hay condición avanza la tarea
        //            if (condicion != string.Empty) 
        //            {

        //                NumeroSilpaDalc numvital = new NumeroSilpaDalc();
        //                string strProcessinstance = numvital.ProcessInstance(ComEEIdentity.numSilpa);

        //                // si existe el processInstance  se finaliza
        //                if (!String.IsNullOrEmpty(strProcessinstance)) 
        //                {
        //                    ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
        //                    servicioWorkflow.ValidarActividadActual(Convert.ToInt64(strProcessinstance), ActividadSilpa.RegistrarInformacionDocumento);
        //                    servicioWorkflow.FinalizarTarea(Convert.ToInt64(strProcessinstance), ActividadSilpa.RegistrarInformacionDocumento, objConfiguracion.UserFinaliza, condicion);
        //                }
        //            }
        //        //}
        //    }
        //    else
        //    {

        //        List<PersonaIdentity> _listaPersona = new List<PersonaIdentity>();
        //        PersonaDalc _objPersonaDalc = new PersonaDalc();

        //        _listaPersona = _objPersonaDalc.BuscarPersonaNumeroVITAL(ComEEIdentity.numSilpa);

        //        //Se envia el correo por cada persona o solicitante
        //        foreach (PersonaIdentity _objPersona in _listaPersona)
        //        {       
        //            ICorreo.Correo.EnviarComunicacionSol(ComEEIdentity, _objPersona);
        //        }
        //    }            
           
        }

        private bool ValidacionEtapaRadicacion(int idProcessInstance)
        {
            SILPA.AccesoDatos.Generico.RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
            return dalc.IdInstacePuedeRadicar(idProcessInstance);
        }

        public void MonitorearRespuestaEE(string numeroExpediente)
        {   
            //Consulta y monitorea que entidades no han enviado respuesta
            EsperarRespuestaDalc _objRespuesta = new EsperarRespuestaDalc();
            DataSet _respuesta = _objRespuesta.ListarEsperarRespuestaPendiente(numeroExpediente);
             
            //Se envia el correo por cada entidad o solicitante que no ha enviado respuesta
            foreach (DataRow _fila in _respuesta.Tables[0].Rows)
            {

                ICorreo.Correo.EnviarComunicacionEERespuesta(_fila["SOA_NUMERO_EXPEDIENTE"].ToString(), _fila["ERE_NOMBRE"].ToString(), _fila["ERE_CORREO_ELECTRONICO"].ToString(), _fila["ESR_FECHA_ENVIO"].ToString());

                //Se actualiza la informacion indicando que se ha enviado el correo 
                _objRespuesta.ActualizarEsperarRespuesta(Convert.ToInt32(_fila["ESR_ID"]), true);
            }           

        }

        public void ActualizarRespuestaEE(string numeroSilpa)
        {
            //Consulta y monitorea que entidades no han enviado respuesta
            EsperarRespuestaDalc _objRespuesta = new EsperarRespuestaDalc();
            DataSet _respuesta = _objRespuesta.ListarEsperarRespuestaPendiente(numeroSilpa);

            //Se envia el correo por cada entidad o solicitante que no ha enviado respuesta
            foreach (DataRow _fila in _respuesta.Tables[0].Rows)
            {

                ICorreo.Correo.EnviarComunicacionEERespuesta(_fila["SOA_NUMERO_EXPEDIENTE"].ToString(), _fila["ERE_NOMBRE"].ToString(), _fila["ERE_CORREO_ELECTRONICO"].ToString(), _fila["ESR_FECHA_ENVIO"].ToString());

                //Se actualiza la informacion indicando que se ha enviado el correo 
                _objRespuesta.ActualizarEsperarRespuesta(Convert.ToInt32(_fila["ESR_ID"]), true);
            }

        }
       
    }
}

