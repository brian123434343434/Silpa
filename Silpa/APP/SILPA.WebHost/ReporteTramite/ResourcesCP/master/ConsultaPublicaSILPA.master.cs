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

public partial class plantillas_Silpa : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //if ((Session["Autenticado"]!=null) && (Session["Autenticado"].ToString() == "Si"))
        //{
        //    mMenu.DataSourceID = "xdsMenuFuncionario";            
        //}
        //else
        //{
        //    mMenu.DataSourceID = "xdsMenu";            
        //}

        ////this.lblMensaje.Text = string.Empty;   
        //string strFileMenu = ConfigurationManager.AppSettings["RUTA_XML"] + "mMenu" + DatosSesion.DatosUsuario.MenuAsociado + ".xml";
        //// this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>alert('" + DatosSesion.Usuario + ".')</script>");
        ////this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>alert('" + DatosSesion.DatosUsuario.MenuAsociado + ".')</script>");
        //if (!System.IO.File.Exists(strFileMenu))
        //{
        //    strFileMenu = ConfigurationManager.AppSettings["RUTA_XML"] + "mMenuVacio.xml";
        //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>alert('El menú no esta configurado correctamente, comuniquese con el administrador.')</script>");
        //}

        //this.xdsMenu.DataFile = strFileMenu;
        //this.xdsMenu.XPath = "/*/*";

        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>alert('" + Session["UserOld"].ToString()+ ".')</script>");

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
