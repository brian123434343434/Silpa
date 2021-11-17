using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using SILPA.LogicaNegocio.Generico;
using System.Xml;
using System.Data;
using SILPA.LogicaNegocio.ICorreo;
using SILPA.LogicaNegocio.DAA;
using Silpa.Workflow;
using SoftManagement.Log;

namespace SILPA.Servicios.Generico.RadicarDocumento
{
    /// <summary>
    /// Fachada para la radicacion de los documentos.
    /// </summary>
    public class RadicacionDocumentoFachada
    {
        public RadicacionDocumento objRadicacion;
        private Configuracion objConfiguracion;
        public AutoridadAmbiental objAutoridad;

        /// <summary>
        /// constructor 
        /// </summary>
        public RadicacionDocumentoFachada()
        {
            objRadicacion = new RadicacionDocumento();
            objConfiguracion = new Configuracion();
        }
        
        /// <summary>
        /// Constructor de los objetos de radicacion de documentos
        /// </summary>
        /// <param name="strNumeroSilpa"></param>
        /// <param name="strNumeroRadicacionDocumento"></param>
        /// <param name="strActoAdministrativo"></param>
        /// <param name="intNumeroFormulario"></param>
        /// <param name="intIdSolicitante"></param>
        /// <param name="intIdRadicacion"></param>
        /// <param name="strDocumentoAdjunto"></param>
        public RadicacionDocumentoFachada(string strNumeroSilpa, string strNumeroRadicacionDocumento,
                                          string strActoAdministrativo, int intNumeroFormulario,
                                          int intIdSolicitante, int intIdRadicacion,
                                          string strDocumentoAdjunto, byte[] bteDocumentoAdjunto)
        {
            objRadicacion = new RadicacionDocumento();
        }

        /// <summary>
        /// Constructor del objeto de radicacion de documentos
        /// </summary>
        /// <param name="strNumeroSilpa">Numero silpa</param>
        /// <param name="strNumeroRadicacionDocumento">Numero de radicacion del documento fisico en la AA</param>
        /// <param name="strActoAdministrativo"> identificador del acto administrativo</param>
        /// <param name="intNumeroFormulario">Identificador del formulario FormBuilder asociado</param>
        /// <param name="intIdSolicitante">identificador del solicitante  cuando esta registrado</param>
        /// <param name="intIdRadicacion"></param>
        /// <param name="lstStrDocumentoAdjunto">lista de los nombres de los archivos adjuntos</param>
        /// <param name="lstBteDocumentoAdjunto">lista de archivos adjunto en bytes</param>
        /// <param name="IdAA">identificador de la autoridad ambiental</param>
        public RadicacionDocumentoFachada(string strNumeroSilpa,
                                          string strNumeroRadicacionDocumento,
                                          string strActoAdministrativo,
                                          int intNumeroFormulario,
                                          string intIdSolicitante,
                                          int intIdRadicacion,
                                          List<string> lstStrDocumentoAdjunto,
                                          List<byte[]> lstBteDocumentoAdjunto,
                                          int? IdAA, int intTipoDocumentoRadicado, string strIdSolicitante
                                        )
        {

            /// contiene la configuracion del web.config
            objConfiguracion = new Configuracion();

            /// carga la autoridad ambiental:
            if (IdAA.HasValue && IdAA.Value != 0)
                objAutoridad = new AutoridadAmbiental(IdAA.Value);

            objRadicacion = new RadicacionDocumento();

            this.objRadicacion._objRadDocIdentity.NumeroSilpa = strNumeroSilpa;
            this.objRadicacion._objRadDocIdentity.NumeroRadicacionDocumento = strNumeroRadicacionDocumento;
            this.objRadicacion._objRadDocIdentity.ActoAdministrativo = strActoAdministrativo;
            this.objRadicacion._objRadDocIdentity.NumeroFormulario = intNumeroFormulario;
            this.objRadicacion._objRadDocIdentity.NombreFormulario = null;
            this.objRadicacion._objRadDocIdentity.IdSolicitante = intIdSolicitante;
            this.objRadicacion._objRadDocIdentity.IdRadicacion = intIdRadicacion;
            this.objRadicacion._objRadDocIdentity.IdAA = IdAA;
            this.objRadicacion._objRadDocIdentity.IdSolicitante = strIdSolicitante;


            // oficio, acto, concepto, resolución
            this.objRadicacion._objRadDocIdentity.TipoDocumento = intTipoDocumentoRadicado;

            // Nombre del documento(s) adjunto(s)
            this.objRadicacion._objRadDocIdentity.LstNombreDocumentoAdjunto = lstStrDocumentoAdjunto;
            this.objRadicacion._objRadDocIdentity.LstBteDocumentoAdjunto = lstBteDocumentoAdjunto;
            if (this.objRadicacion._objRadDocIdentity.LstBteDocumentoAdjunto != null)
            {
                for (int i = 0; i < objRadicacion._objRadDocIdentity.LstNombreDocumentoAdjunto.Count; i++)
                {
                    this.objRadicacion._objRadDocIdentity.LstNombreDocumentoAdjunto[i] = objConfiguracion.FileTraffic + this.objRadicacion._objRadDocIdentity.LstNombreDocumentoAdjunto[i];
                }
            }
        }
        
