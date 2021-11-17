using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using SILPA.LogicaNegocio.Sancionatorio;
using SILPA.Servicios.Sancionatorio;
using SILPA.LogicaNegocio.Comunicacion;
using SILPA.Comun;
using System.Xml;
using SoftManagement.Log;


/// <summary>
/// Summary description for WSSAN
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSSAN : System.Web.Services.WebService
{

    public WSSAN()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //public string HelloWorld()
    //{
    //    return "Hello World";
    //}

    [WebMethod(Description = "[Recibe los datos de la queja]")]
    public string RespuestaQuejaDenuncia(string xmlRespuestaQueja)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";

        try
        {
            iIdPadre = SoftManagement.LogWS.SMLogWS.EscribirInicio(this.ToString(), "RespuestaQuejaDenuncia", xmlRespuestaQueja, "", 0);
            SancionatorioFachada _objSanFachada = new SancionatorioFachada();
            _objSanFachada.EnviarInformacionQueja(xmlRespuestaQueja);
            return "Exito";
        }
        catch (Exception ex)
        {
            SoftManagement.LogWS.SMLogWS.EscribirExcepcion(this.ToString(), "RespuestaQuejaDenuncia", ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SoftManagement.LogWS.SMLogWS.EscribirFinalizar(this.ToString(), "RespuestaQuejaDenuncia", xmlRespuestaQueja, strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

}

