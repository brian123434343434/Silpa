using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using SILPA.Servicios.BPMProcess;
using SILPA.Servicios.BPMProcess.Entidades;


/// <summary>
/// Summary description for WSProcess
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSProcess : System.Web.Services.WebService
{

    public WSProcess()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod(Description = "Crear proceso.")]
    
    public string CrearProceso(string ClientId, Int64 FormId, Int64 UserID, string ValoresXML)
    {
        BPMProcess objProcess = new BPMProcess();
        return objProcess.CrearProcesoBPM(ClientId, FormId, UserID, ValoresXML);
    }

    [WebMethod(Description = "Crear proceso SILA.")]
    public string CrearProcesoAutoridad(Int64 TramiteId, Int64 PerId, Int64 AutId,string ValoresXML)
    {
        try
        {
	        BPMProcess objProcess = new BPMProcess();
	        return objProcess.CrearProcesoAutoridad(TramiteId, PerId, AutId, ValoresXML);
        }
        catch (Exception ex)
        {
            string strException = "Validar los pasos efectuados al Crear proceso en SILA.";
            throw new Exception(strException, ex);
        }
    }


    [WebMethod(Description = "Registrar un formulario en VITAL.")]
    public string RegistrarFormularioProceso(string strUsuarioAutorizado, long lngFormularioID, long lngUsuarioFormularioID, string xmlFormulario)
    {
        try
        {
            BPMProcess objProcess = new BPMProcess();
            return objProcess.RegistrarFormularioProceso(strUsuarioAutorizado, lngFormularioID, lngUsuarioFormularioID, xmlFormulario);
        }
        catch (Exception ex)
        {
            throw new Exception("Error durante el registro del formulario en el sistema", ex);
        }
    }


    [WebMethod(Description = "Finaliza el proceso actual relacionada al numero VITAL indicado.")]
    public string FinalizarProcesoNumeroVITAL(string strNumeroVITAL)
    {
        try
        {
            BPMProcess objProcess = new BPMProcess();
            return objProcess.FinalizarProcesoNumeroVITAL(strNumeroVITAL);
        }
        catch (Exception ex)
        {
            throw new Exception("Error durante la finalizacion del proceso", ex);
        }
    }


    [WebMethod(Description = "Obtiene la lista de los formularios disponibles para ser utilizados")]
    public string ObtenerFormularios()
    {
        BPMProcess objProcess = new BPMProcess();
        return objProcess.ObtenerFormularios();
    } 

    [WebMethod(Description = "Obtiene la lista De campos de un formulario Especifico")]
    public string ObtenerCampos(Int64 FormId)
    {
        BPMProcess objProcess = new BPMProcess();
        return objProcess.ObtenerCampos(FormId);
    }


    [WebMethod(Description = "Obtiene la lista de los registros pendientes por radicar en SIGPRO. Se envían como parametros: identificador de la autoridad, fecha inicial (yyyy-MM-dd HH:mm:ss)  y fecha final (yyyy-MM-dd HH:mm:ss)")]
    public RespuestaWsConsultaRegistrosRadicarSigproEntity ConsultarRegistrosRadicarSigpro(int idAutoridadAmbiental, string fechaInicial, string fechaFinal)
    {
        var respuesta = new RespuestaWsConsultaRegistrosRadicarSigproEntity();

        try
        {
            BPMProcess objProcess = new BPMProcess();
            var listaRespuesta = objProcess.ObtenerRegistrosPendientesRadicacionSigpro(idAutoridadAmbiental, fechaInicial, fechaFinal);            
            respuesta.ListaRegistrosRadicarSigpro = listaRespuesta;
            respuesta.CantidadRegistro = listaRespuesta.Count;
        }
        catch (Exception ex)
        {
            respuesta.Error = true;
            respuesta.TextoError = ex.Message;
        }        

        return respuesta;        
    }


    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

}

