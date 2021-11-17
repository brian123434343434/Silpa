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
using SILPA.Comun;

public partial class plantillas_SilpaIdioma : System.Web.UI.MasterPage
{
    #region VARIABLES DE CONFIGURACIÓN DEL SISTEMA

        protected string URL_TESTSILPA = System.Configuration.ConfigurationManager.AppSettings["URL_TESTSILPA"].ToString(); 

    #endregion

    /// <summary>
    /// Cargar los datos de la idioma
    /// </summary>
    /// <returns>string con el nombre del idioma seleccionado</returns>
    private string CargarDatosIdioma()
    {
        string strIdioma = "";

        //Limpiar Desplegable
        this.cboIdioma.Items.Clear();

        //Cargar el idioma contenido en sesion
        if (Session["Idioma"] != null && !string.IsNullOrWhiteSpace(Session["Idioma"].ToString()))
        {
            //Cargar idioma
            strIdioma = Session["Idioma"].ToString();

            //Cargar desplegable según idioma
            if (strIdioma == "en")
            {
                this.lblIdioma.Text = "Language:";
                this.cboIdioma.Items.Add(new ListItem("Select", ""));
                this.cboIdioma.Items.Add(new ListItem("Spanish", "es"));
                this.cboIdioma.Items.Add(new ListItem("English", "en"));
                this.lnkVolverHome.Attributes.Remove("title");
                this.lnkVolverHome.Attributes.Add("title", "Back to Home");
                this.lnkVolverHome.Attributes.Remove("href");
                this.lnkVolverHome.Attributes.Add("href", this.URL_TESTSILPA);
            }
            else
            {
                this.lblIdioma.Text = "Idioma:";
                this.cboIdioma.Items.Add(new ListItem("Seleccione", ""));
                this.cboIdioma.Items.Add(new ListItem("Español", "es"));
                this.cboIdioma.Items.Add(new ListItem("Ingles", "en"));
                this.lnkVolverHome.Attributes.Remove("title");
                this.lnkVolverHome.Attributes.Add("title", "Regresar a la página principal");
                this.lnkVolverHome.Attributes.Remove("href");
                this.lnkVolverHome.Attributes.Add("href", this.URL_TESTSILPA);
            }

            //Seleccionar valor
            this.cboIdioma.SelectedValue = strIdioma;
        }
        else
        {
            this.lblIdioma.Text = "Idioma / Language:";
            this.cboIdioma.Items.Add(new ListItem("Seleccione / Select", ""));
            this.cboIdioma.Items.Add(new ListItem("Español / Spanish", "es"));
            this.cboIdioma.Items.Add(new ListItem("Ingles / English", "en"));
            this.cboIdioma.SelectedValue = "";
            this.lnkVolverHome.Attributes.Remove("title");
            this.lnkVolverHome.Attributes.Add("title", "Regresar a la página principal / Back to Home");
            this.lnkVolverHome.Attributes.Remove("href");
            this.lnkVolverHome.Attributes.Add("href", this.URL_TESTSILPA);
        }

        //Cargar idioma a retornar
        if (!string.IsNullOrWhiteSpace(strIdioma))
        {
            strIdioma = (strIdioma == "es" ? Idiomas.Espanol.ToString() : Idiomas.Ingles.ToString());
        }

        return strIdioma;
    }


    /// <summary>
    /// Inicializar la pagina
    /// </summary>
    private void InicializarPagina()
    {
        string strIdioma = "";

        //Cargar los datos según el idioma seleccionado
        strIdioma = this.CargarDatosIdioma();

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InicializarPagina();
        }
    }

    protected void lnkFinalizarImpersonalizacion_Click(object sender, EventArgs e)
    {
        Session["User"] = Session["UserOld"];
        Session["UserOld"] = null;    
        Response.Redirect(String.Format(ConfigurationManager.AppSettings["URLFinalizarSilpa"].ToString()));
    }

    protected void cboIdioma_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Verificar que hallan seleccionado una opción
        if (this.cboIdioma.SelectedValue != "0")
        {
            Session["Idioma"] = this.cboIdioma.SelectedValue;
        }
        else
        {
            Session["Idioma"] = null;
        }

        this.InicializarPagina();
    }
}
