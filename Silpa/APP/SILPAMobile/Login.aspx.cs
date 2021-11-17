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
using SILPA.LogicaNegocio.Generico;
using SILPA.Comun;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        bool Authenticated = false;
        Authenticated = SiteLevelCustomAuthenticationMethod(Login1.UserName, Login1.Password);
        e.Authenticated = Authenticated;        
      //  FormsAuthentication.SetAuthCookie(Login1.UserName, true);
        if (Authenticated == true)
        {
            FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);
        }
    }
    private bool SiteLevelCustomAuthenticationMethod(string UserName, string Password)
    {
        bool boolReturnValue = false;
        // Insert code that implements a site-specific custom 
        // authentication method here.
        // This example implementation always returns false.

        Persona objPersona = new Persona();        
        string EncritpPassword = EnDecript.Encriptar(Password);
        boolReturnValue = objPersona.ConsultarPersonaPol(UserName, EncritpPassword);
        return boolReturnValue;        
    } 

}
