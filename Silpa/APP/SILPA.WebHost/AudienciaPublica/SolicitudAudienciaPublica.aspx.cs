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
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using SILPA.Comun;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.AudienciaPublica;
using SILPA.LogicaNegocio.Enumeracion;
using SILPA.AccesoDatos.AudienciaPublica;
using SILPA.Servicios.Generico.RadicarDocumento;
using SILPA.AccesoDatos.ImpresionesFus;
using SILPA.Servicios.Generico;
using SILPA.Servicios.SolicitudDAA;
using SoftManagement.Log;

using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.BPMProcess;
using System.Xml.Serialization;
using SILPA.LogicaNegocio.Sancionatorio;
using System.Data.SqlTypes;
using SILPA.LogicaNegocio.DAA;

public partial class Informacion_Publicaciones : System.Web.UI.Page
{
    public string _usuarioRegistrado;
    public PersonaDalc personaDalc;
    public PersonaIdentity personaIdentity;
    public List<DireccionPersonaIdentity> lstDirecciones;

    public string tipoPersona;

    protected void Page_Load(object sender, EventArgs e)
    {
        Mensaje.LimpiarMensaje(this);
        this._usuarioRegistrado = string.Empty;
        //   listaCoordenadas = new List<string>();
        if (Page.Request["Ubic"] == null)
        {
            if (ValidacionToken())
            {
                this._usuarioRegistrado = (string)Session["Usuario"];
                this.trCaptcha.Visible = false;
            }

        }


        if (!Page.IsPostBack)
        {
            //if (Request.QueryString["id"] != null)
            //    this.lblId.Text = Request.QueryString["id"].ToString();
            this.lblFormulario.Text = Path.GetFileName(Request.Url.AbsolutePath);
            this.llenarControles();
            this.CrearTabla();

            //this.ConsultarDatos(Convert.ToInt32(this.lblId.Text));         
            this.tbAudienciaPublica.Tabs[1].Enabled = false; // persona natural
            this.tbAudienciaPublica.Tabs[2].Enabled = false;
            this.tbAudienciaPublica.Tabs[4].Enabled = false; // el vocero
            this.tbAudienciaPublica.Tabs[5].Enabled = false;
            this.tbAudienciaPublica.Tabs[6].Enabled = false;            
            this.tbAudienciaPublica.ActiveTabIndex = 0;

            this.rfvPrimerNombrePersona.Enabled = false;
            this.rfvPrimerApellidoPersona.Enabled = false;
            this.rfvTipoDocumentoPersona.Enabled = false;
            this.rfvNumeroIdentificacionPersona.Enabled = false;
            this.rfvDepartamentoOrigenPersona.Enabled = false;
            this.rfvMunicipioOrigenPersona.Enabled = false;
            this.rfvCorreoPersona.Enabled = false;
            this.rfvFuncionario.Enabled = false;
            this.rfvPrimerNombreFuncionario.Enabled = false;
            this.rfvPrimerApellidoFuncionario.Enabled = false;
            this.rfvTipoDocumentoFuncionario.Enabled = false;
            this.rfvNumeroIdentificacionFuncionario.Enabled = false;
            this.rfvDepartamentoOrigenFuncionario.Enabled = false;
            this.rfvMunicipioOrigenFuncionario.Enabled = false;
            this.rfvDireccionFuncionario.Enabled = false;
            this.rfvDepartamentoFuncionario.Enabled = false;
            this.rfvMunicipioFuncionario.Enabled = false;
            this.rfvTelefonoFuncionario.Enabled = false;
            this.rfvCorreoFuncionario.Visible = false;
            this.rfvRazonSocialEntidad.Enabled = false;
            this.rfvNitEntidad.Enabled = false;
            this.rfvCorreoEntidad.Enabled = false;
            this.rfvPrimerNombreRepresentante.Enabled = false;
            this.rfvPrimerApellidoRepresentante.Enabled = false;
            this.rfvTipoDocumentoRepresentante.Enabled = false;
            this.rfvNumeroIdentificacionRepresentante.Enabled = false;
            this.rfvDepartamentoOrigenRepresentante.Enabled = false;
            this.rfvMunicipioOrigenRepresentante.Enabled = false;
            this.rfvCorreoRepresentante.Enabled = false;
            this.rfvPrimerNombreVocero.Enabled = false;
            this.rfvPrimerApellidoVocero.Enabled = false;
            this.rfvTipoDocumentoVocero.Enabled = false;
            this.rfvNumeroIdentificacionVocero.Enabled = false;
            this.rfvDepartamentoOrigenVocero.Enabled = false;
            this.rfvMunicipioOrigenVocero.Enabled = false;
            this.rfvDireccionVocero.Enabled = false;
            this.rfvDepartamentoVocero.Enabled = false;
            this.rfvMunicipioVocero.Enabled = false;
            this.rfvTelefonoVocero.Enabled = false;
            this.rfvCorreoVocero.Enabled = false;
            this.rfvTramite.Enabled = false;
            this.rfvUsuarioTramite.Enabled = false;

            if (!string.IsNullOrEmpty(this._usuarioRegistrado))
            {
                PersonaDalc objPersonaDalc = new PersonaDalc();

                PersonaIdentity Identity = objPersonaDalc.BuscarPersonaByUserId(this._usuarioRegistrado);
                this.IdUser.Text = Identity.IdApplicationUser.ToString();
                this.PrecargaDatosPersona(this._usuarioRegistrado);

            }


        }
    }

