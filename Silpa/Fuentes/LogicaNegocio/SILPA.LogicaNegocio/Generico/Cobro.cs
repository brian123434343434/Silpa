using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;
using System.Data.SqlClient;
using SILPA.AccesoDatos.DAA;
using SILPA.Comun;
using System.Data;
using System.Configuration;

namespace SILPA.LogicaNegocio.Generico
{
    [Serializable]
    public class Cobro
    {
        public CobroIdentity objCobro;
        public List<DetalleCobroIdentity> listaDetalles;
        public Persona objPersona;
        public AutoridadAmbiental objAutAmb;



        public Cobro()
        {
            this.objCobro = new CobroIdentity();
            this.listaDetalles = new List<DetalleCobroIdentity>();
            this.objPersona = new Persona();
            this.objAutAmb = new AutoridadAmbiental();

        }

        public string cargarPrueba(string numeroSilpa)
        {
            CobroType tp = new CobroType();
            tp.idAutoridad = 62;
            tp.indicadorProceso = "abc";
            tp.numDocumento = "18703404";
            tp.numExpediente = "LIQ-2293";
            tp.numFormulario = "20093558889292";
            //tp.numSILPA = "01000018703404010165";
            tp.numSILPA = numeroSilpa;
            tp.fechaExpedicion = DateTime.Now.ToString();
            tp.fechaVencimiento = DateTime.Now.AddDays(20).ToString();
            tp.concepto = 1;
            tp.codigoBarras = "12002934765832720920398203479342002";
            DetalleCobroType[] detallecobro = new DetalleCobroType[2];
            detallecobro[0] = new DetalleCobroType();
            detallecobro[0].descripcion = "Proceso 1";
            detallecobro[0].valorCobro = 23000032;
            detallecobro[1] = new DetalleCobroType();
            detallecobro[1].descripcion = "Proceso 2";
            detallecobro[1].valorCobro = 35532300;
            tp.conceptoCobro = detallecobro;
            
            ArchivoType[] archivos = new ArchivoType[1];
            archivos[0] = new ArchivoType();
            archivos[0].nombreArchivo = "archivoConcepto.txt";
            System.IO.MemoryStream s = new System.IO.MemoryStream();
            System.IO.StreamWriter st = new System.IO.StreamWriter(s);
            st.Write("abc");
            st.Flush();
            st.Close();
            archivos[0].archivo = s.ToArray();
            tp.archivos = archivos;
            XmlSerializador ser = new XmlSerializador();
            return ser.serializar(tp);
        }

