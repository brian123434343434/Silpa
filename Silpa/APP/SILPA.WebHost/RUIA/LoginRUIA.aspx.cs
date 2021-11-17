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

public partial class LoginRUIA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarAutoridades();
        }
        if ((Request.QueryString.Count > 0) && (Request.QueryString["salir"] == "si"))
        {
            Session.Remove("Autenticado");
            Session.Remove("Autoridad_Ambiental");
        }
    }
    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        if (cboAutoridadAmbiental.SelectedValue != "-1")
        {
            if (Membership.ValidateUser(txtUsuario.Text, txtClave.Text))
            {
                Session.Add("Autenticado", "Si");
                Session.Add("Autoridad_Ambiental", cboAutoridadAmbiental.SelectedValue);
                FormsAuthentication.SetAuthCookie(txtUsuario.Text, false);
                Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Session["Autenticado"] != null)
                {
                    Session.Remove("Autenticado");
                    Session.Remove("Autoridad_Ambiental");
                }
                lblMensaje.Visible = true;
                lblMensaje.Text = "Usuario no válido para ingresar al sistema";
                txtUsuario.Text = "";
                txtClave.Text = "";
            }
        }
        else
        {
            lblMensaje.Text = "Seleccione una autoridad Ambiental";
            lblMensaje.Visible = true;
            txtUsuario.Text = "";
            txtClave.Text = "";
        }
    }

    #region Funciones programador ...

    /// <summary>
    /// Funcion que carga el listado de Autoridades Ambientales
    /// </summary>
    private void CargarAutoridades()
    {
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        cboAutoridadAmbiental.DataSource = _listaAutoridades.ListarAutoridades(null);
        cboAutoridadAmbiental.DataValueField = "AUT_ID";
        cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
        cboAutoridadAmbiental.DataBind();
        //cboAutoridadAmbiental.Items.Insert(0, new ListItem("ADMINISTRACION", "0"));
        cboAutoridadAmbiental.Items.Insert(0, new ListItem("Seleccione....", "-1"));
    }

    #endregion    
}
