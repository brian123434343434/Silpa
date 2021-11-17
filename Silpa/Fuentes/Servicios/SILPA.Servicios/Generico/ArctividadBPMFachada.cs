using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using SILPA.LogicaNegocio.DAA;
using SILPA.Servicios.Generico;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using Silpa.Workflow;
using SoftManagement.Log;

namespace SILPA.Servicios
{
    /// <summary>
    /// Procesa el XML entregado el BPM en las Condiciones de la Actividad
    /// </summary>
    public class ArctividadBPMFachada
    {


        public ArctividadBPMFachada()
        {

        }

        /// <summary>
        /// Procesa el XML entregado BPM para obtener el estado de la solicitud
        /// </summary>
        /// <param name="mensaje">XML de la Actividad</param>
        /// <returns>Estado de la solicitud</returns>
        //public static string ProcesarEstadoDAAXML(string mensaje)
        public string ProcesarEstadoDAAXML(string mensaje)
        {
            //archivoX(mensaje);
            XmlDocument _xmlActividad = new XmlDocument();
            long IDProcessInstance = 0;
            int operationalValue = 0;
            int estado = 0;
            XmlNodeList _xmlActivityInstance;
            XmlNodeList _xmlDatosActividad;
            _xmlActividad.LoadXml(mensaje);
            _xmlActivityInstance = _xmlActividad.GetElementsByTagName("ActivityInstance");

            foreach (XmlNode _xmlnode in _xmlActivityInstance)
            {
                _xmlDatosActividad = _xmlnode.ChildNodes;
                foreach (XmlNode _xmlDatoActividad in _xmlDatosActividad)
                {
                    if (_xmlDatoActividad.Name == "IDProcessInstance")
                    {
                        IDProcessInstance = Convert.ToInt64(_xmlDatoActividad.InnerText.Trim());
                    }
                    if (_xmlDatoActividad.Name == "OptionalValue")
                    {
                        operationalValue = Convert.ToInt32(_xmlDatoActividad.InnerText.Trim());
                    }
                }
            }
            SolicitudDAAEIA _daaeia = new SolicitudDAAEIA();
            _daaeia.ConsultarSolicitudByProcessInstance(IDProcessInstance);
            estado = _daaeia.Identity.IdTipoEstadoSolicitud;

            /// Esta logida debe desaparecer...  se hizo para permitir el flujo del proceso
            /// 
            if (estado == 0) 
            { 
                estado = 1;
            }

            StringBuilder x = new StringBuilder();
            x.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            x.Append("<response>");

            if (operationalValue == estado)
            {
               x.Append("<operationResult>True</operationResult>");
            }  
            else
                x.Append("<operationResult>False</operationResult>");
            //Se continua la construcción del XML
            x.Append("<operationType>");
            x.Append("<code>0</code>");
            x.Append("<name>RecibirXML</name>");
            x.Append("</operationType>");
            x.Append("<operationResponse>");
            x.Append("<SystemData>");
            x.Append("<DataType>Integer</DataType>");
            x.Append("<DataValue>" + estado.ToString().Trim() + "</DataValue>");
            //x.Append("<DataValue>3</DataValue>");
            x.Append("</SystemData>");
            x.Append("</operationResponse>");
            x.Append("<errorMessage>");
            x.Append("<code></code>");
            x.Append("<message></message>");
            x.Append("</errorMessage>");
            x.Append("</response>");
            //archivoX(x.ToString());
            return x.ToString();

        }

        //public static void archivoX(string mensaje)
        //{
        //    //------------------- INICIO DE ESCRITURA TEMPORAL (ELIMINAR) -------------------------
        //    using (StreamWriter archivo = new StreamWriter(@"c:\temp\logGattaca.txt", true))
        //    {
        //        archivo.WriteLine("------------------------------------------------------------------------");
        //        archivo.WriteLine(mensaje);
        //    }
        //    //----- FIN DE ESCRITURA --------------------------------------------------------------