        /// <summary>
        /// Constructor del objeto de radicacion de documentos cuando los archivos se encuentran en la base de datos del bpm
        /// </summary>
        /// <param name="strNumeroSilpa">Numero silpa</param>
        /// <param name="strNumeroRadicacionDocumento">Numero de radicacion del documento fisico en la AA</param>
        /// <param name="strActoAdministrativo"> identificador del acto administrativo</param>
        /// <param name="intNumeroFormulario">Identificador del formulario FormBuilder asociado</param>
        /// <param name="intIdSolicitante">identificador del solicitante  cuando esta registrado</param>
        /// <param name="intIdRadicacion"></param>
        /// <param name="IdAA">identificador de la autoridad ambiental</param>
        public RadicacionDocumentoFachada(string strNumeroSilpa,
                                          string strNumeroRadicacionDocumento,
                                          string strActoAdministrativo,
                                          int intNumeroFormulario,
                                          string intIdSolicitante,
                                          int intIdRadicacion,
                                          int? IdAA, int intTipoDocumentoRadicado,
                                          string strIdSolicitante, DateTime fechaSolicitud,
                                          string numeroVITALCompleto
                                        )
        {

            /// contiene la configuracion del web.config
            objConfiguracion = new Configuracion();

            /// carga la autoridad ambiental:
            if (IdAA.HasValue && IdAA.Value != 0)
                objAutoridad = new AutoridadAmbiental(IdAA.Value);

            objRadicacion = new RadicacionDocumento();

            this.objRadicacion._objRadDocIdentity.NumeroSilpa = strNumeroSilpa;
            this.objRadicacion._objRadDocIdentity.NumeroRadicacionDocumento = strNumeroRadicacionDocumento;
            this.objRadicacion._objRadDocIdentity.ActoAdministrativo = strActoAdministrativo;
            this.objRadicacion._objRadDocIdentity.NumeroFormulario = intNumeroFormulario;
            this.objRadicacion._objRadDocIdentity.IdSolicitante = intIdSolicitante;
            this.objRadicacion._objRadDocIdentity.IdRadicacion = intIdRadicacion;
            this.objRadicacion._objRadDocIdentity.IdAA = IdAA;
            this.objRadicacion._objRadDocIdentity.FechaSolicitud = fechaSolicitud;
            this.objRadicacion._objRadDocIdentity.NumeroVITALCompleto = numeroVITALCompleto;

            this.objRadicacion._objRadDocIdentity.IdSolicitante = strIdSolicitante;

            // oficio, acto, concepto, resolución
            this.objRadicacion._objRadDocIdentity.TipoDocumento = intTipoDocumentoRadicado;

            // Obtiene los archivos del bpm para un registro especifico
            this.objRadicacion.ObtenerArchivosBPM(Convert.ToInt64(strNumeroSilpa));
            //this.objRadicacion.EliminarFilesBPM(this.objRadicacion._objRadDocIdentity.LstNombreDocumentoAdjunto);

        }