        public string CrearCobro(string xmlCobro)
        {
            try
            {
	            Comun.XmlSerializador _objSerial = new SILPA.Comun.XmlSerializador();
	            CobroType cobroxml = new CobroType();
	            cobroxml = (CobroType)_objSerial.Deserializar(new CobroType(), xmlCobro);
	            CobroIdentity cobro = new CobroIdentity();
	
	            ConceptoCobroDalc _conceptoDalc = new ConceptoCobroDalc();
	            ConceptoIdentity _conceptoIdentity = new ConceptoIdentity();
	            _conceptoIdentity.IDConcepto = cobroxml.concepto;
	            //----------------------------------------------------------------
	            //TODO: Control de Errores, que pasa cuando el concepto no exista?
	            _conceptoDalc.ObtenerConcepto(ref _conceptoIdentity);
	            //----------------------------------------------------------------
	            SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
	            SolicitudDAAEIAIdentity _solicitudIdentity = new SolicitudDAAEIAIdentity();
	            _solicitudIdentity = _solicitudDalc.ObtenerSolicitud(null, null, cobroxml.numSILPA);
	            cobro.NumSILPA = cobroxml.numSILPA;
	            cobro.NumExpediente = cobroxml.numExpediente;
	            cobro.NumReferencia = cobroxml.numFormulario;
	            cobro.IndicadorProcesoField = cobroxml.indicadorProceso;
	            cobro.NumDocumentoField = cobroxml.numDocumento;
	
	            cobro.ConceptoCobro = _conceptoIdentity;
	            cobro.NumSolicitud = _solicitudIdentity.IdSolicitud;
	
	            //------------------------------------------------------------------
	            //TODO: Control de Errores, fecha con formato incorrecto?
	            cobro.FechaExpedicion = Convert.ToDateTime(cobroxml.fechaExpedicion);
	            cobro.FechaVencimiento = Convert.ToDateTime(cobroxml.fechaVencimiento);
	            //------------------------------------------------------------------
	            cobro.CodigoBarras = cobroxml.codigoBarras;
	            
	
	            ////----------------------------------------------------------------------
	            ////Se guardan los archivos 
	            //TraficoDocumento trafico = new TraficoDocumento();
	            //if (cobroxml.archivos.Length > 0)
	            //{
	            //    //Se crea una lista de archivos
	            //    List<byte[]> listaArchivos = new List<byte[]>();
	            //    List<string> listaNombresArchivos = new List<string>();
	            //    foreach (ArchivoType archivo in cobroxml.archivos)
	            //    {
	            //        listaArchivos.Add(archivo.archivo);
	            //        listaNombresArchivos.Add(archivo.nombreArchivo);
	            //    }
	            //    string ruta = string.Empty;
	            //    trafico.RecibirDocumento(cobro.NumSILPA, _solicitudIdentity.IdSolicitante.ToString(), listaArchivos, listaNombresArchivos, ref ruta);
	            //    cobro.RutaArchivo = ruta;
	            //}
	            //----------------------------------------------------------------------
	            CobroDalc _cobroDalc = new CobroDalc();
	            _cobroDalc.InsertarCobro(ref cobro);
	
	            DetalleCobroDalc _detalleDalc = new DetalleCobroDalc();
	            foreach (DetalleCobroType dt in cobroxml.conceptoCobro)
	
	            {
	                DetalleCobroIdentity _detalleIdentity = new DetalleCobroIdentity();
	                _detalleIdentity.Descripcion = dt.descripcion;
	                _detalleIdentity.Valor = dt.valorCobro;
	                _detalleDalc.InsertarDetalle(ref _detalleIdentity, cobro.IdCobro);
	            }
	
	            return cobro.NumSILPA;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al recibir los datos del cobro entregados por la Autoridad Ambiental para generar el formulario de cobro.";
                throw new Exception(strException, ex);
            }
        }
        
        /// <summary>
        /// Inserta un cobro en el sistema
        /// </summary>
        /// <param name="objCobro">Objeto con información básica del cobro</param>
        /// <param name="objConcepto">Objeto con concepto del Cobro</param>
        /// <param name="listaCobros">Lista de Descripciones del cobro</param>
        /// <returns>True si se insertó correctamente</returns>
        private bool InstertarCobro(CobroIdentity objCobro, List<DetalleCobroIdentity> listaDetalles)
        {

            CobroDalc _cobro = new CobroDalc();
            DetalleCobroDalc _detalle = new DetalleCobroDalc();
            DetalleCobroIdentity _detIdentity = new DetalleCobroIdentity();
            _cobro.InsertarCobro(ref objCobro);
            foreach (DetalleCobroIdentity det in listaDetalles)
            {
                _detIdentity = det;
                _detalle.InsertarDetalle(ref _detIdentity, objCobro.IdCobro);
                _detIdentity = null;
            }
            return true;
        }

        /// <summary>
        /// Obtiene los valores de la persona asociados a determinado numero silpa
        /// </summary>
        /// <param name="strNumSilpa">Numero silpa</param>
        public void ObtenerPersona(string strNumSilpa)
        {
            this.objPersona.ObternerPersonaByNumeroSilpa(strNumSilpa);
        }


        /// <summary>
        ///  Consulta la informacion de pago
        /// </summary>
        /// <param name="numReferencia">numero de referencia</param>
        public string ConsultarDatosPago(string numReferencia)
        {
            CobroDalc obj = new CobroDalc();
            CobroType objCobro = new CobroType();
            objCobro = obj.ConsultarDatosPago(numReferencia);

            string xmlObject = string.Empty;
            /// serialización
            XmlSerializador ser = new XmlSerializador();
            /// Obtiene el objeto Serializado.
            xmlObject = ser.serializar(objCobro);

            return xmlObject;
        }

