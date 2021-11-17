using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Generico;
using System.Configuration;
using System.IO;
using Subgurim.Controles;
using System.Data;
using SILPA.LogicaNegocio.ParametrizacionUrlSolicitudAutAmb;
using SILPA.AccesoDatos.Generico;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class ParametrizacionUrlSolicitudAutAmb_ParametrizacionUrlSolicitudAutAmb : System.Web.UI.Page
{
    public DataTable dt_resultado
    {
        get
        {
            return (DataTable)ViewState["dt_resultado"]; ;
        }

        set
        {
            ViewState["dt_resultado"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["Usuario"] = 22361;
            if (this.ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                //Iniciliazar datos de la página
                CargarPagina();
            }
        }
    }

    private bool ValidacionToken()
    {
        if (DatosSesion.Usuario == string.Empty)
        {
            return false;
        }

        string idUsuario = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

        Session["IDForToken"] = (object)idUsuario;

        Session["Usuario"] = Session["IDForToken"];

        SILPA.LogicaNegocio.Usuario.TokenUsuario token = new SILPA.LogicaNegocio.Usuario.TokenUsuario();

        Session["Token"] = token.TomarTokenUsuario(Int32.Parse(Session["IDForToken"].ToString()));

        using (WSValidacionToken.GattacaSecurityServices9000 servicio = new WSValidacionToken.GattacaSecurityServices9000())
        {
            servicio.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSValidacionToken");
            string mensaje = servicio.GetUserInfoByToken("Softmanagement", Session["Token"].ToString());
            //string mensaje = "Token valido";

            if (mensaje.IndexOf("Token invalido") > 0)
                return false;
        }
        return true;
    }

    public void CargarPagina()
    {

        lblTituloPrincipal.Text = "PARAMETRIZACION URL INFORMATIVA Y CODIGOS USUARIOS CORPORACIONES POR TIPO DE TRAMITE Y AUTORIDAD AMBIENTAL";


        DataSet _dTipoTRamite = new DataSet();
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        ParametrizacionUrlSolicitudAutAmb ParametrizacionUrlSolicitudAutAmb = new ParametrizacionUrlSolicitudAutAmb();
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridades(null).Tables[0], cboAutoridadAmbiental, "AUT_NOMBRE", "AUT_ID", true);
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridades(null).Tables[0], CboAutAmbientalEdit, "AUT_NOMBRE", "AUT_ID", false);

        _dTipoTRamite = ParametrizacionUrlSolicitudAutAmb.ObtenerListaTramites();
        Utilidades.LlenarComboTabla(_dTipoTRamite.Tables[0], CboTramite, "NOMBRE_TIPO_TRAMITE", "ID", true);
        Utilidades.LlenarComboTabla(_dTipoTRamite.Tables[0], CboTipoTramiteEdit, "NOMBRE_TIPO_TRAMITE", "ID", false);




    }

    protected void BtnConsultar_Click(object sender, EventArgs e)
    {
        ConsultarRegistros();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Eliminar_Url")
        {
            EliminarRegistro(e.CommandArgument.ToString());
        }

        if (e.CommandName == "Editar_Url")
        {
            CargarPantallaEdicion(e.CommandArgument.ToString());
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GVASIGANCIONSERIES_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GVASIGANCIONSERIES_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVASIGANCIONSERIES.PageIndex = e.NewPageIndex;
        GVASIGANCIONSERIES.DataSource = dt_resultado;
        GVASIGANCIONSERIES.DataBind();
    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        string URL = TxtUrl.Text;
        Int32 ID_PARTICIPANTE = 0;

        if (!string.IsNullOrEmpty(URL))
        {
            URL = URL.Replace("\t", string.Empty).Trim().Replace("\r", string.Empty).Replace("\n", string.Empty);
        }
        else
        {
            URL = string.Empty;
        }

        
        if (!string.IsNullOrEmpty(TxtIdParticipante.Text))
        {
            ID_PARTICIPANTE = Convert.ToInt32(TxtIdParticipante.Text);
        }
        else
        {
            ID_PARTICIPANTE = 0;
        }
        

        if (ActualizarRegistro(Convert.ToInt32(this.CboAutAmbientalEdit.SelectedValue), Convert.ToInt32(this.CboTipoTramiteEdit.SelectedValue), URL, ID_PARTICIPANTE))
        {
            ConsultarRegistros();
            limpiar_modal();
        }
    }

    protected void BtnCancelarEdicion_Click(object sender, EventArgs e)
    {
        limpiar_modal();
        mpeValidarNumeracion.Hide();
    }

    protected void limpiar_modal()
    {
        TxtUrl.Text = string.Empty;
        CboAutAmbientalEdit.Enabled = true;
        CboTipoTramiteEdit.Enabled = true;
    }

    public void ConsultarRegistros()
    {
        DataSet ds = new DataSet();

        int ID_AA;
        int ID_TRAMITE ;

        if (!Int32.TryParse(this.cboAutoridadAmbiental.SelectedValue, out ID_AA))
        {
            ID_AA = -1;
        }

        if (!Int32.TryParse(this.CboTramite.SelectedValue, out ID_TRAMITE))
        {
            ID_TRAMITE = -1;
        }
        ParametrizacionUrlSolicitudAutAmb ParametrizacionUrlSolicitudAutAmb = new ParametrizacionUrlSolicitudAutAmb();
        ds = ParametrizacionUrlSolicitudAutAmb.ConsultarUrlSolicitudXAutAmb(ID_AA, ID_TRAMITE);
        GVASIGANCIONSERIES.DataSource = ds;
        GVASIGANCIONSERIES.DataBind();
    }

    public bool ActualizarRegistro(int ID_AA, int ID_TRAMITE, string URL, Int32 ID_PARTICIPANTE)
    {
        bool respuesta = true;
        try
        {
            ParametrizacionUrlSolicitudAutAmb ParametrizacionUrlSolicitudAutAmb = new ParametrizacionUrlSolicitudAutAmb();
            ParametrizacionUrlSolicitudAutAmb.ActualizarUrlSolicitudXAutAmb(ID_AA, ID_TRAMITE, URL, ID_PARTICIPANTE);
            return respuesta;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void EliminarRegistro(string id)
    {

        string[] arreglo = id.Split('|');
        int ID_AA = Convert.ToInt32(arreglo[0].ToString());
        int ID_TRAMITE = Convert.ToInt32(arreglo[1].ToString());
        ParametrizacionUrlSolicitudAutAmb ParametrizacionUrlSolicitudAutAmb = new ParametrizacionUrlSolicitudAutAmb();
        ParametrizacionUrlSolicitudAutAmb.EliminarUrlSolicitudXAutAmb(ID_AA, ID_TRAMITE);
        ConsultarRegistros();
    }

    protected void BtnNuevoregistro_Click(object sender, EventArgs e)
    {
        if (CboAutAmbientalEdit.Items.Count == 0)
        {
            CboAutAmbientalEdit.Items.Clear();
            SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
            Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridades(null).Tables[0], CboAutAmbientalEdit, "AUT_NOMBRE", "AUT_ID", false);
        }

        if (CboTipoTramiteEdit.Items.Count == 0)
        {
            CboTipoTramiteEdit.Items.Clear();
            DataSet _dTipoTRamite = new DataSet();
            ParametrizacionUrlSolicitudAutAmb ParametrizacionUrlSolicitudAutAmb = new ParametrizacionUrlSolicitudAutAmb();
            _dTipoTRamite = ParametrizacionUrlSolicitudAutAmb.ObtenerListaTramites();
            Utilidades.LlenarComboTabla(_dTipoTRamite.Tables[0], CboTipoTramiteEdit, "NOMBRE_TIPO_TRAMITE", "ID", false);
        }

        mpeValidarNumeracion.Show();
    }

    protected void CargarPantallaEdicion(string id)
    {

        string[] arreglo = id.Split('|');
        string ID_AA = arreglo[0].ToString();
        string ID_TRAMITE = arreglo[1].ToString();
        string URL = arreglo[2].ToString().Replace("\t", string.Empty).Trim().Replace("\r",string.Empty).Replace("\n",string.Empty);
        string ID_PARTICIPANTE = arreglo[3].ToString();



        mpeValidarNumeracion.Show();
        CboTipoTramiteEdit.SelectedValue = ID_TRAMITE.ToString();
        CboAutAmbientalEdit.SelectedValue = ID_AA.ToString();
        CboAutAmbientalEdit.Enabled = false;
        CboTipoTramiteEdit.Enabled = false;
        TxtUrl.Text = URL;
        TxtIdParticipante.Text = ID_PARTICIPANTE;
    }


}