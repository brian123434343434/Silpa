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
using SoftManagement.Log;

public partial class plantillas_SilpaArchivos : System.Web.UI.MasterPage
{
    #region VARIABLES DE CONFIGURACIÓN DEL SISTEMA

    protected string URL_TESTSILPA = System.Configuration.ConfigurationManager.AppSettings["URL_TESTSILPA"].ToString();

    #endregion

    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblFecha.Text = DateTime.Today.ToString("dddd, dd 'de' MMMM 'de' yyyy");
        if (Session["UserOld"] != null)
        {
            if (Session["User"].ToString() != Session["UserOld"].ToString())
            {
                lblInfoImpers.Text = "Usuario: " + Session["UserOld"].ToString() + " actuando en nombre de: " + Session["User"];
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
    protected void lnkFinalizarImpersonalizacion_Click(object sender, EventArgs e)
    {
        Session["User"] = Session["UserOld"];
        Session["UserOld"] = null;    
        Response.Redirect(String.Format(ConfigurationManager.AppSettings["URLFinalizarSilpa"].ToString()));
    }
}