        /// <summary>
        ///  Consulta la informacion de pago 
        ///  17-jun-2010 - aegb: se agrega parametro para consulta de todos los registros pagados
        /// </summary>
        /// <param name="numReferencia">numero de referencia</param>
        public string ConsultarDatosPago(string numReferencia, string codigoExpediente, string estado)
        {
            CobroDalc obj = new CobroDalc();
            CobroType objCobro = new CobroType();
            objCobro = obj.ConsultarDatosPago(numReferencia, codigoExpediente, estado);

            string xmlObject = string.Empty;
            /// serialización
            XmlSerializador ser = new XmlSerializador();
            /// Obtiene el objeto Serializado.
            xmlObject = ser.serializar(objCobro);

            return xmlObject;
        }

        /// <summary>
        ///  Actualiza la informacion del estado del pago
        /// </summary>
        /// <param name="numReferencia">numero de referencia</param>
        public void ActualizarCobroEstado(CobroIdentity objIdentity)
        {
            CobroDalc obj = new CobroDalc();
            obj.ActualizarCobroEstado(objIdentity);
        }

        
        /// <summary>
        /// Acciones a ejecutar en caso que el solicitante no haya realizado el pago
        /// </summary>
        public void MonitorearPago(string numReferencia)
        {
            CobroDalc objCobro = new CobroDalc();
            CobroIdentity cobro = new CobroIdentity();
            //Se consultan los cobros
            cobro.NumReferencia = numReferencia;
            DataSet dsCobro = objCobro.ListarCobrosPago(cobro);
            DateTime fechaActual = DateTime.Now;
            int tiempoPago = 0;
            int tiempoRecordacion = 0;
            //Se consulta el tiempo de pago
            if (ConfigurationManager.ConnectionStrings["int_tiempo_pago"] != null)
                if (ConfigurationManager.ConnectionStrings["int_tiempo_pago"].ToString() != string.Empty)
                    tiempoPago = Convert.ToInt32(ConfigurationManager.ConnectionStrings["int_tiempo_pago"].ToString());
            //Se consulta el tiempo de recordacion
            if (ConfigurationManager.ConnectionStrings["int_tiempo_recordacion"] != null)
                if (ConfigurationManager.ConnectionStrings["int_tiempo_recordacion"].ToString() != string.Empty)
                    tiempoRecordacion = Convert.ToInt32(ConfigurationManager.ConnectionStrings["int_tiempo_recordacion"].ToString());

            //Se valida la informacion
            foreach (DataRow drRow in dsCobro.Tables[0].Rows)
            {
                bool enviarCorreo = false;
                //Se inicializa el objeto cobro
                cobro.IdCobro = Convert.ToDecimal(drRow["COB_ID"]);
                cobro.NumSILPA = Convert.ToString(drRow["COB_NUMERO_SILPA"]);
                cobro.NumExpediente = Convert.ToString(drRow["COB_NUMERO_EXPEDIENTE"]);
                cobro.NumReferencia = Convert.ToString(drRow["COB_REFERENCIA"]);
                cobro.FechaExpedicion = Convert.ToDateTime(drRow["COB_FECHA_EXPEDICION"]);
                cobro.FechaVencimiento = Convert.ToDateTime(drRow["COB_FECHA_VENCIMIENTO"]);
                cobro.CodigoBarras = Convert.ToString(drRow["COB_CODIGO_BARRAS"]);
                cobro.RutaArchivo = Convert.ToString(drRow["COB_RUTA_ARCHIVOS"]);
                cobro.NumSolicitud = Convert.ToInt64(drRow["SOL_ID"]);
                cobro.EstadoCobro = drRow["ECO_ID"] == null ? 0 : Convert.ToInt32(drRow["ECO_ID"]);
                cobro.Transaccion = drRow["COB_TRANSACCION"] == null ? 0 : Convert.ToInt32(drRow["COB_TRANSACCION"]);
                cobro.FechaTransaccion = drRow["COB_FECHA_TRANSACCION"] == null ? string.Empty : drRow["COB_FECHA_TRANSACCION"].ToString();
                cobro.Banco = drRow["COB_BANCO"].ToString();
                cobro.FechaRecordacion = drRow["COB_FECHA_RECORDACION"] == null ? string.Empty : drRow["COB_FECHA_RECORDACION"].ToString();
                //Se agrega el Concepto
                ConceptoIdentity concepto = new ConceptoIdentity();
                concepto.IDConcepto = Convert.ToInt32(drRow["CON_ID"]);
                ConceptoCobroDalc objConcepto = new ConceptoCobroDalc(ref concepto);
                cobro.ConceptoCobro = concepto;
               
                //Se busca si se ha pagado
                if (cobro.EstadoCobro > 0)
                {
                    //Se consulta el id del estado del cobro
                    EstadoCobro objEstadoCobro = new EstadoCobro();
                    EstadoCobroIdentity estadoCobro = new EstadoCobroIdentity();                  
                    estadoCobro.IdEstadoCobro = cobro.EstadoCobro ;
                    objEstadoCobro.ConsultarEstadoCobro(ref estadoCobro);
                    if (estadoCobro.Nombre != "OK")
                        enviarCorreo = true;
                }
                else
                    enviarCorreo = true;

                //Si no se ha pagado
                if (enviarCorreo)
                {       
                       //Se busca la persona y autoridad ambiental
                        PersonaDalc objPersonaDalc = new PersonaDalc();
                        List<PersonaIdentity> listaPersona = new List<PersonaIdentity>();
                        FormularioDalc objFormulario = new FormularioDalc();
                        DataSet dtFormulario = new DataSet();
                        SolicitudDAAEIAIdentity daaSolicitud = new SolicitudDAAEIAIdentity();
                        SolicitudDAAEIADalc objDaaSolicitud = new SolicitudDAAEIADalc();
                       
                       //Se obtiene la solicitud
                       daaSolicitud.IdSolicitud = cobro.NumSolicitud;
                       objDaaSolicitud.ObtenerSolicitud(ref daaSolicitud);
                       //se obtiene la informacion del nombre proyecto
                       dtFormulario = objFormulario.ObtenerDatosFormulariosProceso(daaSolicitud.IdProcessInstance, "Nombre del Proyecto");
                       string nombreProyecto = dtFormulario.Tables[0].Rows[0]["VALOR"].ToString();
                       //Se buscan las personas
                       listaPersona = objPersonaDalc.BuscarPersonaNumeroVITAL(cobro.NumSILPA);
                                            
                       //Si es correo de cobro por vencimiento
                       if (fechaActual < cobro.FechaVencimiento.AddDays(tiempoPago))
                        {
                            //Se envia correo de vencimiento al solicitante y a la autoridad ambiental
                            //Se obtiene la autoridad ambiental
                            AutoridadAmbientalIdentity autoridad = new AutoridadAmbientalIdentity();
                            AutoridadAmbientalDalc objAutoridad = new AutoridadAmbientalDalc();
                            autoridad.IdAutoridad = daaSolicitud.IdAutoridadAmbiental == null ? 0 : Convert.ToInt32(daaSolicitud.IdAutoridadAmbiental);
                            objAutoridad.ObtenerAutoridadById(ref autoridad);

                            //objAutoridad.ObtenerAutoridadNoIntegradaById(ref autoridad);

                            //Se envia el correo por cada persona o solicitante
                            foreach (PersonaIdentity persona in listaPersona)
                                ICorreo.Correo.EnviarComunicacionVencimiento(cobro, persona, nombreProyecto);
                            //Se envia el correo a la autoridad ambiental
                            ICorreo.Correo.EnviarComunicacionVencimiento(cobro, autoridad, listaPersona, nombreProyecto);
                            //Se actualiza la fecha de envio de correo
                            objCobro.ActualizarCobroRecordacion(cobro);
                        }
                        else if (cobro.FechaVencimiento.AddDays(-tiempoRecordacion) <= fechaActual)
                        {
                            bool enviarRecordacion = false;
                            if (cobro.FechaRecordacion != string.Empty)
                            {
                                if (Convert.ToDateTime(cobro.FechaRecordacion) < fechaActual)
                                    enviarRecordacion = true;
                            }
                            else
                                enviarRecordacion = true;

                            if (enviarRecordacion)
                            {
                                //Se envia correo de recordacion por cada persona o solicitante
                                foreach (PersonaIdentity persona in listaPersona)
                                    ICorreo.Correo.EnviarComunicacionRecordatorio(cobro, persona, nombreProyecto);
                                //Se actualiza la fecha de recordacion
                                objCobro.ActualizarCobroRecordacion(cobro);
                            }
                        }                   
                }
            }
        }

