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
using SILPA.AccesoDatos;
using SILPA.LogicaNegocio.RUIA;

public partial class RUIA_ConsultarSancion : System.Web.UI.Page
{
    private void Page_PreInit(object sender,EventArgs e) 
    {
        if (Page.Request["Ubic"] == null)
            if (DatosSesion.Usuario != "")
                this.Page.MasterPageFile = "~/plantillas/SILPA.master";

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Mensaje.LimpiarMensaje(this);
       
        
       if (!IsPostBack)
        {
        
            //Si se va a actualizar la informacion
            SILPA.LogicaNegocio.Usuario.Usuario usuario = new SILPA.LogicaNegocio.Usuario.Usuario();         
            DataTable autoridad = usuario.ConsultarUsuarioSistemaCompania(DatosSesion.Usuario);
            CargarAutoridades();
            if (autoridad.Rows.Count > 0)
            {
                SILPA.AccesoDatos.Generico.ParametroDalc _parametroDalc = new SILPA.AccesoDatos.Generico.ParametroDalc();
                SILPA.AccesoDatos.Generico.ParametroEntity _parametro;
                _parametro = new SILPA.AccesoDatos.Generico.ParametroEntity();
                _parametro.IdParametro = -1;
                _parametro.NombreParametro = "funcionario_mavdt";
                _parametroDalc.obtenerParametros(ref _parametro);
                foreach (DataRow row in autoridad.Rows)
                {
                    if (row["IDUserGroup"].ToString().Trim() == _parametro.Parametro.Trim())
                    {
                        this.lblConsulta.Text = "1"; //Indica que el usuario es funcionario Maestro AA-RUIA
                        break;
                        //this.cboAutoridadAmbiental.SelectedValue = row["IdAutoridad"].ToString();
                        //this.cboAutoridadAmbiental.Enabled = false;
                    }
                }
                _parametro = new SILPA.AccesoDatos.Generico.ParametroEntity();
                _parametro.IdParametro = -1;
                _parametro.NombreParametro = "Funcionario_AA_RUIA";
                _parametroDalc.obtenerParametros(ref _parametro);
                foreach (DataRow row in autoridad.Rows)
                {
                    if (row["IDUserGroup"].ToString().Trim() == _parametro.Parametro.Trim())
                    {
                        this.lblConsulta.Text = "2";//Indica que el usuario es funcionarion AA-RUIA
                        this.cboAutoridadAmbiental.SelectedValue = row["IdAutoridad"].ToString();
                        this.cboAutoridadAmbiental.Enabled = false;
                        break;
                    }
                }

            }             
            
            CargarTiposFalta();
            CargarTiposSancion();
            this.cargarDepartamento();

            //if (ValidacionToken() == false)
            //    Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            CargarLinkRUIA();
        }
       
        dvwSancion.Visible = false;

        this.btnEliminar.Attributes.Add("OnClick", "javascript:return confirm('Esta seguro de eliminar la publicación?')");
    }

    private void CargarLinkRUIA()
    {
        string textoLink = "", urlLink="";
        textoLink = ConfigurationManager.AppSettings["textoLinkRUIA"].ToString();
        urlLink = ConfigurationManager.AppSettings["urlLinkRUIA"].ToString();
        this.HlnkRUIA.Text = textoLink;
        this.HlnkRUIA.NavigateUrl = urlLink;
    }

    #region Funciones Programador ...

    private void CargarAutoridades()
    {
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        DataSet _temp = _listaAutoridades.ListarAutoridades(null);
        foreach (DataRow _fila in _temp.Tables[0].Rows)
        {
            ListItem _item = new ListItem(_fila["AUT_NOMBRE"].ToString(), _fila["AUT_ID"].ToString());
            cboAutoridadAmbiental.Items.Add(_item);
        }        
    }

    private void CargarTiposFalta()
    {
        Listas _tipos = new Listas();
        DataSet _temp= _tipos.ListaTipoFalta();
        foreach (DataRow _fila in _temp.Tables[0].Rows)
        {
            ListItem _item = new ListItem(_fila["NOMBRE_TIPO_FALTA"].ToString(), _fila["ID_TIPO_FALTA"].ToString());
            cboTipoFalta.Items.Add(_item);
        }        
    }

    private void CargarTiposSancion()
    {
        Listas _tipos = new Listas();
        DataSet _temp = _tipos.ListaTipoSancion();
        foreach (DataRow _fila in _temp.Tables[0].Rows)
        {
            ListItem _item = new ListItem(_fila["NOMBRE_TIPO_SANCION"].ToString(), _fila["ID_TIPO_SANCION"].ToString());
            cboSancionAplicada.Items.Add(_item);
        }
    }

