using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using SoftManagement.Log;
using SoftManagement.LogWS;
using System.Data;
using System.Threading;


/// <summary>
/// Descripción breve de WSCES02
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSCES02 : System.Web.Services.WebService
{

    public WSCES02()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "[Recibe los datos PARAMETROS necesarios para ejecutar el cambio de cesionario como se especifica en los CUS Ces.01 y Ces.02]", MessageName = "CambiarCesionario")]
    public void CambiarCesionario(string nVital, string cesionario)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = Int32.Parse(nVital);
        String strNoVital = nVital;
        String sDesMetodo = "CambiarCesionario";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, nVital +" "+ cesionario, "", 0);
            string usuarioNuevo;
            SILPA.LogicaNegocio.CesionDeDerechos.Cesion ces;
            {
                ces = new SILPA.LogicaNegocio.CesionDeDerechos.Cesion(nVital);
                usuarioNuevo = ces.ConsultarIdUsuarioPorSilpa();
            }

            ces = new SILPA.LogicaNegocio.CesionDeDerechos.Cesion(nVital, usuarioNuevo, cesionario);
            ces.Ejecutar();
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
        }

        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, nVital +" "+ cesionario,"", iAA, iIdPadre);
        }
    }


    /// <summary>
    /// hava:28-sep-10
    /// Cambia el derecho sobre los trámites
    /// </summary>
    /// <param name="nVital">string: número vital del trámite</param>
    /// <param name="cedente">string: identificador del cedente</param>
    /// <param name="cesionario">string: identificador del cesionario</param>
    [WebMethod(Description = "[Recibe los datos PARAMETROS necesarios para ejecutar el cambio de cesionario como se especifica en los CUS Ces.01 y Ces.02]", MessageName = "CambiarDerechos")]
    public void CambiarDerechos(string nVital, string cedente, string cesionario)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = nVital;
        String sDesMetodo = "CambiarDerechos";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, nVital + " " + cesionario, "", 0);
            string usuarioNuevo;

            SILPA.LogicaNegocio.CesionDeDerechos.Cesion ces;
            {
                ces = new SILPA.LogicaNegocio.CesionDeDerechos.Cesion(nVital);
                usuarioNuevo = ces.ConsultarIdUsuarioPorSilpa();
            }

           // ces = new SILPA.LogicaNegocio.CesionDeDerechos.Cesion(nVital, usuarioNuevo, cesionario);
            ces = new SILPA.LogicaNegocio.CesionDeDerechos.Cesion(nVital, cedente, cesionario);
            ces.Ejecutar();

        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);

            string strException = "Validar los pasos efectuados al cambiar el derecho sobre los trámites.";
            throw new Exception(strException, ex);
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, nVital + " " + cesionario, "", iAA, iIdPadre);
        }
    }


    [WebMethod(Description = "[Recibe los datos PARAMETROS necesarios para ejecutar el cambio de cesionario como se especifica en los CUS Ces.01 y Ces.02]", MessageName = "CambiarCesionarioXSD")]
    public void CambiarCesionarioXSD(string datosCesion)
    {
        DataSet DS = new DataSet();
        DS.ReadXmlSchema(datosCesion);
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "CambiarCesionarioXSD";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, datosCesion, "", 0);
            string usuarioNuevo;
            SILPA.LogicaNegocio.CesionDeDerechos.Cesion ces;
            SILPA.AccesoDatos.CesionDeDerechos.CesionEntity entidad = new SILPA.AccesoDatos.CesionDeDerechos.CesionEntity();
            {
                SILPA.Servicios.CesionDerechos.CesionFechada c = new SILPA.Servicios.CesionDerechos.CesionFechada();
                entidad = c.LeerCesion(datosCesion);
                ces = new SILPA.LogicaNegocio.CesionDeDerechos.Cesion(entidad.NumeroSilpa);
                usuarioNuevo = ces.ConsultarIdUsuarioPorSilpa();
            }

            ces = new SILPA.LogicaNegocio.CesionDeDerechos.Cesion(entidad.NumeroSilpa, usuarioNuevo, entidad.IdCesionario);
            ces.Ejecutar();
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
       }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, datosCesion, strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

}

