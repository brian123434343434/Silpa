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

public partial class controles_User : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*if (Session["ApplicationUserEntity"] != null)
        {
            
        }*/
        this.lblUsuario.Text = DatosSesion.DatosUsuario.NombreUsuario; //HttpContext.Current.User.Identity.Name;
        this.lblAcceso.Text = DatosSesion.DatosUsuario.UltimoLogin.ToString(); //"yyyy/MM/dd hh:mm:ss"
        // this.lblUsuario.Text = ((Session("ApplicationUserEntity"))Gattaca.Entity.eSecurity.ApplicationUserEntity).Name;
    }
}
