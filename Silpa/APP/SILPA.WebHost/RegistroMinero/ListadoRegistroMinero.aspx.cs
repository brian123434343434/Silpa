using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.RegistroMinero;
using System.Text;
using SILPA.AccesoDatos.Generico;
using System.Data;
using System.Configuration;
using System.Drawing;

public partial class RegistroMinero_ListadoRegistroMinero : System.Web.UI.Page
{
    public string _usuarioRegistrado = string.Empty;
    public PersonaIdentity personaIdentity;
    public PersonaIdentity personaIdentityView
    {
        get { return (PersonaIdentity)ViewState["personaIdentity"]; }
        set { ViewState["personaIdentity"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
     {
        if (!IsPostBack)
        {
            if (ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                this._usuarioRegistrado = (string)Session["Usuario"];
                PersonaDalc per = new PersonaDalc();
                this.personaIdentity = per.BuscarPersonaByUserId(this._usuarioRegistrado);
                personaIdentityView = personaIdentity;
                
                PersonaIdentity p = new PersonaIdentity();
                int idAA = 0;
                string autNombre = per.ObtenerAutoridadPorPersona(long.Parse(this._usuarioRegistrado), out idAA);

                SILPA.LogicaNegocio.Usuario.Usuario usuario = new SILPA.LogicaNegocio.Usuario.Usuario();
                DataTable dtautoridad = usuario.ConsultarUsuarioSistemaCompania(DatosSesion.Usuario);
                SILPA.AccesoDatos.Generico.ParametroDalc _parametroDalc = new SILPA.AccesoDatos.Generico.ParametroDalc();
                SILPA.AccesoDatos.Generico.ParametroEntity _parametro = new SILPA.AccesoDatos.Generico.ParametroEntity();
                _parametro.IdParametro = -1;
                _parametro.NombreParametro = "ConsultaMinero";
                _parametroDalc.obtenerParametros(ref _parametro);

                this.GridRegistroMin.Columns[9].Visible = false;
               DataRow[] dtrGrupoConsulta = dtautoridad.Select("IDUserGroup = " +_parametro.Parametro.Trim());
               if (dtrGrupoConsulta.Length > 0)
               {
                   CargarCombos(null);
                   
               }
               _parametro.IdParametro = -1;
               _parametro.NombreParametro = "RegistroMinero";
               _parametroDalc.obtenerParametros(ref _parametro);

               DataRow[] dtrGrupoRegistro = dtautoridad.Select("IDUserGroup = " + _parametro.Parametro.Trim());
               if (dtrGrupoRegistro.Length > 0)
               {
                   CargarCombos(idAA);
                   this.GridRegistroMin.Columns[9].Visible = true;
               }

            }
        }
    }

    private void CargarCombos(int? idAA)
    {
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();

        TipoResgistroIndetity TipoRegistro = new TipoResgistroIndetity();
        cboTipoRegistro.DataValueField = "TipoRegistroID";
        cboTipoRegistro.DataTextField = "NombreTipoRegistro";
        cboTipoRegistro.DataSource = TipoRegistro.ListaTipoRegistro();
        cboTipoRegistro.DataBind();
        cboTipoRegistro.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        Listas listaAutoridades = new Listas();
        cboAutoridadAmbiental.DataValueField = "AUT_ID";
        cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
        cboAutoridadAmbiental.DataSource = listaAutoridades.ListarAutoridadAmbientalRegistroMinero(idAA);
        cboAutoridadAmbiental.DataBind();
        if (idAA == null)
        {
            cboAutoridadAmbiental.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }

        Listas litDepartamento = new Listas();
        cboDepartamento.DataValueField = "DEP_ID";
        cboDepartamento.DataTextField = "DEP_NOMBRE";
        cboDepartamento.DataSource = litDepartamento.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
        cboDepartamento.DataBind();
        cboDepartamento.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboDepartamento.SelectedValue == "-1")
        {
            cboMunicipio.Items.Clear();
            cboMunicipio.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            return;
        }

        Listas litMunicipio = new Listas();
        cboMunicipio.DataValueField = "MUN_ID";
        cboMunicipio.DataTextField = "MUN_NOMBRE";
        cboMunicipio.DataSource = litMunicipio.ListaMunicipios(null, int.Parse(cboDepartamento.SelectedValue), null);
        cboMunicipio.DataBind();
        cboMunicipio.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnBuscarRegMinero_Click(object sender, EventArgs e)
    {
        GridRegistroMin.PageIndex = 0;
        CargarGrid();
    }

    private void CargarGrid()
    {
        RegistroMineroIdentity RegistroMinero = new RegistroMineroIdentity();

        int? tipoRegistro = null;
        int? autoridadAmbiental = null;
        int? departamento = null;
        int? municipio = null;

        if (cboTipoRegistro.SelectedValue != "-1")
        {
            tipoRegistro = int.Parse(cboTipoRegistro.SelectedValue);
        }

        if (cboAutoridadAmbiental.SelectedValue != "-1")
        {
            autoridadAmbiental = int.Parse(cboAutoridadAmbiental.SelectedValue);
        }

        if (cboDepartamento.SelectedValue != "-1")
        {
            departamento = int.Parse(cboDepartamento.SelectedValue);
        }

        if (cboMunicipio.SelectedValue != "-1")
        {
            municipio = int.Parse(cboMunicipio.SelectedValue);
        }

        NumRegistros.Text = "";

        DataTable dtResultado = new DataTable();
        dtResultado = RegistroMinero.ConsultaRegistroMineria(tipoRegistro, autoridadAmbiental, txtNombreOperador.Text.Trim(),
            txtIdentifAutoridad.Text.Trim(), txtCodRegMineria.Text.Trim(), txtNombreProyecto.Text.Trim(), txtNombreMina.Text.Trim(),
            departamento, municipio);
        GridRegistroMin.DataSource = dtResultado;
        GridRegistroMin.DataBind();
        if (dtResultado.Rows.Count > 0)
            btnMapas.Visible = true;
        else
            btnMapas.Visible = false;
        NumRegistros.Text = "Resultado de Búsqueda: "+dtResultado.Rows.Count.ToString();

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
            if (mensaje.IndexOf("Token invalido") > 0)
                return false;
        }
        return true;
    }
    protected void GridRegistroMin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridRegistroMin.PageIndex = e.NewPageIndex;
        CargarGrid();  
    }
    
    protected void GridRegistroMin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lcoordenada");
            System.Web.UI.WebControls.Image imagen = (System.Web.UI.WebControls.Image)e.Row.FindControl("ImgCoordenada");

            if (lbl.Text == "N")
            {
                e.Row.BackColor = Color.FromName("#FBFD9F");
                imagen.ImageUrl = "~/App_Themes/Img/alerta2.png";
                imagen.Visible = true;
                lbl.Text = "";
                imagen.ToolTip = "Registro inconsistente, no existen coordenadas asociadas.!";
            }
            else
            {
                imagen.Visible = false;
                lbl.Text = "SI";
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chkTodos = (CheckBox)e.Row.FindControl("chkTodos");
            chkTodos.Attributes.Add("onclick", string.Format("SelectAll('{0}',this);", GridRegistroMin.ClientID));
        }
    }
    protected void btnMapas_Click(object sender, ImageClickEventArgs e)
    {
        string registrosId = "";
        foreach (GridViewRow row in GridRegistroMin.Rows)
        {
            Label lblRegistroMineroID = (Label)row.FindControl("lblRegistroMineroID");
            CheckBox chkGraficar = (CheckBox)row.FindControl("chkGraficar");
            if (chkGraficar.Checked)
            {
                registrosId += string.Format(",{0}", lblRegistroMineroID.Text);
            }
        }
        if (registrosId != string.Empty)
            ClientScript.RegisterStartupScript(this.GetType(), "Ubicaciones", string.Format("popup('UbicacionRegistrosMineros.aspx?regmis={0}');", registrosId.Remove(0, 1)),true);
    }

    protected void imb_ver_todos_Click(object sender, EventArgs e)
    {
        //this.GridRegistroMin.PageSize = this.oConfig.DataGridPageSize * this.dgv_tabla.PageCount;
        this.GridRegistroMin.AllowPaging = false;
      
        CargarGrid(); 
    }
}