    private void CargarSanciones()
    {        
        //Se consulta fecha de acuerdo al tipo de usuario
        Sancion _sancion = new Sancion();
		string strIncluyeHistorico = "";
        if (this.lblConsulta.Text == "0")
        {
            strIncluyeHistorico = "0";
		}		
        DataSet _temp = _sancion.ListaSancion(null, int.Parse(cboAutoridadAmbiental.SelectedValue),
            int.Parse(cboTipoFalta.SelectedValue), int.Parse(this.cboSancionAplicada.SelectedValue), txtNumeroExpediente.Text, txtNumeroActo.Text,
            txtNombreResponsable.Text, txtFechaDesde.Text, txtFechaHasta.Text,
            int.Parse(this.cboDepartamento.SelectedValue), int.Parse(this.cboMunicipio.SelectedValue), int.Parse(this.cboCorregimiento.SelectedValue), int.Parse(this.cboVereda.SelectedValue),
            strIncluyeHistorico, txtNumeroDocumento.Text, this.cboEstadoSancion.SelectedValue);
        if (_temp.Tables[0].Rows.Count > 0)
        {
            this.lblContador.Visible = true;
            this.lblContador.Text = string.Format("Se encontraron {0} registros.", _temp.Tables[0].Rows.Count.ToString());
            dvwSancion.Visible = false;
            grdSanciones.DataSource = _temp;
            grdSanciones.DataBind();

            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("RIA - CONSULTAR SANCIÓN", 2, "Se consulto Sanción");

        }
        else
        {
            this.lblContador.Visible = true;
            this.lblContador.Text = "No se encontraron Registros.";
            grdSanciones.DataSource = _temp;
            grdSanciones.DataBind();
            dvwSancion.Visible = false;
        }
    }

    /// <summary>
    /// Funcion que carga la sanción en vista detalle
    /// </summary>
    /// <param name="_codigoPublicacion">Identificador de la sanción</param>
    private void CargarVistaDetalle(Int64 _codigoSancion)
    {
        Sancion _listaSanciones = new Sancion();
        DataSet _temp = _listaSanciones.ListaSancionDetalle(_codigoSancion, null, null);

        dvwSancion.DataSource = _temp;
        dvwSancion.DataBind();
        this.mpeDetalles.Show();
        this.tbDetalles.Visible = true;
        dvwSancion.Visible = true;        
                      
    }

    #endregion

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
      
