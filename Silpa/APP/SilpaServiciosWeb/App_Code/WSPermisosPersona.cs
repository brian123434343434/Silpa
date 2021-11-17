using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Threading;
using SoftManagement.Log;
using SILPA.Servicios.Generico;
using SILPA.LogicaNegocio.Usuario;
using SILPA.Servicios.Usuario;
using SILPA.AccesoDatos.Generico;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;


/// <summary>
/// Summary description for wsPermisosPersona
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class wsPermisosPersona : System.Web.Services.WebService
{

    public wsPermisosPersona()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    /// <summary>
    /// Si es acto administrativo y requiere notificación se reenvia al servicio de emitirDocumento
    /// Si es un acto administrativo y nno requiere notificación se envía el correo al solicitante
    /// Si es un oficio se envía el documento al correo del solicitante
    /// </summary>
    /// <param name="documentoXML"></param>
    /// <returns></returns>
    [WebMethod(Description = "[Marcar los permisos necesarios para un usuario", MessageName = "CU-USU-02")]
    public bool Activarpersona(string xmlObject)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "Activarpersona";

        try
        {
            iIdPadre = SoftManagement.LogWS.SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, xmlObject, "", 0);
            ThreadPool.QueueUserWorkItem(new WaitCallback(UsuarioFachada.ActivarUsuario), (object)xmlObject);
            return true;
        }
        catch (Exception ex)
        {
            SoftManagement.LogWS.SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            return false;
        }
        finally
        {
            SoftManagement.LogWS.SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, xmlObject.ToString(), strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "[Devuelve los datos con los tipos de Usuarios", MessageName = "CU-USU-03")]
    public string ObtenerTiposDeUsuario()
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        String sDesMetodo = "ObtenerTiposDeUsuario";


        try
        {
            iIdPadre = SoftManagement.LogWS.SMLogWS.EscribirInicio(this.ToString(), sDesMetodo, "", "", 0);
            TipoUsuario objTipoUsuario = new TipoUsuario();
            List<TipoUsuarioIdentity> arrayUsuarios = objTipoUsuario.CargarTipoUsuarios();
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer serializador = new XmlSerializer(typeof(List<TipoUsuarioIdentity>));
            serializador.Serialize(memoryStream, arrayUsuarios);

            return System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
        }
        catch (Exception ex)
        {
            SoftManagement.LogWS.SMLogWS.EscribirExcepcion(this.ToString(), sDesMetodo, ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SoftManagement.LogWS.SMLogWS.EscribirFinalizar(this.ToString(), sDesMetodo, "", strNoVital, iAA, iIdPadre);
        }
    }
    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

}

