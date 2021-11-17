using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SILPA.AccesoDatos.Generico;
using Silpa.Workflow.Entidades;
using Silpa.Workflow.AccesoDatos;

/// <summary>
/// Summary description for DatosSesion
/// </summary>
public static class DatosSesion
{
    private const string UserNameSesionId = "UserName";
    private const string IdRelatedSesionId = "IdRelated";

    public static string Usuario
    {
        get 
        {
            //return "26032011";
            return HttpContext.Current.User.Identity.Name;
        }
    }

    public static DatosUsuario DatosUsuario
    {
        get 
        {
            HttpContext.Current.Session["DatosUsuario"] = ApplicationUserDao.ConsultarDatosUsuario(DatosSesion.Usuario);
         
            return (DatosUsuario)HttpContext.Current.Session["DatosUsuario"];
        }
    }

    /// <summary>
    /// Identificador del usuariom logeado.
    /// </summary>
    public static long IdUsuario
    {
        get
        {
            if (String.IsNullOrEmpty(Usuario))
            {
                //Convertir a userName y asignar a userName
                PersonaDalc persona = new PersonaDalc();
                PersonaIdentity identityPersona = persona.BuscarPersonaByUserId(Usuario);

                return identityPersona.IdSolicitante;
            }
            else { return 0; }
        }

    }

}

