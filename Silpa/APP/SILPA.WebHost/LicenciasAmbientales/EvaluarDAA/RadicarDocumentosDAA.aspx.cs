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

public partial class LicenciasAmbientales_LPA_02a_Evaluar_Necesidad_DAA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtFechaRadicacion.Text = DateTime.Now.ToString();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/LicenciasAmbientales/LPA_02_Evaluar_Necesidad_DAA.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
