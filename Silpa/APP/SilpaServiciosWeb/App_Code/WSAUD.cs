using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using SILPA.LogicaNegocio;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos;
using SILPA.Servicios;
using SILPA.Servicios.Audiencia;
using System.Data;
using SoftManagement.Log;
using SoftManagement.LogWS;

/// <summary>
/// Summary description for WSAUD
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSAUD : System.Web.Services.WebService
{

    public WSAUD()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Recibe un edicto de audiencia pública
    /// </summary>
    /// <param name="datosComunicacionXML">Datos del edicto en formato xml</param>
    /// <returns>bool con respuesta existosa o fallida</returns>
    [WebMethod(Description = "[Recibe los datos entregados por la Autoridad Ambiental de un edicto de audiencia pública]", MessageName = "CU-AUD-02")]
    public bool RecibirDatosAudienciaPublica(string datosAudienciaXML)
    {   
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "RecibirDatosAudienciaPublica";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, datosAudienciaXML, "", 0);
            AudienciaFachada objAudienciaFachada = new AudienciaFachada();
            objAudienciaFachada.GuardarAudiencia(datosAudienciaXML);
            return true;
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            string strException = "Validar los pasos efectuados al recibir los datos entregados por la Autoridad Ambiental de un edicto de audiencia pública.";
            throw new Exception(strException, ex);
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, datosAudienciaXML, strNoVital, iAA, iIdPadre);
        }
    }


    /// <summary>
    /// Recibe un edicto de audiencia pública
    /// </summary>
    /// <param name="datosComunicacionXML">Datos del edicto en formato xml</param>
    /// <returns>string con respuesta existosa o fallida</returns>
    [WebMethod(Description = "[Devuelve un string con el listado de inscritos en las audiencias]", MessageName = "CU-AUD-03")]
    public string RespuestaInscripcionAudiencia(string idAudiencia)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "RespuestaInscripcionAudiencia";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, idAudiencia, "", 0);
            AudienciaFachada objAudienciaFachada = new AudienciaFachada();

            string inscritos = objAudienciaFachada.ConsultaInscritosAudiencia(idAudiencia);

            return inscritos;
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, idAudiencia, strNoVital, iAA, iIdPadre);
        }
    }


    /// <summary>
    /// Recibe un edicto de audiencia pública
    /// </summary>
    /// <param name="datosComunicacionXML">Datos del edicto en formato xml</param>
    /// <returns>bool con respuesta existosa o fallida</returns>
    [WebMethod(Description = "[Aprueba un inscrito]")]
    public bool ApruebaInscripcionAudiencia(string strNumeroInscripcion, bool blnAprobado, string strMotivo)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "ApruebaInscripcionAudiencia";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, strNumeroInscripcion + " " + blnAprobado.ToString() + strMotivo, "", 0);
            AudienciaFachada objAudienciaFachada = new AudienciaFachada();
            //bool aprobado = objAudienciaFachada.ApruebaInscritosAudiencia(strNumeroInscripcion, blnAprobado, strNumeroInscripcion + " " + blnAprobado.ToString() + strMotivo);
            bool aprobado = objAudienciaFachada.ApruebaInscritosAudiencia(strNumeroInscripcion, blnAprobado, strMotivo);
            return aprobado;
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            return false;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, strNumeroInscripcion, strNoVital, iAA, iIdPadre);
        }
    }



    /// <summary>
    /// Recibe un edicto de audiencia pública
    /// </summary>
    /// <param name="datosComunicacionXML">Datos del edicto en formato xml</param>
    /// <returns>bool con respuesta existosa o fallida</returns>
    [WebMethod(Description = "[Finalización de audiencia]", MessageName = "FinalizarAudiencia")]
    public void FinalizarAudiencia(string idExpediente)
    {
        try
        {
            SILPA.LogicaNegocio.Audiencia.Audiencia aud = new SILPA.LogicaNegocio.Audiencia.Audiencia();
            aud.FinalizarAudiencia(idExpediente);
        }
        catch (Exception ex)
        {
            string strException = "Validar los pasos efectuados al Finalizar Audiencia.";
            throw new Exception(strException, ex);
        }
    }

    [WebMethod(Description = "[Obtiene los documentos de un inscrito a audiencia publica]")]
    public string ObtenerDocumentos(string numeroVitalAudiencia)
    {
        SILPA.LogicaNegocio.Audiencia.Audiencia aud = new SILPA.LogicaNegocio.Audiencia.Audiencia();
        return aud.ObtenerDocumentos(numeroVitalAudiencia);
    }


    [WebMethod(Description = "[Obtiene el documento de un inscrito a audiencia publica]")]
    public Byte[] ObtenerDocumento(string numeroVitalAudiencia, string nombreArchivo)
    {
        SILPA.LogicaNegocio.Audiencia.Audiencia aud = new SILPA.LogicaNegocio.Audiencia.Audiencia();
        return aud.ObtenerDocumento(numeroVitalAudiencia, nombreArchivo);
    }


    [WebMethod(Description = "[Obtiene el listado de solicitantes para audiencia]")]
    public string ConsultarSolicitanteAudienciaPublicaGenEdi(int idprocess, int sol_id_aa, string exp_cod)
    {
        SILPA.LogicaNegocio.Audiencia.Audiencia aud = new SILPA.LogicaNegocio.Audiencia.Audiencia();
        return aud.ConsultarSolicitanteAudienciaPublicaGenEdi(idprocess, sol_id_aa, exp_cod);
    }


    [WebMethod(Description = "[Obtiene el listado de solicitantes para audiencia]")]
    public string ConsultarSolicitanteAudienciaPublica(int idprocess, int sol_id_aa, Int32 mov_id)
    {
        SILPA.LogicaNegocio.Audiencia.Audiencia aud = new SILPA.LogicaNegocio.Audiencia.Audiencia();
        return aud.ConsultarSolicitanteAudienciaPublica(idprocess, sol_id_aa, mov_id);
    }


    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }
}

