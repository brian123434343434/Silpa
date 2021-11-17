using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class SILPANuevoUsuarios : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
         {
             this.lblFecha.Text =DateTime.Today.ToString("dddd, dd 'de' MMMM yyyy");
             if (Session["UserOld"] != null)
             {
                 if (Session["User"].ToString() != Session["UserOld"].ToString())
                 {
                     lblInfoImpers.Text = "Usuario: " + Session["UserOld"].ToString() + " actuando en nombre de: " + Session["User"].ToString();
                     lnkFinalizarImpersonalizacion.Visible = true;
                 }
                 else
                     lnkFinalizarImpersonalizacion.Visible = false;
             }
             else
             {
                 lblInfoImpers.Visible = false;
                 lnkFinalizarImpersonalizacion.Visible = false;
             }
         }
    }
    protected void lnkFinalizarImpersonalizacion_Click(object sender, EventArgs e)
    {
        Session["User"] = Session["UserOld"];
        Session["UserOld"] = null;
        Response.Redirect(String.Format(ConfigurationManager.AppSettings["URLFinalizarSilpa"].ToString()));
    }
}
