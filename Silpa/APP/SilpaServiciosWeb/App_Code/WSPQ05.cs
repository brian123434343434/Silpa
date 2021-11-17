using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text;
using SILPA.Servicios;
using System.IO;
using SILPA.Servicios.Generico;
using SoftManagement.Log;
using SoftManagement.LogWS;


/// <summary>
/// Summary description for WSPQ05
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSPQ05 : System.Web.Services.WebService
{

    public WSPQ05()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Recibe el documento
    /// </summary>
    /// <param name="datosCobroXML">Paquete de Datos en XML con la información del cobro</param>
    /// <returns>string en formato XML con la respuesta de la operación</returns>
    [WebMethod(Description = "[Recibe los datos del cobro entregados por la Autoridad Ambiental para generar el formulario de cobro]", MessageName = "[CU-GN-09]")]
    public string RecibirDatosCobro(string datosCobroXML)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        try
        {     

            CobroFachada _objCobro = new CobroFachada();
            _objCobro.CrearCobro(datosCobroXML, DatosSesion.Usuario);
            return "Éxito";
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "RecibirDatosCobro", ex.ToString(), iIdPadre);
            string strException = "Validar los pasos efectuados al recibir los datos del cobro entregados por la Autoridad Ambiental para generar el formulario de cobro.";
            throw new Exception(strException, ex);
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "RecibirDatosCobro", datosCobroXML, strNoVital, iAA, iIdPadre);
        }

    }

    /// <summary>
    /// Recibe el documento
    /// </summary>
    /// <param name="datosCobroXML">Paquete de Datos en XML con la información del cobro</param>
    /// <returns>string en formato XML con la respuesta de la operación</returns>
    [WebMethod(Description = "[Retorna los Valores correspondientes a los pagos realizados por PSR]")]
    public string ConsultarDatosCobro(string IdExpediente)
    {
        try
        {
            CobroFachada _objCobro = new CobroFachada();
            return _objCobro.ConsultarCobro(IdExpediente);             
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
            throw;
        }

    }


    /// <summary>
    /// Prueba para validar el funcionamiento de condiciones de gattaca
    /// </summary>
    /// <param name="datosCobroXML">Paquete de Datos en XML con la información gattaca</param>
    /// <returns>string en formato XML con la respuesta de la operación</returns>
    [WebMethod(Description = "[Recibe los datos de BPM]", MessageName = "RecibirXML")]
    public string RecibirXML(string mensaje)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "RecibirXML", mensaje, "", 0);

            //SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "++++Inicio RecibirXML...");
            ArctividadBPMFachada recibir = new ArctividadBPMFachada();
            string retorno = recibir.ProcesarEstadoDAAXML(mensaje);
            //string retorno = ArctividadBPMFachada.ProcesarEstadoDAAXML(mensaje);
            //SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "++++retorno" + retorno);
            return retorno;
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "++++Error WSPQ05.RecibirXML..." + ex.Message);
            SMLogWS.EscribirExcepcion(this.ToString(), "RecibirXML", ex.ToString(), iIdPadre);
            throw;
        }
        finally
        {
            //SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "++++Termino WSPQ05.RecibirXML...");
            SMLogWS.EscribirFinalizar(this.ToString(), "RecibirXML", mensaje, strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public string RecibirTest(string mensaje)
    {
        try
        {            
            ArctividadBPMFachada recibir = new ArctividadBPMFachada();
            string retorno = recibir.ProcesarEstadoDAAXML(mensaje);
            //string retorno = ArctividadBPMFachada.ProcesarEstadoDAAXML(mensaje);
            retorno = retorno.Replace("False", "True");            
            return retorno;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "++++Error WSPQ05.RecibirTest..." + ex.Message);
            throw;
        }        
    }

}

