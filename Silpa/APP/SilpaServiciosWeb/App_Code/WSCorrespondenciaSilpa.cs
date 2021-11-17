using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using System.Data;
using SILPA.LogicaNegocio;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Correspondencia;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.DAA;
using SoftManagement.Log;
using SoftManagement.LogWS;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ws_correspondencia : System.Web.Services.WebService
{

    public ws_correspondencia() { }

    [WebMethod]
    public DataSet listarEstados(string str_visible)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "listarEstados";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, str_visible, "", 0);
            CorrespondenciaSilpaDalc _objCorres = new CorrespondenciaSilpaDalc();
            return _objCorres.listarEstados(str_visible);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, str_visible, strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod]
    public DataSet consultarMovimientos
        (string str_cod_dependencia, string str_nur, string str_remitente, 
        DateTime dte_fecha_desde, DateTime dte_fecha_hasta, string str_ciclo_id, 
        string str_estado_id, string str_asunto_resumen, string str_numero_silpa, string strIdAA)
        {
        string sMensaje = str_cod_dependencia+" "+str_nur+" "+str_remitente+" "+dte_fecha_desde.ToString()+" "+dte_fecha_hasta.ToString()+" "+str_ciclo_id+" "+str_estado_id+" "+str_asunto_resumen+" "+str_numero_silpa+" "+strIdAA;

        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "consultarMovimientos";

         try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, sMensaje, "", 0);
            CorrespondenciaSilpaDalc _objCorres = new CorrespondenciaSilpaDalc();
            DataSet dsData = _objCorres.consultarMovimientos(str_cod_dependencia, str_nur, str_remitente, dte_fecha_desde, dte_fecha_hasta, str_ciclo_id, str_estado_id, str_asunto_resumen, str_numero_silpa, strIdAA);
            return dsData;
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }

        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, sMensaje, strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod]
    public Movimiento consultarMovimientoxID(int int_mov_id)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "consultarMovimientoxID";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, int_mov_id.ToString(), "", 0);
            CorrespondenciaSilpaDalc _objCorres = new CorrespondenciaSilpaDalc();
            return _objCorres.consultarMovimientoxID(int_mov_id);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            //throw new ApplicationException(ex.Message, ex); 
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, int_mov_id.ToString(), strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod]
    public Movimiento consultarMovimientoxNUR(string str_nur)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "consultarMovimientoxNUR";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, str_nur, "", 0);
            CorrespondenciaSilpaDalc _objCorres = new CorrespondenciaSilpaDalc();
            return _objCorres.consultarMovimientoxNUR(str_nur);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, str_nur, strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod]
    public DataSet listarGrupoMovimientos(string str_movimientos)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "listarGrupoMovimientos";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, str_movimientos.ToString(), "", 0);
            CorrespondenciaSilpaDalc _objCorres = new CorrespondenciaSilpaDalc();
            return _objCorres.listarGrupoMovimientos(str_movimientos);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, str_movimientos, strNoVital, iAA, iIdPadre);
        }
    }


    /// <summary>
    /// Obtiene los datos necesarios para crear la carpeta (expediente en sila) en Sila.
    /// Caso de uso CU-COR-02-Crear Carpeta.
    /// </summary>
    /// <param name="int intIdRadicacion">Int: identificador de la radicación en SILPA</param>
    /// <returns>DataSet:  conjunto de resultados de DataSet</returns>
    [WebMethod]
    public DataSet ObtenerDatosCrearCarpeta(int intIdRadicacion)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "ObtenerDatosCrearCarpeta";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, intIdRadicacion.ToString(), "", 0);
            CorrespondenciaSilpaDalc _objCorres = new CorrespondenciaSilpaDalc();
            return _objCorres.ObtenerDatosCrearCarpeta(intIdRadicacion);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, intIdRadicacion.ToString(), strNoVital, iAA, iIdPadre);
        }
    }


    /// <summary>
    /// Método para obtener datos para la actividad nueva
    /// </summary>
    /// <param name="intIdRadicacion">id_radicacion</param>
    /// <returns></returns>
    [WebMethod]
    public DataSet ObtenerDatosAbrirActividad(Int64 intIdRadicacion)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "ObtenerDatosAbrirActividad";


        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, intIdRadicacion.ToString(), "", 0);
            CorrespondenciaSilpaDalc _objCorres = new CorrespondenciaSilpaDalc();
            return _objCorres.ObtenerDatosActividadNueva(intIdRadicacion);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, intIdRadicacion.ToString(), strNoVital, iAA, iIdPadre);
        }
    }


    /// <summary>
    /// Método para reasignar la autoridad ambiental a la radicación... 
    /// </summary>
    /// <param name="intIdRadicacion">identificador de la radicación</param>
    /// <param name="intAutIdAsignada"> autoridad asignada</param>
    /// <param name="intAutIdEntrega">Autoridad que entrega</param>
    /// <returns>Exito / Fracaso de la operación: </returns>
    [WebMethod]
    public bool ReasignarAutoridadRadicacion(string datosEnvio)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "ReasignarAutoridadRadicacion";

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, datosEnvio, "", 0);
            CorrespondenciaSilpaDalc _objCorres = new CorrespondenciaSilpaDalc();
            SILPA.LogicaNegocio.DAA.DAA objDaa = new SILPA.LogicaNegocio.DAA.DAA();
            objDaa.ReenviarSolicitud(datosEnvio);
            //bool result = _objCorres.ReasignarAutoridadRadicacion(intIdRadicacion, intAutIdAsignada, intAutIdEntrega);
            return true;
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }

        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, datosEnvio, strNoVital, iAA, iIdPadre);
        }
    }

   

    /// <summary>
    /// Método que obtiene el listado de documentos radicados desde Silpa
    /// </summary>
    /// <param name="intIdExpediente">identificador del expediente en SilaMC</param>
    /// <param name="intAutId">Identificador de la autoridad ambiental</param>
    /// <returns>bool: true / false</returns>
    [WebMethod]
    public DataSet ObtenerCorresPondenciaPorExpediente(string numeroSilpa, int autId)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "ObtenerCorresPondenciaPorExpediente";

        try
        {
            //CorrespondenciaSilpaDalc _objCorres = new CorrespondenciaSilpaDalc();
            //return _objCorres.CorresPondenciaPorExpediente(intIdExpediente, intAutId);

            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, numeroSilpa + " " + autId.ToString(), "", 0);
            Correspondencia _objCorres = new Correspondencia();
            return _objCorres.CorresPondenciaPorExpediente(numeroSilpa, autId);
            
            //DataSet ds = new DataSet();
            //ds.ReadXml(@"D:\hava\xsd\ObtenerCorresPondenciaPorExpediente.xml");
            //string resultado = ds.GetXml();
            //return ds;
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, numeroSilpa + " " + autId.ToString(), strNoVital, iAA, iIdPadre);
        }
    }


    /// <summary>
    /// Obtiene el correo de la autoridad ambiental
    /// </summary>
    /// <param name="autId">int con el identificador de la autoridad</param>
    /// <returns>string con el correo de la autoridad ambiental</returns>
    [WebMethod]
    public string ObtenerCorreoAutoridadAmbiental(int autId)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "ObtenerCorreoAutoridadAmbiental";

        try
        {

            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, autId.ToString(), "", 0);
            SILPA.LogicaNegocio.Generico.AutoridadAmbiental objAutoridad = new SILPA.LogicaNegocio.Generico.AutoridadAmbiental();
            return objAutoridad.ObtenerCorreoAutoridadAmbiental(autId);
        }
        catch (Exception ex)
        {
            SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, autId.ToString(), strNoVital, iAA, iIdPadre);
        }
    }



    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

    [WebMethod]
    public string ayes()
    {
        RadicacionDocumentoIdentity objEntity = new RadicacionDocumentoIdentity();
        objEntity.NumeroSilpa = "5489";        
        objEntity.NumeroRadicadoAA = "WsCorponor";
        objEntity.IdRadicacion=4888;
        objEntity.IdAA=186;
        SILPA.Comun.XmlSerializador ser = new SILPA.Comun.XmlSerializador();
        string xml = ser.serializar(objEntity);
        return "Mi Nombre es ayes - Feat Moises";
    }


}
