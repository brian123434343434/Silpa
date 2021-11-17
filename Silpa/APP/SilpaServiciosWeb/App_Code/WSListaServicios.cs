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
using SoftManagement.Log;
using SoftManagement.LogWS;


/// <summary>
/// Descripción breve de WSListaServicios
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSListaServicios : System.Web.Services.WebService
{

    public WSListaServicios()
    {
        
    }

    [WebMethod(Description = "[retorna el listado de Metodos por coincidencia del identificador del metodo]")]
    public List<SILPA.AccesoDatos.ListaServicios.Metodo> Metodo(int idMetodo)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "Metodo";


        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, idMetodo.ToString(), "", 0);
            SILPA.LogicaNegocio.ListaServicios.ListaServicios ls = new SILPA.LogicaNegocio.ListaServicios.ListaServicios();
            return ls.Metodo(idMetodo);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            return null;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, idMetodo.ToString(), strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "[retorna el listado de Metodos por coincidencia del identificador del metodo y del servicio]")]
    public List<SILPA.AccesoDatos.ListaServicios.Metodo> MetodoServicioFull(int idMetodo, int idServicio)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "Metodo";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, idMetodo.ToString() + " " + idServicio.ToString(), "", 0);
            SILPA.LogicaNegocio.ListaServicios.ListaServicios ls = new SILPA.LogicaNegocio.ListaServicios.ListaServicios();
            return ls.MetodoServicio(idMetodo, idServicio);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            return null;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, idMetodo.ToString() + " " + idServicio.ToString(), strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "[retorna el listado de Metodos por coincidencia del identificador del servicio]")]
    public List<SILPA.AccesoDatos.ListaServicios.Metodo> MetodoServicio(int idServicio)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "MetodoServicio";

        try
        {
            SILPA.LogicaNegocio.ListaServicios.ListaServicios ls = new SILPA.LogicaNegocio.ListaServicios.ListaServicios();
            return ls.MetodoServicio(idServicio);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            return null;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, idServicio.ToString() + " " + idServicio.ToString(), strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "[retorna el listado de Servicios]")]
    public List<SILPA.AccesoDatos.ListaServicios.Servicio> Servicios()
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "Servicios";
        
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, "", "", 0);
            SILPA.LogicaNegocio.ListaServicios.ListaServicios ls = new SILPA.LogicaNegocio.ListaServicios.ListaServicios();
            return ls.Servicios();
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            return null;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, "", strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "[retorna el listado de Metodos]")]
    public List<SILPA.AccesoDatos.ListaServicios.Metodo> Metodos()
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "Servicios";

        try
        {
            SILPA.LogicaNegocio.ListaServicios.ListaServicios ls = new SILPA.LogicaNegocio.ListaServicios.ListaServicios();
            return ls.Metodos();
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            return null;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, "", strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

}


