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

public partial class Impersonalizacion_FinalizarImpersonalizacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["UserOld"] = null;
        Session["User"] = null;
        Response.Redirect(String.Format(ConfigurationManager.AppSettings["URLImpersonalizar"], "") + "&modo=F");
    }
}
