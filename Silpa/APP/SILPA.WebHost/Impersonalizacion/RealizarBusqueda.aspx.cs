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
using SILPA.Comun;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;


public partial class Impersonalizacion_RealizarBusqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarInformacionInicial();
        }
    }

    private void CargarInformacionInicial()
    {
        Listas _listaTiposId = new Listas();
        DataSet _temp = _listaTiposId.ListaTipoIdentificacion();
        cboTipoIdentificacion.DataSource = _temp.Tables[0]; // ListaDocumentos(_temp, "TPE_ID = " + Convert.ToString((int)TipoPersona.Natural));
        cboTipoIdentificacion.DataValueField = "TID_ID";
        cboTipoIdentificacion.DataTextField = "TID_NOMBRE";
        cboTipoIdentificacion.DataBind();
        cboTipoIdentificacion.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void btnIniciar_Click(object sender, EventArgs e)
    {        
        Session["UserOld"] = DatosSesion.Usuario;
        Session["User"] = Usuario;                        
        Response.Redirect(String.Format(ConfigurationManager.AppSettings["URLImpersonalizar"].ToString(), Usuario));
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        Label lblMensaje = (Label)this.Master.FindControl("lblMensaje");
        PersonaDalc persona = new PersonaDalc();
        DataTable datos = persona.ConsultarPersona(Convert.ToInt32(this.cboTipoIdentificacion.SelectedValue), this.txtNumeroIdentificacion.Text);
        if (datos.Rows.Count > 0)
        {
            lblMensaje.Text = "";
            this.lblNombre.Text = datos.Rows[0]["NOMBRE"].ToString();
            this.Usuario = datos.Rows[0]["Name"].ToString();
            this.btnIniciar.Visible = true;
        }
        else
        {
            this.lblNombre.Text = "";
            this.btnIniciar.Visible = false;
            Mensaje.MostrarMensaje(this.Page, "No existe información.");

        }
    }

    private string Usuario 
    {
        get { return ViewState["Usuario"].ToString(); }
        set { ViewState["Usuario"] = value; }
    }
}
