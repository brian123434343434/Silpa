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

public partial class Informacion_GenerarPublicacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (this.cboVigencia.SelectedValue == "0")
            Response.Redirect("Fijacion.aspx");
        else if (cboVigencia.SelectedValue == "1")
            Response.Redirect("Publicacion.aspx");
    }
}