        //}
        /// <summary>
        /// Determina si la actividad actual del proceso de BPM para el número SILPA dado
        /// se encuentra contenida en la tabla BPM_PARAMETROS y la avanza a la siguiente actividad
        /// </summary>
        /// <param name="numeroSILPA">Número SILPA de la tabla de solicitud</param>
        public string DeterminarAvanceActividad(string numeroSILPA, string usuario)
        {

            SolicitudDAAEIA _solicitud = new SolicitudDAAEIA();
            _solicitud.ConsultarSolicitudNumeroSILPA(numeroSILPA);

            ServicioWorkflow servicioWorkflow = new ServicioWorkflow();

            string mensaje = servicioWorkflow.ValidarActividadActual(_solicitud.Identity.IdProcessInstance, usuario, (long)ActividadSilpa.ConsultarPago);
            if (mensaje == "")
                servicioWorkflow.FinalizarTarea(_solicitud.Identity.IdProcessInstance, ActividadSilpa.ConsultarPago, usuario);
            else
            {
                //JNS 20190822 se escribe mensaje de error y se inicializa mensaje en vacio para no detener el proceso
                SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "BPM - Validacion :: ActividadBPMFachada::DeterminarAvanceActividad - numeroSILPA: " + (!string.IsNullOrEmpty(numeroSILPA) ? numeroSILPA : "null") + " - usuario: " + (!string.IsNullOrEmpty(usuario) ? usuario : "null") + "\n\n Error: " + mensaje, "BPM_VAL_CON");
                mensaje = "";
            }
            
            return mensaje;

        }


        public static string DeterminarAvanceActividadSalvoconducto(string numeroSILPA, string usuario)
        {

            SolicitudDAAEIA _solicitud = new SolicitudDAAEIA();
            _solicitud.ConsultarSolicitudNumeroSILPA(numeroSILPA);

            ServicioWorkflow servicioWorkflow = new ServicioWorkflow();

            string mensaje= servicioWorkflow.ValidarActividadActual(_solicitud.Identity.IdProcessInstance, usuario, (long) ActividadSilpa.RecibirDatosSalvoconducto);
            if (mensaje=="")
                servicioWorkflow.FinalizarTarea(_solicitud.Identity.IdProcessInstance, ActividadSilpa.RecibirDatosSalvoconducto, usuario);
            else
            {
                //JNS 20190822 se escribe mensaje de error y se inicializa mensaje en vacio para no detener el proceso
                SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "BPM - Validacion :: ActividadBPMFachada::DeterminarAvanceActividadSalvoconducto - numeroSILPA: " + (!string.IsNullOrEmpty(numeroSILPA) ? numeroSILPA : "null") + " - usuario: " + (!string.IsNullOrEmpty(usuario) ? usuario : "null") + "\n\n Error: " + mensaje, "BPM_VAL_CON");
                mensaje = "";
            }

