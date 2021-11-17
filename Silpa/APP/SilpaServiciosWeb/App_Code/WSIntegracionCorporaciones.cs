using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SILPA.Servicios.IntegracionCorporacion;
using SoftManagement.Log;

/// <summary>
/// Descripción breve de WSIntegracionCorporaciones
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSIntegracionCorporaciones : System.Web.Services.WebService {

    [WebMethod(Description = "[Obtiene la Informacion del Usuario que se Conecto por la pagina web]")]
    public string ObtenerDatosUsuario(string strUsuario)
    {
        Int64 intIdPadre = 0;
        string strDatosRecibidos = "";
        string strRespuestaXML = "";
        IntegracionCorporacionFacade objFachada = null;

        try
        {
            //Insertar log
            strDatosRecibidos = "strUsuario: " + (!string.IsNullOrEmpty(strUsuario) ? strUsuario : "null");
            intIdPadre = SoftManagement.LogWS.SMLogWS.EscribirInicio(this.ToString(), "ObtenerDatosUsuario", strDatosRecibidos, "", 0);

            //Generar certificado
            objFachada = new IntegracionCorporacionFacade();
            strRespuestaXML = objFachada.ObtenerDatosUsuario(strUsuario);

        }
        catch (Exception ex)
        {
            SoftManagement.LogWS.SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerDatosUsuario", ex.ToString(), intIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SoftManagement.LogWS.SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerDatosUsuario", strRespuestaXML, "", 0, intIdPadre);
        }

        return strRespuestaXML;
    }

}