        /// <summary>
        /// Obtiene los valores para la acutoriad ambiental especificada
        /// </summary>
        /// <param name="intIdAutoridadAmbiental">Identificador de la acutoriad ambiental</param>
        public void ObtenerAutoridadAmbiental(int intIdAutoridadAmbiental)
        {
            this.objAutAmb.ObtenerAutoridadAmbiental(intIdAutoridadAmbiental);
        }

        /// <summary>
        /// Obtiene los valores para la acutoriad ambiental especificada
        /// </summary>
        /// <param name="intIdAutoridadAmbiental">Identificador de la acutoriad ambiental</param>
        public void ObtenerNumeroSilpa(int intProcessInstance)
        {
            this.objAutAmb.ObtenerAutoridadAmbiental(intProcessInstance);
        }


        /// <summary>
        /// Obtener la información de cobro perteneciente a la referencia especificada
        /// </summary>
        /// <param name="p_strNumeroReferencia">string con el número de referencia</param>
        /// <returns>CobroIdentity con la información del cobro</returns>
        public CobroIdentity ObtenerCobroTransaccion(string p_strNumeroReferencia)
        {
            CobroDalc objCobroDalc = new CobroDalc();
            return objCobroDalc.ObtenerCobroTransaccion(p_strNumeroReferencia);
        }


