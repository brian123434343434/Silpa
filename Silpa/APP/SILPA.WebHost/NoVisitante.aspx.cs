using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class NoVisitante : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ClientIP = this.Request.UserHostName;  

        SILPA.LogicaNegocio.Usuario.Usuario objUsuario = new SILPA.LogicaNegocio.Usuario.Usuario();
        System.Net.IPHostEntry hostInfo = System.Net.Dns.Resolve(this.Request.UserHostName);                     
        this.lblNumeroVisitante.Text = objUsuario.ConsultarUsuarioVisitanteNo(Session.SessionID.ToString());

    }
}
