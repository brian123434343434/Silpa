using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using SoftManagement.Log;
using SILPA.Servicios.Liquidacion;
using System.Configuration;

/// <summary>
/// Descripción breve de WSAUTOLIQ
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSAUTOLIQ : System.Web.Services.WebService {

    public WSAUTOLIQ () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "[Consulta la informacion de la solicitud de liquidación]")]
    public string ConsultarSolicitudLiquidacion(string strNumeroVital, int intAutoridadAmbiental)
    {
        Int64 intIdPadre = 0;
        string strDatosRecibidos = "";
        string strRespuestaXML = "";
        AutoliquidacionFachada objFachada = null;

        try
        {
            //Insertar log
            strDatosRecibidos = "strNumeroVital: " + (!string.IsNullOrEmpty(strNumeroVital) ? strNumeroVital : "null");
            strDatosRecibidos += " - intAutoridadAmbiental: " + intAutoridadAmbiental.ToString();
            intIdPadre = SoftManagement.LogWS.SMLogWS.EscribirInicio(this.ToString(), "ConsultarSolicitudLiquidacion", strDatosRecibidos, strNumeroVital, 0);

            //Generar certificado
            objFachada = new AutoliquidacionFachada();
            strRespuestaXML = objFachada.ConsultarInformacionSolicitud(strNumeroVital, intAutoridadAmbiental);

        }
        catch (Exception ex)
        {
            SoftManagement.LogWS.SMLogWS.EscribirExcepcion(this.ToString(), "GenerarCertConsultarSolicitudLiquidacionificadoFinal", ex.ToString(), intIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SoftManagement.LogWS.SMLogWS.EscribirFinalizar(this.ToString(), "ConsultarSolicitudLiquidacion", strRespuestaXML, strNumeroVital, 0, intIdPadre);
        }

        return strRespuestaXML;
    }
    

    [WebMethod(Description = "[Registra un nuevo cobro en VITAL]")]
    public string CrearCobro(string strInformacionCobro, bool blnAvanzaTarea)
    {
        Int64 intIdPadre = 0;
        string strMensaje = "";
        string strDatosRecibidos = "";
        AutoliquidacionFachada objFachada = null;

        try
        {
            //Insertar log
            strDatosRecibidos = "strInformacionCobro: " + (!string.IsNullOrEmpty(strInformacionCobro) ? strInformacionCobro : "null");
            intIdPadre = SoftManagement.LogWS.SMLogWS.EscribirInicio(this.ToString(), "CrearCobro", strDatosRecibidos, "", 0);

            //Generar certificado
            objFachada = new AutoliquidacionFachada();
            strMensaje = objFachada.CrearCobro(strInformacionCobro, DatosSesion.Usuario, blnAvanzaTarea);

        }
        catch (Exception ex)
        {
            SoftManagement.LogWS.SMLogWS.EscribirExcepcion(this.ToString(), "CrearCobro", ex.ToString(), intIdPadre);
            SMLog.Escribir(ex);
            strMensaje = "Se presento error durante el registro del cobro. Error: " + ex.ToString();
        }
        finally
        {
            SoftManagement.LogWS.SMLogWS.EscribirFinalizar(this.ToString(), "CrearCobro", strMensaje, "", 0, intIdPadre);
        }

        return strMensaje;
    }


    [WebMethod(Description = "[Registra un nuevo cobro de autoliquidacion en VITAL]")]
    public string CrearCobroAutoliquidacion(string strInformacionCobro)
    {
        Int64 intIdPadre = 0;
        string strMensaje = "";
        string strDatosRecibidos = "";
        AutoliquidacionFachada objFachada = null;

        try
        {
            //Insertar log
            strDatosRecibidos = "strInformacionCobro: " + (!string.IsNullOrEmpty(strInformacionCobro) ? strInformacionCobro : "null");
            intIdPadre = SoftManagement.LogWS.SMLogWS.EscribirInicio(this.ToString(), "CrearCobroAutoliquidacion", strDatosRecibidos, "", 0);

            //Generar certificado
            objFachada = new AutoliquidacionFachada();
            strMensaje = objFachada.CrearCobroAutoliquidacion(strInformacionCobro, DatosSesion.Usuario);

        }
        catch (Exception ex)
        {
            SoftManagement.LogWS.SMLogWS.EscribirExcepcion(this.ToString(), "CrearCobroAutoliquidacion", ex.ToString(), intIdPadre);
            SMLog.Escribir(ex);
            strMensaje = "Se presento error durante el registro del cobro de autoliquidacion. Error: " + ex.ToString();
        }
        finally
        {
            SoftManagement.LogWS.SMLogWS.EscribirFinalizar(this.ToString(), "CrearCobroAutoliquidacion", strMensaje, "", 0, intIdPadre);
        }

        return strMensaje;
    }


    [WebMethod(Description = "[Finaliza el proceso de cobro registrado en VITAL]")]
    public string FinalizarProcesoCobro(string strCodigoReferencia)
    {
        Int64 intIdPadre = 0;
        string strMensaje = "";
        string strDatosRecibidos = "";
        AutoliquidacionFachada objFachada = null;

        try
        {
            //Insertar log
            strDatosRecibidos = "strCodigoReferencia: " + (!string.IsNullOrEmpty(strCodigoReferencia) ? strCodigoReferencia : "null");
            intIdPadre = SoftManagement.LogWS.SMLogWS.EscribirInicio(this.ToString(), "FinalizarProcesoCobro", strDatosRecibidos, "", 0);

            //Generar certificado
            objFachada = new AutoliquidacionFachada();
            strMensaje = objFachada.FinalizarTransaccionCobro(strCodigoReferencia);

        }
        catch (Exception ex)
        {
            SoftManagement.LogWS.SMLogWS.EscribirExcepcion(this.ToString(), "FinalizarProcesoCobro", ex.ToString(), intIdPadre);
            SMLog.Escribir(ex);
            strMensaje = "Se presento error durante finalización de cobro. Error: " + ex.ToString();
        }
        finally
        {
            SoftManagement.LogWS.SMLogWS.EscribirFinalizar(this.ToString(), "FinalizarProcesoCobro", strMensaje, "", 0, intIdPadre);
        }

        return strMensaje;
    }


    [WebMethod(Description = "[Obtiene el listado de autoridades ambientales para la solicitud de permisos]")]
    public string ListarAutoridadAmbientalPermisos()
    {
        Int64 intIdPadre = 0;
        string strMensaje = "";
        AutoliquidacionFachada objFachada = null;

        try
        {
            //Insertar log
            intIdPadre = SoftManagement.LogWS.SMLogWS.EscribirInicio(this.ToString(), "ListarAutoridadAmbientalPermisos", "", "", 0);

            //Generar certificado
            objFachada = new AutoliquidacionFachada();
            strMensaje = objFachada.ListarAutoridadAmbientalPermisos();

        }
        catch (Exception ex)
        {
            SoftManagement.LogWS.SMLogWS.EscribirExcepcion(this.ToString(), "ListarAutoridadAmbientalPermisos", ex.ToString(), intIdPadre);
            SMLog.Escribir(ex);
            strMensaje = "Se presento error durante obtencion de las autoridades ambientales para manejo de permisos. Error: " + ex.ToString();
        }
        finally
        {
            SoftManagement.LogWS.SMLogWS.EscribirFinalizar(this.ToString(), "ListarAutoridadAmbientalPermisos", strMensaje, "", 0, intIdPadre);
        }

        return strMensaje;
    }

}