        public RadicacionDocumentoFachada(long idProcessIntance, int IdAutoridadAmbiental)
        {
            /// contiene la configuracion del web.config
            objConfiguracion = new Configuracion();

            objRadicacion = new RadicacionDocumento();

            this.objRadicacion._objRadDocIdentity.IdAA = IdAutoridadAmbiental;
            this.objRadicacion._objRadDocIdentity.NumeroSilpa = idProcessIntance.ToString();
        }

        /// <summary>
        /// Fachada para la radicación  de los documentos.
        /// </summary>
        /// <returns></returns>
        public int RadicarDocumento()
        {
            return this.objRadicacion.RadicarDocumento();
        }

        public int ActualizarRadicacion()
        {
            return this.objRadicacion.ActualizarRadicacion();
        }
        public void ActualizarRutaRadicacion(int radicacionid,string ruta)
        {
            this.objRadicacion.ActualizarRutaRadicacion(radicacionid, ruta);
        }
        
        
        /// <summary>
        /// Metodo que permite a las Autoridades Ambientales enviar el numero y fecha de radicación a SILPA 
        /// Modificado hava: 20-abr-10
        /// En modificación hava: 02-Jun-10
        /// </summary>
        /// <param name="strXmlParametros">XML: con los datos provenientes desde las autoriadad ambiental</param>
        /// <returns></returns>
        public string EnviarDatosRadicacion(string strXmlDatos, string usuario)
        {
            //Proceso _objProceso = new Proceso();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXmlDatos);
            /// El elemento de documento es autoridad
            XmlElement xmlEle = xmlDoc.DocumentElement;

            StringBuilder sbRes = new StringBuilder();

            //SILPA.Servicios.BPMServices.GattacaBPMServices9000 _servicioBPM = new SILPA.Servicios.BPMServices.GattacaBPMServices9000();

            /// --- Nueva clase para avanzar la actividad

            //Proceso objProceso;
            PersonaIdentity _objPersonaId = new PersonaIdentity();
            PersonaDalc _objPersonaDalc = new PersonaDalc();

            RadicacionDocumentoIdentity rad = new RadicacionDocumentoIdentity();
            rad = (RadicacionDocumentoIdentity)rad.Deserializar(strXmlDatos);

            int? _idAA = rad.IdAA;
            int _idRadicacion = rad.IdRadicacion;
            String _numeroRadicion = rad.NumeroRadicadoAA;
            DateTime _dtFechaRadicacion = rad.FechaRadicacion;

            sbRes = sbRes.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Resultado Valor = \" ");
            sbRes = sbRes.Append(this.objRadicacion.ActualizarDatosRadicacion(_idRadicacion, _numeroRadicion, _dtFechaRadicacion).ToString() + "\"");
            sbRes.Replace("\\", "");
            sbRes.Append("/>");

            this.objRadicacion.ObtenerRadicacion(Convert.ToInt32(_idRadicacion), null);

           SMLog.Escribir(Severidad.Informativo, "antes:  ValidacionEtapaRadicacion");

            // Identifica si la radicación es por causa de una EE..
            