            return mensaje;        
        }

        /// <summary>
        /// Método que avanza la tarea
        /// </summary>
        /// <param name="xmlDocumentos">objeto notificacion en formato xml</param>
        /// <param name="numeroSILPA">numero silpa</param>
        /// <param name="usuario">usuario que adelanta la actividad</param>
        public static string DeterminarAvanceActividad(NotificacionType xmlDocumentos, string numeroSILPA, string usuario)
        {
            Comun.XmlSerializador _serializer = new SILPA.Comun.XmlSerializador();
            NotificacionType objNotificacion = new NotificacionType();
            objNotificacion = xmlDocumentos;

            string condicion = string.Empty;
            

            if (!String.IsNullOrEmpty(objNotificacion.tipoActoAdministrativo))
            {
                TipoDocumentoDalc tipoDocDalc = new TipoDocumentoDalc();
                condicion = tipoDocDalc.ObtenerCondicionTipoDocumento(int.Parse(objNotificacion.tipoActoAdministrativo));
            }


            SolicitudDAAEIA _solicitud = new SolicitudDAAEIA();
            _solicitud.ConsultarSolicitudNumeroSILPA(numeroSILPA);

            ServicioWorkflow servicioWorkflow = new ServicioWorkflow();

            SILPA.LogicaNegocio.AdmTablasBasicas.TipoTramite objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.TipoTramite();
            System.Data.DataTable dtCondicion = objTablasBasicas.ListarCondicionesEspeciales( null, condicion, null);
            string mensaje="";
            if (dtCondicion.Rows.Count == 0)
            {
                //JNS 20190822 Se realiza ajusta para que en caso de error se escribe mensaje en log y se limpia mensaje para romper flujo de BPM
                mensaje = servicioWorkflow.ValidarActividadActual(_solicitud.Identity.IdProcessInstance, usuario,(long) ActividadSilpa.RegistrarInformacionDocumento);
                if (string.IsNullOrEmpty(mensaje))
                    servicioWorkflow.FinalizarTarea(_solicitud.Identity.IdProcessInstance, ActividadSilpa.RegistrarInformacionDocumento, usuario, condicion);
                else
                {
                    SMLog.Escribir(Severidad.Informativo, "BPM - Validacion :: ActividadBPMFachada::DeterminarAvanceActividad - numeroSILPA: " + (!string.IsNullOrEmpty(numeroSILPA) ? numeroSILPA : "null") +
                                                              " - usuario: " + (!string.IsNullOrEmpty(usuario) ? usuario : "null") +
                                                              " - numActoAdministrativo: \n" + (xmlDocumentos != null ? xmlDocumentos.numActoAdministrativo : "null") +
                                                              " - tipoActoAdministrativo: \n" + (xmlDocumentos != null ? xmlDocumentos.tipoActoAdministrativo : "null") +
                                                              "\n\n Error: " + mensaje, "BPM_VAL_CON");
                    mensaje = "";
                }

            }
            return mensaje;

        }


        public static string DeterminarAvanceActividadV(NotificacionType xmlDocumentos, string numeroSILPA, string usuario)
        {
            try
            {
	            NotificacionType objNotificacion = new NotificacionType();
	            objNotificacion = xmlDocumentos;

	            SolicitudDAAEIA _solicitud = new SolicitudDAAEIA();
	            _solicitud.ConsultarSolicitudNumeroSILPA(numeroSILPA);
	
	            string condicion = string.Empty;
	
	            if (!String.IsNullOrEmpty(objNotificacion.tipoActoAdministrativo))
	            {
	                TipoDocumentoDalc tipoDocDalc = new TipoDocumentoDalc();
	                condicion = tipoDocDalc.ObtenerCondicionTipoDocumento(int.Parse(objNotificacion.tipoActoAdministrativo));
	            }
	
	            SILPA.LogicaNegocio.AdmTablasBasicas.TipoTramite objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.TipoTramite();
	            System.Data.DataTable dtCondicion = objTablasBasicas.ListarCondicionesEspeciales(null, condicion, null);
	            string mensaje = "";
	            ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
	            if (dtCondicion.Rows.Count == 0)
	            {
                    //JNS 20190822 Se realiza ajuste para saltar procesos de BPM, en caso de falla se escribe en log y se limpia mensaje para que continue procesos
	                mensaje = servicioWorkflow.ValidarActividadActualCondicion(_solicitud.Identity.IdProcessInstance, usuario, condicion);
                    if (!string.IsNullOrEmpty(mensaje))
                    {
                        SMLog.Escribir(Severidad.Informativo, "BPM - Validacion :: ActividadBPMFachada::DeterminarAvanceActividadV - numeroSILPA: " + (!string.IsNullOrEmpty(numeroSILPA) ? numeroSILPA : "null") + 
                                                              " - usuario: " + (!string.IsNullOrEmpty(usuario) ? usuario : "null") + 
                                                              " - numActoAdministrativo: \n" + (xmlDocumentos != null ? xmlDocumentos.numActoAdministrativo : "null") +
                                                              " - tipoActoAdministrativo: \n" + (xmlDocumentos != null ? xmlDocumentos.tipoActoAdministrativo : "null") +
                                                              "\n\n Error: " + mensaje, "BPM_VAL_CON");
                        mensaje = "";
                    }
	            }
	            return mensaje;
			}
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Determinar el Avance de la Actividad.";
                throw new Exception(strException, ex);
            }
        }


    }
}
