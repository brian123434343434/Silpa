using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Notificacion;
using SoftManagement.LogWS;
using System.Data;
using SILPA.Servicios;


/// <summary>
/// Descripción breve de Servicio de Notificación
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSNOT : System.Web.Services.WebService
{

    public WSNOT()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    /// <summary>
    /// El metodo verificar no sirve para verificar el estado que retorna el servicio de consulta de estado de Notificacion electronica
    /// </summary>
    /// <param name="acto">Cadena que representa el objeto ActoAdministrativo serializado</param>
    /// <returns>Cadena que representa el Objeto estdo serializado </returns>
    [WebMethod(Description = "[Expone la información del estado del proceso de Notificación a la autoridad ambiental para que la consulte]", MessageName = "CU-NOT-03")]
    public string VerificarEstado(string datosConsulta)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "VerificarEstado";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, datosConsulta.ToString(), "", 0);
            SILPA.LogicaNegocio.Notificacion.Notificacion _objNOT = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            //return _objNOT.RespuestaPruebaNotificado(datosConsulta);
            return _objNOT.ConsultarEstado(datosConsulta);

        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, datosConsulta.ToString(), strNoVital, iAA, iIdPadre);
        }

        
        
    }


    /// <summary>
    /// Método que permite obtener la fehca de notificación y citación de un acto administrativo
    /// </summary>
    /// <param name="acto">Numero del acto administrativo</param>
    /// <returns>Cadena que representa el Objeto estado serializado </returns>
    [WebMethod(Description = "[Obtiene las fechas de citación y notificación del acto administrativo]", MessageName = "--")]
    public string ObtenerFechasCitacionNotificacion(string datosConsulta, int idAA, string codigoExpediente)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "ObtenerFechasCitacionNotificacion";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, datosConsulta.ToString(), "", 0);
            SILPA.LogicaNegocio.Notificacion.Notificacion _objNOT = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            return _objNOT.ConsultarFechaCitacionNotificacion(datosConsulta, idAA, codigoExpediente);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, datosConsulta.ToString(), strNoVital, iAA, iIdPadre);
        }

    }


    /// <summary>
    /// Obtener información de enlace generado en VITAL
    /// </summary>
    /// <param name="strIdentificadorEnlace">string con el identificador del enlace</param>
    /// <param name="strLlaveEnlace">string </param>
    /// <returns>string con XML que contiene información de enlace</returns>
    [WebMethod(Description = "[Obtener información de enlace generado en VITAL]", MessageName = "NOT_EN")]
    public string ObtenerInformacionEnlace(string strIdentificadorEnlace, string strLlaveEnlace)
    {
        EnlaceDocumentoSilaEntity objEnlace = null;
        SILPA.LogicaNegocio.Notificacion.EnlaceDocumentoSila objEnlaceSila = null; 
        long lngIdPadre = 0;
        string strXMLEnlace = "";

        try
        {
            lngIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerInformacionEnlace", strIdentificadorEnlace + " - " + strLlaveEnlace, "", 0);
            objEnlaceSila = new SILPA.LogicaNegocio.Notificacion.EnlaceDocumentoSila();
            objEnlace = objEnlaceSila.ConsultarEnlace(strIdentificadorEnlace, strLlaveEnlace);
            if (objEnlace != null)
            {
                strXMLEnlace = objEnlace.GetXml();
            }
        }
        catch(Exception exc){
            SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerInformacionEnlace", exc.ToString(), lngIdPadre);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerInformacionEnlace", strIdentificadorEnlace + " - " + strLlaveEnlace, "", 0, lngIdPadre);
        }

        return strXMLEnlace;
    }


    /// <summary>
    /// Obtener el listado de direcciones de una persona
    /// </summary>
    /// <param name="p_strNumeroIdentificacion">string con el numero identificacion de una persona</param>
    /// <returns>string con el XML que contiene el listado de direcciones de una persona</returns>
    [WebMethod(Description = "Obtener el listado de direcciones de una persona]")]
    public string ObtenerDireccionesNumeroIdentificacion(string p_strNumeroIdentificacion)
    {
        NotificacionFachada objNotificacionFachada = null;
        long lngIdPadre = 0;
        string strXMLInformacion = "";

        try
        {
            lngIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerDireccionesNumeroIdentificacion", "p_strNumeroIdentificacion: " + (p_strNumeroIdentificacion ?? "null"), "", 0);

            objNotificacionFachada = new NotificacionFachada();
            strXMLInformacion = objNotificacionFachada.ObtenerDireccionesNumeroIdentificacion(p_strNumeroIdentificacion);
        }
        catch (Exception exc)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerDireccionesNumeroIdentificacion", exc.ToString(), lngIdPadre);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerDireccionesNumeroIdentificacion", "strXMLInformacion: " + (strXMLInformacion ?? "null"), "", 0, lngIdPadre);
        }

        return strXMLInformacion;
    }

    /// <summary>
    /// Obtener la información de la dirección de la persona
    /// </summary>
    /// <param name="p_lngDIreccionID">long con el identificador de la direccion</param>
    /// <returns>string con la información de la dirección</returns>
    [WebMethod(Description = "Obtener la información de la dirección de la persona]")]
    public string ObtenerInformacionDireccionPersona(long p_lngDIreccionID)
    {
        NotificacionFachada objNotificacionFachada = null;
        long lngIdPadre = 0;
        string strXMLInformacion = "";

        try
        {
            lngIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerInformacionDireccionPersona", "p_lngDIreccionID: " + p_lngDIreccionID.ToString(), "", 0);

            objNotificacionFachada = new NotificacionFachada();
            strXMLInformacion = objNotificacionFachada.ObtenerInformacionDireccionPersona(p_lngDIreccionID);
        }
        catch (Exception exc)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerInformacionDireccionPersona", exc.ToString(), lngIdPadre);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerInformacionDireccionPersona", "strXMLInformacion: " + (strXMLInformacion ?? "null"), "", 0, lngIdPadre);
        }

        return strXMLInformacion;
    }


    /// <summary>
    /// Obtener el listado de correos de una persona
    /// </summary>
    /// <param name="p_strNumeroIdentificacion">string con el numero de identificacion</param>
    /// <returns>List con la información de los correos</returns>
    [WebMethod(Description = "Obtener el listado de correos de una persona]")]
    public List<string> ObtenerListadoCorreosNotificarNumeroIdentificacion(string p_strNumeroIdentificacion)
    {
        NotificacionFachada objNotificacionFachada = null;
        long lngIdPadre = 0;
        List<string> objLstCorreos = null;

        try
        {
            lngIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerListadoCorreosNotificarNumeroIdentificacion", "p_strNumeroIdentificacion: " + (p_strNumeroIdentificacion ?? "null"), "", 0);

            objNotificacionFachada = new NotificacionFachada();
            objLstCorreos = objNotificacionFachada.ObtenerListadoCorreosNotificarNumeroIdentificacion(p_strNumeroIdentificacion);
        }
        catch (Exception exc)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerListadoCorreosNotificarNumeroIdentificacion", exc.ToString(), lngIdPadre);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerListadoCorreosNotificarNumeroIdentificacion", "objLstCorreos: " + (objLstCorreos != null ? objLstCorreos.ToString() : "null"), "", 0, lngIdPadre);
        }

        return objLstCorreos;
    }




    [WebMethod(Description = "[Proceso que adelanta proceso de notificacion en el BPM]", MessageName = "NOT_EL")]
    public void AdelantarProcesosDeNotificacion()
    {
        NotificacionFachada objNotificacionFachada = new NotificacionFachada();
        objNotificacionFachada.ProcesarNotificacionesPendientesBPM();
    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

}