        /// <summary>
        /// Obtiene los valores de los objetos a partir del identificador del proceso
        /// </summary>
        /// <param name="intProcessInstance">identificador del proceso</param>
        public void ObtenerValoresObjetos(int intProcessInstance)
        {
            CobroDalc _cobroDalc = new CobroDalc();
            SolicitudDAAEIADalc objSolDalc = new SolicitudDAAEIADalc();
            this.objPersona.ObternerPersonaByProcessInstace(intProcessInstance);

            //this.objAutAmb.ObtenerAutoridadAmbiental(this.objPersona.Identity.IdAutoridadAmbiental);
            
            this.objCobro = new CobroIdentity();
            //this.objCobro.NumSILPA = objSolDalc.ObtenerSolicitud(null, intProcessInstance, null).NumeroSilpa;

            SolicitudDAAEIAIdentity solIdentity = objSolDalc.ObtenerSolicitud(null, intProcessInstance, null);
            this.objCobro.NumSILPA = solIdentity.NumeroSilpa;
            int idAutoridad = (int)solIdentity.IdAutoridadAmbiental;

            this.objAutAmb.ObtenerAutoridadAmbiental(idAutoridad);

            _cobroDalc.ObtenerCobro(ref this.objCobro);
        }


        /// <summary>
        /// Obtener la información del cobro por identificador del cobro
        /// </summary>
        /// <param name="intCobroID">int conn el identificador del cobro</param>
        public void ObtenerValoresCobroAutoliquidacion(int intCobroAutoliquidacionID)
        {
            CobroDalc _cobroDalc = new CobroDalc();
            _cobroDalc.ObtenerCobroAutoliquidacion(intCobroAutoliquidacionID, ref this.objCobro);

            //Cargar datos de la autoridad ambiental
            this.objAutAmb.ObtenerAutoridadAmbiental(this.objCobro.AutoridaAmbientalID);

            //Cargar datos de la persona
            this.objPersona.ObternerPersonaByUserIdApp(this.objCobro.SolicitanteID.ToString());            
        }

        public DataTable ListarCobros(string strNumSILPA)
        {
            CobroDalc _cobroDalc = new CobroDalc();
            this.objCobro = new CobroIdentity();
            this.objCobro.NumSILPA = strNumSILPA;
            return _cobroDalc.ListarCobrosPago(this.objCobro).Tables[0];
        }

        public DataTable ListarCobros(string strNumSILPA, int idUsuario)
        {
            CobroDalc _cobroDalc = new CobroDalc();
            this.objCobro = new CobroIdentity();
            this.objCobro.IdCobro = -1;
            this.objCobro.NumExpediente = "-1";
            this.objCobro.NumSolicitud = -1;
            this.objCobro.NumReferencia = "-1";
            this.objCobro.NumSILPA = strNumSILPA;
            return _cobroDalc.ListarCobrosPago(this.objCobro, idUsuario).Tables[0];
        }

        public List<CobroPSE> ConsultarDatosPagoPSE(string IdExpediente)
        {
            CobroPSE obj = new CobroPSE();
            
            CobroDalc _cobroDalc = new CobroDalc();
            this.objCobro.NumExpediente = IdExpediente;
            return _cobroDalc.ListarCobrosPagoPSE(this.objCobro);

        }


