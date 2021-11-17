using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using SoftManagement.Log;
using SoftManagement.LogWS;

/// <summary>
/// Permite utilizar el comportamiento de la logica fileTraffic usado en SILPA
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSFileTraffic : System.Web.Services.WebService
{

    private SILPA.Comun.TraficoDocumento objFileTraffic;

    public WSFileTraffic()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Recibe el documento a almacenar en el FileTraffic
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public bool RecibirDocumento
        (string strNumeroSilpa, 
        string strUsuario,List<Byte[]> lstBytesDocumento, 
        List<String> lstStrNombreDocumento)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "RecibirDocumento";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, strNumeroSilpa +" "+ strUsuario, "", 0);
            string ruta = string.Empty;
            objFileTraffic = new TraficoDocumento();
            List<string> lstNombres = lstStrNombreDocumento;
            return objFileTraffic.RecibirDocumento(strNumeroSilpa, strUsuario, lstBytesDocumento, ref lstStrNombreDocumento, ref ruta);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            return false;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, strNumeroSilpa + " " + strUsuario, strNoVital, iAA, iIdPadre);
        }
    }



    /// <returns>True/ false</returns>


    /// <summary>
    /// Método que recibe los archivos adjuntos mediante un documento XML
    /// </summary>
    /// <param name="strNumeroSilpa">string: numero silpa del proceso, o el numero de la queja</param>
    /// <param name="strUsuario">string: usuario que utilizar el servicio (si es nulo se indica que es una queja)</param>
    /// <param name="strXmlData">string: xml datos con los documentos adjuntos</param>
    /// <returnsr>True: exito / False: Fracaso </returns>
    [WebMethod(Description="Método que recibe los archivos adjuntos mediante un documento XML")]
    public bool RecibirDocumentoPorXml(string strNumeroSilpa, string strUsuario, string strXmlData)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = strNumeroSilpa;
        String sDesMetodo = "RecibirDocumentoPorXml";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, strNumeroSilpa + " " + strUsuario, "", 0);
            objFileTraffic = new TraficoDocumento();
            //return objFileTraffic.RecibirDocumento(strNumeroSilpa, strUsuario, strXmlData);
            return true;
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            return false;
        }

        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, strNumeroSilpa + " " + strUsuario, strNoVital, iAA, iIdPadre);
        }
    }

    /// <summary>
    /// Método para listar los formularios disponibles en el bpm
    /// </summary>
    [WebMethod]
    public DataSet ListarFormulariosBpm(Int64 idProcessInstance)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "ListarFormulariosBpm";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, idProcessInstance.ToString(), "", 0);
            FormularioDalc frm = new FormularioDalc();
            return frm.ListarFormulariosBPM(idProcessInstance);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            return null;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, idProcessInstance.ToString(), strNoVital, iAA, iIdPadre);
        }
    }


    /// <summary>
    /// Método que obtiene el formulario
    /// </summary>
    /// <param name="strXmlData">contiene: Idforminstance , Numero_Silpa </param>
    /// <returns>Xml: con la información del formulario </returns>
    [WebMethod]
    public DataSet ObtenerDatosFormulariosProceso(Int64 idProcessInstance)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "ObtenerDatosFormulariosProceso";

        try
        {
            /// clases que consultan los datos a la base de datos del bpm 
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, idProcessInstance.ToString(), "", 0);
            FormularioDalc frm = new FormularioDalc();
            return frm.ObtenerDatosFormulariosPorProceso(idProcessInstance);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            return null;
        }

        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, idProcessInstance.ToString(), strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

}

