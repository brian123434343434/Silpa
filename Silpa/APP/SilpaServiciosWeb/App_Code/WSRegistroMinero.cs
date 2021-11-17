using SILPA.AccesoDatos.Generico;
using SILPA.LogicaNegocio.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SoftManagement.LogWS;

/// <summary>
/// Summary description for WSRegistroMinero
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WSRegistroMinero : System.Web.Services.WebService {

    public WSRegistroMinero () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod(Description = "Obtiene la lista de los registros mineros creados, modificados y eliminados despues de la fecha enviada como parametro. Se envía como parametro la fecha inicial de busqueda.")]
    public RespuestaWsConsultaRegistrosMinerosEntity ConsultarRegistrosMineros(DateTime fechaConsulta)
    {
        var respuesta = new RespuestaWsConsultaRegistrosMinerosEntity();
        Int64 iIdPadre = 0;
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ConsultarRegistrosMineros", "Fecha Consulta: " + fechaConsulta.ToString(),"", 0);
            ConsultaRegistroMinero objConsultaRegistrosMineros = new ConsultaRegistroMinero();
            var listaRegistrosMineros = objConsultaRegistrosMineros.ConsultarRegistroMinero(fechaConsulta);
            respuesta.ListaRegistrosMineros = listaRegistrosMineros;
            respuesta.CantidadRegistro = listaRegistrosMineros.Count;
        }
        catch (Exception ex)
        {
            respuesta.Error = true;
            respuesta.TextoError = ex.Message;
            SMLogWS.EscribirExcepcion(this.ToString(), "ConsultarRegistrosMineros", ex.ToString(), iIdPadre);
        }
        finally {
            SMLogWS.EscribirFinalizar(this.ToString(), "ConsultarRegistrosMineros", "Fecha Consulta: " + fechaConsulta.ToString(), "", 0, iIdPadre);
        } 

        return respuesta;
    }
}
