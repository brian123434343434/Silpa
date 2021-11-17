using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos;
using SILPA.Comun;
using SILPA.LogicaNegocio;
using SILPA.Servicios.RUIA;
using SoftManagement.Log;
using SoftManagement.LogWS;


/// <summary>
/// Summary description for WSPQ07
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSPQ07 : System.Web.Services.WebService
{

    public WSPQ07()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "Retorna el listado de tipos de tramite")]
    public List<SILPA.AccesoDatos.Parametrizacion.TipoTramite> Tramites()
    {
        try
        {
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion tramite = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
            return tramite.Tramites();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
            throw;
        }
    }

      [WebMethod(Description = "Retorna el listado de tipos de tramite segun el parametro enviado")]
    public SILPA.AccesoDatos.Parametrizacion.TipoTramite ListarTramites(int idTramite)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ListarTramites", idTramite.ToString(), "", 0);
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion tramite = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
            return tramite.Tramites(idTramite);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), "ListarTramites", ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "ListarTramites", idTramite.ToString(), strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "Retorna el listado de tipos de documento")]
    public string Documentos()
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "Documentos", "", "", 0);
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion documento = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
            return documento.Documentos();
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "Documentos", ex.ToString(), iIdPadre);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "Documentos", "", strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "Retorna las listas de objetos básicos utilizados en RUIA")]
    public string listasRUIA()
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "listasRUIA", "", "", 0);
            return new RUIAFachada().consultaTotal();
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "listasRUIA", ex.ToString(), iIdPadre);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "listasRUIA", "", strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

    //21-jun-2010 - aegb
    [WebMethod(Description = "Retorna las listas de objetos básicos utilizados en homologacion de datos")]
    public string ListasDatosHomologacion(int idTabla)
    {
        try
        {
            SILPA.LogicaNegocio.Generico.Listas datos = new SILPA.LogicaNegocio.Generico.Listas();
            return datos.ObtenerDatosHomologacion(idTabla);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
            throw;
        }
    }

        //22092020 - FRS
    [WebMethod(Description = "Retorna el ApplicationUser a partir del Persona Id")]
    public string ListarApplicationUserComplementoHomologacion(int idPersona)
    {
        try
        {
            SILPA.LogicaNegocio.Generico.Listas datos = new SILPA.LogicaNegocio.Generico.Listas();
            return datos.ObtenerApplicationUserComplementoHomologacion(idPersona);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
            throw;
        }
    }



    //22092020 - FRS
    [WebMethod(Description = "Retorna si la persona se encuentra activa a partir del Persona Id")]
    public int ObtenerSiPersonaActivaHomologacion(int idPersona) 
    {
        try
        {
            SILPA.LogicaNegocio.Generico.Listas datos = new SILPA.LogicaNegocio.Generico.Listas();
            return datos.ObtenerSiPersonaActivaHomologacion(idPersona);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
            throw;
        }
    }



    //22042020 JNS
    [WebMethod(Description = "Retorna el listado de clasificaciones de informacion adicional")]
    public string ListaClasificacionesInformacionAdicional(string p_strDescripcion, bool? p_blnActivo)
    {
        try
        {
            SILPA.LogicaNegocio.Generico.ClasificacionInformacionGeneral objClasificacionInformacionGeneral = new SILPA.LogicaNegocio.Generico.ClasificacionInformacionGeneral();
            return objClasificacionInformacionGeneral.ObtenerClasificaciones(p_strDescripcion, p_blnActivo);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
            throw;
        }
    }


    //22042020 JNS
    [WebMethod(Description = "Retorna la de la clasificacion")]
    public string ObtenerClasificacionInformacionAdicional(int clasificacionID)
    {
        try
        {
            SILPA.LogicaNegocio.Generico.ClasificacionInformacionGeneral objClasificacionInformacionGeneral = new SILPA.LogicaNegocio.Generico.ClasificacionInformacionGeneral();
            return objClasificacionInformacionGeneral.ObtenerClasificacionInformacionAdicional(clasificacionID);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
            throw;
        }
    }


}