        /// <summary>
        /// Obtener el listado de cobros vencidos pertenecientes a un origen especifico
        /// </summary>
        /// <param name="p_strOrigenCobro">string con el origen del cobro</param>
        /// <returns>List con la información de los cobros vencidos</returns>
        public List<CobroIdentity> ObtenerListadoCobrosVencidos(string p_strOrigenCobro)
        {
            CobroDalc _cobroDalc = new CobroDalc();
            return _cobroDalc.ObtenerListadoCobrosVencidos(p_strOrigenCobro);
        }


        /// <summary>
        /// Retorna el listado de transacciones pendientes de pago
        /// </summary>
        /// <returns>List con la informacion de las transacciones pendientes</returns>
        public List<TransaccionPSEIdentity> PagosPendientes()
        {
            CobroDalc _cobroDalc = new CobroDalc();            
            return _cobroDalc.ListaPagosPendientes();
        }

        /// <summary>
        /// Retornar el listado de transacciones PSE realizados por el usuario que cumplan con las condiciones de busqueda
        /// </summary>
        /// <param name="p_intUsuarioId">int con el identificador del usuario</param>
        /// <param name="p_strNumeroVital">string con el numero vital. Opcional</param>
        /// <param name="p_lngNumeroTransaccion">long con el numero de transaccion. Opcional (-1)</param>
        /// <param name="p_objFechaInicial">DateTime con la fecha inicial. Opcional</param>
        /// <param name="p_objFechaFinal">DateTime con la fecha final. Opcional</param>
        /// <returns>DataTable con la información de las transacciones</returns>
        public DataTable ListarTransaccionesPSEUsuario(int p_intUsuarioId, string p_strNumeroVital, long p_lngNumeroTransaccion, DateTime p_objFechaInicial, DateTime p_objFechaFinal)
        {
            CobroDalc _cobroDalc = new CobroDalc();
            return _cobroDalc.ListarTransaccionesPSEUsuario(p_intUsuarioId, p_strNumeroVital, p_lngNumeroTransaccion, p_objFechaInicial, p_objFechaFinal);
        }


        /// <summary>
        /// Crea un nuevo registro de transacción PSE con los datos especificados
        /// </summary>
        /// <param name="p_objTransaccioPSE">TransaccionPSEIdentity con la informacion de la transacción</param>
        /// <returns>long con el identificador local de la transacción</returns>
        public long CrearTransaccionPSE(TransaccionPSEIdentity p_objTransaccioPSE)
        {
            CobroDalc _cobroDalc = new CobroDalc();
            return _cobroDalc.CrearTransaccionPSE(p_objTransaccioPSE);
        }


        /// <summary>
        /// Actualizar la información de un atrnsacción PSE
        /// </summary>
        /// <param name="p_objTransaccioPSE">TransaccionPSEIdentity con la informacion de la transacción</param>        
        public void ActualizarTransaccionPSE(TransaccionPSEIdentity p_objTransaccioPSE)
        {
            CobroDalc _cobroDalc = new CobroDalc();
            _cobroDalc.ActualizarTransaccionPSE(p_objTransaccioPSE);
        }

        /// <summary>
        /// Crea un nuevo registro de detalle de transacción PSE con los datos especificados
        /// </summary>
        /// <param name="p_objDetalleTransaccioPSE">DetalleTransaccionPSEIdentity con la informacion de la transacción</param>
        public void CrearDetalleTransaccionPSE(DetalleTransaccionPSEIdentity p_objDetalleTransaccioPSE)
        {
            CobroDalc _cobroDalc = new CobroDalc();
            _cobroDalc.CrearDetalleTransaccionPSE(p_objDetalleTransaccioPSE);
        }

        /// <summary>
        /// Consultar la información de la transacción por CUS
        /// </summary>
        /// <param name="p_lngCUS">long con el identificador unico CUS</param>
        /// <returns>TransaccionPSEIdentity con la información de la transacción</returns>
        public TransaccionPSEIdentity ConsultarInformacionTransaccion(long p_lngCUS)
        {
            CobroDalc _cobroDalc = new CobroDalc();
            return _cobroDalc.ConsultarInformacionTransaccion(p_lngCUS);
        }
    }
}
