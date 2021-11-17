using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Silpa.Workflow.AccesoDatos;

public partial class BandejaTareas_Redirecciona : System.Web.UI.Page
{
    private const string SilpaUserNameSesionId = "SilpaUserName";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["accion"] != null)
        {
            string pagina = Request.QueryString["accion"].ToString();
            string silpaUserName = Convert.ToString(Session[SilpaUserNameSesionId]);

            if (String.IsNullOrEmpty(silpaUserName) || silpaUserName != User.Identity.Name)
            {
               // EstablercerVariablesSesion();
            }
            Server.Transfer(pagina + ".aspx?" + Request.QueryString);
        }
        else
            throw new Exception("No es posible redireccionar porque no se recibió el parámetro accion.");
    }

    //protected void EstablercerVariablesSesion()
    //{
    //    long superUser = 1;
    //    SecurityFacade facade = new SecurityFacade();
    //    String clientName = "SoftManagement";//ConfigurationManager.AppSettings("clientName");
    //    ApplicationCredentials applicationCredentials;
    //    applicationCredentials = PublicFunction.buildApplicationCredentials(clientName, Request, superUser, Session.SessionID);

    //    facade.doLogin(applicationCredentials, "administrator", "admingtk", Gattaca.Entity.eSecurity.AuthenticationMode.SecurityOnly, Session, Request);

        //Session["ApplicationCredentials"] = applicationCredentials;
        //Session["AuthenticatedWFAdmin"] = true;
        //Session["UserID"] = applicationCredentials.UserID;
    //}
}
