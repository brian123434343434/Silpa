using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.Generico;

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
            return "administrator"; 
        }
    }
}