        dvwSancion.Visible = false;
        this.pnlEliminar.Visible = false;
        CargarSanciones();
    }
    protected void grdSanciones_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VerDetalle")
        {
            int _indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow _fila = grdSanciones.Rows[_indice];
            Int64 _codigoSancion = Convert.ToInt64(_fila.Cells[0].Text);   
            CargarVistaDetalle(_codigoSancion);
        }
    }

    // <summary>
    /// Método que carga el departamento de ocurrencia
    /// </summary>
    private void cargarMunicipio()
    {
        int idDepartamento = int.Parse(this.cboDepartamento.SelectedValue);
        SILPA.LogicaNegocio.Generico.Listas listas = new SILPA.LogicaNegocio.Generico.Listas();

        DataTable tabla = listas.ListaMunicipios(0, idDepartamento, 0).Tables[0];

        this.cboMunicipio.DataSource = tabla.DefaultView;
        this.cboMunicipio.DataTextField = "MUN_NOMBRE";
        this.cboMunicipio.DataValueField = "MUN_ID";
        this.cboMunicipio.DataBind();
        this.cboMunicipio.Items.Insert(0, new ListItem("Seleccione...", "0"));

        this.cargarCorregimiento();      
    }


    /// <summary>
    /// Método que carga el combo del departamento de ocurrencia
    /// </summary>
    private void cargarDepartamento()
    {
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();

        DataSet _temp1 = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
        this.cboDepartamento.DataSource = _temp1;
        this.cboDepartamento.DataValueField = "DEP_ID";
        this.cboDepartamento.DataTextField = "DEP_NOMBRE";
        this.cboDepartamento.DataBind();
        this.cboDepartamento.Items.Insert(0, new ListItem("Seleccione...", "0"));

        cargarMunicipio();
       
    }

    /// <summary>
    /// Método que carga el corregimiento de ocurrencia
    /// </summary>
    private void cargarCorregimiento()
    {
        SILPA.LogicaNegocio.Generico.Listas lista = new SILPA.LogicaNegocio.Generico.Listas();

        DataTable dt = lista.ListarCorregimientos(int.Parse(this.cboMunicipio.SelectedValue), 0).Tables[0];
        this.cboCorregimiento.DataSource = dt.DefaultView;
        this.cboCorregimiento.DataTextField = "COR_NOMBRE";
        this.cboCorregimiento.DataValueField = "COR_ID";
        this.cboCorregimiento.DataBind();
        //if (this.cboCorregimiento.Items.Count <= 0)
            this.cboCorregimiento.Items.Insert(0, new ListItem("Seleccione...", "0"));
        this.cargarVereda();

    }

    /// <summary>
    /// Método que permite cargar la vereda de ocurrencia
    /// </summary>
    private void cargarVereda()
    {
        SILPA.Comun.Configuracion configuracion = new SILPA.Comun.Configuracion();
        SILPA.LogicaNegocio.Generico.Listas lista = new SILPA.LogicaNegocio.Generico.Listas();

        DataTable dt = lista.ListarVeredas(int.Parse(this.cboMunicipio.SelectedValue), int.Parse(this.cboCorregimiento.SelectedValue), 0).Tables[0];
        this.cboVereda.DataSource = dt.DefaultView;
        this.cboVereda.DataTextField = "VER_NOMBRE";
        this.cboVereda.DataValueField = "VER_ID";
        this.cboVereda.DataBind();
        //if (this.cboVereda.Items.Count <= 0)
        this.cboVereda.Items.Insert(0, new ListItem("Seleccione...", "0"));
    }

    protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarMunicipio();
    }
    protected void cboMunicipio_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarCorregimiento();
    }
    protected void cboCorregimiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarVereda();
    }

    protected void grdReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //grdReporte.PageIndexChanged();
        grdSanciones.PageIndex = e.NewPageIndex;
        CargarSanciones();
        
        //CargarSector();
    }

    protected void grdSanciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {       
        Response.Redirect("~/RUIA/RegistrarSancion.aspx?id=" + grdSanciones.DataKeys[e.RowIndex].Value.ToString(), false);
    }
    protected void grdSanciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        this.txtMotivoEliminacion.Text = "";
        this.lblId.Text = grdSanciones.DataKeys[e.RowIndex].Value.ToString();        
        this.pnlEliminar.Visible = true;
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        Sancion _sancion = new Sancion();
        _sancion.EliminarSancion(long.Parse(this.lblId.Text), this.txtMotivoEliminacion.Text, DatosSesion.Usuario);
        this.CargarSanciones();
        this.pnlEliminar.Visible = false;
        this.lblId.Text = "0";
        this.txtMotivoEliminacion.Text = "";
        Mensaje.MostrarMensaje(this, "La publicación ha sido eliminada con éxito.");

    }
  
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.CargarSanciones();
        this.pnlEliminar.Visible = false;
        this.lblId.Text = "0";
        this.txtMotivoEliminacion.Text = "";
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        if (Page.Request["Ubic"] == null)
            if (DatosSesion.Usuario != "" )
                Response.Redirect("../../SILPA/TESTSILPA/security/default.aspx");
            else            
                Page.Response.Redirect("~/../ventanillasilpa");        
        else
            Page.Response.Redirect("~/../ventanillasilpa");        

    }

    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        this.mpeDetalles.DropShadow = false;
        this.tbDetalles.Visible = false;

    }
    
    protected void grdSanciones_DataBound(object sender, EventArgs e)
    {
        //Si el usuario es administrador se muestran las columnas de actualizar y eliminar
        //if (this.lblConsulta.Text == "0")
        //{
        //    grdSanciones.Columns[9].Visible = false;
        //    grdSanciones.Columns[10].Visible = false;
        //}

        switch (this.lblConsulta.Text)
        { 
            case "0": //usuario Anonimo
                grdSanciones.Columns[9].Visible = false;
                grdSanciones.Columns[10].Visible = false;
                break;
            case "1":// Mestro AA-RUIA
                //Puede hacer todo
                seccionMaestro.Visible = true;
                if (this.cboEstadoSancion.SelectedValue == "0")
                {
                    this.grdSanciones.Columns[10].Visible = false;
                    this.grdSanciones.Columns[9].Visible = false;
                }
                else
                {
                    this.grdSanciones.Columns[10].Visible = true;
                    this.grdSanciones.Columns[9].Visible = true;
                }
                break;
            case "2":// Funcionario AA-RUIA
                grdSanciones.Columns[9].Visible = true;
                grdSanciones.Columns[10].Visible = false;
                if (this.cboEstadoSancion.SelectedValue == "0")
                {
                    this.grdSanciones.Columns[10].Visible = false;
                    this.grdSanciones.Columns[9].Visible = false;
                }
                else
                {
                    grdSanciones.Columns[9].Visible = true;
                }
                seccionMaestro.Visible = true;
                break;
            default:// en caso de que no cumpla ninguna de las posibilidades
                grdSanciones.Columns[9].Visible = false;
                grdSanciones.Columns[10].Visible = false;
                break;
        }
    }
}