    private bool ValidacionToken()
    {
        //DESCOMENTAR ANTES DEL COMMIT!!!
        //Session["IDForToken"] = Request.QueryString["IdRelated"];
        //Session["IDForToken"] = "7";

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


    /// <summary>
    /// Carga los datos de la persona cuando se encuentra logeado.
    /// </summary>
    /// <param name="username">string: identificador de la persona al logearse</param>
    public void PrecargaDatosPersona(string username)
    {
        this.personaDalc = new PersonaDalc();
        //this.personaIdentity = personaDalc.ConsultarPersona(username);
        this.personaIdentity = personaDalc.ConsultarPersonaPorIdAppUser(Int64.Parse(username));

        if (this.personaIdentity.TipoPersona.CodigoTipoPersona == (int)TipoPersona.Natural)
        {
            this.cboTipoPersona.SelectedValue = this.personaIdentity.TipoPersona.CodigoTipoPersona.ToString();            
            this.cboTipoPersona.Enabled = false;
            this.txtPrimerNombrePersona.Text = this.personaIdentity.PrimerNombre;
            this.txtSegundoNombrePersona.Text = this.personaIdentity.SegundoNombre;
            this.txtPrimerApellidoPersona.Text = this.personaIdentity.PrimerApellido;
            this.txtSegundoApellidoPersona.Text = this.personaIdentity.SegundoApellido;
            this.txtNumeroIdentificacionPersona.Text = this.personaIdentity.NumeroIdentificacion;

            this.cboTipoDocumentoPersona.SelectedValue = this.personaIdentity.TipoDocumentoIdentificacion.Id.ToString();

            this.txtCorreoPersona.Text = this.personaIdentity.CorreoElectronico;

            this.tipoPersona = this.personaIdentity.TipoPersona.CodigoTipoPersona.ToString();

            DireccionPersonaDalc direccion = new DireccionPersonaDalc();
            this.personaIdentity.Direcciones = new List<DireccionPersonaIdentity>();
            this.personaIdentity.Direcciones = direccion.ObtenerDirecciones(this.personaIdentity.PersonaId);

            if (this.personaIdentity.Direcciones != null)
            {
                if (this.personaIdentity.Direcciones.Count > 0)
                {
                    this.cboDepartamentoOrigenPersona.SelectedValue = this.personaIdentity.Direcciones[0].DepartamentoId.ToString();
                    this.CargarComboMunicipios(this.cboDepartamentoOrigenPersona.SelectedValue, this.cboMunicipioOrigenPersona);
                    this.cboMunicipioOrigenPersona.SelectedValue = this.personaIdentity.Direcciones[0].MunicipioId.ToString();
                }
            }
            this.PrecargaDatosVocero();
            TipoPersonaCambia();
        }
    }

    private void llenarControles()
    {
        try
        {           
            Listas _listas = new Listas();
            //Configuracion formulario por tipo de persona
            DataSet _temp = _listas.ListarFormularioPersona(-1, -1, this.lblFormulario.Text);
            Session["FORMULARIO"] = _temp.Tables[0];

            //Tipo de persona
            _temp = _listas.ListarTipoPersona(-1, this.lblFormulario.Text);
            this.cboTipoPersona.DataSource = _temp;
            this.cboTipoPersona.DataValueField = "TPE_ID";
            this.cboTipoPersona.DataTextField = "TPE_NOMBRE";
            this.cboTipoPersona.DataBind();
            this.cboTipoPersona.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            //Funcionario publico
            FuncionarioPublico _listaFuncionario = new FuncionarioPublico();
            _temp = _listaFuncionario.ListarFuncionario(-1);
            this.cboFuncionario.DataSource = _temp;
            this.cboFuncionario.DataValueField = "FPU_ID";
            this.cboFuncionario.DataTextField = "FPU_NOMBRE";
            this.cboFuncionario.DataBind();
            this.cboFuncionario.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            //Autoridad ambiental     
            //_temp = _listas.ListarAutoridades(null);       
            _temp = _listas.ListarAutoridadesActivas();
            this.cboAutoridadAmbiental.DataSource = _temp;
            this.cboAutoridadAmbiental.DataValueField = "AUT_ID";
            this.cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
            this.cboAutoridadAmbiental.DataBind();
            this.cboAutoridadAmbiental.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            //Tipo de identificacion
            _temp = _listas.ListaTipoIdentificacion();
            this.cboTipoDocumentoFuncionario.DataSource = _temp;
            this.cboTipoDocumentoFuncionario.DataValueField = "TID_ID";
            this.cboTipoDocumentoFuncionario.DataTextField = "TID_NOMBRE";
            this.cboTipoDocumentoFuncionario.DataBind();
            this.cboTipoDocumentoFuncionario.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            this.cboTipoDocumentoPersona.DataSource = _temp;
            this.cboTipoDocumentoPersona.DataValueField = "TID_ID";
            this.cboTipoDocumentoPersona.DataTextField = "TID_NOMBRE";
            this.cboTipoDocumentoPersona.DataBind();
            this.cboTipoDocumentoPersona.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            this.cboTipoDocumentoRepresentante.DataSource = _temp;
            this.cboTipoDocumentoRepresentante.DataValueField = "TID_ID";
            this.cboTipoDocumentoRepresentante.DataTextField = "TID_NOMBRE";
            this.cboTipoDocumentoRepresentante.DataBind();
            this.cboTipoDocumentoRepresentante.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            this.cboTipoDocumentoVocero.DataSource = _temp;
            this.cboTipoDocumentoVocero.DataValueField = "TID_ID";
            this.cboTipoDocumentoVocero.DataTextField = "TID_NOMBRE";
            this.cboTipoDocumentoVocero.DataBind();
            this.cboTipoDocumentoVocero.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            //Departamento
            Configuracion _configuracion = new Configuracion();
            _temp = _listas.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
            this.cboDepartamentoFuncionario.DataSource = _temp;
            this.cboDepartamentoFuncionario.DataValueField = "DEP_ID";
            this.cboDepartamentoFuncionario.DataTextField = "DEP_NOMBRE";
            this.cboDepartamentoFuncionario.DataBind();
            this.cboDepartamentoFuncionario.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            this.cboDepartamentoVocero.DataSource = _temp;
            this.cboDepartamentoVocero.DataValueField = "DEP_ID";
            this.cboDepartamentoVocero.DataTextField = "DEP_NOMBRE";
            this.cboDepartamentoVocero.DataBind();
            this.cboDepartamentoVocero.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            //Municipio
            this.cboMunicipioFuncionario.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            this.cboMunicipioVocero.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            //Corregimiento
            this.cboCorregimientoFuncionario.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            this.cboCorregimientoVocero.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            //Vereda
            this.cboVeredaFuncionario.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            this.cboVeredaVocero.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            this.cboDepartamentoOrigenPersona.DataSource = _temp;
            this.cboDepartamentoOrigenPersona.DataValueField = "DEP_ID";
            this.cboDepartamentoOrigenPersona.DataTextField = "DEP_NOMBRE";
            this.cboDepartamentoOrigenPersona.DataBind();
            this.cboDepartamentoOrigenPersona.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            this.cboMunicipioOrigenPersona.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            this.cboDepartamentoOrigenFuncionario.DataSource = _temp;
            this.cboDepartamentoOrigenFuncionario.DataValueField = "DEP_ID";
            this.cboDepartamentoOrigenFuncionario.DataTextField = "DEP_NOMBRE";
            this.cboDepartamentoOrigenFuncionario.DataBind();
            this.cboDepartamentoOrigenFuncionario.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            this.cboMunicipioOrigenPersona.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            this.cboDepartamentoOrigenRepresentante.DataSource = _temp;
            this.cboDepartamentoOrigenRepresentante.DataValueField = "DEP_ID";
            this.cboDepartamentoOrigenRepresentante.DataTextField = "DEP_NOMBRE";
            this.cboDepartamentoOrigenRepresentante.DataBind();
            this.cboDepartamentoOrigenRepresentante.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            this.cboMunicipioOrigenRepresentante.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            this.cboDepartamentoOrigenVocero.DataSource = _temp;
            this.cboDepartamentoOrigenVocero.DataValueField = "DEP_ID";
            this.cboDepartamentoOrigenVocero.DataTextField = "DEP_NOMBRE";
            this.cboDepartamentoOrigenVocero.DataBind();
            this.cboDepartamentoOrigenVocero.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            this.cboMunicipioOrigenVocero.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            // --- combo autoridad ambiental -----
            this.cboAutoridadAmb.DataSource = _listas.ListarAutoridades(-1);
            this.cboAutoridadAmb.DataTextField = "AUT_NOMBRE";
            this.cboAutoridadAmb.DataValueField = "AUT_ID";
            this.cboAutoridadAmb.DataBind();
            this.cboAutoridadAmb.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            this.LimpiarCombo(cboTramite);          
            this.LimpiarCombo(cboUsuarioTramite);
            this.LimpiarCombo(cboNumeroVital);
            this.LimpiarCombo(cboNumeroExpediente);

            //---  setea el tipo de persona.
            if (!string.IsNullOrEmpty(this._usuarioRegistrado))
            {
                this.cboTipoPersona.SelectedValue = this.tipoPersona;
            }

        }
        catch (Exception ex)
        {            
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".llenarControles" + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
        }        
    }

    private void CargarComboMunicipios(string _indice, DropDownList _combo)
    {
        try
        {        
            if (_indice != null)
            {
                Listas _listas = new Listas();
                DataSet _temp = _listas.ListaMunicipios(null, int.Parse(_indice), null);
                _combo.DataSource = _temp;
                _combo.DataValueField = "MUN_ID";
                _combo.DataTextField = "MUN_NOMBRE";
                _combo.DataBind();
                _combo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".llenarControles" + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
        }        
    }

    private void CargarComboCorregimientos(string _indice, DropDownList _combo)
    {
        try
        {            
            if (_indice != null)
            {
                Listas _listas = new Listas();
                DataSet _temp = _listas.ListarCorregimientos(int.Parse(_indice), null);
                _combo.DataSource = _temp;
                _combo.DataValueField = "COR_ID";
                _combo.DataTextField = "COR_NOMBRE";
                _combo.DataBind();
                _combo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".llenarControles" + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
        }        
    }

    private void CargarComboVeredas(string _indiceMunicipio, string _indiceCorregimiento, DropDownList _combo)
    {
        try
        {         
            if (_indiceMunicipio != null && _indiceCorregimiento != null)
            {
                Listas _listas = new Listas();
                DataSet _temp = _listas.ListarVeredas(int.Parse(_indiceMunicipio), int.Parse(_indiceCorregimiento), null);
                _combo.DataSource = _temp;
                _combo.DataValueField = "VER_ID";
                _combo.DataTextField = "VER_NOMBRE";
                _combo.DataBind();
                _combo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".llenarControles" + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
        }
        
    }

    /// <summary>
    /// Se crea la tabla de session del documento
    /// </summary>
    private void CrearTabla()
    {
        DataTable _archivo = new DataTable();
        _archivo.Columns.Add(new DataColumn("ID", typeof(Int32)));
        _archivo.Columns.Add(new DataColumn("NOMBRE", typeof(string)));
        _archivo.Columns.Add(new DataColumn("RADICADO", typeof(string)));
        _archivo.Columns.Add(new DataColumn("DOCUMENTO", typeof(byte[])));
        Session["ARCHIVOS"] = _archivo;
    }


    /// <summary>
    /// Precarga los datos del vocero desde los datos de la persona natural.
    /// </summary>
    public void PrecargaDatosVocero()
    {
        
        this.txtPrimerNombreVocero.Text = this.txtPrimerNombrePersona.Text;
        this.txtSegundoNombreVocero.Text = this.txtSegundoNombrePersona.Text;
        this.txtPrimerApellidoVocero.Text = this.txtPrimerApellidoPersona.Text;
        this.txtSegundoApellidoVocero.Text = this.txtSegundoApellidoPersona.Text;
        this.txtNumeroIdentificacionVocero.Text = this.txtNumeroIdentificacionPersona.Text;
        this.cboTipoDocumentoVocero.SelectedValue = this.cboTipoDocumentoPersona.SelectedValue;

        this.cboDepartamentoOrigenVocero.SelectedValue = this.cboDepartamentoOrigenPersona.SelectedValue;
        this.CargarComboMunicipios(this.cboDepartamentoOrigenVocero.SelectedValue, this.cboMunicipioOrigenVocero);
        this.cboMunicipioOrigenVocero.SelectedValue = this.cboMunicipioOrigenPersona.SelectedValue;

        this.txtCorreoVocero.Text = this.txtCorreoPersona.Text;
    }



 
    private void EliminarDatos(DataTable datos, int indice, GridView grilla)
    {
        DataRow filas = datos.Rows[indice];
        filas.Delete();
    }

    protected void cboTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        TipoPersonaCambia();
    }

    private void TipoPersonaCambia()
    {
        this.cboFuncionario.Enabled = false;
        this.lblFuncionario.Enabled = false;
        this.rfvPrimerNombrePersona.Enabled = false;
        this.rfvPrimerApellidoPersona.Enabled = false;
        this.rfvTipoDocumentoPersona.Enabled = false;
        this.rfvNumeroIdentificacionPersona.Enabled = false;
        this.rfvDepartamentoOrigenPersona.Enabled = false;
        this.rfvMunicipioOrigenPersona.Enabled = false;
        this.rfvCorreoPersona.Enabled = false;
        this.rfvFuncionario.Enabled = false;
        this.rfvPrimerNombreFuncionario.Enabled = false;
        this.rfvPrimerApellidoFuncionario.Enabled = false;
        this.rfvTipoDocumentoFuncionario.Enabled = false;
        this.rfvNumeroIdentificacionFuncionario.Enabled = false;
        this.rfvDepartamentoOrigenFuncionario.Enabled = false;
        this.rfvMunicipioOrigenFuncionario.Enabled = false;
        this.rfvDireccionFuncionario.Enabled = false;
        this.rfvDepartamentoFuncionario.Enabled = false;
        this.rfvMunicipioFuncionario.Enabled = false;
        this.rfvTelefonoFuncionario.Enabled = false;
        this.rfvCorreoFuncionario.Visible = false;
        this.rfvRazonSocialEntidad.Enabled = false;
        this.rfvNitEntidad.Enabled = false;
        this.rfvCorreoEntidad.Enabled = false;
        this.rfvPrimerNombreRepresentante.Enabled = false;
        this.rfvPrimerApellidoRepresentante.Enabled = false;
        this.rfvTipoDocumentoRepresentante.Enabled = false;
        this.rfvNumeroIdentificacionRepresentante.Enabled = false;
        this.rfvDepartamentoOrigenRepresentante.Enabled = false;
        this.rfvMunicipioOrigenRepresentante.Enabled = false;
        this.rfvCorreoRepresentante.Enabled = false;
        this.rfvPrimerNombreVocero.Enabled = false;
        this.rfvPrimerApellidoVocero.Enabled = false;
        this.rfvTipoDocumentoVocero.Enabled = false;
        this.rfvNumeroIdentificacionVocero.Enabled = false;
        this.rfvDepartamentoOrigenVocero.Enabled = false;
        this.rfvMunicipioOrigenVocero.Enabled = false;
        this.rfvDireccionVocero.Enabled = false;
        this.rfvDepartamentoVocero.Enabled = false;
        this.rfvMunicipioVocero.Enabled = false;
        this.rfvTelefonoVocero.Enabled = false;
        this.rfvCorreoVocero.Enabled = false;
        this.rfvAutoridadAmb.Enabled = false;


        this.tbAudienciaPublica.Tabs[1].Enabled = false;
        this.tbAudienciaPublica.Tabs[2].Enabled = false;
        this.tbAudienciaPublica.Tabs[4].Enabled = false;
        this.tbAudienciaPublica.Tabs[5].Enabled = false;
        this.tbAudienciaPublica.Tabs[6].Enabled = false;


        DataTable _archivo = (DataTable)Session["FORMULARIO"];
        string _filtro = "TPE_ID = " + this.cboTipoPersona.SelectedValue;
        DataRow[] _filas = _archivo.Select(_filtro, string.Empty, DataViewRowState.CurrentRows);
        foreach (DataRow _fila in _filas)
        {
            this.tbAudienciaPublica.Tabs[Convert.ToInt32(_fila["FPE_NOMBRE"])].Enabled = true;   
        }

        /// si el usuario se encuentra login se precargan los datos y se oculta la pestaña de persona natural

        //Persona natural
        if (this.tbAudienciaPublica.Tabs[1].Enabled)
        {
            this.rfvPrimerNombrePersona.Enabled = true;
            this.rfvPrimerApellidoPersona.Enabled = true;
            this.rfvTipoDocumentoPersona.Enabled = true;
            this.rfvNumeroIdentificacionPersona.Enabled = true;
            this.rfvDepartamentoOrigenPersona.Enabled = true;
            this.rfvMunicipioOrigenPersona.Enabled = true;
            this.rfvCorreoPersona.Enabled = true;
            this.rfvPrimerNombreVocero.Enabled = true;
            this.rfvPrimerApellidoVocero.Enabled = true;
            this.rfvTipoDocumentoVocero.Enabled = true;
            this.rfvNumeroIdentificacionVocero.Enabled = true;
            this.rfvDepartamentoOrigenVocero.Enabled = true;
            this.rfvMunicipioOrigenVocero.Enabled = true;
            this.rfvDireccionVocero.Enabled = true;
            this.rfvDepartamentoVocero.Enabled = true;
            this.rfvMunicipioVocero.Enabled = true;
            this.rfvTelefonoVocero.Enabled = true;
            this.rfvCorreoVocero.Enabled = true;
            Mensaje.MostrarMensaje(this, "Para que su solicitud de audiencia pública sea válida, adjunto a esta solicitud debe incluir Nombre, Cedula, domicilio  y firma de mínimo 100 personas naturales que respalden su solicitud, según lo exige la normatividad vigente!");
        }
        //Entidad sin animo de lucro
        else if (this.tbAudienciaPublica.Tabs[2].Enabled)
        {
            this.rfvRazonSocialEntidad.Enabled = true;
            this.rfvNitEntidad.Enabled = true;
            this.rfvCorreoEntidad.Enabled = true;
            this.rfvPrimerNombreRepresentante.Enabled = true;
            this.rfvPrimerApellidoRepresentante.Enabled = true;
            this.rfvTipoDocumentoRepresentante.Enabled = true;
            this.rfvNumeroIdentificacionRepresentante.Enabled = true;
            this.rfvDepartamentoOrigenRepresentante.Enabled = true;
            this.rfvMunicipioOrigenRepresentante.Enabled = true;
            this.rfvCorreoRepresentante.Enabled = true;
            this.rfvPrimerNombreVocero.Enabled = true;
            this.rfvPrimerApellidoVocero.Enabled = true;
            this.rfvTipoDocumentoVocero.Enabled = true;
            this.rfvNumeroIdentificacionVocero.Enabled = true;
            this.rfvDepartamentoOrigenVocero.Enabled = true;
            this.rfvMunicipioOrigenVocero.Enabled = true;
            this.rfvDireccionVocero.Enabled = true;
            this.rfvDepartamentoVocero.Enabled = true;
            this.rfvMunicipioVocero.Enabled = true;
            this.rfvTelefonoVocero.Enabled = true;
            this.rfvCorreoVocero.Enabled = true;

            Mensaje.MostrarMensaje(this, "Para que su solicitud de audiencia pública sea válida, adjunto a esta solicitud debe incluir domunento que acredite la constitución de la entidad sin ánimo de lucro, según lo exige la normatividad vigente!");
        }
        else if (this.tbAudienciaPublica.Tabs[5].Enabled)
        {
            this.rfvFuncionario.Enabled = true;
            this.rfvPrimerNombreFuncionario.Enabled = true;
            this.rfvPrimerApellidoFuncionario.Enabled = true;
            this.rfvTipoDocumentoFuncionario.Enabled = true;
            this.rfvNumeroIdentificacionFuncionario.Enabled = true;
            this.rfvDepartamentoOrigenFuncionario.Enabled = true;
            this.rfvMunicipioOrigenFuncionario.Enabled = true;
            this.rfvDireccionFuncionario.Enabled = true;
            this.rfvDepartamentoFuncionario.Enabled = true;
            this.rfvMunicipioFuncionario.Enabled = true;
            this.rfvTelefonoFuncionario.Enabled = true;
            this.rfvCorreoFuncionario.Visible = true;

            this.cboFuncionario.Enabled = true;
            this.lblFuncionario.Enabled = true;
            Mensaje.MostrarMensaje(this, "Tenga en cuenta que los funcionarios públicos que pueden solicitar una audiencia pública son los definidos por la normatividad vigente.");
        }
    }

    protected void cboDepartamentoVocero_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CargarComboMunicipios(this.cboDepartamentoVocero.SelectedValue, this.cboMunicipioVocero);
        this.CargarComboCorregimientos(this.cboMunicipioVocero.SelectedValue, this.cboCorregimientoVocero);
        this.CargarComboVeredas(this.cboMunicipioVocero.SelectedValue, this.cboCorregimientoVocero.SelectedValue, this.cboVeredaVocero);
        this.tbAudienciaPublica.ActiveTabIndex = 4;
    }

    protected void cboMunicipioVocero_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CargarComboCorregimientos(this.cboMunicipioVocero.SelectedValue, this.cboCorregimientoVocero);
        this.CargarComboVeredas(this.cboMunicipioVocero.SelectedValue, this.cboCorregimientoVocero.SelectedValue, this.cboVeredaVocero);
        this.tbAudienciaPublica.ActiveTabIndex = 4;
    }

    protected void cboCorregimientoVocero_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CargarComboVeredas(this.cboMunicipioVocero.SelectedValue, this.cboCorregimientoVocero.SelectedValue, this.cboVeredaVocero);
        this.tbAudienciaPublica.ActiveTabIndex = 4;
    }

