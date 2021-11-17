using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using SILPA.LogicaNegocio;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos;
using SILPA.Servicios;
using SILPA.Servicios.RUIA;
using System.Data;
using System.Threading;
using SoftManagement.Log;
using SoftManagement.LogWS;

/// <summary>
/// Summary description for WSRUIA
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSRUIA : System.Web.Services.WebService
{

    public WSRUIA()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //public string HelloWorld()
    //{
    //    return "Hello World";
    //}       

    [WebMethod(Description = "[Recibe los datos del RUIA]", MessageName = "CU-RUI-02")]
    public bool RecibirDatosRUIA(string xmlObject)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "RecibirDatosRUIA", xmlObject, "", 0);
            RUIAFachada.GuardarRuia(xmlObject);
            return true;
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "RecibirDatosRUIA", ex.ToString(), iIdPadre);
            string strException = "Validar los pasos efectuados al recibir los datos del RUIA.";
            throw new Exception(strException, ex);
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "RecibirDatosRUIA", xmlObject, strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "[Recibe los datos del RUIA para Actualizar Fecha Ejecucion]")]
    public bool RecibirDatosRUIAEjecucion(string xmlObject)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "RecibirDatosRUIAEjecucion", xmlObject, "", 0);
            RUIAEjecutoriaFachada.ActualizarSancionEjecutoria(xmlObject);
            return true;
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), "RecibirDatosRUIAEjecucion", ex.ToString(), iIdPadre);
            return false;
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "RecibirDatosRUIAEjecucion", xmlObject, strNoVital, iAA, iIdPadre);
        }
    }


    [WebMethod(Description = "[Recibe los datos del RUIA para Actualizar Fecha Cumplimiento]", MessageName = "CU-RUI-10")]
    public bool RecibirDatosRUIACumplimiento(string xmlObject)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "RecibirDatosRUIACumplimiento", xmlObject, "", 0);
            RUIAEjecutoriaFachada.ActualizarSancion(xmlObject);
            return true;
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), "RecibirDatosRUIACumplimiento", ex.ToString(), iIdPadre);
            return false;
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "RecibirDatosRUIACumplimiento", xmlObject, strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }
}

