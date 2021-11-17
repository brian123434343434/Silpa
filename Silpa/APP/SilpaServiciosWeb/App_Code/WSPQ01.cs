using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using SILPA.LogicaNegocio.Generico;
using SILPA.Servicios.Publicacion;
using SILPA.AccesoDatos.Publicacion;
using SILPA.AccesoDatos;
using SILPA.Comun;
using System.Xml;
using SoftManagement.Log;
using SoftManagement.LogWS;


/// <summary>
/// Summary description for WSPQ01
/// Servicio de publicaicon
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSPQ01 : System.Web.Services.WebService
{

    public WSPQ01()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Recibe los datos de la AA para publicar
    /// </summary>
    /// <param name="datosPublicacionXML">Paquete de datos en XML con los datos de publicación</param>
    /// <returns>Estructura XML con la respuesta satisfactoria o fallida</returns>
    [WebMethod(Description = "[Recibe los datos entregados por la Autoridad Ambiental para realizar una publicación]", MessageName = "[Recibir Publicación CU-CAP-01]")]
    public bool RecibirPublicacion(string datosPublicacionXML)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "RecibirPublicacion", datosPublicacionXML, "",0);

            PublicacionIdentity ObjPublicacionIdentity = (PublicacionIdentity)(new PublicacionIdentity()).Deserializar(datosPublicacionXML);
            //Se envia informacion          
            PublicacionFachada _objFachada = new PublicacionFachada();
            _objFachada.InsertarPublicacion(ObjPublicacionIdentity);
            iAA = _objFachada._objPublicacion.PubIdentity.IdAutoridad;
            
            return true;
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "RecibirPublicacion", ex.ToString(), iIdPadre);
            string strException = "Validar los pasos efectuados al recibir los datos de la Autoridad Ambiental para publicar.";
            throw new Exception(strException, ex);
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "RecibirPublicacion", datosPublicacionXML, "", iAA, iIdPadre);
        }
    }
    
    /// <summary>
    /// Método que recibe los datos de una pubicación y adjunta los documentos mediante XML
    /// </summary>
    /// <param name="datosPublicacionXML">string XML con los datos de la publicación</param>
    /// <param name="listaDocumentos">string: XML con el listado de los documentos asociados a la publicación</param>
    /// <returns>string </returns>
    [WebMethod]
    public bool RecibirPublicacionDocumentos(string datosPublicacionXML, string listaDocumentos)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "RecibirPublicacionDocumentos", datosPublicacionXML + " " + listaDocumentos, "", 0);

            //Se envia informacion          
            PublicacionFachada _objFachada = new PublicacionFachada();
            _objFachada.InsertarPublicacion(datosPublicacionXML, listaDocumentos);
            iAA = _objFachada._objPublicacion.PubIdentity.IdAutoridad;
            return true;
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "RecibirPublicacionDocumentos", ex.ToString(), iIdPadre);
            return false;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "RecibirPublicacionDocumentos", datosPublicacionXML + " " + listaDocumentos, "", iAA, iIdPadre);
        }
    }


    /// <summary>
    /// Recibe los datos de la AA para publicar
    /// </summary>
    /// <param name="datosPublicacionXML">Paquete de datos en XML con los datos de publicación</param>
    /// <returns>identificador de la publicacion. En caso de fallo retorna -1</returns>
    [WebMethod(Description = "[Recibe los datos entregados por la Autoridad Ambiental para realizar una publicación y retorna identificador de esta]")]
    public long CrearPublicacion(string datosPublicacionXML)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        long lngIdentificadorPublicacion = -1;

        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "CrearPublicacion", datosPublicacionXML, "", 0);

            PublicacionIdentity ObjPublicacionIdentity = (PublicacionIdentity)(new PublicacionIdentity()).Deserializar(datosPublicacionXML);
            //Se envia informacion          
            PublicacionFachada _objFachada = new PublicacionFachada();
            lngIdentificadorPublicacion = _objFachada.InsertarPublicacion(ObjPublicacionIdentity);
            iAA = _objFachada._objPublicacion.PubIdentity.IdAutoridad;

        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "CrearPublicacion", ex.ToString(), iIdPadre);
            string strException = "Validar los pasos efectuados al recibir los datos de la Autoridad Ambiental para publicar.";
            throw new Exception(strException, ex);
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "CrearPublicacion", "lngIdentificadorPublicacion: " + lngIdentificadorPublicacion.ToString(), "", iAA, iIdPadre);
        }

        return lngIdentificadorPublicacion;
    }


    /// <summary>
    /// Elimina la publicacion indicada
    /// </summary>
    /// <param name="p_lngPublicaionID">long con el identificador de la publicacion</param>
    /// <returns>true en caso de que se realice la eliminacion de la publicacion</returns>
    [WebMethod]
    public bool EliminarPublicacionDocumento(long p_lngPublicacionID)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "EliminarPublicacionDocumento", "p_lngPublicacionID: " + p_lngPublicacionID.ToString(), "", 0);

            //Se envia informacion          
            PublicacionFachada _objFachada = new PublicacionFachada();
            _objFachada.EliminarPublicacion(p_lngPublicacionID);
            iAA = _objFachada._objPublicacion.PubIdentity.IdAutoridad;
            return true;
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "EliminarPublicacionDocumento", ex.ToString(), iIdPadre);
            return false;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "EliminarPublicacionDocumento", "p_lngPublicacionID: " + p_lngPublicacionID.ToString(), "", iAA, iIdPadre);
        }
    }


    /// <summary>
    /// Actualiza la fecha de desfijacion de la pubi=licacion actual
    /// </summary>
    /// <param name="p_lngPublicacionID">long con el identificador de la publicacion</param>
    /// <param name="p_objFechaDesfijacion">DateTime con la fecha de desfijacion</param>
    [WebMethod]
    public bool ActualizarDesfijarPublicacion(long p_lngPublicacionID, DateTime p_objFechaDesfijacion)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        try
        {
            iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ActualizarDesfijarPublicacion", "p_lngPublicacionID: " + p_lngPublicacionID.ToString() + " -- p_objFechaDesfijacion: " + p_objFechaDesfijacion.ToString(), "", 0);

            //Se envia informacion          
            PublicacionFachada _objFachada = new PublicacionFachada();
            _objFachada.ActualizarDesfijarPublicacion(p_lngPublicacionID, p_objFechaDesfijacion);
            iAA = _objFachada._objPublicacion.PubIdentity.IdAutoridad;
            return true;
        }
        catch (Exception ex)
        {
            //SMLog.Escribir(ex);
            SMLogWS.EscribirExcepcion(this.ToString(), "ActualizarDesfijarPublicacion", ex.ToString(), iIdPadre);
            return false;
        }
        finally
        {
            SMLogWS.EscribirFinalizar(this.ToString(), "ActualizarDesfijarPublicacion", "p_lngPublicacionID: " + p_lngPublicacionID.ToString() + " -- p_objFechaDesfijacion: " + p_objFechaDesfijacion.ToString(), "", iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }
    


}

