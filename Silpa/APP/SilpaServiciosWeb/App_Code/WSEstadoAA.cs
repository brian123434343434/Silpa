using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using System.Data;
using SILPA.LogicaNegocio.EstadoAA; 
using SILPA.AccesoDatos.EstadoAA; 
using SoftManagement.Log;
using SoftManagement.LogWS;


/// <summary>
/// Summary description for WSEstadoAA
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSEstadoAA : System.Web.Services.WebService
{

    public WSEstadoAA()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "Inserta unh registro para cambio de estado por AA")]
    public void CambioEstadoAA(string xmlMensaje)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "CambioEstadoAA";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, xmlMensaje, "", 0);
            EstadoAA est = new EstadoAA(xmlMensaje);
            est.Insertar();
            return;
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            throw;
        }

        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, xmlMensaje, strNoVital, iAA, iIdPadre);
        }

        
    }

    [WebMethod(Description = "realiza una prueba de conexion con el servicio")]
    public void Test()
    {
        return;
    }

    

}

