using SILPA.AccesoDatos.Aprovechamiento;
using SILPA.AccesoDatos.BPMProcess;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Salvoconducto;
using SILPA.LogicaNegocio.Aprovechamiento;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using AjaxControlToolkit;
using System.Configuration;
using System.Globalization;

public partial class CargueSaldo_SaldoSalvoconducto : System.Web.UI.Page
{
    public List<EspecimenNewIdentity> LstEspecimen { get { return (List<EspecimenNewIdentity>)ViewState["LstEspecimen"]; } set { ViewState["LstEspecimen"] = value; } }
    public List<RutaEntity> LstRutaEntity { get { return (List<RutaEntity>)ViewState["LstRutaEntity"]; } set { ViewState["LstRutaEntity"] = value; } }
    public List<TipoTransporteIdentity> LstTipoTransporte { get { return (List<TipoTransporteIdentity>)ViewState["LstTipoTransporte"]; } set { ViewState["LstTipoTransporte"] = value; } }
    public List<TransporteNewIdentity> LstTransporte { get { return (List<TransporteNewIdentity>)ViewState["LstTransporte"]; } set { ViewState["LstTransporte"] = value; } }

    private string TitularSalvoconducto
    {
        get { return ViewState["TitularSalvoconducto"].ToString(); }
        set { ViewState["TitularSalvoconducto"] = value; }
    }
    private string TitularAprovechamiento
    {
        get { return ViewState["TitularAprovechamiento"].ToString(); }
        set { ViewState["TitularAprovechamiento"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["Usuario"] = 32511;
            //ViewState["autoridad"] = 112;
            //CargarPagina();
            //return;
            if (ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                CargarPagina();
            }
        }
    }
    public void CargarPagina()
    {
        //jmartinez ajustes expedicion
        RVFechaExpedicion.MinimumValue = "01/01/1900";
        RVFechaExpedicion.MaximumValue = DateTime.Now.Date.ToString("dd/MM/yyyy");
        //jmartinez ajustes expedicion
        PersonaDalc per = new PersonaDalc();
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        TipoSalvoconducto clsTipoSalvoconducto = new TipoSalvoconducto();
        ClaseSalvoconducto clsClaseSalvoconducto = new ClaseSalvoconducto();
        SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
        ModoTransporte clsModoTransporte = new ModoTransporte();

        var tblautoridades = _listaAutoridades.ListarAutoridades(null).Tables[0];
        tblautoridades = tblautoridades.AsEnumerable().Where(x => x.Field<int>("AUT_ID") != 62 && x.Field<int>("AUT_ID") != 194 && x.Field<int>("AUT_ID") != 202 && x.Field<int>("AUT_ID") != 193).CopyToDataTable();
        Utilidades.LlenarComboTabla(tblautoridades, cboAutoridadAmbientalOtorga, "AUT_NOMBRE", "AUT_ID", true);
        int autID = 0;
        string autNombre = per.ObtenerAutoridadPorPersona(long.Parse(Session["Usuario"].ToString()), out autID);
        Utilidades.LlenarComboLista(clsClaseSalvoconducto.ListaClaseSalvoconducto(autID), cboClaseSalvoconducto, "ClaseSalvoconducto", "ClaseSalvoconductoID", true);
        Utilidades.LlenarComboTabla(tblautoridades, cboAutoridadAmbientalEmisora, "AUT_NOMBRE", "AUT_ID", true);
        ClaseRecurso vClaseRecurso = new ClaseRecurso();
        Utilidades.LlenarComboLista(vClaseRecurso.ListaClaseRecurso(), cboClaseRecurso, "ClaseRecurso", "ClaseRecursoID", true);
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentos(_configuracion.IdPaisPredeterminado).Tables[0], cboDepartamento, "DEP_NOMBRE", "DEP_ID", true);
        Utilidades.LlenarComboLista(clsTipoSalvoconducto.ListaTipoSalvoconducto(), cboTipoSalvoconducto, "TipoSalvoconducto", "TipoSalvoconductoID", true);
        //Utilidades.LlenarComboVacio(cboClaseAprovechamiento);
        Utilidades.LlenarComboVacio(cboFormaOtorgamiento);
        Utilidades.LlenarComboVacio(cboModoAdquisicion);
        Utilidades.LlenarComboVacio(cboMunicipio);
        Utilidades.LlenarComboVacio(cboClaseProducto);
        Utilidades.LlenarComboVacio(cboTipoProducto);
        Utilidades.LlenarComboVacio(cboUnidadMedida);
        Utilidades.LlenarComboVacio(cboFinalidadRecurso);
        Listas _listaTiposId = new Listas();
        DataSet _temp = _listaTiposId.ListaTipoIdentificacion();
        cboTipoIdentificacion.DataSource = _temp.Tables[0]; // ListaDocumentos(_temp, "TPE_ID = " + Convert.ToString((int)TipoPersona.Natural));
        Utilidades.LlenarComboTabla(_temp.Tables[0], cboTipoIdentificacion, "TID_NOMBRE", "TID_ID", true);
        Utilidades.LlenarComboTabla(_temp.Tables[0], cboTipoIdentificacionTitularApro, "TID_NOMBRE", "TID_ID", true);

        Utilidades.LlenarComboVacio(cboTipoRuta);
        Utilidades.LlenarComboVacio(cboModoTransporte);
        Utilidades.LlenarComboVacio(CboDpartamentoSunl);
        Utilidades.LlenarComboVacio(CboMunicipioSunl);


        Utilidades.LlenarComboLista(clsSalvoconductoNew.ListaTipoRuta(), cboTipoRuta, "TipoRutaDescripcion", "TipoRutaID", true);
        Utilidades.LlenarComboLista(clsModoTransporte.ListaModoTransporte(), cboModoTransporte, "ModoTransporte", "ModoTransporteID", true);
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentos(_configuracion.IdPaisPredeterminado).Tables[0], CboDpartamentoSunl, "DEP_NOMBRE", "DEP_ID", true);


    }
    private void InicializarFormulario()
    {
        this.cboAutoridadAmbientalOtorga.SelectedIndex = 0;
        this.cboAutoridadAmbientalEmisora.SelectedIndex = 0;
        this.cboTipoSalvoconducto.SelectedIndex = 0;
        this.cboClaseRecurso.Enabled = true;
        this.cboClaseRecurso.SelectedIndex = 0;
        //Utilidades.LlenarComboVacio(cboClaseAprovechamiento);
        Utilidades.LlenarComboVacio(cboFormaOtorgamiento);
        Utilidades.LlenarComboVacio(cboModoAdquisicion);
        Utilidades.LlenarComboVacio(cboFinalidadRecurso);
        this.txtNumeroActoAdministrativo.Text = string.Empty;
        this.txtFechaActoAdminstrativo.Text = string.Empty;
        this.txtFechaExpedicion.Text = string.Empty;
        this.txtFechaDesde.Text = string.Empty;
        this.txtFechaHasta.Text = string.Empty;
        this.txtNroSalvoconducto.Text = string.Empty;
        this.txtEstablecimiento.Text = string.Empty;
        this.cboDepartamento.SelectedIndex = 0;
        Utilidades.LlenarComboVacio(this.cboMunicipio);
        this.txtCorregimiento.Text = string.Empty;
        this.txtVereda.Text = string.Empty;
        this.txtSolicitante.Text = string.Empty;
        this.txtTitularApro.Text = string.Empty;
        this.hfIdSolicitante.Value = string.Empty;
        this.hdfTitularAprovechamientoID.Value = string.Empty;
        this.dgv_Especies.DataSource = null;
        this.dgv_Especies.DataBind();
        this.gdvEspecimenes.DataSource = null;
        this.gdvEspecimenes.DataBind();
        this.LstEspecimen = null;
        this.cboClaseSalvoconducto.SelectedValue = "";
        cboClaseSalvoconducto_SelectedIndexChanged(null, null);
        this.lblArchivo.Visible = false;
        lnkAdicionarArchivo_Click(null, null);
        this.lnkCancelarArchivo.Visible = false;
        this.lbltextValMovilizar.Visible = false;
        this.txtNombreTitular.Text = string.Empty;
        this.txtIdentificacionTitular.Text = string.Empty;
        this.txtPaisProcedencia.Text = string.Empty;
        this.LblCantVolMovilizar.Text = string.Empty;
        this.txtSUNAnterior.Text = string.Empty;
        this.grvRutaDesplazamiento.DataSource = null;
        this.grvRutaDesplazamiento.DataBind();
        this.grvTransporte.DataSource = null;
        this.grvTransporte.DataBind();
        LstRutaEntity = null;
        LstTransporte = null;

    }
    protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboDepartamento.SelectedValue == "")
        {
            Utilidades.LlenarComboVacio(cboMunicipio);
            return;
        }
        else
        {
            Listas litMunicipio = new Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(cboDepartamento.SelectedValue), null).Tables[0];
            dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            Utilidades.LlenarComboTabla(dtMunicipios, cboMunicipio, "MUN_NOMBRE", "MUN_ID", true);
        }
    }
    private bool ValidacionSaldo()
    {
        string mensajeValidacion = "";
        bool ValidaNumeroSalvoconducto = false;
        SalvoconductoNew clsSalvoconducto = new SalvoconductoNew();
        ValidaNumeroSalvoconducto = clsSalvoconducto.VerificarNumeroSalvoconducto(this.txtNroSalvoconducto.Text);

        if (ValidaNumeroSalvoconducto)
        {
            mensajeValidacion += "<br />El numero de Salvoconducto: " + this.txtNroSalvoconducto.Text + " ya existe en el sistema.";
        }


        if (LstEspecimen == null || LstEspecimen.Count == 0)
        {
            mensajeValidacion += "<br /> Información de especimenes/Debe Ingresar al menos un Especimen.";
        }
        if (mensajeValidacion != string.Empty)
        {
            lblErrorReds.Text = mensajeValidacion;
            lblErrorReds.Visible = true;
            return false;
        }
        lblErrorReds.Visible = false;
        return true;
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
        SILPA.LogicaNegocio.Usuario.Usuario usuario = new SILPA.LogicaNegocio.Usuario.Usuario();
        DataTable autoridad = usuario.ConsultarUsuarioSistemaCompania(DatosSesion.Usuario);
        if (autoridad.Rows.Count > 0)
        {
            ViewState["autoridad"] = autoridad.Rows[0]["IDAutoridad"].ToString();
        }
        return true;
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
    private string CrearXml()
    {
        List<ValoresIdentity> objValoresList = new List<ValoresIdentity>();
        //objValoresList.Add(CargarValores(1, "Bas", this.txtNombreIniciativa.Text, 1, new Byte[1]));
        //objValoresList.Add(CargarValores(2, "Bas", Session["Usuario"].ToString(), 1, new Byte[1]));
        //objValoresList.Add(CargarValores(3, "Bas", this.txtNombreRazonSocial.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(4, "Bas", "134", 1, new Byte[1])); // se envia a MADS
        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer serializador = new XmlSerializer(typeof(List<ValoresIdentity>));
        serializador.Serialize(memoryStream, objValoresList);
        string xml = System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
        return xml;
    }
    protected void cboClaseRecurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboClaseRecurso.SelectedValue != string.Empty)
        {
            ClaseAprovechamiento vClaseAprovechamiento = new ClaseAprovechamiento();
            ModoAdquisicionRecurso vModoAdquisicionRecurso = new ModoAdquisicionRecurso();
            ClaseProducto vClaseProducto = new ClaseProducto();
            FormaOtorgamiento vFormaOtorgamiento = new FormaOtorgamiento();
            FinalidadAprovechamiento clsFinalidadAprovechamiento = new FinalidadAprovechamiento();
            //Utilidades.LlenarComboLista(vClaseAprovechamiento.ListaClaseAprovechamiento(Convert.ToInt32(this.cboClaseRecurso.SelectedValue)), cboClaseAprovechamiento, "ClaseAprovechamiento", "ClaseAprovechamientoId", true);
            Utilidades.LlenarComboLista(vModoAdquisicionRecurso.ListaModoAdquisicionRecurso(Convert.ToInt32(this.cboClaseRecurso.SelectedValue), true,null), cboModoAdquisicion, "ModAdqRecurso", "ModAdqRecursoID", true);
            Utilidades.LlenarComboLista(vClaseProducto.ListaClaseProducto(Convert.ToInt32(this.cboClaseRecurso.SelectedValue), true), cboClaseProducto, "ClaseProducto", "ClaseProductoID", true);
            Utilidades.LlenarComboLista(vFormaOtorgamiento.ListaFormaOtorgamiento(Convert.ToInt32(this.cboClaseRecurso.SelectedValue), true) , cboFormaOtorgamiento, "FormaOtorgamiento", "FormaOtorgamientoID", true);
            Utilidades.LlenarComboLista(clsFinalidadAprovechamiento.ListaFinalidadAprovechamiento(Convert.ToInt32(this.cboClaseRecurso.SelectedValue)), cboFinalidadRecurso, "FinalidadRecurso", "FinalidadRecursoId", true);
            if (this.cboClaseSalvoconducto.SelectedValue == "1")
            {
                switch (Convert.ToInt32(this.cboClaseRecurso.SelectedValue))
                {
                    case 1:
                        //this.cboClaseAprovechamiento.Visible = true;
                        //this.rfvClaseAprovechamiento.Enabled = true;
                        //lblNAClaseAprovechamiento.Visible = false;
                        //this.cboModoAdquisicion.Visible = false;
                        //this.rfvModoAdquisicion.Enabled = false;
                        //lblNAModAdqui.Visible = true;
                        break;
                    case 2:
                        //this.cboClaseAprovechamiento.Visible = true;
                        //this.rfvClaseAprovechamiento.Enabled = true;
                        //lblNAClaseAprovechamiento.Visible = false;
                        //this.cboModoAdquisicion.Visible = false;
                        //this.rfvModoAdquisicion.Enabled = false;
                        //lblNAModAdqui.Visible = true;
                        break;
                    case 3:
                        //this.cboClaseAprovechamiento.Visible = false;
                        //this.rfvClaseAprovechamiento.Enabled = false;
                        //lblNAClaseAprovechamiento.Visible = true;
                        this.cboModoAdquisicion.Visible = true;
                        this.rfvModoAdquisicion.Enabled = true;
                        lblNAModAdqui.Visible = false;
                        break;
                    case 4:
                        //this.cboClaseAprovechamiento.Visible = false;
                        //this.rfvClaseAprovechamiento.Enabled = false;
                        //lblNAClaseAprovechamiento.Visible = true;
                        this.cboModoAdquisicion.Visible = true;
                        this.rfvModoAdquisicion.Enabled = true;
                        lblNAModAdqui.Visible = false;
                        break;
                }
            }
            Utilidades.LlenarComboVacio(cboTipoProducto);
            Utilidades.LlenarComboVacio(cboUnidadMedida);
        }
        else
        {
            //Utilidades.LlenarComboVacio(cboClaseAprovechamiento);
            Utilidades.LlenarComboVacio(cboModoAdquisicion);
            Utilidades.LlenarComboVacio(cboTipoProducto);
            Utilidades.LlenarComboVacio(cboUnidadMedida);
        }
    }
    protected void lnkSolicitante_Click(object sender, EventArgs e)
    {
        this.mpeSolicitantes.Show();
    }
    protected void lnkTitularApro_Click(object sender, EventArgs e)
    {
        this.mpeTitularApro.Show();
    }
    protected void btnBuscarSolicitante_Click(object sender, EventArgs e)
    {
        PersonaDalc persona = new PersonaDalc();
        if (persona.ConsultarPersona(Convert.ToInt32(this.cboTipoIdentificacion.SelectedValue), this.txtNumeroIdentificacion.Text).AsEnumerable().Where(x => x.Field<string>("active") == "T").Count() > 0)
        {
            DataTable datos = persona.ConsultarPersona(Convert.ToInt32(this.cboTipoIdentificacion.SelectedValue), this.txtNumeroIdentificacion.Text).AsEnumerable().Where(x => x.Field<string>("active") == "T").CopyToDataTable();
                datos = datos.AsEnumerable().Where(x => x.Field<int>("TIENE_GRUPO") != 0).CopyToDataTable();
            if (datos.Rows.Count > 0)
            {
                this.lblNombreSolicitante.Text = datos.Rows[0]["NOMBRE"].ToString();
                this.TitularSalvoconducto = datos.Rows[0]["ID_APPLICATION_USER"].ToString();
                this.lnkSeleccionarSolicitante.Visible = true;
            }
            else
            {
                this.lblNombreSolicitante.Text = "";
                this.lnkSeleccionarSolicitante.Visible = false;
                this.lnkSeleccionarSolicitante.Visible = false;
                lblNombreSolicitante.Text = "El usuario debe estar registrado y activo en VITAL";
            }
        }
        else
        {
                this.lblNombreSolicitante.Text = "";
                this.lnkSeleccionarSolicitante.Visible = false;
                this.lnkSeleccionarSolicitante.Visible = false;
                lblNombreSolicitante.Text = "El usuario debe estar registrado y activo en VITAL";
        }
        this.mpeSolicitantes.Show();
    }
    protected void btnBuscarTitularApro_Click(object sender, EventArgs e)
    {
        PersonaDalc persona = new PersonaDalc();
        if (persona.ConsultarPersona(Convert.ToInt32(this.cboTipoIdentificacionTitularApro.SelectedValue), this.txtNumeroIdentificacionTitularApro.Text).AsEnumerable().Where(x => x.Field<string>("active") == "T").Count() > 0)
        {
            DataTable datos = persona.ConsultarPersona(Convert.ToInt32(this.cboTipoIdentificacionTitularApro.SelectedValue), this.txtNumeroIdentificacionTitularApro.Text).AsEnumerable().Where(x => x.Field<string>("active") == "T").CopyToDataTable();
            if (datos.Rows.Count > 0)
            {
                this.lblNombreTitularApro.Text = datos.Rows[0]["NOMBRE"].ToString();
                this.TitularAprovechamiento = datos.Rows[0]["ID_APPLICATION_USER"].ToString();
                this.lnkSeleccionarTitularApro.Visible = true;
            }
            else
            {
                this.lblNombreTitularApro.Text = "";
                this.lnkSeleccionarTitularApro.Visible = false;
                this.lnkSeleccionarTitularApro.Visible = false;
                lblNombreTitularApro.Text = "El usuario debe estar registrado y activo en VITAL";
            }
        }
        else
        {
            this.lblNombreTitularApro.Text = "";
            this.lnkSeleccionarTitularApro.Visible = false;
            this.lnkSeleccionarTitularApro.Visible = false;
            lblNombreTitularApro.Text = "El usuario debe estar registrado y activo en VITAL";
        }
        this.mpeTitularApro.Show();
    }
    protected void lnkSeleccionarSolicitante_Click(object sender, EventArgs e)
    {
        this.txtSolicitante.Text = lblNombreSolicitante.Text;
        this.hfIdSolicitante.Value = TitularSalvoconducto;
        LimpiarSolicitante();
        this.mpeSolicitantes.Hide();
    }
    protected void lnkSeleccionarTitularApro_Click(object sender, EventArgs e)
    {
        this.txtTitularApro.Text = lblNombreTitularApro.Text;
        this.hdfTitularAprovechamientoID.Value = TitularAprovechamiento;
        LimpiarTitularAprovechamiento();
        this.mpeTitularApro.Hide();
    }
    protected void LimpiarSolicitante()
    {
        this.cboTipoIdentificacion.SelectedIndex = 0;
        this.txtNumeroIdentificacion.Text = "";
        this.lblNombreSolicitante.Text = "";
        this.lnkSeleccionarSolicitante.Visible = false;
    }
    protected void LimpiarTitularAprovechamiento()
    {
        this.cboTipoIdentificacionTitularApro.SelectedIndex = 0;
        this.txtNumeroIdentificacionTitularApro.Text = "";
        this.lblNombreTitularApro.Text = "";
        this.lnkSeleccionarTitularApro.Visible = false;
    }
    protected void LimpiarEspecimen()
    {
        this.txtNombreComun.Text = "";
        this.dgv_Especies.DataSource = null;
        this.dgv_Especies.DataBind();
    }
    protected void lnkEspecie_Click(object sender, EventArgs e)
    {
        if (this.cboClaseRecurso.SelectedValue != string.Empty)
        {
            LimpiarEspecimen();
            this.mpeEspecimen.Show();
        }
        else
            this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "asociacion", "<script>alert('Debe seleccionar una clase de recurso');</script>");
    }
    protected void btnBuscarEspecie_Click(object sender, EventArgs e)
    {
        BuscarEspecie();
    }
    protected void dgv_Especies_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string dataKeys = ((GridView)sender).DataKeys[e.NewEditIndex].Value.ToString();
        Label lblNombreComun = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lblNombreComun");
        string[] especie = { dataKeys, lblNombreComun.Text };
        this.txtNombreEspecie.Text = lblNombreComun.Text;
        this.hfEspcimenID.Value = especie[0];
        this.mpeEspecimen.Hide();
        LimpiarEspecimen();
    }
    protected void dgv_Especies_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.dgv_Especies.PageIndex = e.NewPageIndex;
        BuscarEspecie();
    }
    protected void BuscarEspecie()
    {
        if (this.cboClaseRecurso.SelectedValue != string.Empty && this.txtNombreComun.Text.Trim() != string.Empty)
        {
            Especie vEspecie = new Especie();
            var lstEspecimen = vEspecie.ListaEspecie(this.txtNombreComun.Text, Convert.ToInt32(this.cboClaseRecurso.SelectedValue));

            var lista = (from datos in lstEspecimen
                         select new { ESEPCIE_ID = datos.EspecieID.ToString(), NOMBRE_COMUN = datos.NombreComun, NOMBRE_CIENTIFICO = datos.NombreCientifico });
            this.dgv_Especies.DataSource = lista.ToList();
            this.dgv_Especies.DataBind();
        }
        else
        {
            this.dgv_Especies.DataSource = null;
            this.dgv_Especies.DataBind();
        }
        this.mpeEspecimen.Show();
    }
    protected void cboClaseProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboClaseProducto.SelectedValue != string.Empty)
        {
            TipoProducto vTipoProducto = new TipoProducto();
            Utilidades.LlenarComboLista(vTipoProducto.ListaTipoProducto(Convert.ToInt32(this.cboClaseProducto.SelectedValue), true), this.cboTipoProducto, "TipoProducto", "TipoProductoID", true);
            Utilidades.LlenarComboVacio(this.cboUnidadMedida);
        }
        else
        {
            Utilidades.LlenarComboVacio(this.cboTipoProducto);
            Utilidades.LlenarComboVacio(this.cboUnidadMedida);
        }
    }
    protected void cboTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoProducto.SelectedValue != string.Empty)
        {
            UnidadMedida vUnidadMedida = new UnidadMedida();
            Utilidades.LlenarComboLista(vUnidadMedida.ListaUnidadMedidaTipoProducto(Convert.ToInt32(this.cboTipoProducto.SelectedValue)), this.cboUnidadMedida, "UnidadMedidad", "UnidadMedidaId", true);
        }
        else
            Utilidades.LlenarComboVacio(this.cboUnidadMedida);
    }
    protected void btnAgregarEspecie_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            if (LstEspecimen == null)
                LstEspecimen = new List<EspecimenNewIdentity>();
            LstEspecimen.Add(new EspecimenNewIdentity
            {
                EspecieTaxonomiaID = Convert.ToInt32(this.hfEspcimenID.Value),
                NombreEspecie = this.txtNombreEspecie.Text,
                NombreComunEspecie = this.txtNombreComunEspecie.Text,
                UnidadMedidaId = Convert.ToInt32(this.cboUnidadMedida.SelectedValue),
                UnidadMedida = this.cboUnidadMedida.SelectedItem.Text,
                Cantidad = Convert.ToDouble(this.txtCantidad.Text, CultureInfo.InvariantCulture),
                CantidadLetras = new Utilidades().NumeroALetras(this.txtCantidad.Text),
                ClaseProductoID = Convert.ToInt32(this.cboClaseProducto.SelectedValue),
                ClaseProducto = this.cboClaseProducto.SelectedItem.Text,
                TipoProductoId = Convert.ToInt32(this.cboTipoProducto.SelectedValue),
                TipoProducto = this.cboTipoProducto.SelectedItem.Text,
                Volumen = Convert.ToDouble(this.txtCantidad.Text, CultureInfo.InvariantCulture),
                Descripcion = this.txtDescripcion.Text.Trim()
            });
            this.cboClaseProducto.SelectedIndex = 0;
            this.cboClaseProducto_SelectedIndexChanged(null, null);
            this.cboTipoProducto.SelectedIndex = 0;
            this.cboTipoProducto_SelectedIndexChanged(null, null);
            this.cboUnidadMedida.SelectedIndex = 0;
            this.cboUnidadMedida_SelectedIndexChanged(null, null);
            this.gdvEspecimenes.DataSource = LstEspecimen;
            this.gdvEspecimenes.DataBind();


            if (this.LstEspecimen.Count > 0)
            {
                this.lbltextValMovilizar.Visible = true;
                this.tblVolumenTotal.Visible = true;
                this.LblCantVolMovilizar.Visible = true;
                this.LblCantVolMovilizar.Text = LstEspecimen.Sum(x => x.Cantidad).ToString("N");
                cboClaseRecurso.Enabled = false;
            }
            else
            { cboClaseRecurso.Enabled = true; }
            LimpiarEspecie();
        }
    }
    protected void LimpiarEspecie()
    {
        this.hfEspcimenID.Value = null;
        this.txtNombreEspecie.Text = string.Empty;
        this.cboUnidadMedida.SelectedIndex = 0;
        this.cboClaseProducto.SelectedIndex = 0;
        this.lblCantidadLetras.Text = string.Empty;
        this.txtCantidad.Text = string.Empty;
        this.cboTipoProducto.SelectedIndex = 0;
        //this.txtCantVolTotal.Text = string.Empty;
        this.txtDescripcion.Text = string.Empty;
        this.txtNombreComunEspecie.Text = string.Empty;
    }
    protected void gdvEspecimenes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = this.gdvEspecimenes.DataKeys[e.RowIndex].Value.ToString();
        LstEspecimen.Remove(LstEspecimen.Where(x => x.EspecieTaxonomiaID == Convert.ToInt32(id)).First());
        this.gdvEspecimenes.DataSource = LstEspecimen;
        this.gdvEspecimenes.DataBind();
        if (this.LstEspecimen.Count > 0)
        {
            cboClaseRecurso.Enabled = false;
            lbltextValMovilizar.Visible = true;
            LblCantVolMovilizar.Visible = true;
            this.LblCantVolMovilizar.Text = LstEspecimen.Sum(x => x.Cantidad).ToString("N");
        }
        else
        {
            cboClaseRecurso.Enabled = true;
            lbltextValMovilizar.Visible = false;
            LblCantVolMovilizar.Visible = false;
        }
    }
    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.fuplDocumentoSoporte.FileName))
        {
            FileInfo fi = new FileInfo(this.fuplDocumentoSoporte.FileName);

            if (!fi.ToString().ToUpper().Contains(".PDF"))
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('La Exension del archivo de soporte la obtención del recurso debe ser PDF')</script>", false);
                return;
            }
        }

        if (IsValid)
        {
            if (ValidacionSaldo())
            {
                AprovechamientoIdentity objAprovechamientoIdentity = new AprovechamientoIdentity();
                SalvoconductoNewIdentity objSalvoconductoNewIdentity = new SalvoconductoNewIdentity();
                SalvoconductoNew clsSalvoconducto = new SalvoconductoNew();
                try
                {
                    if (fuplDocumentoSoporte.PostedFile != null)
                    {
                        if (this.cboClaseSalvoconducto.SelectedValue != "1")
                        {
                            int tipoAprovechamiento = 0;
                            switch (this.cboClaseSalvoconducto.SelectedValue)
                            {
                                case "2":
                                    tipoAprovechamiento = (Int32)enumTipoAprovechamiento.CITES;
                                    break;
                                case "3":
                                    tipoAprovechamiento = (Int32)enumTipoAprovechamiento.No_CITES;
                                    break;
                                case "4":
                                    tipoAprovechamiento = (Int32)enumTipoAprovechamiento.Acta;
                                    break;
                                case "5":
                                    tipoAprovechamiento = (Int32)enumTipoAprovechamiento.DocumentoTecnico;
                                    break;
                                case "6":
                                    tipoAprovechamiento = (Int32)enumTipoAprovechamiento.Convenio;
                                    break;

                            }
                            // se crea el aprovechamiento con la clase definida para la clase del salvoconducto
                            objAprovechamientoIdentity.Numero = this.txtNroSalvoconducto.Text;
                            objAprovechamientoIdentity.FechaExpedicion = Convert.ToDateTime(this.txtFechaExpedicion.Text);
                            if (this.txtFechaDesde.Visible)
                                objAprovechamientoIdentity.FechaDesde = Convert.ToDateTime(this.txtFechaDesde.Text);
                            if (this.txtFechaHasta.Visible)
                            {
                                objAprovechamientoIdentity.FechaHasta = Convert.ToDateTime(this.txtFechaHasta.Text);
                            }
                            if (cboAutoridadAmbientalEmisora.Visible)
                                objAprovechamientoIdentity.AutoridadEmisoraID = Convert.ToInt32(this.cboAutoridadAmbientalEmisora.SelectedValue);
                            if (this.txtPaisProcedencia.Visible)
                                objAprovechamientoIdentity.PaisProcedencia = this.txtPaisProcedencia.Text;
                            objAprovechamientoIdentity.TipoAprovechamientoID = tipoAprovechamiento;
                            objAprovechamientoIdentity.SolicitanteID = Convert.ToInt32(this.hfIdSolicitante.Value);
                            if (this.txtEstablecimiento.Visible)
                                objAprovechamientoIdentity.EstablecimientoProcedencia = this.txtEstablecimiento.Text;
                            if (this.cboAutoridadAmbientalOtorga.Visible)
                                objAprovechamientoIdentity.AutoridadOtorgaID = Convert.ToInt32(this.cboAutoridadAmbientalOtorga.SelectedValue);
                            if (this.txtNumeroActoAdministrativo.Visible)
                                objAprovechamientoIdentity.NumeroDocOtorga = this.txtNumeroActoAdministrativo.Text;
                            if (this.txtFechaActoAdminstrativo.Visible)
                                objAprovechamientoIdentity.FechaDocOtorga = Convert.ToDateTime(this.txtFechaActoAdminstrativo.Text);
                            if (tbTitularAProvechamiento.Visible)
                                objAprovechamientoIdentity.SolicitanteOtorgaID = Convert.ToInt32(hdfTitularAprovechamientoID.Value);
                            objAprovechamientoIdentity.ClaseRecursoId = Convert.ToInt32(this.cboClaseRecurso.SelectedValue);
                            objAprovechamientoIdentity.DepartamentoProcedenciaID = Convert.ToInt32(this.cboDepartamento.SelectedValue);
                            if (this.cboModoAdquisicion.SelectedValue != string.Empty)
                                objAprovechamientoIdentity.ModoAdquisicionRecursoID = Convert.ToInt32(this.cboModoAdquisicion.SelectedValue);
                            objAprovechamientoIdentity.MunicipioProcedenciaID = Convert.ToInt32(this.cboMunicipio.SelectedValue);
                            if (this.cboFormaOtorgamiento.SelectedValue != string.Empty)
                                objAprovechamientoIdentity.FormatOtorgamientoID = Convert.ToInt32(this.cboFormaOtorgamiento.SelectedValue);
                            objAprovechamientoIdentity.VeredaProcedencia = this.txtVereda.Text;
                            objAprovechamientoIdentity.CorregimientoProcedencia = this.txtCorregimiento.Text;
                            if (txtPaisProcedencia.Visible != false)
                                objAprovechamientoIdentity.PaisProcedencia = this.txtPaisProcedencia.Text;
                            if (this.cboFinalidadRecurso.Visible)
                                objAprovechamientoIdentity.FinalidadID = Convert.ToInt32(this.cboFinalidadRecurso.SelectedValue);
                            objAprovechamientoIdentity.LstEspecies = ConvertirEspeciesSalvoAAprovechamiento(LstEspecimen);
                            Aprovechamiento vAprovechamiento = new Aprovechamiento();
                            string numeroAprovechamiento = vAprovechamiento.CrearAprovechamiento(ref objAprovechamientoIdentity);
                            string rutaArchivo = ConfigurationManager.AppSettings["DireccionFus"] + @"SaldoAprovechamiento\";
                            if (!Directory.Exists(rutaArchivo + numeroAprovechamiento))
                                Directory.CreateDirectory(rutaArchivo + numeroAprovechamiento);
                            fuplDocumentoSoporte.SaveAs(rutaArchivo + numeroAprovechamiento + @"\\" + fuplDocumentoSoporte.FileName);
                            vAprovechamiento.ActualizarRutaArchivoSaldoAprovechamiento(objAprovechamientoIdentity.AprovechamientoID, rutaArchivo + numeroAprovechamiento + "\\" + fuplDocumentoSoporte.FileName);
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('" + this.cboClaseSalvoconducto.SelectedItem.Text + " Nro. Registro " + numeroAprovechamiento + "')</script>", false);
                        }
                        else
                        {
                            // SE CREA EL OBJETO SALVOCONDUCTO.
                            if (this.cboTipoSalvoconducto.Visible != false)


                             if (LstRutaEntity == null || LstRutaEntity.Count < 2)
                             {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Debe agregar una ruta para el Salvoconducto.');", true);
                                    return;
                             }
                            if (LstTransporte == null || LstTransporte.Count == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Debe agregar un modo de transporte para el Salvoconducto.');", true);
                                return;
                            }
                            objSalvoconductoNewIdentity.TipoSalvoconductoID = Convert.ToInt32(this.cboTipoSalvoconducto.SelectedValue);
                            objSalvoconductoNewIdentity.SolicitanteID = Convert.ToInt32(hfIdSolicitante.Value);
                            objSalvoconductoNewIdentity.Numero = this.txtNroSalvoconducto.Text;
                            objSalvoconductoNewIdentity.EstablecimientoProcedencia = this.txtEstablecimiento.Text.Trim();
                            objSalvoconductoNewIdentity.EstadoID = 2;
                            objSalvoconductoNewIdentity.FechaExpedicion = Convert.ToDateTime(this.txtFechaExpedicion.Text);
                            objSalvoconductoNewIdentity.Vigencia = objSalvoconductoNewIdentity.FechaExpedicion.Year;
                            if (this.txtFechaDesde.Text.Trim() != string.Empty)
                                objSalvoconductoNewIdentity.FechaInicioVigencia = Convert.ToDateTime(this.txtFechaDesde.Text);
                            if (this.txtFechaHasta.Text.Trim() != string.Empty)
                                objSalvoconductoNewIdentity.FechaFinalVigencia = Convert.ToDateTime(this.txtFechaHasta.Text);


                            objSalvoconductoNewIdentity.ClaseRecursoID = Convert.ToInt32(this.cboClaseRecurso.SelectedValue);
                            objSalvoconductoNewIdentity.DepartamentoProcedenciaID = Convert.ToInt32(this.cboDepartamento.SelectedValue);
                            objSalvoconductoNewIdentity.MunicipioProcedenciaID = Convert.ToInt32(this.cboMunicipio.SelectedValue);
                            objSalvoconductoNewIdentity.CorregimientoProcedencia = this.txtCorregimiento.Text.Trim();
                            objSalvoconductoNewIdentity.VeredaProcedencia = this.txtVereda.Text.Trim();

                            if (this.cboAutoridadAmbientalEmisora.SelectedValue != string.Empty)
                                objSalvoconductoNewIdentity.AutoridadEmisoraID = Convert.ToInt32(cboAutoridadAmbientalEmisora.SelectedValue);// autoridad emite salvoconducto
                            if (this.cboFormaOtorgamiento.Visible != false)
                                objSalvoconductoNewIdentity.FormatOtorgamientoID = Convert.ToInt32(this.cboFormaOtorgamiento.SelectedValue);
                            if (this.cboModoAdquisicion.SelectedValue != string.Empty)
                                objSalvoconductoNewIdentity.ModoAdquisicionRecursoID = Convert.ToInt32(this.cboModoAdquisicion.SelectedValue);
                            if (this.cboFinalidadRecurso.Visible)
                                objSalvoconductoNewIdentity.FinalidadID = Convert.ToInt32(this.cboFinalidadRecurso.SelectedValue);
                            if (this.cboTipoSalvoconducto.SelectedValue == "1")
                                objSalvoconductoNewIdentity.SolicitanteOtorgaID = Convert.ToInt32(hfIdSolicitante.Value);
                            else
                            {
                                if (this.tbTitularAProvechamiento.Visible)
                                    objSalvoconductoNewIdentity.SolicitanteOtorgaID = Convert.ToInt32(hdfTitularAprovechamientoID.Value);
                            }
                            if (this.cboAutoridadAmbientalOtorga.Visible)
                                objSalvoconductoNewIdentity.AutoridadOtorgaID = Convert.ToInt32(this.cboAutoridadAmbientalOtorga.SelectedValue);
                            if (this.txtNumeroActoAdministrativo.Visible)
                                objSalvoconductoNewIdentity.NumeroDocOtorga = this.txtNumeroActoAdministrativo.Text;
                            if (this.txtFechaActoAdminstrativo.Visible)
                                objSalvoconductoNewIdentity.FechaDocOtorga = Convert.ToDateTime(this.txtFechaActoAdminstrativo.Text);
                            if (this.txtSUNAnterior.Visible)
                                objSalvoconductoNewIdentity.NumeroSUNAnterior = this.txtSUNAnterior.Text;
                            objSalvoconductoNewIdentity.EstablecimientoProcedencia = this.txtEstablecimiento.Text;
                            objSalvoconductoNewIdentity.UsuarioCargue = DatosSesion.Usuario;
                            objSalvoconductoNewIdentity.AutoridadCargueID = Convert.ToInt32(ViewState["autoridad"].ToString());// autoridad que realizo el cargue

                            objSalvoconductoNewIdentity.LstEspecimen = LstEspecimen;

                            objSalvoconductoNewIdentity.TitularSalvoconducto = this.txtNombreTitular.Text.Trim();
                            objSalvoconductoNewIdentity.IdentificacionTitularSalvocoducto = this.txtIdentificacionTitular.Text.Trim();

                            //Bosques.tic para el salvoconducto se adiciona ruta y modo de transporte
                            objSalvoconductoNewIdentity.LstRuta = LstRutaEntity;
                            objSalvoconductoNewIdentity.LstTransporte = LstTransporte;

                            clsSalvoconducto.CargarSalvoconducto(ref objSalvoconductoNewIdentity);
                            string numeroSalvoconducto = objSalvoconductoNewIdentity.Numero;
                            string rutaArchivo = ConfigurationManager.AppSettings["DireccionFus"] + @"SaldoSalvoconducto\";
                            if (!Directory.Exists(rutaArchivo + numeroSalvoconducto))
                                Directory.CreateDirectory(rutaArchivo + numeroSalvoconducto);
                            fuplDocumentoSoporte.SaveAs(rutaArchivo + numeroSalvoconducto + @"\\" + fuplDocumentoSoporte.FileName);
                            clsSalvoconducto.ActualizarRutaArchivoSaldo(objSalvoconductoNewIdentity.SalvoconductoID, rutaArchivo + numeroSalvoconducto + "\\" + fuplDocumentoSoporte.FileName);
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('" + this.cboClaseSalvoconducto.SelectedItem.Text + " Nro. " + this.txtNroSalvoconducto.Text.Trim() + " registrado con exito')</script>", false);
                        }

                        InicializarFormulario();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Debe seleccionar una archivo en donde se soporte la obtención del recurso')</script>", false);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Se genero el siguiente error: " + ex.Message + "')</script>", false);
                    Aprovechamiento clsAprovechamiento = new Aprovechamiento();
                    clsSalvoconducto.EliminarSalvoconducto(objSalvoconductoNewIdentity.SalvoconductoID);
                    clsAprovechamiento.EliminarCargueAprovechamiento(objAprovechamientoIdentity.AprovechamientoID);
                    throw;
                }

            }
        }
    }
    protected void btnCerrarVinculosActividad_Click(object sender, EventArgs e)
    {
        LimpiarSolicitante();
        this.mpeSolicitantes.Hide();
    }
    protected void btnCerrarTitularApro_Click(object sender, EventArgs e)
    {
        LimpiarTitularAprovechamiento();
        this.mpeTitularApro.Hide();
    }
    protected void cboClaseSalvoconducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        string texto = "Identificacion Documento / ";
        this.LblIdentificacionDocumento.InnerText = texto;
        if (this.cboClaseSalvoconducto.SelectedValue != string.Empty)
        {
            // switch de pasi / autoridad ambiental
            switch (this.cboClaseSalvoconducto.SelectedValue)
            {
                case "2":
                    this.LblIdentificacionDocumento.InnerText = texto + this.cboClaseSalvoconducto.SelectedItem;
                    this.cboAutoridadAmbientalEmisora.SelectedIndex = 0;
                    this.cboAutoridadAmbientalEmisora.Enabled = false;
                    this.cboAutoridadAmbientalEmisora.Visible = false;
                    this.rfvAutoridadAmbientalEmisora.Enabled = false;
                    this.LblNAAutoridadAMbientalEmisora.Visible = true;
                    this.txtPaisProcedencia.Visible = true;
                    this.txtPaisProcedencia.Text = string.Empty;
                    this.rfvPaisProcedencia.Enabled = true;
                    this.LblNAPaisProcedencia.Visible = false;
                    LimpiarPestañaTransporte();
                    LimpiarPestañaRutaDesplazamiento();
                    break;
                case "3":
                    this.LblIdentificacionDocumento.InnerText = texto + this.cboClaseSalvoconducto.SelectedItem;
                    this.cboAutoridadAmbientalEmisora.SelectedIndex = 0;
                    this.cboAutoridadAmbientalEmisora.Visible = true;
                    this.cboAutoridadAmbientalEmisora.Enabled = true;
                    this.LblNAAutoridadAMbientalEmisora.Visible = false;
                    this.txtPaisProcedencia.Visible = true;
                    this.txtPaisProcedencia.Text = string.Empty;
                    this.rfvPaisProcedencia.Enabled = true;
                    this.LblNAPaisProcedencia.Visible = false;
                    LimpiarPestañaTransporte();
                    LimpiarPestañaRutaDesplazamiento();
                    break;
                case "4":
                    this.LblIdentificacionDocumento.InnerText = texto + this.cboClaseSalvoconducto.SelectedItem;
                    this.cboAutoridadAmbientalEmisora.SelectedValue = ViewState["autoridad"].ToString();
                    this.cboAutoridadAmbientalEmisora.Enabled = false;
                    this.txtPaisProcedencia.Visible = true;
                    this.txtPaisProcedencia.Text = string.Empty;
                    this.rfvPaisProcedencia.Enabled = true;
                    this.LblNAPaisProcedencia.Visible = false;
                    LimpiarPestañaTransporte();
                    LimpiarPestañaRutaDesplazamiento();
                    break;

                case "5":
                    this.LblIdentificacionDocumento.InnerText = texto + this.cboClaseSalvoconducto.SelectedItem;
                    this.cboAutoridadAmbientalEmisora.SelectedValue = ViewState["autoridad"].ToString();
                    this.cboAutoridadAmbientalEmisora.Enabled = false;
                    this.txtPaisProcedencia.Visible = true;
                    this.txtPaisProcedencia.Text = string.Empty;
                    this.rfvPaisProcedencia.Enabled = true;
                    this.LblNAPaisProcedencia.Visible = false;
                    LimpiarPestañaTransporte();
                    LimpiarPestañaRutaDesplazamiento();
                    break;

                case "6":
                    this.LblIdentificacionDocumento.InnerText = texto + this.cboClaseSalvoconducto.SelectedItem;
                    this.cboAutoridadAmbientalEmisora.SelectedValue = ViewState["autoridad"].ToString();
                    this.cboAutoridadAmbientalEmisora.Enabled = false;
                    this.txtPaisProcedencia.Visible = true;
                    this.txtPaisProcedencia.Text = string.Empty;
                    this.rfvPaisProcedencia.Enabled = true;
                    this.LblNAPaisProcedencia.Visible = false;
                    LimpiarPestañaTransporte();
                    LimpiarPestañaRutaDesplazamiento();
                    break;

                default:
                    this.LblIdentificacionDocumento.InnerText = texto + this.cboClaseSalvoconducto.SelectedItem;
                    this.cboAutoridadAmbientalEmisora.Enabled = true;
                    this.cboAutoridadAmbientalEmisora.SelectedIndex = 0;
                    this.cboAutoridadAmbientalEmisora.Visible = true;
                    this.rfvAutoridadAmbientalEmisora.Enabled = true;
                    this.LblNAAutoridadAMbientalEmisora.Visible = false;
                    this.txtPaisProcedencia.Visible = false;
                    this.txtPaisProcedencia.Text = string.Empty;
                    this.rfvPaisProcedencia.Enabled = false;
                    this.LblNAPaisProcedencia.Visible = true;
                    break;
            }
            // switch de tipo 
            switch (this.cboClaseSalvoconducto.SelectedValue)
            {
                case "1":
                    this.cboTipoSalvoconducto.SelectedIndex = 0;
                    this.cboTipoSalvoconducto.Enabled = true;
                    this.cboTipoSalvoconducto.Visible = true;
                    this.LblNATipoSalvoconducto.Visible = false;
                    this.rfvTipoSalvoconducto.Enabled = true;
                    break;
                default:
                    this.cboTipoSalvoconducto.SelectedIndex = 0;
                    this.cboTipoSalvoconducto.Enabled = false;
                    this.cboTipoSalvoconducto.Visible = false;
                    this.LblNATipoSalvoconducto.Visible = true;
                    this.rfvTipoSalvoconducto.Enabled = false;
                    break;
            }
            // switch de clase aprovechamiento, forma otorgamiento y modo adquisicion
            this.rfvFormaOtorgamiento.Enabled = true;
            this.rfvModoAdquisicion.Enabled = true;
            this.cboFormaOtorgamiento.Visible = true;
            this.cboModoAdquisicion.Visible = true;
            this.lblNAFormaOtorgamiento.Visible = false;
            this.lblNAModAdqui.Visible = false;
            switch (this.cboClaseSalvoconducto.SelectedValue)
            {
                case "2":
                    //this.cboClaseAprovechamiento.Visible = false;
                    //this.cboClaseAprovechamiento.SelectedIndex = 0;
                    //this.lblNAClaseAprovechamiento.Visible = true;
                    this.cboFormaOtorgamiento.Visible = false;
                    this.cboFormaOtorgamiento.SelectedIndex = 0;
                    this.rfvFormaOtorgamiento.Enabled = false;
                    this.lblNAFormaOtorgamiento.Visible = true;
                    this.cboModoAdquisicion.Visible = false;
                    this.cboModoAdquisicion.SelectedIndex = 0;
                    this.lblNAModAdqui.Visible = true;
                    this.rfvModoAdquisicion.Enabled = false;
                    break;
                case "3":
                    //this.cboClaseAprovechamiento.Visible = false;
                    //this.cboClaseAprovechamiento.SelectedIndex = 0;
                    //this.lblNAClaseAprovechamiento.Visible = true;
                    this.cboFormaOtorgamiento.Visible = false;
                    this.cboFormaOtorgamiento.SelectedIndex = 0;
                    this.lblNAFormaOtorgamiento.Visible = true;
                    this.rfvFormaOtorgamiento.Enabled = false;
                    this.cboModoAdquisicion.Visible = false;
                    this.cboModoAdquisicion.SelectedIndex = 0;
                    this.rfvModoAdquisicion.Enabled = false;
                    this.lblNAModAdqui.Visible = true;
                    this.cboFinalidadRecurso.Visible = true;
                    this.rfvFinalidadRecurso.Enabled = true;
                    this.cboFinalidadRecurso.SelectedIndex = 0;
                    this.lblNAFinalidad.Visible = false;

                    break;
                case "4":
                    //this.cboClaseAprovechamiento.Visible = false;
                    //this.cboClaseAprovechamiento.SelectedIndex = 0;
                    //this.lblNAClaseAprovechamiento.Visible = true;
                    this.cboFormaOtorgamiento.Visible = false;
                    this.cboFormaOtorgamiento.SelectedIndex = 0;
                    this.lblNAFormaOtorgamiento.Visible = true;
                    this.rfvFormaOtorgamiento.Enabled = false;
                    this.cboModoAdquisicion.Visible = false;
                    this.cboModoAdquisicion.SelectedIndex = 0;
                    this.lblNAModAdqui.Visible = true;
                    this.rfvModoAdquisicion.Enabled = false;
                    this.cboFinalidadRecurso.Visible = false;
                    this.cboFinalidadRecurso.SelectedIndex = 0;
                    this.lblNAFinalidad.Visible = true;
                    this.rfvFinalidadRecurso.Enabled = false;

                    break;
                case "5":
                    //this.cboClaseAprovechamiento.Visible = false;
                    //this.cboClaseAprovechamiento.SelectedIndex = 0;
                    //this.lblNAClaseAprovechamiento.Visible = true;
                    this.cboFormaOtorgamiento.Visible = false;
                    this.cboFormaOtorgamiento.SelectedIndex = 0;
                    this.lblNAFormaOtorgamiento.Visible = true;
                    this.rfvFormaOtorgamiento.Enabled = false;
                    this.cboModoAdquisicion.Visible = false;
                    this.cboModoAdquisicion.SelectedIndex = 0;
                    this.lblNAModAdqui.Visible = true;
                    this.rfvModoAdquisicion.Enabled = false;
                    this.cboFinalidadRecurso.Visible = false;
                    this.cboFinalidadRecurso.SelectedIndex = 0;
                    this.lblNAFinalidad.Visible = true;
                    this.rfvFinalidadRecurso.Enabled = false;
                    break;
                case "6":
                    //this.cboClaseAprovechamiento.Visible = false;
                    //this.cboClaseAprovechamiento.SelectedIndex = 0;
                    //this.lblNAClaseAprovechamiento.Visible = true;
                    this.cboFormaOtorgamiento.Visible = false;
                    this.cboFormaOtorgamiento.SelectedIndex = 0;
                    this.lblNAFormaOtorgamiento.Visible = true;
                    this.rfvFormaOtorgamiento.Enabled = false;
                    this.cboModoAdquisicion.Visible = false;
                    this.cboModoAdquisicion.SelectedIndex = 0;
                    this.lblNAModAdqui.Visible = true;
                    this.rfvModoAdquisicion.Enabled = false;
                    this.cboFinalidadRecurso.Visible = false;
                    this.cboFinalidadRecurso.SelectedIndex = 0;
                    this.lblNAFinalidad.Visible = true;
                    this.rfvFinalidadRecurso.Enabled = false;
                    break;

            }
            // switch pestañas
            this.lnkTitularApro.Enabled = true;
            switch (this.cboClaseSalvoconducto.SelectedValue)
            {
                case "2":

                    this.txtNumeroActoAdministrativo.Text = string.Empty;
                    this.txtNumeroActoAdministrativo.Visible = false;
                    this.rfvNumeroActoAdministrativo.Enabled = false;
                    this.LblNumeroActoAdministrativo.Visible = true;

                    this.txtFechaActoAdminstrativo.Text = string.Empty;
                    this.txtFechaActoAdminstrativo.Visible = false;
                    this.rfvFechaActoAdminstrativo.Enabled = false;
                    this.LblFechaActoAdministrativo.Visible = true;

                    this.cboAutoridadAmbientalOtorga.Visible = false;
                    this.rfvAutoridadAmbientalOtorga.Enabled = false;
                    this.LblNAAutoridadAmbientalOtorga.Visible = true;

                    this.lnkTitularApro.Enabled = false;
                    this.tbTitularAProvechamiento.Visible = false;
                    this.hdfTitularAprovechamientoID.Value = string.Empty;
                    break;
                case "3":

                    this.txtNumeroActoAdministrativo.Text = string.Empty;
                    this.txtNumeroActoAdministrativo.Visible = true;
                    this.rfvNumeroActoAdministrativo.Enabled = true;
                    this.LblNumeroActoAdministrativo.Visible = false;
                    this.txtFechaActoAdminstrativo.Text = string.Empty;
                    this.txtFechaActoAdminstrativo.Visible = true;
                    this.rfvFechaActoAdminstrativo.Enabled = true;
                    this.LblFechaActoAdministrativo.Visible = false;

                    this.cboAutoridadAmbientalOtorga.SelectedValue = "144";
                    this.cboAutoridadAmbientalOtorga.Enabled = false;
                    this.cboAutoridadAmbientalOtorga.Visible = true;
                    this.rfvAutoridadAmbientalOtorga.Enabled = true;
                    this.LblNAAutoridadAmbientalOtorga.Visible = false;
                    break;
                case "4":

                    this.txtNumeroActoAdministrativo.Text = string.Empty;
                    this.txtNumeroActoAdministrativo.Visible = false;
                    this.rfvNumeroActoAdministrativo.Enabled = false;
                    this.LblNumeroActoAdministrativo.Visible = true;

                    this.txtFechaActoAdminstrativo.Text = string.Empty;
                    this.txtFechaActoAdminstrativo.Visible = false;
                    this.rfvFechaActoAdminstrativo.Enabled = false;
                    this.LblFechaActoAdministrativo.Visible = true;

                    this.cboAutoridadAmbientalOtorga.Visible = false;
                    this.rfvAutoridadAmbientalOtorga.Enabled = false;
                    this.LblNAAutoridadAmbientalOtorga.Visible = true;
                    break;
                case "5":

                    this.txtNumeroActoAdministrativo.Text = string.Empty;
                    this.txtNumeroActoAdministrativo.Visible = false;
                    this.rfvNumeroActoAdministrativo.Enabled = false;
                    this.LblNumeroActoAdministrativo.Visible = true;

                    this.txtFechaActoAdminstrativo.Text = string.Empty;
                    this.txtFechaActoAdminstrativo.Visible = false;
                    this.rfvFechaActoAdminstrativo.Enabled = false;
                    this.LblFechaActoAdministrativo.Visible = true;

                    this.cboAutoridadAmbientalOtorga.Visible = false;
                    this.rfvAutoridadAmbientalOtorga.Enabled = false;
                    this.LblNAAutoridadAmbientalOtorga.Visible = true;
                    break;
                case "6":

                    this.txtNumeroActoAdministrativo.Text = string.Empty;
                    this.txtNumeroActoAdministrativo.Visible = false;
                    this.rfvNumeroActoAdministrativo.Enabled = false;
                    this.LblNumeroActoAdministrativo.Visible = true;

                    this.txtFechaActoAdminstrativo.Text = string.Empty;
                    this.txtFechaActoAdminstrativo.Visible = false;
                    this.rfvFechaActoAdminstrativo.Enabled = false;
                    this.LblFechaActoAdministrativo.Visible = true;

                    this.cboAutoridadAmbientalOtorga.Visible = false;
                    this.rfvAutoridadAmbientalOtorga.Enabled = false;
                    this.LblNAAutoridadAmbientalOtorga.Visible = true;
                    break;
                default:
                    
                    this.txtNumeroActoAdministrativo.Text = string.Empty;
                    this.txtNumeroActoAdministrativo.Visible = true;
                    this.rfvNumeroActoAdministrativo.Enabled = true;
                    this.LblNumeroActoAdministrativo.Visible = false;
                    this.txtFechaActoAdminstrativo.Text = string.Empty;
                    this.txtFechaActoAdminstrativo.Visible = true;
                    this.rfvFechaActoAdminstrativo.Enabled = true;
                    this.LblFechaActoAdministrativo.Visible = false;

                    this.cboAutoridadAmbientalOtorga.Visible = true;
                    this.cboAutoridadAmbientalOtorga.Enabled = true;
                    this.cboAutoridadAmbientalOtorga.SelectedIndex = 0;
                    this.rfvAutoridadAmbientalOtorga.Enabled = true;
                    this.LblNAAutoridadAmbientalOtorga.Visible = false;
                    break;
            }
            // switch titular aprovechamiento
            switch (this.cboClaseSalvoconducto.SelectedValue)
            {
                case "1":
                    this.tbTitularAProvechamiento.Visible = false;
                    this.rfvTitularApro.Enabled = false;
                break;
                case "2":
                    this.tbTitularAProvechamiento.Visible = false;
                    this.rfvTitularApro.Enabled = false;
                break;
                case "3":
                    this.tbTitularAProvechamiento.Visible = false;
                    this.rfvTitularApro.Enabled = false;
                break;
                case "4":
                    this.tbTitularAProvechamiento.Visible = false;
                        this.rfvTitularApro.Enabled = false;
                break;
                case "5":
                    this.tbTitularAProvechamiento.Visible = false;
                    this.rfvTitularApro.Enabled = false;
                    break;
                case "6":
                    this.tbTitularAProvechamiento.Visible = false;
                    this.rfvTitularApro.Enabled = false;
                    break;
                default:
                    this.LblIdentificacionDocumento.InnerText = this.LblIdentificacionDocumento.InnerText + this.cboClaseProducto.SelectedItem;
                    this.tbTitularAProvechamiento.Visible = true;
                    this.rfvTitularApro.Enabled = false;
                break;
            }
            // switch titular salvocoducto
            this.titularOtro.Visible = false;
            this.titularVital.Visible = true;
            switch (this.cboClaseSalvoconducto.SelectedValue)
            {
                case "1":
                    //this.LblIdentificacionDocumento.InnerText = this.LblIdentificacionDocumento.InnerText + this.cboClaseProducto.SelectedItem;
                    this.titularOtro.Visible = true;
                    this.txtIdentificacionTitular.Text = string.Empty;
                    this.txtNombreTitular.Text = string.Empty;
                    break;
                default:
                    break;
            }
            //switch de fecha desde y hasta
            switch (this.cboClaseSalvoconducto.SelectedValue)
            {
                case "4":
                    this.txtFechaDesde.Visible = false;
                    this.txtFechaDesde.Text = string.Empty;
                    this.lblNAFechaDesde.Visible = true;
                    this.txtFechaHasta.Visible = false;
                    this.txtFechaHasta.Text = string.Empty;
                    this.lblNAFechaHasta.Visible = true;
                    this.cvFechaHasta.Enabled = false;
                    this.rfvFechaHasta.Enabled = false;
                    this.rfvFechaDesde.Enabled = false;
                    break;
                case "5":
                    this.txtFechaDesde.Visible = false;
                    this.txtFechaDesde.Text = string.Empty;
                    this.lblNAFechaDesde.Visible = true;
                    this.txtFechaHasta.Visible = false;
                    this.txtFechaHasta.Text = string.Empty;
                    this.lblNAFechaHasta.Visible = true;
                    this.cvFechaHasta.Enabled = false;
                    this.rfvFechaHasta.Enabled = false;
                    this.rfvFechaDesde.Enabled = false;
                    break;
                case "6":
                    this.txtFechaDesde.Visible = false;
                    this.txtFechaDesde.Text = string.Empty;
                    this.lblNAFechaDesde.Visible = true;
                    this.txtFechaHasta.Visible = false;
                    this.txtFechaHasta.Text = string.Empty;
                    this.lblNAFechaHasta.Visible = true;
                    this.cvFechaHasta.Enabled = false;
                    this.rfvFechaHasta.Enabled = false;
                    this.rfvFechaDesde.Enabled = false;
                    break;
                default:
                    this.txtFechaDesde.Visible = true;
                    this.txtFechaDesde.Text = string.Empty;
                    this.lblNAFechaDesde.Visible = false;
                    this.txtFechaHasta.Visible = true;
                    this.txtFechaHasta.Text = string.Empty;
                    this.lblNAFechaHasta.Visible = false;
                    this.cvFechaHasta.Enabled = true;
                    this.rfvFechaHasta.Enabled = true;
                    this.rfvFechaDesde.Enabled = true;
                    break;
            }

        }
        else
        {
            this.cboAutoridadAmbientalEmisora.SelectedIndex = 0;
            this.cboAutoridadAmbientalEmisora.Enabled = true;
            this.cboAutoridadAmbientalEmisora.SelectedIndex = 0;
            this.cboAutoridadAmbientalEmisora.Visible = true;
            this.LblNAAutoridadAMbientalEmisora.Visible = false;
        }
    }
    protected void cboTipoSalvoconducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoSalvoconducto.SelectedValue != string.Empty)
        {
            if (this.cboTipoSalvoconducto.SelectedValue != "1")
            {
                this.tableSUNAnterior.Visible = true;
                tbTitularAProvechamiento.Visible = true;
            }
            else
            {
                tableSUNAnterior.Visible = false;
                tbTitularAProvechamiento.Visible = false;
            }
        }
        else
        {
            tableSUNAnterior.Visible = false;
        }
    }
    /// <summary>
    /// Evento que se ejecuta cuando se da clic en Modificar Archivo. Muestra el control de carga de archivo y cancelar, y oculta los demas controles
    /// </summary>
    protected void lnkAdicionarArchivo_Click(object sender, EventArgs e)
    {
        AsyncFileUpload objFileUpload = null;
        Label objLabel = null;
        LinkButton objLinkAdicionar = null;
        LinkButton objLinkCancelar = null;
        HyperLink objLinkVerArchivo = null;

        try
        {
            //Cargar controles de la fila
            objFileUpload = fuplDocumentoSoporte;
            objLinkAdicionar = lnkAdicionarArchivo;
            objLinkCancelar = lnkCancelarArchivo;
            objLinkVerArchivo = lnkVerArchivo;
            objLabel = lblArchivo;

            //Mostrar y ocultar controles
            objFileUpload.Visible = true;
            objLabel.Visible = false;
            objLinkAdicionar.Visible = false;
            objLinkCancelar.Visible = true;
            objLinkCancelar.Text = "Cancelar";

            //Ocultar boton de ver archivo
            if (objLinkVerArchivo != null)
            {
                objLinkVerArchivo.Visible = false;
            }
        }
        catch (Exception exc)
        {
            ////Escribir error
            //SMLog.Escribir(Severidad.Critico, "PDV_CrearCertificado :: lnkAdicionarArchivo_Click -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

            ////Cargar mensaje de error                        
            //this.MostrarMensaje("Se genero un error habilitando adición de archivo. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
        }
        finally
        {
            //Regenerar listados
            //RecargarListados();
        }
    }
    /// <summary>
    /// Evento que se ejecuta cuando se da clic en Cancelar. Muestra el control de Modificar Archivo, y oculta los demas controles
    /// </summary>
    protected void lnkCancelarArchivo_Click(object sender, EventArgs e)
    {
        AsyncFileUpload objFileUpload = null;
        Label objLabel = null;
        LinkButton objLinkAdicionar = null;
        LinkButton objLinkCancelar = null;
        HyperLink objLinkVerArchivo = null;

        try
        {
            //Cargar controles de la fila
            objFileUpload = fuplDocumentoSoporte;
            objLinkAdicionar = lnkAdicionarArchivo;
            objLinkCancelar = lnkCancelarArchivo;
            objLinkVerArchivo = lnkVerArchivo;
            objLabel = lblArchivo;
            //Mostrar y ocultar controles                    
            if (objFileUpload.HasFile)
            {
                objFileUpload.Visible = false;
                objLabel.Visible = true;
                objLabel.Text = objFileUpload.FileName;
                objLinkAdicionar.Visible = true;
                objLinkCancelar.Visible = false;
                objLinkCancelar.Text = "Cancelar";
                if (objLinkVerArchivo != null)
                {
                    objLinkVerArchivo.Visible = false;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(objLinkCancelar.Text))
                {
                    objFileUpload.Visible = false;
                    objLabel.Visible = true;
                    objLinkAdicionar.Visible = true;
                    objLinkCancelar.Visible = false;
                    objLinkCancelar.Text = "Cancelar";
                    if (objLinkVerArchivo != null)
                    {
                        objLinkVerArchivo.Visible = true;
                    }
                }
                else
                {
                    objFileUpload.Visible = true;
                    objLabel.Visible = false;
                    objLinkAdicionar.Visible = false;
                    objLinkCancelar.Visible = false;
                    if (objLinkVerArchivo != null)
                    {
                        objLinkVerArchivo.Visible = false;
                    }
                }
            }
        }
        catch (Exception exc)
        {
            //Escribir error
            //SMLog.Escribir(Severidad.Critico, "PDV_CrearCertificado :: lnkCancelarArchivo_Click -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

            ////Cargar mensaje de error                        
            //this.MostrarMensaje("Se genero un error cancelando adjuntar archivo. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
        }
        finally
        {
            //Regenerar listados
            //RecargarListados();
        }
    }
    enum enumTipoAprovechamiento
    {
        Aprovechamiento = 1,
        CITES = 2,
        No_CITES = 3,
        Acta = 4,
        DocumentoTecnico = 5,
        Convenio = 6

    }
    private List<EspecieAprovechamientoIdentity> ConvertirEspeciesSalvoAAprovechamiento(List<EspecimenNewIdentity> LstEspecies)
    {
        List<EspecieAprovechamientoIdentity> Lst = new List<EspecieAprovechamientoIdentity>();
        foreach (EspecimenNewIdentity especimen in LstEspecies)
        {
            Lst.Add(new EspecieAprovechamientoIdentity
            {
                EspecieTaxonomiaID = especimen.EspecieTaxonomiaID,
                NombreEspecie = especimen.NombreEspecie,
                NombreComunEspecie = especimen.NombreComunEspecie, //jmartinez
                UnidadMedidaID = especimen.UnidadMedidaId,
                UnidadMedida = especimen.UnidadMedida,
                Cantidad = especimen.Cantidad,
                CantidadEspecieLetras = especimen.CantidadLetras,
                ClaseProductoID = especimen.ClaseProductoID,
                ClaseProducto = especimen.ClaseProducto,
                TipoProductoID = especimen.TipoProductoId,
                TipoProducto = especimen.TipoProducto,
                CantidadVolumenMovilizar = especimen.Volumen,
                CantidadVolumenRemanente = 0
            });
        }
        return Lst;
    }

    protected void cboAutoridadAmbientalOtorga_SelectedIndexChanged(object sender, EventArgs e)
    {
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();

        if (cboAutoridadAmbientalOtorga.SelectedValue != "144") //PARA LA ANLA SE DEJA TODOS LOS DEPARTAMENTOS
        {
            Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentosPorAutoridadAmbiental(_configuracion.IdPaisPredeterminado, Convert.ToInt32(this.cboAutoridadAmbientalOtorga.SelectedValue)).Tables[0], cboDepartamento, "DEP_NOMBRE", "DEP_ID", true);
            Utilidades.LlenarComboVacio(this.cboMunicipio);
        }
        else
        {
            Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentos(_configuracion.IdPaisPredeterminado).Tables[0], cboDepartamento, "DEP_NOMBRE", "DEP_ID", true);
            Utilidades.LlenarComboVacio(this.cboMunicipio);
        }
    }



    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        InicializarFormulario();
        LimpiarEspecimen();
        LimpiarTitularAprovechamiento();
        LimpiarSolicitante();
    }

    protected void cboUnidadMedida_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    #region Cambio para la ruta de desplzamiento y modo de transporte
    protected void cboTipoRuta_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CboDpartamentoSunl.SelectedIndex = 0;
        this.CboDpartamentoSunl_SelectedIndexChanged(null, null);
        LstRutaEntity = new List<RutaEntity>();
        grvRutaDesplazamiento.DataSource = null;
        grvRutaDesplazamiento.DataBind();
        this.CboDpartamentoSunl.Enabled = true;
        this.CboMunicipioSunl.Enabled = true;
        if (this.cboTipoRuta.SelectedValue == "1")
        {
            this.trBarrio.Visible = false;
            this.txtBarrio.Text = string.Empty;
        }
        else
        {
            this.trBarrio.Visible = true;
            this.txtBarrio.Text = string.Empty;
        }
    }

    #endregion


    #region Adicion Ruta desplazamiento y modo de transporte
    protected void CboDpartamentoSunl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CboDpartamentoSunl.SelectedValue == "")
        {
            Utilidades.LlenarComboVacio(CboMunicipioSunl);
            return;
        }
        else
        {
            Listas litMunicipio = new Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(CboDpartamentoSunl.SelectedValue), null).Tables[0];
            dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            Utilidades.LlenarComboLista(dtMunicipios.AsEnumerable().Select(x => new { MUN_NOMBRE = x.Field<string>("MUN_NOMBRE"), MUN_ID = x.Field<Int32>("MUN_ID") }).Distinct().ToList(), CboMunicipioSunl, "MUN_NOMBRE", "MUN_ID", true);
        }
    }

    protected void CboMunicipioSunl_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnAgregarRuta_Click(object sender, EventArgs e)
    {
        if (this.cboClaseSalvoconducto.SelectedValue != "1")
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Solo se puede agregar ruta cuando el tipo de documento es Salvoconducto')</script>", false);
            LimpiarPestañaRutaDesplazamiento();
            return;
        }

        if (LstRutaEntity == null)
            LstRutaEntity = new List<RutaEntity>();
        if (this.cboTipoRuta.SelectedValue == "1")
        {
            if (LstRutaEntity.Where(x => x.DepartamentoID == Convert.ToInt32(this.CboDpartamentoSunl.SelectedValue) && x.MunicipioID == Convert.ToInt32(this.CboMunicipioSunl.SelectedValue)).Count() == 0)
            {
                LstRutaEntity.Add(new RutaEntity
                {
                    Barrio = this.txtBarrio.Text.Trim(),
                    DepartamentoID = Convert.ToInt32(this.CboDpartamentoSunl.SelectedValue),
                    Departamento = this.CboDpartamentoSunl.SelectedItem.Text,
                    MunicipioID = Convert.ToInt32(this.CboMunicipioSunl.SelectedValue),
                    Municipio = this.CboMunicipioSunl.SelectedItem.Text,
                    Orden = LstRutaEntity.Count() + 1,
                    TipoRutaID = Convert.ToInt32(this.cboTipoRuta.SelectedValue)
                });
            }
        }
        else if (this.cboTipoRuta.SelectedValue == "2")
        {
            if (LstRutaEntity.Where(x => x.DepartamentoID == Convert.ToInt32(this.CboDpartamentoSunl.SelectedValue) && x.MunicipioID == Convert.ToInt32(this.cboMunicipio.SelectedValue) && x.Barrio == txtBarrio.Text.Trim()).Count() == 0)
            {
                LstRutaEntity.Add(new RutaEntity
                {
                    Barrio = this.txtBarrio.Text.Trim(),
                    DepartamentoID = Convert.ToInt32(this.CboDpartamentoSunl.SelectedValue),
                    Departamento = this.cboDepartamento.SelectedItem.Text,
                    MunicipioID = Convert.ToInt32(this.CboMunicipioSunl.SelectedValue),
                    Municipio = this.CboMunicipioSunl.SelectedItem.Text,
                    Orden = LstRutaEntity.Count() + 1,
                    TipoRutaID = Convert.ToInt32(this.cboTipoRuta.SelectedValue)
                });
            }
        }



        if (this.cboTipoRuta.SelectedValue == "2")
        {
            this.cboDepartamento.Enabled = false;
            this.cboMunicipio.Enabled = false;
            this.txtBarrio.Text = string.Empty;
            this.grvRutaDesplazamiento.Columns[2].Visible = true;
        }
        else
        {
            this.cboDepartamento.Enabled = true;
            this.cboMunicipio.Enabled = true;
            this.grvRutaDesplazamiento.Columns[2].Visible = false;
        }
        this.grvRutaDesplazamiento.DataSource = LstRutaEntity;
        this.grvRutaDesplazamiento.DataBind();
    }
    protected void grvRutaDesplazamiento_lnkEliminar_Click(object sender, EventArgs e)
    {
        int ordenEminar = 0;
        int nuevoOrden = 1;
        try
        {
            //Cargamos el id del salvoconducto
            ordenEminar = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString().Trim());
            LstRutaEntity.RemoveAll(x => x.Orden == ordenEminar);
            foreach (var ruta in LstRutaEntity)
            {
                ruta.Orden = nuevoOrden;
                nuevoOrden++;
            }
            this.grvRutaDesplazamiento.DataSource = LstRutaEntity;
            this.grvRutaDesplazamiento.DataBind();
        }
        catch (Exception exc)
        {
        }
    }

    protected void cboModoTransporte_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboModoTransporte.SelectedValue != string.Empty)
        {
            TipoTransporte clsTipoTransporte = new TipoTransporte();
            LstTipoTransporte = clsTipoTransporte.ListaTipoTransportePorModoTransporte(Convert.ToInt32(this.cboModoTransporte.SelectedValue));
            Utilidades.LlenarComboLista(LstTipoTransporte, cboTipoVehiculo, "TipoTransporte", "TipoTransporteID", true);
        }
        else
        {
            Utilidades.LlenarComboVacio(cboTipoVehiculo);
            cboTipoVehiculo_SelectedIndexChanged(null, null);
        }
    }
    protected void cboTipoVehiculo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoVehiculo.SelectedItem.Text.Trim().Contains("Otro"))
        {
            this.trOtroTipoVehiculo.Visible = true;
            this.txtOtroTipoVehiculo.Text = string.Empty;
        }
        else
        {
            this.trOtroTipoVehiculo.Visible = false;
            this.txtOtroTipoVehiculo.Text = string.Empty;
        }
    }

    protected void btnAgregarTransporte_Click(object sender, EventArgs e)
    {
        if (this.cboClaseSalvoconducto.SelectedValue != "1")
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Solo se puede agregar modo de transporte cuando el tipo de documento es Salvoconducto')</script>", false);
            LimpiarPestañaTransporte();
            return;
        }
        if (IsValid)
        {
            int contador = 0;
            if (LstTransporte == null)
            {
                LstTransporte = new List<TransporteNewIdentity>();
            }
            contador = LstTransporte.Count() + 1;
            LstTransporte.Add(new TransporteNewIdentity
            {
                ModoTransporteID = Convert.ToInt32(this.cboModoTransporte.SelectedValue),
                ModoTransporte = this.cboModoTransporte.SelectedItem.Text,
                TipoTransporteID = Convert.ToInt32(this.cboTipoVehiculo.SelectedValue),
                TipoTransporte = this.cboTipoVehiculo.SelectedItem.Text.Contains("Otro") ? this.txtOtroTipoVehiculo.Text.Trim() : this.cboTipoVehiculo.SelectedItem.Text,
                Empresa = this.txtEmpresa.Text.Trim(),
                NumeroIdentificacionMedioTransporte = this.txtIdentificacionTransporte.Text.Trim(),
                NombreTransportador = this.txtNombreTransportador.Text.Trim(),
                NumeroIdentificacionTransportador = this.txtIdentificacionTransportador.Text.Trim(),
                TransporteSalvoconductoID = contador
            });
            grvTransporte.DataSource = LstTransporte;
            grvTransporte.DataBind();
            ReiniciarPestañaTransporte();
        }
    }

    private void ReiniciarPestañaTransporte()
    {
        this.cboModoTransporte.SelectedIndex = 0;
        this.cboTipoVehiculo.SelectedIndex = 0;
        this.txtEmpresa.Text = string.Empty;
        this.txtIdentificacionTransporte.Text = string.Empty;
        this.txtNombreTransportador.Text = string.Empty;
        this.txtIdentificacionTransportador.Text = string.Empty;
    }

    private void LimpiarPestañaTransporte()
    {
        this.cboModoTransporte.SelectedIndex = 0;
        cboModoTransporte_SelectedIndexChanged(null, null);
        this.txtEmpresa.Text = string.Empty;
        this.txtIdentificacionTransporte.Text = string.Empty;
        this.txtNombreTransportador.Text = string.Empty;
        this.txtIdentificacionTransportador.Text = string.Empty;
        this.grvRutaDesplazamiento.DataSource = null;
        this.grvRutaDesplazamiento.DataBind();
        LstTransporte = null;
    }

    private void LimpiarPestañaRutaDesplazamiento()
    {
        this.cboTipoRuta.SelectedIndex = 0;
        this.CboDpartamentoSunl.SelectedIndex = 0;
        CboDpartamentoSunl_SelectedIndexChanged(null, null);
        this.txtOtroTipoVehiculo.Text = string.Empty;
        this.txtEmpresa.Text = string.Empty;
        this.txtIdentificacionTransporte.Text = string.Empty;
        this.txtNombreTransportador.Text = string.Empty;
        this.txtIdentificacionTransportador.Text = string.Empty;
        this.grvTransporte.DataSource = null;
        this.grvTransporte.DataBind();
        LstRutaEntity = null;
    }
    protected void grvTransporte_lnkEliminar_Click(object sender, EventArgs e)
    {
        int Orden = 0;
        int identity = 0;
        try
        {
            //Cargamos el id del salvoconducto
            Orden = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString().Trim());
            identity = LstTransporte.Where(x => x.TransporteSalvoconductoID == Orden).FirstOrDefault().TransporteSalvoconductoID;
            LstTransporte.RemoveAll(x => x.TransporteSalvoconductoID == Orden);

            Orden = 0;
            foreach (var transporte in LstTransporte)
            {
                transporte.TransporteSalvoconductoID = Orden + 1;
            }
            this.grvTransporte.DataSource = LstTransporte;
            this.grvTransporte.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void LimpiarPestañasSunl()
    {
        tabInfoGeneral.Focus();
        this.cboAutoridadAmbientalEmisora.SelectedIndex = 0;
        //cboAutoridadAmbientalEmisora_SelectedIndexChanged(null, null);
        this.cboTipoSalvoconducto.SelectedIndex = 0;
        cboTipoSalvoconducto_SelectedIndexChanged(null, null);
        this.cboClaseRecurso.SelectedIndex = 0;
        cboClaseRecurso_SelectedIndexChanged(null, null);
        this.cboTipoRuta.SelectedIndex = 0;
        cboTipoRuta_SelectedIndexChanged(null, null);
        this.cboModoTransporte.SelectedIndex = 0;
        cboModoTransporte_SelectedIndexChanged(null, null);
        this.txtEmpresa.Text = "";
        this.txtIdentificacionTransporte.Text = "";
        this.txtNombreTransportador.Text = "";
        this.txtIdentificacionTransportador.Text = "";
        this.cboFinalidadRecurso.SelectedIndex = 0;
        this.txtFechaDesde.Text = string.Empty;
        this.txtFechaHasta.Text = string.Empty;
        this.grvTransporte.DataSource = null;
        this.grvTransporte.DataBind();
        //JMARTINEZ SALVOCONDUCTO FASE 2
        LimpiarPestañaTransporte();
        LimpiarPestañaRutaDesplazamiento();
    }

    #endregion
}