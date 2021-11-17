using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using SILPA.LogicaNegocio;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos;
using SILPA.Servicios.Generico.RadicarDocumento;
using SILPA.Servicios;
using System.Data;
using SoftManagement.Log;
using SoftManagement.LogWS;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.ReporteTramite;
using SILPA.Servicios.Generico;
using SILPA.Comun;
using System.Text;
using SILPA.LogicaNegocio.DAA;
using Silpa.Workflow;
using System.Configuration;
using SILPA.AccesoDatos.DAA;

/// <summary>
/// Descripción breve de WSGN01
/// radicaicon prueba fusion MR
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSPQ03 : System.Web.Services.WebService
{

    public WSPQ03()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 

        
    }

    /// <summary>
    /// Si es acto administrativo y requiere notificación se reenvia al servicio de emitirDocumento
    /// Si es un acto administrativo y nno requiere notificación se envía el correo al solicitante
    /// Si es un oficio se envía el documento al correo del solicitante
    /// </summary>
    /// <param name="documentoXML"></param>
    /// <returns></returns>
    [WebMethod(Description = "[Recibe los datos del Documento (Acto Administrativo u Oficio) de la Autoridad Ambiental para ser expuestos o emitidos según corresponda ]", MessageName = "RecibirActoAdministrativoEIA-01")]
    public string RecibirDocumento(string documentoXML)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        string respuesta;
        //Se determina si el tipo de acto es oficio o acto
        try
        {            

            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "RecibirDocumento", documentoXML, "", 0);
            //documentoXML = documentoXML.Replace(((char)'\n'), ' ');
            //documentoXML = documentoXML.Replace(((char)'\r'), ' ');

            SILPA.LogicaNegocio.Notificacion.Notificacion _objNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();

            NotificacionType _xmlNotificacion = new NotificacionType();
            XmlSerializador _ser = new XmlSerializador();
            _xmlNotificacion = (NotificacionType)_ser.Deserializar(new NotificacionType(), documentoXML);
            //rricaurte
            //Cesion de derechos para quejas.
            _objNotificacion.DeterminarSancionatorio(ref _xmlNotificacion);

            //rricaurte
            //Cesion de derechos para quejas.
            _objNotificacion.DeterminarAudiencia(ref _xmlNotificacion);

            respuesta = NotificarVariosSolicitantes(ref _xmlNotificacion);


            return respuesta;

        }
        catch (Exception ex)
        {   
            SMLogWS.EscribirExcepcion(this.ToString(), "RecibirDocumento", ex.ToString(), iIdPadre);

            string strException = "Validar los pasos efectuados al recibir los datos del Documento (Acto Administrativo u Oficio) de la Autoridad Ambiental.";
            throw new Exception(strException, ex);
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "RecibirDocumento", documentoXML, strNoVital, iAA, iIdPadre);
        }
    }
    /// <summary>
    /// Metodo que realiza la validacion de la informacion correspondiente a recibir un documento de un sistema externo en este caso SILA.
    /// </summary>
    /// <param name="documentoXML"></param>
    /// <returns></returns>
    [WebMethod(Description = "[Recibe los datos del Documento (Acto Administrativo u Oficio) de la Autoridad Ambiental para ser validacion de informacion]", MessageName = "ValidacionRecibirDocumento")]
    public string RecibirDocumentoValidacion(string documentoXML)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        List<WSRespuesta> Lstrespuesta;
        WSRespuesta objWsrespuesta = new WSRespuesta();
        //Se determina si el tipo de acto es oficio o acto
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ValidacionRecibirDocumento", documentoXML, "", 0);
            SILPA.LogicaNegocio.Notificacion.Notificacion _objNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            NotificacionType _xmlNotificacion = new NotificacionType();
            XmlSerializador _ser = new XmlSerializador();
            _xmlNotificacion = (NotificacionType)_ser.Deserializar(new NotificacionType(), documentoXML);
            Lstrespuesta = NotificarVariosSolicitantesValidacion(ref _xmlNotificacion);
            return _ser.serializar(Lstrespuesta);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), "RecibirDocumento", ex.ToString(), iIdPadre);

            string strException = "Validar los pasos efectuados al recibir los datos del Documento (Acto Administrativo u Oficio) de la Autoridad Ambiental.";
            throw new Exception(strException, ex);
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "RecibirDocumento", documentoXML, strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "[Devuelve la informacion en xml de una solicitud]")]
    public string SolicitudXml(string numeroVital)
    {

        //SILPA.LogicaNegocio.DAA.DAA objDaa = new SILPA.LogicaNegocio.DAA.DAA();
        //return objDaa.ConsultarDatosFormulario(numeroVital);

        SILPA.LogicaNegocio.DAA.DAA objDaa = new SILPA.LogicaNegocio.DAA.DAA();
        string xmlInformacion = objDaa.ConsultarDatosFormulario(numeroVital, "D");
        return xmlInformacion;

    }


    [WebMethod(Description = "[Estructura individual de formularios diligenciados por los ciudadanos]")]
    public string EstructuraFormularioXml(string numeroVital)
    {

        SILPA.LogicaNegocio.DAA.DAA objDaa = new SILPA.LogicaNegocio.DAA.DAA();
        return objDaa.ConsultarEstructuraFormulario(numeroVital);
    }


    /// <summary>
    /// Envía los datos del Acto administrativo al Sistema de Notificación
    /// </summary>
    /// <param name="actoAdm">Documento - Acto Administrativo en XML</param>
    /// <returns>Número de Proceso de Notificación</returns>
    [WebMethod(Description = "[Consume un servicio del sistema de notificación en línea por medio de PDI para entregar los datos para el ejecutoriar el acto administrativo de un proceso de notificación]", MessageName = "EjecutoriarActo")]
    public string EjecutoriarActo(string documento, out bool respuesta)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        WSRespuesta resultado = new WSRespuesta();

        try
        { 

            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "Entra  EjecutoriarActo", "Inicia:"+documento, "", 0);            
            respuesta = false;
            /*
             ***** Asi Funciona Notificacion En linea Lo reemplazo con los metodos directos utilizados en Componentenotificacion
            NotificacionFachada not = new NotificacionFachada();
            string resp = not.ComponenteNot(SILPA.Comun.DatoComponenteNotificacion.ejecutoriar, documento);                        

            if (!String.IsNullOrEmpty(resp))
            {
                resultado = (WSRespuesta)resultado.Deserializar(resp);
                respuesta = resultado.Exito;
            }
            */
            SILPA.LogicaNegocio.Notificacion.Notificacion _objNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            string resp = _objNotificacion.Ejecutoria(documento, out respuesta);
            return resp;
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), "EjecutoriarActo", ex.ToString(), iIdPadre);

            string strException = "Validar los pasos efectuados al notificar en línea por medio de PDI para entregar los datos para ejecutoriar el Acto Administrativo de un proceso de notificación.";
            throw new Exception(strException, ex);
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "EjecutoriarActo", documento, strNoVital, iAA, iIdPadre);
        }
    }

    /// <summary>
    /// Envía los datos del Acto administrativo al Sistema de Notificación
    /// </summary>
    /// <param name="actoAdm">Documento - Acto Administrativo en XML</param>
    /// <returns>Número de Proceso de Notificación</returns>
    [WebMethod(Description = "[Consume un servicio del sistema de notificación en línea por medio de PDI para entregar los datos para el proceso de notificación de un Acto Administrativo]", MessageName = "EmitirDocumento")]
    public string EmitirDocumento(NotificacionType documento)
    {
        Int64 iIdPadre = 0;
        //Int32 iAA = 0;
        //String strNoVital = "";
        try
        {
            //iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "EmitirDocumento", documento, "", 0);
            /*
            ---Asi Funciona Notificacion en Linea lo reemplazo con la funcion a la cual esta dirigiendose el metodo en ComponenteNotificacion---
            NotificacionFachada _objNotificacion = new NotificacionFachada();            
            string salida = _objNotificacion.ComponenteNot(SILPA.Comun.DatoComponenteNotificacion.crear, documento);            
            */
            SILPA.LogicaNegocio.Notificacion.Notificacion _objNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            string salida = _objNotificacion.CrearProceso(documento);
            return salida;

            //
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), "EmitirDocumento", ex.ToString(), iIdPadre);

            string strException = "Validar los pasos efectuados al Emitir Documento.";
            throw new Exception(strException, ex);
        }
    }

    /// <summary>
    /// Proceso que invoca a consola para realizar tareas de Notificación
    /// </summary>
    /// <param name="metodo"></param>
    /// <param name="datos"></param>
    /// <returns></returns>
    //public string ComponenteNot(string metodo, string datos)
    //{
    //    string respuesta= string.Empty;
    //    Random e = new Random(100);
    //    string idTransaccion = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + e.Next(999);
    //    string ruta = @"C:\TMP\" + idTransaccion + ".txt";
    //    System.IO.File.WriteAllText(ruta, datos);
        
        
        
    //    System.Diagnostics.Process p = new System.Diagnostics.Process();
    //    p.StartInfo.UseShellExecute = false;
    //    p.StartInfo.RedirectStandardOutput = true;
    //    p.StartInfo.FileName = @"C:\TMP\ComponenteNotificacion.exe";
    //    p.StartInfo.Arguments = ruta + " " + idTransaccion+" "+metodo;
    //    //-----
    //    p.StartInfo.CreateNoWindow = true;
    //    p.StartInfo.RedirectStandardError = true;
    //    p.Start(); // Do not wait for the child process to exit before 
    //    // reading to the end of its redirected stream. 
    //    // p.WaitForExit(); 
    //    // Read the output stream first and then wait. string output = p.StandardOutput.ReadToEnd(); p.WaitForExit();
    //    p.WaitForExit();
    //    respuesta = System.IO.File.ReadAllText(ruta.Replace("txt", "") + "_" + idTransaccion + "_" + "respuesta.txt");
    //    return respuesta;
    //}

    #region Métodos relacionados con radicacion de documentos

    /// <summary>
    /// Hava
    /// Método para obtener las radicaciones desde Silpa, este método publica la información para que la consuma una Autoridad Ambiental, un Sistema de Gestión Documental o un 
    /// </summary>
    /// <param name="strXmlParametros">XML: con los datos provenientes desde SILPA, la autoridad ambiental y true o false si es un sistema de radicación o un sistema diferente </param>
    /// <returns>string: con el conjunto de resultados en formato XML, que contiene toda la información y documentos entregados con éxito para esta AA</returns>
    [WebMethod(Description = "[Expone la información y datos enviados por el solicitante, tercero interviniente o quejoso para que la AA correspondiente los consuma]")]
    public string ObtenerDatosAdjuntos(string strXmlDatos)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerDatosAdjuntos", strXmlDatos, "", 0);
            //throw new ApplicationException("hava xml:--> " + strXmlDatos );
            RadicacionDocumentoFachada objRadicacionFachada = new RadicacionDocumentoFachada();
            strXmlDatos = objRadicacionFachada.ObtenerDatosRadicacion(strXmlDatos);
            return strXmlDatos;
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerDatosAdjuntos", ex.ToString(), iIdPadre);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerDatosAdjuntos", strXmlDatos, strNoVital, iAA, iIdPadre);
        }
    }

    /// <summary>
    /// Metodo que permite a las Autoridades Ambientales enviar el numero y fecha de radicación a SILPA 
    /// ( --- debe incluir el id de radicaicon en silpa --- )
    /// </summary>
    /// <param name="strXmlParametros">XML: con los datos provenientes desde las autoriadad ambiental</param>
    /// <returns>String en formato xml con un mensaje de acuse de la ejecucion satisfactória o erronea</returns>
    [WebMethod(Description = "[La Autoridad Ambiental Envía la informacion del numero y fecha de radicación para una solicitud o Documento ]", MessageName = "EnviarDatosRadicacion")]
    public string EnviarDatosRadicacion(string strXmlDatos)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "EnviarDatosRadicacion", strXmlDatos, "", 0);

            RadicacionDocumentoFachada objRadicacionFachada = new RadicacionDocumentoFachada();
            strXmlDatos = objRadicacionFachada.EnviarDatosRadicacion(strXmlDatos, DatosSesion.Usuario);

            return strXmlDatos;
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "EnviarDatosRadicacion", ex.ToString(), iIdPadre);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "EnviarDatosRadicacion", strXmlDatos, strNoVital, iAA, iIdPadre);
        }
       
    }


    /// <summary>
    /// Metodo que permite Insertar la relacion que existe entre expedientes para la consulta de mis tramites
        /// </summary>
    /// <param name="expedienteId">String: Parametro Expediente Padre.</param>
    /// <param name="expedienteIdRef">String: Paremetro Expediente Hijo</param>
    /// <returns>True o False</returns>
    [WebMethod(Description = "[Al Asociar un expediente con otro en el sistema el registro se guarda en SILPA para ser utilizado en los reportes de mis tramites]")]
    public bool InsertarRelacionExpedientes(string expedienteId,string expedienteIdRef,string numeroVital)
    {        
        try
        {
            ReporteTramite objReporTramite = new ReporteTramite();
            return objReporTramite.CrearRelacionExpedienteExpediente(expedienteId, expedienteIdRef,numeroVital);
            
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "InsertarRelacionExpedientes", ex.ToString(), 5);
            return false;
        }
        
    }

    /// <summary>
    /// Metodo que permite Eliminar la relacion que existe entre expedientes para la consulta de mis tramites
    /// </summary>
    /// <param name="expedienteId">String: Parametro Expediente Padre.</param>
    /// <param name="expedienteIdRef">String: Paremetro Expediente Hijo</param>
    /// <returns>True o False</returns>
    [WebMethod(Description = "[Al Eliminar un expediente asociado con otro en el sistema el registro se Elimina en SILPA para ser utilizado en los reportes de mis tramites]")]
    public bool EliminarRelacionExpedientes(string expedienteId, string expedienteIdRef)
    {
        try
        {
            ReporteTramite objReporTramite = new ReporteTramite();
            return objReporTramite.EliminarRelacionExpedienteExpediente(expedienteId, expedienteIdRef);

        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "EliminarRelacionExpedientes", ex.ToString(), 5);
            return false;
        }

    }

    /// <summary>
    /// Metodo que permite Insertar los datos Necesarios para la consulta de mis tramites
    /// </summary>
    /// <param name="strParametrosXml">XML con los parametros necesarios para mis tramites.</param>    
    /// <returns>True o False</returns>
    [WebMethod(Description = "[Al Finalizar una actividad se crean crean datos necesarios para la consulta de Mis tramites]")]
    public bool CrearDatoMisTramites(string strParametrosXml)
    {
        try
        {
            SMLogWS.EscribirInicio(this.ToString(), "CrearDatoMisTramites", strParametrosXml, "", 0);
            ReporteTramites objReporte = new ReporteTramites();
            return objReporte.CrearMisTramites(strParametrosXml);
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "CrearDatoMisTramites", ex.ToString(), 0);

            string strException = "Validar los pasos efectuados al finalizar una actividad se crean datos necesarios para la consulta de Mis Trámites.";
            throw new Exception(strException, ex);

            return false;
        }

    }


    /// <summary>
    /// Metodo que permite Insertar los datos Necesarios para la consulta de mis tramites
    /// </summary>
    /// <param name="strParametrosXml">XML con los parametros necesarios para mis tramites.</param>    
    /// <returns>True o False</returns>
    [WebMethod(Description = "[Modificar la informacion del expediente de un tramite]")]
    public bool ModificarDatosExpedienteTramite(string strTramiteAnterior, string strTramiteNuevo)
    {
        try
        {
            SMLogWS.EscribirInicio(this.ToString(), "CrearDatoMisTramites", strTramiteAnterior + "\n\n" + strTramiteNuevo, "", 0);
            ReporteTramites objReporte = new ReporteTramites();
            return objReporte.ModificarDatosExpedienteTramite(strTramiteAnterior, strTramiteNuevo);
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "CrearDatoMisTramites", ex.ToString(), 0);

            string strException = "Validar los pasos efectuados al finalizar una actividad se crean datos necesarios para la consulta de Mis Trámites.";
            throw new Exception(strException, ex);

            return false;
        }

    }


    #endregion

  

   // [WebMethod(Description = "Metodo de test del servicio")]
   // public void TestRadica(Int64 resultado, string EntryData, string IDEntryData)
   // {
        //Proce:2738 EntryData:1 IDEntryData:6132toFachada 
        //RadicacionDocumentoFachada objRadFachada = new RadicacionDocumentoFachada();
        //objRadFachada.EnviarDatosRadicacion(processInstanceID, entryData, idEntryData);
       // objRadFachada.EnviarDatosRadicacion(resultado, EntryData, IDEntryData);
   // }

    /// <summary>
    /// En caso que aplique PDI, envía a la lógica de PDI.  En los demás casos, Finaliza la Tarea.
    /// </summary>
    /// <param name="documentoXML"></param>
    private string NotificarIndividual(ref NotificacionType documentoXML)
    {
        try
        {
            SILPA.LogicaNegocio.Notificacion.Notificacion _objNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            string mensaje = "";
            if (documentoXML.requiereNotificacion)
            {
                if (documentoXML.numSILPA != ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString())
                {
                    SILPA.AccesoDatos.Notificacion.NotificacionEntity objIdentity1 = _objNotificacion.ObtenerObjetoNotificacion(documentoXML);
                    mensaje = SILPA.Servicios.ArctividadBPMFachada.DeterminarAvanceActividadV(documentoXML, objIdentity1.NumeroSILPA, DatosSesion.Usuario);
                }

                if (mensaje == "")
                    return EmitirDocumento(documentoXML);
                else
                    return mensaje;
            }
            else
            {
                WSRespuesta WSR = new WSRespuesta();
                WSR.CodigoMensaje = "";
                WSR.Mensaje = string.Empty;
                WSR.IdExterno = "-1"; // no esta relacionado a una publicación
                WSR.IdSilpa = string.Empty;

                SILPA.AccesoDatos.Notificacion.NotificacionEntity objIdentity = _objNotificacion.ObtenerObjetoNotificacion(documentoXML);

                TipoDocumentoDalc tipoDocDalc = new TipoDocumentoDalc();
                bool blnTipoDocumento = tipoDocDalc.ObtenerDocumentoEE(objIdentity.IdTipoActo.ID);
                if (documentoXML.numSILPA != ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString())
                {
                    if (blnTipoDocumento == true)
                    {

                        WSR.Mensaje = SILPA.Servicios.ArctividadBPMFachada.DeterminarAvanceActividad(documentoXML, objIdentity.NumeroSILPA, DatosSesion.Usuario);
                    }
                    else
                    {
                        WSR.Mensaje = SILPA.Servicios.ArctividadBPMFachada.DeterminarAvanceActividad(documentoXML, _objNotificacion.EnviarCorreo(documentoXML), DatosSesion.Usuario);
                    }
                }
                if (WSR.Mensaje == "")
                {
                    try
                    {
                        SolicitudDAAEIAIdentity solicitud = new SolicitudDAAEIAIdentity();
                        SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
                        TraficoDocumento traficoArchivos = new TraficoDocumento();
                        List<byte[]> listaArchivos = new List<byte[]>();
                        listaArchivos.Add(documentoXML.datosArchivo);
                        List<string> nombres = new List<string>();
                        nombres.Add(documentoXML.nombreArchivo);
                        string ruta = "";
                        string archivo = "";
                        if (documentoXML.numSILPA != ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString())
                        {
                            solicitud = _solicitudDalc.ObtenerSolicitud(null, null, documentoXML.numSILPA);
                            traficoArchivos.RecibirDocumento(documentoXML.numSILPA, solicitud.IdSolicitante.ToString(), listaArchivos, ref nombres, ref ruta);
                        }
                        else
                        {
                            traficoArchivos.RecibirDocumento(documentoXML.numSILPA, null, listaArchivos, ref nombres, ref ruta);
                        }
                        archivo = nombres[0].Split('\\')[nombres[0].Split('\\').Length - 1].ToString();
                        WSR.IdSilpa = objIdentity.NumeroSILPA;
                        //TODO EMITIR DOCUMENTO QUE NO SE NOTIFICA
                        SILPA.AccesoDatos.Documento.MisTramitesDocumentosDALC _rutas = new SILPA.AccesoDatos.Documento.MisTramitesDocumentosDALC();
                        _rutas.InsertarDocumentoNoNotifica(Convert.ToInt32(objIdentity.CodigoAA), objIdentity.NumeroSILPA, objIdentity.NumeroActoAdministrativo, ruta + archivo, objIdentity.IdTipoActo.ID.ToString());
                        WSR.Exito = true;
                    }
                    catch (Exception ex)
                    {
                        WSR.Mensaje = "Error en NotificarIndividual: " + ex.Message;
                        WSR.Exito = false;
                    }
                }

                return WSR.GetXml();
            }
        }
        catch (Exception ex)
        {
            string strException = "Validar los pasos efectuados al Notificar Individual.";
            throw new Exception(strException, ex);
        }        
    }
    /// <summary>
    /// Implementa la lógica para manejar varios solicitantes.  Este requerimiento se detectó en las pruebas.
    /// </summary>
    /// <param name="xmlNotificacionType"></param>
    private string NotificarVariosSolicitantes(ref NotificacionType xmlNotificacionType)
    {
        try
        {
	        //TODO: La respuesta se está tomando del último paquete de personas.  Debería regresarse mejor una lista de respuestas.
	        string respuestaPrincipal = String.Empty;
	        string respuesta = String.Empty;
	
	        Dictionary<string, List<PersonaType>[]> indice = new Dictionary<string, List<PersonaType>[]>();
	        //Identifica y Agrupa los diferentes números SILPA en el diccionario indice
	        if (xmlNotificacionType.listaPersonas != null || xmlNotificacionType.listaPersonaComunicar != null)
	        {
	            if (xmlNotificacionType.listaPersonas != null)
	            {
	                foreach (PersonaType persona in xmlNotificacionType.listaPersonas)
	                {
	                    string numeroSilpa = null;
	                    if (!string.IsNullOrEmpty(persona.numeroSilpa))
	                    {
	                        NumeroSilpaDalc numeroSilpaDalc = new NumeroSilpaDalc();
	                        string processInstance = numeroSilpaDalc.ProcessInstance(persona.numeroSilpa);
	
	                        if (!string.IsNullOrEmpty(processInstance))
	                        {
	                            ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
	                            if (servicioWorkflow.ConsultarActividadActual(long.Parse(processInstance)) != null)
	                            {
	                                //Si el Número SILPA tiene actividad actual, toma el de la persona
	                                //De lo contrario va a tomar el principal
	                                numeroSilpa = persona.numeroSilpa;
	                            }
	                        }
	                    }
	
	                    if (string.IsNullOrEmpty(numeroSilpa))
	                    {
	                        //Debido a que no se selecciono el número SILPA de la persona, toma el del principal
	                        numeroSilpa = xmlNotificacionType.numSILPA;
	                    }
	
	                    // validamos si no viene el numero silpa
	                    if (string.IsNullOrEmpty(xmlNotificacionType.numSILPA))
	                    {
	                        // si no viene el numero silpa es porque es una actividad de sila ministerio y por ende no tiene numero silpa
	                        // asignamos un numero silpa unico
	                        numeroSilpa = ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString();
	                    }
	
	                    List<PersonaType>[] lista;
	                    if (indice.ContainsKey(numeroSilpa))
	                    {
	                        lista = indice[numeroSilpa];
	                    }
	                    else
	                    {
	                        lista = new List<PersonaType>[2];
	                        indice.Add(numeroSilpa, lista);
	                    }
	
	                    if (lista[0] == null)
	                        lista[0] = new List<PersonaType>();
	                    lista[0].Add(persona);
	                }
	            }
	
	            if (xmlNotificacionType.listaPersonaComunicar != null)
	            {
	                foreach (PersonaType persona in xmlNotificacionType.listaPersonaComunicar)
	                {
	                    string numeroSilpa = null;
	                    if (!string.IsNullOrEmpty(persona.numeroSilpa))
	                    {
	                        NumeroSilpaDalc numeroSilpaDalc = new NumeroSilpaDalc();
	                        string processInstance = numeroSilpaDalc.ProcessInstance(persona.numeroSilpa);
	
	                        if (!string.IsNullOrEmpty(processInstance))
	                        {
	                            ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
	                            if (servicioWorkflow.ConsultarActividadActual(long.Parse(processInstance)) != null)
	                            {
	                                //Si el Número SILPA tiene actividad actual, toma el de la persona
	                                //De lo contrario va a tomar el principal
	                                numeroSilpa = persona.numeroSilpa;
	                            }
	                        }
	                    }
	
	                    if (string.IsNullOrEmpty(numeroSilpa))
	                    {
	                        //Debido a que no se selecciono el número SILPA de la persona, toma el del principal
	                        numeroSilpa = xmlNotificacionType.numSILPA;
	                    }
	
	                    // validamos si no viene el numero silpa
	                    if (string.IsNullOrEmpty(xmlNotificacionType.numSILPA))
	                    {
	                        // si no viene el numero silpa es porque es una actividad de sila ministerio y por ende no tiene numero silpa
	                        // asignamos un numero silpa unico
	                        numeroSilpa = ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString();
	                    }
	
	                    List<PersonaType>[] lista;
	                    if (indice.ContainsKey(numeroSilpa))
	                    {
	                        lista = indice[numeroSilpa];
	                    }
	                    else
	                    {
	                        lista = new List<PersonaType>[2];
	                        indice.Add(numeroSilpa, lista);
	                    }
	
	                    if (lista[1] == null)
	                        lista[1] = new List<PersonaType>();
	                    lista[1].Add(persona);
	                }
	            }
	
	        }
	        else
	        {
	            WSRespuesta WSR = new WSRespuesta();
	            WSR.Mensaje = "No tiene usuarios asociados para notificar y/o comunicar";
	            WSR.Exito = false;
	            respuesta = WSR.GetXml();
	            SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, String.Format("NotificarVariosSolicitantes Agregando '{0}' SILPA '{1}'", "niguno", "no se encontraron datos"));
	        }
	
	
	        //Para cada Número SILPA recibido, invoca el método NotificarIndividual
	        foreach (string numeroSilpa in indice.Keys)
	        {
	            List<PersonaType>[] lista = indice[numeroSilpa];
	
	            NotificacionType notificacionPersona;
	            notificacionPersona = xmlNotificacionType;
	
	            notificacionPersona.numSILPA = numeroSilpa;
	            if (lista[0] != null)
	                notificacionPersona.listaPersonas = lista[0].ToArray();
	            else
	                notificacionPersona.listaPersonas = null;
	            if (lista[1] != null)
	                notificacionPersona.listaPersonaComunicar = lista[1].ToArray();
	            else
	                notificacionPersona.listaPersonaComunicar = null;
	
	            respuesta = NotificarIndividual(ref notificacionPersona);
	
	            if (notificacionPersona.numSILPA == numeroSilpa)
	            {
	                respuestaPrincipal = respuesta;
	            }
	        }
	
	        //Regresa la respuesta del principal si existe.   De lo contrario regresa la última recibida.
	        if (!string.IsNullOrEmpty(respuestaPrincipal))
	        {
	            return respuestaPrincipal;
	        }
	        else
	        {
	            return respuesta;
	        }
        }
        catch (Exception ex)
        {
            string strException = "Validar los pasos efectuados al Notificar a varios Solicitantes.";
            throw new Exception(strException, ex);
        }
    }

    /// <summary>
    /// Valida la notificacion Individual
    /// </summary>
    /// <param name="documentoXML"></param>
    /// <returns></returns>
    private WSRespuesta NotificarIndividualValidacion(ref NotificacionType documentoXML)
    {
        try
        {
            WSRespuesta WSR = new WSRespuesta();
            SILPA.LogicaNegocio.Notificacion.Notificacion _objNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            string mensaje = "";
            if (documentoXML.requiereNotificacion)
            {
                if (documentoXML.numSILPA != ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString())
                {
                    SILPA.AccesoDatos.Notificacion.NotificacionEntity objIdentity1 = _objNotificacion.ObtenerObjetoNotificacion(documentoXML);
                    mensaje = SILPA.Servicios.ArctividadBPMFachada.DeterminarAvanceActividadV(documentoXML, objIdentity1.NumeroSILPA, DatosSesion.Usuario);
                }
                if (mensaje != string.Empty)
                {
                    WSR.CodigoMensaje = "";
                    WSR.Mensaje = mensaje;
                    WSR.IdExterno = "-1"; // no esta relacionado a una publicación
                    WSR.IdSilpa = string.Empty;
                }
                else
                {
                    WSR.Exito = true;
                }
                return WSR;
                
            }
            else
            {
                
                WSR.CodigoMensaje = "";
                WSR.Mensaje = string.Empty;
                WSR.IdExterno = "-1"; // no esta relacionado a una publicación
                WSR.IdSilpa = string.Empty;

                SILPA.AccesoDatos.Notificacion.NotificacionEntity objIdentity = _objNotificacion.ObtenerObjetoNotificacion(documentoXML);

                TipoDocumentoDalc tipoDocDalc = new TipoDocumentoDalc();
                bool blnTipoDocumento = tipoDocDalc.ObtenerDocumentoEE(objIdentity.IdTipoActo.ID);
                if (documentoXML.numSILPA != ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString())
                {
                    if (blnTipoDocumento == true)
                    {

                        WSR.Mensaje = SILPA.Servicios.ArctividadBPMFachada.DeterminarAvanceActividadV(documentoXML, objIdentity.NumeroSILPA, DatosSesion.Usuario);
                    }
                    else
                    {
                        WSR.Mensaje = SILPA.Servicios.ArctividadBPMFachada.DeterminarAvanceActividadV(documentoXML, _objNotificacion.EnviarCorreo(documentoXML), DatosSesion.Usuario);
                    }
                }
                if (WSR.Mensaje == "")
                {
                    try
                    {
                        SolicitudDAAEIAIdentity solicitud = new SolicitudDAAEIAIdentity();
                        SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
                        TraficoDocumento traficoArchivos = new TraficoDocumento();
                        List<byte[]> listaArchivos = new List<byte[]>();
                        listaArchivos.Add(documentoXML.datosArchivo);
                        List<string> nombres = new List<string>();
                        nombres.Add(documentoXML.nombreArchivo);
                        if (documentoXML.numSILPA != ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString())
                        {

                            // validamos si existe un listado de archivos
                            if (nombres.Count == 0)
                            {
                                throw new Exception("No hay documento asociado");
                            }
                        }
                        else
                        {
                            if (nombres.Count == 0)
                            {
                                throw new Exception("No hay documento asociado");
                            }
                        }
                        WSR.IdSilpa = objIdentity.NumeroSILPA;
                        WSR.Exito = true;
                    }
                    catch (Exception ex)
                    {
                        WSR.Mensaje = "Error en NotificarIndividualValidacion: " + ex.Message;
                        WSR.Exito = false;
                    }
                }

                return WSR;
            }
        }
        catch (Exception ex)
        {
            string strException = "Validar los pasos efectuados al Notificar Individual.";
            throw new Exception(strException, ex);
        }
    }
    /// <summary>
    /// Valida la notificacion Varios Solicitantes
    /// </summary>
    /// <param name="xmlNotificacionType"></param>
    /// <returns></returns>
    private List<WSRespuesta> NotificarVariosSolicitantesValidacion(ref NotificacionType xmlNotificacionType)
    {
        try
        {
            List<WSRespuesta> lstRespuestaValidacion = new List<WSRespuesta>();
            WSRespuesta WSR = new WSRespuesta();

            Dictionary<string, List<PersonaType>[]> indice = new Dictionary<string, List<PersonaType>[]>();
            //Identifica y Agrupa los diferentes números SILPA en el diccionario indice
            if (xmlNotificacionType.listaPersonas != null || xmlNotificacionType.listaPersonaComunicar != null)
            {
                if (xmlNotificacionType.listaPersonas != null)
                {
                    foreach (PersonaType persona in xmlNotificacionType.listaPersonas)
                    {
                        string numeroSilpa = null;
                        if (!string.IsNullOrEmpty(persona.numeroSilpa))
                        {
                            NumeroSilpaDalc numeroSilpaDalc = new NumeroSilpaDalc();
                            string processInstance = numeroSilpaDalc.ProcessInstance(persona.numeroSilpa);

                            if (!string.IsNullOrEmpty(processInstance))
                            {
                                ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
                                if (servicioWorkflow.ConsultarActividadActual(long.Parse(processInstance)) != null)
                                {
                                    numeroSilpa = persona.numeroSilpa;
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(numeroSilpa))
                        {
                            //Debido a que no se selecciono el número SILPA de la persona, toma el del principal
                            numeroSilpa = xmlNotificacionType.numSILPA;
                        }

                        // validamos si no viene el numero silpa
                        if (string.IsNullOrEmpty(xmlNotificacionType.numSILPA))
                        {
                            // si no viene el numero silpa es porque es una actividad de sila ministerio y por ende no tiene numero silpa
                            // asignamos un numero silpa unico
                            numeroSilpa = ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString();
                        }

                        List<PersonaType>[] lista;
                        if (indice.ContainsKey(numeroSilpa))
                        {
                            lista = indice[numeroSilpa];
                        }
                        else
                        {
                            lista = new List<PersonaType>[2];
                            indice.Add(numeroSilpa, lista);
                        }

                        if (lista[0] == null)
                            lista[0] = new List<PersonaType>();
                        lista[0].Add(persona);
                    }


                    if (xmlNotificacionType.listaPersonaComunicar != null)
                    {
                        foreach (PersonaType persona in xmlNotificacionType.listaPersonaComunicar)
                        {
                            string numeroSilpa = null;
                            if (!string.IsNullOrEmpty(persona.numeroSilpa))
                            {
                                NumeroSilpaDalc numeroSilpaDalc = new NumeroSilpaDalc();
                                string processInstance = numeroSilpaDalc.ProcessInstance(persona.numeroSilpa);

                                if (!string.IsNullOrEmpty(processInstance))
                                {
                                    ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
                                    if (servicioWorkflow.ConsultarActividadActual(long.Parse(processInstance)) != null)
                                    {
                                        //Si el Número SILPA tiene actividad actual, toma el de la persona
                                        //De lo contrario va a tomar el principal
                                        numeroSilpa = persona.numeroSilpa;
                                    }
                                }
                            }

                            if (string.IsNullOrEmpty(numeroSilpa))
                            {
                                //Debido a que no se selecciono el número SILPA de la persona, toma el del principal
                                numeroSilpa = xmlNotificacionType.numSILPA;
                            }

                            // validamos si no viene el numero silpa
                            if (string.IsNullOrEmpty(xmlNotificacionType.numSILPA))
                            {
                                // si no viene el numero silpa es porque es una actividad de sila ministerio y por ende no tiene numero silpa
                                // asignamos un numero silpa unico
                                numeroSilpa = ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString();
                            }

                            List<PersonaType>[] lista;
                            if (indice.ContainsKey(numeroSilpa))
                            {
                                lista = indice[numeroSilpa];
                            }
                            else
                            {
                                lista = new List<PersonaType>[2];
                                indice.Add(numeroSilpa, lista);
                            }

                            if (lista[1] == null)
                                lista[1] = new List<PersonaType>();
                            lista[1].Add(persona);
                        }
                    }

                }
                else
                {
                    WSR.CodigoMensaje = "EXCEP_01";
                    WSR.Mensaje = "No tiene usuarios asociados para notificar y/o comunicar.";
                    WSR.Exito = false;
                    lstRespuestaValidacion.Add(WSR);
                    SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, String.Format("NotificarVariosSolicitantes Agregando '{0}' SILPA '{1}'", "niguno", "no se encontraron datos"));
                }
                

                //Para cada Número SILPA recibido, invoca el método NotificarIndividual
                foreach (string numeroSilpa in indice.Keys)
                {
                    List<PersonaType>[] lista = indice[numeroSilpa];

                    NotificacionType notificacionPersona;
                    notificacionPersona = xmlNotificacionType;

                    notificacionPersona.numSILPA = numeroSilpa;
                    if (lista[0] != null)
                        notificacionPersona.listaPersonas = lista[0].ToArray();
                    else
                        notificacionPersona.listaPersonas = null;
                    if (lista[1] != null)
                        notificacionPersona.listaPersonaComunicar = lista[1].ToArray();
                    else
                        notificacionPersona.listaPersonaComunicar = null;

                    lstRespuestaValidacion.Add(NotificarIndividualValidacion(ref notificacionPersona));
                }
            }
            
            return lstRespuestaValidacion;
            
        }
        catch (Exception ex)
        {
            string strException = "Validar los pasos efectuados al Notificar a varios Solicitantes.";
            throw new Exception(strException, ex);
        }
    }

    /// <summary>
    /// aegb
    /// Método para Asociar Expediente y numero silpa
    /// </summary>
    [WebMethod(Description = "[Asociar Expediente y numero silpa]")]
    public void AsociarExpedienteNumeroSilpa(int autoridadId, string numeroExpediente, string tipoAsociacion, List<string> numerosSilpa)
    {
        try
        {
	        SolicitudExpediente solicitudExpediente = new SolicitudExpediente();
	        solicitudExpediente.AsociarExpedienteNumeroSilpa(autoridadId, numeroExpediente, tipoAsociacion, numerosSilpa);
        }
        catch (Exception ex)
        {
            string strException = "Validar los pasos efectuados al Asociar Expediente y número VITAL.";
            throw new Exception(strException, ex);
        }
    }


    [WebMethod(Description = "[Asociar Info Expediente a un numero silpa]")]
    public void AsociarInfoExpedienteNumeroSilpa(string codigoExpediente, string numeroVital, string nombreExpediente, string descripcionExpediente, string sectorId, List<string> ubicacionExpediente, List<string> localizacionExpediente)
    {
        SolicitudExpediente solicitudExpediente = new SolicitudExpediente();
        solicitudExpediente.AsociarInfoExpedienteNumeroSilpa(codigoExpediente,  numeroVital, nombreExpediente, descripcionExpediente, sectorId, ubicacionExpediente, localizacionExpediente);
    }

    /// <summary>
    /// aegb
    /// Método para activar el Expediente y numero silpa
    /// </summary>
    [WebMethod(Description = "[Activar Expediente y numero silpa]")]
    public void ActivatExpedienteNumeroSilpa(int autoridadId, string numeroExpediente)
    {
        SolicitudExpediente solicitudExpediente = new SolicitudExpediente();
        solicitudExpediente.ActivarExpedienteNumeroSilpa(autoridadId, numeroExpediente);
    }


    /// <summary>
    /// HAVA
    /// 20-DIC-10
    /// Método para obtener los documentos de radicación
    /// </summary>
    [WebMethod(Description = "[Obtiene los documentos de la radicación]")]
    public string ObtenerDocumentosRadicacion(long idRadicacion)
    {
        SILPA.LogicaNegocio.Generico.RadicacionDocumento radDoc = new RadicacionDocumento();
        return radDoc.ObtenerDocumentosRadicacion(idRadicacion);
    }

    [WebMethod(Description = "[Obtiene los documentos de la radicación]")]
    public string ObtenerDocumentosNUR(string strNUR)
    {
        SILPA.LogicaNegocio.Generico.RadicacionDocumento radDoc = new RadicacionDocumento();
        return radDoc.ObtenerDocumentosNUR(strNUR);
    }


    [WebMethod(Description = "[Obtiene el documento de una radicacion]")]
    public Byte[] ObtenerDocumentoRadicacion(long idRadicacion,string nombreArchivo)
    {
        SILPA.LogicaNegocio.Generico.RadicacionDocumento radDoc = new RadicacionDocumento();
        return radDoc.ObtenerDocumentoRadicacion(idRadicacion,nombreArchivo);
    }

    [WebMethod(Description = "[Obtiene el documento de una radicacion]")]
    public string ObtenerPathRadicacion(long idRadicacion, string nombreArchivo)
    {
        SILPA.LogicaNegocio.Generico.RadicacionDocumento radDoc = new RadicacionDocumento();
        return radDoc.ObtenerPathRadicacion(idRadicacion, nombreArchivo);
    }

    [WebMethod(Description = "[Obtiene el documento de una radicacion]")]
    public string ObtenerPathCorrespondencia(string NUR, string nombreArchivo)
    {
        SILPA.LogicaNegocio.Generico.RadicacionDocumento radDoc = new RadicacionDocumento();
        return radDoc.ObtenerPathNUR(NUR, nombreArchivo);
    }

    [WebMethod(Description = "[Obtiene el path de radicacion fus]")]
    public string ObtenerPathFus(long int_id_radicacion)
    {
        SILPA.LogicaNegocio.Generico.RadicacionDocumento radDoc = new RadicacionDocumento();
        return radDoc.ObtenerPathFus(int_id_radicacion);
    }

    [WebMethod(Description = "[Obtiene el path de radicacion fus]")]
    public string ObtenerPathDocumentos(long int_id_radicacion)
    {
        SILPA.LogicaNegocio.Generico.RadicacionDocumento radDoc = new RadicacionDocumento();
        return radDoc.ObtenerPathDocumentos(int_id_radicacion);
    }


    [WebMethod(Description = "[Obtiene el documento de una radicacion]")]
    public string EnviarDocumentoRadicacion(long idRadicacion, string nombreArchivo,Byte[] bytesArchivo)
    {
        SILPA.LogicaNegocio.Generico.RadicacionDocumento radDoc = new RadicacionDocumento();
        return radDoc.EnviarDocumentoRadicacion(idRadicacion, nombreArchivo,bytesArchivo);
    }

    [WebMethod(Description = "[Obtiene la ruta de los archivos y si el archivo ya existe]")]
    public string EnviarDocumentoRadicacionAlterno(long idRadicacion, string nombreArchivo, out string rutaArchivos)
    {
        SILPA.LogicaNegocio.Generico.RadicacionDocumento radDoc = new RadicacionDocumento();
        return radDoc.EnviarDocumentoRadicacion(idRadicacion, nombreArchivo, out rutaArchivos);
    }
    [WebMethod(Description = "[Obtiene la ruta de los archivos y si el archivo ya existe]")]
    public string EnviarDocumentoCorrespondencia(string NUR, string nombreArchivo, string strCarpetaNURs, out string rutaArchivos)
    {
        SILPA.LogicaNegocio.Generico.RadicacionDocumento radDoc = new RadicacionDocumento();
        return radDoc.EnviarDocumentoNUR(NUR, nombreArchivo,strCarpetaNURs, out rutaArchivos);
    }


    /// <summary>
    /// HAVA
    /// 20-DIC-10
    /// Método para obtener los documentos de radicación
    /// </summary>
   /// [WebMethod(Description = "[Obtiene los documentos de la radicación]")]
    //public string ObtenerURLDocumentosRadicacion(long idRadicacion)
    //{
    //    SILPA.LogicaNegocio.Generico.RadicacionDocumento radDoc = new RadicacionDocumento();
    //    //return radDoc.ObtenerDocumentosRadicacion(idRadicacion);
    //    return radDoc.ObtenerURLDocumentosRadicacion(idRadicacion);

    //    //return this.Context.Request.Url.ToString();
    //}

    [WebMethod(Description = "[Guarda la relacion entre dos expedientes]")]
    public void RelacionarExpedientesPadreHijo(string codigo_Expediente, string padre, string hijo, int tipoTramite)
    {
        try
        {
            SILPA.LogicaNegocio.Expedientes.ExpedienteAutExt objExpedienteAutExt = new SILPA.LogicaNegocio.Expedientes.ExpedienteAutExt();
            objExpedienteAutExt.RelacionarExpedienteSilpaPadreHijo(codigo_Expediente, padre, hijo, tipoTramite);
        }
        catch (Exception ex)
        {
            string strException = "Se presento falla creando relacion entre expedientes. " + ex.Message + " - " + ex.InnerException.ToString();
            throw new Exception(strException, ex);
        }
    }


    [WebMethod(Description = "[Crear solicitud manual]")]
    public void CrearSolicitudManual(int AutoridadID, int ExpedienteID, string CodigoExpediente, int SectorID, int PersonaID, string NumeroVITAL, string NumeroRadicado, int TramiteID, string NombreProyecto)
    {
        try
        {
            SolicitudExpediente solicitudExpediente = new SolicitudExpediente();
            solicitudExpediente.CrearSolicitudManual(AutoridadID, ExpedienteID, CodigoExpediente, SectorID, PersonaID, NumeroVITAL, NumeroRadicado, TramiteID, NombreProyecto);
        }
        catch (Exception ex)
        {
            string strException = "Se presento falla en la insercion manual del expediente. " + ex.Message + " - " + ex.InnerException.ToString();
            throw new Exception(strException, ex);
        }
    }

    [WebMethod(Description = "[Obtener el numero vital padre de una solicitud ]")]
    public string ObtenerNumeroVITALPadreSolicitud(string strNumeroVital)
    {
        try
        {
            SolicitudExpediente solicitudExpediente = new SolicitudExpediente();
            return solicitudExpediente.ObtenerNumeroVITALPadreSolicitud(strNumeroVital);
        }
        catch (Exception ex)
        {
            string strException = "Se presento falla en la consulta del numero vital padre. " + ex.Message + " - " + ex.InnerException.ToString();
            throw new Exception(strException, ex);
        }
    }


    [WebMethod(Description = "[Devuelve la informacion de las autoridades ambientales de una solicitud]")]
    public string ObtenerAutoridadesSolicitud(string numeroVital)
    {
        SILPA.LogicaNegocio.DAA.DAA objDaa = new SILPA.LogicaNegocio.DAA.DAA();
        string xmlInformacion = objDaa.ObtenerAutoridadesSolicitud(numeroVital);
        return xmlInformacion;

    }


    [WebMethod(Description = "[Devuelve la informacion de las comunidades de una solicitud]")]
    public string ObtenerComunidadesSolicitud(string numeroVital)
    {
        SILPA.LogicaNegocio.DAA.DAA objDaa = new SILPA.LogicaNegocio.DAA.DAA();
        string xmlInformacion = objDaa.ObtenerComunidadesSolicitud(numeroVital);
        return xmlInformacion;

    }
    


    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

}


