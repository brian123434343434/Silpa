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
using System.Collections.Generic;
using SILPA.LogicaNegocio.RUIA;
using SILPA.AccesoDatos.RUIA;
using SILPA.Comun;
using System.IO;

public partial class RUIA_RegistrarSancion : System.Web.UI.Page
{
    /// <summary>
    /// mardila: Clase para almacenar las sanciones secundarias que va ingresando el usuario
    /// debe ser serializable para que se pueda escribir en el viewstate
    /// </summary>
    [Serializable]
    public class SancionSecundaria
    {
        private int _idTipoSancion;
        private string _nombreTipoSancion;
        private string _sancionAplicada;

        public string SancionAplicada
        {
            get { return _sancionAplicada; }
            set { _sancionAplicada = value; }
        }

        public string NombreTipoSancion 
        {
            get { return _nombreTipoSancion; }
            set { _nombreTipoSancion = value; }
        }
        
        public int IdTipoSancion
        {
            get {return _idTipoSancion;}
            set {_idTipoSancion = value;}
        }

        public string Descripcion
        {
            get { return _nombreTipoSancion + " - " + _sancionAplicada; }
        }
    }

    /// <summary>
    /// Propiedad que trae las sanciones secundarias del viewstate
    /// </summary>
    private IList<SancionSecundaria> SancionesSecundarias
    {
        get { return (IList<SancionSecundaria>)this.ViewState["SECUNDARIAS"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // mardila 05/04/2010 Se elimina el texto anterior si es que existe
        this.lblMensajeSecundaria.Text = "";

        Mensaje.LimpiarMensaje(this);
       

        if (!IsPostBack)
        {
            this.lblFormulario.Text = Path.GetFileName(Request.Url.AbsolutePath);

             if (this.Request.QueryString["id"] != null)      
                this.lblId.Text = this.Request.QueryString["id"].ToString();
            if(this.Request.QueryString["new"] != null)
                Mensaje.MostrarMensaje(this, "Información Insertada.");

            string idGrupo = string.Empty;
            //System.Text.StringBuilder mensaje = new System.Text.StringBuilder();
            //mensaje.Append("Inicio");
            // 12-jul-2010 - aegb            
            if (DatosSesion.Usuario != string.Empty)
            {
               
                //Descripcion: Si es un usuario maestro de RUIA, la autoridad con la que se debe registrar es la original de la sancion
                //             se crea variable de Session["Funcionario_mavdt"] para identificar el tema
                //Autor:mcarvajal
                //Fecha: 09 Noviembre 2011
                Session["Funcionario_mavdt"] = null;
                SILPA.LogicaNegocio.Usuario.Usuario usuario = new SILPA.LogicaNegocio.Usuario.Usuario();
               // DataTable autoridad = usuario.ConsultarUsuarioSistemaCompania("52982");
                DataTable autoridad = usuario.ConsultarUsuarioSistemaCompania(DatosSesion.Usuario);
                if (autoridad.Rows.Count > 0)
                {
                    ViewState["autoridad"] = autoridad.Rows[0]["IDAutoridad"].ToString();                    
                    idGrupo = autoridad.Rows[0]["IDUserGroup"].ToString();
                    //mensaje.Append("Autoridad: " + autoridad.Rows[0]["IDCompany"].ToString());
                    SILPA.AccesoDatos.Generico.ParametroDalc _parametroDalc = new SILPA.AccesoDatos.Generico.ParametroDalc();
                    SILPA.AccesoDatos.Generico.ParametroEntity _parametro = new SILPA.AccesoDatos.Generico.ParametroEntity();
                    _parametro.IdParametro = -1;
                    _parametro.NombreParametro = "funcionario_mavdt";
                    _parametroDalc.obtenerParametros(ref _parametro);
                    btnAgregar.Enabled = false;
                    foreach (DataRow row in autoridad.Rows)
                    {
                        if (row["IDUserGroup"].ToString() == _parametro.Parametro.Trim())
                        {
                            btnAgregar.Enabled = true;
                            Session["Funcionario_mavdt"] = "EsMaestro";
                        }
                    }

                    _parametro.IdParametro = -1;
                    _parametro.NombreParametro = "Funcionario_AA_RUIA";
                    _parametroDalc.obtenerParametros(ref _parametro);
                    foreach (DataRow row in autoridad.Rows)
                    {
                        if (row["IDUserGroup"].ToString().Trim() == _parametro.Parametro.Trim())
                        {
                            btnAgregar.Enabled = true;
                            break;
                        }
                    }
                }

            }
           //Si el usuario no pertenece a ninguna autoridad
            if (ViewState["autoridad"] == null)
                btnAgregar.Enabled = false;
            

            //mensaje.Append("Fin");

            //Mensaje.MostrarMensaje(this, mensaje.ToString());
         
          
            tbcContenedor.ActiveTabIndex = 0;
            tbcContenedor.Tabs[2].Enabled = false;
            // mardila 05/04/2010: se comenta, las validaciones no aplican
            // cboOpcionesPrincipal.Enabled = false;
            // chkAccesoria.Enabled = false;
            // pnlOpcionesAccesoria.Enabled = false;
            HabilitarValidacionNatural(true);
            HabilitarValidacionJuridica(false);
            CargarCombosTipoDocumento();
            CargarCombosOrigen();
            CargarComboOpcionesSancion();
            CargarTiposFalta();
            btnAgregar.Attributes.Add("OnClick", "javascript:return confirm('Recuerde que una vez le de Aceptar, ésta información será publicada en el RUIA')");

            // mardila 05/04/2010: se crea la lista de las sanciones secundarias y se almacena en el viewstate
            IList<SancionSecundaria> lista = new List<SancionSecundaria>();
            // agregamos la lista al viewstate
            this.ViewState["SECUNDARIAS"] = lista;
            // mardila 05/04/2010: cargamos la lista de departamentos de ocurrencia
            this.cargarDepartamentoOcurrencia();

            this.ConsultarControles();
            //Si se va a actualizar la informacion
            if (long.Parse(this.lblId.Text) > 0)                 
                this.ConsultarRegistro();
                  

        }
        //if ((Session["Autenticado"]==null) || (Session["Autenticado"].ToString() != "Si"))
        //    Response.Redirect("LoginRUIA.aspx");
    }

    /// <summary>
    /// Método que carga el combo del departamento de ocurrencia
    /// </summary>
    private void ConsultarRegistro()
    {
        Sancion _sancion = new Sancion();
        SancionIdentity _objSancion = new SancionIdentity();
        _objSancion = _sancion.ListaSancionDetalles(long.Parse(this.lblId.Text), null, null);

        //Descripcion: Si es un usuario maestro de RUIA, la autoridad con la que se debe registrar es la original de la sancion
        //             se crea variable de Session["Funcionario_mavdt"] para identificar el tema
        //Autor:mcarvajal
        //Fecha: 09 Noviembre 2011
        if ((Session["Funcionario_mavdt"] != null) && (long.Parse(this.lblId.Text) > 0))
        {
            ViewState["autoridad"] = _objSancion.IdAutoridad;
        }
        this.cboTipoPersona.SelectedValue = _objSancion.TipoPersona.ToString();
        this.cboTipoPersona.Enabled = false;
        this.txtDescripcionDesfijacion.Text = _objSancion.DescripcionDesfijacion;
        this.txtDescripcionNorma.Text = _objSancion.DescripcionNorma;        
        this.txtFechaEjecucion.Text = _objSancion.FechaEjecucion;
        this.txtFechaEjecutoria.Text = _objSancion.FechaEjecutoria;
        this.txtFechaExpedicion.Text = _objSancion.FechaExpedicion;
        this.txtNumeroActo.Text = _objSancion.NumeroActo;     
        this.txtNumeroExpediente.Text = _objSancion.NumeroExpediente;
        this.txtSancionAplicadaPpal.Text = _objSancion.SancionPrincipal;
        this.cboDepartamentoOcurrencia.SelectedValue = _objSancion.DepId.ToString();
        SILPA.LogicaNegocio.Generico.Listas _listas = new SILPA.LogicaNegocio.Generico.Listas();
        cboMunicipioOcurrencia.DataSource = _listas.ListaMunicipios(null, int.Parse(cboDepartamentoOcurrencia.SelectedValue), null); ;
        cboMunicipioOcurrencia.DataValueField = "MUN_ID";
        cboMunicipioOcurrencia.DataTextField = "MUN_NOMBRE";
        cboMunicipioOcurrencia.DataBind();
        this.cboMunicipioOcurrencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        this.cboMunicipioOcurrencia.SelectedValue = _objSancion.MunId.ToString();
        this.cboCorregimientoOcurrencia.DataSource = _listas.ListarCorregimientos(int.Parse(this.cboMunicipioOcurrencia.SelectedValue), 0).Tables[0]; ;
        this.cboCorregimientoOcurrencia.DataTextField = "COR_NOMBRE";
        this.cboCorregimientoOcurrencia.DataValueField = "COR_ID";
        this.cboCorregimientoOcurrencia.DataBind();
        this.cboCorregimientoOcurrencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        this.cboCorregimientoOcurrencia.SelectedValue = _objSancion.CorId.ToString();
        this.cboVeredaOcurrencia.DataSource = _listas.ListarVeredas(int.Parse(this.cboMunicipioOcurrencia.SelectedValue), int.Parse(this.cboCorregimientoOcurrencia.SelectedValue), 0).Tables[0]; ;
        this.cboVeredaOcurrencia.DataTextField = "VER_NOMBRE";
        this.cboVeredaOcurrencia.DataValueField = "VER_ID";
        this.cboVeredaOcurrencia.DataBind();
        this.cboVeredaOcurrencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        this.cboVeredaOcurrencia.SelectedValue = _objSancion.VerId.ToString();      
        this.cboTipoFalta.SelectedValue = _objSancion.IdFalta.ToString();
        this.cboOpcionesPrincipal.SelectedValue = _objSancion.IdSancionPrincipal.ToString();
        this.txtMotivoModificacion.Text = _objSancion.MotivoModificacion;
        this.txtMotivoModificacion.Visible = true;
        this.lblMotivoModificacion.Visible = true;
        this.cboTramiteModificacion.SelectedValue = _objSancion.TramiteModificacion.ToString();
        this.cboTramiteModificacion.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        this.cboTramiteModificacion.Visible = true;
        this.lblTramiteModificacion.Visible = true;
        this.txtObservaciones.Text = _objSancion.Observaciones;

        if (this.cboTipoPersona.SelectedItem.Text.Contains("Natural"))
        {
            int? deptoIDNatural = null; 
            this.txtPrimerApellido.Text = _objSancion.PrimerApellido;
            this.txtPrimerNombre.Text = _objSancion.PrimerNombre;
            this.txtSegundoApellido.Text = _objSancion.SegundoApellido;
            this.txtSegundoNombre.Text = _objSancion.SegundoNombre;
            this.txtNumeroDocumento.Text = _objSancion.NumeroIdentificacion;
            if (_objSancion.IdDpto.ToString() != "-1")
            {
                this.cboDepartamentoNatural.SelectedValue = _objSancion.IdDpto.ToString();
            }
            if (cboDepartamentoNatural.SelectedValue != string.Empty)
            {
                deptoIDNatural = Convert.ToInt32(cboDepartamentoNatural.SelectedValue);
                cboMunicipioNatural.DataSource = _listas.ListaMunicipios(null, deptoIDNatural, null); ;
                cboMunicipioNatural.DataValueField = "MUN_ID";
                cboMunicipioNatural.DataTextField = "MUN_NOMBRE";
                cboMunicipioNatural.DataBind();
            }
            if(_objSancion.IdMunicipio.ToString() != "-1")
                this.cboMunicipioNatural.SelectedValue = _objSancion.IdMunicipio.ToString();
            this.cboTipoDocumento.SelectedValue = _objSancion.IdTipoIdentificacion.ToString();
            //Se inhabilitan
            this.txtPrimerApellido.Enabled = false;
            this.txtPrimerNombre.Enabled = false;
            this.txtSegundoApellido.Enabled = false;
            this.txtSegundoNombre.Enabled = false;
            this.txtNumeroDocumento.Enabled = false;
            this.cboDepartamentoNatural.Enabled = false;
            this.cboMunicipioNatural.Enabled = false;
            this.cboTipoDocumento.Enabled = false;

            this.rfvNit.Enabled = false;
            this.rfvNumeroDocumentoRepresentante.Enabled = false;
            this.rfvPrimerApellidoRepresentante.Enabled = false;
            this.rfvPrimerNombreRepresentante.Enabled = false;
            this.rfvRazonSocial.Enabled = false;
                   
            tbcContenedor.Tabs[1].Enabled = true;
            tbcContenedor.Tabs[2].Enabled = false;
            tbcContenedor.Tabs[1].Visible = true;
            tbcContenedor.Tabs[2].Visible = false;
           }
        else
        {
            int? deptoIDJuridica = null;
            this.txtNit.Text = _objSancion.Nit;
            this.txtNumeroDocumentoRepresentante.Text = _objSancion.NumeroIdentificacion;
            this.txtPrimerApellidoRepresentante.Text = _objSancion.PrimerApellido;
            this.txtPrimerNombreRepresentante.Text = _objSancion.PrimerNombre;
            this.txtRazonSocial.Text = _objSancion.RazonSocial;
            this.txtSegundoApellidoRepresentante.Text = _objSancion.SegundoApellido;
            this.txtSegundoNombreRepresentante.Text = _objSancion.SegundoNombre;
            if (_objSancion.IdDpto.ToString() != "-1")
            {
                this.cboDepartamentoJuridica.SelectedValue = _objSancion.IdDpto.ToString();
            }
            if (cboDepartamentoJuridica.SelectedValue != string.Empty)
            {
                deptoIDJuridica = Convert.ToInt32(cboDepartamentoJuridica.SelectedValue);
                cboMunicipioJuridica.DataSource = _listas.ListaMunicipios(null, deptoIDJuridica, null);
                cboMunicipioJuridica.DataValueField = "MUN_ID";
                cboMunicipioJuridica.DataTextField = "MUN_NOMBRE";
                cboMunicipioJuridica.DataBind();
            }
            if(_objSancion.IdMunicipio.ToString() != "-1")
                this.cboMunicipioJuridica.SelectedValue = _objSancion.IdMunicipio.ToString();
            this.cboTipoDocumentoJuridica.SelectedValue = _objSancion.IdTipoIdentificacion.ToString();
            //Se inhabilitan
            this.txtNit.Enabled = false;
            this.txtNumeroDocumentoRepresentante.Enabled = false;
            this.txtPrimerApellidoRepresentante.Enabled = false;
            this.txtPrimerNombreRepresentante.Enabled = false;
            this.txtRazonSocial.Enabled = false;
            this.txtSegundoApellidoRepresentante.Enabled = false;
            this.txtSegundoNombreRepresentante.Enabled = false;
            this.cboDepartamentoJuridica.Enabled = false;
            this.cboMunicipioJuridica.Enabled = false;
            this.cboTipoDocumentoJuridica.Enabled = false;

            this.rfvPrimerApellido.Enabled = false;
            this.rfvPrimerNombre.Enabled = false;
            this.rfvNumeroDocumento.Enabled = false;

            tbcContenedor.Tabs[1].Enabled = false;
            tbcContenedor.Tabs[2].Enabled = true;
            tbcContenedor.Tabs[1].Visible = false;
            tbcContenedor.Tabs[2].Visible = true;
        }

        //this.cboTipoSancionSecundaria.SelectedValue = _objSancion.DescripcionDesfijacion;
        //this.txtSancionAplicadaSec.Text = _objSancion.DescripcionDesfijacion;
        this.SancionesSecundarias.Clear();
        this.lstSecundaria.Items.Clear();
        Sancion _listaOpciones = new Sancion();
        DataSet _opciones = _listaOpciones.ListaOpcionesSancion(_objSancion.IdSancion);
        // tomamos la lista de sanción de la base de datos       
        foreach (DataRow _fila in _opciones.Tables[0].Rows)
        {
            if (_fila["RSA_ID_TIPO_SANCION"].ToString() == TipoSancion.Accesoria.ToString())
            {          
                // creamos y agregamos la sanción secundaria
                SancionSecundaria secundaria = new SancionSecundaria();
                secundaria.IdTipoSancion = int.Parse(_fila["OPS_ID_OPCION"].ToString());
                secundaria.NombreTipoSancion =  _fila["OPS_NOMBRE_OPCION"].ToString();
                secundaria.SancionAplicada = _fila["RSA_SANCION_APLICADA"].ToString();

                this.SancionesSecundarias.Add(secundaria);
            }
        }

        actualizarListaSecundarias();
    }

    /// <summary>
    /// Método que carga el combo del departamento de ocurrencia
    /// </summary>
    private void ConsultarControles()
    {
        SILPA.LogicaNegocio.Generico.Listas _listas = new SILPA.LogicaNegocio.Generico.Listas();
        //Configuracion formulario por tipo de persona
        //DataSet _temp = _listas.ListarFormularioPersona(-1, -1, this.lblFormulario.Text);
        //Session["FORMULARIO"] = _temp.Tables[0];

        //Tipo de persona
        DataSet _temp = _listas.ListarTipoPersona(-1, this.lblFormulario.Text);
        this.cboTipoPersona.DataSource = _temp;
        this.cboTipoPersona.DataValueField = "TPE_ID";
        this.cboTipoPersona.DataTextField = "TPE_NOMBRE";
        this.cboTipoPersona.DataBind();
        //this.cboTipoPersona.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    
    protected void cboDepartamentoNatural_SelectedIndexChanged(object sender, EventArgs e)
    {
        SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
        int _codigoDep = int.Parse(cboDepartamentoNatural.SelectedItem.Value);
        DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
        cboMunicipioNatural.DataSource = _temp;
        cboMunicipioNatural.DataValueField = "MUN_ID";
        cboMunicipioNatural.DataTextField = "MUN_NOMBRE";
        cboMunicipioNatural.DataBind();
    }

    /// <summary>
    /// Método que carga el departamento de ocurrencia
    /// </summary>
    private void cargarMunicipioOcurrencia()
    {        
        int idDepartamento = int.Parse(this.cboDepartamentoOcurrencia.SelectedValue);        
        SILPA.LogicaNegocio.Generico.Listas listas = new SILPA.LogicaNegocio.Generico.Listas();

        DataTable tabla = listas.ListaMunicipios(0, idDepartamento, 0).Tables[0];

        this.cboMunicipioOcurrencia.DataSource = tabla.DefaultView;
        this.cboMunicipioOcurrencia.DataTextField = "MUN_NOMBRE";
        this.cboMunicipioOcurrencia.DataValueField = "MUN_ID";
        this.cboMunicipioOcurrencia.DataBind();     
        this.cboMunicipioOcurrencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        this.cargarCorregimientoOcurrencia();
    }


    /// <summary>
    /// Método que carga el combo del departamento de ocurrencia
    /// </summary>
    private void cargarDepartamentoOcurrencia()
    {
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();        

        DataSet _temp1 = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
        this.cboDepartamentoOcurrencia.DataSource = _temp1;
        this.cboDepartamentoOcurrencia.DataValueField = "DEP_ID";
        this.cboDepartamentoOcurrencia.DataTextField = "DEP_NOMBRE";
        this.cboDepartamentoOcurrencia.DataBind();

        cargarMunicipioOcurrencia();
     
    }

    /// <summary>
    /// Método que carga el corregimiento de ocurrencia
    /// </summary>
    private void cargarCorregimientoOcurrencia()
    {
        SILPA.LogicaNegocio.Generico.Listas lista = new SILPA.LogicaNegocio.Generico.Listas();

        DataTable dt = lista.ListarCorregimientos(int.Parse(this.cboMunicipioOcurrencia.SelectedValue), 0).Tables[0];
        this.cboCorregimientoOcurrencia.DataSource = dt.DefaultView;
        this.cboCorregimientoOcurrencia.DataTextField = "COR_NOMBRE";
        this.cboCorregimientoOcurrencia.DataValueField = "COR_ID";
        this.cboCorregimientoOcurrencia.DataBind();
        this.cboCorregimientoOcurrencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        this.cargarVeredaOcurrencia();
     
    }

    /// <summary>
    /// Método que permite cargar la vereda de ocurrencia
    /// </summary>
    private void cargarVeredaOcurrencia()
    {
        SILPA.Comun.Configuracion configuracion = new SILPA.Comun.Configuracion();
        SILPA.LogicaNegocio.Generico.Listas lista = new SILPA.LogicaNegocio.Generico.Listas();

        DataTable dt = lista.ListarVeredas(int.Parse(this.cboMunicipioOcurrencia.SelectedValue), int.Parse(this.cboCorregimientoOcurrencia.SelectedValue), 0).Tables[0];
        this.cboVeredaOcurrencia.DataSource = dt.DefaultView;
        this.cboVeredaOcurrencia.DataTextField = "VER_NOMBRE";
        this.cboVeredaOcurrencia.DataValueField = "VER_ID";
        this.cboVeredaOcurrencia.DataBind();
        this.cboVeredaOcurrencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    #region Funciones Programador

    private void CargarCombosTipoDocumento()
    {
        SILPA.LogicaNegocio.Generico.Listas _tiposDoc = new SILPA.LogicaNegocio.Generico.Listas();
        DataSet _temp = _tiposDoc.ListaTipoIdentificacion();
        cboTipoDocumento.DataSource = _temp;
        cboTipoDocumento.DataValueField = "TID_ID";
        cboTipoDocumento.DataTextField = "TID_NOMBRE";
        cboTipoDocumento.DataBind();

        cboTipoDocumentoJuridica.DataSource = _temp;
        cboTipoDocumentoJuridica.DataValueField = "TID_ID";
        cboTipoDocumentoJuridica.DataTextField = "TID_NOMBRE";
        cboTipoDocumentoJuridica.DataBind();
    }


    /// <summary>
    /// Carga los combos para seleccionar el lugar de expedicion del Documento en Natural y Juridica
    /// </summary>
    private void CargarCombosOrigen()
    {
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();

        DataSet _temp1 = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
        cboDepartamentoNatural.DataSource = _temp1;
        cboDepartamentoNatural.DataValueField = "DEP_ID";
        cboDepartamentoNatural.DataTextField = "DEP_NOMBRE";
        cboDepartamentoNatural.DataBind();
        cboDepartamentoNatural.Items.Insert(0, new ListItem("Seleccione",""));

        cboDepartamentoJuridica.DataSource = _temp1;
        cboDepartamentoJuridica.DataValueField = "DEP_ID";
        cboDepartamentoJuridica.DataTextField = "DEP_NOMBRE";
        cboDepartamentoJuridica.DataBind();
        cboDepartamentoJuridica.Items.Insert(0, new ListItem("Seleccione", ""));
        cboMunicipioNatural.Items.Insert(0, new ListItem("Seleccione", ""));
        cboMunicipioJuridica.Items.Insert(0, new ListItem("Seleccione", ""));
    }

    private void CargarComboOpcionesSancion()
    {
        Listas _listas = new Listas();
        DataSet _temp = _listas.ListaOpcionSancion();
        DataTable tabla = _temp.Tables[0].Copy();
        cboOpcionesPrincipal.DataSource = _temp;
        cboOpcionesPrincipal.DataValueField = "ID_OPCION_SANCION";
        cboOpcionesPrincipal.DataTextField = "NOMBRE_OPCION_SANCION";
        cboOpcionesPrincipal.DataBind();        

        // mardila 05/04/2010: cargamos el tipo de sanción secundaria        
        this.cboTipoSancionSecundaria.DataSource = tabla.DefaultView;
        this.cboTipoSancionSecundaria.DataTextField = "NOMBRE_OPCION_SANCION";
        this.cboTipoSancionSecundaria.DataValueField = "ID_OPCION_SANCION";
        this.cboTipoSancionSecundaria.DataBind();

    }

    private void HabilitarValidacionNatural(bool habilitar)
    {
        rfvPrimerNombre.Enabled = habilitar;
        rfvPrimerApellido.Enabled = habilitar;
        rfvNumeroDocumento.Enabled = habilitar;        
    }

    private void HabilitarValidacionJuridica(bool habilitar)
    {
        rfvRazonSocial.Enabled = habilitar;
        rfvNit.Enabled = habilitar;
        revNit.Enabled = habilitar;
        rfvPrimerNombreRepresentante.Enabled = habilitar;
        rfvPrimerApellidoRepresentante.Enabled = habilitar;
        rfvNumeroDocumentoRepresentante.Enabled = habilitar;
    }

    private void CargarTiposFalta()
    {
        Listas _tipos = new Listas();
        DataSet _temp = _tipos.ListaTipoFalta();
        cboTipoFalta.DataSource = _temp;
        cboTipoFalta.DataValueField = "ID_TIPO_FALTA";
        cboTipoFalta.DataTextField = "NOMBRE_TIPO_FALTA";
        cboTipoFalta.DataBind();

    }

    #endregion

    protected void cboDepartamentoJuridica_SelectedIndexChanged(object sender, EventArgs e)
    {
        SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
        int _codigoDep = int.Parse(cboDepartamentoJuridica.SelectedItem.Value);
        DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
        cboMunicipioJuridica.DataSource = _temp;
        cboMunicipioJuridica.DataValueField = "MUN_ID";
        cboMunicipioJuridica.DataTextField = "MUN_NOMBRE";
        cboMunicipioJuridica.DataBind();
    }
    protected void cvaPrincipal_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // mardila 05/04/2010: se comenta porque la validación no aplica
        /*
        if (!chkPrincipal.Checked)        
            args.IsValid = false;
        else
            args.IsValid = true;
         */

    }
    protected void cvaAccesoria_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // mardila 05/04/2010: se comenta porque la validación no aplica
        /*
        if (chkAccesoria.Checked)
        {
            if ((!chkMulta.Checked) &&
                (!chkDemolicion.Checked) &&
                (!chkTrabajoComunitario.Checked) &&
                (!chkSuspension.Checked))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
         */
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                Sancion _sancion = new Sancion();
                int? _idAutoridad;
                if (ViewState["autoridad"] != null)
                    _idAutoridad = int.Parse(ViewState["autoridad"].ToString());
                else
                    _idAutoridad = null;

                // mardila 05/04/2010 validamos que la sanción principal no sea igual a la secundaria
                int idSancionSecundaria = int.Parse(this.cboOpcionesPrincipal.SelectedValue);

                bool encontrado = false;

                foreach (SancionSecundaria secundaria in SancionesSecundarias)
                {
                    if (secundaria.IdTipoSancion == idSancionSecundaria)
                    {
                        encontrado = true;
                        break;
                    }
                }

                if (encontrado)
                {
                    Mensaje.MostrarMensaje(this, "El tipo de sanción principal no puede ser secundaria");
                    return;
                }

                if (this.txtMotivoModificacion.Visible && this.txtMotivoModificacion.Text.Trim() == string.Empty)
                {
                     Mensaje.MostrarMensaje(this, "El motivo de la modificación es requerido");
                    return;
                }


                // mardila 05/04/2010 tomamos la ubicaciòn de la ocurrencia
                //int depId = int.Parse(this.cboDepartamentoOcurrencia.SelectedValue);
                int munId = int.Parse(this.cboMunicipioOcurrencia.SelectedValue);
                int corId = int.Parse(this.cboCorregimientoOcurrencia.SelectedValue);
                int verId = int.Parse(this.cboVeredaOcurrencia.SelectedValue);
                long idSancion = long.Parse(this.lblId.Text);
                if (idSancion <= 0)
                {
                    if (this.cboTipoPersona.SelectedItem.Text.Contains("Natural"))
                    {
                        int? municipioIdNatural = null;
                        if (cboMunicipioNatural.SelectedValue != string.Empty)
                            municipioIdNatural = int.Parse(cboMunicipioNatural.SelectedValue);
                        // mardila 05/04/2010 insertamos el lugar de ocurrencia
                        this.lblId.Text = _sancion.InsertarSancion(int.Parse(this.cboTipoPersona.SelectedValue), int.Parse(cboTipoFalta.SelectedValue),
                            txtDescripcionNorma.Text, txtNumeroExpediente.Text,
                            txtNumeroActo.Text, txtFechaExpedicion.Text, txtFechaEjecutoria.Text,
                            txtFechaEjecucion.Text, null, null, txtPrimerNombre.Text, txtSegundoNombre.Text,
                            txtPrimerApellido.Text, txtSegundoApellido.Text, int.Parse(cboTipoDocumento.SelectedValue),
                            txtNumeroDocumento.Text, municipioIdNatural, _idAutoridad,
                            txtDescripcionDesfijacion.Text, munId, corId, verId, int.Parse(this.cboOpcionesPrincipal.SelectedValue),
                            int.Parse(this.cboTramiteModificacion.SelectedValue),this.txtObservaciones.Text).ToString();

                        // mardila 05/04/2010, insertamos la sanción principal aplicada
                        _sancion.InsertarRelacionOpcion(long.Parse(this.lblId.Text), TipoSancion.Principal, int.Parse(cboOpcionesPrincipal.SelectedValue), this.txtSancionAplicadaPpal.Text);

                        // insertamos las sanciones secundarias
                        foreach (SancionSecundaria secundaria in this.SancionesSecundarias)
                        {
                            _sancion.InsertarRelacionOpcion(long.Parse(this.lblId.Text), TipoSancion.Accesoria, secundaria.IdTipoSancion, secundaria.SancionAplicada);
                        }

                        GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
                        CrearLogAuditoria.Insertar("RIA - REGISTRAR SANCIÓN", 1, "Se almaceno Registro Sanción");


                    }
                    else
                    {
                        int? municipioIdJuridica = null;
                        if(cboMunicipioJuridica.SelectedValue != string.Empty)
                            municipioIdJuridica = int.Parse(cboMunicipioJuridica.SelectedValue);
                        // mardila 05/04/2010 insertamos el lugar de ocurrencia
                        this.lblId.Text = _sancion.InsertarSancion(int.Parse(this.cboTipoPersona.SelectedValue), int.Parse(cboTipoFalta.SelectedValue),
                             txtDescripcionNorma.Text, txtNumeroExpediente.Text,
                             txtNumeroActo.Text, txtFechaExpedicion.Text, txtFechaEjecutoria.Text,
                             txtFechaEjecucion.Text, txtRazonSocial.Text, txtNit.Text, txtPrimerNombreRepresentante.Text,
                             txtSegundoNombreRepresentante.Text, txtPrimerApellidoRepresentante.Text,
                             txtSegundoApellidoRepresentante.Text, int.Parse(cboTipoDocumentoJuridica.SelectedValue),
                             txtNumeroDocumentoRepresentante.Text, municipioIdJuridica,
                             _idAutoridad, txtDescripcionDesfijacion.Text, munId, corId, verId, int.Parse(this.cboOpcionesPrincipal.SelectedValue),
                             int.Parse(this.cboTramiteModificacion.SelectedValue),this.txtObservaciones.Text).ToString();

                        // mardila 05/04/2010 se agrega la sancion aplicada
                        _sancion.InsertarRelacionOpcion(long.Parse(this.lblId.Text), TipoSancion.Principal, int.Parse(cboOpcionesPrincipal.SelectedValue), this.txtSancionAplicadaPpal.Text);

                        // insertamos las sanciones secundarias
                        foreach (SancionSecundaria secundaria in this.SancionesSecundarias)
                        {
                            _sancion.InsertarRelacionOpcion(long.Parse(this.lblId.Text), TipoSancion.Accesoria, secundaria.IdTipoSancion, secundaria.SancionAplicada);
                        }

                        GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
                        CrearLogAuditoria.Insertar("RIA - REGISTRAR SANCIÓN", 1, "Se almaceno Registro Sanción");
                       
                    }
                    Page.Response.Redirect(string.Format("RegistrarSancion.aspx?Id={0}&new={1}",this.lblId.Text,"1"));
                }
                else
                {

                    //aegb 12/05/2010 Se eliminan las relaciones de las opciones
                    _sancion.EliminarRelacionOpcion(idSancion);
                    //aegb 12/05/2010 se actualiza la informacion de la sancion
                    _sancion.ActualizarSancion(idSancion, int.Parse(cboTipoFalta.SelectedValue),
                        txtDescripcionNorma.Text, txtNumeroExpediente.Text,
                        txtNumeroActo.Text, txtFechaExpedicion.Text, txtFechaEjecutoria.Text,
                        txtFechaEjecucion.Text, _idAutoridad,
                        txtDescripcionDesfijacion.Text, munId, corId, verId,
                        this.txtMotivoModificacion.Text, int.Parse(this.cboOpcionesPrincipal.SelectedValue),
                        int.Parse(this.cboTramiteModificacion.SelectedValue), this.txtObservaciones.Text, DatosSesion.Usuario);

                    // mardila 05/04/2010, se inserta la sanción principal aplicada
                    _sancion.InsertarRelacionOpcion(long.Parse(this.lblId.Text), TipoSancion.Principal, int.Parse(cboOpcionesPrincipal.SelectedValue), this.txtSancionAplicadaPpal.Text);

                    // se insertan las sanciones secundarias
                    foreach (SancionSecundaria secundaria in this.SancionesSecundarias)
                    {
                        _sancion.InsertarRelacionOpcion(long.Parse(this.lblId.Text), TipoSancion.Accesoria, secundaria.IdTipoSancion, secundaria.SancionAplicada);
                    }
                    this.ConsultarRegistro();
                }

                Mensaje.MostrarMensaje(this, "Se ha guardado la información.");
            }           
        }
        catch (Exception ex)
        {
            Mensaje.MostrarMensaje(this.Page, ex.ToString());
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        //string _respuestaEnvio = "<script language='JavaScript'>" +
        //      "window.close()</script>";
        //Page.RegisterStartupScript("PopupScript", _respuestaEnvio);
        Response.Redirect("../../SILPA/TESTSILPA/security/default.aspx");
    }

    protected void btnAgregarSec_Click(object sender, EventArgs e)
    {
        // si no ha seleccionado el tipo de sanción secundaria
        if (this.cboTipoSancionSecundaria.SelectedIndex == -1)
        {
             Mensaje.MostrarMensaje(this, "Debe seleccionar el tipo de sanción secundaria");
            return;
        }

        // tomamos el identificador de la opción secundaria
        int idOpcionSecundaria = int.Parse(this.cboTipoSancionSecundaria.SelectedValue);

        // confirmamos si el tipo de sanción se encuentra como principal
        if((this.cboOpcionesPrincipal.SelectedIndex != -1) && (idOpcionSecundaria == int.Parse(this.cboOpcionesPrincipal.SelectedValue)))
        {
             Mensaje.MostrarMensaje(this, "El tipo de sanción secundaria no puede ser la misma que la principal");
            return;
        }

        // verificamos si ha escrito la sancion secundaria aplicada
        if (this.txtSancionAplicadaSec.Text.Trim().Length == 0)
        {
             Mensaje.MostrarMensaje(this, "Debe digitar la sanción secundaria aplicada");
            return;
        }

        // verificamos si ya se encuentra en la lista de tipos de sanciones seleccionadas
        bool encontrado = false;

        foreach (SancionSecundaria sancionSecundaria in this.SancionesSecundarias)
        {
            if (sancionSecundaria.IdTipoSancion == idOpcionSecundaria)
            {
                encontrado = true;
                break;
            }
        }

        if (encontrado)
        {
             Mensaje.MostrarMensaje(this, "El tipo de opción secundaria ya fué elegida con anterioridad");
            return;
        }

        // tomamos la lista de tipos de sanción de la base de datos
        Listas listas = new Listas();
        DataTable tabla = listas.ListaOpcionSancion().Tables[0];
        listas = null;

        string nombreTipoSancion = "";
        
        foreach (DataRow dr in tabla.Rows)
        {
            if (int.Parse(dr["ID_OPCION_SANCION"].ToString()) == idOpcionSecundaria)
            {
                nombreTipoSancion = dr["NOMBRE_OPCION_SANCION"].ToString();
                break;
            }            
        }

        tabla = null;

        // creamos y agregamos la sanción secundaria
        SancionSecundaria secundaria = new SancionSecundaria();
        secundaria.IdTipoSancion = idOpcionSecundaria;
        secundaria.NombreTipoSancion = nombreTipoSancion;
        secundaria.SancionAplicada = this.txtSancionAplicadaSec.Text;

        this.SancionesSecundarias.Add(secundaria);

        actualizarListaSecundarias();
    }


    /// <summary>
    /// Método que permite actualizar la lista de sanciones secundarias
    /// </summary>
    private void actualizarListaSecundarias()
    {
        this.lstSecundaria.Items.Clear();
        this.lstSecundaria.DataSource = this.SancionesSecundarias;
        this.lstSecundaria.DataTextField = "Descripcion";
        this.lstSecundaria.DataValueField = "IdTipoSancion";
        this.lstSecundaria.DataBind();
    }
        
    /// <summary>
    /// Método que permite eliminar una sanción secundaria de la lista
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQuitarSec_Click(object sender, EventArgs e)
    {
        if (this.lstSecundaria.SelectedIndex == -1)
        {
             Mensaje.MostrarMensaje(this, "Debe seleccionar el tipo de sanción secundaria a eliminar");
            return;
        }

        int posicion = this.lstSecundaria.SelectedIndex;

        // eliminamos la i-esima sanción 
        this.SancionesSecundarias.RemoveAt(posicion);

        // actualizamos la lista
        actualizarListaSecundarias();
    }
    protected void cboDepartamentoOcurrencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarMunicipioOcurrencia();
    }
    protected void cboMunicipioOcurrencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarCorregimientoOcurrencia();
    }
    protected void cboCorregimientoOcurrencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarVeredaOcurrencia();
    }
    protected void cboTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoPersona.SelectedItem.Text.Contains("Natural"))
        {
            tbcContenedor.Tabs[1].Enabled = true;
            tbcContenedor.Tabs[2].Enabled = false;
            tbcContenedor.Tabs[1].Visible = true;
            tbcContenedor.Tabs[2].Visible = false;
            HabilitarValidacionNatural(true);
            HabilitarValidacionJuridica(false);
        }
        else
        {
            //else if (optTipoPersona.SelectedItem.Value == "Juridica")
            tbcContenedor.Tabs[1].Enabled = false;
            tbcContenedor.Tabs[2].Enabled = true;
            tbcContenedor.Tabs[1].Visible = false;
            tbcContenedor.Tabs[2].Visible = true;
            HabilitarValidacionNatural(false);
            HabilitarValidacionJuridica(true);
        }
    }
}
