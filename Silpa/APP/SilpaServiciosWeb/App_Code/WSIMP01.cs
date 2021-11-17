using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Threading;
using SoftManagement.Log;
using SILPA.AccesoDatos.ImpresionesFus;
using SoftManagement.LogWS;
using SILPA.Servicios.Generico.RadicarDocumento;
using SILPA.LogicaNegocio.Generico;

/// <summary>
/// Descripción breve de WSIMP01
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSIMP01 : System.Web.Services.WebService
{

    public WSIMP01()
    {
                
    }

    [WebMethod(Description = "[Agrega el Id del proceso para su impresion fisica en alguno ubicacion del servidor de aplicaciones o archivos]", MessageName = "AdicionarProcesoImpresionFus")]
    public void AdicionarProcesoImpresionFus(int idProceso)
    {
        Int64 iIdPadre = 0;        
        String sDesMetodo = "AdicionarProcesoImpresionFus";
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, idProceso.ToString(), "", 0);
            RadicacionDocumento objRadicacion = new RadicacionDocumento();
            objRadicacion.ObtenerRadicacion(null, (int)idProceso);
            SILPA.Servicios.ImpresionFUS.ImpresionFUSFachada.GenerarFus(objRadicacion._objRadDocIdentity.Id, (int)(idProceso), objRadicacion._objRadDocIdentity.UbicacionDocumento, objRadicacion._objRadDocIdentity.NumeroVITALCompleto, objRadicacion._objRadDocIdentity.IdSolicitante);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);            
            throw;
            
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, "Finaliza", idProceso.ToString(), 0,iIdPadre);
        }

    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

}

