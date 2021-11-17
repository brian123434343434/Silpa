using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.AccesoDatos.RegistroMinero;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using System.Data;


public partial class RegistroMinero_RegistroMinero : System.Web.UI.Page
{
    public long _idApplicationUser = -1;
    public string _usuarioRegistrado = string.Empty;
    public PersonaIdentity personaIdentity;
    public PersonaIdentity personaIdentityView
    {
        get { return (PersonaIdentity)ViewState["personaIdentity"]; }
        set { ViewState["personaIdentity"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["Usuario"] = "1958";

        if (!IsPostBack)
        {
            if (ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                Inicializar();

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
                _parametro.NombreParametro = "RegistroMinero";
                _parametroDalc.obtenerParametros(ref _parametro);

                DataRow[] dtrGrupos = dtautoridad.Select("IDUserGroup = " + _parametro.Parametro.Trim());
                if (dtrGrupos.Length > 0)
                {
                    CargarCombos(idAA);
                    CargaEditar();
                    this.btnRadicarRegMinero.Visible = true;
                }
                else
                {
                    this.btnRadicarRegMinero.Visible = false;
                }
            }

        }
    }

    /// <summary>
    /// Permite editar el registro minero
    /// </summary>
    private void CargaEditar()
    {
        if (Request.QueryString["idreg"] == null)
        {
            return;
        }

        RegistroMineroIdentity RegistroMinero = new RegistroMineroIdentity(int.Parse(Request.QueryString["idreg"]));
        RegistroMinero.Consultar(true);
        cboTipoRegistro.SelectedValue = RegistroMinero.TipoRegistroMineroID.ToString();
        txtNumeroActoAdmin.Text = RegistroMinero.NroActoAdministrativo;
        txtFechaActo.Text = RegistroMinero.FechaActoAdministrativo.Value.ToShortDateString();
        txtNumeroExpediente.Text = RegistroMinero.NroExpediente;
        cboAutoridadAmbiental.SelectedValue = RegistroMinero.AutoridadAmbiental.ToString();
        txtCodRegMineria.Text = RegistroMinero.CodigoTituloMinero;
        txtObservaciones.Text = RegistroMinero.Observaciones;

        EstadoArchivo(RegistroMinero.Archivo);

        txtFechaExpedicion.Text = RegistroMinero.FechaExpiracion.Value.ToShortDateString();

        cboVigencia.SelectedValue = RegistroMinero.Vigencia.ToString();
        txtVigencia.Enabled = false;

        if (RegistroMinero.FechaVigencia != null)
        {
            if (RegistroMinero.FechaVigencia.Value.ToString().Trim().Length != 0)
            {
                txtVigencia.Text = RegistroMinero.FechaVigencia.Value.ToShortDateString();
                txtVigencia.Enabled = true;
            }
        }

        EstadoVigencia();

        cboTipoEstado.SelectedValue = RegistroMinero.EstadoID.ToString();
        txtNombreProyecto.Text = RegistroMinero.NombreProyecto;
        txtAreaHectareas.Text = RegistroMinero.AreaHectareas.ToString();
        txtNombreMina.Text = RegistroMinero.NombreMina;

        LisTitular = new List<TitularIdentity>(RegistroMinero.LstTitulares);
        LisMineral = new List<MineralIdentity>(RegistroMinero.LstMinerales);
        LisUbicacion = new List<UbicacionIdentity>(RegistroMinero.LstUbicacion);


        foreach (var item in LisTitular)
        {
            lstOperador.Items.Insert(0, new ListItem(item.NombreTitular + " - " + item.Nroidentificacion, item.Nroidentificacion));
        }

        foreach (var item in LisMineral)
        {
            lstMineral.Items.Insert(0, new ListItem(item.NombreMineral, item.MineralID.ToString()));
        }

        foreach (var item in LisUbicacion)
        {
            lstUbicacion.Items.Insert(0, new ListItem(item.NombreDepartamento + " - " + item.NombreMunicipio, item.MunicipioID.ToString()));
        }
    }

    public void Inicializar()
    {
        LisTitular = new List<TitularIdentity>();
        LisMineral = new List<MineralIdentity>();
        LisUbicacion = new List<UbicacionIdentity>();
    }

    public List<TitularIdentity> LisTitular
    {
        get
        {
            return (List<TitularIdentity>)ViewState["LisTitular"];
        }
        set
        {
            ViewState["LisTitular"] = value;
        }
    }

    public List<MineralIdentity> LisMineral
    {
        get
        {
            return (List<MineralIdentity>)ViewState["LisMineral"];
        }
        set
        {
            ViewState["LisMineral"] = value;
        }
    }

    public List<UbicacionIdentity> LisUbicacion
    {
        get
        {
            return (List<UbicacionIdentity>)ViewState["LisUbicacion"];
        }
        set
        {
            ViewState["LisUbicacion"] = value;
        }
    }

    /// <summary>
    /// Permite el cargue de Combos
    /// </summary>
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
        //cboAutoridadAmbiental.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        EstadoRegistroMineroIdenity EstadoRegistro = new EstadoRegistroMineroIdenity();
        cboTipoEstado.DataValueField = "EstadoID";
        cboTipoEstado.DataTextField = "NombreEstado";
        cboTipoEstado.DataSource = EstadoRegistro.ListaEstadoregistroMinero();
        cboTipoEstado.DataBind();
        cboTipoEstado.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        MineralIdentity Mineral = new MineralIdentity();
        cboMineral.DataValueField = "MineralID";
        cboMineral.DataTextField = "NombreMineral";
        cboMineral.DataSource = Mineral.ListaMinerales();
        cboMineral.DataBind();
        cboMineral.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        Listas litDepartamento = new Listas();
        cboDepartamento.DataValueField = "DEP_ID";
        cboDepartamento.DataTextField = "DEP_NOMBRE";
        cboDepartamento.DataSource = litDepartamento.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
        cboDepartamento.DataBind();
        cboDepartamento.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    /// <summary>
    /// Agregar operador a la lista
    /// </summary>
    private void AgregarOperador()
    {
        if (txtNombreOperador.Text.Trim().Length != 0 && txtIdentifOperador.Text.Trim().Length != 0)
        {
            //valida que el registro no se encuentre ya en la lista
            TitularIdentity item = LisTitular.Find(x => x.Nroidentificacion == txtIdentifOperador.Text.Trim());

            if (item != null)
            {
                return;
            }

            TitularIdentity dtitular = new TitularIdentity();
            dtitular.NombreTitular = txtNombreOperador.Text.Trim();
            dtitular.Nroidentificacion = txtIdentifOperador.Text.Trim();
            LisTitular.Add(dtitular);
            lstOperador.Items.Insert(0, new ListItem(txtNombreOperador.Text.Trim() + " - " + txtIdentifOperador.Text.Trim(), txtIdentifOperador.Text.Trim()));
            txtNombreOperador.Text = "";
            txtIdentifOperador.Text = "";
            estadovalidacion();
        }
    }

    /// <summary>
    /// Agregar Minerales a la lista
    /// </summary>
    private void AgregarMineral()
    {
        if (cboMineral.SelectedValue != "-1")
        {
            //valida que el registro no se encuentre ya en la lista
            MineralIdentity item = LisMineral.Find(x => x.MineralID == int.Parse(cboMineral.SelectedValue));

            if (item != null)
            {
                return;
            }

            MineralIdentity Mineral = new MineralIdentity();
            Mineral.MineralID = int.Parse(cboMineral.SelectedValue);
            Mineral.NombreMineral = cboMineral.SelectedItem.Text.Trim();
            LisMineral.Add(Mineral);
            lstMineral.Items.Insert(0, new ListItem(cboMineral.SelectedItem.Text.Trim(), cboMineral.SelectedValue));
            cboMineral.SelectedValue = "-1";
            estadovalidacion();
        }
    }

    /// <summary>
    /// Agregar Ubicación a la lista
    /// </summary>
    private void AgregarUbicacion()
    {
        if (cboDepartamento.SelectedValue != "-1" && cboMunicipio.SelectedValue != "-1")
        {
            //valida que el registro no se encuentre ya en la lista
            UbicacionIdentity item = LisUbicacion.Find(x => x.MunicipioID == int.Parse(cboMunicipio.SelectedValue) && x.DepartamentoID == int.Parse(cboDepartamento.SelectedValue));

            if (item != null)
            {
                return;
            }

            UbicacionIdentity Ubicacion = new UbicacionIdentity();
            Ubicacion.MunicipioID = int.Parse(cboMunicipio.SelectedValue);
            Ubicacion.NombreMunicipio = cboMunicipio.SelectedItem.Text.Trim();
            Ubicacion.DepartamentoID = int.Parse(cboDepartamento.SelectedValue);
            Ubicacion.NombreDepartamento = cboDepartamento.SelectedItem.Text.Trim();
            LisUbicacion.Add(Ubicacion);
            lstUbicacion.Items.Insert(0, new ListItem(cboDepartamento.SelectedItem.Text.Trim() + " - " + cboMunicipio.SelectedItem.Text.Trim(), cboMunicipio.SelectedValue));
            cboDepartamento.SelectedValue = "-1";
            cboMunicipio.SelectedValue = "-1";
            estadovalidacion();
        }
    }

    /// <summary>
    /// Elimina el item del listado de Operador
    /// </summary>
    private void QuitarOperador()
    {
        LisTitular.RemoveAll(x => x.Nroidentificacion == lstOperador.SelectedValue.ToString());

        for (int i = 0; i < lstOperador.Items.Count; i++)
        {
            if (lstOperador.Items[i].Selected)
            {
                lstOperador.Items.RemoveAt(i);
                break;
            }
        }
    }

    /// <summary>
    /// Elimina el item del listado de Mineral
    /// </summary>
    private void QuitarMineral()
    {
        LisMineral.RemoveAll(x => x.MineralID == int.Parse(lstMineral.SelectedValue));

        for (int i = 0; i < lstMineral.Items.Count; i++)
        {
            if (lstMineral.Items[i].Selected)
            {
                lstMineral.Items.RemoveAt(i);
                break;
            }
        }
    }

    /// <summary>
    /// Elimina el item del listado de Ubicación
    /// </summary>
    private void QuitarUbicacion()
    {
        LisUbicacion.RemoveAll(x => x.MunicipioID == int.Parse(lstUbicacion.SelectedValue));

        for (int i = 0; i < lstUbicacion.Items.Count; i++)
        {
            if (lstUbicacion.Items[i].Selected)
            {
                lstUbicacion.Items.RemoveAt(i);
                break;
            }
        }
    }

    /// <summary>
    /// agregar Operador
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAgregarOperador_Click(object sender, EventArgs e)
    {
        AgregarOperador();
    }

    /// <summary>
    /// Almacenar formulario de registro minero
    /// </summary>
    private void Almacenar()
    {
        RegistroMineroIdentity RegistroMinero = new RegistroMineroIdentity();

        if (Request.QueryString["idreg"] != null)
        {
            RegistroMinero.RegistroMineroID = int.Parse(Request.QueryString["idreg"]);
        }

        RegistroMinero.TipoRegistroMineroID = int.Parse(cboTipoRegistro.SelectedValue);
        RegistroMinero.NroActoAdministrativo = txtNumeroActoAdmin.Text.Trim();
        RegistroMinero.FechaActoAdministrativo = DateTime.Parse(txtFechaActo.Text.Trim());
        RegistroMinero.NroExpediente = txtNumeroExpediente.Text.Trim();
        RegistroMinero.AutoridadAmbiental = int.Parse(cboAutoridadAmbiental.SelectedValue);
        RegistroMinero.CodigoTituloMinero = txtCodRegMineria.Text.Trim();
        RegistroMinero.Observaciones = txtObservaciones.Text.Trim();

        if (fldArchivoActo.HasFile)
        {
            string DirectoryRandom = System.IO.Path.GetRandomFileName();
            DirectoryRandom = DirectoryRandom + "-0-" + fldArchivoActo.FileName;
            if (!Directory.Exists(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + @"\Minero\")) Directory.CreateDirectory(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + @"\Minero\");
            String Ruta = ConfigurationManager.AppSettings["FILE_TRAFFIC"] + @"\Minero\" + DirectoryRandom;
            fldArchivoActo.SaveAs(Ruta);
            RegistroMinero.Archivo = DirectoryRandom;
        }

        RegistroMinero.FechaExpiracion = DateTime.Parse(txtFechaExpedicion.Text.Trim());
        RegistroMinero.Vigencia = cboVigencia.SelectedItem.Text.Trim();

        if (txtVigencia.Text.Trim().Length != 0)
        {
            RegistroMinero.FechaVigencia = DateTime.Parse(txtVigencia.Text.Trim());
        }

        RegistroMinero.EstadoID = int.Parse(cboTipoEstado.SelectedValue);
        RegistroMinero.NombreProyecto = txtNombreProyecto.Text.Trim();
        RegistroMinero.AreaHectareas = decimal.Parse(txtAreaHectareas.Text.Trim());
        RegistroMinero.NombreMina = txtNombreMina.Text.Trim();

        RegistroMinero.LstTitulares = new List<TitularIdentity>(LisTitular);
        RegistroMinero.LstMinerales = new List<MineralIdentity>(LisMineral);
        RegistroMinero.LstUbicacion = new List<UbicacionIdentity>(LisUbicacion);


        if (RegistroMinero.RegistroMineroID == 0)
        {
            RegistroMinero.Insertar();
        }

        else
        {
            RegistroMinero.Actualizar();
        }


        Response.Redirect("RegistroCoordenadas.aspx?idreg=" + RegistroMinero.RegistroMineroID);
    }

    private void estadovalidacion()
    {
        Loperador.Visible = false;
        Lmineral.Visible = false;
        Lubicacion.Visible = false;

        if (lstOperador.Items.Count == 0)
        {
            Loperador.Visible = true;
        }

        if (lstMineral.Items.Count == 0)
        {
            Lmineral.Visible = true;
        }

        if (lstUbicacion.Items.Count == 0)
        {
            Lubicacion.Visible = true;
        }
    }

    protected void btnRadicarRegMinero_Click(object sender, EventArgs e)
    {

        if (lstOperador.Items.Count == 0 || lstOperador.Items[0].Text.Trim().Length == 0)
        {
            estadovalidacion();
            ClientScript.RegisterStartupScript(GetType(), "validar", "<script>alert('Se requiere al menos un Operador');</script>");
            return;
        }

        if (lstMineral.Items.Count == 0 || lstMineral.Items[0].Text.Trim().Length == 0)
        {
            estadovalidacion();
            ClientScript.RegisterStartupScript(GetType(), "validar", "<script>alert('Se requiere al menos un Mineral');</script>");
            return;
        }

        if (lstUbicacion.Items.Count == 0 || lstUbicacion.Items[0].Text.Trim().Length == 0)
        {
            estadovalidacion();
            ClientScript.RegisterStartupScript(GetType(), "validar", "<script>alert('Se requiere al menos una Ubicación');</script>");
            return;
        }

        if (Page.IsValid)
        {
            Almacenar();
        }
    }

    protected void btnQuitarOperador_Click(object sender, EventArgs e)
    {
        QuitarOperador();
    }

    protected void btnAgregarMineral_Click(object sender, EventArgs e)
    {
        AgregarMineral();
    }
    protected void btnQuitarMineral_Click(object sender, EventArgs e)
    {
        QuitarMineral();
    }
    protected void btnAgregarUbicacion_Click(object sender, EventArgs e)
    {
        AgregarUbicacion();
    }
    protected void btnQuitarUbicacion_Click(object sender, EventArgs e)
    {
        QuitarUbicacion();
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

    private void EstadoArchivo(string sNomArchivo)
    {
        if (sNomArchivo.Trim().Length == 0)
        {
            fldArchivoActo.Visible = true;
            RRFVarchivo.Enabled = true;
            LBarchivo.Visible = false;
            LBestadoArchivo.Visible = false;
        }
        else
        {
            fldArchivoActo.Visible = false;
            RRFVarchivo.Enabled = false;
            ViewState["sNomArchivo"] = sNomArchivo;
            
            if (sNomArchivo.IndexOf("-0-") != 0)
            {
                LBarchivo.Text = sNomArchivo.Substring(sNomArchivo.IndexOf("-0-") + 3);
            }
            else
            {
                LBarchivo.Text = sNomArchivo;
            }
            
            LBarchivo.Visible = true;
            LBestadoArchivo.Visible = true;
        }
    }

    protected void LBestadoArchivo_Click(object sender, EventArgs e)
    {
        if (LBarchivo.Text.Trim().Length != 0)
        {
            String Ruta = ConfigurationManager.AppSettings["FILE_TRAFFIC"] + @"\Minero\" + ViewState["sNomArchivo"].ToString();
            System.IO.File.Delete(Ruta);
            EstadoArchivo("");
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
            if (mensaje.IndexOf("Token invalido") > 0)
                return false;
        }
        return true;
    }

    private void EstadoVigencia()
    {
        if (cboVigencia.SelectedValue == "Fecha")
        {
            txtVigencia.Enabled = true;
            RFVvigencia.Enabled = true;
            RFVvigenciaFormato.Enabled = true;
        }
        else
        {
            txtVigencia.Enabled = false;
            RFVvigencia.Enabled = false;
            RFVvigenciaFormato.Enabled = false;
        }
    }

    protected void cboVigencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtVigencia.Text = "";
        EstadoVigencia();
    }
}