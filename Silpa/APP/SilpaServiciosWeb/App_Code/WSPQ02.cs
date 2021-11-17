using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using SILPA.LogicaNegocio.Comunicacion;
using SILPA.Servicios.Comunicacion;
using SILPA.Comun;
using System.Xml;
using System.Threading;
using SoftManagement.Log;
using SoftManagement.LogWS;
using System.Collections.Generic;
using SILPA.AccesoDatos.Comunicacion;

/// <summary>
/// Summary description for WSPQ02
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSPQ02 : System.Web.Services.WebService
{

    public WSPQ02()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Recibe una comunicación de la AA para enviársela a una EE
    /// </summary>
    /// <param name="datosComunicacionXML">Datos de la comunicación en formato xml</param>
    /// <returns>string con respuesta existosa o fallida en formato xml</returns>
    [WebMethod(Description = "[Recibe los datos entregados por la Autoridad Ambiental para enviar una comunicación a una entidad Externa]", MessageName = "CU-COM")]
    public string EnviarComunicacionEE(string datosComunicacionXML)
    {
        Int64 iIdPadre = 0;
        string numeroVital = "";
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "EnviarComunicacionEE", datosComunicacionXML, "", 0);
            //ComunicacionEEFachada.EnviarComunicacionFachada(datosComunicacionXML);
            numeroVital= ComunicacionEEFachada.EnviarProceso(datosComunicacionXML);
            
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "EnviarComunicacionEE", ex.ToString(), iIdPadre);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "EnviarComunicacionEE", datosComunicacionXML, "", 0, iIdPadre);
        }

        return numeroVital;
    }

    /// <summary>
    /// Recibe una comunicación de la AA para enviársela a una EE
    /// </summary>
    /// <param name="datosComunicacionXML">Datos de la comunicación en formato xml</param>
    /// <returns>string con respuesta existosa o fallida en formato xml</returns>
    [WebMethod(Description = "[Responde los datos solicitados por la Autoridad Ambiental para enviar una comunicación a una entidad Externa]", MessageName = "CU-COMR")]
    public bool ResponderComunicacionEE(string datosComunicacionXML)
    {
        Int64 iIdPadre = 0;
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ResponderComunicacionEE", "ResponderComunicacionEE", "", 0);
            //System.IO.File.WriteAllText(@"c:\temp\datosComunicacionXML.xml", datosComunicacionXML);
            //throw new ApplicationException(datosComunicacionXML);
            //ComunicacionEEFachada.EnviarComunicacionFachada(datosComunicacionXML);
            ThreadPool.QueueUserWorkItem(new WaitCallback(ComunicacionEEFachada.EnviarProcesoRespuesta), (object)datosComunicacionXML);
            return true;
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "ResponderComunicacionEE", ex.ToString(), iIdPadre);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "ResponderComunicacionEE", "ResponderComunicacionEE", "", 0, iIdPadre);
        }
    }

    /// <summary>
    /// Recibe una comunicación de visita de la AA para enviársela a un solicitante
    /// </summary>
    /// <param name="datosComunicacionXML">Datos de la comunicación de visita en formato xml</param>
    /// <returns>string con respuesta existosa o fallida en formato xml</returns>
    [WebMethod(Description = "[Recibe los datos entregados por la Autoridad Ambiental para enviar una comunicación de visita a un solicitante]", MessageName = "CU-COM-03")]
    public bool EnviarComunicacionVisita(string datosComunicacionXML)
    {
        Int64 iIdPadre = 0;
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "EnviarComunicacionVisita", datosComunicacionXML, "", 0);
            //Se envia informacion          
            //ComunicacionVisitaFachada.EnviarComunicacionFachada(datosComunicacionXML);
            ThreadPool.QueueUserWorkItem(new WaitCallback(ComunicacionVisitaFachada.EnviarProceso), (object)datosComunicacionXML);
            return true;
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "EnviarComunicacionVisita", ex.ToString(), iIdPadre);
            throw; ;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "EnviarComunicacionVisita", datosComunicacionXML, "", 0, iIdPadre);
        }
    }

    [WebMethod(Description = "[Recibe los datos entregados por la Autoridad Ambiental para enviar una comunicación de Reunion a un solicitante]", MessageName = "CU-COM-02")]
    public bool EnviarComunicacionReunion(string datosComunicacionXML)
    {
        Int64 iIdPadre = 0;
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "EnviarComunicacionReunion", datosComunicacionXML, "", 0);
            //Se envia informacion          
            //ComunicacionVisitaFachada.EnviarComunicacionFachada(datosComunicacionXML);
            ThreadPool.QueueUserWorkItem(new WaitCallback(ComunicacionReunionFachada.EnviarProceso), (object)datosComunicacionXML);
            return true;
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "EnviarComunicacionReunion", ex.ToString(), iIdPadre);
            string strException = "Validar los pasos efectuados al recibir los datos entregados por la Autoridad Ambiental para enviar una comunicación de Reunión a un Solicitante.";
            throw new Exception(strException, ex);
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "EnviarComunicacionReunion", datosComunicacionXML, "", 0, iIdPadre);
        }
    }
    [WebMethod(Description = "[Recibe los datos entregados por la Autoridad Ambiental para enviar una comunicación de Reunion a un solicitante]", MessageName = "CU-COM-05")]
    public bool EnviarComunicacionReunionInfoAdicional(List<ComunicacionReunionType> datosComunicacionXML)
    {
        Int64 iIdPadre = 0;
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "EnviarComunicacionReunionInfoAdicional", datosComunicacionXML.ToString(), "", 0);
            //Se envia informacion          
            //ComunicacionVisitaFachada.EnviarComunicacionFachada(datosComunicacionXML);
            ThreadPool.QueueUserWorkItem(new WaitCallback(ComunicacionReunionFachada.EnviarProcesoInfoAdicional), (object)datosComunicacionXML);
            return true;
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "EnviarComunicacionReunionInfoAdicional", ex.ToString(), iIdPadre);
            throw; ;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "EnviarComunicacionReunionInfoAdicional", datosComunicacionXML.ToString(), "", 0, iIdPadre);
        }
    }

    /// <summary>
    /// Envia correo de espera de respuesta a la EE
    /// </summary>
    /// <param name="numeroExpediente">Numero del expediente</param>
    /// <returns>string con respuesta existosa o fallida </returns>
    [WebMethod(Description = "[Recibe los datos entregados por la Autoridad Ambiental para enviar una comunicación a una entidad Externa]", MessageName = "CU-COM-04")]
    public bool MonitorearRespuestaEE(string numeroExpediente)
    {
        Int64 iIdPadre = 0;
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "MonitorearRespuestaEE", numeroExpediente, "", 0);
            ComunicacionEE respuesta = new ComunicacionEE();
            respuesta.MonitorearRespuestaEE(numeroExpediente);
            return true;
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "MonitorearRespuestaEE", ex.ToString(), iIdPadre);
            return false;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "MonitorearRespuestaEE", numeroExpediente, "", 0, iIdPadre);
        }

    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

}