            //Buscar el Idprocessinstace en Daa_Solicitud
            if (ValidacionEtapaRadicacion(Convert.ToInt32(this.objRadicacion._objRadDocIdentity.NumeroSilpa)))
            {
                _objPersonaId = _objPersonaDalc.BuscarPersonaByUserId(this.objRadicacion._objRadDocIdentity.IdSolicitante);

                /// Se envía el correo al solicitante solo si la radicacion no es de EE.
                if (this.objRadicacion._objRadDocIdentity.ID_EE == -1 ) 
                {
                    
                    if (!String.IsNullOrEmpty(this.objRadicacion._objRadDocIdentity.NumeroRadicacionDocumento))
                    {
                        SMLog.Escribir(Severidad.Informativo, "Radica:  "  + this.objRadicacion._objRadDocIdentity.NumeroRadicacionDocumento);
                        SILPA.LogicaNegocio.ICorreo.Correo objCorreo = new SILPA.LogicaNegocio.ICorreo.Correo();
                        SILPA.LogicaNegocio.ICorreo.Correo.EnviarRadicacion(this.objRadicacion._objRadDocIdentity, _objPersonaId);
                    }
                }
           
                
                SMLog.Escribir(Severidad.Informativo, "despues:  ValidacionEtapaRadicacion");

                ///El usuario se quema temporalmente....  Administrator
                ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
                string strMensaje = servicioWorkflow.ValidarActividadActual(int.Parse(this.objRadicacion._objRadDocIdentity.NumeroSilpa),usuario, (long)ActividadSilpa.Radicar);
                if (string.IsNullOrEmpty(strMensaje))
                    servicioWorkflow.FinalizarTarea(int.Parse(this.objRadicacion._objRadDocIdentity.NumeroSilpa), ActividadSilpa.Radicar, usuario);
                else
                {
                    strMensaje = servicioWorkflow.ValidarActividadActual(int.Parse(this.objRadicacion._objRadDocIdentity.NumeroSilpa), usuario, (long)ActividadSilpa.RegistrarInformacionDocumento);
                    if (string.IsNullOrEmpty(strMensaje))
                        servicioWorkflow.FinalizarTarea(int.Parse(this.objRadicacion._objRadDocIdentity.NumeroSilpa), ActividadSilpa.RegistrarInformacionDocumento, usuario);
                    else
                    {
                        //JNS 20190822 se escribe mensaje de error
                        SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "BPM - Validacion :: RadicacionDocumentoFachada::EnviarDatosRadicacion - usuario: " + (!string.IsNullOrEmpty(usuario) ? usuario : "null") + " - strXmlDatos: \n" + (!string.IsNullOrEmpty(strXmlDatos) ? strXmlDatos : "null") + "\n\n Error: " + strMensaje, "BPM_VAL_CON");
                    }
                }

            }
            return Convert.ToString(sbRes);
            
        }


        /// <summary>
        /// HAVA: 
        /// Determmina si el proceso es una queja.
        /// 25-oct-10
        /// </summary>
        /// <returns></returns>
        public bool EsRadicacionQueja(long idRadicacion) 
        {
            bool result = false;
            RadicacionDocumentoDalc rdalc =  new RadicacionDocumentoDalc();
            rdalc.EsRadicaionQueja(idRadicacion);

            
            return result;
        }

        
        /// <summary>
        /// Envía la radicación desde la funcionalidad de comunicación de entidad externa
        /// </summary>
        /// <param name="strXmlDatos"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public string EnviarDatosRadicacionEE(string strXmlDatos, string usuario)
        {
            //Proceso _objProceso = new Proceso();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXmlDatos);
            /// El elemento de documento es autoridad
            XmlElement xmlEle = xmlDoc.DocumentElement;

            StringBuilder sbRes = new StringBuilder();

            //SILPA.Servicios.BPMServices.GattacaBPMServices9000 _servicioBPM = new SILPA.Servicios.BPMServices.GattacaBPMServices9000();

            /// --- Nueva clase para avanzar la actividad

            //Proceso objProceso;
            PersonaIdentity _objPersonaId = new PersonaIdentity();
            PersonaDalc _objPersonaDalc = new PersonaDalc();

            RadicacionDocumentoIdentity rad = new RadicacionDocumentoIdentity();
            rad = (RadicacionDocumentoIdentity)rad.Deserializar(strXmlDatos);

            int? _idAA = rad.IdAA;
            int _idRadicacion = rad.IdRadicacion;
            String _numeroRadicion = rad.NumeroRadicadoAA;
            DateTime _dtFechaRadicacion = rad.FechaRadicacion;

            sbRes = sbRes.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Resultado Valor = \" ");
            sbRes = sbRes.Append(this.objRadicacion.ActualizarDatosRadicacion(_idRadicacion, _numeroRadicion, _dtFechaRadicacion).ToString() + "\"");
            sbRes.Replace("\\", "");
            sbRes.Append("/>");

            this.objRadicacion.ObtenerRadicacion(Convert.ToInt32(_idRadicacion), null);

            ///SMLog.Escribir(Severidad.Informativo, "antes:  ValidacionEtapaRadicacion");

            //Buscar el Idprocessinstace en Daa_Solicitud
            if (ValidacionEtapaRadicacion(Convert.ToInt32(this.objRadicacion._objRadDocIdentity.NumeroSilpa)))
            {
                _objPersonaId = _objPersonaDalc.BuscarPersonaByUserId(this.objRadicacion._objRadDocIdentity.IdSolicitante);

                ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
                string strMensaje = servicioWorkflow.ValidarActividadActual(int.Parse(this.objRadicacion._objRadDocIdentity.NumeroSilpa), usuario, (long)ActividadSilpa.Radicar);
                if (string.IsNullOrEmpty(strMensaje))
                    servicioWorkflow.FinalizarTarea(int.Parse(this.objRadicacion._objRadDocIdentity.NumeroSilpa), ActividadSilpa.Radicar, usuario);
                else
                    //JNS 20190822 se escribe mensaje de error
                    SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "BPM - Validacion :: RadicacionDocumentoFachada::EnviarDatosRadicacionEE - usuario: " + (!string.IsNullOrEmpty(usuario) ? usuario : "null") + " - strXmlDatos: \n" + (!string.IsNullOrEmpty(strXmlDatos) ? strXmlDatos : "null") + "\n\n Error: " + strMensaje, "BPM_VAL_CON");
            }
            return Convert.ToString(sbRes);
        }

        /// <summary>
        /// Metodo que permite a las Autoridades Ambientales enviar el numero y fecha de radicación a SILPA 
        /// </summary>
        /// <param name="strXmlParametros">XML: con los datos provenientes desde las autoriadad ambiental</param>
        /// <returns></returns>
        public void EnviarDatosRadicacion(Int64 int64ProcessInstance, string strIdForm, string strIdFormInstance)
        {
           // SoftManagement.Log.SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, " Rad - ProcessInstance: " + int64ProcessInstance.ToString() +" "+ strIdForm +" "+ strIdFormInstance);
            
            Proceso _objProceso = new Proceso();
            Formulario objFormulario = new Formulario();
            DataSet dtFormulario = new DataSet();
            this.objRadicacion.ObtenerRadicacion(null, int64ProcessInstance);
            SolicitudDAAEIA _daaeia = new SolicitudDAAEIA();
            _daaeia.ConsultarSolicitudByProcessInstance(int64ProcessInstance);
            this.objRadicacion._objRadDocIdentity.NumeroVITALCompleto = _daaeia.Identity.NumeroSilpa;
            PersonaIdentity _objPersonaId = new PersonaIdentity();
            PersonaDalc _objPersonaDalc = new PersonaDalc();

            try
            {
                if (strIdForm == Convert.ToString(_objProceso.ObtenerCodigoParametroBPM(8))) //8 ES EL IDENTIFICADOR DE LA RADICACIÓN EN LA TABLA DE PARÁMTEROS BPM formulario numero 2 en el formbuilder
                {
                    dtFormulario = objFormulario.ConsultarListadoCamposForm(Convert.ToInt32(strIdFormInstance));
                    if (dtFormulario != null)
                    {
                        if (dtFormulario.Tables[0].Rows.Count > 0)
                        {

                            _objPersonaId = _objPersonaDalc.BuscarPersonaByUserId(this.objRadicacion._objRadDocIdentity.IdSolicitante);

                            if (dtFormulario.Tables[0].Rows[0]["TEXTO"].ToString() == "Ingrese el Número de Radicación")
                            {
                                this.objRadicacion._objRadDocIdentity.NumeroRadicacionDocumento = dtFormulario.Tables[0].Rows[0]["VALOR"].ToString();

                            }
                            if (dtFormulario.Tables[0].Rows.Count > 1)
                            {
                                if (dtFormulario.Tables[0].Rows[1]["TEXTO"].ToString() == "Ingrese la Fecha de Radicación")
                                {
                                    this.objRadicacion._objRadDocIdentity.FechaRadicacion = Convert.ToDateTime(dtFormulario.Tables[0].Rows[1]["VALOR"]);
                                }
                                else
                                {
                                    this.objRadicacion._objRadDocIdentity.FechaRadicacion = DateTime.MinValue;
                                }
                            }
                            if (this.objRadicacion._objRadDocIdentity.FechaRadicacion == null || this.objRadicacion._objRadDocIdentity.FechaRadicacion == DateTime.MinValue)
                            {
                                this.objRadicacion._objRadDocIdentity.FechaRadicacion = DateTime.Now;
                            }

                            this.objRadicacion.ActualizarDatosRadicacion();
                            SILPA.LogicaNegocio.ICorreo.Correo.EnviarRadicacion(this.objRadicacion._objRadDocIdentity, _objPersonaId);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// Fachada que obtiene los datos de radicación para cada AA
        /// </summary>
        /// <param name="xmlDatosConsulta">XML: con los parámetros para obtener el conjunto de resultados</param>
        /// <returns>string: XML con el conjunto de resultados </returns>
        //public string ObtenerDatosRadicacion(int IdAA) 
        public string ObtenerDatosRadicacion(string strXmlDatos)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXmlDatos);
            /// El elemento de documento es autoridad
            XmlElement xmlEle = xmlDoc.DocumentElement;
            int idAA = int.Parse(xmlEle.Attributes["id"].Value.ToString());
            bool radicar = Convert.ToBoolean(xmlEle.Attributes["permiteRadicar"].Value);

            //return this.objRadicacion.ObtenerDatosRadicacion(idAA);
            return this.objRadicacion.ObtenerDatosRadicacion(idAA, radicar);
        }

        /// <summary>
        /// Fachada que obtiene los datos de radicación para cada AA
        /// </summary>
        /// <param name="xmlDatosConsulta">XML: con los parámetros para obtener el conjunto de resultados</param>
        /// <returns>string: XML con el conjunto de resultados </returns>
        //public string ObtenerDatosRadicacion(int IdAA) 
        public void ObtenerDatosRadicacion(int idRadicado)
        {
            //return this.objRadicacion.ObtenerDatosRadicacion(idAA);
            this.objRadicacion.ObtenerRadicacion(idRadicado, null);
        }
        
        /// <summary>
        /// Método que permite obtener los tipos de documentos asociados 
        /// de acuerdo al tipo de trámite
        /// Soft - Netco - Hava
        /// </summary>
        /// <param name="intIdTipoProceso">Int: identificador del tipo de proceso</param>
        /// <returns>DataSet con el listado de los documentos asociados al trámite</returns>
        public DataSet ObtenerDocumentosAsociados(int intIdProceso)
        {
            DataSet dsResultado = null;
            dsResultado = this.objRadicacion.ObtenerDocumentosAsociados(intIdProceso);
            return dsResultado;
        }
        
        /// <summary>
        /// Verifica si la Actividad es Radicable
        /// Genera un Registro de Radicación para esta Actividad
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="UserID"></param>
        /// <param name="activityInstanceID"></param>
        /// <param name="processInstanceID"></param>
        /// <param name="entryDataType"></param>
        /// <param name="entryData"></param>
        /// <param name="idEntryData"></param>
        public void VerificarActividadRadicable(string Client, long UserID, long activityInstanceID, long processInstanceID, string entryDataType, string entryData, string idEntryData)
        {
            //SMLog.Escribir(Severidad.Informativo, "Borrar VerificarActividadRadicable " + processInstanceID.ToString() + ',' + activityInstanceID.ToString());
            ActividadRadicable _objActividadRadicable = new ActividadRadicable();
            if (_objActividadRadicable.ObtenerActividad(activityInstanceID, processInstanceID))
            {
                ///Generara Registro de Radicación
                _objActividadRadicable.GenerarRadicacion(Client, UserID, activityInstanceID,
                    processInstanceID, entryDataType, entryData, idEntryData);
            }
        }

        private bool ValidacionEtapaRadicacion(int idProcessInstance)
        {
            SILPA.AccesoDatos.Generico.RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
            return dalc.IdInstacePuedeRadicar(idProcessInstance);
        }
        
        /// <summary>
        /// Fachada  de recibir documento para el servicio WSPQ03
        /// </summary>
        /// <param name="xmlObject"></param>
        public void RecibirDocumento(string documentoXML) 
        {
            //Se determina si el tipo de acto es oficio o acto
            //try
            //{
            //    documentoXML = documentoXML.Replace(((char)'\n'), ' ');
            //    documentoXML = documentoXML.Replace(((char)'\t'), ' ');

            //    SILPA.LogicaNegocio.Notificacion.Notificacion _objNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            //    if (_objNotificacion.DeterminarNotificacion(documentoXML))
            //    {
            //        //Se Notifica
            //        SMLog.Escribir(Severidad.Informativo, "++++Documento" + documentoXML.Substring(documentoXML.Length - 300));
            //        using (System.IO.StreamWriter esc = new System.IO.StreamWriter(@"d:\temp\Aydee.txt", true))
            //        {
            //            esc.WriteLine("*****");
            //            esc.WriteLine("Esc " + DateTime.Now.ToString());
            //            esc.WriteLine(documentoXML.ToString());
            //            esc.WriteLine("*****");
            //        }
            //        return EmitirDocumento(documentoXML);
            //    }
            //    else
            //    {
            //        //Se avanza la actividad del BPM, en caso de pertenecer a la lista de BPM_PARAMETROS
            //        //SILPA.Servicios.ArctividadBPMFachada.DeterminarAvanceActividad(_objNotificacion.EnviarCorreo(documentoXML));
            //        //en la tabla de gen_parametro vamos a tener un registro. valor = <ComunicacionEEType xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">  

            //        SILPA.AccesoDatos.Notificacion.NotificacionEntity objIdentity = _objNotificacion.ObtenerObjetoNotificacion(documentoXML);

            //        /// Permiso - Licencia - Recibir un documento desde EE
            //        /// Se determina si el tipo de documento silpa_pre.dbo.Gen_tipo_documento proviene de una Comunicacion  EE 
            //        TipoDocumentoDalc tipoDocDalc = new TipoDocumentoDalc();
            //        bool blnTipoDocumento = tipoDocDalc.ObtenerDocumentoEE(objIdentity.IdTipoActo.ID);

            //        //if (objIdentity.IdTipoActo.ID == (int)SILPA.Comun.DocumentosComunicacionEE.OficioSolicitudInformacionEELicencia || objIdentity.IdTipoActo.ID == (int)SILPA.Comun.DocumentosComunicacionEE.OficioRespuestaSolicitudInformación)
            //        if (blnTipoDocumento == true)
            //        {
            //            //string numeroSilpa = objIdentify.NumeroSILPA;
            //            SILPA.Servicios.ArctividadBPMFachada.DeterminarAvanceActividad(documentoXML, objIdentity.NumeroSILPA, DatosSesion.Usuario);
            //        }
            //        else
            //        {
            //            SILPA.Servicios.ArctividadBPMFachada.DeterminarAvanceActividad(documentoXML, _objNotificacion.EnviarCorreo(documentoXML), DatosSesion.Usuario);
            //        }

            //    }
            //    return "true";// ?
            //}
            //catch (Exception ex)
            //{
            //    SMLog.Escribir(ex);
            //    throw;
            //}
        }

        public void ConsultarAutoridad(int? IdAA)
        {
            /// carga la autoridad ambiental:
            if (IdAA.HasValue && IdAA.Value != 0)
                objAutoridad = new AutoridadAmbiental(IdAA.Value);
        }

    }
}