    protected void cboDepartamentoFuncionario_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CargarComboMunicipios(this.cboDepartamentoFuncionario.SelectedValue, this.cboMunicipioFuncionario);
        this.CargarComboCorregimientos(this.cboMunicipioFuncionario.SelectedValue, this.cboCorregimientoFuncionario);
        this.CargarComboVeredas(this.cboMunicipioFuncionario.SelectedValue, this.cboCorregimientoFuncionario.SelectedValue, this.cboVeredaFuncionario);
        this.tbAudienciaPublica.ActiveTabIndex = 5;
    }

    protected void cboMunicipioFuncionario_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CargarComboCorregimientos(this.cboMunicipioFuncionario.SelectedValue, this.cboCorregimientoFuncionario);
        this.CargarComboVeredas(this.cboMunicipioFuncionario.SelectedValue, this.cboCorregimientoFuncionario.SelectedValue, this.cboVeredaFuncionario);
        this.tbAudienciaPublica.ActiveTabIndex = 5;
    }

    protected void cboCorregimientoFuncionario_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CargarComboVeredas(this.cboMunicipioFuncionario.SelectedValue, this.cboCorregimientoFuncionario.SelectedValue, this.cboVeredaFuncionario);
        this.tbAudienciaPublica.ActiveTabIndex = 5;
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {

            if (Page.IsValid)
            {
                DateTime fechaInicial = new DateTime();
                DateTime fechaFinal = new DateTime();
                String str_Id_AA = "";
                String str_Autoridad_Ambiental = "";

                fechaInicial = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                fechaFinal = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());

                SILPA.LogicaNegocio.DAA.SolicitudDAAEIA objTramite = new SILPA.LogicaNegocio.DAA.SolicitudDAAEIA();


                DataTable dtReporte = objTramite.ListarTramitesRPT(false, fechaInicial, fechaFinal,
                    -1, this.cboNumeroVital.SelectedValue, -1,
                    "", -1, -1, this.cboNumeroExpediente.SelectedValue, "", "", 0, "0", "0", "-1");


                if (dtReporte.Rows.Count == 0)
                {
                    Mensaje.MostrarMensaje(this.Page, "No se encontro un Proyecto,Obra o Actividad asociado a los datos diligenciados");
                    return;
                }
                else
                {
                    str_Id_AA = dtReporte.Rows[0]["SOL_ID_AA"].ToString();
                    AutoridadAmbientalDalc objAutoridad = new AutoridadAmbientalDalc();
                    DataSet _ds_AA = objAutoridad.ListarAutoridadAmbiental(Convert.ToInt32(str_Id_AA));
                    str_Autoridad_Ambiental = _ds_AA.Tables[0].Rows[0]["AUT_NOMBRE"].ToString();
                }
                List<ValoresIdentity> objValoresList = new List<ValoresIdentity>();

                objValoresList.Add(CargarValores(1, "Bas", this.cboTipoPersona.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(2, "Bas", this.cboFuncionario.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(3, "Bas", this.txtRadicado.Text, 1, new Byte[1]));

                objValoresList.Add(CargarValores(4, "Bas", this.txtMotivacion.Text, 1, new Byte[1]));

                //Pestaña Proyecto Obra o Actividad
                objValoresList.Add(CargarValores(5, "Bas", this.txtNombreProyecto.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(6, "Bas", this.txtTitular.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(10, "Bas", this.cboNumeroVital.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(11, "Bas", this.cboNumeroExpediente.SelectedValue, 1, new Byte[1]));
                //objValoresList.Add(CargarValores(7, "Bas", this.cboAutoridadAmbiental.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(7, "Bas", this.cboAutoridadAmb.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(8, "Bas", this.cboTramite.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(9, "Bas", this.cboUsuarioTramite.SelectedValue, 1, new Byte[1]));


                //Pestaña Persona Natural
                objValoresList.Add(CargarValores(12, "Bas", this.txtPrimerNombrePersona.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(13, "Bas", this.txtSegundoNombrePersona.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(14, "Bas", this.txtPrimerApellidoPersona.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(15, "Bas", this.txtSegundoApellidoPersona.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(16, "Bas", this.cboTipoDocumentoPersona.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(17, "Bas", this.txtNumeroIdentificacionPersona.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(18, "Bas", this.cboDepartamentoOrigenPersona.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(19, "Bas", this.cboMunicipioOrigenPersona.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(20, "Bas", this.txtCorreoPersona.Text, 1, new Byte[1]));


                //Vocero del Grupo
                objValoresList.Add(CargarValores(21, "Bas", this.txtPrimerNombreVocero.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(22, "Bas", this.txtSegundoNombreVocero.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(23, "Bas", this.txtPrimerApellidoVocero.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(24, "Bas", this.txtSegundoApellidoVocero.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(25, "Bas", this.cboTipoDocumentoVocero.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(26, "Bas", this.txtNumeroIdentificacionVocero.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(27, "Bas", this.cboDepartamentoOrigenVocero.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(28, "Bas", this.cboMunicipioOrigenVocero.SelectedValue, 1, new Byte[1]));

                objValoresList.Add(CargarValores(29, "Bas", this.txtDireccionVocero.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(30, "Bas", this.cboDepartamentoVocero.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(31, "Bas", this.cboMunicipioVocero.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(32, "Bas", this.cboCorregimientoVocero.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(33, "Bas", this.cboVeredaVocero.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(34, "Bas", this.txtTelefonoVocero.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(35, "Bas", this.txtCelularVocero.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(36, "Bas", this.txtFaxVocero.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(37, "Bas", this.txtCorreoVocero.Text, 1, new Byte[1]));


                //Funcionario Publico
                objValoresList.Add(CargarValores(38, "Bas", this.txtPrimerNombreFuncionario.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(39, "Bas", this.txtSegundoNombreFuncionario.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(40, "Bas", this.txtPrimerApellidoFuncionario.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(41, "Bas", this.txtSegundoApellidoFuncionario.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(42, "Bas", this.cboTipoDocumentoFuncionario.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(43, "Bas", this.txtNumeroIdentificacionFuncionario.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(44, "Bas", this.cboDepartamentoOrigenFuncionario.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(45, "Bas", this.cboMunicipioOrigenFuncionario.SelectedValue, 1, new Byte[1]));

                objValoresList.Add(CargarValores(46, "Bas", this.txtDireccionFuncionario.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(47, "Bas", this.cboDepartamentoFuncionario.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(48, "Bas", this.cboMunicipioFuncionario.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(49, "Bas", this.cboCorregimientoFuncionario.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(50, "Bas", this.cboVeredaFuncionario.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(51, "Bas", this.txtTelefonoFuncionario.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(52, "Bas", this.txtCelularFuncionario.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(53, "Bas", this.txtFaxFuncionario.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(54, "Bas", this.txtCorreoFuncionario.Text, 1, new Byte[1]));


                //Pestaña Representante Legal
                objValoresList.Add(CargarValores(55, "Bas", this.txtPrimerNombreRepresentante.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(56, "Bas", this.txtSegundoNombreRepresentante.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(57, "Bas", this.txtPrimerApellidoRepresentante.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(58, "Bas", this.txtSegundoApellidoRepresentante.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(59, "Bas", this.cboTipoDocumentoRepresentante.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(60, "Bas", this.txtNumeroIdentificacionRepresentante.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(61, "Bas", this.cboDepartamentoOrigenRepresentante.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(62, "Bas", this.cboMunicipioOrigenRepresentante.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(63, "Bas", this.txtCorreoRepresentante.Text, 1, new Byte[1]));


                //Entidades Sin Animo de Lucro no activo en Form Builder
                objValoresList.Add(CargarValores(64, "Bas", this.txtRazonSocialEntidad.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(65, "Bas", this.txtNitEntidad.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(66, "Bas", this.txtCorreoEntidad.Text, 1, new Byte[1]));


                //Lista de Archivos
                int i = 0;
                DataTable _Archivo = (DataTable)Session["ARCHIVOS"];
                Session["ARCHIVOS"] = _Archivo;
                if (Session["ARCHIVOS"] != null)
                {
                    for (i = 1; i <= _Archivo.Rows.Count; i++)
                    {
                        string nombreArchivo;
                        string[] cadenaArchivo = _Archivo.Rows[i - 1]["NOMBRE"].ToString().Replace("\\", "/").Split('/');
                        nombreArchivo = cadenaArchivo[cadenaArchivo.Length - 1];
                        switch (i)
                        {
                            case 1:
                                objValoresList.Add(CargarValores(67, "List1", nombreArchivo, i, (Byte[])_Archivo.Rows[i - 1]["DOCUMENTO"]));
                                break;
                            case 2:
                                objValoresList.Add(CargarValores(70, "List1", nombreArchivo, i, (Byte[])_Archivo.Rows[i - 1]["DOCUMENTO"]));
                                break;
                            case 3:
                                objValoresList.Add(CargarValores(71, "List1", nombreArchivo, i, (Byte[])_Archivo.Rows[i - 1]["DOCUMENTO"]));
                                break;
                        }

                    }
                    if (_Archivo.Rows.Count == 0)
                    {
                        objValoresList.Add(CargarValores(67, "List1", "", 1, new byte[1]));
                        objValoresList.Add(CargarValores(70, "List1", "", 1, new byte[1]));
                        objValoresList.Add(CargarValores(71, "List1", "", 1, new byte[1]));
                    }
                    if (_Archivo.Rows.Count == 1)
                    {
                        objValoresList.Add(CargarValores(70, "List1", "", 1, new byte[1]));
                        objValoresList.Add(CargarValores(71, "List1", "", 1, new byte[1]));
                    }
                    if (_Archivo.Rows.Count == 2)
                        objValoresList.Add(CargarValores(71, "List1", "", 1, new byte[1]));
                }
                else
                {
                    objValoresList.Add(CargarValores(67, "List1", "", 1, new byte[1]));
                    objValoresList.Add(CargarValores(70, "List1", "", 1, new byte[1]));
                    objValoresList.Add(CargarValores(71, "List1", "", 1, new byte[1]));
                }

                objValoresList.Add(CargarValores(68, "List1", this.txtRadicado.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(69, "List1", "", 1, new Byte[1]));

                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer serializador = new XmlSerializer(typeof(List<ValoresIdentity>));
                serializador.Serialize(memoryStream, objValoresList);
                string xml = System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
                Queja _queja = new Queja();
                //Estos parametros hay que parametrizarlos
                DataTable ParametrosFormulario = _queja.ObtenerUsuarioAudiencia().Tables[0];
                Int64 UsuarioQueja;
                if (string.IsNullOrEmpty(this._usuarioRegistrado) || this.IdUser.Text == "")
                    UsuarioQueja = Int64.Parse(ParametrosFormulario.Rows[0]["ID_APPLICATION_USER"].ToString());
                else
                    UsuarioQueja = Int64.Parse(this.IdUser.Text);
                Int64 Formularioqueja = Int64.Parse(ParametrosFormulario.Rows[0]["FORM_ID"].ToString());
                string ClientId = ParametrosFormulario.Rows[0]["CLIENT_ID"].ToString();

                string remitente = this._usuarioRegistrado;
                //Persona Natural
                //if (string.IsNullOrEmpty(this._usuarioRegistrado))
                //{
                if (this.tbAudienciaPublica.Tabs[1].Enabled)
                {
                    remitente = this.txtPrimerNombrePersona.Text + " " + this.txtSegundoNombrePersona.Text + " " + this.txtPrimerApellidoPersona.Text + " " + this.txtSegundoApellidoPersona.Text;
                }
                //Entidad sin animo de lucro
                else if (this.tbAudienciaPublica.Tabs[2].Enabled)
                {
                    remitente = this.txtPrimerNombreRepresentante.Text + " " + this.txtSegundoNombreRepresentante.Text + " " + this.txtPrimerApellidoRepresentante.Text + " " + this.txtSegundoApellidoRepresentante.Text;
                }
                //Funcionario Publico
                else if (this.tbAudienciaPublica.Tabs[5].Enabled)
                {
                    remitente = this.txtPrimerNombreFuncionario.Text + " " + this.txtSegundoNombreFuncionario.Text + " " + this.txtPrimerApellidoFuncionario.Text + " " + this.txtSegundoApellidoFuncionario.Text;
                }
                //}
                string numerosilpa;
                if (remitente == "")
                    numerosilpa = _queja.CrearProcesoQueja(ClientId, Formularioqueja, UsuarioQueja, xml);
                else
                    numerosilpa = _queja.CrearProcesoQueja(ClientId, Formularioqueja, UsuarioQueja, xml, remitente);

                List<string> lst_numero_silpa = new List<string>();
                lst_numero_silpa.Add(numerosilpa);
                SolicitudExpediente solicitudExpediente = new SolicitudExpediente();
                solicitudExpediente.AsociarExpedienteNumeroSilpa(Convert.ToInt32(str_Id_AA), cboNumeroExpediente.SelectedValue, "Expediente", lst_numero_silpa);

                string Line1 = "RESULTADO ";
                string Line2 = "Proceso realizado correctamente ";
                string Line3 = "El número vital asignado a su proceso es el " + numerosilpa;
                //string Line4 = "Su solicítud será gestionado por la Autoridad Ambiental " + this.cboAutoridadAmbiental.SelectedItem.ToString();
                string Line4 = "Su solicítud será gestionado por la Autoridad Ambiental " + str_Autoridad_Ambiental;

                string msg;
                msg = "".PadLeft(37) + Line1 + "\\n";
                msg += "".PadLeft(25) + Line2 + "\\n";
                msg += "".PadLeft(35 - Line3.Length / 2) + Line3 + "\\n";
                msg += "".PadLeft(35 - Line4.Length / 2) + Line4;

                Mensaje.MostrarMensaje(this, msg);


                string strScript = "<script language='JavaScript'>" +
                     "location.href = '" + "../../ventanillasilpa/" + "'" +
                     "</script>";
                Page.RegisterStartupScript("PopupScript", strScript);

            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".llenarControles" + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
        }     
    }

    private ValoresIdentity CargarValores(int id, string grupo, string valor, int orden, Byte[] archivo)
    {
        ValoresIdentity objValores = new ValoresIdentity();
        objValores.Id = id;
        objValores.Grupo = grupo;
        objValores.Valor = valor;
        objValores.Orden = orden;
        objValores.Archivo = archivo;
        return objValores;
    }

    private void GenerarRTF(string numeroSilpa, string usuario, List<ImpresionArchivoFus> datos, int idradicacion)
    {
        string fecha = DateTime.Now.ToString("yyyyMMdd");
        string nombreDirectorio = ConfigurationManager.AppSettings["DireccionFus"] + fecha + "\\" + numeroSilpa + "\\" + usuario + "\\";

        string nombreArchivo = numeroSilpa + "_" + DateTime.Now.ToString("yyyyMMddmmss") + ".rtf";
        if (!(Directory.Exists(nombreDirectorio)))
        {
            Directory.CreateDirectory(nombreDirectorio);
        }
        // Escribir el Archivo
        using (StreamWriter arc = new StreamWriter(nombreDirectorio + nombreArchivo, false))
        {
            foreach (ImpresionArchivoFus fus in datos)
            {
                arc.WriteLine(fus.strCampo.ToUpper().ToString() + "\t" + fus.strValor);
            }
        }
        //Actualizar Ruta del Documento
        RadicacionDocumentoDalc rad = new RadicacionDocumentoDalc();
        rad.ActualizarRadicacionRuta(idradicacion, nombreDirectorio);

    }

    protected void lnkVerTramite_Click(object sender, EventArgs e)
    {
        //string urlPage = Request.Url.AbsolutePath;
        //urlPage = urlPage.Replace(this.lblFormulario.Text, "AdjuntarDocumento.aspx");
        //string popupScript = "<script>window.open('" + urlPage + "','FileTraffic','location=no,resizable=yes,scrollbars=yes')</script>";
        //if (!this.Page.ClientScript.IsStartupScriptRegistered("WOpen"))
        //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "WOpen", popupScript);
        string urlPage = Request.Url.AbsolutePath;
        //urlPage = urlPage.Replace(this.lblFormulario.Text, "../Mantenimiento/EnConstruccion.aspx");
        urlPage = urlPage.Replace(this.lblFormulario.Text, "../ReporteTramite/ReporteTramiteCiudadano.aspx?Ubic=ext");
        string popupScript = "<script>window.open('" + urlPage + "','FileTraffic','location=no,resizable=yes,scrollbars=yes')</script>";
        if (!this.Page.ClientScript.IsStartupScriptRegistered("WOpen"))
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "WOpen", popupScript);

    }

    protected void cboDepartamentoOrigenFuncionario_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CargarComboMunicipios(this.cboDepartamentoOrigenFuncionario.SelectedValue, this.cboMunicipioOrigenFuncionario);
        this.tbAudienciaPublica.ActiveTabIndex = 5;
    }

    protected void cboDepartamentoOrigenVocero_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CargarComboMunicipios(this.cboDepartamentoOrigenVocero.SelectedValue, this.cboMunicipioOrigenVocero);
        this.tbAudienciaPublica.ActiveTabIndex = 4;
    }

    protected void cboDepartamentoOrigenRepresentante_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CargarComboMunicipios(this.cboDepartamentoOrigenRepresentante.SelectedValue, this.cboMunicipioOrigenRepresentante);
        this.tbAudienciaPublica.ActiveTabIndex = 2;
    }

    protected void cboDepartamentoOrigenPersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CargarComboMunicipios(this.cboDepartamentoOrigenPersona.SelectedValue, this.cboMunicipioOrigenPersona);
        this.tbAudienciaPublica.ActiveTabIndex = 1;

        this.cboDepartamentoOrigenVocero.SelectedValue = this.cboDepartamentoOrigenPersona.SelectedValue;
        this.CargarComboMunicipios(this.cboDepartamentoOrigenVocero.SelectedValue, this.cboMunicipioOrigenVocero);
    }

    protected void btnAdjuntar_Click(object sender, EventArgs e)
    {
        if (this.uplAdjuntar.FileName.Length <= 0)
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Advertencia", "<script>alert('Debe seleccionar un archivo.')</script>");
            return;
        }

        //Obtiene la tabla
        DataTable _Archivo = (DataTable)Session["ARCHIVOS"];

        int consecutivo = 1;
        bool documento = true;
        if (_Archivo.Rows.Count > 0)
        {
            consecutivo = Tools.ObtenerConsecutivo(_Archivo, "ID");
            //Se valida que el documento no se haya adicionado
            string filtro = "NOMBRE = '" + this.uplAdjuntar.FileName + "'";
            DataRow[] _rows = _Archivo.Select(filtro, "", DataViewRowState.CurrentRows);
            if (_rows.Length > 0)
                documento = false;
        }

        if (!documento)
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Advertencia", "<script>alert('El documento ya ha sido agregado.')</script>");
            return;
        }

        //Adiciona el registro a la tabla
        DataRow _row = _Archivo.NewRow();
        _row["ID"] = consecutivo;
        _row["NOMBRE"] = this.uplAdjuntar.FileName;
        _row["RADICADO"] = this.txtRadicado.Text;
        _row["DOCUMENTO"] = this.uplAdjuntar.FileBytes;
        _Archivo.Rows.Add(_row);
        Session["ARCHIVOS"] = _Archivo;

        ListItem _item = new ListItem(uplAdjuntar.FileName.ToString(), consecutivo.ToString());
        lstListaArchivos.Items.Add(_item);
        lstListaArchivos.Visible = true;
        txtRadicado.Text = "";

    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        Int64 _tamanioArchivos = 0;

        if (lstListaArchivos.SelectedIndex > -1)
        {
            int _indiceArchivo = Convert.ToInt32(lstListaArchivos.SelectedValue);

            DataTable _Temp = new DataTable();
            _Temp = (DataTable)Session["ARCHIVOS"];

            if (_Temp.Select("ID = '" + lstListaArchivos.SelectedValue.ToString() + "'").Length > 0)
            {
                foreach (DataRow _reg in _Temp.Rows)
                {
                    if (Convert.ToInt32(_reg["ID"]) == _indiceArchivo)
                    {
                        ListItem _item = new ListItem(_reg["NOMBRE"].ToString(), _reg["ID"].ToString());
                        lstListaArchivos.Items.Remove(_item);
                        _Temp.Rows.Remove(_reg);
                        _Temp.AcceptChanges();
                        Session["ARCHIVOS"] = _Temp;

                        if (lstListaArchivos.Items.Count > 0)
                            lstListaArchivos.Visible = true;
                        else
                            lstListaArchivos.Visible = false;
                        return;
                    }
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string _respuestaEnvio;
        if (string.IsNullOrEmpty(this._usuarioRegistrado))
        {
            Page.Response.Redirect("MenuAudienciaPublica.aspx");
        }
        else
        {
            _respuestaEnvio = "<script language='JavaScript'>" +
               "window.close()</script>";
            Page.RegisterStartupScript("PopupScript", _respuestaEnvio);

        }
    }
    protected void tbAudienciaPublica_ActiveTabChanged(object sender, EventArgs e)
    {
        //// se precargan los datos del vocer solo si la persona seleccionada es la persona natural
        //if (int.Parse(this.cboTipoPersona.SelectedValue.ToString()) == (int)TipoPersona.Natural) 
        //{
        //    this.PrecargaDatosVocero();
        //}
    }
    protected void txtPrimerNombrePersona_TextChanged(object sender, EventArgs e)
    {

    }
    protected void cboTipoDocumentoPersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (int.Parse(this.cboTipoPersona.SelectedValue.ToString()) == (int)TipoPersona.Natural)
        this.cboTipoDocumentoVocero.SelectedValue = this.cboTipoDocumentoPersona.SelectedValue;
    }
    protected void cboMunicipioOrigenPersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.cboTipoDocumentoVocero.SelectedValue = this.Persona.SelectedValue;

        this.cboMunicipioOrigenVocero.SelectedValue = this.cboMunicipioOrigenPersona.SelectedValue;
    }
    protected void cboAutoridadAmb_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboAutoridadAmb.SelectedValue != "-1")
        {
            Listas _listas = new Listas();
            this.cboTramite.DataSource = _listas.ListarTipoTramiteVisibleAutoridadAmbiental(int.Parse(this.cboAutoridadAmb.SelectedValue));
            this.cboTramite.DataTextField = "NOMBRE_TIPO_TRAMITE";
            this.cboTramite.DataValueField = "ID";
            this.cboTramite.DataBind();
            this.cboTramite.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        else
        {
            this.LimpiarCombo(cboTramite);
            this.LimpiarCombo(cboUsuarioTramite);
            this.LimpiarCombo(cboNumeroVital);
            this.LimpiarCombo(cboNumeroExpediente);
        }
        this.tbAudienciaPublica.ActiveTabIndex = 3;
    }
    protected void cboTramite_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTramite.SelectedValue != "-1")
        {
            Listas _listas = new Listas();
            this.cboUsuarioTramite.DataSource = _listas.ListarUsuariosPorTramite(int.Parse(this.cboTramite.SelectedValue));
            this.cboUsuarioTramite.DataTextField = "NOMBRE";
            this.cboUsuarioTramite.DataValueField = "ID";
            this.cboUsuarioTramite.DataBind();
            this.cboUsuarioTramite.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        else
        {
            LimpiarCombo(this.cboUsuarioTramite);
            LimpiarCombo(this.cboNumeroVital);
            LimpiarCombo(this.cboNumeroExpediente);
        }
        this.tbAudienciaPublica.ActiveTabIndex = 3;
    }
    protected void cboUsuarioTramite_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((this.cboUsuarioTramite.SelectedValue != "-1") && (this.cboAutoridadAmb.SelectedValue != "-1"))
        {
            Listas _listas = new Listas();
            this.cboNumeroVital.DataSource = _listas.ListaNumeroVitalPorUsuarioyAA(int.Parse(this.cboUsuarioTramite.SelectedValue), int.Parse(this.cboAutoridadAmb.SelectedValue));
            this.cboNumeroVital.DataValueField = "SOL_NUMERO_SILPA";
            this.cboNumeroVital.DataTextField = "NOMBRE";
            this.cboNumeroVital.DataBind();
            this.cboNumeroVital.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        else
        {
            LimpiarCombo(cboNumeroVital);
            LimpiarCombo(cboNumeroExpediente);
        }
        this.tbAudienciaPublica.ActiveTabIndex = 3;
    }
    private void LimpiarCombo(DropDownList cbo)
    {
        cbo.Items.Clear();
        cbo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void cboNumeroVital_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboNumeroVital.SelectedValue != "-1")
        {
            Listas _listas = new Listas();
            this.cboNumeroExpediente.DataSource = _listas.ListaNumeroExpedientePorNumeroVITAL(this.cboNumeroVital.SelectedValue);
            this.cboNumeroExpediente.DataValueField = "EXPEDIENTE";
            this.cboNumeroExpediente.DataTextField = "EXPEDIENTE";
            this.cboNumeroExpediente.DataBind();
            this.cboNumeroExpediente.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        else
        {
            LimpiarCombo(cboNumeroExpediente);
        }
        this.tbAudienciaPublica.ActiveTabIndex = 3;
    }
}
