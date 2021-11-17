using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftManagement.Persistencia;

public partial class ResumenEIA_Fichas_ctrFicha3 : System.Web.UI.UserControl
{


    DataSet dsGrilla = new DataSet();
    
    public DateTime Fecha
    {
        get { return DateTime.Now; }
        set { }
    }
    public int IDProyecto
    {

        get
        {
            if (ViewState["IdProyecto"] != null)
                return (int)ViewState["IdProyecto"];
            return -1;
        }

        set { ViewState["IdProyecto"] = value; }


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    public void CargarTodaInfo()
    {
        cargarCombos();
        cargarDatos();
    }

    #region Métodos de inicialización

    protected void cargarCombos()
    {
        cargarComboTipoAcuifero();
        cargarComboGradienteHidraulico();
        cargarComboTipoPtoAgua();
        cargarComboDepartamento();
        cargarComboMunicipio();
        cargarTipoVarClimatica();
        cargarLabAguaSupef();
        cargarTipoMuestra();
        cargarPeriodoMuestra();
        cargarcboCaracteristicasFisicas();
        cargarcboCaracteristicasQuimicas();
        CargarcboMetodoDeter();
        cargarcboCaracteristicasFisicoQuimicas();
        cargarLabAguaSubt();
        cargarFuentAguaSubt();
        cargarTipoOla();
        cargarLabCalAire();
        cargarTipoFuenteRuido();
        cargarSubSecMonitRuido();
        cargarClasTipoEcosistema();
        cargarTipoEcosisTerrestre();
        cargarFuenteInfoEcoterr();
        cargarUnidadArea();
        cargarEscalaTrabajo();
        cargarTipoOleaje();
        cargarEstrucVertDom();
        cargarPosFitoDominante();
        cargarTipoEstrato();
        cargarTipoEspecieFlora();
        cargarTipoEspecieFaunaAnf();
        cargarTipoEspecieFaunaRep();
        cargarTipoEspecieFaunaAve();
        cargarTipoEspecieFaunaMan();
        cargarTipoBiot();
        CargarComboEspeciesInteres();
        CargarComboEcosistema();
        CargarComboUnidadPolAdmin();        
        CargarComboTiposPoblacion();
        CargarComboTipoInstitucion();
    }

    protected void cargarDatos()
    {
        cargarGrillaGeologia();
        cargarGrillaEstabilizacionGeo();
        cargarGrillaGeomorfologia();
        cargarGrillaUnidadHidroPresente(null);
        cargarGrillaPuntosDeAgua(null, null);
        VisualizarFuentesSubterraneasCar();
        VisualizarOlas();
        cargarGrillaMareas();
        cargarGrillaSitMonitCalAire();
        CargarAreasRecarga(true);
        CargarAreasDescarga(true);
        cargarGrillaSuelosMedAbio();
        VisualizarEstacionesMetereo();
        VisualizarVariablesClimaticas();
        cargarGrillaCuencaHidrologia();
        VisualizarFuentesDeAguaSuper();
        VisualizarCaracteristicasSub();
        VisualizarFuentesSubterraneas();
        CargarSisCorriente();
        CargarTipoOleaje();
        VisualizarDetFuentesEmisiones();
        cargarGrillaFuentRuidExist();
        VisualizarResultadoMonitoreo();
        VisualizarGrillaSitMonitRuido();
        VisualizarResultadoRuido();
        VisualizarTipoEcosis();
        VisualizarFuenteInformacion();
        VisualizarCovertVegetal();
        VisualizarDescrFisionomica();
        Visualizar("EIV_INFO_FLORA_ECOTERR", grvInfoFlora);
        Visualizar("EIV_INFO_FAUNA_ECOTERR_AMFIB", grvInfoFaunaAnf);
        Visualizar("EIV_INFO_FAUNA_ECOTERR_REPT", grvInfoFaunaRep);
        Visualizar("EIV_INFO_FAUNA_ECOTERR_AVES", grvInfoFaunaAve);
        Visualizar("EIV_INFO_FAUNA_ECOTERR_MAMIF", grvInfoFaunaMam);
        Visualizar("EIH_SITIO_MUESTREO_ECOSIST_ACUATICO", grvEcoAcuaCont);
        Visualizar("EIV_SIST_LOTICO_ECOACUATICO", grvSistLoticos);
        Visualizar("EIV_SIST_LENTICO_ECOACUATICO", grvSistLenticos);
        Visualizar("EIV_CORALES_ECOMARINO", grvInfCoral);
        Visualizar("EIV_BENTOS_ECOMARINO", grvInfBentos);
        Visualizar("EIV_ZOOPLANCTON_ECOMARINO", grvInfZooplancton);
        Visualizar("EIV_ICTIOFAUNA_ECOMARINO", grvInfIctiofauna);
        Visualizar("EIV_FLORA_ECOMARINO", grvInfFloraMar);
        VisualizarAreaInfluencia();
        Visualizar("EIH_TERR_ETNICO_INFLUENCIA", grvTerrEtnico);
        Visualizar("EIV_DIMENSION_DEMOGRAFICA", grvDimDemo);
        Visualizar("EIV_DIMENSION_DEMOGRAFICA", grvDimDemo2);
        Visualizar("EIV_DIMENSION_ESPACIAL", grvDimEspac);
        Visualizar("EIV_DIMENSION_ECONOMICA", grvDimEcono);
        Visualizar("EIV_HHISTORICOS_AREAINFL_DIRECTA", grvComNoEtn);
        Visualizar("EIV_PERCEP_VAL_CULTURAL", grvPerpCult);
        Visualizar("EIV_RELACIONES_CULTURALES", grvRelCulEnt);
        Visualizar("EIV_INFO_COMUNIDADES_ETNICAS", grvInfoComEtn);
        Visualizar("EIV_INFO_GRUPOS_INDIGENAS", grvInfIndHist);
        Visualizar("EIV_INFO_SITIOS_ARQUEOLOGICOS_ANT", grvSitArqAnt);
        Visualizar("EIV_INFO_SITIOS_ARQUEOLOGICOS", grvSitArqProy);
        Visualizar("EIV_INFORMACION_ACT_AREA_INFDIR", grvActDimPol);
        Visualizar("EIV_INFO_PRES_INSTITUCIONAL_AREAINFDIR", grvInfPresInst);
        Visualizar("EIV_TEND_DESA_SERVICIOS_PUBLICOS", grvInfServPub);
        Visualizar("EIV_TEND_DESA_SERVICIOS_SOCIALES", grvInfServSoc);
        Visualizar("EIV_TEND_DESA_ECONOMICA_CULTURAL", grvInfEcoCul);
        Visualizar("EIV_TEND_DESA_ORGANIZACION_COMUNITARIA", grvInfOrgCom);
        Visualizar("EIH_UNID_ZONIFIC_FISICAS", grvUnidZonFis);
        Visualizar("EIH_UNID_ZONIFIC_BIOTICAS", grvUnidZonBio);
        Visualizar("EIH_UNID_ZONIFIC_SOCIOECONOM", grvUnidZonSocEco);
        Visualizar("EIH_UNID_ZONIFIC_AMBIENTAL", grvUnidZonAmb);
    }

    private void limpiarPlaceHolder(PlaceHolder pl)
    {
        foreach (Control item in pl.Controls)
        {
            switch (item.GetType().ToString())
            {
                case "System.Web.UI.WebControls.TextBox": ((System.Web.UI.WebControls.TextBox)item).Text = string.Empty;
                    break;
                case "System.Web.UI.WebControls.DropDownList": ((System.Web.UI.WebControls.DropDownList)item).SelectedValue = "-1";
                    break;
            }
        }

    }
    
    private void CargarTabla(string NombreTabla)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, NombreTabla);
    }

    private void CargarTabla(string NombreTabla,string NombreCampo,string valor)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@" + NombreCampo, SqlDbType.Int, 10, NombreCampo);
        ParametroCarga.Value = int.Parse(valor);
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, NombreTabla);
    }

    private void Visualizar(string nombreTabla, GridView drvTabla)
    {
        CargarTabla(nombreTabla);
        drvTabla.DataSource = dsGrilla;
        drvTabla.DataBind();
    }

    private void Visualizar(string nombreTabla, string NombreCampo,string valor,GridView drvTabla)
    {
        CargarTabla(nombreTabla,NombreCampo,valor);
        drvTabla.DataSource = dsGrilla;
        drvTabla.DataBind();
    }

    private void Eliminar(string nombreTabla,int index)
    {
        CargarTabla(nombreTabla);
        dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, nombreTabla);
    }

    private void CargarCombo(string campo, string nombreTabla, string id, string value, DropDownList grvTabla)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@" + campo, SqlDbType.Bit, 1, campo);
        ParametroCarga.Value = 1;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, nombreTabla);
        grvTabla.DataSource = dsGrilla;
        grvTabla.DataValueField = id;
        grvTabla.DataTextField = value;
        grvTabla.DataBind();
        grvTabla.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    #endregion

    #region 3.1.1 Geologia

    protected void btnNuevaGeologia_Click(object sender, EventArgs e)
    {
        plhGeologia.Visible = true;
        plhGeologia.Focus();

    }

    protected void btnCancelarGeologia_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhGeomorfologia);
        plhGeologia.Visible = false;
    }

    protected void btnAgregarGeologia_Click(object sender, EventArgs e)
    {
        addRegistroGeologia();
        limpiarPlaceHolder(plhGeomorfologia);
    }

    protected void addRegistroGeologia()
    {
        cargarGrillaGeologia();
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EGM_CODIGO_MAPA"] = txtCodigoMapaGeologia.Text;
        dr["EGM_UNIDADES_LITOLOGICAS"] = txtUnidLitoAreaGeografia.Text;
        dr["EGM_RASGOS_ESTRUCTURALES"] = txtRasgosEstrucGeologia.Text;
        dr["EGM_CARACT_UNIDAD_LITOLOGICA"] = txtCaracUnidLitoGeologia.Text;
        dr["EGM_CARACT_HIDRO_GEOLOGICAS"] = txtCaracHidroGeografia.Text;


        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_GEOLOGIA_MEDIO_ABIOTICO");

        this.txtCodigoMapaGeologia.Text = "";
        this.txtUnidLitoAreaGeografia.Text = "";
        this.txtRasgosEstrucGeologia.Text = "";
        this.txtCaracUnidLitoGeologia.Text = "";
        this.txtCaracHidroGeografia.Text = "";
        cargarGrillaGeologia();
    }

    protected void cargarGrillaGeologia()
    {
        CargarTabla("EIH_GEOLOGIA_MEDIO_ABIOTICO");        
        this.grvGeologia.DataSource = dsGrilla.Tables[0];
        this.grvGeologia.DataBind();
        grvGeologia.Focus();
    }

    protected void grvGeologia_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        eliminarRegistroGeologia(e.RowIndex);
    }
    
    protected void eliminarRegistroGeologia(int index)
    {
        cargarGrillaGeologia();
        this.dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_GEOLOGIA_MEDIO_ABIOTICO");
        cargarGrillaGeologia();
    }

    #endregion

    #region 3.1.2 Geomorfología

    protected void btnNuevaGeomorfologia_Click(object sender, EventArgs e)
    {
        plhGeomorfologia.Visible = true;
        plhGeomorfologia.Focus();

    }

    protected void btnAgregarGeomorfologia_Click(object sender, EventArgs e)
    {
        addRegistroGeomorfologia();
        limpiarPlaceHolder(plhGeomorfologia);
    }

    protected void addRegistroGeomorfologia()
    {
        cargarGrillaGeomorfologia();
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EMM_CODIGO_MAPA"] = txtCodigoMapaGeomorfologia.Text;
        dr["EMM_UNI_GEOMORFOLOGICAS"] = txtUnidadesGeomorfologia.Text;
        dr["EMM_PEND_NATURALES"] = txtPendNaturGeomorfologia.Text;
        dr["EMM_SUCEPT_EROSION"] = txtSuscepErosionGeomorfologia.Text;
        dr["EMM_PROC_MORFODINAMICO"] = txtProcMorfoGeomorfologia.Text;
        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_GEOMORFOLOGIA_MEDIO_ABIOTICO");
        this.txtCodigoMapaGeomorfologia.Text = "";
        this.txtUnidadesGeomorfologia.Text = "";
        this.txtPendNaturGeomorfologia.Text = "";
        this.txtSuscepErosionGeomorfologia.Text = "";
        this.txtProcMorfoGeomorfologia.Text = "";
        cargarGrillaGeomorfologia();

    }

    protected void cargarGrillaGeomorfologia()
    {
        CargarTabla("EIH_GEOMORFOLOGIA_MEDIO_ABIOTICO");        
        this.grvGeomorfologia.DataSource = dsGrilla.Tables[0];
        this.grvGeomorfologia.DataBind();
        grvGeomorfologia.Focus();
    }

    protected void btnCancelarGeomorfologia_Click(object sender, EventArgs e)
    {
        plhGeomorfologia.Visible = false;
        limpiarPlaceHolder(plhGeomorfologia);

    }

    protected void grvGeomorfologia_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarGeomorfologia(e.RowIndex);
    }

    private void EliminarGeomorfologia(int index)
    {
        cargarGrillaGeomorfologia();
        dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_GEOMORFOLOGIA_MEDIO_ABIOTICO");
        cargarGrillaGeomorfologia();
    }

    #endregion

    #region 3.1.3 Estabilidad Geotécnica

    protected void btnNuevaEstGeotecnica_Click(object sender, EventArgs e)
    {
        plhEstGeotecnica.Visible = true;
        plhEstGeotecnica.Focus();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        addRegistroEstabilidadGeo();
    }

    protected void addRegistroEstabilidadGeo()
    {
        cargarGrillaEstabilizacionGeo();
        DataRow dr = dsGrilla.Tables[0].NewRow();

        dr["EIP_ID"] = IDProyecto;
        dr["EEG_CODIGO_MAPA"] = txtCodigoMapaEstGeotecnica.Text;
        dr["EEG_UNIDAD_GEOTECNICA"] = txtUnidGeotecnica.Text;
        dr["EEG_DESC_UNIDAD_GEOTECNICA"] = txtDescUnidGeotecnica.Text;
        dr["EEG_GRADO_ESTABILIDAD"] = cboGradoEstabilidad.SelectedValue.ToString();
        dr["EEG_FECHA_CREACION"] = DateTime.Now;

        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_EST_GEOTECNICA_MEDIO_ABIOTICO");

        txtCodigoMapaEstGeotecnica.Text = "";
        txtUnidGeotecnica.Text = "";
        txtDescUnidGeotecnica.Text = "";
        cboGradoEstabilidad.SelectedIndex = -1;

        cargarGrillaEstabilizacionGeo();
    }

    protected void cargarGrillaEstabilizacionGeo()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIC_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_EST_GEOTECNICA_MEDIO_ABIOTICO");
        this.grvEstGeotecnica.DataSource = dsGrilla.Tables[0];
        this.grvEstGeotecnica.DataBind();
        this.grvEstGeotecnica.Visible = true;

    }

    protected void btnCancelarEstGeotecnica_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhEstGeotecnica);
        plhEstGeotecnica.Visible = false;
    }

    protected void grvEstGeotecnica_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarEstgeotecnico(e.RowIndex);
    }
    protected void EliminarEstgeotecnico(int id)
    {
        cargarGrillaEstabilizacionGeo();
        this.dsGrilla.Tables[0].Rows[id].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_EST_GEOTECNICA_MEDIO_ABIOTICO");
        cargarGrillaEstabilizacionGeo();
    }

    #endregion

    #region  3.1.4.1 Caracteristicas de Unidades Hidrogeológicas Presentes

    protected void cargarComboTipoAcuifero()
    {
        cboTipoAcuifeUnidHidrogeolo.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTiposAcuiferos();
        cboTipoAcuifeUnidHidrogeolo.DataValueField = "ETA_ID";
        cboTipoAcuifeUnidHidrogeolo.DataTextField = "ETA_TIPO_ACUIFERO";
        cboTipoAcuifeUnidHidrogeolo.DataBind();
        cboTipoAcuifeUnidHidrogeolo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void cargarComboGradienteHidraulico()
    {
        cboGradHidraUnidHidrogeolo.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaGradienteHidraulico();
        cboGradHidraUnidHidrogeolo.DataValueField = "EGH_ID";
        cboGradHidraUnidHidrogeolo.DataTextField = "EGH_TIPO_GRADIENTE_HIDRA";
        cboGradHidraUnidHidrogeolo.DataBind();
        cboGradHidraUnidHidrogeolo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnNuevaUnidHidrogeolo_Click(object sender, EventArgs e)
    {
        plhUnidHidrogeolo.Visible = true;
        plhUnidHidrogeolo.Focus();

    }

    protected void btnAgregarUnidHidrogeolo_Click(object sender, EventArgs e)
    {
        addRegistroUnidadHidroGeo();
    }

    protected void addRegistroUnidadHidroGeo()
    {
        cargarGrillaUnidadHidroInsert();
        DataRow dr = dsGrilla.Tables[0].NewRow();

        dr["EIP_ID"] = IDProyecto;
        dr["ECU_FECHA_CREACION"] = Fecha;
        dr["ECU_CODIGO_MAPA"] = txtCodigoMapaUnidHidrogeolo.Text;
        dr["ECU_NOMBRE_UNIDAD_HG"] = txtNombreHidrogeolo.Text;
        dr["ECU_DIRECCION_FLUJO_SUB"] = cboDirFluUnidHidrogeolo.Text;
        dr["ETA_ID"] = cboTipoAcuifeUnidHidrogeolo.SelectedValue;
        dr["EGH_ID"] = cboGradHidraUnidHidrogeolo.SelectedValue;

        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_CARACT_UNID_HG_PRESENTES");

        txtCodigoMapaUnidHidrogeolo.Text = "";
        txtNombreHidrogeolo.Text = "";
        cboDirFluUnidHidrogeolo.SelectedIndex = 0;
        cboTipoAcuifeUnidHidrogeolo.SelectedIndex = -1;
        cboGradHidraUnidHidrogeolo.SelectedIndex = -1;

        cargarGrillaUnidadHidroPresente(null);
    }



    protected void cargarGrillaUnidadHidroInsert()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = -1;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_CARACT_UNID_HG_PRESENTES");
    }

    protected void cargarGrillaUnidadHidroPresente(int? id)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga;
        if (id == null)
        {
            ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
            ParametroCarga.Value = IDProyecto;
            parametrosConsulta.Add(ParametroCarga);
            Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIV_CARACT_UNID_HG_PRESENTES");
            this.grvUnidHidrogeolo.DataSource = dsGrilla.Tables[0];
            this.grvUnidHidrogeolo.DataBind();
            this.grvUnidHidrogeolo.Visible = true;
        }
        else
        {
            ParametroCarga = new SqlParameter("@ECU_ID", SqlDbType.Int, 10, "ECU_ID");
            ParametroCarga.Value = id;
            parametrosConsulta.Add(ParametroCarga);
            Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_CARACT_UNID_HG_PRESENTES");
        }
    }
    protected void btnCancelarUnidHidrogeolo_Click(object sender, EventArgs e)
    {
        plhUnidHidrogeolo.Visible = false;
        limpiarPlaceHolder(plhUnidHidrogeolo);

    }

    protected void grvUnidHidrogeolo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarUnidadHidro(e.RowIndex);
    }

    protected void EliminarUnidadHidro(int id)
    {
        int idGrilla = Convert.ToInt32(grvUnidHidrogeolo.Rows[id].Cells[1].Text);
        cargarGrillaUnidadHidroPresente(idGrilla);
        //cargarGrillaUnidadHidroInsert();//Realiza la Carga de la grilla a partir de la vista
        this.dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_CARACT_UNID_HG_PRESENTES");
        cargarGrillaUnidadHidroPresente(null);
    }

    #endregion

    #region 3.1.4.2 Puntos de Agua en Área de Influencia

    protected void cargarComboTipoPtoAgua()
    {
        cboTipoPtoAguaAreaInf.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoPtoAgua();
        cboTipoPtoAguaAreaInf.DataValueField = "ETP_ID";
        cboTipoPtoAguaAreaInf.DataTextField = "ETP_TIPO_PTO_AGUA";
        cboTipoPtoAguaAreaInf.DataBind();
        cboTipoPtoAguaAreaInf.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnNuevaPtoAguaAreaInf_Click(object sender, EventArgs e)
    {
        plhPtoAguaAreaInf.Visible = true;
        plhPtoAguaAreaInf.Focus();

    }

    protected void btnAgregarPtoAguaAreaInf_Click(object sender, EventArgs e)
    {
        addRegistroPuntosAgua();
    }

    protected void addRegistroPuntosAgua()
    {
        if (!ctrCoorPtoAguaAreaInf.Valido)
            return;
        cargarGrillaPuntosDeAgua(0, null);
        DataRow dr = dsGrilla.Tables[0].NewRow();

        dr["ETP_ID"] = cboTipoPtoAguaAreaInf.SelectedValue.ToString();
        dr["EIP_ID"] = IDProyecto;
        dr["EAA_FECHA_CREACION"] = Fecha;
        dr["EAA_COOR_NORTE_UBICACION"] = ctrCoorPtoAguaAreaInf.CoorNorte;
        dr["EAA_COOR_ESTE_UBICACION"] = ctrCoorPtoAguaAreaInf.CoorEste;
        dr["EAA_NIVEL_PIEZOMETRICO"] = txtNivPiezoPtoAguaAreaInf.Text;
        dr["EAA_UNID_ACUIF_CAPTA"] = txtCaudalPtoAguaAreaInf.Text;
        dr["EAA_CAUDAL"] = txtUnidAcuiPtoAguaAreaInf.Text;
        dr["EAA_USO"] = txtUsoPtoAguaAreaInf.Text;

        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_PTO_AGUA_AREA_INFLUENCIA");

        cboTipoPtoAguaAreaInf.SelectedIndex = -1;
        ctrCoorPtoAguaAreaInf.CoorNorte = "";
        ctrCoorPtoAguaAreaInf.CoorEste = "";
        txtNivPiezoPtoAguaAreaInf.Text = "";
        txtCaudalPtoAguaAreaInf.Text = ""; ;
        txtUnidAcuiPtoAguaAreaInf.Text = "";
        txtUsoPtoAguaAreaInf.Text = "";

        cargarGrillaPuntosDeAgua(null, null);
    }

    protected void cargarGrillaPuntosDeAgua(int? noAplicaVista, int? idBusqueda)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga;
        if (noAplicaVista == null)
        {
            ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
            ParametroCarga.Value = IDProyecto;
            parametrosConsulta.Add(ParametroCarga);
            Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIV_PTO_AGUA_AREA_INFLUENCIA");
            grvPtoAguaAreaInf.DataSource = dsGrilla;
            grvPtoAguaAreaInf.DataBind();
        }
        else
        {
            if (idBusqueda == null)
            {
                ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
                ParametroCarga.Value = IDProyecto;
                parametrosConsulta.Add(ParametroCarga);
                Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_PTO_AGUA_AREA_INFLUENCIA");
            }
            else
            {
                ParametroCarga = new SqlParameter("@EAA_ID", SqlDbType.Int, 10, "EAA_ID");
                ParametroCarga.Value = idBusqueda;
                parametrosConsulta.Add(ParametroCarga);
                Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_PTO_AGUA_AREA_INFLUENCIA");

            }
        }

        grvPtoAguaAreaInf.Visible = true;

    }

    protected void btnCancelarPtoAguaAreaInf_Click(object sender, EventArgs e)
    {
        plhPtoAguaAreaInf.Visible = false;
        limpiarPlaceHolder(plhPtoAguaAreaInf);
    }

    protected void grvPtoAguaAreaInf_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarPtoAguaAreaInf(e.RowIndex);
    }

    protected void EliminarPtoAguaAreaInf(int id)
    {
        int idGrilla = Convert.ToInt32(grvPtoAguaAreaInf.Rows[id].Cells[1].Text);
        cargarGrillaPuntosDeAgua(0, idGrilla);
        this.dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_PTO_AGUA_AREA_INFLUENCIA");
        cargarGrillaPuntosDeAgua(null, null);
    }

    #endregion

    #region 3.1.4.2.1 Areas de Recarga

    protected void btnNuevaAreaRecarga_Click(object sender, EventArgs e)
    {
        plhAreasRecarga.Visible = true;
        plhAreasRecarga.Focus();
    }

    protected void btnCancelarAreasRecarga_Click(object sender, EventArgs e)
    {
        plhAreasRecarga.Visible = false;

    }

    protected void btnAgregarAreasRecarga_Click(object sender, EventArgs e)
    {
        if (this.ctrCoorAreasRecarga.Validar(1, 5))
        {
            GuardarAreasRecarga();
            this.ctrCoorAreasRecarga.LimpiarObjetos();
        }
    }

    private void GuardarAreasRecarga()
    {

        CargarAreasRecarga(false);
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EAR_FECHA_CREACION"] = Fecha;
        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_AREA_RECARGA");

        CargarAreasRecarga(false);

        GuardarCoordenadasAreaRecarga(dsGrilla.Tables[0].Rows[dsGrilla.Tables[0].Rows.Count - 1]["EAR_ID"].ToString());

        CargarAreasRecarga(true);

    }

    private void GuardarCoordenadasAreaRecarga(string earId)
    {
        CargarCoordenadasAreaRecarga(earId);
        foreach (DataRow row in this.ctrCoorAreasRecarga.Coordenadas.Rows)
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAR_ID"] = earId;
            dr["ECR_COOR_NORTE"] = row["CoorNorte"].ToString();
            dr["ECR_COOR_ESTE"] = row["CoorEste"].ToString();
            dsGrilla.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsGrilla, "EIH_COOR_AREA_RECARG");
    }

    private void CargarCoordenadasAreaRecarga(string earId)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga;
        ParametroCarga = new SqlParameter("@EAR_ID", SqlDbType.Int, 10, "EAR_ID");
        ParametroCarga.Value = int.Parse(earId);
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_COOR_AREA_RECARG");

    }

    private void CargarAreasRecarga(bool gridVisible)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga;
        ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_AREA_RECARGA");
        if (gridVisible)
        {
            this.grvAreasRecarga.DataSource = dsGrilla;
            this.grvAreasRecarga.DataBind();
        }

    }
    protected void grvAreasRecarga_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarAreaRecarga(e.RowIndex);
    }
    private void EliminarAreaRecarga(int index)
    {
        Label lblEarId = (Label)this.grvAreasRecarga.Rows[index].FindControl("lblEarId");
        string earId = lblEarId.Text;
        CargarCoordenadasAreaRecarga(earId);
        DataTable temp = dsGrilla.Tables[0];
        int i = 0;
        for (i = temp.Rows.Count - 1; i >= 0; i--)
        {
            dsGrilla.Tables[0].Rows[i].Delete();
        }
        Contexto.guardarTabla(dsGrilla, "EIH_COOR_AREA_RECARG");

        CargarAreasRecarga(false);

        dsGrilla.Tables[0].Rows[index].Delete();

        Contexto.guardarTabla(dsGrilla, "EIH_AREA_RECARGA");

        CargarAreasRecarga(true);

    }

    #endregion

    #region 3.1.4.2.2 Areas de Descarga

    protected void btnNuevaAreaDescarga_Click(object sender, EventArgs e)
    {
        plhAreasDescarga.Visible = true;
        plhAreasDescarga.Focus();

    }

    protected void btnAgregarAreasdescarga_Click(object sender, EventArgs e)
    {
        if (this.ctrCoorAreasDescarga.Validar(1, 5))
        {
            GuardarAreasDescarga();
            this.ctrCoorAreasDescarga.LimpiarObjetos();
        }
    }

    private void GuardarAreasDescarga()
    {
        CargarAreasDescarga(false);
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EAD_FECHA_CREACION"] = Fecha;
        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_AREA_DESCARGA");

        CargarAreasDescarga(false);

        GuardarCoordenadasAreaDescarga(dsGrilla.Tables[0].Rows[dsGrilla.Tables[0].Rows.Count - 1]["EAD_ID"].ToString());

        CargarAreasDescarga(true);


    }

    private void GuardarCoordenadasAreaDescarga(string eadId)
    {
        CargarCoordenadasAreaDescarga(eadId);
        foreach (DataRow row in this.ctrCoorAreasDescarga.Coordenadas.Rows)
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAD_ID"] = eadId;
            dr["ECD_COOR_NORTE"] = row["CoorNorte"].ToString();
            dr["ECD_COOR_ESTE"] = row["CoorEste"].ToString();
            dsGrilla.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsGrilla, "EIH_COOR_AREA_DESC");
    }

    private void CargarCoordenadasAreaDescarga(string eadId)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga;
        ParametroCarga = new SqlParameter("@EAD_ID", SqlDbType.Int, 10, "EAD_ID");
        ParametroCarga.Value = int.Parse(eadId);
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_COOR_AREA_DESC");

    }

    private void CargarAreasDescarga(bool gridVisible)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga;
        ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_AREA_DESCARGA");
        if (gridVisible)
        {
            this.grvAreasDescarga.DataSource = dsGrilla;
            this.grvAreasDescarga.DataBind();
        }
    }

    protected void btnCancelarAreasDescarga_Click(object sender, EventArgs e)
    {
        plhAreasDescarga.Visible = false;

    }

    protected void grvAreasDescarga_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarAreaDescarga(e.RowIndex);
    }
    private void EliminarAreaDescarga(int index)
    {
        Label lblEadId = (Label)this.grvAreasDescarga.Rows[index].FindControl("lblEadId");
        string eadId = lblEadId.Text;
        CargarCoordenadasAreaDescarga(eadId);
        DataTable temp = dsGrilla.Tables[0];
        int i = 0;
        for (i = temp.Rows.Count - 1; i >= 0; i--)
        {
            dsGrilla.Tables[0].Rows[i].Delete();
        }
        Contexto.guardarTabla(dsGrilla, "EIH_COOR_AREA_DESC");

        CargarAreasDescarga(false);

        dsGrilla.Tables[0].Rows[index].Delete();

        Contexto.guardarTabla(dsGrilla, "EIH_AREA_DESCARGA");

        CargarAreasDescarga(true);
    }
    #endregion

    #region 3.1.5 Suelos

    protected void btnNuevaSuelosMedAbio_Click(object sender, EventArgs e)
    {
        plhSuelosMedAbio.Visible = true;
        plhSuelosMedAbio.Focus();

    }
    protected void btnCancelarSuelosMedAbio_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhSuelosMedAbio);
        plhSuelosMedAbio.Visible = false;
    }

    protected void btnAgregarSuelosMedAbio_Click(object sender, EventArgs e)
    {
        addRegistroSuelosMedAbio();
    }
    protected void cargarGrillaSuelosMedAbio()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_SUELOS_MEDIO_ABIOTICO");
        this.grvSuelosMedAbio.DataSource = dsGrilla.Tables[0];
        this.grvSuelosMedAbio.DataBind();
    }

    protected void addRegistroSuelosMedAbio()
    {
        cargarGrillaSuelosMedAbio();
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["ESM_CODIGO_MAPA"] = txtCodigoMapaSuelosMedAbio.Text;
        dr["ESM_UNIDAD_SUELO"] = txtUnidadesSuelosMedAbio.Text;
        dr["ESM_CLASI_AGROLOGICA"] = txtClaAgroSuelosMedAbio.Text;
        dr["ESM_DESCR_UNIDAD"] = txtDescUnidadSuelosMedAbio.Text;
        dr["ESM_PRINC_USO_SUELO"] = txtPricUsoSuelosMedAbio.Text;
        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_SUELOS_MEDIO_ABIOTICO");
        this.txtCodigoMapaSuelosMedAbio.Text = "";
        this.txtUnidadesSuelosMedAbio.Text = "";
        this.txtClaAgroSuelosMedAbio.Text = "";
        this.txtDescUnidadSuelosMedAbio.Text = "";
        this.txtPricUsoSuelosMedAbio.Text = "";
        cargarGrillaSuelosMedAbio();
    }

    protected void grvSuelosMedAbio_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        eliminarRegistroSuelosMedAbio(e.RowIndex);
    }

    protected void eliminarRegistroSuelosMedAbio(int index)
    {
        cargarGrillaSuelosMedAbio();
        this.dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_SUELOS_MEDIO_ABIOTICO");
        cargarGrillaSuelosMedAbio();
    }

    #endregion

    #region 3.1.6.1 Estaciones Hidrometeorológicas

    protected void cboDeptoEstMetereoClima_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarComboMunicipio();
    }

    protected void cargarComboDepartamento()
    {
        cboDeptoEstMetereoClima.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaDepartamento();
        cboDeptoEstMetereoClima.DataValueField = "DEP_ID";
        cboDeptoEstMetereoClima.DataTextField = "DEP_NOMBRE";
        cboDeptoEstMetereoClima.DataBind();
        cboDeptoEstMetereoClima.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void cargarComboMunicipio()
    {
        int idDepto = -1;
        int.TryParse(cboDeptoEstMetereoClima.SelectedValue.ToString(), out idDepto);
        cboMunicEstMetereoClima.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaMunicipio(idDepto);
        cboMunicEstMetereoClima.DataValueField = "MUN_ID";
        cboMunicEstMetereoClima.DataTextField = "MUN_NOMBRE";
        cboMunicEstMetereoClima.DataBind();
        cboMunicEstMetereoClima.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnNuevaEstMetereoClima_Click(object sender, EventArgs e)
    {
        plhEstMetereoClima.Visible = true;
        plhEstMetereoClima.Focus();
    }

    protected void btnCancelarEstMetereoClima_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhEstMetereoClima);
        plhEstMetereoClima.Visible = false;
        this.ctrCoorEstMetereoClima.CoorNorte = "";
        this.ctrCoorEstMetereoClima.CoorEste = "";
    }

    protected void btnAgregarEstMetereoClima_Click(object sender, EventArgs e)
    {
        if (this.ctrCoorEstMetereoClima.Valido)
        {
            GuardarEstacionesMetereo();
            limpiarPlaceHolder(plhEstMetereoClima);
            this.ctrCoorEstMetereoClima.CoorNorte = "";
            this.ctrCoorEstMetereoClima.CoorEste = "";
        }

    }

    private void GuardarEstacionesMetereo()
    {
        CargarEstacionesMetereo();
        DataRow dr = dsGrilla.Tables[0].NewRow();

        dr["EIP_ID"] = IDProyecto;
        dr["EEH_FECHA_CREACION"] = Fecha;
        dr["EEH_COD_ESTACION"] = this.txtCodigoEstMetereoClima.Text;
        dr["EEH_NOMBRE_ESTACION"] = this.txtNombreEstMetereoClima.Text;
        dr["EEH_TIPO_ESTACION"] = this.txtTipoEstMetereoClima.Text;
        dr["DEP_ID"] = this.cboDeptoEstMetereoClima.SelectedValue;
        dr["MUN_ID"] = this.cboMunicEstMetereoClima.SelectedValue;
        dr["EEH_CORRIENTE"] = this.txtCorrienteEstMetereoClima.Text;
        dr["EEH_COOR_NORTE_UBICACION"] = this.ctrCoorEstMetereoClima.CoorNorte;
        dr["EEH_COOR_ESTE_UBICACION"] = this.ctrCoorEstMetereoClima.CoorEste;
        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_ESTACIONES_HIDRO");

        VisualizarEstacionesMetereo();


    }

    private void VisualizarEstacionesMetereo()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIV_ESTACIONES_HIDRO");
        this.grvEstMetereoClima.DataSource = dsGrilla;
        this.grvEstMetereoClima.DataBind();
    }

    private void CargarEstacionesMetereo()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_ESTACIONES_HIDRO");
    }

    protected void grvEstMetereoClima_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarEstacionesMetereo(e.RowIndex);
    }
    private void EliminarEstacionesMetereo(int index)
    {
        CargarEstacionesMetereo();

        dsGrilla.Tables[0].Rows[index].Delete();

        Contexto.guardarTabla(dsGrilla, "EIH_ESTACIONES_HIDRO");

        VisualizarEstacionesMetereo();
    }

    #endregion

    #region 3.1.6.2 Variable Climáticas

    protected void cargarTipoVarClimatica()
    {

        cboVariableClima.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoVarClimatica();
        cboVariableClima.DataValueField = "ETV_ID";
        cboVariableClima.DataTextField = "ETV_TIPO_VAR_CLIMATICA";
        cboVariableClima.DataBind();
        cboVariableClima.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnNuevaVarClima_Click(object sender, EventArgs e)
    {
        plhVarClima.Visible = true;
        plhVarClima.Focus();
    }
    protected void btnCancelarVarClima_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhVarClima);
        plhVarClima.Visible = false;
    }

    protected void btnAgregarVarClima_Click(object sender, EventArgs e)
    {
        GuardarVariablesClimaticas();
        limpiarPlaceHolder(plhVarClima);
    }

    private void GuardarVariablesClimaticas()
    {
        CargarVariablesClimaticas();
        DataRow dr = dsGrilla.Tables[0].NewRow();

        dr["EIP_ID"] = IDProyecto;
        dr["EVC_FECHA_CREACION"] = Fecha;
        dr["ETV_ID"] = this.cboVariableClima.SelectedValue;
        dr["EVC_VALOR_MAXIMO"] = this.txtValMaxVarClima.Text;
        dr["EVC_MES_VALORES_MAXIMOS"] = this.txtMesValMaxVarClima.Text;
        dr["EVC_VALOR_MINIMO"] = this.txtValMinVarClima.Text;
        dr["EVC_MES_VALORES_MINIMOS"] = this.txtMesValMinVarClima.Text;
        dr["EVC_PROM_MULTIANUAL"] = this.txtPromMultiaVarClima.Text;

        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_VARIABLES_CLIMATICAS");

        VisualizarVariablesClimaticas();

    }

    private void VisualizarVariablesClimaticas()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIV_VARIABLES_CLIMATICAS");

        this.grvVarClima.DataSource = dsGrilla;
        this.grvVarClima.DataBind();

    }

    private void CargarVariablesClimaticas()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_VARIABLES_CLIMATICAS");
    }

    protected void grvVarClima_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarVariablesClimaticas(e.RowIndex);
    }
    private void EliminarVariablesClimaticas(int index)
    {
        CargarVariablesClimaticas();

        dsGrilla.Tables[0].Rows[index].Delete();

        Contexto.guardarTabla(dsGrilla, "EIH_VARIABLES_CLIMATICAS");

        VisualizarVariablesClimaticas();
    }
    #endregion

    #region 3.1.7.1 Cuenca 1


    //  //Medio Abiotico - Hidrología cuencas
    protected void btnNuevaCuencaHidrologia_Click(object sender, EventArgs e)
    {
        plhCuencaHidrologia.Visible = true;
        plhCuencaHidrologia.Focus();

    }
    protected void btnCancelarCuencaHidrologia_Click(object sender, EventArgs e)
    {
        plhCuencaHidrologia.Visible = false;

    }

    protected void btnAgregarCuencaHidrologia_Click(object sender, EventArgs e)
    {
        addRegistroCuencaHidrologia();
    }
    protected void cargarGrillaCuencaHidrologia()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIC_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_CUENCA");
        this.grvCuencaHidrologia.DataSource = dsGrilla.Tables[0];
        this.grvCuencaHidrologia.DataBind();
    }

    protected void addRegistroCuencaHidrologia()
    {
        cargarGrillaCuencaHidrologia();
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["ECH_NOMBRE_CUENCA"] = txtNombreCuencaHidrologia.Text;
        dr["ECH_AREA_CUENCA"] = txtAreaCuencaHidrologia.Text;
        dr["ECH_USO_PRINCIPAL"] = txtUsoPricCuencaHidrologia.Text;
        dr["ECH_NO_ORDEN_CORR_PRINC"] = txtNoOrdenRedDrenaje.Text;
        dr["ECH_DENS_DRENAJE_CUENCA_INT"] = txtDensidadRedDrenaje.Text;
        dr["ECH_TIPO_RED_DRENAJE"] = txtTipoRedDrenaje.Text;
        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_CUENCA");
        this.txtNombreCuencaHidrologia.Text = "";
        this.txtAreaCuencaHidrologia.Text = "";
        this.txtUsoPricCuencaHidrologia.Text = "";
        cargarGrillaCuencaHidrologia();
    }

    protected void grvCuencaHidrologia_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        eliminarRegistroCuencaHidrologia(e.RowIndex);
    }

    protected void eliminarRegistroCuencaHidrologia(int index)
    {
        cargarGrillaCuencaHidrologia();
        this.dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_CUENCA");
        cargarGrillaCuencaHidrologia();
    }
    #endregion

    #region 3.1.8.1 Fuentes de Agua Superficiales

    protected void cargarLabAguaSupef()
    {

        cboNombLabFuentAguaSupef.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaLaboratorio(1);
        cboNombLabFuentAguaSupef.DataValueField = "ELC_ID";
        cboNombLabFuentAguaSupef.DataTextField = "ELC_LABORATORIO_EST_CALIDAD";
        cboNombLabFuentAguaSupef.DataBind();
        cboNombLabFuentAguaSupef.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnNuevaFuentAguaSupef_Click(object sender, EventArgs e)
    {
        plhFuentAguaSupef.Visible = true;
        plhFuentAguaSupef.Focus();
    }

    protected void btnCancelarFuentAguaSupef_Click(object sender, EventArgs e)
    {
        plhFuentAguaSupef.Visible = false;
        limpiarPlaceHolder(plhFuentAguaSupef);
        this.ctrCoorFuentAguaSupef.CoorNorte = "";
        this.ctrCoorFuentAguaSupef.CoorEste = "";
    }

    protected void btnAgregarFuentAguaSupef_Click(object sender, EventArgs e)
    {
        if (this.ctrCoorFuentAguaSupef.Valido)
        {
            GuardarFuentesDeAguaSuper();
            limpiarPlaceHolder(plhFuentAguaSupef);
            this.ctrCoorFuentAguaSupef.CoorNorte = "";
            this.ctrCoorFuentAguaSupef.CoorEste = "";
        }
    }

    private void GuardarFuentesDeAguaSuper()
    {
        CargarFuentesDeAguaSuper();

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["ELC_ID"] = this.cboNombLabFuentAguaSupef.SelectedValue;
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();

            dr["EIP_ID"] = IDProyecto;
            dr["ECS_FECHA_CREACION"] = Fecha;
            dr["ELC_ID"] = this.cboNombLabFuentAguaSupef.SelectedValue;

            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_CALIDAD_FUENTES_AGUA_SUPRF");

        CargarFuentesDeAguaSuper();

        string ecsId = dsGrilla.Tables[0].Rows[dsGrilla.Tables[0].Rows.Count - 1]["ECS_ID"].ToString();
        CargarFuentesDeAguaSuperPuntos(ecsId, null);

        DataRow dr2 = dsGrilla.Tables[0].NewRow();

        dr2["ECS_ID"] = int.Parse(ecsId);
        dr2["EPM_ID"] = DBNull.Value;
        dr2["ETM_ID"] = DBNull.Value;
        dr2["EPS_ID_NOMBRE_FUENTE"] = this.txtNombreFuentAguaSupef.Text;
        dr2["EPS_COOR_NORTE_UBICACION"] = this.ctrCoorFuentAguaSupef.CoorNorte;
        dr2["EPS_COOR_ESTE_UBICACION"] = this.ctrCoorFuentAguaSupef.CoorEste;
        dr2["EPS_FECHA_MUESTREO"] = DBNull.Value;
        dr2["EPS_HORA_MUESTREO"] = DBNull.Value;
        dr2["EPS_DURACION_MUESTREO"] = DBNull.Value;

        dsGrilla.Tables[0].Rows.Add(dr2);

        Contexto.guardarTabla(dsGrilla, "EIH_PUNTOS_MONI_CAL_AGUA_SUPERF");

        VisualizarFuentesDeAguaSuper();

    }

    private void VisualizarFuentesDeAguaSuper()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIV_CALIDAD_FUENTES_AGUA_SUPRF");

        this.grvInvFAgua.DataSource = dsGrilla;
        this.grvInvFAgua.DataBind();

        if (dsGrilla.Tables[0].Rows.Count > 0)
            this.cboNombLabFuentAguaSupef.SelectedValue = dsGrilla.Tables[0].Rows[0]["ELC_ID"].ToString();


        this.cboFuentesSuper.DataSource = dsGrilla;
        cboFuentesSuper.DataValueField = "EPS_ID";
        cboFuentesSuper.DataTextField = "EPS_ID_NOMBRE_FUENTE";
        cboFuentesSuper.DataBind();
        cboFuentesSuper.Items.Insert(0, new ListItem("Seleccione...", "-1"));


        this.cboFuentesCar.DataSource = dsGrilla;
        cboFuentesCar.DataValueField = "EPS_ID";
        cboFuentesCar.DataTextField = "EPS_ID_NOMBRE_FUENTE";
        cboFuentesCar.DataBind();
        cboFuentesCar.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        this.cboFuentesQuimicas.DataSource = dsGrilla;
        cboFuentesQuimicas.DataValueField = "EPS_ID";
        cboFuentesQuimicas.DataTextField = "EPS_ID_NOMBRE_FUENTE";
        cboFuentesQuimicas.DataBind();
        cboFuentesQuimicas.Items.Insert(0, new ListItem("Seleccione...", "-1"));


    }

    private void CargarFuentesDeAguaSuperPuntos(string ecsId, string epsId)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        if (ecsId != null)
        {
            SqlParameter ParametroCarga = new SqlParameter("@ECS_ID", SqlDbType.Int, 10, "ECS_ID");
            ParametroCarga.Value = int.Parse(ecsId);
            parametrosConsulta.Add(ParametroCarga);
        }
        if (epsId != null)
        {
            SqlParameter ParametroCarga = new SqlParameter("@EPS_ID", SqlDbType.Int, 10, "EPS_ID");
            ParametroCarga.Value = int.Parse(epsId);
            parametrosConsulta.Add(ParametroCarga);
        }
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_PUNTOS_MONI_CAL_AGUA_SUPERF");

    }

    private void CargarFuentesDeAguaSuper()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_CALIDAD_FUENTES_AGUA_SUPRF");
    }


    protected void grvInvFAgua_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarFuentesDeAguaSuper(e.RowIndex);
        VisualizarFuentesDeAguaSuper();
    }
    private void EliminarFuentesDeAguaSuper(int index)
    {
        Label lblEcsId = (Label)this.grvInvFAgua.Rows[index].FindControl("lblEcsId");

        EliminarFuentesDeAguaSuperPuntos(lblEcsId.Text);

        CargarFuentesDeAguaSuper();

        dsGrilla.Tables[0].Rows[index].Delete();

        Contexto.guardarTabla(dsGrilla, "EIH_CALIDAD_FUENTES_AGUA_SUPRF");


    }

    private void EliminarFuentesDeAguaSuperPuntos(string ecsId)
    {
        CargarFuentesDeAguaSuperPuntos(ecsId, null);
        dsGrilla.Tables[0].Rows[0].Delete();

        Contexto.guardarTabla(dsGrilla, "EIH_PUNTOS_MONI_CAL_AGUA_SUPERF");
    }
    #endregion

    #region 3.1.8.1 Fuentes de Agua Superficiales Detalles

    protected void cargarTipoMuestra()
    {
            
        cboTipoMuestraFuentAguaSupef.DataSource= SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoMuestra();
        cboTipoMuestraFuentAguaSupef.DataValueField = "ETM_ID";
        cboTipoMuestraFuentAguaSupef.DataTextField = "ETM_TIPO_MUESTRA";
        cboTipoMuestraFuentAguaSupef.DataBind();
        cboTipoMuestraFuentAguaSupef.Items.Insert(0, new ListItem("Seleccione...", "-1"));


        cboTipoMuestraFuentAguaSubt.DataSource= SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoMuestra();
        cboTipoMuestraFuentAguaSubt.DataValueField = "ETM_ID";
        cboTipoMuestraFuentAguaSubt.DataTextField = "ETM_TIPO_MUESTRA";
        cboTipoMuestraFuentAguaSubt.DataBind();
        cboTipoMuestraFuentAguaSubt.Items.Insert(0, new ListItem("Seleccione...", "-1"));
      
    }
    protected void cargarPeriodoMuestra()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EPM_ACTIVO", SqlDbType.Bit, 1, "EPM_ACTIVO");
        ParametroCarga.Value = 1;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIB_PERIODO_MUESTREO");
        cboPerMuestFuentAguaSupef.DataSource = dsGrilla;
        cboPerMuestFuentAguaSupef.DataValueField = "EPM_ID";
        cboPerMuestFuentAguaSupef.DataTextField = "EPM_PERIODO_MUESTREO";
        cboPerMuestFuentAguaSupef.DataBind();
        cboPerMuestFuentAguaSupef.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboPerMuestFuentAguaSubt.DataSource = dsGrilla;
        cboPerMuestFuentAguaSubt.DataValueField = "EPM_ID";
        cboPerMuestFuentAguaSubt.DataTextField = "EPM_PERIODO_MUESTREO";
        cboPerMuestFuentAguaSubt.DataBind();
        cboPerMuestFuentAguaSubt.Items.Insert(0, new ListItem("Seleccione...", "-1"));        

    }

    protected void cargarcboCaracteristicasFisicas()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EPC_ACTIVO", SqlDbType.Bit, 1, "EPC_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@ETP_ID", SqlDbType.Int, 10, "ETP_ID");
        par2.Value = 0;
        parametros.Add(par2);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsGrilla, "EIB_PARAMETROS_EST_CALIDAD");

        this.cboCaracterFisic.DataSource = dsGrilla;
        this.cboCaracterFisic.DataTextField = "EPC_PARAMETROS_EST_CALIDAD";
        this.cboCaracterFisic.DataValueField = "EPC_ID";
        this.cboCaracterFisic.DataBind();
        this.cboCaracterFisic.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }
    

    protected void cargarcboCaracteristicasQuimicas()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EPC_ACTIVO", SqlDbType.Bit, 1, "EPC_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@ETP_ID", SqlDbType.Int, 10, "ETP_ID");
        par2.Value = 0;
        parametros.Add(par2);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsGrilla, "EIB_PARAMETROS_EST_CALIDAD");

        this.cboCaracteristicasQuimicas.DataSource = dsGrilla;
        this.cboCaracteristicasQuimicas.DataTextField = "EPC_PARAMETROS_EST_CALIDAD";
        this.cboCaracteristicasQuimicas.DataValueField = "EPC_ID";
        this.cboCaracteristicasQuimicas.DataBind();
        this.cboCaracteristicasQuimicas.Items.Insert(0, new ListItem("Seleccione..", "-1"));


        this.cboCaracQuimSub.DataSource = dsGrilla;
        this.cboCaracQuimSub.DataTextField = "EPC_PARAMETROS_EST_CALIDAD";
        this.cboCaracQuimSub.DataValueField = "EPC_ID";
        this.cboCaracQuimSub.DataBind();
        this.cboCaracQuimSub.Items.Insert(0, new ListItem("Seleccione..", "-1"));
        
    }

    private void CargarcboMetodoDeter()
    {
        DataSet dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EMC_ACTIVO", SqlDbType.Bit, 1, "EMC_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_METODOS_DET_CALIDAD");

        cboMetodoDetermin.DataSource = dsDatos;
        cboMetodoDetermin.DataTextField = "EMC_METODOS_DET_CALIDAD";
        cboMetodoDetermin.DataValueField = "EMC_ID";
        cboMetodoDetermin.DataBind();
        cboMetodoDetermin.Items.Insert(0, new ListItem("Seleccione..", "-1"));

        cboMetDetQuim.DataSource = dsDatos;
        cboMetDetQuim.DataTextField = "EMC_METODOS_DET_CALIDAD";
        cboMetDetQuim.DataValueField = "EMC_ID";
        cboMetDetQuim.DataBind();
        cboMetDetQuim.Items.Insert(0, new ListItem("Seleccione..", "-1"));

        cboMetDeterQuimSub.DataSource = dsDatos;
        cboMetDeterQuimSub.DataTextField = "EMC_METODOS_DET_CALIDAD";
        cboMetDeterQuimSub.DataValueField = "EMC_ID";
        cboMetDeterQuimSub.DataBind();
        cboMetDeterQuimSub.Items.Insert(0, new ListItem("Seleccione..", "-1"));

        cboMetDeterm.DataSource = dsDatos;
        cboMetDeterm.DataTextField = "EMC_METODOS_DET_CALIDAD";
        cboMetDeterm.DataValueField = "EMC_ID";
        cboMetDeterm.DataBind();
        cboMetDeterm.Items.Insert(0, new ListItem("Seleccione..", "-1"));       
        

    }

    protected void btnNuevaFuentAguaSupefDet_Click(object sender, EventArgs e)
    {
        this.plhDetallesFuentesSup.Visible = true;
    }

    protected void btnCancelarFuentAguaDetSupef_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhDetallesFuentesSup);
        this.plhDetallesFuentesSup.Visible = false;
    }

    protected void btnAgregarFuentAguaDetSupef_Click(object sender, EventArgs e)
    {
        GuardarDetallesFuentesDeAguaSuperDet();
    }

    private void GuardarDetallesFuentesDeAguaSuperDet()
    {
        CargarFuentesDeAguaSuperPuntos(null, this.cboFuentesSuper.SelectedValue);

        dsGrilla.Tables[0].Rows[0]["EPM_ID"] = this.cboPerMuestFuentAguaSupef.SelectedValue;
        dsGrilla.Tables[0].Rows[0]["ETM_ID"] = this.cboTipoMuestraFuentAguaSupef.SelectedValue;
        dsGrilla.Tables[0].Rows[0]["EPS_FECHA_MUESTREO"] = this.txtFechaMuestFuentAguaSupef.Text;
        dsGrilla.Tables[0].Rows[0]["EPS_HORA_MUESTREO"] = this.txtHoraMuestFuentAguaSupef.Text;
        dsGrilla.Tables[0].Rows[0]["EPS_DURACION_MUESTREO"] = this.txtDurMuestFuentAguaSupef.Text;

        Contexto.guardarTabla(dsGrilla, "EIH_PUNTOS_MONI_CAL_AGUA_SUPERF");

    }


    protected void btnAgregarCaracSub_Click(object sender, EventArgs e)
    {
        GuardarCaracteristicasSub();
        limpiarPlaceHolder(plhCarFisFuenSupp);
    }

    private void GuardarCaracteristicasSub()
    {
        string idFuente = this.cboFuentesCar.SelectedValue;
        string txtValorFuente = this.cboFuentesCar.SelectedValue;

        GuardarCarCaracteristicas(this.cboFuentesCar.SelectedValue, this.cboCaracterFisic.SelectedValue, this.cboMetodoDetermin.SelectedValue, this.txtLimitedeDeteccion.Text, this.txtValorFuente.Text);

    }


    private void VisualizarCaracteristicasSub()
    {
        SILPA.LogicaNegocio.ResumenEIA.Procedimientos objPro = new SILPA.LogicaNegocio.ResumenEIA.Procedimientos();        
        grvCalidadFuentesSuper.DataSource = objPro.ProcCalidadFuentesSuperf(IDProyecto.ToString());
        grvCalidadFuentesSuper.DataBind();

    }

    private void GuardarCarCaracteristicas(string epsId, string epcId,string emcId,string limiteDeteccion,string valor)
    {
        CargarFuentesDeAguaSuperPuntos(null, epsId);
        string ecsId = dsGrilla.Tables[0].Rows[0]["ECS_ID"].ToString();
        CargarCarCarSub(ecsId, epcId);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EMC_ID"] = int.Parse(emcId);
            dsGrilla.Tables[0].Rows[0]["EFM_LIMITE_DETEC"] = limiteDeteccion;
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["ECS_ID"] = int.Parse(ecsId);
            dr["EPC_ID"] = int.Parse(epcId);
            dr["EMC_ID"] = int.Parse(emcId);
            dr["EFM_LIMITE_DETEC"] = limiteDeteccion;
            dsGrilla.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsGrilla, "EIH_PARAM_MUESTREO_SUPERF");
        CargarCarCarSub(ecsId, epcId);

        GuardarCarCarCarSub(epsId, dsGrilla.Tables[0].Rows[0]["EFM_ID"].ToString(),valor);

    }
    private void GuardarCarCarCarSub(string epsId, string efmId,string valor)
    {
        CargarCarCarCarSub(efmId, epsId);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EPM_VALOR"] = valor;
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EPS_ID"] = epsId;
            dr["EFM_ID"] = efmId;
            dr["EPM_VALOR"] = valor;
            dsGrilla.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsGrilla, "EIH_PARAM_MUESTREO_SUPERF_FUENTES");
    }
    private void CargarCarCarCarSub(string efmId, string epsId)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EFM_ID", SqlDbType.Int, 10, "EFM_ID");
        par.Value = int.Parse(efmId);
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@EPS_ID", SqlDbType.Int, 10, "EPS_ID");
        par2.Value = int.Parse(epsId);
        parametros.Add(par2);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsGrilla, "EIH_PARAM_MUESTREO_SUPERF_FUENTES");
    }


    private void CargarCarCarSub(string ecsId, string epcId)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ECS_ID", SqlDbType.Int, 10, "ECS_ID");
        par.Value = int.Parse(ecsId);
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@EPC_ID", SqlDbType.Int, 10, "EPC_ID");
        par2.Value = int.Parse(epcId);
        parametros.Add(par2);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsGrilla, "EIH_PARAM_MUESTREO_SUPERF");

    }

    protected void btnAgregarCarQui_Click(object sender, EventArgs e)
    {
        GuardarCaracteristicasQui();
        limpiarPlaceHolder(plhCarQuimFuenSupp);
    }

    private void GuardarCaracteristicasQui()
    {
        GuardarCarCaracteristicas(this.cboFuentesQuimicas.SelectedValue, this.cboCaracteristicasQuimicas.SelectedValue, this.cboMetDetQuim.SelectedValue, this.txtLimiteDetec.Text, this.txtValorCarQui.Text);
    }

  

    protected void btnCancelarCarQui_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhCarQuimFuenSupp);
        this.plhCarQuimFuenSupp.Visible = false;
    }

    protected void grvCalidadFuentesSuper_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 1)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
        //e.Row.Cells[3].Visible = false;        
    }

    protected void btnCarFisFuenSupp_Click(object sender, EventArgs e)
    {
        this.plhCarFisFuenSupp.Visible = true;
    }


    protected void btnCancelarCarcSub_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(this.plhCarFisFuenSupp);
        this.plhCarFisFuenSupp.Visible = false;
    }

    protected void btnCarQuimFuenSupp_Click(object sender, EventArgs e)
    {
        this.plhCarQuimFuenSupp.Visible = true;
    }

    #endregion

    #region 3.1.8.1 Fuentes de Aguas Subterraneas

    protected void cargarLabAguaSubt()
    {

        cboNombLabFuentAguaSubt.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaLaboratorio(2);
        cboNombLabFuentAguaSubt.DataValueField = "ELC_ID";
        cboNombLabFuentAguaSubt.DataTextField = "ELC_LABORATORIO_EST_CALIDAD";
        cboNombLabFuentAguaSubt.DataBind();
        cboNombLabFuentAguaSubt.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void cargarFuentAguaSubt()
    {

        cboTipoFuenteFuentAguaSubt.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoFuentAguaSubt();
        cboTipoFuenteFuentAguaSubt.DataValueField = "ETS_ID";
        cboTipoFuenteFuentAguaSubt.DataTextField = "ETS_TIPO_AGUA_SUBT";
        cboTipoFuenteFuentAguaSubt.DataBind();
        cboTipoFuenteFuentAguaSubt.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
   
    protected void btnNuevaFuentAguaSubt_Click(object sender, EventArgs e)
    {
        plhFuentAguaSubt.Visible = true;
        plhFuentAguaSubt.Focus();

    }
    protected void btnCancelarFuentAguaSubt_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhFuentAguaSubt);
        plhFuentAguaSubt.Visible = false;

    }

    protected void btnAgregarFuentAguaSubt_Click(object sender, EventArgs e)
    {
        if (this.ctrCoorFuentAguaSubt.Valido)
        {
            GuardarFuentesSubterraneas();
            this.ctrCoorFuentAguaSubt.CoorEste = "";
            this.ctrCoorFuentAguaSubt.CoorNorte = "";
            limpiarPlaceHolder(plhFuentAguaSubt);
        }
    }

    private void GuardarFuentesSubterraneas()
    {
        CargarFuentesSubterraneas();
        if (dsGrilla.Tables[0].Rows.Count>0)
        {
            dsGrilla.Tables[0].Rows[0]["ELC_ID"]=this.cboNombLabFuentAguaSubt.SelectedValue;
            if (this.txtOtroLabFuentAguaSubt.Text == "")
                dsGrilla.Tables[0].Rows[0]["ELC_OTRO"] = DBNull.Value;
            else
                dsGrilla.Tables[0].Rows[0]["ELC_OTRO"] = this.txtOtroLabFuentAguaSubt.Text;
            

        }
        else
        {
            DataRow dr= dsGrilla.Tables[0].NewRow();
            dr["EIP_ID"]=IDProyecto;
            dr["ECT_FECHA_CREACION"]=Fecha;
            dr["ELC_ID"]=this.cboNombLabFuentAguaSubt.SelectedValue;
            if (this.txtOtroLabFuentAguaSubt.Text == "")
                dr["ELC_OTRO"] = DBNull.Value;
            else
                dr["ELC_OTRO"] = this.txtOtroLabFuentAguaSubt.Text;
            dsGrilla.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsGrilla, "EIH_CALIDAD_FUENTES_AGUA_SUBT");
        CargarFuentesSubterraneas();
        string ectId=dsGrilla.Tables[0].Rows[0]["ECT_ID"].ToString();
        CargarFuentesSubterraneasPuntos(ectId,null);

        DataRow dr2=dsGrilla.Tables[0].NewRow();
        dr2["ECT_ID"]=ectId;
        dr2["ETS_ID"] = cboTipoFuenteFuentAguaSubt.SelectedValue;
        dr2["ETM_ID"] = DBNull.Value;
        dr2["EPM_ID"] = DBNull.Value;
        dr2["EPT_COOR_NORTE_UBICACION"] = this.ctrCoorFuentAguaSubt.CoorNorte;
        dr2["EPT_COOR_ESTE_UBICACION"] = this.ctrCoorFuentAguaSubt.CoorEste;
        dr2["EPT_DESC_PIEZO_DIST_NFREAT"] = this.txtDescPiezoFuentAguaSubt.Text;
        dr2["EPT_USOS"] = this.txtUsosFuentAguaSubt.Text;
        dr2["EPT_FECHA_MUESTREO"] = DBNull.Value;
        dr2["EPT_HORA_MUESTREO"] = DBNull.Value;
        dr2["EPT_DURACION_MUESTREO"] = DBNull.Value;

        dsGrilla.Tables[0].Rows.Add(dr2);
        Contexto.guardarTabla(dsGrilla, "EIH_PUNTOS_MONI_CAL_AGUA_SUBT");

        VisualizarFuentesSubterraneas();
    }

    private void VisualizarFuentesSubterraneas()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIV_CALIDAD_FUENTES_AGUA_SUBT");

        this.grvFuentAguaSubt.DataSource = dsGrilla;
        this.grvFuentAguaSubt.DataBind();

        this.cboFuentesDet.DataSource = dsGrilla;
        cboFuentesDet.DataValueField = "EPT_ID";
        cboFuentesDet.DataTextField = "ETS_TIPO_AGUA_SUBT";
        cboFuentesDet.DataBind();
        cboFuentesDet.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        this.cboFuenteQuimSub.DataSource = dsGrilla;
        cboFuenteQuimSub.DataValueField = "EPT_ID";
        cboFuenteQuimSub.DataTextField = "ETS_TIPO_AGUA_SUBT";
        cboFuenteQuimSub.DataBind();
        cboFuenteQuimSub.Items.Insert(0, new ListItem("Seleccione...", "-1"));





        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            this.cboNombLabFuentAguaSubt.SelectedValue = dsGrilla.Tables[0].Rows[0]["ELC_ID"].ToString();
            if (this.cboNombLabFuentAguaSubt.SelectedItem.Text == "Otro")
            {
                this.trNombreLab2.Visible=true;
                this.txtOtroLabFuentAguaSubt.Text = dsGrilla.Tables[0].Rows[0]["ELC_OTRO"].ToString();
            }
        }



    }

    private void CargarFuentesSubterraneasPuntos(string ectId,string eptId)    
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        if (ectId != null)
        {
            SqlParameter ParametroCarga = new SqlParameter("@ECT_ID", SqlDbType.Int, 10, "ECT_ID");
            ParametroCarga.Value = int.Parse(ectId);
            parametrosConsulta.Add(ParametroCarga);        
        }
        if (eptId != null)
        {
            SqlParameter ParametroCarga = new SqlParameter("@EPT_ID", SqlDbType.Int, 10, "EPT_ID");
            ParametroCarga.Value = int.Parse(eptId);
            parametrosConsulta.Add(ParametroCarga);
        }
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_PUNTOS_MONI_CAL_AGUA_SUBT");
    }

    private void CargarFuentesSubterraneas()
    {
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_CALIDAD_FUENTES_AGUA_SUBT");
        
    }

    protected void grvFuentAguaSubt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarFuentesSubterraneas(e.RowIndex);
    }

    private void EliminarFuentesSubterraneas(int index)
    {
        Label eptId = (Label)this.grvFuentAguaSubt.Rows[index].FindControl("lblEptId");
        CargarFuentesSubterraneasPuntos(null, eptId.Text);
        dsGrilla.Tables[0].Rows[0].Delete();

        Contexto.guardarTabla(dsGrilla, "EIH_PUNTOS_MONI_CAL_AGUA_SUBT");

        VisualizarFuentesSubterraneas();
    }

    protected void cboNombLabFuentAguaSubt_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.trNombreLab2.Visible=false;
        this.txtOtroLabFuentAguaSubt.Text="";
        if (this.cboNombLabFuentAguaSubt.SelectedItem.Text == "Otro")
        {
            this.trNombreLab2.Visible = true;
        }
    }

    #endregion
    
    #region 3.1.8.1 Fuentes de Aguas Subterraneas Detalles

    protected void btnAgregarFuentAguaSubtDet_Click(object sender, EventArgs e)
    {
        GuardarDetallesFuentesSubterraneas();
        VisualizarFuentesSubterraneasCar();
        limpiarPlaceHolder(plhDetallesFuentesSub);
    }

    private void GuardarDetallesFuentesSubterraneas()
    {
        CargarFuentesSubterraneasPuntos(null,this.cboFuentesDet.SelectedValue);

        dsGrilla.Tables[0].Rows[0]["ETM_ID"] = this.cboTipoMuestraFuentAguaSubt.SelectedValue;
        dsGrilla.Tables[0].Rows[0]["EPM_ID"] = this.cboPerMuestFuentAguaSubt.SelectedValue;
        dsGrilla.Tables[0].Rows[0]["EPT_FECHA_MUESTREO"] = this.txtFechaMuestFuentAguaSubt.Text;
        dsGrilla.Tables[0].Rows[0]["EPT_HORA_MUESTREO"] = this.txtHoraMuestFuentAguaSubt.Text;
        dsGrilla.Tables[0].Rows[0]["EPT_DURACION_MUESTREO"] = this.txtDurMuestFuentAguaSubt.Text;

        Contexto.guardarTabla(dsGrilla, "EIH_PUNTOS_MONI_CAL_AGUA_SUBT");

    }

    protected void btnCancelarFuentAguaSubtDet_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhDetallesFuentesSub);
        this.plhDetallesFuentesSub.Visible = false;
    }

    protected void grvCalidadFuentesSub_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 1)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
          //  e.Row.Cells[3].Visible = false;
        }
    }

    protected void btnAgregarCaracQuiSub_Click(object sender, EventArgs e)
    {
        CargarFuentesSubterraneasPuntos(null, this.cboFuenteQuimSub.SelectedValue);
        string ectId = dsGrilla.Tables[0].Rows[0]["ECT_ID"].ToString();
        CargarCarFuentesSup(ectId);
        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EMC_ID"] = this.cboMetDeterQuimSub.SelectedValue;
            dsGrilla.Tables[0].Rows[0]["EFT_LIMITE_DETEC"] = this.txtLimitDeteccSub.Text;
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["ECT_ID"] = ectId;
            dr["EPC_ID"] = this.cboCaracQuimSub.SelectedValue;
            dr["EMC_ID"] = this.cboMetDeterQuimSub.SelectedValue;
            dr["EFT_LIMITE_DETEC"] = this.txtLimitDeteccSub.Text;
            dsGrilla.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsGrilla, "EIH_PARAM_MUESTREO_SUBT");
        CargarCarFuentesSup(ectId);
        string eftId=dsGrilla.Tables[0].Rows[0]["EFT_ID"].ToString();
        CargarValorCarFuentesSub(eftId, this.cboFuenteQuimSub.SelectedValue);
        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EPB_VALOR"] = this.txtValorFuenteQuimSub.Text;
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EFT_ID"] = eftId;
            dr["EPT_ID"] = this.cboFuenteQuimSub.SelectedValue;
            dr["EPB_VALOR"] = this.txtValorFuenteQuimSub.Text;
            dsGrilla.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsGrilla, "EIH_PARAM_MUESTREO_SUBT_FUENTES");
        VisualizarFuentesSubterraneasCar();
    }
    private void VisualizarFuentesSubterraneasCar()
    {
        dsGrilla = new DataSet();
        SILPA.LogicaNegocio.ResumenEIA.Procedimientos objPro = new SILPA.LogicaNegocio.ResumenEIA.Procedimientos();
        drvFuentesSubDet.DataSource = objPro.ProcCalidadFuentesSubt(IDProyecto.ToString());
        drvFuentesSubDet.DataBind();
    }


    private void CargarValorCarFuentesSub(string eftId, string eptId)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EFT_ID", SqlDbType.Int, 10, "EFT_ID");
        ParametroCarga.Value = int.Parse(eftId);
        parametrosConsulta.Add(ParametroCarga);
        SqlParameter ParametroCarga2 = new SqlParameter("@EPT_ID", SqlDbType.Int, 10, "EPT_ID");
        ParametroCarga2.Value = int.Parse(eptId);
        parametrosConsulta.Add(ParametroCarga2);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_PARAM_MUESTREO_SUBT_FUENTES");
    }

    private void CargarCarFuentesSup(string ectId)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@ECT_ID", SqlDbType.Int, 10, "ECT_ID");
        ParametroCarga.Value = int.Parse(ectId);
        parametrosConsulta.Add(ParametroCarga);
        SqlParameter ParametroCarga2 = new SqlParameter("@EPC_ID", SqlDbType.Int, 10, "EPC_ID");
        ParametroCarga2.Value = int.Parse(this.cboCaracQuimSub.SelectedValue);
        parametrosConsulta.Add(ParametroCarga2);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_PARAM_MUESTREO_SUBT");
    }

    protected void btnCancelarCaracQuiSub_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhCarFuentesSubt);
        this.plhCarFuentesSubt.Visible = false;
    }

    protected void btnDetalleFuentesSub_Click(object sender, EventArgs e)
    {
        this.plhDetallesFuentesSub.Visible = true;
    }

    protected void btnCarFuentesSubt_Click(object sender, EventArgs e)
    {
        this.plhCarFuentesSubt.Visible = true;
    }

    #endregion

    #region 3.1.9.1 Sistemas de Corrientes



    protected void btnNuevoSistCorriente_Click(object sender, EventArgs e)
    {
        plhSistCorriente.Visible = true;
        plhSistCorriente.Focus();

    }
    protected void btnCancelarSistCorriente_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhSistCorriente);
        plhSistCorriente.Visible = false;

    }
    
    protected void btnAgregarSistCorriente_Click(object sender, EventArgs e)
    {
        GuardarSisCorriente();
        limpiarPlaceHolder(plhSistCorriente);
        
    }

    private void GuardarSisCorriente()
    {
        CargarSisCorriente();
        DataRow rw = dsGrilla.Tables[0].NewRow();
        rw["EIP_ID"] = IDProyecto;
        rw["ECO_FECHA_CREACION"] = Fecha;
        rw["ECO_PLUMA_DISPERSION"] = this.txtPlumaDispSistCorriente.Text;
        rw["ECO_EFEC_VIENTO_MAREA"] = this.txtEfectVientMarSistCorriente.Text;
        rw["ECO_DIRECCION"] = this.txtDireccionSistCorriente.Text;
        rw["ECO_PROB_OCURRENCIA"] = this.txtProbOcurSistCorriente.Text;
        rw["ECO_INTENSIDAD"] = this.txtIntenSistCorriente.Text;

        dsGrilla.Tables[0].Rows.Add(rw);

        Contexto.guardarTabla(dsGrilla, "EIH_SISTE_CORRIENTE_OCEANOGRAFIA");

        CargarSisCorriente();

    }

    private void CargarSisCorriente()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_SISTE_CORRIENTE_OCEANOGRAFIA");

        this.grvSistCorriente.DataSource = dsGrilla;
        this.grvSistCorriente.DataBind();

    }

    protected void grvSistCorriente_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarSistCorriente(e.RowIndex);
    }

    private void EliminarSistCorriente(int index)
    { 
        CargarSisCorriente();
        dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_SISTE_CORRIENTE_OCEANOGRAFIA");

        CargarSisCorriente();

    }

    #endregion

    #region 3.1.9.2 Oleaje en Playas


    protected void cargarTipoOleaje()
    {
        cboTipoOleaje.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoOleaje();
        cboTipoOleaje.DataValueField = "ETO_ID";
        cboTipoOleaje.DataTextField = "ETO_TIPO_OLEAJE";
        cboTipoOleaje.DataBind();
        cboTipoOleaje.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnAsignarOleaje_Click(object sender, EventArgs e)
    {
        GuardarTipoOleaje();
    }

    private void GuardarTipoOleaje()
    { 
         CargarTipoOleaje();
         if (dsGrilla.Tables[0].Rows.Count > 0)
         {
             dsGrilla.Tables[0].Rows[0]["ETO_ID"] = this.cboTipoOleaje.SelectedValue;
         }
         else
         {
             DataRow rw = dsGrilla.Tables[0].NewRow();
             rw["EIP_ID"] = IDProyecto;
             rw["EOP_FECHA_CREACION"] = Fecha;
             rw["ETO_ID"] = this.cboTipoOleaje.SelectedValue;             
             dsGrilla.Tables[0].Rows.Add(rw);
             
         }
         Contexto.guardarTabla(dsGrilla, "EIH_OLEAJE_PLAYAS_OCEANOGRAFIA");

         CargarTipoOleaje();
        
    }
    private void CargarTipoOleaje()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_OLEAJE_PLAYAS_OCEANOGRAFIA");

        if (dsGrilla.Tables[0].Rows.Count > 0)
            this.cboTipoOleaje.SelectedValue = dsGrilla.Tables[0].Rows[0]["ETO_ID"].ToString();
    }

    #endregion
    
    #region 3.1.9.3 Olas
    
    protected void cargarTipoOla()
    {
        cboTipoOlas.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoOla();
        cboTipoOlas.DataValueField = "EOO_ID";
        cboTipoOlas.DataTextField = "EOO_TIPO_OLAS";
        cboTipoOlas.DataBind();
        cboTipoOlas.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void btnNuevoOlas_Click(object sender, EventArgs e)
    {
        plhOlas.Visible = true;
        plhOlas.Focus();

    }
    protected void btnCancelarOlas_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhOlas);
        plhOlas.Visible = false;

    }

    protected void btnAgregarOlas_Click(object sender, EventArgs e)
    {
        GuardarOlas();
        limpiarPlaceHolder(plhOlas);
    }

    private void GuardarOlas()
    {
        CargarOlas();
        DataRow dr = dsGrilla.Tables[0].NewRow();

        dr["EIP_ID"] = IDProyecto;
        dr["EOC_FECHA_CREACION"] = Fecha;
        dr["EOO_ID"] = this.cboTipoOlas.SelectedValue;
        dr["EOC_FRECUENCIA"] = this.txtFrecuenciaOlas.Text;
        dr["EOC_ALTURA"] = this.txtAlturaOlas.Text;
        dr["EOC_DIRECCION"] = this.txtDireccionOlas.Text;
        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_OLAS_OCEANOGRAFIA");

        VisualizarOlas();        

    }

    private void VisualizarOlas()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIV_OLAS_OCEANOGRAFIA");

        this.grvOlas.DataSource = dsGrilla;
        this.grvOlas.DataBind();

    }

    private void CargarOlas()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_OLAS_OCEANOGRAFIA");
    }

    protected void grvOlas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarOlas(e.RowIndex);
    }
    private void EliminarOlas(int index)
    {
        CargarOlas();
        dsGrilla.Tables[0].Rows[index].Delete();

        Contexto.guardarTabla(dsGrilla, "EIH_OLAS_OCEANOGRAFIA");

        VisualizarOlas();

    }

    #endregion

    #region 3.1.9.4 Mareas

    protected void btnNuevoMareas_Click(object sender, EventArgs e)
    {
        plhMareas.Visible = true;
        plhMareas.Focus();
    }
    protected void btnCancelarMareas_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhMareas);
        plhMareas.Visible = false;
    }
    protected void btnAgregarMareas_Click(object sender, EventArgs e)
    {
        addRegistroMareas();
    }
    protected void cargarGrillaMareas()
    {

        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIC_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_MAREAS_OCEANOGRAFIA");
        this.grvMareas.DataSource = dsGrilla.Tables[0];
        this.grvMareas.DataBind();
    }

    protected void addRegistroMareas()
    {
        cargarGrillaMareas();
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EMO_DESCRIPCION"] = txtDescMareas.Text;
        dr["EMO_ALT_MAX_SICIGIAS"] = txtAltMaxSicigiasMareas.Text;
        dr["EMO_ALT_MIN_SICIGIAS"] = txtAltMinSicigiasMareas.Text;
        dr["EMO_ALT_MAX_CUADRATU"] = txtAltMaxCuadraturaMareas.Text;
        dr["EMO_ALT_MIN_CUADRATU"] = txtAltMinCuadraturaMareas.Text;
        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_MAREAS_OCEANOGRAFIA");
        this.txtDescMareas.Text = "";
        this.txtAltMaxSicigiasMareas.Text = "";
        this.txtAltMinSicigiasMareas.Text = "";
        this.txtAltMaxCuadraturaMareas.Text = "";
        this.txtAltMinCuadraturaMareas.Text = "";
        cargarGrillaMareas();
    }

    protected void grvMareas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        eliminarRegistroMareas(e.RowIndex);
    }

    protected void eliminarRegistroMareas(int index)
    {
        cargarGrillaMareas();
        this.dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_MAREAS_OCEANOGRAFIA");
        cargarGrillaMareas();
    }
    #endregion

    #region 3.1.9.5 Tormentas
    protected void btnNuevoTormentas_Click(object sender, EventArgs e)
    {
        plhTormentas.Visible = true;
        plhTormentas.Focus();

    }
    protected void btnCancelarTormentas_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhTormentas);
        plhTormentas.Visible = false;

    }
    protected void btnAgregarTormentas_Click(object sender, EventArgs e)
    {
        addRegistroTormentas();
    }
    protected void cargarGrillaTormentas()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_TORMENTAS_OCEANOGRAFIA");
        this.grvTormentas.DataSource = dsGrilla.Tables[0];
        this.grvTormentas.DataBind();
    }

    protected void addRegistroTormentas()
    {
        cargarGrillaTormentas();
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["ETO_DIRECCION"] = txtDireccionTormentas.Text;
        dr["ETO_FRECUENCIA"] = txtFrecuenciaTormentas.Text;
        dr["ETO_PERIODOS"] = txtPeriodosTormentas.Text;
        dr["ETO_ALTURA_OLAS"] = txtAltOlasTormentas.Text;
        dr["ETO_VELOCIDAD_PROP"] = txtVelPropagTormentas.Text;
        dr["ETO_EPOC_MAX_ACTIVIDAD"] = txtEpMayorActTormentas.Text;
        dr["ETO_EF_INST_PORTUARIAS"] = txtEfectInstPortTormentas.Text;
        dr["ETO_PRED_FENOMENO"] = txtPredicFenTormentas.Text;
        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_TORMENTAS_OCEANOGRAFIA");
        this.txtDireccionTormentas.Text = "";
        this.txtFrecuenciaTormentas.Text = "";
        this.txtPeriodosTormentas.Text = "";
        this.txtAltOlasTormentas.Text = "";
        this.txtVelPropagTormentas.Text = "";
        this.txtEpMayorActTormentas.Text = "";
        this.txtEfectInstPortTormentas.Text = "";
        this.txtPredicFenTormentas.Text = "";
        cargarGrillaTormentas();
    }

    protected void grvTormentas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        eliminarRegistroTormentas(e.RowIndex);
    }

    protected void eliminarRegistroTormentas(int index)
    {
        cargarGrillaTormentas();
        this.dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_TORMENTAS_OCEANOGRAFIA");
        cargarGrillaTormentas();
    }

    #endregion

    #region 3.1.10.1 Fuentes de Emisión Existentes

    protected void btnNuevoFuentEmiExist_Click(object sender, EventArgs e)
    {
        plhFuentEmiExist.Visible = true;
        plhFuentEmiExist.Focus();
        btnAgregarFuenteDet2.Visible = true;

    }
    protected void btnCancelarFuentEmiExist_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhFuentEmiExist);
        this.ctrCoorFuentEmiExist.CoorEste = "";
        this.ctrCoorFuentEmiExist.CoorNorte = "";
        plhFuentEmiExist.Visible = false;
        btnAgregarFuenteDet2.Visible = false;
    }
    protected void btnAgregarFuentEmiExist_Click(object sender, EventArgs e)
    {
        if (this.ctrCoorFuentEmiExist.Valido)
        {
            GuardarFuenteEmisionAire();
            limpiarPlaceHolder(plhFuentEmiExist);
            this.ctrCoorFuentEmiExist.CoorEste = "";
            this.ctrCoorFuentEmiExist.CoorNorte = "";
        }
    }

    private void GuardarFuenteEmisionAire()
    {
        CargarFuenteEmisionAire();
        if (dsGrilla.Tables[0].Rows.Count == 0)        
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EIP_ID"] = IDProyecto;
            dr["ECA_FECHA_CREACION"] = Fecha;
            dr["ELC_ID"] = DBNull.Value;
            dr["ECA_DESCRIP_FUE_MOVILES"] = DBNull.Value;
            dr["ECA_DESCRIP_FUE_LIENALES"] = DBNull.Value;
            dr["ECA_DESCRIP_FUE_AREA"] = DBNull.Value;
            dsGrilla.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsGrilla, "EIH_CALIDAD_AIRE");
        CargarFuenteEmisionAire();

        string ecaID = dsGrilla.Tables[0].Rows[0]["ECA_ID"].ToString();
        CargarFuentesFijas(ecaID);
        DataRow dr2 = dsGrilla.Tables[0].NewRow();
        dr2["ECA_ID"] = ecaID;
        dr2["EFF_DESCRIPCION"] = this.txtDescripcionFuentEmiExist.Text;
        dr2["EFF_COOR_NORTE_UBICA"] = this.ctrCoorFuentEmiExist.CoorNorte;
        dr2["EFF_COOR_ESTE_UBICA"] = this.ctrCoorFuentEmiExist.CoorEste;
        this.dsGrilla.Tables[0].Rows.Add(dr2);
        Contexto.guardarTabla(dsGrilla, "EIH_FUENTES_FIJAS_EMISION");
        CargarFuentesFijas(ecaID);

    }

    private void CargarFuentesFijas(string ecaID)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@ECA_ID", SqlDbType.Int, 10, "ECA_ID");
        ParametroCarga.Value = int.Parse(ecaID);
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_FUENTES_FIJAS_EMISION");

        this.grvFuentEmiExist.DataSource = dsGrilla;
        this.grvFuentEmiExist.DataBind();
    }

    private void CargarFuenteEmisionAire()
    { 
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_CALIDAD_AIRE");        
    }

   

    private void ActualizarDetFuentesEmisiones()
    {
        CargarFuenteEmisionAire();
        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["ECA_DESCRIP_FUE_MOVILES"] = this.txtDescFuentEmiMov.Text;
            dsGrilla.Tables[0].Rows[0]["ECA_DESCRIP_FUE_LIENALES"] = this.txtDescFuentEmiLin.Text;
            dsGrilla.Tables[0].Rows[0]["ECA_DESCRIP_FUE_AREA"] = this.txtDescFuentEmiArea.Text;            
        }
        Contexto.guardarTabla(dsGrilla, "EIH_CALIDAD_AIRE");       

        VisualizarDetFuentesEmisiones();
    }

    private void VisualizarDetFuentesEmisiones()
    {
        CargarFuenteEmisionAire();
        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            string ecaID = dsGrilla.Tables[0].Rows[0]["ECA_ID"].ToString();
            this.txtDescFuentEmiMov.Text=dsGrilla.Tables[0].Rows[0]["ECA_DESCRIP_FUE_MOVILES"].ToString();
            this.txtDescFuentEmiLin.Text=dsGrilla.Tables[0].Rows[0]["ECA_DESCRIP_FUE_LIENALES"].ToString();
            this.txtDescFuentEmiArea.Text=dsGrilla.Tables[0].Rows[0]["ECA_DESCRIP_FUE_AREA"].ToString();

            if (dsGrilla.Tables[0].Rows[0]["ELC_ID"].ToString() != "")
                this.cboLabMonitCalAire.SelectedValue = dsGrilla.Tables[0].Rows[0]["ELC_ID"].ToString();

            CargarFuentesFijas(ecaID);

            
    
        }
    }
    protected void btnAgregarFuenteDet2_Click(object sender, EventArgs e)
    {

        ActualizarDetFuentesEmisiones();


    }

    #endregion

    #region 3.1.10.2 Sitios de Monitoreo de Calidad del Aire

    protected void btnNuevoSitMonitCalAire_Click(object sender, EventArgs e)
    {
        plhSitMonitCalAire.Visible = true;
        plhSitMonitCalAire.Focus();

    }

    protected void btnCancelarSitMonitCalAire_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhSitMonitCalAire);
        plhSitMonitCalAire.Visible = false;
        this.ctrCoorSitMonitCalAire.CoorEste = "";
        this.ctrCoorSitMonitCalAire.CoorNorte = "";

    }

    protected void btnAgregarSitMonitCalAire_Click(object sender, EventArgs e)
    {
        if (this.ctrCoorSitMonitCalAire.Valido)
        {
            addRegistroSitMonitCalAire();
            this.ctrCoorSitMonitCalAire.CoorEste = "";
            this.ctrCoorSitMonitCalAire.CoorNorte = "";
        }
    }

    protected void cargarGrillaSitMonitCalAire()
    {
        CargarTabla("V_SITIOS_MONIT_CALIDAD_AIRE");
        this.grvSitMonitCalAire.DataSource = dsGrilla.Tables[0];
        this.grvSitMonitCalAire.DataBind();


        this.cboSitioDetalles.DataSource = dsGrilla;
        this.cboSitioDetalles.DataValueField = "ESM_ID";
        this.cboSitioDetalles.DataTextField = "ESM_DESCRIPCION";
        this.cboSitioDetalles.DataBind();
        this.cboSitioDetalles.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        this.cboSitiosCar.DataSource = dsGrilla;
        this.cboSitiosCar.DataValueField = "ESM_ID";
        this.cboSitiosCar.DataTextField = "ESM_DESCRIPCION";
        this.cboSitiosCar.DataBind();
        this.cboSitiosCar.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        

    }

    private void CargarRegistroMinitCalAire(string esmId)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        if (esmId != null)
        {
            SqlParameter ParametroCarga2 = new SqlParameter("@ESM_ID", SqlDbType.Int, 10, "ESM_ID");
            ParametroCarga2.Value = int.Parse(esmId);
            parametrosConsulta.Add(ParametroCarga2);
        }
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_SITIOS_MONIT_CALIDAD_AIRE");
    }

    protected void addRegistroSitMonitCalAire()
    {
        if (ctrCoorSitMonitCalAire.Valido)
        {
            CargarRegistroMinitCalAire(null);
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EIP_ID"] = IDProyecto;
            dr["ESM_DESCRIPCION"] = txtDescSitMonitCalAire.Text;
            dr["ESM_ALTITUD"] = txtAltSitMonitCalAire.Text;
            dr["ESM_COOR_NORTE_UBICACION"] = ctrCoorSitMonitCalAire.CoorNorte;
            dr["ESM_COOR_ESTE_UBICACION"] = ctrCoorSitMonitCalAire.CoorEste;
            dr["ESM_FECHA_MUESTREO"] = DBNull.Value;
            dr["ESM_DURACION_MUESTREO"] = DBNull.Value;
            dr["ESM_FREC_MUESTREO"] = DBNull.Value;
            dsGrilla.Tables[0].Rows.Add(dr);
            Contexto.guardarTabla(dsGrilla, "EIH_SITIOS_MONIT_CALIDAD_AIRE");
            this.txtDescSitMonitCalAire.Text = "";
            this.txtAltSitMonitCalAire.Text = "";
            this.ctrCoorSitMonitCalAire.CoorNorte = "";
            this.ctrCoorSitMonitCalAire.CoorEste = "";
            this.txtFeMuestSitMonitCalAire.Text = "";
            this.txtDurMuestSitMonitCalAire.Text = "";
            this.txtHoMuestSitMonitCalAire.Text = "";
            this.txtFrecMuestSitMonitCalAire.Text = "";
            cargarGrillaSitMonitCalAire();
        }
    }

    protected void grvSitMonitCalAire_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        eliminarRegistroSitMonitCalAire(e.RowIndex);
    }

    protected void eliminarRegistroSitMonitCalAire(int index)
    {
        CargarTabla("EIH_SITIOS_MONIT_CALIDAD_AIRE");
        this.dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_SITIOS_MONIT_CALIDAD_AIRE");
        cargarGrillaSitMonitCalAire();
    }
  
    #endregion

    #region 3.1.10.3 Resultados de Monitoreo de Calidad del Aire

    protected void cargarLabCalAire()
    {

        cboLabMonitCalAire.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaLaboratorio(3);
        cboLabMonitCalAire.DataValueField = "ELC_ID";
        cboLabMonitCalAire.DataTextField = "ELC_LABORATORIO_EST_CALIDAD";
        cboLabMonitCalAire.DataBind();
        cboLabMonitCalAire.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    
    protected void cargarcboCaracteristicasFisicoQuimicas()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EPC_ACTIVO", SqlDbType.Bit, 1, "EPC_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@ETP_ID", SqlDbType.Int, 10, "ETP_ID");
        par2.Value = 2;
        parametros.Add(par2);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsGrilla, "EIB_PARAMETROS_EST_CALIDAD");

        this.cboCarFisiQ.DataSource = dsGrilla;
        this.cboCarFisiQ.DataTextField = "EPC_PARAMETROS_EST_CALIDAD";
        this.cboCarFisiQ.DataValueField = "EPC_ID";
        this.cboCarFisiQ.DataBind();
        this.cboCarFisiQ.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }    

    protected void btnAsifnarLabFuentRuid_Click(object sender, EventArgs e)
    {
        AsignarLaboratorio();
    }

    private void AsignarLaboratorio()
    {
        CargarFuenteEmisionAire();
        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["ELC_ID"] = this.cboLabMonitCalAire.SelectedValue;            
        }

        Contexto.guardarTabla(dsGrilla, "EIH_CALIDAD_AIRE");

        VisualizarDetFuentesEmisiones();

    }

    protected void btnAsignarDetallesSitios_Click(object sender, EventArgs e)
    {
        this.plhDetallesSitios.Visible = true;
    }

    protected void btnAgregarDetSitios_Click(object sender, EventArgs e)
    {
        AsignarDetallesValores();
        limpiarPlaceHolder(plhDetallesSitios);
    }

    private void AsignarDetallesValores()
    {
        CargarRegistroMinitCalAire(this.cboSitioDetalles.SelectedValue);
        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            DateTime fechaHoraMuestreo = new DateTime();
            fechaHoraMuestreo = Convert.ToDateTime(txtFeMuestSitMonitCalAire.Text + " " + txtHoMuestSitMonitCalAire.Text);
            dsGrilla.Tables[0].Rows[0]["ESM_FECHA_MUESTREO"] = fechaHoraMuestreo;
            dsGrilla.Tables[0].Rows[0]["ESM_DURACION_MUESTREO"] = txtDurMuestSitMonitCalAire.Text;
            dsGrilla.Tables[0].Rows[0]["ESM_FREC_MUESTREO"] = txtFrecMuestSitMonitCalAire.Text;
            Contexto.guardarTabla(dsGrilla, "EIH_SITIOS_MONIT_CALIDAD_AIRE");
        }
        VisualizarResultadoMonitoreo();
    }

    protected void btnCancelarDetSitios_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhDetallesSitios);
        this.plhDetallesSitios.Visible = false;
    }

    protected void btnAgregarCarSitios_Click(object sender, EventArgs e)
    {
        GuardarCaracSitios();
        limpiarPlaceHolder(plhCarSitiosAire);
    }

    private void GuardarCaracSitios()
    {
        CargarRegistroMinitCalAire(this.cboSitiosCar.SelectedValue);
        string esmId = dsGrilla.Tables[0].Rows[0]["ESM_ID"].ToString();
        CargarCaracSitios(esmId,this.cboCarFisiQ.SelectedValue);
        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EMC_ID"] = this.cboMetDeterm.SelectedValue;
            dsGrilla.Tables[0].Rows[0]["EQA_CONCENT_PERMIT"] = this.txtConcePermitida.Text;
            dsGrilla.Tables[0].Rows[0]["EQA_FREC_MUESTREO"] = this.txtFrecMuest.Text;
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["ESM_ID"] = esmId;
            dr["EPC_ID"] = this.cboCarFisiQ.SelectedValue;
            dr["EMC_ID"] = this.cboMetDeterm.SelectedValue;
            dr["EQA_CONCENT_PERMIT"] = this.txtConcePermitida.Text;
            dr["EQA_FREC_MUESTREO"] = this.txtFrecMuest.Text;
            dsGrilla.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsGrilla, "EIH_PARAM_MUEST_AIRE");

        CargarCaracSitios(esmId, this.cboCarFisiQ.SelectedValue);
        string eqaId = dsGrilla.Tables[0].Rows[0]["EQA_ID"].ToString();
        CargarValCaracSitios(eqaId, this.cboSitiosCar.SelectedValue);
        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EPA_VALOR"] = this.txtValorCarSitio.Text; 
        }
        else 
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EQA_ID"] = eqaId;
            dr["ESM_ID"] = this.cboSitiosCar.SelectedValue;
            dr["EPA_VALOR"] = this.txtValorCarSitio.Text;            
            dsGrilla.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsGrilla, "EIH_PAR_MUE_AIRE_SITIOS");

        VisualizarResultadoMonitoreo();

    }

    private void VisualizarResultadoMonitoreo()
    {
        SILPA.LogicaNegocio.ResumenEIA.Procedimientos objPro = new SILPA.LogicaNegocio.ResumenEIA.Procedimientos();
        grvResultMonitoreoSitios.DataSource = objPro.ProcCalidadSitioAire(IDProyecto.ToString());
        grvResultMonitoreoSitios.DataBind();
    }

    private void CargarValCaracSitios(string eqaId, string esmId)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@ESM_ID", SqlDbType.Int, 10, "ESM_ID");
        ParametroCarga.Value = int.Parse(esmId);
        parametrosConsulta.Add(ParametroCarga);
        SqlParameter ParametroCarga2 = new SqlParameter("@EQA_ID", SqlDbType.Int, 10, "EQA_ID");
        ParametroCarga2.Value = int.Parse(eqaId);
        parametrosConsulta.Add(ParametroCarga2);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_PAR_MUE_AIRE_SITIOS");    
    }

    private void CargarCaracSitios(string esmId,string epcId)
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@ESM_ID", SqlDbType.Int, 10, "ESM_ID");
        ParametroCarga.Value = int.Parse(esmId);
        parametrosConsulta.Add(ParametroCarga);
        SqlParameter ParametroCarga2 = new SqlParameter("@EPC_ID", SqlDbType.Int, 10, "EPC_ID");
        ParametroCarga2.Value = int.Parse(epcId);
        parametrosConsulta.Add(ParametroCarga2);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_PARAM_MUEST_AIRE");        
    }

    protected void btnCancelarCarSitios_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhCarSitiosAire);
        this.plhCarSitiosAire.Visible = false;
    }

    protected void grvResultMonitoreoSitios_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 1)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;            
        }
    }

    protected void btnCarSitiosAire_Click(object sender, EventArgs e)
    {
        this.plhCarSitiosAire.Visible = true;
    }

    #endregion

    #region 3.1.10.4 Fuentes de Ruido Existentes
  
    protected void cargarTipoFuenteRuido()
    {
        cboTipoFuentRuidExist.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoFuenteRuido();
        cboTipoFuentRuidExist.DataValueField = "ETR_ID";
        cboTipoFuentRuidExist.DataTextField = "ETR_TIPO_FUENT_RUIDO";
        cboTipoFuentRuidExist.DataBind();
        cboTipoFuentRuidExist.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnNuevoFuentRuidExist_Click(object sender, EventArgs e)
    {
        plhFuentRuidExist.Visible = true;
        plhFuentRuidExist.Focus();
    }

    protected void btnCancelarFuentRuidExist_Click(object sender, EventArgs e)
    {
        plhFuentRuidExist.Visible = false;
    }

    protected void btnAgregarFuentRuidExist_Click(object sender, EventArgs e)
    {
        addRegistroFuentRuidExist();
    }

    protected void cargarGrillaFuentRuidExist()
    {        
        CargarTabla("V_FUENTES_RUIDO");
        this.grvFuentRuidExist.DataSource = dsGrilla.Tables[0];
        this.grvFuentRuidExist.DataBind();
    }

    protected void addRegistroFuentRuidExist()
    {        
        CargarTabla("EIH_FUENTES_RUIDO");

        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EFR_DESCRIP_FUENT_RUIDO"] = txtDescFuentRuidExist.Text;
        dr["ETR_ID"] = cboTipoFuentRuidExist.SelectedValue.ToString();
        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_FUENTES_RUIDO");
        this.txtDescFuentRuidExist.Text = "";
        this.cboTipoFuentRuidExist.SelectedIndex = -1;
        cargarGrillaFuentRuidExist();
    }

    protected void grvFuentRuidExist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        eliminarRegistroFuentRuidExist(e.RowIndex);
    }

    protected void eliminarRegistroFuentRuidExist(int index)
    {      
        CargarTabla("EIH_FUENTES_RUIDO");
        this.dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_FUENTES_RUIDO");
        cargarGrillaFuentRuidExist();
    }

    #endregion    

    #region 3.1.10.5 Sitios de Monitoreo de Ruido Ambiental

    protected void cargarSubSecMonitRuido()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@ESS_ACTIVO", SqlDbType.Bit, 10, "ESS_ACTIVO");
        ParametroCarga.Value = 1;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIB_SUBSECTOR_SITIO_MONIT_RUIDO");


        cboSubSectSitMonitRuido.DataSource = dsGrilla;
        cboSubSectSitMonitRuido.DataValueField = "ESS_ID";
        cboSubSectSitMonitRuido.DataTextField = "ESS_SUBSECTOR_SITIO_MONIT";
        cboSubSectSitMonitRuido.DataBind();
        cboSubSectSitMonitRuido.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnNuevoSitMonitRuido_Click(object sender, EventArgs e)
    {
        plhSitMonitRuido.Visible = true;
        plhSitMonitRuido.Focus();

    }

    protected void btnCancelarSitMonitRuido_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhSitMonitRuido);
        this.ctrCoorSitMonitRuido.CoorEste = "";
        this.ctrCoorSitMonitRuido.CoorNorte="";
        plhSitMonitRuido.Visible = false;

    }

    protected void btnAgregarSitMonitRuido_Click(object sender, EventArgs e)
    {
        addRegistroSitMonitRuido();
    }

    protected void cargarGrillaSitMonitRuido()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_SITIOS_MONIT_RUIDO_AMB");      

    }

    protected void addRegistroSitMonitRuido()
    {
        if (ctrCoorSitMonitRuido.Valido)
        {
            //Guarda el registro de sitios de monitoreo de ruido
            dsGrilla = new DataSet();
            List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
            SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
            ParametroCarga.Value = IDProyecto;
            parametrosConsulta.Add(ParametroCarga);
            Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_SITIOS_MONIT_RUIDO_AMB");
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EIP_ID"] = IDProyecto;
            dr["ESS_ID"] = cboSubSectSitMonitRuido.SelectedValue.ToString();
            dr["ESR_COOR_NORTE_UBI"] = ctrCoorSitMonitRuido.CoorNorte;
            dr["ESR_COOR_ESTE_UBI"] = ctrCoorSitMonitRuido.CoorEste;
            DateTime fechaHoraSitMonitRuido = new DateTime();
            fechaHoraSitMonitRuido = Convert.ToDateTime(txtFeMedSitMonitRuido.Text + " " + txtHoMedSitMonitRuido.Text);
            dr["ESR_FECHA_MEDICION"] = fechaHoraSitMonitRuido;
            dr["ESR_EQUIPO_MEDIDA"] = txtEqMedSitMonitRuido.Text;
            dr["ESR_DURACION_MED"] = txtDurMedSitMonitRuido.Text;
            dr["ESR_NIVEL_P_SONORA_EQ_DIUR"] = txtLeqtaDSitMonitRuido.Text;
            dr["ESR_NIVEL_P_SONORA_EQ_NOCT"] = txtLeqtaNSitMonitRuido.Text;
            dr["ESR_NIVEL_P_SONORA_RE_DIUR"] = txtLeqtDSitMonitRuido.Text;
            dr["ESR_NIVEL_P_SONORA_RE_NOCT"] = txtLeqtNSitMonitRuido.Text;
            dr["ESR_NIVEL_PERCENTIL_DIUR"] = txtL90DSitMonitRuido.Text;
            dr["ESR_NIVEL_PERCENTIL_NOCT"] = txtL90NSitMonitRuido.Text;
            dr["ESR_NIVEL_RUIDO_PERMITIDO"] = txtRuidPerSitMonitRuido.Text;
            dsGrilla.Tables[0].Rows.Add(dr);
            Contexto.guardarTabla(dsGrilla, "EIH_SITIOS_MONIT_RUIDO_AMB");
            //Guarda el responsable de la medición
            dsGrilla = new DataSet();
            parametrosConsulta = new List<SqlParameter>();
            ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
            ParametroCarga.Value = IDProyecto;
            parametrosConsulta.Add(ParametroCarga);
            Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_ESTUDIO_RUIDO");
            if (dsGrilla.Tables[0].Rows.Count == 0)
            {
                dr = dsGrilla.Tables[0].NewRow();
                dr["EIP_ID"] = IDProyecto;
                dr["EER_RESP_MEDICIO"] = txtRespMedSitMonitRuido.Text;
                dsGrilla.Tables[0].Rows.Add(dr);
            }
            else
            {
                dsGrilla.Tables[0].Rows[0]["EER_RESP_MEDICIO"] = txtRespMedSitMonitRuido.Text;
            }
            Contexto.guardarTabla(dsGrilla, "EIH_ESTUDIO_RUIDO");
            //Reinicia el formulario
            cboSubSectSitMonitRuido.SelectedIndex = -1;
            ctrCoorSitMonitRuido.CoorNorte = "";
            ctrCoorSitMonitRuido.CoorEste = "";
            limpiarPlaceHolder(plhSitMonitRuido);
            VisualizarGrillaSitMonitRuido();
            VisualizarResultadoRuido();      
        }
    }

    private void VisualizarResultadoRuido()
    {
        SILPA.LogicaNegocio.ResumenEIA.Procedimientos objPro = new SILPA.LogicaNegocio.ResumenEIA.Procedimientos();
        grvSitMonitRuido.DataSource = objPro.ProcCalidadSitioRuido(IDProyecto.ToString());
        grvSitMonitRuido.DataBind();
    }   

    private void VisualizarGrillaSitMonitRuido()
    {        
        CargarTabla("EIV_SITIOS_MONIT_RUIDO_AMB");

        this.grvMonitRuido.DataSource = dsGrilla;
        this.grvMonitRuido.DataBind();
                
        CargarTabla("EIH_ESTUDIO_RUIDO");

        if (dsGrilla.Tables[0].Rows.Count != 0)
            txtRespMedSitMonitRuido.Text = dsGrilla.Tables[0].Rows[0]["EER_RESP_MEDICIO"].ToString();
    }

    protected void grvMonitRuido_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarMonitRuido(e.RowIndex);
    }

    private void EliminarMonitRuido(int index)
    {
        cargarGrillaSitMonitRuido();
        this.dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_SITIOS_MONIT_RUIDO_AMB");
        VisualizarGrillaSitMonitRuido();
        VisualizarResultadoRuido();      
    }
    
    #endregion

    #region 3.2.1.1 Tipo de Ecosistema

    protected void cargarClasTipoEcosistema()
    {
        cboClasTipoEcosistema.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaClasTipoEcosistema();
        cboClasTipoEcosistema.DataValueField = "ECE_ID";
        cboClasTipoEcosistema.DataTextField = "ECE_CLASIFICACION_TERRESTRE";
        cboClasTipoEcosistema.DataBind();
        cboClasTipoEcosistema.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void cargarTipoEcosisTerrestre()
    {
        int idClasEcosistema = -1;
        int.TryParse(cboClasTipoEcosistema.SelectedValue.ToString(), out idClasEcosistema);
        cboTipoEcosistema.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoEcosisTerrestre(idClasEcosistema);
        cboTipoEcosistema.DataValueField = "ETE_ID";
        cboTipoEcosistema.DataTextField = "ETE_TIPO_ECOSISTEMA_TERRESTRE";
        cboTipoEcosistema.DataBind();
        cboTipoEcosistema.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void btnNuevoTipoEcosistema_Click(object sender, EventArgs e)
    {
        plhTipoEcosistema.Visible = true;
        plhTipoEcosistema.Focus();

    }
    protected void btnCancelarTipoEcosistema_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhTipoEcosistema);
        plhTipoEcosistema.Visible = false;

    }
    protected void cboClasTipoEcosistema_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarTipoEcosisTerrestre();
    }

    protected void btnAgregarTipoEcosistema_Click(object sender, EventArgs e)
    {
        GuardarTipoEcosis();
    }

    private void GuardarTipoEcosis()
    {
        CargarTabla("EIH_TIPOS_ECOSISTEMAS_TERRESTRES");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["ETT_FECHA_CREACION"] = Fecha;
        dr["ETE_ID"] = this.cboTipoEcosistema.SelectedValue;
        dr["ECE_ID"] = this.cboClasTipoEcosistema.SelectedValue;
        dr["ETT_COD_MAPA"] = this.txtCodMapaTipoEcosistema.Text;
        dr["ETT_DESCRIPCION"] = this.txtDescTipoEcosistema.Text;
        dr["ETT_OBSERVACIONES"] = this.txtObserTipoEcosistema.Text;

        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_TIPOS_ECOSISTEMAS_TERRESTRES");
        limpiarPlaceHolder(plhTipoEcosistema);
        VisualizarTipoEcosis();
    }
    private void VisualizarTipoEcosis()
    {
        CargarTabla("EIV_TIPOS_ECOSISTEMAS_TERRESTRES");
        this.grvTipoEcosistema.DataSource = dsGrilla;
        this.grvTipoEcosistema.DataBind();
    }

    protected void grvTipoEcosistema_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarTipoEcosis(e.RowIndex);
    }
    private void EliminarTipoEcosis(int index)
    {
        CargarTabla("EIH_TIPOS_ECOSISTEMAS_TERRESTRES");
        dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_TIPOS_ECOSISTEMAS_TERRESTRES");
        VisualizarTipoEcosis();
    }

    #endregion

    #region 3.2.1.2 Fuente de la Información

    protected void cargarEscalaTrabajo()
    {
        cboEscTraFuentInfo.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaEscalaTrabajo();
        cboEscTraFuentInfo.DataValueField = "EET_ID";
        cboEscTraFuentInfo.DataTextField = "EET_ESCALA_TRABAJO";
        cboEscTraFuentInfo.DataBind();
        cboEscTraFuentInfo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void cargarFuenteInfoEcoterr()
    {
        cboFuentInfo.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaFuenteInfoEcoterr();
        cboFuentInfo.DataValueField = "EFI_ID";
        cboFuentInfo.DataTextField = "EFI_FUENT_INFO_ECOTERR";
        cboFuentInfo.DataBind();
        cboFuentInfo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnNuevoFuentInfo_Click(object sender, EventArgs e)
    {
        plhFuentInfo.Visible = true;
        plhFuentInfo.Focus();

    }
    protected void btnCancelarFuentInfo_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhFuentInfo);
        plhFuentInfo.Visible = false;

    }

    protected void cboFuentInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.trOtroFuenteInfo.Visible = false;
        this.trEscala.Visible=false;
        this.trTipoFuente.Visible = false;
        this.txtOtroFuentInfo.Text = "";
        this.txtEscalaFuentInfo.Text = "";
        this.txtTipoFuentInfo.Text = "";
        
        if (this.cboFuentInfo.SelectedItem.Text.Contains("Otro"))
        {            
            this.trOtroFuenteInfo.Visible = true;
            this.trEscala.Visible = true;
        }            

        if (this.cboFuentInfo.SelectedItem.Text.Contains("Fotografía Aérea"))
            this.trEscala.Visible = true;            
        

        if (this.cboFuentInfo.SelectedItem.Text.Contains("Imagen de Satélite"))
            this.trTipoFuente.Visible = true;       

        
    }
    protected void btnAgregarFuentInfo_Click(object sender, EventArgs e)
    {
        GuardarFuenteInformacion();
        limpiarPlaceHolder(plhFuentInfo);
    }

    private void GuardarFuenteInformacion()    
    {
        CargarTabla("EIH_FUENT_INFO_ECOTERR");
        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EET_ID"] = this.cboEscTraFuentInfo.SelectedValue;
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EIP_ID"] = IDProyecto;
            dr["EET_FECHA_CREACION"] = Fecha;
            dr["EET_ID"] = this.cboEscTraFuentInfo.SelectedValue;

            dsGrilla.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsGrilla, "EIH_FUENT_INFO_ECOTERR");
        CargarTabla("EIH_FUENT_INFO_ECOTERR");
        string efeId = dsGrilla.Tables[0].Rows[0]["EFE_ID"].ToString();

        CargarTabla("EIH_FUENTES_INFO_ECOTERR","EFE_ID",efeId);

        DataRow dr2 = dsGrilla.Tables[0].NewRow();
        dr2["EFE_ID"] = efeId;
        dr2["EFI_ID"] = this.cboFuentInfo.SelectedValue;
        if (this.txtOtroFuentInfo.Text=="")
            dr2["EFI_ID_OTRO"] = DBNull.Value;
        else
            dr2["EFI_ID_OTRO"] = this.txtOtroFuentInfo.Text;
        dr2["EIE_ANIO"] = this.txtAnioFuentInfo.Text;
        if (this.txtEscalaFuentInfo.Text == "")
            dr2["EIE_ESCALA"] = DBNull.Value;
        else
            dr2["EIE_ESCALA"] = this.txtEscalaFuentInfo.Text;
        if (this.txtTipoFuentInfo.Text == "")
            dr2["EIE_TIPO"] = DBNull.Value;
        else
            dr2["EIE_TIPO"] = this.txtTipoFuentInfo.Text;
        dr2["EIE_NIVEL_RESOLUCION"] = this.txtNivResolFuentInfo.Text;

        dsGrilla.Tables[0].Rows.Add(dr2);

        Contexto.guardarTabla(dsGrilla, "EIH_FUENTES_INFO_ECOTERR");
        VisualizarFuenteInformacion();
        
    }
    private void VisualizarFuenteInformacion()
    {
        Visualizar("EIV_FUENT_INFO_ECOTERR", grvFuentInfo);
        if (dsGrilla.Tables[0].Rows.Count > 0)
            this.cboEscTraFuentInfo.SelectedValue = dsGrilla.Tables[0].Rows[0]["EET_ID"].ToString();
    }

    protected void grvFuentInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIH_FUENT_INFO_ECOTERR");
        string efeId = dsGrilla.Tables[0].Rows[0]["EFE_ID"].ToString();
        CargarTabla("EIH_FUENTES_INFO_ECOTERR", "EFE_ID", efeId);
        dsGrilla.Tables[0].Rows[e.RowIndex].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_FUENTES_INFO_ECOTERR");
        VisualizarFuenteInformacion();
    }
    

    #endregion

    #region 3.2.1.3 Tipos de Unidad de Cobertura Vegetal Presentes

    protected void cargarUnidadArea()
    {
        cboUnidAreaTipoUnidCoberVeg.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaUnidadArea();
        cboUnidAreaTipoUnidCoberVeg.DataValueField = "EUA_ID";
        cboUnidAreaTipoUnidCoberVeg.DataTextField = "EUA_UNIDAD_AREA";
        cboUnidAreaTipoUnidCoberVeg.DataBind();
        cboUnidAreaTipoUnidCoberVeg.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnNuevoTipoUnidCoberVeg_Click(object sender, EventArgs e)
    {
        plhTipoUnidCoberVeg.Visible = true;
        plhTipoUnidCoberVeg.Focus();

    }
    protected void btnCancelarTipoUnidCoberVeg_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhTipoUnidCoberVeg);
        plhTipoUnidCoberVeg.Visible = false;
    }

    protected void btnAgregarTipoUnidCoberVeg_Click(object sender, EventArgs e)
    {
        GuardarCovertVegetal();
        limpiarPlaceHolder(plhTipoUnidCoberVeg);
        
    }

    private void GuardarCovertVegetal()
    {
        CargarTabla("EIH_UNIDAD_COVERT_VEGETAL");
        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EUA_ID"] = this.cboUnidAreaTipoUnidCoberVeg.SelectedValue;
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EIP_ID"] = IDProyecto;
            dr["EUA_FECHA_CREACION"] = Fecha;
            dr["EUA_ID"] = this.cboUnidAreaTipoUnidCoberVeg.SelectedValue;
            dsGrilla.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsGrilla, "EIH_UNIDAD_COVERT_VEGETAL");
        CargarTabla("EIH_UNIDAD_COVERT_VEGETAL");
        string euvId = dsGrilla.Tables[0].Rows[0]["EUV_ID"].ToString();
        CargarTabla("EIH_TIPO_UNIDAD_COVER_VEGETAL", "EUV_ID", euvId);
        DataRow dr2 = dsGrilla.Tables[0].NewRow();
        dr2["EUV_ID"] = euvId;
        dr2["ECV_COD_MAPA"] = this.txtCodMapaTipoUnidCoberVeg.Text;
        dr2["ECV_TIPO_UNIDAD"] = this.txtTipoUnidCoberVeg.Text;
        dr2["ECV_DESCRIPCION"] = this.txtDescTipoUnidCoberVeg.Text;
        dr2["ECV_AREA_AREA_EST"] = this.txtAreaTipoUnidCoberVeg.Text;
        dr2["ECV_PORC_AREA_INTERV"] = this.txtPorcUnidCoberVeg.Text;
        dr2["ECV_FUNC_ECO_FAUNA"] = this.txtFuncEcoUnidCoberVeg.Text;

        dsGrilla.Tables[0].Rows.Add(dr2);

        Contexto.guardarTabla(dsGrilla, "EIH_TIPO_UNIDAD_COVER_VEGETAL");

        VisualizarCovertVegetal();

    }

    private void VisualizarCovertVegetal()
    {
        Visualizar("EIV_UNIDAD_COVERT_VEGETAL", grvTipoUnidCoberVeg);        
        if (dsGrilla.Tables[0].Rows.Count > 0)
        {            
            this.cboUnidAreaTipoUnidCoberVeg.SelectedValue = dsGrilla.Tables[0].Rows[0]["EUA_ID"].ToString();
            this.grvTipoUnidCoberVeg.HeaderRow.Cells[4].Text = "Área del Área de Estudio (" + this.cboUnidAreaTipoUnidCoberVeg.SelectedItem.Text + ")"; ;
            this.lblAreaEstidio.Text = "Área del Área de Estudio ()";
            if (this.cboUnidAreaTipoUnidCoberVeg.SelectedValue != "-1")
                this.lblAreaEstidio.Text = "Área del Área de Estudio (" + this.cboUnidAreaTipoUnidCoberVeg.SelectedItem.Text + ")";
        }
        
    }

    protected void grvTipoUnidCoberVeg_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIH_UNIDAD_COVERT_VEGETAL");
        string euvId = dsGrilla.Tables[0].Rows[0]["EUV_ID"].ToString();
        CargarTabla("EIH_TIPO_UNIDAD_COVER_VEGETAL", "EUV_ID", euvId);
        dsGrilla.Tables[0].Rows[e.RowIndex].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_TIPO_UNIDAD_COVER_VEGETAL");
        VisualizarCovertVegetal();
    }

    protected void cboUnidAreaTipoUnidCoberVeg_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblAreaEstidio.Text = "Área del Área de Estudio ()";
        if (this.cboUnidAreaTipoUnidCoberVeg.SelectedValue != "-1")
            this.lblAreaEstidio.Text = "Área del Área de Estudio (" + this.cboUnidAreaTipoUnidCoberVeg.SelectedItem.Text + ")";
    }

    protected void cboAplica1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.btnNuevoTipoUnidCoberVeg.Visible = false;
        if (this.cboAplica1.SelectedValue == "0")
        {
            this.btnNuevoTipoUnidCoberVeg.Visible = true;
            this.grvTipoUnidCoberVeg.Visible = true;
        }
           
    }

    #endregion

    #region   3.2.1.4 Descripción Fisionómica
    protected void btnNuevoSitioDescFisio_Click(object sender, EventArgs e)
    {
        plhSitioDescFisio.Visible = true;
        plhSitioDescFisio.Focus();

    }
    protected void btnCancelarSitioDescFisio_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhSitioDescFisio);
        plhSitioDescFisio.Visible = false;

    }

    protected void btnAgregarSitioDescFisio_Click(object sender, EventArgs e)
    {
        if (this.ctrCoorSitioDescFisio.Validar(1, 5))
        {
            GuardarDescrFisionomica();
            this.ctrCoorSitioDescFisio.LimpiarObjetos();
            limpiarPlaceHolder(plhSitioDescFisio);

        }
    }

    private void GuardarDescrFisionomica()
    {
        GuardarFisionomica();
        string edfId = dsGrilla.Tables[0].Rows[0]["EDF_ID"].ToString();

        CargarTabla("EIH_SITIO_MUESTREO_DESC_FISIONOMICA", "EDF_ID", edfId);

        DataRow dr2 = dsGrilla.Tables[0].NewRow();
        dr2["EDF_ID"] = edfId;
        dr2["ESM_COD_MAPA"] = this.txtCodMapaSitioDescFisio.Text;
        dr2["ESM_TIPO_UNIDAD"] = this.txtTipoUnidadSitioDescFisio.Text;
        dr2["ESM_DESCRIPCION"] = this.txtDescSitioDescFisio.Text;
        dr2["ESM_AREA_MUESTREADA"] = this.txtAreaSitioDescFisio.Text;

        dsGrilla.Tables[0].Rows.Add(dr2);
        Contexto.guardarTabla(dsGrilla, "EIH_SITIO_MUESTREO_DESC_FISIONOMICA");

        CargarTabla("EIH_SITIO_MUESTREO_DESC_FISIONOMICA", "EDF_ID", edfId);

        string esmId = dsGrilla.Tables[0].Rows[dsGrilla.Tables[0].Rows.Count-1]["ESM_ID"].ToString();

        CargarTabla("EIH_COOR_STIO_MUES_DESC_FISION", "ESM_ID", esmId);

        foreach (DataRow row in this.ctrCoorSitioDescFisio.Coordenadas.Rows)
        {
            DataRow row2 = dsGrilla.Tables[0].NewRow();
            row2["ESM_ID"] = esmId;
            row2["ECM_COOR_NORTE"] = row["COORNORTE"].ToString() ;
            row2["ECM_COOR_ESTE"] = row["COORESTE"].ToString();
            dsGrilla.Tables[0].Rows.Add(row2);
        }
        Contexto.guardarTabla(dsGrilla, "EIH_COOR_STIO_MUES_DESC_FISION");

        Visualizar("EIH_SITIO_MUESTREO_DESC_FISIONOMICA", "EDF_ID", edfId, grvSitioDescFisio);
    }

    private void GuardarFisionomica()
    {
        CargarTabla("EIH_DESC_FISIONOMICA_ECOTERR");

        if (dsGrilla.Tables[0].Rows.Count == 0)
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EIP_ID"] = IDProyecto;
            dr["EDF_FECHA_CREACION"] = Fecha;
            dr["EFS_ID"] = DBNull.Value;
            dr["ETE_ID"] = DBNull.Value;
            dsGrilla.Tables[0].Rows.Add(dr);
            Contexto.guardarTabla(dsGrilla, "EIH_DESC_FISIONOMICA_ECOTERR");
            CargarTabla("EIH_DESC_FISIONOMICA_ECOTERR");
        }
    }

    protected void grvSitioDescFisio_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarDescrFisionomica(e.RowIndex);
    }

    private void EliminarDescrFisionomica(int index)
    {
        CargarTabla("EIH_DESC_FISIONOMICA_ECOTERR");
        string edfId = dsGrilla.Tables[0].Rows[0]["EDF_ID"].ToString();

        CargarTabla("EIH_SITIO_MUESTREO_DESC_FISIONOMICA", "EDF_ID", edfId);

        string esmId = dsGrilla.Tables[0].Rows[index]["ESM_ID"].ToString();

        CargarTabla("EIH_COOR_STIO_MUES_DESC_FISION", "ESM_ID", esmId);

        DataTable temp = dsGrilla.Tables[0];
        int i;
        for(i=temp.Rows.Count-1;i>=0;i--)
        {
            dsGrilla.Tables[0].Rows[i].Delete();
        }
        Contexto.guardarTabla(dsGrilla, "EIH_COOR_STIO_MUES_DESC_FISION");

        CargarTabla("EIH_SITIO_MUESTREO_DESC_FISIONOMICA", "EDF_ID", edfId);

        dsGrilla.Tables[0].Rows[index].Delete();

        Contexto.guardarTabla(dsGrilla, "EIH_SITIO_MUESTREO_DESC_FISIONOMICA");

        Visualizar("EIH_SITIO_MUESTREO_DESC_FISIONOMICA", "EDF_ID", edfId, grvSitioDescFisio);
    }

    private void VisualizarDescrFisionomica()
    {
        CargarTabla("EIH_DESC_FISIONOMICA_ECOTERR");
        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            if (dsGrilla.Tables[0].Rows[0]["ETE_ID"].ToString()!="")
                this.cboEstVertDom.SelectedValue = dsGrilla.Tables[0].Rows[0]["ETE_ID"].ToString();
            if (dsGrilla.Tables[0].Rows[0]["EFS_ID"].ToString()!="")
                this.cboPosFitoDom.SelectedValue = dsGrilla.Tables[0].Rows[0]["EFS_ID"].ToString();
            
            string edfId = dsGrilla.Tables[0].Rows[0]["EDF_ID"].ToString();
            Visualizar("EIH_SITIO_MUESTREO_DESC_FISIONOMICA", "EDF_ID", edfId, grvSitioDescFisio);
            Visualizar("EIV_ESPECIES_DOMINATES_ECOTERR", "EDF_ID", edfId, grvEspDomEstrato);
        }
    }

    protected void cargarEstrucVertDom()
    {
        cboEstVertDom.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaEstrucVertDom();
        cboEstVertDom.DataValueField = "ETE_ID";
        cboEstVertDom.DataTextField = "ETE_TIPO_ESTRUC_VERT_DOM";
        cboEstVertDom.DataBind();
        cboEstVertDom.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void cboEstVertDom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboEstVertDom.SelectedValue != "-1")
        {
            ActualizarDescrFisionomica(this.cboEstVertDom.SelectedValue);
        }
    }
    private void ActualizarDescrFisionomica(string eteId)
    {
        CargarTabla("EIH_DESC_FISIONOMICA_ECOTERR");
        dsGrilla.Tables[0].Rows[0]["ETE_ID"] = eteId;
        Contexto.guardarTabla(dsGrilla, "EIH_DESC_FISIONOMICA_ECOTERR");        
    }

    
    #endregion

    #region Especies Dominantes por Estrato

    protected void cargarTipoEstrato()
    {
        cboTipoEstrato.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoEstrato();
        cboTipoEstrato.DataValueField = "ETE_ID";
        cboTipoEstrato.DataTextField = "ETE_TIPO_ESTRATO_ECOTERR";
        cboTipoEstrato.DataBind();
        cboTipoEstrato.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboEstVertDom.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoEstrato();
        cboEstVertDom.DataValueField = "ETE_ID";
        cboEstVertDom.DataTextField = "ETE_TIPO_ESTRATO_ECOTERR";
        cboEstVertDom.DataBind();
        cboEstVertDom.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        
    }

    protected void btnNuevoEspDomEstrato_Click(object sender, EventArgs e)
    {
        plhEspDomEstrato.Visible = true;
        plhEspDomEstrato.Focus();
    }

    protected void btnCancelarEspDomEstrato_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhEspDomEstrato);
        plhEspDomEstrato.Visible = false;
    }

    protected void btnAgregarEspDomEstrato_Click(object sender, EventArgs e)
    {
        GuardarEspecDom();
        limpiarPlaceHolder(plhEspDomEstrato);
    }

    private void GuardarEspecDom()
    {
        GuardarFisionomica();

        string edfId = dsGrilla.Tables[0].Rows[0]["EDF_ID"].ToString();

        CargarTabla("EIH_ESPECIES_DOMINATES_ECOTERR", "EDF_ID", edfId);

        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EDF_ID"] = edfId;
        dr["ETE_ID"] = this.cboTipoEstrato.SelectedValue;
        dr["EED_NOMBRE_COMUN"] = this.txtNomComunEspDomEstrato.Text;
        dr["EED_NOMBRE_CIENTI"] = this.txtNomCientEspDomEstrato.Text;        
        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_ESPECIES_DOMINATES_ECOTERR");
        Visualizar("EIV_ESPECIES_DOMINATES_ECOTERR", "EDF_ID", edfId, grvEspDomEstrato);
    }

    protected void grvEspDomEstrato_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIH_DESC_FISIONOMICA_ECOTERR");
        string edfId = dsGrilla.Tables[0].Rows[0]["EDF_ID"].ToString();

        CargarTabla("EIH_ESPECIES_DOMINATES_ECOTERR", "EDF_ID", edfId);       

        dsGrilla.Tables[0].Rows[e.RowIndex].Delete();

        Contexto.guardarTabla(dsGrilla, "EIH_ESPECIES_DOMINATES_ECOTERR");

        Visualizar("EIV_ESPECIES_DOMINATES_ECOTERR", "EDF_ID", edfId, grvEspDomEstrato);
    }


    protected void cargarPosFitoDominante()
    {
        cboPosFitoDom.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaPosFitoDominante();
        cboPosFitoDom.DataValueField = "EFS_ID";
        cboPosFitoDom.DataTextField = "EFS_TIPO_POS_FITOSOC_DOM";
        cboPosFitoDom.DataBind();
        cboPosFitoDom.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void cboPosFitoDom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboPosFitoDom.SelectedValue != "-1")
        {
            ActualizarDescrFisionomica2(this.cboPosFitoDom.SelectedValue);
        }
    }
    private void ActualizarDescrFisionomica2(string efsId)
    {
        CargarTabla("EIH_DESC_FISIONOMICA_ECOTERR");
        dsGrilla.Tables[0].Rows[0]["EFS_ID"] = efsId;
        Contexto.guardarTabla(dsGrilla, "EIH_DESC_FISIONOMICA_ECOTERR");
    }

    

    #endregion

    #region 3.2.1.5 Información Complementaria Flora
    protected void btnNuevoInfoFlora_Click(object sender, EventArgs e)
    {
        plhInfoFlora.Visible = true;
        plhInfoFlora.Focus();

    }
    protected void btnCancelarInfoFlora_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfoFlora);
        plhInfoFlora.Visible = false;
    }

    protected void cargarTipoEspecieFlora()
    {
        cboEspIntInfoFlora.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoEspecieFlora();
        cboEspIntInfoFlora.DataValueField = "EEF_ID";
        cboEspIntInfoFlora.DataTextField = "EEF_TIPO_ESPECIE";
        cboEspIntInfoFlora.DataBind();
        cboEspIntInfoFlora.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnAgregarInfoFlora_Click(object sender, EventArgs e)
    {
        GuardarInfoFlora();
        limpiarPlaceHolder(plhInfoFlora);
    }

    private void GuardarInfoFlora()
    {
        CargarTabla("EIH_INFO_FLORA_ECOTERR");

        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EIF_FECHA_CREACION"] = Fecha;
        dr["EEF_ID"] = this.cboEspIntInfoFlora.SelectedValue;
        dr["EIF_NOMBRE_COMUN"] = this.txtNomComInfoFlora.Text;
        dr["EIF_NOMBRE_CIENTF"] = this.txtNomCientInfoFlora.Text;
        dr["EIF_TIPO_FUENTE"] = this.cboTipFuentInfoFlora.SelectedValue;
        dr["EIF_FUENTE"] = this.txtFuenteInfoFlora.Text;

        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_INFO_FLORA_ECOTERR");

        Visualizar("EIV_INFO_FLORA_ECOTERR", grvInfoFlora);
    }

    protected void grvInfoFlora_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_INFO_FLORA_ECOTERR", e.RowIndex);
        Visualizar("EIV_INFO_FLORA_ECOTERR", grvInfoFlora);
    }

    #endregion

    #region 3.2.1.6 Información Complementaria Fauna

    protected void btnNuevoInfoFaunaAnf_Click(object sender, EventArgs e)
    {
        plhInfoFaunaAnf.Visible = true;
        plhInfoFaunaAnf.Focus();

    }
    protected void btnCancelarInfoFaunaAnf_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfoFaunaAnf);
        plhInfoFaunaAnf.Visible = false;
    }

    protected void cargarTipoEspecieFaunaAnf()
    {
        cboEspIntInfoFaunaAnf.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoEspeciesFauna();
        cboEspIntInfoFaunaAnf.DataValueField = "ETF_ID";
        cboEspIntInfoFaunaAnf.DataTextField = "ETF_TIPO_ESPECIE";
        cboEspIntInfoFaunaAnf.DataBind();
        cboEspIntInfoFaunaAnf.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnAgregarInfoFaunaAnf_Click(object sender, EventArgs e)
    {
        GuardarInfoFauna();
        limpiarPlaceHolder(plhInfoFaunaAnf);
    }

    private void GuardarInfoFauna()
    {
        CargarTabla("EIH_INFO_FAUNA_ECOTERR_AMFIB");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EFA_FECHA_CREACION"] = Fecha;
        dr["ETF_ID"] = this.cboEspIntInfoFaunaAnf.SelectedValue;
        dr["EFA_NOMBRE_COMUN"] = this.txtNomComInfoFaunaAnf.Text;
        dr["EFA_NOMBRE_CIENTF"] = this.txtNomCientInfoFaunaAnf.Text;
        dr["EFA_TIPO_FUENTE"] = this.cboTipFuentInfoFaunaAnf.SelectedValue;
        dr["EFA_FUENTE"] = this.txtFuenteInfoFaunaAnf.Text;

        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_INFO_FAUNA_ECOTERR_AMFIB");

        Visualizar("EIV_INFO_FAUNA_ECOTERR_AMFIB", grvInfoFaunaAnf);

    }

    protected void grvInfoFaunaAnf_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_INFO_FAUNA_ECOTERR_AMFIB", e.RowIndex);
        Visualizar("EIV_INFO_FAUNA_ECOTERR_AMFIB", grvInfoFaunaAnf);
    }

    #endregion

    #region Reptiles

    protected void btnNuevoInfoFaunaRep_Click(object sender, EventArgs e)
    {
        plhInfoFaunaRep.Visible = true;
        plhInfoFaunaRep.Focus();

    }
    protected void btnCancelarInfoFaunaRep_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfoFaunaRep);
        plhInfoFaunaRep.Visible = false;
    }

    protected void cargarTipoEspecieFaunaRep()
    {
        cboEspIntInfoFaunaRep.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoEspeciesFauna();
        cboEspIntInfoFaunaRep.DataValueField = "ETF_ID";
        cboEspIntInfoFaunaRep.DataTextField = "ETF_TIPO_ESPECIE";
        cboEspIntInfoFaunaRep.DataBind();
        cboEspIntInfoFaunaRep.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnAgregarInfoFaunaRep_Click(object sender, EventArgs e)
    {
        GuardarInfoFaunaRep();
    }

    private void GuardarInfoFaunaRep()
    {
        CargarTabla("EIH_INFO_FAUNA_ECOTERR_REPT");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EFR_FECHA_CREACION"] = Fecha;
        dr["ETF_ID"] = this.cboEspIntInfoFaunaRep.SelectedValue;
        dr["EFR_NOMBRE_COMUN"] = this.txtNomComInfoFaunaRep.Text;
        dr["EFR_NOMBRE_CIENTF"] = this.txtNomCientInfoFaunaRep.Text;
        dr["EFR_TIPO_FUENTE"] = this.cboTipFuentInfoFaunaRep.SelectedValue;
        dr["EFR_FUENTE"] = this.txtFuenteInfoFaunaRep.Text;

        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_INFO_FAUNA_ECOTERR_REPT");

        Visualizar("EIV_INFO_FAUNA_ECOTERR_REPT", grvInfoFaunaRep);

    }

    protected void grvInfoFaunaRep_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_INFO_FAUNA_ECOTERR_REPT", e.RowIndex);
        Visualizar("EIV_INFO_FAUNA_ECOTERR_REPT", grvInfoFaunaRep);
    }

    #endregion

    #region Aves
    protected void cargarTipoEspecieFaunaAve()
    {
        cboEspIntInfoFaunaAve.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoEspeciesFauna();
        cboEspIntInfoFaunaAve.DataValueField = "ETF_ID";
        cboEspIntInfoFaunaAve.DataTextField = "ETF_TIPO_ESPECIE";
        cboEspIntInfoFaunaAve.DataBind();
        cboEspIntInfoFaunaAve.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    } 

    protected void btnNuevoInfoFaunaAve_Click(object sender, EventArgs e)
    {
        plhInfoFaunaAve.Visible = true;
        plhInfoFaunaAve.Focus();

    }
    protected void btnCancelarInfoFaunaAve_Click(object sender, EventArgs e)
    {
        plhInfoFaunaAve.Visible = false;
    }

    protected void btnAgregarInfoFaunaAve_Click(object sender, EventArgs e)
    {
        GuardarInfoFaunaAve();
        limpiarPlaceHolder(plhInfoFaunaAve);
    }

    private void GuardarInfoFaunaAve()
    {
        CargarTabla("EIH_INFO_FAUNA_ECOTERR_AVES");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EFA_FECHA_CREACION"] = Fecha;
        dr["ETF_ID"] = this.cboEspIntInfoFaunaAve.SelectedValue;
        dr["EFA_NOMBRE_COMUN"] = this.txtNomComInfoFaunaAve.Text;
        dr["EFA_NOMBRE_CIENTF"] = this.txtNomCientInfoFaunaAve.Text;
        dr["EFA_TIPO_FUENTE"] = this.cboTipFuentInfoFaunaAve.SelectedValue;
        dr["EFA_FUENTE"] = this.txtFuenteInfoFaunaAve.Text;

        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_INFO_FAUNA_ECOTERR_AVES");

        Visualizar("EIV_INFO_FAUNA_ECOTERR_AVES", grvInfoFaunaAve);

    }
    protected void grvInfoFaunaAve_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_INFO_FAUNA_ECOTERR_AVES", e.RowIndex);
        Visualizar("EIV_INFO_FAUNA_ECOTERR_AVES", grvInfoFaunaAve);
    }

    #endregion

    #region Mamíferos

    protected void btnNuevoInfoFaunaMam_Click(object sender, EventArgs e)
    {
        plhInfoFaunaMam.Visible = true;
        plhInfoFaunaMam.Focus();

    }
    protected void btnCancelarInfoFaunaMam_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfoFaunaMam);
        plhInfoFaunaMam.Visible = false;
    }
    protected void cargarTipoEspecieFaunaMan()
    {
        cboEspIntInfoFaunaMam.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoEspeciesFauna();
        cboEspIntInfoFaunaMam.DataValueField = "ETF_ID";
        cboEspIntInfoFaunaMam.DataTextField = "ETF_TIPO_ESPECIE";
        cboEspIntInfoFaunaMam.DataBind();
        cboEspIntInfoFaunaMam.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnAgregarInfoFaunaMam_Click(object sender, EventArgs e)
    {
        GuardarInfoFaunaMam();
        limpiarPlaceHolder(plhInfoFaunaMam);
    }

    private void GuardarInfoFaunaMam()
    {
        CargarTabla("EIH_INFO_FAUNA_ECOTERR_MAMIF");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EFM_FECHA_CREACION"] = Fecha;
        dr["ETF_ID"] = this.cboEspIntInfoFaunaMam.SelectedValue;
        dr["EFM_NOMBRE_COMUN"] = this.txtNomComInfoFaunaMam.Text;
        dr["EFM_NOMBRE_CIENTF"] = this.txtNomCientInfoFaunaMam.Text;
        dr["EFM_TIPO_FUENTE"] = this.cboTipFuentInfoFaunaMam.SelectedValue;
        dr["EFM_FUENTE"] = this.txtFuenteInfoFaunaMam.Text;

        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_INFO_FAUNA_ECOTERR_MAMIF");

        Visualizar("EIV_INFO_FAUNA_ECOTERR_MAMIF", grvInfoFaunaMam);
    }

    protected void grvInfoFaunaMam_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_INFO_FAUNA_ECOTERR_MAMIF", e.RowIndex);
        Visualizar("EIV_INFO_FAUNA_ECOTERR_MAMIF", grvInfoFaunaMam);
    }

    #endregion

    #region 3.2.2 Ecosistema Acuático Continental
    protected void btnNuevoEcoAcuaCont_Click(object sender, EventArgs e)
    {
        plhEcoAcuaCont.Visible = true;
        plhEcoAcuaCont.Focus();
    }
    protected void btnCancelarEcoAcuaCont_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhEcoAcuaCont);
        this.ctrCoorEcoAcuaCont.CoorEste = "";
        this.ctrCoorEcoAcuaCont.CoorNorte = "";
        plhEcoAcuaCont.Visible = false;
    }

    protected void btnAgregarEcoAcuaCont_Click(object sender, EventArgs e)
    {
        if (this.ctrCoorEcoAcuaCont.Valido)
        {
            GuardarEcoAcuaCont();
            limpiarPlaceHolder(plhEcoAcuaCont);
            this.ctrCoorEcoAcuaCont.CoorEste = "";
            this.ctrCoorEcoAcuaCont.CoorNorte = "";
        }
    }
    private void GuardarEcoAcuaCont()
    {
        CargarTabla("EIH_SITIO_MUESTREO_ECOSIST_ACUATICO");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"]=IDProyecto;
        dr["EEA_FECHA_CREACION"] = Fecha;
        dr["EEA_SISTEMA_MUESTREADO"] = this.txtSistMuestEcoAcuaCont.Text;
        dr["EEA_TIPO_SUSTRAT_PREDOM"] = this.txtTtpSustPredEcoAcuaCont.Text;
        dr["EEA_NOMB_DREN_SIST_MUEST"] = this.txtNomDrenEcoAcuaCont.Text;
        dr["EEA_CATEG_PROTECCION"] = this.txtCatProtecEcoAcuaCont.Text;
        dr["EEA_COOR_NORTE_UBIC"] = this.ctrCoorEcoAcuaCont.CoorNorte;
        dr["EEA_COOR_ESTE_UBIC"] = this.ctrCoorEcoAcuaCont.CoorEste;

        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_SITIO_MUESTREO_ECOSIST_ACUATICO");

        Visualizar("EIH_SITIO_MUESTREO_ECOSIST_ACUATICO", grvEcoAcuaCont);

    }
    protected void grvEcoAcuaCont_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_SITIO_MUESTREO_ECOSIST_ACUATICO",e.RowIndex);
        Visualizar("EIH_SITIO_MUESTREO_ECOSIST_ACUATICO", grvEcoAcuaCont);
    }

    #endregion

    #region 3.2.2.1 Sistemas Lóticos

    private void cargarTipoBiot()
    {
        CargarCombo("ETB_ACTIVO", "EIB_TIPO_BIOTA","ETB_ID","ETB_TIPO_BIOTA", cboTipoBiotaSistLoticos);
        CargarCombo("ETB_ACTIVO", "EIB_TIPO_BIOTA", "ETB_ID", "ETB_TIPO_BIOTA", cboTipoBiotaSistLenticos);
        
    }

    protected void btnNuevoSistLoticos_Click(object sender, EventArgs e)
    {
        plhSistLoticos.Visible = true;
        plhSistLoticos.Focus();
    }
    protected void btnCancelarSistLoticos_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhSistLoticos);
        plhSistLoticos.Visible = false;
    }
    protected void btnAgregarSistLoticos_Click(object sender, EventArgs e)
    {
        GuardarSistLoticos();
        limpiarPlaceHolder(plhSistLoticos);
    }
    private void GuardarSistLoticos()
    {
        CargarTabla("EIH_SIST_LOTICO_ECOACUATICO");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;        
        dr["ELA_FECHA_CREACION"] = Fecha;
        dr["ETB_ID"] = this.cboTipoBiotaSistLoticos.SelectedValue;
        dr["ELA_NOMBRE_COMUN"] = this.txtNomComSistLoticos.Text;
        dr["ELA_NOMBRE_CIENTF"] = this.txtNomCientSistLoticos.Text;
        dr["ELA_GRUPO"] = this.txtGrupoSistLoticos.Text;
        dr["ELA_PORCENTAJE"] = this.txtPorcSistLoticos.Text;
        dr["ELA_BIOINDICACION"] = this.txtBioSistLoticos.Text;
        dr["ELA_FUENTE"] = this.txtFuenteSistLoticos.Text;

        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_SIST_LOTICO_ECOACUATICO");

        Visualizar("EIV_SIST_LOTICO_ECOACUATICO", grvSistLoticos);

    }

    protected void grvSistLoticos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_SIST_LOTICO_ECOACUATICO", e.RowIndex);
        Visualizar("EIV_SIST_LOTICO_ECOACUATICO", grvSistLoticos);
    }


    #endregion

    #region 3.2.2.2 Sistemas Lénticos
    protected void btnNuevoSistLenticos_Click(object sender, EventArgs e)
    {
        plhSistLenticos.Visible = true;
        plhSistLenticos.Focus();

    }
    protected void btnCancelarSistLenticos_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhSistLenticos);
        plhSistLenticos.Visible = false;
    }

    protected void btnAgregarSistLenticos_Click(object sender, EventArgs e)
    {
        GuardarSistLenticos();
        limpiarPlaceHolder(plhSistLenticos);
    }

    private void GuardarSistLenticos()
    {
        CargarTabla("EIH_SIST_LENTICO_ECOACUATICO");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["ESL_FECHA_CREACION"] = Fecha;
        dr["ETB_ID"] = this.cboTipoBiotaSistLenticos.SelectedValue;
        dr["ESL_NOMBRE_COMUN"] = this.txtNomComSistLenticos.Text;
        dr["ESL_NOMBRE_CIENTF"] = this.txtNomCientSistLenticos.Text;
        dr["ESL_GRUPO"] = this.txtGrupoSistLenticos.Text;
        dr["ESL_PORCENTAJE"] = this.txtPorcSistLenticos.Text;
        dr["ESL_BIOINDICACION"] = this.txtBioSistLenticos.Text;
        dr["ESL_FUENTE"] = this.txtFuenteSistLenticos.Text;

        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_SIST_LENTICO_ECOACUATICO");

        Visualizar("EIV_SIST_LENTICO_ECOACUATICO", grvSistLenticos);
    }

    protected void grvSistLenticos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_SIST_LENTICO_ECOACUATICO", e.RowIndex);
        Visualizar("EIV_SIST_LENTICO_ECOACUATICO", grvSistLenticos);
    }

    #endregion

    #region 3.2.3.1 Fauna - Corales

    private void CargarComboEspeciesInteres()
    {
        CargarCombo("ETM_ACTIVO", "EIB_TIPO_ESPECIE_MARINA", "ETM_ID", "ETM_TIPO_ESPECIE_MARINA", cboEspIntInfCoral);
        CargarCombo("ETM_ACTIVO", "EIB_TIPO_ESPECIE_MARINA", "ETM_ID", "ETM_TIPO_ESPECIE_MARINA", cboEspIntInfBentos);
        CargarCombo("ETM_ACTIVO", "EIB_TIPO_ESPECIE_MARINA", "ETM_ID", "ETM_TIPO_ESPECIE_MARINA", cboEspIntInfZooplancton);
        CargarCombo("ETM_ACTIVO", "EIB_TIPO_ESPECIE_MARINA", "ETM_ID", "ETM_TIPO_ESPECIE_MARINA", cboEspIntInfIctiofauna);
        CargarCombo("ETM_ACTIVO", "EIB_TIPO_ESPECIE_MARINA", "ETM_ID", "ETM_TIPO_ESPECIE_MARINA", cboEspIntInfFloraMar);                        
        
    }

    private void CargarComboEcosistema()
    {
        CargarCombo("EEM_ACTIVO", "EIB_TIPO_ECO_MARINO", "EEM_ID", "EEM_TIPO_ECO_MARINO", cboEcoMarino);
        CargarCombo("EEM_ACTIVO", "EIB_TIPO_ECO_MARINO", "EEM_ID", "EEM_TIPO_ECO_MARINO", cboEcosistemaBentos);
        CargarCombo("EEM_ACTIVO", "EIB_TIPO_ECO_MARINO", "EEM_ID", "EEM_TIPO_ECO_MARINO", cboEcoZOO);
        CargarCombo("EEM_ACTIVO", "EIB_TIPO_ECO_MARINO", "EEM_ID", "EEM_TIPO_ECO_MARINO", cboEcoIcti);
        CargarCombo("EEM_ACTIVO", "EIB_TIPO_ECO_MARINO", "EEM_ID", "EEM_TIPO_ECO_MARINO", cboEcoFlora);  
    }      

    protected void btnNuevoInfCoral_Click(object sender, EventArgs e)
    {
        plhInfCoral.Visible = true;
        plhInfCoral.Focus();
    }
    protected void btnCancelarInfCoral_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfCoral);
        plhInfCoral.Visible = false;
    }

    protected void btnAgregarInfCoral_Click(object sender, EventArgs e)
    {
        GuardarInfCoral();
        limpiarPlaceHolder(plhInfCoral);
    }

    private void GuardarInfCoral()
    {
        CargarTabla("EIH_CORALES_ECOMARINO");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["ECE_FECHA_CREACION"] = Fecha;
        dr["ETM_ID"] = this.cboEspIntInfCoral.SelectedValue;
        dr["ECE_NOMBRE_COMUN"] = this.txtNomComInfCoral.Text;
        dr["ECE_NOMBRE_CIENTIF"] = this.txtNomCientInfCoral.Text;
        dr["EEM_ID"] = this.cboEcoMarino.SelectedValue;
        dr["ECE_IMP_ECOLOGICA"] = this.txtImpEcoInfCoral.Text;
        dr["ECE_IMP_ECONOMICA"] = this.txtImpEconInfCoral.Text;        

        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_CORALES_ECOMARINO");

        Visualizar("EIV_CORALES_ECOMARINO", grvInfCoral);
    }

    protected void grvInfCoral_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_CORALES_ECOMARINO", e.RowIndex);
        Visualizar("EIV_CORALES_ECOMARINO", grvInfCoral);
    }

    #endregion

    #region 3.2.3.1 Fauna - Bentos
    protected void btnNuevoInfBentos_Click(object sender, EventArgs e)
    {
        plhInfBentos.Visible = true;
        plhInfBentos.Focus();
    }
    protected void btnCancelarInfBentos_Click(object sender, EventArgs e)
    {
        
        plhInfBentos.Visible = false;
    }

    protected void btnAgregarInfBentos_Click(object sender, EventArgs e)
    {
        GuardarInfBentos();
        limpiarPlaceHolder(plhInfBentos);
    }

    private void GuardarInfBentos()
    {
        CargarTabla("EIH_BENTOS_ECOMARINO");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EBE_FECHA_CREACION"] = Fecha;
        dr["ETM_ID"] = this.cboEspIntInfBentos.SelectedValue;
        dr["EBE_NOMBRE_COMUN"] = this.txtNomComInfBentos.Text;
        dr["EBE_NOMBRE_CIENTIF"] = this.txtNomCientInfBentos.Text;
        dr["EEM_ID"] = this.cboEcosistemaBentos.SelectedValue;
        dr["EBE_IMP_ECOLOGICA"] = this.txtImpEcoInfBentos.Text;
        dr["EBE_IMP_ECONOMICA"] = this.txtImpEconInfBentos.Text;

        dsGrilla.Tables[0].Rows.Add(dr);        

        Contexto.guardarTabla(dsGrilla, "EIH_BENTOS_ECOMARINO");

        Visualizar("EIV_BENTOS_ECOMARINO", grvInfBentos);
    }

    protected void grvInfBentos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_BENTOS_ECOMARINO", e.RowIndex);
        Visualizar("EIV_BENTOS_ECOMARINO", grvInfBentos);
    }    
    #endregion

    #region  3.2.3.1 Fauna - Zooplancton

    protected void btnNuevoInfZooplancton_Click(object sender, EventArgs e)
    {
        plhInfZooplancton.Visible = true;
        plhInfZooplancton.Focus();
    }
    protected void btnCancelarInfZooplancton_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfZooplancton);
        plhInfZooplancton.Visible = false;
    }

    protected void btnAgregarInfZooplancton_Click(object sender, EventArgs e)
    {
        GuardarInfoZoo();
        limpiarPlaceHolder(plhInfZooplancton);
    }
    private void GuardarInfoZoo()
    {
        CargarTabla("EIH_ZOOPLANCTON_ECOMARINO");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EZE_FECHA_CREACION"] = Fecha;
        dr["ETM_ID"] = this.cboEspIntInfZooplancton.SelectedValue;
        dr["EZE_NOMBRE_COMUN"] = this.txtNomComInfZooplancton.Text;
        dr["EZE_NOMBRE_CIENTIF"] = this.txtNomCientInfZooplancton.Text;
        dr["EEM_ID"] = this.cboEcoZOO.SelectedValue;
        dr["EZE_IMP_ECOLOGICA"] = this.txtImpEcoInfZooplancton.Text;
        dr["EZE_IMP_ECONOMICA"] = this.txtImpEconInfZooplancton.Text;

        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_ZOOPLANCTON_ECOMARINO");

        Visualizar("EIV_ZOOPLANCTON_ECOMARINO", grvInfZooplancton);
    }

    protected void grvInfZooplancton_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_ZOOPLANCTON_ECOMARINO", e.RowIndex);
        Visualizar("EIV_ZOOPLANCTON_ECOMARINO", grvInfZooplancton);
    }
    
    #endregion

    #region 3.2.3.1 Fauna - Ictiofauna

    protected void btnNuevoInfIctiofauna_Click(object sender, EventArgs e)
    {
        plhInfIctiofauna.Visible = true;
        plhInfIctiofauna.Focus();
    }
    protected void btnCancelarInfIctiofauna_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfIctiofauna);
        plhInfIctiofauna.Visible = false;
    }

    protected void btnAgregarInfIctiofauna_Click(object sender, EventArgs e)
    {
        GuardarInfoIcti();
        limpiarPlaceHolder(plhInfIctiofauna);
    }
    private void GuardarInfoIcti()
    {
        CargarTabla("EIH_ICTIOFAUNA_ECOMARINO");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EIE_FECHA_CREACION"] = Fecha;
        dr["ETM_ID"] = this.cboEspIntInfIctiofauna.SelectedValue;
        dr["EIE_NOMBRE_COMUN"] = this.txtNomComInfIctiofauna.Text;
        dr["EIE_NOMBRE_CIENTIF"] = this.txtNomCientInfIctiofauna.Text;
        dr["EEM_ID"] = this.cboEcoIcti.SelectedValue;
        dr["EIE_IMP_ECOLOGICA"] = this.txtImpEcoInfIctiofauna.Text;
        dr["EIE_IMP_ECONOMICA"] = this.txtImpEconInfIctiofauna.Text;

        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_ICTIOFAUNA_ECOMARINO");

        Visualizar("EIV_ICTIOFAUNA_ECOMARINO", grvInfIctiofauna);
    }

    protected void grvInfIctiofauna_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_ICTIOFAUNA_ECOMARINO", e.RowIndex);
        Visualizar("EIV_ICTIOFAUNA_ECOMARINO", grvInfIctiofauna);
    }

    #endregion

    #region 3.2.3.2 Flora
    protected void btnNuevoInfFloraMar_Click(object sender, EventArgs e)
    {
        plhInfFloraMar.Visible = true;
        plhInfFloraMar.Focus();
    }

    protected void btnCancelarInfFloraMar_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfFloraMar);
        plhInfFloraMar.Visible = false;
    }

    protected void btnAgregarInfFloraMar_Click(object sender, EventArgs e)
    {
        GuardarInfFlora();
        limpiarPlaceHolder(plhInfFloraMar);
    }

    private void GuardarInfFlora()
    {
        CargarTabla("EIH_FLORA_ECOMARINO");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EFE_FECHA_CREACION"] = Fecha;
        dr["ETM_ID"] = this.cboEspIntInfFloraMar.SelectedValue;
        dr["EFE_NOMBRE_COMUN"] = this.txtNomComInfFloraMar.Text;
        dr["EFE_NOMBRE_CIENTIF"] = this.txtNomCientInfFloraMar.Text;
        dr["EEM_ID"] = this.cboEcoFlora.SelectedValue;
        dr["EFE_IMP_ECOLOGICA"] = this.txtImpEcoInfFloraMar.Text;
        dr["EFE_IMP_ECONOMICA"] = this.txtImpEconInfFloraMar.Text;

        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_FLORA_ECOMARINO");

        Visualizar("EIV_FLORA_ECOMARINO", grvInfFloraMar);
    }

    protected void grvInfFloraMar_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_FLORA_ECOMARINO", e.RowIndex);
        Visualizar("EIV_FLORA_ECOMARINO", grvInfFloraMar);
    }    
    #endregion    

    #region 3.3.1 Área de Influencia Directa

    private void CargarComboUnidadPolAdmin()
    {
        CargarCombo("EPA_ACTIVO", "EIB_UNIDAD_POL_ADMIN", "EPA_ID", "EPA_UNIDAD_POL_ADMIN", cboUnidPolAreaInfdir);
    }

    protected void btnNuevoAreaInfDir_Click(object sender, EventArgs e)
    {
        plhAreaInfdir.Visible = true;
        plhAreaInfdir.Focus();
    }
    protected void btnCancelarAreaInfdir_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhAreaInfdir);
        plhAreaInfdir.Visible = false;
    }

    protected void btnAgregarAreaInfdir_Click(object sender, EventArgs e)
    {
        GuardarAreasInfDirec();
        limpiarPlaceHolder(plhAreaInfdir);
    }

    private void GuardarAreasInfDirec()
    {
        CargarTabla("EIH_AREA_INFLUENCIA_DIRECTA");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EAI_FECHA_CREACION"] = Fecha;
        dr["EPA_ID"] = this.cboUnidPolAreaInfdir.SelectedValue;
        dr["EAI_NOMBRE"] = this.txtNomUnidAreaInfdir.Text;
        dr["EAI_MUNICIPIO"] = this.txtMunicipioAreaInfdir.Text;        

        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_AREA_INFLUENCIA_DIRECTA");
        VisualizarAreaInfluencia();
    }


    private void VisualizarAreaInfluencia()
    {
        Visualizar("EIV_AREA_INFLUENCIA_DIRECTA", grvAreaInfdir);
        cboAreas1.DataSource = dsGrilla;
        cboAreas1.DataValueField = "EAI_ID";
        cboAreas1.DataTextField = "EAI_NOMBRE";
        cboAreas1.DataBind();
        cboAreas1.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas2.DataSource = dsGrilla;
        cboAreas2.DataValueField = "EAI_ID";
        cboAreas2.DataTextField = "EAI_NOMBRE";
        cboAreas2.DataBind();
        cboAreas2.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas3.DataSource = dsGrilla;
        cboAreas3.DataValueField = "EAI_ID";
        cboAreas3.DataTextField = "EAI_NOMBRE";
        cboAreas3.DataBind();
        cboAreas3.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas4.DataSource = dsGrilla;
        cboAreas4.DataValueField = "EAI_ID";
        cboAreas4.DataTextField = "EAI_NOMBRE";
        cboAreas4.DataBind();
        cboAreas4.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas5.DataSource = dsGrilla;
        cboAreas5.DataValueField = "EAI_ID";
        cboAreas5.DataTextField = "EAI_NOMBRE";
        cboAreas5.DataBind();
        cboAreas5.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas6.DataSource = dsGrilla;
        cboAreas6.DataValueField = "EAI_ID";
        cboAreas6.DataTextField = "EAI_NOMBRE";
        cboAreas6.DataBind();
        cboAreas6.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas7.DataSource = dsGrilla;
        cboAreas7.DataValueField = "EAI_ID";
        cboAreas7.DataTextField = "EAI_NOMBRE";
        cboAreas7.DataBind();
        cboAreas7.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas8.DataSource = dsGrilla;
        cboAreas8.DataValueField = "EAI_ID";
        cboAreas8.DataTextField = "EAI_NOMBRE";
        cboAreas8.DataBind();
        cboAreas8.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas9.DataSource = dsGrilla;
        cboAreas9.DataValueField = "EAI_ID";
        cboAreas9.DataTextField = "EAI_NOMBRE";
        cboAreas9.DataBind();
        cboAreas9.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas10.DataSource = dsGrilla;
        cboAreas10.DataValueField = "EAI_ID";
        cboAreas10.DataTextField = "EAI_NOMBRE";
        cboAreas10.DataBind();
        cboAreas10.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas11.DataSource = dsGrilla;
        cboAreas11.DataValueField = "EAI_ID";
        cboAreas11.DataTextField = "EAI_NOMBRE";
        cboAreas11.DataBind();
        cboAreas11.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas12.DataSource = dsGrilla;
        cboAreas12.DataValueField = "EAI_ID";
        cboAreas12.DataTextField = "EAI_NOMBRE";
        cboAreas12.DataBind();
        cboAreas12.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas13.DataSource = dsGrilla;
        cboAreas13.DataValueField = "EAI_ID";
        cboAreas13.DataTextField = "EAI_NOMBRE";
        cboAreas13.DataBind();
        cboAreas13.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas14.DataSource = dsGrilla;
        cboAreas14.DataValueField = "EAI_ID";
        cboAreas14.DataTextField = "EAI_NOMBRE";
        cboAreas14.DataBind();
        cboAreas14.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboAreas15.DataSource = dsGrilla;
        cboAreas15.DataValueField = "EAI_ID";
        cboAreas15.DataTextField = "EAI_NOMBRE";
        cboAreas15.DataBind();
        cboAreas15.Items.Insert(0, new ListItem("Seleccione...", "-1"));    
        
    }
    protected void grvAreaInfdir_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_AREA_INFLUENCIA_DIRECTA", e.RowIndex);
        VisualizarAreaInfluencia();
    }

    #endregion    

    #region Territorios Étnicos

    protected void btnNuevoTerrEtnico_Click(object sender, EventArgs e)
    {
        plhTerrEtnico.Visible = true;
        plhTerrEtnico.Focus();
    }

    protected void btnCancelarTerrEtnico_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhTerrEtnico);
        plhTerrEtnico.Visible = false;
    }

    protected void btnAgregarTerrEtnico_Click(object sender, EventArgs e)
    {
        GuardarTerritorioEtnico();
    }

    private void GuardarTerritorioEtnico()
    {
        CargarTabla("EIH_TERR_ETNICO_INFLUENCIA");
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EEI_FECHA_CREACION"] = Fecha;
        dr["EEI_NOMBRE_TERR_ETNICO"] = this.txtNombTerrEtnico.Text;
        dr["EEI_MUNICIPIO"] = this.txtMunicipioTerrEtnico.Text;

        dsGrilla.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsGrilla, "EIH_TERR_ETNICO_INFLUENCIA");

        Visualizar("EIH_TERR_ETNICO_INFLUENCIA", grvTerrEtnico);
    }

    protected void grvTerrEtnico_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_TERR_ETNICO_INFLUENCIA", e.RowIndex);
        Visualizar("EIH_TERR_ETNICO_INFLUENCIA", grvTerrEtnico);
    }

    #endregion

    #region 3.3.2 Dimensión Demográfica Area de Influencia Directa


    private void CargarComboTiposPoblacion()
    {
        CargarCombo("ETO_ACTIVO", "EIB_TIPO_OTRA_POBLACION", "ETO_ID", "ETO_TIPO_OTRA_POBLACION", cboOtrosPobDimDemo);
    }    

    protected void btnNuevoDimDemo_Click(object sender, EventArgs e)
    {
        plhDimDemo.Visible = true;
        plhDimDemo.Focus();
    }
    protected void btnCancelarDimDemo_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhDimDemo);
        plhDimDemo.Visible = false;
    }

    protected void cboOtrosPobDimDemo_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.txtOtroPobDimDemo.Visible = false;
        this.txtOtroPobDimDemo.Text = "";
        if (this.cboOtrosPobDimDemo.SelectedValue != "-1")
        {
            if (this.cboOtrosPobDimDemo.SelectedItem.Text.Contains("Otro"))
            {
                this.txtOtroPobDimDemo.Visible = true;
            }
        }
    }

    protected void btnAgregarDimDemo_Click(object sender, EventArgs e)
    {
        GuardarDimDemo();
        limpiarPlaceHolder(plhDimDemo);
    }

    private void GuardarDimDemo()
    {
        CargarTabla("EIH_DIMENSION_DEMOGRAFICA", "EAI_ID", this.cboAreas1.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EDD_NO_HABITANTES"] = this.txtNoHabitDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_PORC_INDIGENAS"] = this.txtIndigPobDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_PORC_AFROD"] = this.txtAfroPobDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_PORC_COLONOS"] = this.txtColonPobDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_PORC_CAMPESINOS"] = this.txtCampPobDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["ETO_ID"] = this.cboOtrosPobDimDemo.SelectedValue;
            if (this.txtOtroPobDimDemo.Text == "")
                dsGrilla.Tables[0].Rows[0]["ETO_OTRO"] = DBNull.Value;
            else
                dsGrilla.Tables[0].Rows[0]["ETO_OTRO"] = this.txtOtroPobDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["ETO_ID"] = this.cboOtrosPobDimDemo.SelectedValue;
            dsGrilla.Tables[0].Rows[0]["EDD_PORC_MASCULINO"] = this.txtMascDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_PORC_FEMENINO"] = this.txtFemDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_PORC_TEND_CRECIMI"] = this.txtTenCreDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_PORC_TASA_NATAL"] = this.txtTasNatDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_NO_FAMILIAS"] = this.txtNoFamDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_NO_0_5"] = this.txt0_5AniosDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_NO_6_12"] = this.txt6_12AniosDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_NO_13_50"] = this.txt13_50AniosDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_NO_51_60"] = this.txt51_60AniosDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_NO_60"] = this.txt60AniosDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_PORC_TASA_MORTAL"] = this.txtTasaMortDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_PORC_TASA_NBI"] = this.txtTasaNbiDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_PORC_POBL_EC_ACTI"] = this.txtPobActDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_PORC_INDIC_CALIDAD_V"] = this.txtIndCalDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_PORC_MORBILIDAD"] = this.txtPorMorbDimDemo.Text;
            dsGrilla.Tables[0].Rows[0]["EDD_DESC_ENFERMEDADES"] = this.txtDescEnfDimDemo.Text;
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();            
            dr["EAI_ID"] = this.cboAreas1.SelectedValue;
            dr["EDD_NO_HABITANTES"] = this.txtNoHabitDimDemo.Text;
            dr["EDD_PORC_INDIGENAS"] = this.txtIndigPobDimDemo.Text;
            dr["EDD_PORC_AFROD"] = this.txtAfroPobDimDemo.Text;
            dr["EDD_PORC_COLONOS"] = this.txtColonPobDimDemo.Text;
            dr["EDD_PORC_CAMPESINOS"] = this.txtCampPobDimDemo.Text;
            dr["ETO_ID"] = this.cboOtrosPobDimDemo.SelectedValue;
            if (this.txtOtroPobDimDemo.Text == "")
                dr["ETO_OTRO"] = DBNull.Value;
            else
                dr["ETO_OTRO"] = this.txtOtroPobDimDemo.Text;
            dr["ETO_ID"] = this.cboOtrosPobDimDemo.SelectedValue;
            dr["EDD_PORC_MASCULINO"] = this.txtMascDimDemo.Text;
            dr["EDD_PORC_FEMENINO"] = this.txtFemDimDemo.Text;
            dr["EDD_PORC_TEND_CRECIMI"] = this.txtTenCreDimDemo.Text;
            dr["EDD_PORC_TASA_NATAL"] = this.txtTasNatDimDemo.Text;
            dr["EDD_NO_FAMILIAS"] = this.txtNoFamDimDemo.Text;
            dr["EDD_NO_0_5"] = this.txt0_5AniosDimDemo.Text;
            dr["EDD_NO_6_12"] = this.txt6_12AniosDimDemo.Text;
            dr["EDD_NO_13_50"] = this.txt13_50AniosDimDemo.Text;
            dr["EDD_NO_51_60"] = this.txt51_60AniosDimDemo.Text;
            dr["EDD_NO_60"] = this.txt60AniosDimDemo.Text;
            dr["EDD_PORC_TASA_MORTAL"] = this.txtTasaMortDimDemo.Text;
            dr["EDD_PORC_TASA_NBI"] = this.txtTasaNbiDimDemo.Text;
            dr["EDD_PORC_POBL_EC_ACTI"] = this.txtPobActDimDemo.Text;
            dr["EDD_PORC_INDIC_CALIDAD_V"] = this.txtIndCalDimDemo.Text;
            dr["EDD_PORC_MORBILIDAD"] = this.txtPorMorbDimDemo.Text;
            dr["EDD_DESC_ENFERMEDADES"] = this.txtDescEnfDimDemo.Text;
            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_DIMENSION_DEMOGRAFICA");

        Visualizar("EIV_DIMENSION_DEMOGRAFICA", grvDimDemo);
        Visualizar("EIV_DIMENSION_DEMOGRAFICA", grvDimDemo2);
    }

    protected void grvDimDemo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Visualizar("EIV_DIMENSION_DEMOGRAFICA", grvDimDemo);
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_DIMENSION_DEMOGRAFICA", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_DIMENSION_DEMOGRAFICA");
        Visualizar("EIV_DIMENSION_DEMOGRAFICA", grvDimDemo);
        Visualizar("EIV_DIMENSION_DEMOGRAFICA", grvDimDemo2);
    }
    protected void grvDimDemo2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Visualizar("EIV_DIMENSION_DEMOGRAFICA", grvDimDemo);
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_DIMENSION_DEMOGRAFICA", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_DIMENSION_DEMOGRAFICA");
        Visualizar("EIV_DIMENSION_DEMOGRAFICA", grvDimDemo);
        Visualizar("EIV_DIMENSION_DEMOGRAFICA", grvDimDemo2);
    }

    #endregion

    #region 3.3.3 Dimensión Espacial Area de Infuencia Directa
    protected void btnNuevoDimEspac_Click(object sender, EventArgs e)
    {
        plhDimEspac.Visible = true;
        plhDimEspac.Focus();
    }
    protected void btnCancelarDimEspac_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhDimEspac);
        plhDimEspac.Visible = false;
    }    

    protected void btnAgregarDimEspac_Click(object sender, EventArgs e)
    {
        GuardarDimensionEspacial();
        limpiarPlaceHolder(plhDimEspac);
    }

    private void GuardarDimensionEspacial()
    {
        CargarTabla("EIH_DIMENSION_ESPACIAL", "EAI_ID", this.cboAreas2.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EDE_NO_VIVIENDAS"] = this.txtNoVivDimeEcono.Text;
            dsGrilla.Tables[0].Rows[0]["EDE_PORC_ENERGIA_ELECTRICA"] = this.txtPEnerElecDimEspac.Text;
            dsGrilla.Tables[0].Rows[0]["EDE_PORC_ACUE_OTRO_SIST"] = this.txtPAcuedDimEspac.Text;
            dsGrilla.Tables[0].Rows[0]["EDE_PORC_ALCANTARILLADO"] = this.txtPAlcanDimEspac.Text;
            dsGrilla.Tables[0].Rows[0]["EDE_PORC_TELEFONIA"] = this.txtPTelefDimEspac.Text;
            dsGrilla.Tables[0].Rows[0]["EDE_PORC_DEF_VIVIENDA"] = this.txtPDefVivienDimEspac.Text;
            dsGrilla.Tables[0].Rows[0]["EDE_PORC_COBERT_SALUD"] = this.txtPCobSaludDimEspac.Text;
            dsGrilla.Tables[0].Rows[0]["EDE_PORC_COBERT_EDU"] = this.txtPCobEduDimEspac.Text;
            dsGrilla.Tables[0].Rows[0]["EDE_PORC_OTROS_SIST"] = this.txtPCobOtrosDimEspac.Text;
            dsGrilla.Tables[0].Rows[0]["EDE_VIAS_PRINCIPALES"] = this.txtViasPDimEspac.Text;
            dsGrilla.Tables[0].Rows[0]["EDE_VIAS_SECUNDARIAS"] = this.txtViasSDimEspac.Text;
            dsGrilla.Tables[0].Rows[0]["EDE_VIAS_TERCIARIAS"] = this.txtViasTDimEspac.Text;            
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas2.SelectedValue;
            dr["EDE_NO_VIVIENDAS"] = this.txtNoVivDimeEcono.Text;
            dr["EDE_PORC_ENERGIA_ELECTRICA"] = this.txtPEnerElecDimEspac.Text;
            dr["EDE_PORC_ACUE_OTRO_SIST"] = this.txtPAcuedDimEspac.Text;
            dr["EDE_PORC_ALCANTARILLADO"] = this.txtPAlcanDimEspac.Text;
            dr["EDE_PORC_TELEFONIA"] = this.txtPTelefDimEspac.Text;
            dr["EDE_PORC_DEF_VIVIENDA"] = this.txtPDefVivienDimEspac.Text;
            dr["EDE_PORC_COBERT_SALUD"] = this.txtPCobSaludDimEspac.Text;
            dr["EDE_PORC_COBERT_EDU"] = this.txtPCobEduDimEspac.Text;
            dr["EDE_PORC_OTROS_SIST"] = this.txtPCobOtrosDimEspac.Text;
            dr["EDE_VIAS_PRINCIPALES"] = this.txtViasPDimEspac.Text;
            dr["EDE_VIAS_SECUNDARIAS"] = this.txtViasSDimEspac.Text;
            dr["EDE_VIAS_TERCIARIAS"] = this.txtViasTDimEspac.Text;                     
            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_DIMENSION_ESPACIAL");

        Visualizar("EIV_DIMENSION_ESPACIAL", grvDimEspac);
    }


    protected void grvDimEspac_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_DIMENSION_ESPACIAL");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_DIMENSION_ESPACIAL", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_DIMENSION_ESPACIAL");
        Visualizar("EIV_DIMENSION_ESPACIAL", grvDimEspac);
    }
    #endregion

    #region  3.3.4 Dimensión Económica Area de Influencia Directa

    protected void btnNuevoDimEcono_Click(object sender, EventArgs e)
    {
        plhDimEcono.Visible = true;
        plhDimEcono.Focus();
    }
    protected void btnCancelarDimEcono_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhDimEcono);
        plhDimEcono.Visible = false;
    }

    protected void btnAgregarDimEcono_Click(object sender, EventArgs e)
    {
        GuardarDimensionEconocica();
        limpiarPlaceHolder(plhDimEcono);
    }

    private void GuardarDimensionEconocica()
    {
        CargarTabla("EIH_DIMENSION_ECONOMICA", "EAI_ID", this.cboAreas3.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EED_PORC_MINIFUNDIO"] = this.txtMinifunDimEcono.Text;
            dsGrilla.Tables[0].Rows[0]["EED_PORC_MED_PROPIEDAD"] = this.txtMedPropDimEcono.Text;
            dsGrilla.Tables[0].Rows[0]["EED_PORC_GRAN_PROPIEDAD"] = this.txtGranPDimEcono.Text;
            dsGrilla.Tables[0].Rows[0]["EED_PORC_ACTECO_PRIMER"] = this.txtPriOrdDimEcono.Text;
            dsGrilla.Tables[0].Rows[0]["EED_PORC_ACTECO_SEGUNDO"] = this.txtSegOrdDimEcono.Text;
            dsGrilla.Tables[0].Rows[0]["EED_PORC_ACTECO_TERCERO"] = this.txtTerOrdDimEcono.Text;
            dsGrilla.Tables[0].Rows[0]["EED_PORC_TASA_DESEMPLEO"] = this.txtTasaDesempDimEcono.Text;            
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas3.SelectedValue;
            dr["EED_PORC_MINIFUNDIO"] = this.txtMinifunDimEcono.Text;
            dr["EED_PORC_MED_PROPIEDAD"] = this.txtMedPropDimEcono.Text;
            dr["EED_PORC_GRAN_PROPIEDAD"] = this.txtGranPDimEcono.Text;
            dr["EED_PORC_ACTECO_PRIMER"] = this.txtPriOrdDimEcono.Text;
            dr["EED_PORC_ACTECO_SEGUNDO"] = this.txtSegOrdDimEcono.Text;
            dr["EED_PORC_ACTECO_TERCERO"] = this.txtTerOrdDimEcono.Text;
            dr["EED_PORC_TASA_DESEMPLEO"] = this.txtTasaDesempDimEcono.Text;         
            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_DIMENSION_ECONOMICA");

        Visualizar("EIV_DIMENSION_ECONOMICA", grvDimEcono);
    }

    protected void grvDimEcono_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_DIMENSION_ECONOMICA");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_DIMENSION_ECONOMICA", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_DIMENSION_ECONOMICA");
        Visualizar("EIV_DIMENSION_ECONOMICA", grvDimEcono);
    }

    #endregion

    #region 3.3.5.1 Información de Comunidades NO Étnicas

    protected void btnNuevoComNoEtn_Click(object sender, EventArgs e)
    {
        plhComNoEtn.Visible = true;
        plhComNoEtn.Focus();
    }
    protected void btnCancelarComNoEtn_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhComNoEtn);
        plhComNoEtn.Visible = false;
    }

    protected void btnAgregarComNoEtn_Click(object sender, EventArgs e)
    {
        GuardarNoEtnic();
        limpiarPlaceHolder(plhComNoEtn);
    }

    private void GuardarNoEtnic()
    {
        CargarTabla("EIH_HHISTORICOS_AREAINFL_DIRECTA", "EAI_ID", this.cboAreas4.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EHH_ACONT_HIST_HUMANOS"] = this.txtHistHumComNoEtn.Text;
            dsGrilla.Tables[0].Rows[0]["EHH_ACONT_HIST_NATURALES"] = this.txtHistNatComNoEtn.Text;            
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas4.SelectedValue;
            dr["EHH_ACONT_HIST_HUMANOS"] = this.txtHistHumComNoEtn.Text;
            dr["EHH_ACONT_HIST_NATURALES"] = this.txtHistNatComNoEtn.Text;                        
            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_HHISTORICOS_AREAINFL_DIRECTA");

        Visualizar("EIV_HHISTORICOS_AREAINFL_DIRECTA", grvComNoEtn);

        CargarTabla("EIH_PERCEP_VAL_CULTURAL", "EAI_ID", this.cboAreas4.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EPC_PERCEP_VAL_CULTURAL"] = this.txtPercValComNoEtn.Text;            
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas4.SelectedValue;
            dr["EPC_PERCEP_VAL_CULTURAL"] = this.txtPercValComNoEtn.Text;                        
            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_PERCEP_VAL_CULTURAL");

        Visualizar("EIV_PERCEP_VAL_CULTURAL", grvPerpCult);

    }


    protected void grvComNoEtn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_HHISTORICOS_AREAINFL_DIRECTA");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_HHISTORICOS_AREAINFL_DIRECTA", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_HHISTORICOS_AREAINFL_DIRECTA");
        Visualizar("EIV_HHISTORICOS_AREAINFL_DIRECTA", grvComNoEtn);
    }


    protected void grvPerpCult_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_PERCEP_VAL_CULTURAL");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_PERCEP_VAL_CULTURAL", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_PERCEP_VAL_CULTURAL");
        Visualizar("EIV_PERCEP_VAL_CULTURAL", grvPerpCult);
    }
    #endregion

    #region 3.3.5.2 Relaciones Culturales con el Entorno

    protected void btnNuevoRelCulEnt_Click(object sender, EventArgs e)
    {
        plhRelCulEnt.Visible = true;
        plhRelCulEnt.Focus();
    }
    protected void btnCancelarRelCulEnt_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhRelCulEnt);
        plhRelCulEnt.Visible = false;
    }

    protected void btnAgregarRelCulEnt_Click(object sender, EventArgs e)
    {
        GuardarRelCult();
        limpiarPlaceHolder(plhRelCulEnt);
    }

    private void GuardarRelCult()
    {
        CargarTabla("EIH_RELACIONES_CULTURALES", "EAI_ID", this.cboAreas4.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["ERC_USO_REC_NATURALES"] = this.txtUsoManRecNatRelCulEnt.Text;
            dsGrilla.Tables[0].Rows[0]["ERC_PRACTICAS_CULTURALES"] = this.txtPractConMarRelCulEnt.Text;            
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas5.SelectedValue;
            dr["ERC_USO_REC_NATURALES"] = this.txtUsoManRecNatRelCulEnt.Text;
            dr["ERC_PRACTICAS_CULTURALES"] = this.txtPractConMarRelCulEnt.Text;            
            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_RELACIONES_CULTURALES");

        Visualizar("EIV_RELACIONES_CULTURALES", grvRelCulEnt);
        
    }

    protected void grvRelCulEnt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_RELACIONES_CULTURALES");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_RELACIONES_CULTURALES", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_RELACIONES_CULTURALES");
        Visualizar("EIV_RELACIONES_CULTURALES", grvRelCulEnt);
    }

    #endregion

    #region 3.3.5.3 Información de Comunidades Étnicas
    protected void btnNuevoInfoComEtn_Click(object sender, EventArgs e)
    {
        plhInfoComEtn.Visible = true;
        plhInfoComEtn.Focus();
    }
    protected void btnCancelarInfoComEtn_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfoComEtn);
        plhInfoComEtn.Visible = false;
    }
    protected void btnAgregarInfoComEtn_Click(object sender, EventArgs e)
    {
        GuardarInfoComEnt();
        limpiarPlaceHolder(plhInfoComEtn);
    }

    private void GuardarInfoComEnt()
    {
        CargarTabla("EIH_INFO_COMUNIDADES_ETNICAS", "EAI_ID", this.cboAreas4.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EIC_NOMBRE"] = this.txtNombreInfoComEtn.Text;
            dsGrilla.Tables[0].Rows[0]["EIC_TERRITORIO"] = this.txtTerrInfoComEtn.Text;
            dsGrilla.Tables[0].Rows[0]["EIC_NO_HABITANTES"] = this.txtNoHabInfoComEtn.Text;
            dsGrilla.Tables[0].Rows[0]["EIC_SIST_ECONOMICO"] = this.txtSistEconoInfoComEtn.Text;
            dsGrilla.Tables[0].Rows[0]["EIC_PRES_INSTITUCIONAL"] = this.txtPreInstInfoComEtn.Text;
            dsGrilla.Tables[0].Rows[0]["EIC_ORG_COMUNITARIA"] = this.txtOrgComunInfoComEtn.Text;

        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas6.SelectedValue;
            dr["EIC_NOMBRE"] = this.txtNombreInfoComEtn.Text;
            dr["EIC_TERRITORIO"] = this.txtTerrInfoComEtn.Text;
            dr["EIC_NO_HABITANTES"] = this.txtNoHabInfoComEtn.Text;
            dr["EIC_SIST_ECONOMICO"] = this.txtSistEconoInfoComEtn.Text;
            dr["EIC_PRES_INSTITUCIONAL"] = this.txtPreInstInfoComEtn.Text;
            dr["EIC_ORG_COMUNITARIA"] = this.txtOrgComunInfoComEtn.Text;
            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_INFO_COMUNIDADES_ETNICAS");

        Visualizar("EIV_INFO_COMUNIDADES_ETNICAS", grvInfoComEtn);
    }

    protected void grvInfoComEtn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_INFO_COMUNIDADES_ETNICAS");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_INFO_COMUNIDADES_ETNICAS", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_INFO_COMUNIDADES_ETNICAS");
        Visualizar("EIV_INFO_COMUNIDADES_ETNICAS", grvInfoComEtn);
    }
    #endregion

    #region 3.3.5.4 Información de Grupos Indigenas Etnohistóricos

    protected void btnNuevoInfIndHist_Click(object sender, EventArgs e)
    {
        plhInfIndHist.Visible = true;
        plhInfIndHist.Focus();
    }
    protected void btnCancelarInfIndHist_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfIndHist);
        plhInfIndHist.Visible = false;
    }

    protected void btnAgregarInfIndHist_Click(object sender, EventArgs e)
    {
        CargarTabla("EIH_INFO_GRUPOS_INDIGENAS", "EAI_ID", this.cboAreas7.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EGI_NOMBRE"] = this.txtNombreInfIndHist.Text;            
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas7.SelectedValue;
            dr["EGI_NOMBRE"] = this.txtNombreInfIndHist.Text;                        
            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_INFO_GRUPOS_INDIGENAS");

        Visualizar("EIV_INFO_GRUPOS_INDIGENAS", grvInfIndHist);

    }

    protected void grvInfIndHist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_INFO_GRUPOS_INDIGENAS");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_INFO_GRUPOS_INDIGENAS", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_INFO_GRUPOS_INDIGENAS");
        Visualizar("EIV_INFO_GRUPOS_INDIGENAS", grvInfIndHist);
    }

    #endregion

    #region 3.3.5.5 Información de Sitios Arqueológicos con Anterioridad
    protected void btnNuevoSitArqAnt_Click(object sender, EventArgs e)
    {
        plhSitArqAnt.Visible = true;
        plhSitArqAnt.Focus();
    }

    protected void btnCancelarSitArqAnt_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhSitArqAnt);
        plhSitArqAnt.Visible = false;
    }

    protected void btnAgregarSitArqAnt_Click(object sender, EventArgs e)
    {
        if(this.ctrCoorSitArqAnt.Validar(1,5))
        {
            GuardarSitArqAnt();
            this.ctrCoorSitArqAnt.LimpiarObjetos();
            limpiarPlaceHolder(plhSitArqAnt);
        }
    }
    private void GuardarSitArqAnt()
    {
        CargarTabla("EIH_INFO_SITIOS_ARQUEOLOGICOS_ANT", "EAI_ID", this.cboAreas8.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["ESA_DESCRIPCION"] = this.txtDescSitArqAnt.Text;
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas8.SelectedValue;
            dr["ESA_DESCRIPCION"] = this.txtDescSitArqAnt.Text;
            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_INFO_SITIOS_ARQUEOLOGICOS_ANT");

        CargarTabla("EIH_INFO_SITIOS_ARQUEOLOGICOS_ANT", "EAI_ID", this.cboAreas8.SelectedValue);

        string esaId = dsGrilla.Tables[0].Rows[dsGrilla.Tables[0].Rows.Count - 1]["ESA_ID"].ToString();

        CargarTabla("EIH_COOR_SITIO_ARQ_ANT", "ESA_ID", esaId);

        if (dsGrilla.Tables[0].Rows.Count == 0)
        {
            foreach (DataRow row in this.ctrCoorSitArqAnt.Coordenadas.Rows)
            {
                DataRow dr2 = dsGrilla.Tables[0].NewRow();
                dr2["ESA_ID"] = esaId;
                dr2["ECA_COOR_NORTE"] = row["COORNORTE"];
                dr2["ECA_COOR_ESTE"] = row["COORESTE"];
                dsGrilla.Tables[0].Rows.Add(dr2);
            }
            Contexto.guardarTabla(dsGrilla, "EIH_COOR_SITIO_ARQ_ANT");
        }

        Visualizar("EIV_INFO_SITIOS_ARQUEOLOGICOS_ANT", grvSitArqAnt);
    }

    protected void grvSitArqAnt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_INFO_SITIOS_ARQUEOLOGICOS_ANT");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        string esaId = dsGrilla.Tables[0].Rows[e.RowIndex]["ESA_ID"].ToString();
        CargarTabla("EIH_COOR_SITIO_ARQ_ANT", "ESA_ID", esaId);

        DataTable TEMP = dsGrilla.Tables[0];
        int i = 0;
        for (i = TEMP.Rows.Count - 1; i >= 0; i--)
        {
            dsGrilla.Tables[0].Rows[i].Delete();
        }
        Contexto.guardarTabla(dsGrilla, "EIH_COOR_SITIO_ARQ_ANT");

        CargarTabla("EIH_INFO_SITIOS_ARQUEOLOGICOS_ANT", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_INFO_SITIOS_ARQUEOLOGICOS_ANT");
        Visualizar("EIV_INFO_SITIOS_ARQUEOLOGICOS_ANT", grvSitArqAnt);
    }

    #endregion

    #region 3.3.5.6 Información de Sitios Arqueológicos Rescatados en este Proyecto

    protected void btnNuevoSitArqProy_Click(object sender, EventArgs e)
    {
        plhSitArqProy.Visible = true;
        plhSitArqProy.Focus();
    }

    protected void btnCancelarSitArqProy_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhSitArqProy);
        plhSitArqProy.Visible = false;
    }

    protected void btnAgregarSitArqProy_Click(object sender, EventArgs e)
    {
        if (this.ctrCoorSitArqProy.Validar(1, 5))
        {
            GuardarSitArtProy();
            this.ctrCoorSitArqProy.LimpiarObjetos();
            limpiarPlaceHolder(plhSitArqProy);
        }
    }

    private void GuardarSitArtProy()
    {
        CargarTabla("EIH_INFO_SITIOS_ARQUEOLOGICOS", "EAI_ID", this.cboAreas9.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["ESA_DESCRIPCION"] = this.txtDescSitArqProy.Text;
        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas9.SelectedValue;
            dr["ESA_DESCRIPCION"] = this.txtDescSitArqProy.Text;
            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_INFO_SITIOS_ARQUEOLOGICOS");

        CargarTabla("EIH_INFO_SITIOS_ARQUEOLOGICOS", "EAI_ID", this.cboAreas9.SelectedValue);

        string esaId = dsGrilla.Tables[0].Rows[dsGrilla.Tables[0].Rows.Count - 1]["ESA_ID"].ToString();

        CargarTabla("EIH_COOR_SIT_ARQ", "ESA_ID", esaId);

        if (dsGrilla.Tables[0].Rows.Count == 0)
        {
            foreach (DataRow row in this.ctrCoorSitArqProy.Coordenadas.Rows)
            {
                DataRow dr2 = dsGrilla.Tables[0].NewRow();
                dr2["ESA_ID"] = esaId;
                dr2["ECS_COOR_NORTE"] = row["COORNORTE"];
                dr2["ECS_COOR_ESTE"] = row["COORESTE"];
                dsGrilla.Tables[0].Rows.Add(dr2);
            }
            Contexto.guardarTabla(dsGrilla, "EIH_COOR_SIT_ARQ");
        }

        Visualizar("EIV_INFO_SITIOS_ARQUEOLOGICOS", grvSitArqProy);
    }


    protected void grvSitArqProy_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_INFO_SITIOS_ARQUEOLOGICOS");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        string esaId = dsGrilla.Tables[0].Rows[e.RowIndex]["ESA_ID"].ToString();
        CargarTabla("EIH_COOR_SIT_ARQ", "ESA_ID", esaId);

        DataTable TEMP = dsGrilla.Tables[0];
        int i = 0;
        for (i = TEMP.Rows.Count - 1; i >= 0; i--)
        {
            dsGrilla.Tables[0].Rows[i].Delete();
        }
        Contexto.guardarTabla(dsGrilla, "EIH_COOR_SIT_ARQ");

        CargarTabla("EIH_INFO_SITIOS_ARQUEOLOGICOS", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_INFO_SITIOS_ARQUEOLOGICOS");
        Visualizar("EIV_INFO_SITIOS_ARQUEOLOGICOS", grvSitArqProy);
    }

    #endregion

    #region 3.3.6.1  Información de Actores

    protected void btnNuevoActDimPol_Click(object sender, EventArgs e)
    {
        plhActDimPol.Visible = true;
        plhActDimPol.Focus();
    }

    protected void btnCancelarActDimPol_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhActDimPol);
        plhActDimPol.Visible = false;
    }

    protected void btnAgregarActDimPol_Click(object sender, EventArgs e)
    {
        GuardarActDimPol();
        limpiarPlaceHolder(plhActDimPol);
    }

    private void GuardarActDimPol()
    {
        CargarTabla("EIH_INFORMACION_ACT_AREA_INFDIR", "EAI_ID", this.cboAreas10.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EAA_LIDER_COMUNAL"] = this.txtLidComActDimPol.Text;
            dsGrilla.Tables[0].Rows[0]["EAA_INFO_CONTACTO"] = this.txtInfContacActDimPol.Text;
            dsGrilla.Tables[0].Rows[0]["EAA_OTROS_ACTORES"] = this.txtOtrosActDimPol.Text;
            dsGrilla.Tables[0].Rows[0]["EAA_INFO_CONTACTO_OTROS"] = this.txtInfContOtroActDimPol.Text;            

        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas10.SelectedValue;
            dr["EAA_LIDER_COMUNAL"] = this.txtLidComActDimPol.Text;
            dr["EAA_INFO_CONTACTO"] = this.txtInfContacActDimPol.Text;
            dr["EAA_OTROS_ACTORES"] = this.txtOtrosActDimPol.Text;
            dr["EAA_INFO_CONTACTO_OTROS"] = this.txtInfContOtroActDimPol.Text;         
            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_INFORMACION_ACT_AREA_INFDIR");

        Visualizar("EIV_INFORMACION_ACT_AREA_INFDIR", grvActDimPol);
    }

    protected void grvActDimPol_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_INFORMACION_ACT_AREA_INFDIR");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_INFORMACION_ACT_AREA_INFDIR", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_INFORMACION_ACT_AREA_INFDIR");
        Visualizar("EIV_INFORMACION_ACT_AREA_INFDIR", grvActDimPol);
    }

    #endregion

    #region 3.3.6.2 Información de Presencia Institucional

    protected void btnNuevoInfPresInst_Click(object sender, EventArgs e)
    {
        plhInfPresInst.Visible = true;
        plhInfPresInst.Focus();
    }
    protected void btnCancelarInfPresInst_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfPresInst);
        plhInfPresInst.Visible = false;
    }

    private void CargarComboTipoInstitucion()
    {
        CargarCombo("ETI_ACTIVO", "EIB_TIPO_INSTITUCION", "ETI_ID", "ETI_TIPO_INSTITUCION", cboTipoInfPresInst);
    }


    protected void cboTipoInfPresInst_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.trTipoInfPre.Visible = false;
        this.txtOtroTipoInfPresInst.Text = "";
        if (this.cboTipoInfPresInst.SelectedItem.Text.Contains("Otro"))
        {
            this.trTipoInfPre.Visible = true;
        }
    }

    protected void btnAgregarInfPresInst_Click(object sender, EventArgs e)
    {
        GuardarInfPresInst();
        limpiarPlaceHolder(plhInfPresInst);
    }

    private void GuardarInfPresInst()
    {
        CargarTabla("EIH_INFO_PRES_INSTITUCIONAL_AREAINFDIR", "EAI_ID", this.cboAreas11.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["ETI_ID"] = this.cboTipoInfPresInst.SelectedValue;
            dsGrilla.Tables[0].Rows[0]["EPI_ACTIVIDAD"] = this.txtActInfPresInst.Text;
            if (this.txtOtroTipoInfPresInst.Text=="")
                dsGrilla.Tables[0].Rows[0]["ETI_OTRO"] = DBNull.Value;            
            else
                dsGrilla.Tables[0].Rows[0]["ETI_OTRO"] = this.txtOtroTipoInfPresInst.Text;            

        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas11.SelectedValue;
            dr["ETI_ID"] = this.cboTipoInfPresInst.SelectedValue;
            dr["EPI_ACTIVIDAD"] = this.txtActInfPresInst.Text;
            if (this.txtOtroTipoInfPresInst.Text == "")
                dr["ETI_OTRO"] = DBNull.Value;
            else
                dr["ETI_OTRO"] = this.txtOtroTipoInfPresInst.Text;    
            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_INFO_PRES_INSTITUCIONAL_AREAINFDIR");

        Visualizar("EIV_INFO_PRES_INSTITUCIONAL_AREAINFDIR", grvInfPresInst);
    }

    protected void grvInfPresInst_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_INFO_PRES_INSTITUCIONAL_AREAINFDIR");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_INFO_PRES_INSTITUCIONAL_AREAINFDIR", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_INFO_PRES_INSTITUCIONAL_AREAINFDIR");
        Visualizar("EIV_INFO_PRES_INSTITUCIONAL_AREAINFDIR", grvInfPresInst);
    }

    #endregion

    #region 3.3.7.1 Información de Servicios Públicos

    protected void btnNuevoInfServPub_Click(object sender, EventArgs e)
    {
        plhInfServPub.Visible = true;
        plhInfServPub.Focus();
    }

    protected void btnCancelarInfServPub_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfServPub);
        plhInfServPub.Visible = false;
    }

    protected void btnAgregarInfServPub_Click(object sender, EventArgs e)
    {
        GuardarInfServPub();
        limpiarPlaceHolder(plhInfServPub);
    }

    private void GuardarInfServPub()
    {
        CargarTabla("EIH_TEND_DESA_SERVICIOS_PUBLICOS", "EAI_ID", this.cboAreas12.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["ESP_PORC_ACUEDUCTO"] = this.txtPorcAcuedInfServPub.Text;
            dsGrilla.Tables[0].Rows[0]["ESP_PORC_ALCANTARILLADO"] = this.txtPorcAlcantInfServPub.Text;
            dsGrilla.Tables[0].Rows[0]["ESP_PORC_ELECTRIFICACION"] = this.txtPorcElectInfServPub.Text;
            dsGrilla.Tables[0].Rows[0]["ESP_PORC_MAN_RES_SOLIDOS"] = this.txtPorcReSolInfServPub.Text;

        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas12.SelectedValue;
            dr["ESP_PORC_ACUEDUCTO"] = this.txtPorcAcuedInfServPub.Text;
            dr["ESP_PORC_ALCANTARILLADO"] = this.txtPorcAlcantInfServPub.Text;
            dr["ESP_PORC_ELECTRIFICACION"] = this.txtPorcElectInfServPub.Text;
            dr["ESP_PORC_MAN_RES_SOLIDOS"] = this.txtPorcReSolInfServPub.Text;

            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_TEND_DESA_SERVICIOS_PUBLICOS");

        Visualizar("EIV_TEND_DESA_SERVICIOS_PUBLICOS", grvInfServPub);
    }

    protected void grvInfServPub_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_TEND_DESA_SERVICIOS_PUBLICOS");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_TEND_DESA_SERVICIOS_PUBLICOS", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_TEND_DESA_SERVICIOS_PUBLICOS");
        Visualizar("EIV_TEND_DESA_SERVICIOS_PUBLICOS", grvInfServPub);    
    }
    
    
    #endregion

    #region 3.3.7.2 Información de Servicios Sociales
    protected void btnNuevoInfServSoc_Click(object sender, EventArgs e)
    {
        plhInfServSoc.Visible = true;
        plhInfServSoc.Focus();
    }
    protected void btnCancelarInfServSoc_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfServSoc);
        plhInfServSoc.Visible = false;
    }

    protected void btnAgregarInfServSoc_Click(object sender, EventArgs e)
    {
        GuardarInfServSoc();
        limpiarPlaceHolder(plhInfServSoc);
    }

    private void GuardarInfServSoc()
    {
        CargarTabla("EIH_TEND_DESA_SERVICIOS_SOCIALES", "EAI_ID", this.cboAreas13.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["ESS_PORC_VIVIENDA"] = this.txtPorcVivInfServSoc.Text;
            dsGrilla.Tables[0].Rows[0]["ESS_PORC_EDUCACION"] = this.txtPorcEduInfServSoc.Text;
            dsGrilla.Tables[0].Rows[0]["ESS_PORC_SALUD"] = this.txtPorcSalInfServSoc.Text;
            dsGrilla.Tables[0].Rows[0]["ESS_PORC_OTROS"] = this.txtPorcOtrosInfServSoc.Text;

        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas13.SelectedValue;
            dr["ESS_PORC_VIVIENDA"] = this.txtPorcVivInfServSoc.Text;
            dr["ESS_PORC_EDUCACION"] = this.txtPorcEduInfServSoc.Text;
            dr["ESS_PORC_SALUD"] = this.txtPorcSalInfServSoc.Text;
            dr["ESS_PORC_OTROS"] = this.txtPorcOtrosInfServSoc.Text;

            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_TEND_DESA_SERVICIOS_SOCIALES");

        Visualizar("EIV_TEND_DESA_SERVICIOS_SOCIALES", grvInfServSoc);
    }


    protected void grvInfServSoc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_TEND_DESA_SERVICIOS_SOCIALES");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_TEND_DESA_SERVICIOS_SOCIALES", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_TEND_DESA_SERVICIOS_SOCIALES");
        Visualizar("EIV_TEND_DESA_SERVICIOS_SOCIALES", grvInfServSoc);
    }


   
    #endregion

    #region 3.3.7.3 Información Economíca y Cultural
    protected void btnNuevoInfEcoCul_Click(object sender, EventArgs e)
    {
        plhInfEcoCul.Visible = true;
        plhInfEcoCul.Focus();
    }
    protected void btnCancelarInfEcoCul_Click(object sender, EventArgs e)
    {
        plhInfEcoCul.Visible = false;
    }

    protected void btnAgregarInfEcoCul_Click(object sender, EventArgs e)
    {
        GuardarInfEcoCul();
        limpiarPlaceHolder(plhInfEcoCul);
    }

    private void GuardarInfEcoCul()
    {
        CargarTabla("EIH_TEND_DESA_ECONOMICA_CULTURAL", "EAI_ID", this.cboAreas14.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EEC_PORC_PRIMER_ORDEN"] = this.txtEcoPriInfEcoCul.Text;
            dsGrilla.Tables[0].Rows[0]["EEC_PORC_SEGUNDO_ORDEN"] = this.txtEcoSegInfEcoCul.Text;
            dsGrilla.Tables[0].Rows[0]["EEC_PORC_TERCERO_ORDEN"] = this.txtEcoTerInfEcoCul.Text;
            dsGrilla.Tables[0].Rows[0]["EEC_PORC_SOCIALES"] = this.txtCulSocInfEcoCul.Text;
            dsGrilla.Tables[0].Rows[0]["EEC_PORC_PATRIMONIO"] = this.txtCulPatInfEcoCul.Text;

        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas14.SelectedValue;
            dr["EEC_PORC_PRIMER_ORDEN"] = this.txtEcoPriInfEcoCul.Text;
            dr["EEC_PORC_SEGUNDO_ORDEN"] = this.txtEcoSegInfEcoCul.Text;
            dr["EEC_PORC_TERCERO_ORDEN"] = this.txtEcoTerInfEcoCul.Text;
            dr["EEC_PORC_SOCIALES"] = this.txtCulSocInfEcoCul.Text;
            dr["EEC_PORC_PATRIMONIO"] = this.txtCulPatInfEcoCul.Text;

            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_TEND_DESA_ECONOMICA_CULTURAL");

        Visualizar("EIV_TEND_DESA_ECONOMICA_CULTURAL", grvInfEcoCul);
    }

    protected void grvInfEcoCul_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_TEND_DESA_ECONOMICA_CULTURAL");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_TEND_DESA_ECONOMICA_CULTURAL", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_TEND_DESA_ECONOMICA_CULTURAL");
        Visualizar("EIV_TEND_DESA_ECONOMICA_CULTURAL", grvInfEcoCul);
    }

    #endregion

    #region 3.3.7.4 Información de Organización Comunitaria, de Funcionamiento y de Indicador de Desempeño Municipal
    protected void btnNuevoInfOrgCom_Click(object sender, EventArgs e)
    {
        plhInfOrgCom.Visible = true;
        plhInfOrgCom.Focus();
    }
    protected void btnCancelarInfOrgCom_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhInfOrgCom);
        plhInfOrgCom.Visible = false;
    }

    protected void btnAgregarInfOrgCom_Click(object sender, EventArgs e)
    {
        GuardarInfOrgCom();
        limpiarPlaceHolder(plhInfOrgCom);
    }

    private void GuardarInfOrgCom()
    {
        CargarTabla("EIH_TEND_DESA_ORGANIZACION_COMUNITARIA", "EAI_ID", this.cboAreas15.SelectedValue);

        if (dsGrilla.Tables[0].Rows.Count > 0)
        {
            dsGrilla.Tables[0].Rows[0]["EOC_PORC_ORG_COMUNITARIA"] = this.txtCapaInfOrgCom.Text;
            dsGrilla.Tables[0].Rows[0]["EOC_PORC_GAST_FUNCIONAMI"] = this.txtFuncInfOrgCom.Text;
            dsGrilla.Tables[0].Rows[0]["EOC_INDICADOR_DESP_MUNIC"] = this.txtIndDesmMun.Text;            

        }
        else
        {
            DataRow dr = dsGrilla.Tables[0].NewRow();
            dr["EAI_ID"] = this.cboAreas15.SelectedValue;
            dr["EOC_PORC_ORG_COMUNITARIA"] = this.txtCapaInfOrgCom.Text;
            dr["EOC_PORC_GAST_FUNCIONAMI"] = this.txtFuncInfOrgCom.Text;
            dr["EOC_INDICADOR_DESP_MUNIC"] = this.txtIndDesmMun.Text;            

            dsGrilla.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsGrilla, "EIH_TEND_DESA_ORGANIZACION_COMUNITARIA");

        Visualizar("EIV_TEND_DESA_ORGANIZACION_COMUNITARIA", grvInfOrgCom);
    }


    protected void grvInfOrgCom_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargarTabla("EIV_TEND_DESA_ORGANIZACION_COMUNITARIA");
        string eaiId = dsGrilla.Tables[0].Rows[e.RowIndex]["EAI_ID"].ToString();
        CargarTabla("EIH_TEND_DESA_ORGANIZACION_COMUNITARIA", "EAI_ID", eaiId);
        dsGrilla.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_TEND_DESA_ORGANIZACION_COMUNITARIA");
        Visualizar("EIV_TEND_DESA_ORGANIZACION_COMUNITARIA", grvInfOrgCom);
    }

    #endregion

    #region 3.4.1 Medio Abiótico (Físico)

    protected void btnNuevoUnidZonFis_Click(object sender, EventArgs e)
    {
        plhUnidZonFis.Visible = true;
        plhUnidZonFis.Focus();
    }
    protected void btnCancelarUnidZonFis_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhUnidZonFis);
        plhUnidZonFis.Visible = false;
    }

    protected void btnAgregarUnidZonFis_Click(object sender, EventArgs e)
    {
        GuardarUnidZonFis();
        limpiarPlaceHolder(plhUnidZonFis);
    }

    private void GuardarUnidZonFis()
    {
        CargarTabla("EIH_UNID_ZONIFIC_FISICAS");
    
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EZF_FECHA_CREACION"] = Fecha;
        dr["EZF_COD_MAPA"] = this.txtCodMapaUnidZonFis.Text;
        dr["EZF_TIPO_UNIDAD"] = this.txtTipoUnidZonFis.Text;
        dr["EZF_DESCRIPCION"] = this.txtDescUnidZonFis.Text;
        dr["EZF_CRITERIOS"] = this.txtCritUnidZonFis.Text;
        dr["EZF_AREA_AREA_ESTUDIO"] = this.txtAreaUnidZonFis.Text;
        dr["EZF_PORC_AREA_ESTUDIO"] = this.txtPorcAreaUnidZonFis.Text;

        dsGrilla.Tables[0].Rows.Add(dr);


        Contexto.guardarTabla(dsGrilla, "EIH_UNID_ZONIFIC_FISICAS");

        Visualizar("EIH_UNID_ZONIFIC_FISICAS", grvUnidZonFis);
    }

    protected void grvUnidZonFis_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_UNID_ZONIFIC_FISICAS", e.RowIndex);
        Visualizar("EIH_UNID_ZONIFIC_FISICAS", grvUnidZonFis);
    }

    #endregion

    #region 3.4.2 Medio biótico
    protected void btnNuevoUnidZonBio_Click(object sender, EventArgs e)
    {
        plhUnidZonBio.Visible = true;
        plhUnidZonBio.Focus();
    }
    protected void btnCancelarUnidZonBio_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhUnidZonBio);
        plhUnidZonBio.Visible = false;
    }

    protected void btnAgregarUnidZonBio_Click(object sender, EventArgs e)
    {
        GuardarUnidZonBio();
        limpiarPlaceHolder(plhUnidZonBio);
    }
    private void GuardarUnidZonBio()
    {
        CargarTabla("EIH_UNID_ZONIFIC_BIOTICAS");

        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EZB_FECHA_CREACION"] = Fecha;
        dr["EZB_COD_MAPA"] = this.txtCodMapaUnidZonBio.Text;
        dr["EZB_TIPO_UNIDAD"] = this.txtTipoUnidZonBio.Text;
        dr["EZB_DESCRIPCION"] = this.txtDescUnidZonBio.Text;
        dr["EZB_CRITERIOS"] = this.txtCritUnidZonBio.Text;
        dr["EZB_AREA_AREA_ESTUDIO"] = this.txtAreaUnidZonBio.Text;
        dr["EZB_PORC_AREA_ESTUDIO"] = this.txtPorcAreaUnidZonBio.Text;

        dsGrilla.Tables[0].Rows.Add(dr);


        Contexto.guardarTabla(dsGrilla, "EIH_UNID_ZONIFIC_BIOTICAS");

        Visualizar("EIH_UNID_ZONIFIC_BIOTICAS", grvUnidZonBio);
    }


    protected void grvUnidZonBio_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_UNID_ZONIFIC_BIOTICAS", e.RowIndex);
    }

    #endregion

    #region 3.4.3 Medio Socioeconómico

    protected void btnNuevoUnidZonSocEco_Click(object sender, EventArgs e)
    {
        plhUnidZonSocEco.Visible = true;
        plhUnidZonSocEco.Focus();
    }
    protected void btnCancelarUnidZonSocEco_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhUnidZonSocEco);
        plhUnidZonSocEco.Visible = false;
    }

    protected void btnAgregarUnidZonSocEco_Click(object sender, EventArgs e)
    {
        GuardarUnidZonSocEco();
        limpiarPlaceHolder(plhUnidZonSocEco);
    }
    private void GuardarUnidZonSocEco()
    {
        CargarTabla("EIH_UNID_ZONIFIC_SOCIOECONOM");

        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EZS_FECHA_CREACION"] = Fecha;
        dr["EZS_COD_MAPA"] = this.txtCodMapaUnidZonSocEco.Text;
        dr["EZS_TIPO_UNIDAD"] = this.txtTipoUnidZonSocEco.Text;
        dr["EZS_DESCRIPCION"] = this.txtDescUnidZonSocEco.Text;
        dr["EZS_CRITERIOS"] = this.txtCritUnidZonSocEco.Text;
        dr["EZS_AREA_AREA_ESTUDIO"] = this.txtAreaUnidZonSocEco.Text;
        dr["EZS_PORC_AREA_ESTUDIO"] = this.txtPorcAreaUnidZonSocEco.Text;

        dsGrilla.Tables[0].Rows.Add(dr);


        Contexto.guardarTabla(dsGrilla, "EIH_UNID_ZONIFIC_SOCIOECONOM");

        Visualizar("EIH_UNID_ZONIFIC_SOCIOECONOM", grvUnidZonSocEco);
    }

    protected void grvUnidZonSocEco_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_UNID_ZONIFIC_SOCIOECONOM", e.RowIndex);
    }

    #endregion

    #region 3.4.1 Zonificación ambiental
    
    protected void btnNuevoUnidZonAmb_Click(object sender, EventArgs e)
    {
        plhUnidZonAmb.Visible = true;
        plhUnidZonAmb.Focus();
    }
    protected void btnCancelarUnidZonAmb_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhUnidZonAmb);
        plhUnidZonAmb.Visible = false;
    } 

    protected void btnAgregarUnidZonAmb_Click(object sender, EventArgs e)
    {
        GuardarUnidZonAmb();
        limpiarPlaceHolder(plhUnidZonAmb);
    }

    private void GuardarUnidZonAmb()
    {
        CargarTabla("EIH_UNID_ZONIFIC_AMBIENTAL");

        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EZS_FECHA_CREACION"] = Fecha;
        dr["EZS_COD_MAPA"] = this.txtCodMapaUnidZonAmb.Text;
        dr["EZS_TIPO_UNIDAD"] = this.txtTipoUnidZonAmb.Text;
        dr["EZS_MEDIO_FISICO"] = this.txtMFisUnidZonAmb.Text;
        dr["EZS_MEDIO_BIOTICO"] = this.txtMBioUnidZonAmb.Text;
        dr["EZS_MEDIO_SOCIAL"] = this.txtMSocUnidZonAmb.Text;
        dr["EZS_CRITERIOS"] = this.txtCritUnidZonAmb.Text;
        dr["EZS_AREA_AREA_ESTUDIO"] = this.txtAreaUnidZonAmb.Text;
        dr["EZS_PORC_AREA_ESTUDIO"] = this.txtPorcAreaUnidZonAmb.Text;

        dsGrilla.Tables[0].Rows.Add(dr);


        Contexto.guardarTabla(dsGrilla, "EIH_UNID_ZONIFIC_AMBIENTAL");

        Visualizar("EIH_UNID_ZONIFIC_AMBIENTAL", grvUnidZonAmb);
    }

    protected void grvUnidZonAmb_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Eliminar("EIH_UNID_ZONIFIC_AMBIENTAL", e.RowIndex);
        Visualizar("EIH_UNID_ZONIFIC_AMBIENTAL", grvUnidZonAmb);
    }

    #endregion
    

    
    protected void txtNombreHidrogeolo_TextChanged(object sender, EventArgs e)
    {
       
    }



   
}