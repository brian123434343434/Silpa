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
using System.Data.SqlClient;
using System.Collections.Generic;
using SoftManagement.Persistencia;
using SILPA.AccesoDatos.ResumenEIA.Generacion;


public partial class ResumenEIA_Fichas_ctrFicha4 : System.Web.UI.UserControl
{

    DataSet dsDatos;    
    public DateTime Fecha
    {
        get { return DateTime.Now; }
        set{}
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
        if (!Page.IsPostBack)
        {
            
        }
    }

    public void CargarTodaInfo()
    {
        CargarGrillas();
    }

    protected void CargarGrillas()
    {
        cargarcboMatConTip();
        CargarCboUnidadArea();
        VisualizarfuentesAutorizadas();
        CargarAguasSuperficiales();
        CargarDrenajes();
        CargarAguasSuperficialesDetallesVista();
        VisualizarOcupacionCaucesFuentes();
        CargarAguasSubterraneas();
        CargarAguasSubterraneas2();
        cargarCboTipoReceptor();
        cargarCboTipoReceptorH();

        cargarTipoObra();
        if (dsDatos.Tables[0].Rows.Count > 0)
        {
            AsignarTipoReceptor();
            string Par = dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString();
            CargarReceptoresVertimiento(Par);
            VisualizaSuelosReceptores(Par);
            VisualizarInfoAcuiferos(Par);
            VisualizarDescripcionVerimientos(Par);
            CargarCalidadFisicoquimica();
            VisibleDepTipoReceptor(this.cboTipoReceptor.SelectedValue);
        }

        CargarNombreLaboratorios();
        CargarNombreLaboratorio();
        CargarNombreLaboratorioSuelos();

        CargarFuentesVertimientos();

        CargarCaracteristicasFisicas();
        CargarCaracteristicasQuimicas();
        CargarCaracteristicasFisicoQuimicasSuelos();
        CargarCaracteristicasBacteri();


        TipoMuestraSuelos();
        PeriodoClimatico();

        CargarReceptoresVertimientosDetalles();

        CargarTiposAcuifero();
        CargarOtrosUsosAcuifero();

        CargarTipoVertimiento();
        CargarUnidadCaudal();
        CargarDescargaPrevista();

        CargarTratamientoVerDesc();
        VisualizarTipoDeTratamientoPrevistos();

        VisualizarOcupacionCauces();
        cargarcboTipoOcupacion();
        VisualizarGrvMaterialesConstruccion();
        VisualizarCoberturasVegetales();
        CargarAprovechamientoRealizar();
        VisualizarAprovechamientoForestal();

        CargarCboTipoFuente();
        CargarCboTipoDucto();

        VisualizarEmisiones();

        CargarRuido();

        CargarCboQuienManeja();
        CargarCboTipoAprovechamientoNoPel();
        CargarCboTipoAprovechamientoPel();
        CargarCboTipoTratamientoPel();
        CargarCboTipoTratamientoNoPel();
        CargarCboTipoDispPel();
        CargarCboTipoDispNoPel();
        VisualizarResiduosNoPeligrosos();
        VisualizarResiduosPeligrosos();
    }

    private bool ValidacionTipoReceptor()
    {
        if (this.cboTipoReceptor.SelectedValue == "-1")
        {
            Mensaje.MostrarMensaje(this.Page, "Debe Seleccionar un tipo de Receptor.");
            return false;
        }
        return true;
    }

    #region 4.1.1 Aguas Superficiales

    private void CargarAguasSuperficiales()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_CAPT_AGUA_FUENTES_SUPERF");
    }

    private void CargarDrenajes()
    {
        this.cboListaDrenajes.DataSource = dsDatos.Tables[0];
        this.cboListaDrenajes.DataTextField = "EAS_NOMBRE_DRENAJE";
        this.cboListaDrenajes.DataValueField = "EAS_ID";
        this.cboListaDrenajes.DataBind();
        cboListaDrenajes.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    private void GuardarAguasSuperficiales()
    {
        CargarAguasSuperficiales();

        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EAS_FECHA_CREACION"] = Fecha;
        dr["EAS_NOMBRE_DRENAJE"] = this.txtNombreDrenajeAguasSup.Text;

        dsDatos.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsDatos, "EIH_CAPT_AGUA_FUENTES_SUPERF");
        CargarDrenajes();
    }

    private void EliminarAguasSuperficiales()
    {
        CargarAguasSuperficiales();

        dsDatos.Tables[0].Rows[this.cboListaDrenajes.SelectedIndex - 1].Delete();
        Contexto.guardarTabla(dsDatos, "EIH_CAPT_AGUA_FUENTES_SUPERF");
        CargarDrenajes();
    }

    protected void btnAgregarDrenaje_Click(object sender, EventArgs e)
    {
        GuardarAguasSuperficiales();
        this.txtNombreDrenajeAguasSup.Text = "";
        Mensaje.MostrarMensaje(this.Page, "Inserto Satisfactorio");
    }

    protected void btnEliminarAguasSuper_Click(object sender, EventArgs e)
    {
        EliminarAguasSuperficiales();
        this.txtNombreDrenajeAguasSup.Text = "";
    }

    private void CargarAguasSuperficialesDetalles(string easId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EAS_ID", SqlDbType.Int, 20, "EAS_ID");
        par.Value = easId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_DET_CAPT_AGUA_SUPERF");
    }

    private void CargarAguasSuperficialesDetallesVista()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIV_DET_CAPT_AGUA_SUPERF");
        this.grvDrenajes.DataSource = dsDatos;
        this.grvDrenajes.DataBind();
    }


    private void GuardarAguasSuperficialesDetalles(string easID)
    {
        CargarAguasSuperficialesDetalles(easID);
        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EAS_ID"] = easID;
        dr["EDS_NO_SITIO"] = txtSitioCapacitacion.Text;
        dr["EDS_COOR_NORTE_UBICACION"] = this.ctrUbic.CoorNorte.ToString();
        dr["EDS_COOR_ESTE_UBICACION"] = this.ctrUbic.CoorEste.ToString();
        dr["EDS_CAUDAL_SOLICITADO"] = this.txtCaudalSolicitadoAguasSup.Text;
        dr["EDS_CAUDAL_AFOR_FUENTE"] = this.txtCaudalAforado.Text;
        dr["EDS_METOD_CAPT_PREVISTO"] = this.txtMetodoCaptacionPrevisto.Text;
        dr["EDS_USO_AGUA_AGUAS_ABAJO"] = this.txtUsosDelAgua.Text;

        dsDatos.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsDatos, "EIH_DET_CAPT_AGUA_SUPERF");
        CargarAguasSuperficialesDetallesVista();
    }

    private void EliminarAguasSuperficialesDetalles(string easID, string edsId)
    {
        CargarAguasSuperficialesDetalles(easID);

        int i = 0;
        DataSet dsTemp = dsDatos;
        foreach (DataRow row in dsDatos.Tables[0].Rows)
        {
            if (row["EDS_ID"].ToString() == edsId)
                dsTemp.Tables[0].Rows[i].Delete();
            i += 1;
        }

        Contexto.guardarTabla(dsTemp, "EIH_DET_CAPT_AGUA_SUPERF");

        CargarAguasSuperficialesDetallesVista();
    }



    protected void btnAgregarDrenajeDetalles_Click(object sender, EventArgs e)
    {
        this.Page.Validate("DrenajeFuentes");
        if (!this.Page.IsValid)
            return;

        if (!this.ctrUbic.Valido)
            return;
        string easId = this.cboListaDrenajes.SelectedValue;

        GuardarAguasSuperficialesDetalles(easId);

        this.txtSitioCapacitacion.Text = "";
        this.ctrUbic.CoorNorte = "";
        this.ctrUbic.CoorEste = "";
        this.txtCaudalSolicitadoAguasSup.Text = "";
        this.txtCaudalAforado.Text = "";
        this.txtMetodoCaptacionPrevisto.Text = "";
        this.txtUsosDelAgua.Text = "";

    }

    protected void grvDrenajes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row;
        int index = Convert.ToInt32(e.CommandArgument);

        if (index < 0)
            return;
        if (e.CommandName == "Eliminar")
        {
            row = grvDrenajes.Rows[index];
            Label AguasSuperficialesEasID = (Label)row.FindControl("AguasSuperficialesEasID");
            Label AguasSuperficialesEdsID = (Label)row.FindControl("AguasSuperficialesEdsID");
            EliminarAguasSuperficialesDetalles(AguasSuperficialesEasID.Text, AguasSuperficialesEdsID.Text);
        }
    }


    protected void btnNuevosDrenajes_Click(object sender, EventArgs e)
    {
        this.plhDrenaje.Visible = true;
    }
    protected void btnCancelarDrenajeDetalles_Click(object sender, EventArgs e)
    {
        this.txtSitioCapacitacion.Text = "";
        this.ctrUbic.CoorNorte = "";
        this.ctrUbic.CoorEste = "";
        this.txtCaudalSolicitadoAguasSup.Text = "";
        this.txtCaudalAforado.Text = "";
        this.txtMetodoCaptacionPrevisto.Text = "";
        this.txtUsosDelAgua.Text = "";

        this.plhDrenaje.Visible = false;
    }

    #endregion

    #region 4.1.2 Aguas Subterraneas

    private void CargarAguasSubterraneas()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 20, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_CAPT_AGUA_PUNTOS_SUBT");

        this.grvAguasSubterraneas.DataSource = dsDatos;
        this.grvAguasSubterraneas.DataBind();
    }

    private void GuardarAguasSubterraneas()
    {
        CargarAguasSubterraneas();

        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EAT_FECHA_CREACION"] = Fecha;
        dr["EAT_NOMBRE"] = this.txtPuntoCaptacion.Text;
        dr["EAT_COOR_NORTE_UBICACION"] = this.ctrUbicAguasSub.CoorNorte;
        dr["EAT_COOR_ESTE_UBICACION"] = this.ctrUbicAguasSub.CoorEste;
        dr["EAT_CAUDAL_SOLICITADO"] = this.txtCaudalSolicitadoAguasSub.Text;
        dr["EAT_CAUDAL_DISPONIBLE"] = this.txtCaudalDisponible.Text;
        dr["EAT_MET_CAPTA_PREVISTO"] = this.txtMetCapPrevs.Text;
        dr["EAT_USO_AGUA_AREA_CIRC"] = this.txtUsoAguaSubter.Text;

        dsDatos.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsDatos, "EIH_CAPT_AGUA_PUNTOS_SUBT");
        CargarAguasSubterraneas();
    }

    private void EliminarAguasSubterraneas(int index)
    {
        CargarAguasSubterraneas();
        dsDatos.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsDatos, "EIH_CAPT_AGUA_PUNTOS_SUBT");
        CargarAguasSubterraneas();
    }

    protected void btnAgregarAguasSubterraneas_Click(object sender, EventArgs e)
    {
        if (!this.ctrUbicAguasSub.Valido)
            return;
        GuardarAguasSubterraneas();

        this.txtPuntoCaptacion.Text = "";
        this.ctrUbicAguasSub.CoorNorte = "";
        this.ctrUbicAguasSub.CoorEste = "";
        this.txtCaudalSolicitadoAguasSub.Text = "";
        this.txtCaudalDisponible.Text = "";
        this.txtMetCapPrevs.Text = "";
        this.txtUsoAguaSubter.Text = "";

    }

    protected void grvAguasSubterraneas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarAguasSubterraneas(e.RowIndex);
    }


    private void CargarAguasSubterraneas2()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 20, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_CAPT_AGUA_FUENTES_SUBT");

        this.grvAguasSubterraneas2.DataSource = dsDatos;
        this.grvAguasSubterraneas2.DataBind();
    }

    private void GuardarAguasSubterraneas2()
    {
        CargarAguasSubterraneas2();

        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EFS_FECHA_CREACION"] = Fecha;
        dr["EFS_SEV"] = this.txtSEV.Text;
        dr["EFS_COOR_NORTE_UBICACION"] = this.CtrUbicacion2.CoorNorte;
        dr["EFS_COOR_ESTE_UBICACION"] = this.CtrUbicacion2.CoorEste;
        dr["EFS_AZIMUT"] = this.txtAzimut.Text;
        dr["EFS_CAPA_ACUIFERA"] = this.txtCapaAcuifera.Text;
        dr["EFS_RESISTIVIDAD"] = this.txtResistividadCapaAcuifera.Text;
        dr["EFS_LITOLOGIA"] = this.txtLitologia.Text;
        dr["EFS_ESPESOR"] = this.txtEspesor.Text;

        dsDatos.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsDatos, "EIH_CAPT_AGUA_FUENTES_SUBT");
        CargarAguasSubterraneas2();
    }

    private void EliminarAguasSubterraneas2(int index)
    {
        CargarAguasSubterraneas2();
        dsDatos.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsDatos, "EIH_CAPT_AGUA_FUENTES_SUBT");
        CargarAguasSubterraneas2();
    }


    protected void btnAgregarAguasSubterraneas2_Click(object sender, EventArgs e)
    {
        if (!this.CtrUbicacion2.Valido)
            return;
        GuardarAguasSubterraneas2();
        this.txtSEV.Text = "";
        this.CtrUbicacion2.CoorNorte = "";
        this.CtrUbicacion2.CoorEste = "";
        this.txtAzimut.Text = "";
        this.txtCapaAcuifera.Text = "";
        this.txtResistividadCapaAcuifera.Text = "";
        this.txtLitologia.Text = "";
        this.txtEspesor.Text = "";
    }

    protected void grvAguasSubterraneas2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarAguasSubterraneas2(e.RowIndex);

    }

    protected void btnCaptacionSubterraneos_Click(object sender, EventArgs e)
    {
        this.plhAguasSubterraneas1.Visible = true;
        this.plhAguasSubterraneas2.Visible = true;
    }

    protected void btnCancelarAguasSubterraneas_Click(object sender, EventArgs e)
    {
        this.plhAguasSubterraneas1.Visible = false;
        this.txtPuntoCaptacion.Text = "";
        this.ctrUbicAguasSub.CoorNorte = "";
        this.ctrUbicAguasSub.CoorEste = "";
        this.txtCaudalSolicitadoAguasSub.Text = "";
        this.txtCaudalDisponible.Text = "";
        this.txtMetCapPrevs.Text = "";
        this.txtUsoAguaSubter.Text = "";
    }

    protected void btnCancelarAguasSubterraneas2_Click(object sender, EventArgs e)
    {
        this.plhAguasSubterraneas2.Visible = false;
        this.txtSEV.Text = "";
        this.CtrUbicacion2.CoorNorte = "";
        this.CtrUbicacion2.CoorEste = "";
        this.txtAzimut.Text = "";
        this.txtCapaAcuifera.Text = "";
        this.txtResistividadCapaAcuifera.Text = "";
        this.txtLitologia.Text = "";
        this.txtEspesor.Text = "";
    }

    #endregion

    #region 4.2.1 Rexeptores de Vertimiento

    private void cargarCboTipoReceptor()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ERV_ACTIVO", SqlDbType.Bit, 1, "ERV_ACTIVO");
        par.Value = true;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, ds, "EIB_TIPO_RECEPTOR_VERTIMIENTO");
        this.cboTipoReceptor.DataSource = ds.Tables[0];
        this.cboTipoReceptor.DataTextField = "ERV_TIPO_RECEPTOR_VERTI";
        this.cboTipoReceptor.DataValueField = "ERV_ID";
        this.cboTipoReceptor.DataBind();
        cboTipoReceptor.Items.Insert(0, new ListItem("Seleccione..", "-1"));

    }

    private void cargarCboTipoReceptorH()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 20, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_VERTIMIENTO");

    }
    private void AsignarTipoReceptor()
    {
        if (dsDatos.Tables[0].Rows.Count > 0)
        {
            this.cboTipoReceptor.SelectedValue = dsDatos.Tables[0].Rows[0]["ERV_ID"].ToString();
        }
    }
    private void GuardarCboTipoReceptorH(string ervID)
    {
        cargarCboTipoReceptorH();
        if (dsDatos.Tables[0].Rows.Count > 0)
        {
            dsDatos.Tables[0].Rows[0]["ERV_ID"] = ervID;
        }
        else
        {

            DataRow dr = dsDatos.Tables[0].NewRow();
            dr["ERV_ID"] = ervID;
            dr["EIP_ID"] = IDProyecto;
            dr["EVT_FECHA_CREACION"] = Fecha;
            dsDatos.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsDatos, "EIH_VERTIMIENTO");
    }


    protected void cboTipoReceptor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoReceptor.SelectedValue != "-1")
        {
            //Se quema condiciones por falta de Tiempo
            GuardarCboTipoReceptorH(this.cboTipoReceptor.SelectedValue);
        }
        VisibleDepTipoReceptor(this.cboTipoReceptor.SelectedValue);
    }

    private void VisibleDepTipoReceptor(string value)
    {
       // 1 Fisicoquimicas
        this.btnNuevasFuentes.Visible = false;
        this.grvReceptoresVertimiento.Visible = false;
        this.plhReceptoresVertimientos.Visible = false;
        this.plhNombreLaboratorio.Visible = false;
        this.btnCalidadFisicoquimicas.Visible = false;
        this.grvCalidadFisicoquimicas.Visible = false;

       // 2 Suelos
        this.btnDescripcioPredios.Visible = false;
        this.grvDescPrediosReceptores.Visible = false;
        this.plhLaboratotioSuelos.Visible = false;
        this.btnNuevosReceptoresVertimiento.Visible = false;
        this.grvReceptoresVertimientos.Visible = false;

        switch (this.cboTipoReceptor.SelectedValue)
        {
            case "1":
                this.btnNuevasFuentes.Visible = true;
                this.grvReceptoresVertimiento.Visible = true;                
                this.plhNombreLaboratorio.Visible = true;

                this.btnCalidadFisicoquimicas.Visible = true;
                this.grvCalidadFisicoquimicas.Visible = true;

                break;
            case "2":
                this.btnDescripcioPredios.Visible = true;
                this.grvDescPrediosReceptores.Visible = true;
                this.plhLaboratotioSuelos.Visible = true;
                this.btnNuevosReceptoresVertimiento.Visible = true;
                this.grvReceptoresVertimientos.Visible = true;
                break;
            case "3":

                break;
            case "4":

                break;
            case "5":

                break;
            case "6":

                break;
        }
    }

    protected void btnCalidadFisicoquimicas_Click(object sender, EventArgs e)
    {
        this.plhCalidadFisicoquimicas.Visible = true;
    }
    protected void btnCancelarFisicoQuimicas_Click(object sender, EventArgs e)
    {
        this.plhCalidadFisicoquimicas.Visible = false;
    }

    #endregion

    #region 4.2.1.1.1 Descripción de las Fuentes de Agua Receptores de Vertimiento

    private void CargarReceptoresVertimiento(string evtId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EVT_ID", SqlDbType.Int, 20, "EVT_ID");
        par.Value = evtId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_FUENTES_AGUA_VERTIMIENTO");

        this.grvReceptoresVertimiento.DataSource = dsDatos;
        this.grvReceptoresVertimiento.DataBind();

        this.cboFuentes.DataSource = dsDatos;
        this.cboFuentes.DataTextField = "EFA_NOMBRE_SITIO";
        this.cboFuentes.DataValueField = "EFA_ID";
        this.cboFuentes.DataBind();
        this.cboFuentes.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    private void GuardarReceptoresVertimiento(string EvtId)
    {
        CargarReceptoresVertimiento(EvtId);

        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EVT_ID"] = EvtId;
        dr["ELC_ID"] = DBNull.Value;
        dr["EFA_NOMBRE_SITIO"] = this.txtNombreVertimiento.Text;
        dr["EFA_COOR_NORTE_UBICACION"] = this.ctrUbicacionFuentes.CoorNorte;
        dr["EFA_COOR_ESTE_UBICACION"] = this.ctrUbicacionFuentes.CoorEste;
        dr["EFA_CAPACIDAD_ASIMILACION"] = this.txtCapaAsimilacion.Text;
        dr["EFA_DISTANCIA_MEZCLA"] = this.txtDistMezcla.Text;
        dr["EFA_CAUDAL_ESTIAJE"] = this.txtCaudalEstiaje.Text;
        dr["EFA_USOS_AGUA_ABAJO_VERT"] = this.txtUsoAguaVert.Text;
        dr["ELC_OTRO"] = DBNull.Value;

        dsDatos.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsDatos, "EIH_FUENTES_AGUA_VERTIMIENTO");

        CargarReceptoresVertimiento(EvtId);
    }

    private void EliminarReceptoresVertimiento(int index, string EvtId)
    {
        CargarReceptoresVertimiento(EvtId);
        dsDatos.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsDatos, "EIH_FUENTES_AGUA_VERTIMIENTO");
        CargarReceptoresVertimiento(EvtId);
    }

    protected void btnNuevasFuentes_Click(object sender, EventArgs e)
    {
        if (ValidacionTipoReceptor())
            this.plhFuentesdeAguaReceptores.Visible = true;
    }

    protected void btnAgregarFuentes_Click(object sender, EventArgs e)
    {
        if (!this.ctrUbicacionFuentes.Valido)
            return;
        if (ValidacionTipoReceptor())
        {
            cargarCboTipoReceptorH();
            GuardarReceptoresVertimiento(dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString());
            this.txtNombreVertimiento.Text = "";
            this.ctrUbicacionFuentes.CoorNorte = "";
            this.ctrUbicacionFuentes.CoorEste = "";
            this.txtCapaAsimilacion.Text = "";
            this.txtDistMezcla.Text = "";
            this.txtCaudalEstiaje.Text = "";
            this.txtUsoAguaVert.Text = "";
        }
    }

    protected void grvReceptoresVertimiento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        cargarCboTipoReceptorH();
        EliminarReceptoresVertimiento(e.RowIndex, dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString());
    }

    protected void btnCancelarFuentes_Click1(object sender, EventArgs e)
    {
        this.plhFuentesdeAguaReceptores.Visible = false;
        this.txtNombreVertimiento.Text = "";
        this.ctrUbicacionFuentes.CoorNorte = "";
        this.ctrUbicacionFuentes.CoorEste = "";
        this.txtCapaAsimilacion.Text = "";
        this.txtDistMezcla.Text = "";
        this.txtCaudalEstiaje.Text = "";
        this.txtUsoAguaVert.Text = "";
    }

    #endregion

    #region 4.2.1.1.2 Calidad Fisicoquimica de la Fuente de Agua Receptores de Vertimiento

    private void CargarNombreLaboratorios()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ELC_ACTIVO", SqlDbType.Bit, 1, "ELC_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_LABORATORIO_EST_CALIDAD");

        this.cboNombreLaboratorioFuentes.DataSource = dsDatos;
        this.cboNombreLaboratorioFuentes.DataTextField = "ELC_LABORATORIO_EST_CALIDAD";
        this.cboNombreLaboratorioFuentes.DataValueField = "ELC_ID";
        this.cboNombreLaboratorioFuentes.DataBind();
        this.cboNombreLaboratorioFuentes.Items.Insert(0, new ListItem("Seleccione..", "-1"));

        this.cboNombreLaboratorioSuelos.DataSource = dsDatos;
        this.cboNombreLaboratorioSuelos.DataTextField = "ELC_LABORATORIO_EST_CALIDAD";
        this.cboNombreLaboratorioSuelos.DataValueField = "ELC_ID";
        this.cboNombreLaboratorioSuelos.DataBind();
        this.cboNombreLaboratorioSuelos.Items.Insert(0, new ListItem("Seleccione..", "-1"));


    }

    private void CargarNombreLaboratorio()
    {
        cargarCboTipoReceptorH();
        if (dsDatos.Tables[0].Rows.Count > 0)
        {
            CargarReceptoresVertimiento(dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString());
            if (dsDatos.Tables[0].Rows.Count > 0)
            {
                if (dsDatos.Tables[0].Rows[0]["ELC_ID"].ToString() != "")
                {
                    this.cboNombreLaboratorioFuentes.SelectedValue = dsDatos.Tables[0].Rows[0]["ELC_ID"].ToString();
                    this.txtOtroLaboratorioFuentes.Text = dsDatos.Tables[0].Rows[0]["ELC_OTRO"].ToString();
                }
            }
        }

    }

    private void GuardarNombreLaboratorios(string elcId, string nombreOtroLab)
    {
        cargarCboTipoReceptorH();
        CargarReceptoresVertimiento(dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString());
        if (dsDatos.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsDatos.Tables[0].Rows)
            {
                dr["ELC_ID"] = elcId;
                dr["ELC_OTRO"] = nombreOtroLab;
            }

            Contexto.guardarTabla(dsDatos, "EIH_FUENTES_AGUA_VERTIMIENTO");
        }
    }

    protected void cboNombreLaboratorioFuentes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboNombreLaboratorioFuentes.SelectedItem.Text.Contains("Otro"))
        {
            this.lblOtroLaboratorioFuentes.Visible = true;
            this.txtOtroLaboratorioFuentes.Visible = true;
        }
        else
        {
            this.lblOtroLaboratorioFuentes.Visible = false;
            this.txtOtroLaboratorioFuentes.Visible = false;
        }
    }
    protected void btnAsignarLaboratorio_Click(object sender, EventArgs e)
    {
        string OtroLaboratorio = null;
        if (this.lblOtroLaboratorioFuentes.Visible)
            if (this.txtOtroLaboratorioFuentes.Text == "")
            {
                Mensaje.MostrarMensaje(this.Page, "Ingrese nombre del Otro Laboratorio");
                return;
            }
            else
                OtroLaboratorio = this.txtOtroLaboratorioFuentes.Text;
        GuardarNombreLaboratorios(this.cboNombreLaboratorioFuentes.SelectedValue, OtroLaboratorio);
    }

    private void llamarPopUpFuentes(string efaId, string epcId)
    {
        string strScript = "<script language='JavaScript'>" +
            "window.open('InfoAdicional/ReceptorVertimiento.aspx?tip=" + epcId + "&EFA_ID=" + efaId + "&sitios=" + this.txtCantidadSitios.Text + "&btn=" + btnActualizaGrilla.ClientID + "','Pruebas','location=no,resizable=yes,scrollbars=yes')" +
            "</script>";
        Page.RegisterStartupScript("PopupScript", strScript);
    }


    private bool InformacionAsociada(string efaId, string epcId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EFA_ID", SqlDbType.Int, 21, "EFA_ID");
        par.Value = efaId;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@EPC_ID", SqlDbType.Int, 21, "EPC_ID");
        //Quemamos Valor
        par2.Value = epcId;
        parametros.Add(par2);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_PARAM_MUEST_AGUA_VERT");
        if (dsDatos.Tables[0].Rows.Count > 0)
        {
            Mensaje.MostrarMensaje(this.Page, "Ya tiene Información Asociada a este Item");
            return false;
        }
        return true;
    }

    private void CargarFuentesVertimientos()
    {
        SILPA.LogicaNegocio.ResumenEIA.Procedimientos objPro = new SILPA.LogicaNegocio.ResumenEIA.Procedimientos();
        cargarCboTipoReceptorH();
        if (dsDatos.Tables[0].Rows.Count > 0)
        {
            DataSet ds = objPro.ProcFuentesAguasVertimientos(dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString());
            this.grvCalidadFisicoquimicas.DataSource = ds;
            this.grvCalidadFisicoquimicas.DataBind();

        }

    }

    protected void btnActualizaGrilla_Click(object sender, EventArgs e)
    {
        CargarFuentesVertimientos();
    }

    protected void btnTipoMuestra_Click(object sender, EventArgs e)
    {
        //quemo valor 95 =Tipo Muestra
        if (InformacionAsociada(this.cboFuentes.SelectedValue, "95"))
        {
            llamarPopUpFuentes(this.cboFuentes.SelectedValue, "95");
        }
    }

    protected void btnFechaMuestreo_Click(object sender, EventArgs e)
    {
        //quemo valor 96 =Fecha Muestra
        if (InformacionAsociada(this.cboFuentes.SelectedValue, "96"))
        {
            llamarPopUpFuentes(this.cboFuentes.SelectedValue, "96");
        }
    }

    protected void btnHoraMuestreoFisicoQ_Click(object sender, EventArgs e)
    {
        //quemo valor 97 =Hora Muestra
        if (InformacionAsociada(this.cboFuentes.SelectedValue, "97"))
        {
            llamarPopUpFuentes(this.cboFuentes.SelectedValue, "97");
        }
    }

    protected void btnDuracionMuestreoFisicoQ_Click(object sender, EventArgs e)
    {
        //quemo valor 98 =dURACION Muestreo
        if (InformacionAsociada(this.cboFuentes.SelectedValue, "98"))
        {
            llamarPopUpFuentes(this.cboFuentes.SelectedValue, "98");
        }
    }

    protected void btnPeriodoMuestreo_Click(object sender, EventArgs e)
    {
        //quemo valor 99 =Periodo Muestreo
        if (InformacionAsociada(this.cboFuentes.SelectedValue, "99"))
        {
            llamarPopUpFuentes(this.cboFuentes.SelectedValue, "99");
        }
    }


    private void CargarCaracteristicasFisicas()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EPC_ACTIVO", SqlDbType.Bit, 1, "EPC_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@ETP_ID", SqlDbType.Int, 10, "ETP_ID");
        par2.Value = 0;
        parametros.Add(par2);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_PARAMETROS_EST_CALIDAD");

        this.cboCaracteristicasFisicas.DataSource = dsDatos;
        this.cboCaracteristicasFisicas.DataTextField = "EPC_PARAMETROS_EST_CALIDAD";
        this.cboCaracteristicasFisicas.DataValueField = "EPC_ID";
        this.cboCaracteristicasFisicas.DataBind();
        this.cboCaracteristicasFisicas.Items.Insert(0, new ListItem("Seleccione..", "-1"));

        this.cboCaracterizacionFis.DataSource = dsDatos;
        this.cboCaracterizacionFis.DataTextField = "EPC_PARAMETROS_EST_CALIDAD";
        this.cboCaracterizacionFis.DataValueField = "EPC_ID";
        this.cboCaracterizacionFis.DataBind();
        this.cboCaracterizacionFis.Items.Insert(0, new ListItem("Seleccione..", "-1"));


    }
    protected void btnAgregarCaracteristicas2_Click(object sender, EventArgs e)
    {
        this.Page.Validate("CalidadFisicoquimicas");
        if (!this.Page.IsValid)
            return;
        if (InformacionAsociada(this.cboFuentes.SelectedValue, this.cboCaracteristicasFisicas.SelectedValue))
        {
            llamarPopUpFuentes(this.cboFuentes.SelectedValue, this.cboCaracteristicasFisicas.SelectedValue);
        }
    }

    private void CargarCaracteristicasQuimicas()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EPC_ACTIVO", SqlDbType.Bit, 1, "EPC_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@ETP_ID", SqlDbType.Int, 10, "ETP_ID");
        par2.Value = 1;
        parametros.Add(par2);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_PARAMETROS_EST_CALIDAD");

        this.cboCaracteristicasQuimicas.DataSource = dsDatos;
        this.cboCaracteristicasQuimicas.DataTextField = "EPC_PARAMETROS_EST_CALIDAD";
        this.cboCaracteristicasQuimicas.DataValueField = "EPC_ID";
        this.cboCaracteristicasQuimicas.DataBind();
        this.cboCaracteristicasQuimicas.Items.Insert(0, new ListItem("Seleccione..", "-1"));

        this.cboCaracterizacionQuim.DataSource = dsDatos;
        this.cboCaracterizacionQuim.DataTextField = "EPC_PARAMETROS_EST_CALIDAD";
        this.cboCaracterizacionQuim.DataValueField = "EPC_ID";
        this.cboCaracterizacionQuim.DataBind();
        this.cboCaracterizacionQuim.Items.Insert(0, new ListItem("Seleccione..", "-1"));

    }

    protected void btnAgregarCaracteristicas3_Click(object sender, EventArgs e)
    {
        this.Page.Validate("CalidadFisicoquimicas");
        if (!this.Page.IsValid)
            return;
        if (InformacionAsociada(this.cboFuentes.SelectedValue, this.cboCaracteristicasQuimicas.SelectedValue))
        {
            llamarPopUpFuentes(this.cboFuentes.SelectedValue, this.cboCaracteristicasQuimicas.SelectedValue);
        }

    }

    protected void grvCalidadFisicoquimicas_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
    }

    private void CargarCalidadFisicoquimicas(string efaId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EFA_ID", SqlDbType.Int, 21, "EFA_ID");
        par.Value = efaId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_PARAM_MUEST_AGUA_VERT");
    }

    private void EliminarCalidadFisicoquimicas(string efaId, string epcId)
    {
        CargarCalidadFisicoquimicas(efaId);
        DataSet dsTemp = dsDatos;
        int i = 0;
        for (i = dsDatos.Tables[0].Rows.Count - 1; i >= 0; i--)
        {
            if (dsDatos.Tables[0].Rows[i]["EPC_ID"].ToString() == epcId)
            {
                dsTemp.Tables[0].Rows[i].Delete();
            }
        }
        Contexto.guardarTabla(dsTemp, "EIH_PARAM_MUEST_AGUA_VERT");
        CargarFuentesVertimientos();

    }

    protected void grvCalidadFisicoquimicas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row;
        int index = Convert.ToInt32(e.CommandArgument);

        if (index < 0)
            return;
        if (e.CommandName == "Eliminar")
        {
            row = grvCalidadFisicoquimicas.Rows[index];
            Label lblEfaId = (Label)row.FindControl("lblEfaId");
            Label lblEpcId = (Label)row.FindControl("lblEpcId");
            EliminarCalidadFisicoquimicas(lblEfaId.Text, lblEpcId.Text);
        }
    }



    #endregion

    #region 4.2.1.2 Suelos Receptores de Vertimiento


    private void CargarSuelosReceptores(string evtId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EVT_ID", SqlDbType.Int, 20, "EVT_ID");
        par.Value = evtId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_PREDIOS_RECEP_VERTIMIENTO");

    }

    private void VisualizaSuelosReceptores(string evtId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EVT_ID", SqlDbType.Int, 20, "EVT_ID");
        par.Value = evtId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIV_PREDIOS_RECEP_VERTIMIENTO");
        this.grvDescPrediosReceptores.DataSource = dsDatos;
        this.grvDescPrediosReceptores.DataBind();


        this.cboReceptorVertimientos.DataSource = dsDatos;
        this.cboReceptorVertimientos.DataTextField = "EPV_NOMBRE";
        this.cboReceptorVertimientos.DataValueField = "EPV_ID";
        this.cboReceptorVertimientos.DataBind();
        this.cboReceptorVertimientos.Items.Insert(0, new ListItem("Seleccione..", "-1"));

    }

    private void GuardarSuelosReceptores(string evtId)
    {
        CargarSuelosReceptores(evtId);
        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EVT_ID"] = evtId;
        dr["ELC_ID"] = DBNull.Value;
        dr["EPV_NOMBRE"] = this.txtNombrePredio.Text;
        dr["EPV_USOS_SUELO"] = this.txtUsosDelSuelo.Text;
        dr["ELC_OTRO"] = DBNull.Value;
        dsDatos.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsDatos, "EIH_PREDIOS_RECEP_VERTIMIENTO");

        CargarSuelosReceptores(evtId);
        string epvId = dsDatos.Tables[0].Rows[dsDatos.Tables[0].Rows.Count - 1]["EPV_ID"].ToString();
        GuardarPoligonoSuelosReceptores(epvId);
        VisualizaSuelosReceptores(evtId);
    }

    private void CargarPoligonoSuelosReceptores(string epvId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EPV_ID", SqlDbType.Int, 20, "EPV_ID");
        par.Value = epvId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_COOR_RECEP_VERT");
    }

    private void GuardarPoligonoSuelosReceptores(string epvId)
    {
        CargarPoligonoSuelosReceptores(epvId);

        foreach (DataRow row in this.ctrUbicPolSuelos.Coordenadas.Rows)
        {
            DataRow dr = dsDatos.Tables[0].NewRow();
            dr["EPV_ID"] = epvId;
            dr["ECV_COOR_NORTE"] = row["CoorNorte"].ToString();
            dr["ECV_COOR_ESTE"] = row["CoorEste"].ToString();
            dsDatos.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsDatos, "EIH_COOR_RECEP_VERT");
    }

    protected void btnAgregarPredios_Click(object sender, EventArgs e)
    {
        DataTable dt = this.ctrUbicPolSuelos.Coordenadas;
        if (dt != null)
        {
            if (dt.Rows.Count == 5)
            {
                cargarCboTipoReceptorH();
                GuardarSuelosReceptores(dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString());
                this.ctrUbicPolSuelos.LimpiarObjetos();
                this.txtNombrePredio.Text = "";
                this.txtUsosDelSuelo.Text = "";
            }
            else
                Mensaje.MostrarMensaje(this.Page, "Debe Ingresar 5 Coordenadas");
        }
        else
            Mensaje.MostrarMensaje(this.Page, "No se han agregado Coordenadas");

    }

    private void EliminarPrediosReceptores(string evtId, string epvId)
    {
        EliminarCoordenadasPrediosReceptores(epvId);
        CargarSuelosReceptores(evtId);
        DataSet dsTemp = dsDatos;
        int i = 0;
        for (i = dsDatos.Tables[0].Rows.Count - 1; i >= 0; i--)
        {
            if (dsDatos.Tables[0].Rows[i]["EPV_ID"].ToString() == epvId)
            {
                dsTemp.Tables[0].Rows[i].Delete();
            }
        }
        Contexto.guardarTabla(dsTemp, "EIH_PREDIOS_RECEP_VERTIMIENTO");
        VisualizaSuelosReceptores(evtId);

    }
    private void EliminarCoordenadasPrediosReceptores(string epvId)
    {
        CargarPoligonoSuelosReceptores(epvId);
        DataSet dsTemp = dsDatos;
        int i = 0;
        for (i = dsDatos.Tables[0].Rows.Count - 1; i >= 0; i--)
        {
            if (dsDatos.Tables[0].Rows[i]["EPV_ID"].ToString() == epvId)
            {
                dsTemp.Tables[0].Rows[i].Delete();
            }
        }
        Contexto.guardarTabla(dsTemp, "EIH_COOR_RECEP_VERT");
    }

    protected void grvDescPrediosReceptores_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row;
        int index = Convert.ToInt32(e.CommandArgument);

        if (index < 0)
            return;
        if (e.CommandName == "Eliminar")
        {
            row = grvDescPrediosReceptores.Rows[index];
            Label lblEvtId = (Label)row.FindControl("lblEvtId");
            Label lblEpvId = (Label)row.FindControl("lblEpvId");
            EliminarPrediosReceptores(lblEvtId.Text, lblEpvId.Text);
        }
    }

    protected void btnCancelarPredios_Click(object sender, EventArgs e)
    {
        this.ctrUbicPolSuelos.LimpiarObjetos();
        this.txtNombrePredio.Text = "";
        this.txtUsosDelSuelo.Text = "";
        this.plhPrediosReceptores.Visible = false;
    }

    protected void btnDescripcioPredios_Click(object sender, EventArgs e)
    {
        this.plhPrediosReceptores.Visible = true;
    }



    #endregion

    #region 4.2.1.2.2 Calidad Fisicoquimica del Suelo

    private void CargarNombreLaboratorioSuelos()
    {
        cargarCboTipoReceptorH();
        if (dsDatos.Tables[0].Rows.Count > 0)
        {
            CargarSuelosReceptores(dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString());
            if (dsDatos.Tables[0].Rows.Count > 0)
            {
                if (dsDatos.Tables[0].Rows[0]["ELC_ID"].ToString() != "")
                {
                    this.cboNombreLaboratorioSuelos.SelectedValue = dsDatos.Tables[0].Rows[0]["ELC_ID"].ToString();
                    this.txtOtroLabSuelos.Text = dsDatos.Tables[0].Rows[0]["ELC_OTRO"].ToString();
                    if (this.txtOtroLabSuelos.Text != "")
                        this.txtOtroLabSuelos.Visible = true;
                }
            }
        }

    }

    protected void cboNombreLaboratorioSuelos_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboNombreLaboratorioSuelos.SelectedItem.Text.Contains("Otro"))
        {
            this.lblOtroLabSuelos.Visible = true;
            this.txtOtroLabSuelos.Visible = true;
        }
        else
        {
            this.lblOtroLabSuelos.Visible = false;
            this.txtOtroLabSuelos.Visible = false;
        }
    }


    private void GuardarNombreLaboratoriosSuelos(string elcId, string nombreOtroLab)
    {
        cargarCboTipoReceptorH();
        CargarSuelosReceptores(dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString());
        if (dsDatos.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsDatos.Tables[0].Rows)
            {
                dr["ELC_ID"] = elcId;
                dr["ELC_OTRO"] = nombreOtroLab;
            }

            Contexto.guardarTabla(dsDatos, "EIH_PREDIOS_RECEP_VERTIMIENTO");
        }
    }


    protected void btnAsignarLaboratorioSuelos_Click(object sender, EventArgs e)
    {
        GuardarNombreLaboratoriosSuelos(this.cboNombreLaboratorioSuelos.SelectedValue, this.txtOtroLabSuelos.Text);
    }
    private void TipoMuestraSuelos()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ETM_ACTIVO", SqlDbType.Bit, 1, "ETM_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_TIPO_MUESTRA_CARACT_MUESTREO");
        this.cboTipoMuestra.DataSource = dsDatos;
        this.cboTipoMuestra.DataValueField = "ETM_ID";
        this.cboTipoMuestra.DataTextField = "ETM_TIPO_MUESTRA";
        this.cboTipoMuestra.DataBind();
        this.cboTipoMuestra.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    private void PeriodoClimatico()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ECM_ACTIVO", SqlDbType.Bit, 1, "ECM_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_PERIODO_CLIMATICO_MUEST_EST_CAL");
        this.cboPeridoClimatico.DataSource = dsDatos;
        this.cboPeridoClimatico.DataTextField = "ECM_PERIODO_CLIMAT_EST_CAL";
        this.cboPeridoClimatico.DataValueField = "ECM_ID";
        this.cboPeridoClimatico.DataBind();
        this.cboPeridoClimatico.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }



    private void GuardarCaracteristicasCalidad(string tipoMuestra, string periodoClimatico, string receptorVertimiento)
    {
        cargarCboTipoReceptorH();
        string etvId = dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString();
        CargarSuelosReceptores(etvId);
        foreach (DataRow dr in dsDatos.Tables[0].Rows)
        {
            if (dr["EPV_ID"].ToString() == receptorVertimiento)
            {
                dr["ETM_ID"] = tipoMuestra;
                dr["ECM_ID"] = periodoClimatico;
            }
        }
        Contexto.guardarTabla(dsDatos, "EIH_PREDIOS_RECEP_VERTIMIENTO");
        VisualizaSuelosReceptores(etvId);

    }


    protected void btnAsignarcarFuentes_Click(object sender, EventArgs e)
    {
        this.Page.Validate("SeleccioneFuenteReceptores");
        if (!Page.IsValid)
            return;
        if (!InformacionAsociadaSuelos(this.cboReceptorVertimientos.SelectedValue, "98") && this.cboTipoMuestra.SelectedValue == "0")
        {
            Mensaje.MostrarMensaje(this.Page, "El tipo de Muestra no puede tener Duracion de Muestreo");
            return;
        }
        if (!InformacionAsociadaSuelos(this.cboReceptorVertimientos.SelectedValue, "97") && this.cboTipoMuestra.SelectedValue == "1")
        {
            Mensaje.MostrarMensaje(this.Page, "El tipo de Muestra no puede tener Hora de Muestreo");
            return;
        }

        GuardarCaracteristicasCalidad(this.cboTipoMuestra.SelectedValue, this.cboPeridoClimatico.SelectedValue, this.cboReceptorVertimientos.SelectedValue);

    }

    private void llamarPopUpFuentesSitios(string epvId, string epcId)
    {
        string strScript = "<script language='JavaScript'>" +
            "window.open('InfoAdicional/ReceptorVertimientoSuelos.aspx?tip=" + epcId + "&EPV_ID=" + epvId + "&sitios=" + this.txtCantidadSitiosSuelos.Text + "&btn=" + btnActualizarGrillaSuelos.ClientID + "','Pruebas','location=no,resizable=yes,scrollbars=yes')" +
            "</script>";
        Page.RegisterStartupScript("PopupScript", strScript);
    }

    private bool InformacionAsociadaSuelos(string epvId, string epcId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EPV_ID", SqlDbType.Int, 21, "EPV_ID");
        par.Value = epvId;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@EPC_ID", SqlDbType.Int, 21, "EPC_ID");
       // Quemamos Valor
        par2.Value = epcId;
        parametros.Add(par2);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_PARAM_MUEST_SUELO_VERT");
        if (dsDatos.Tables[0].Rows.Count > 0)
        {
            return false;
        }
        return true;
    }

    protected void txtFechadeMuestreo_Click(object sender, EventArgs e)
    {
        this.Page.Validate("SeleccioneFuenteReceptores");
        if (!Page.IsValid)
            return;
        if (InformacionAsociadaSuelos(this.cboReceptorVertimientos.SelectedValue, "96"))
            llamarPopUpFuentesSitios(this.cboReceptorVertimientos.SelectedValue, "96");
        else
            Mensaje.MostrarMensaje(this.Page, "Ya existe Información Asociada a este Item");

    }

    protected void btnHoraMuestreo_Click(object sender, EventArgs e)
    {
        this.Page.Validate("SeleccioneFuenteReceptores");
        if (!Page.IsValid)
            return;

        cargarCboTipoReceptorH();
        string etvId = dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString();
        CargarSuelosReceptores(etvId);
        //Sigo quemando cosas Tipo de Muestra = 1

        DataRow[] drs = dsDatos.Tables[0].Select("ETM_ID=1 AND EPV_ID=" + this.cboReceptorVertimientos.SelectedValue);
        if (drs.Length > 0)
        {
            Mensaje.MostrarMensaje(this.Page, "No se puede agregar Duracion Con este tipo de Muestra.");
            return;
        }

        if (InformacionAsociadaSuelos(this.cboReceptorVertimientos.SelectedValue, "97"))        
            llamarPopUpFuentesSitios(this.cboReceptorVertimientos.SelectedValue, "97");
        else
            Mensaje.MostrarMensaje(this.Page, "Ya existe Información Asociada a este Item");
    }

    protected void btnDuracionMuestreoSuelo_Click(object sender, EventArgs e)
    {
        this.Page.Validate("SeleccioneFuenteReceptores");
        if (!Page.IsValid)
            return;

        cargarCboTipoReceptorH();
        string etvId = dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString();
        CargarSuelosReceptores(etvId);
       // Sigo quemando cosas Tipo de Muestra = 1

        DataRow[] drs = dsDatos.Tables[0].Select("ETM_ID=0 AND EPV_ID=" + this.cboReceptorVertimientos.SelectedValue);
        if (drs.Length > 0)
        {
            Mensaje.MostrarMensaje(this.Page, "No se puede agregar Duración Con este Tipo de Muestra.");
            return;
        }

        if (InformacionAsociadaSuelos(this.cboReceptorVertimientos.SelectedValue, "98"))        
            llamarPopUpFuentesSitios(this.cboReceptorVertimientos.SelectedValue, "98");        
        else
            Mensaje.MostrarMensaje(this.Page, "Ya existe Información Asociada a este Item");
    }


    private void CargarCaracteristicasFisicoQuimicasSuelos()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EPC_ACTIVO", SqlDbType.Bit, 1, "EPC_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@ETP_ID", SqlDbType.Int, 10, "ETP_ID");
        par2.Value = 2;
        parametros.Add(par2);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_PARAMETROS_EST_CALIDAD");
        //quemo los valores que solo se necesitan        

        this.cboCaracterisFisicoQ.DataSource = dsDatos;
        this.cboCaracterisFisicoQ.DataTextField = "EPC_PARAMETROS_EST_CALIDAD";
        this.cboCaracterisFisicoQ.DataValueField = "EPC_ID";
        this.cboCaracterisFisicoQ.DataBind();
        this.cboCaracterisFisicoQ.Items.Insert(0, new ListItem("Seleccione..", "-1"));

    }
    protected void btnAsignarCarSuelosFisicoQ_Click(object sender, EventArgs e)
    {
        this.Page.Validate("SeleccioneFuenteReceptores");
        this.Page.Validate("ReceptoresVertimientos");
        if (!Page.IsValid)
            return;
        if (InformacionAsociadaSuelos(this.cboReceptorVertimientos.SelectedValue, this.cboCaracterisFisicoQ.SelectedValue))
            llamarPopUpFuentesSitios(this.cboReceptorVertimientos.SelectedValue, this.cboCaracterisFisicoQ.SelectedValue);
        else
            Mensaje.MostrarMensaje(this.Page, "Ya existe Información Asociada a este Item");

    }

    protected void tbnAgregarMetales_Click(object sender, EventArgs e)
    {
        this.Page.Validate("SeleccioneFuenteReceptores");
        this.Page.Validate("ReceptoresVertimientos");
        if (!Page.IsValid)
            return;
        llamarPopUpFuentesSitios(this.cboReceptorVertimientos.SelectedValue, "106");
    }

    private void CargarReceptoresVertimientosDetalles()
    {
        SILPA.LogicaNegocio.ResumenEIA.Procedimientos objPro = new SILPA.LogicaNegocio.ResumenEIA.Procedimientos();
        cargarCboTipoReceptorH();
        if (dsDatos.Tables[0].Rows.Count > 0)
        {
            DataSet ds = objPro.ProcFuentesAguasVertimientosSuelos(dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString());
 
            this.grvReceptoresVertimientos.DataSource = ds;
            this.grvReceptoresVertimientos.DataBind();
        }


    }

    private void CargarCalidadFisicoquimicasSuelos(string epvId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EPV_ID", SqlDbType.Int, 21, "EPV_ID");
        par.Value = epvId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_PARAM_MUEST_SUELO_VERT");
    }

    private void EliminarCalidadFisicoquimicasSuelos(string epvId, string epcId)
    {
        CargarCalidadFisicoquimicasSuelos(epvId);
        DataSet dsTemp = dsDatos;
        int i = 0;
        for (i = dsDatos.Tables[0].Rows.Count - 1; i >= 0; i--)
        {
            if (dsDatos.Tables[0].Rows[i]["EPC_ID"].ToString() == epcId)
            {
                dsTemp.Tables[0].Rows[i].Delete();
            }
        }
        Contexto.guardarTabla(dsTemp, "EIH_PARAM_MUEST_SUELO_VERT");
        CargarReceptoresVertimientosDetalles();

    }

    protected void grvReceptoresVertimientos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row;
        int index = Convert.ToInt32(e.CommandArgument);

        if (index < 0)
            return;
        if (e.CommandName == "Eliminar")
        {
            row = grvReceptoresVertimientos.Rows[index];
            Label lblEpvId = (Label)row.FindControl("lblEpvId");
            Label lblEpcId = (Label)row.FindControl("lblEpcId");
            EliminarCalidadFisicoquimicasSuelos(lblEpvId.Text, lblEpcId.Text);
        }
    }



    protected void btnActualizarGrillaSuelos_Click(object sender, EventArgs e)
    {
        CargarReceptoresVertimientosDetalles();
    }

    protected void grvReceptoresVertimientos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
    }

    protected void btnNuevosReceptoresVertimiento_Click(object sender, EventArgs e)
    {
        this.plhReceptoresVertimientos.Visible = true;
    }

    protected void btnCancelarFisicoquimicosSuelos_Click(object sender, EventArgs e)
    {
        this.plhReceptoresVertimientos.Visible = false;
    }

    #endregion

    #region 4.2.1.3.1 Descripción de los Acuíferos

    private void CargarTiposAcuifero()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ETA_ACTIVO", SqlDbType.Bit, 1, "ETA_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_TIPO_ACUIFERO");

        this.cboTipoAcuifero.DataSource = dsDatos;
        this.cboTipoAcuifero.DataTextField = "ETA_TIPO_ACUIFERO";
        this.cboTipoAcuifero.DataValueField = "ETA_ID";
        this.cboTipoAcuifero.DataBind();
        this.cboTipoAcuifero.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }
    private void CargarOtrosUsosAcuifero()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EUA_ACTIVO", SqlDbType.Bit, 1, "EUA_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_OTROS_USOS_ACUIFERO");

        this.cboOtrosUsos.DataSource = dsDatos;
        this.cboOtrosUsos.DataTextField = "EUA_OTROS_USOS_ACUIFERO";
        this.cboOtrosUsos.DataValueField = "EUA_ID";
        this.cboOtrosUsos.DataBind();
        this.cboOtrosUsos.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    private void CargarInfoAcuiferos(string evtId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EVT_ID", SqlDbType.Int, 20, "EVT_ID");
        par.Value = evtId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_ACUIFE_VERTIMIENTO");
    }


    private void VisualizarInfoAcuiferos(string evtId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EVT_ID", SqlDbType.Int, 20, "EVT_ID");
        par.Value = evtId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIV_ACUIFE_VERTIMIENTO");

        this.grvInformacionAcuiferos.DataSource = dsDatos;
        this.grvInformacionAcuiferos.DataBind();

    }

    private void GuardarInfoAcuiferos()
    {

        cargarCboTipoReceptorH();
        string evtId = dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString();
        CargarInfoAcuiferos(evtId);

        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["ETA_ID"] = this.cboTipoAcuifero.SelectedValue;
        dr["EUA_ID"] = this.cboOtrosUsos.SelectedValue;
        dr["EVT_ID"] = evtId;
        dr["EAV_CAUDAL_VERTER"] = this.txtCaudalVerter.Text;
        dr["EAV_MET_VERTIMIENTO"] = this.txtMetodoInyeccion.Text;
        dr["EAV_CONDUCT_HIDRAULICA"] = this.txtConductividadHidraulica.Text;
        dr["EAV_COEF_ALMACENAMIENTO"] = this.txtCoeficienteAlmacenamiento.Text;
        dr["EAV_PROF_INYECCION"] = this.txtProfundidadInyeccion.Text;

        dsDatos.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsDatos, "EIH_ACUIFE_VERTIMIENTO");

        VisualizarInfoAcuiferos(evtId);

    }

    private void EliminarInfoAcuiferos(int index)
    {
        cargarCboTipoReceptorH();
        string evtId = dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString();
        CargarInfoAcuiferos(evtId);
        dsDatos.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsDatos, "EIH_ACUIFE_VERTIMIENTO");
        VisualizarInfoAcuiferos(evtId);
    }

    protected void btnAgregarAcuiferos_Click(object sender, EventArgs e)
    {
        GuardarInfoAcuiferos();
        this.cboTipoAcuifero.SelectedValue = "-1";
        this.cboOtrosUsos.SelectedValue = "-1";
        this.txtCaudalVerter.Text = "";
        this.txtMetodoInyeccion.Text = "";
        this.txtConductividadHidraulica.Text = "";
        this.txtCoeficienteAlmacenamiento.Text = "";
        this.txtProfundidadInyeccion.Text = "";

    }

    protected void btnCancelarAcuiferos_Click(object sender, EventArgs e)
    {
        this.plhInformacionAcuiferos.Visible = false;
        this.cboTipoAcuifero.SelectedValue = "-1";
        this.cboOtrosUsos.SelectedValue = "-1";
        this.txtCaudalVerter.Text = "";
        this.txtMetodoInyeccion.Text = "";
        this.txtConductividadHidraulica.Text = "";
        this.txtCoeficienteAlmacenamiento.Text = "";
        this.txtProfundidadInyeccion.Text = "";
    }

    protected void btnDescripcionAcuiferos_Click(object sender, EventArgs e)
    {
        this.plhInformacionAcuiferos.Visible = true;
    }

    protected void grvInformacionAcuiferos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarInfoAcuiferos(e.RowIndex);
    }

    #endregion

    #region 4.2.2.1 Descripción de los Vertimientos


    private void CargarTipoVertimiento()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ETV_ACTIVO", SqlDbType.Bit, 1, "ETV_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_TIPO_VERTIMIENTO");

        this.cboTipoVertimiento.DataSource = dsDatos;
        this.cboTipoVertimiento.DataTextField = "ETV_TIPO_VERTIMIENTO";
        this.cboTipoVertimiento.DataValueField = "ETV_ID";
        this.cboTipoVertimiento.DataBind();
        this.cboTipoVertimiento.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }
    private void CargarUnidadCaudal()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EUC_ACTIVO", SqlDbType.Bit, 1, "EUC_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_UNIDAD_CAUDAL");

        this.cboUnidadCausal.DataSource = dsDatos;
        this.cboUnidadCausal.DataTextField = "EUC_UNIDAD_CAUDAL";
        this.cboUnidadCausal.DataValueField = "EUC_ID";
        this.cboUnidadCausal.DataBind();
        this.cboUnidadCausal.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    private void CargarDescargaPrevista()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@CDP_ACTIVO", SqlDbType.Bit, 1, "CDP_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_CLASE_DESCARGA_PREVISTA");

        this.cboDescargaPrevista.DataSource = dsDatos;
        this.cboDescargaPrevista.DataTextField = "CDP_NOMBRE";
        this.cboDescargaPrevista.DataValueField = "CDP_ID";
        this.cboDescargaPrevista.DataBind();
        this.cboDescargaPrevista.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }



    protected void cboTipoVertimiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoVertimiento.SelectedItem.Text.Contains("Otros"))
            this.trTipoVer.Visible = true;
        else
        {
            this.trTipoVer.Visible = false;
            this.txtTipoVertOtro.Text = "";
        }
    }
    private void CargarDescripcionVertimientos(string evtId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EVT_ID", SqlDbType.Int, 20, "EVT_ID");
        par.Value = evtId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_DESCRIP_VERTIMIENTOS");
    }


    private void VisualizarDescripcionVerimientos(string evtId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EVT_ID", SqlDbType.Int, 20, "EVT_ID");
        par.Value = evtId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIV_DESCRIP_VERTIMIENTOS");

        this.grvDescrVerti.DataSource = dsDatos;
        this.grvDescrVerti.DataBind();

        if (dsDatos.Tables[0].Rows.Count > 0)
        {
            this.cboUnidadCausal.SelectedValue = dsDatos.Tables[0].Rows[0]["EUC_ID"].ToString();
            this.grvDescrVerti.HeaderRow.Cells[2].Text = "Caudal se Preve Vertir(" + dsDatos.Tables[0].Rows[0]["EUC_UNIDAD_CAUDAL"].ToString() + ")";
        }


        this.cboTipoVerDesc.DataSource = dsDatos;
        this.cboTipoVerDesc.DataTextField = "TIPO_VERT";
        this.cboTipoVerDesc.DataValueField = "EDV_ID";
        this.cboTipoVerDesc.DataBind();
        this.cboTipoVerDesc.Items.Insert(0, new ListItem("Seleccione..", "-1"));

    }

    private void GuardarDescripcionVertimiento()
    {
        cargarCboTipoReceptorH();
        string evtId = dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString();
        CargarDescripcionVertimientos(evtId);
        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["ETV_ID"] = this.cboTipoVertimiento.SelectedValue;
        dr["EVT_ID"] = evtId;
        dr["EUC_ID"] = this.cboUnidadCausal.SelectedValue;
        dr["CDP_ID"] = this.cboDescargaPrevista.Text;
        dr["EDV_CAUDAL_VERTER"] = this.txtCaudalVertir.Text;
        dr["EDV_COOR_NORTE_PTO_DESC"] = this.CoorDescargaPrevis.CoorNorte;
        dr["EDV_COOR_ESTE_PTO_DESC"] = this.CoorDescargaPrevis.CoorEste;
        dr["EDV_DESC_PTO_DESCARGA"] = this.txtDescPuntoPrev.Text;
        if (string.IsNullOrEmpty(this.txtTipoVertOtro.Text))
            dr["ETV_OTRO"] = DBNull.Value;
        else
            dr["ETV_OTRO"] = this.txtTipoVertOtro.Text;
        dsDatos.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsDatos, "EIH_DESCRIP_VERTIMIENTOS");


        CargarDescripcionVertimientos(evtId);
        ActualizarDescripcionVertimiento(this.cboUnidadCausal.SelectedValue);
        VisualizarDescripcionVerimientos(evtId);
    }

    private void EliminarDescripcionVertimiento(int index)
    {
        cargarCboTipoReceptorH();
        string evtId = dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString();
        CargarDescripcionVertimientos(evtId);
        dsDatos.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsDatos, "EIH_DESCRIP_VERTIMIENTOS");
        VisualizarDescripcionVerimientos(evtId);
    }

    private void ActualizarDescripcionVertimiento(string eucId)
    {
        foreach (DataRow dr in dsDatos.Tables[0].Rows)
        {
            dr["EUC_ID"] = eucId;
        }

        Contexto.guardarTabla(dsDatos, "EIH_DESCRIP_VERTIMIENTOS");
    }

    protected void btnAgregarDescVerti_Click(object sender, EventArgs e)
    {
        if (this.CoorDescargaPrevis.Valido)
        {
            GuardarDescripcionVertimiento();
            this.cboTipoVertimiento.SelectedValue = "-1";
            this.cboDescargaPrevista.SelectedValue = "-1";
            this.txtCaudalVertir.Text = "";
            this.CoorDescargaPrevis.CoorNorte = "";
            this.CoorDescargaPrevis.CoorEste = "";
            this.txtDescPuntoPrev.Text = "";
        }
    }

    protected void btnDescripcionVert_Click(object sender, EventArgs e)
    {
        this.plhDescrVerti.Visible = true;
    }


    protected void btnCancelarDescVerti_Click(object sender, EventArgs e)
    {
        this.plhDescrVerti.Visible = false;
        this.cboTipoVertimiento.SelectedValue = "-1";
        this.cboDescargaPrevista.SelectedValue = "-1";
        this.txtCaudalVertir.Text = "";
        this.CoorDescargaPrevis.CoorNorte = "";
        this.CoorDescargaPrevis.CoorEste = "";
        this.txtDescPuntoPrev.Text = "";
    }

    protected void grvDescrVerti_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarDescripcionVertimiento(e.RowIndex);
    }

    protected void cboUnidadCausal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboUnidadCausal.SelectedValue != "-1")
        {
            cargarCboTipoReceptorH();
            string evtId = dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString();
            CargarDescripcionVertimientos(evtId);
            ActualizarDescripcionVertimiento(this.cboUnidadCausal.SelectedValue);
            VisualizarDescripcionVerimientos(evtId);
        }
    }


    #endregion

    #region 4.2.2.2 Tipos de Tratamientos Previstos

    private void CargarTratamientoVerDesc()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ETT_ACTIVO", SqlDbType.Bit, 1, "ETT_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_TIPOS_TIPOS_TRATAMIENTOS");

        this.cboTratamientoVerDesc.DataSource = dsDatos;
        this.cboTratamientoVerDesc.DataTextField = "ETT_TIPOS_TIPOS_TRATAMIENTOS";
        this.cboTratamientoVerDesc.DataValueField = "ETT_ID";
        this.cboTratamientoVerDesc.DataBind();
        this.cboTratamientoVerDesc.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    private void CargarTipoTratamientoVerDesc(string tratamiento)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ETV_ACTIVO", SqlDbType.Bit, 1, "ETV_ACTIVO");
        par.Value = 1;
        parametros.Add(par);

        SqlParameter par2 = new SqlParameter("@ETT_ID", SqlDbType.Int, 21, "ETT_ID");
        par2.Value = tratamiento;
        parametros.Add(par2);

        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_TIPOS_TRATAMIENTO_VERT");

        this.cboTipoTratamientoVerDesc.DataSource = dsDatos;
        this.cboTipoTratamientoVerDesc.DataTextField = "ETV_TIPOS_TRATAMIENTO_VERT";
        this.cboTipoTratamientoVerDesc.DataValueField = "ETV_ID";
        this.cboTipoTratamientoVerDesc.DataBind();
        this.cboTipoTratamientoVerDesc.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    protected void cboTratamientoVerDesc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTratamientoVerDesc.SelectedValue != "-1")
        {
            CargarTipoTratamientoVerDesc(this.cboTratamientoVerDesc.SelectedValue);
        }
    }

    protected void cboTipoTratamientoVerDesc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoTratamientoVerDesc.SelectedItem.Text.Contains("Otro"))
            this.trOtroTipoTra.Visible = true;
        else
        {
            this.trOtroTipoTra.Visible = false;
            this.txtOtroTipoTra.Text = "";
        }
    }

    private void CargarTipoDeTratamientoPrevistos(string edvId)
    {

        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();

        SqlParameter par = new SqlParameter("@EDV_ID", SqlDbType.Int, 21, "EDV_ID");
        par.Value = edvId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_TRAT_TIPO_VERT_TIPO_TRAT");

    }

    private void GuardarTipoTratamientosPrevistos()
    {
        string edvId = this.cboTipoVerDesc.SelectedValue;
        CargarTipoDeTratamientoPrevistos(edvId);

       // Validar

        SILPA.LogicaNegocio.ResumenEIA.Procedimientos objPro = new SILPA.LogicaNegocio.ResumenEIA.Procedimientos();        
        string Exist = objPro.ProcExisteRelaciionTipoTratamiento(edvId,this.cboTipoTratamientoVerDesc.SelectedValue);


        if (Exist != "0")
        {
            foreach (DataRow dr in dsDatos.Tables[0].Rows)
            {
                if (dr["ETV_ID"].ToString() == Exist)
                {
                    dr["ETV_ID"] = this.cboTipoTratamientoVerDesc.SelectedValue;
                    if (string.IsNullOrEmpty(this.txtOtroTipoTra.Text))
                        dr["ETV_OTRO"] = DBNull.Value;
                    else
                        dr["ETV_OTRO"] = this.txtOtroTipoTra.Text;                    
                }
            }
        }
        else
        {
            DataRow dr = dsDatos.Tables[0].NewRow();
            dr["EDV_ID"] = edvId;
            dr["ETV_ID"] = this.cboTipoTratamientoVerDesc.SelectedValue;
            if (string.IsNullOrEmpty(this.txtOtroTipoTra.Text))
                dr["ETV_OTRO"] = DBNull.Value;
            else
                dr["ETV_OTRO"] = this.txtOtroTipoTra.Text;
            dsDatos.Tables[0].Rows.Add(dr);
        }        

        Contexto.guardarTabla(dsDatos, "EIH_TRAT_TIPO_VERT_TIPO_TRAT");

        VisualizarTipoDeTratamientoPrevistos();
        
    }

    private void VisualizarTipoDeTratamientoPrevistos()
    {
        SILPA.LogicaNegocio.ResumenEIA.Procedimientos objPro = new SILPA.LogicaNegocio.ResumenEIA.Procedimientos();
        cargarCboTipoReceptorH();
        if (dsDatos.Tables[0].Rows.Count > 0)
        {
            DataSet ds = objPro.ProcTiposTratamientosVertimientos(dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString());
            this.grvTipoTraPrevistos.DataSource = ds;
            this.grvTipoTraPrevistos.DataBind();
        }

    }

    protected void btnTipoTraPrevistos_Click(object sender, EventArgs e)
    {
        this.plhTipoTraPrevistos.Visible = true;
    }

    protected void btnCancelarTipoTraPrevistos_Click(object sender, EventArgs e)
    {
        this.plhTipoTraPrevistos.Visible = false;
        this.cboTipoVerDesc.SelectedValue = "-1";
        this.cboTratamientoVerDesc.SelectedValue = "-1";
        if (this.cboTipoTratamientoVerDesc.Items.Count > 0)
            this.cboTipoTratamientoVerDesc.SelectedValue = "-1";

    }

    protected void btnAgregarTipoTraPrevistos_Click(object sender, EventArgs e)
    {
        GuardarTipoTratamientosPrevistos();
        this.cboTipoVerDesc.SelectedValue = "-1";
        this.cboTratamientoVerDesc.SelectedValue = "-1";
        if (this.cboTipoTratamientoVerDesc.Items.Count > 0)
            this.cboTipoTratamientoVerDesc.SelectedValue = "-1";
    }

    protected void grvTipoTraPrevistos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
    }

    private void EliminarTipoDeTratamientoPrevistos(string EvtId, string Codigo)
    {
        CargarTipoDeTratamientoPrevistos(Codigo);
        DataSet dsTemp = dsDatos;
        int i = 0;
        for (i = dsDatos.Tables[0].Rows.Count - 1; i >= 0; i--)
        {
            dsTemp.Tables[0].Rows[i].Delete();
        }
        Contexto.guardarTabla(dsTemp, "EIH_TRAT_TIPO_VERT_TIPO_TRAT");
        VisualizarTipoDeTratamientoPrevistos();

    }

    protected void grvTipoTraPrevistos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row;
        int index = Convert.ToInt32(e.CommandArgument);

        if (index < 0)
            return;
        if (e.CommandName == "Eliminar")
        {
            row = grvTipoTraPrevistos.Rows[index];
            Label lblEvtId = (Label)row.FindControl("lblEvtId");
            Label lblCodigo = (Label)row.FindControl("lblCodigo");
            EliminarTipoDeTratamientoPrevistos(lblEvtId.Text, lblCodigo.Text);
        }
    }

    #endregion

    #region 4.2.2.2 Tipos de Tratamientos Previstos

    private void CargarCaracteristicasBacteri()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EPC_ACTIVO", SqlDbType.Bit, 1, "EPC_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@ETP_ID", SqlDbType.Int, 10, "ETP_ID");
        par2.Value = 5;
        parametros.Add(par2);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_PARAMETROS_EST_CALIDAD");

        this.cboCaracterizacionBacter.DataSource = dsDatos;
        this.cboCaracterizacionBacter.DataTextField = "EPC_PARAMETROS_EST_CALIDAD";
        this.cboCaracterizacionBacter.DataValueField = "EPC_ID";
        this.cboCaracterizacionBacter.DataBind();
        this.cboCaracterizacionBacter.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    private void llamarPopUpCalidad(string epcId, string evtId, string sitios)
    {
        string strScript = "<script language='JavaScript'>" +
            "window.open('InfoAdicional/ReceptorVertimientoCalidad.aspx?tip=" + epcId + "&EVT_ID=" + evtId + "&sitios=" + sitios + "&btn=" + btnActualizaGrilla2.ClientID + "','Pruebas','location=no,resizable=yes,scrollbars=yes')" +
            "</script>";
        Page.RegisterStartupScript("PopupScript", strScript);
    }

    protected void btnAgregarCaracterizacionFisica_Click(object sender, EventArgs e)
    {
        cargarCboTipoReceptorH();
        string evtId = dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString();
        CargarDescripcionVertimientos(evtId);
        if (dsDatos.Tables[0].Rows.Count > 0)
            llamarPopUpCalidad(this.cboCaracterizacionFis.SelectedValue, evtId, dsDatos.Tables[0].Rows.Count.ToString());
        else
            Mensaje.MostrarMensaje(this.Page, "No existen Tipos de Vertimiento");
    }

    protected void dbtAgregarCaracterizacionQuimica_Click(object sender, EventArgs e)
    {
        cargarCboTipoReceptorH();
        string evtId = dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString();
        CargarDescripcionVertimientos(evtId);
        if (dsDatos.Tables[0].Rows.Count > 0)
            llamarPopUpCalidad(this.cboCaracterizacionQuim.SelectedValue, evtId, dsDatos.Tables[0].Rows.Count.ToString());
        else
            Mensaje.MostrarMensaje(this.Page, "No existen Tipos de Vertimiento");
    }

    protected void btnAgregarCaracterizacionBioQuim_Click(object sender, EventArgs e)
    {
        cargarCboTipoReceptorH();
        string evtId = dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString();
        CargarDescripcionVertimientos(evtId);
        if (dsDatos.Tables[0].Rows.Count > 0)
            llamarPopUpCalidad(this.cboCaracterizacionBacter.SelectedValue, evtId, dsDatos.Tables[0].Rows.Count.ToString());
        else
            Mensaje.MostrarMensaje(this.Page, "No existen Tipos de Vertimiento");
    }

    private void CargarCalidadFisicoquimica()
    {
        SILPA.LogicaNegocio.ResumenEIA.Procedimientos objPro = new SILPA.LogicaNegocio.ResumenEIA.Procedimientos();
        cargarCboTipoReceptorH();
        DataSet ds = objPro.ProcCalidadFisicoquimicas(dsDatos.Tables[0].Rows[0]["EVT_ID"].ToString());
        this.grvCalidadFisicoQ.DataSource = ds;
        this.grvCalidadFisicoQ.DataBind();

    }

    protected void btnActualizaGrilla2_Click(object sender, EventArgs e)
    {
        CargarCalidadFisicoquimica();
    }


    private void EliminarCalidadFisicoqimicas(string EvtId, string EcvId)
    {

        DataSet dsDatos4 = new DataSet();
        List<SqlParameter> parametros1 = new List<SqlParameter>();
        SqlParameter par1 = new SqlParameter("@ECV_ID", SqlDbType.Int, 21, "ECV_ID");
        par1.Value = EcvId;
        parametros1.Add(par1);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros1, dsDatos4, "EIH_VALOR_ESP_EST_CALIDAD_TIPO_VERT");

        DataSet dsTemp = dsDatos4;
        int i = 0;
        for (i = dsDatos4.Tables[0].Rows.Count - 1; i >= 0; i--)
        {
            dsTemp.Tables[0].Rows[i].Delete();
        }


        Contexto.guardarTabla(dsTemp, "EIH_VALOR_ESP_EST_CALIDAD_TIPO_VERT");

        DataSet dsDatos2 = new DataSet();
        List<SqlParameter> parametros3 = new List<SqlParameter>();
        SqlParameter par3 = new SqlParameter("@ECV_ID", SqlDbType.Int, 21, "ECV_ID");
        par3.Value = EcvId;
        parametros3.Add(par3);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros3, dsDatos2, "EIH_CALIDAD_ESP_VERTIMIENTO");

  
        dsTemp = dsDatos2;
        for (i = dsDatos2.Tables[0].Rows.Count - 1; i >= 0; i--)
        {
            dsTemp.Tables[0].Rows[i].Delete();
        }


        Contexto.guardarTabla(dsTemp, "EIH_CALIDAD_ESP_VERTIMIENTO");

        CargarCalidadFisicoquimica();

    }

    protected void grvCalidadFisicoQ_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[4].Visible = false;

    }



    protected void grvCalidadFisicoQ_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row;
        int index = Convert.ToInt32(e.CommandArgument);

        if (index < 0)
            return;
        if (e.CommandName == "Eliminar")
        {
            row = grvCalidadFisicoQ.Rows[index];
            Label lblEvtId = (Label)row.FindControl("lblEvtId");
            Label lblEcvId = (Label)row.FindControl("lblEcvId");
            EliminarCalidadFisicoqimicas(lblEvtId.Text, lblEcvId.Text);
        }
    }
    protected void btnCalidadFisicoQ_Click(object sender, EventArgs e)
    {
        this.plhCalidadFisicoQ.Visible = true;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.plhCalidadFisicoQ.Visible = false;
    }

    #endregion

    #region 4.3.1 Información General de las Fuentes

    private void CargarOcupacionCauces()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_INFO_FUENTES_OCUPACION_CAUCES");



    }
    private void VisualizarOcupacionCauces()
    {
        CargarOcupacionCauces();
        this.grvInfoCauces.DataSource = dsDatos;
        this.grvInfoCauces.DataBind();
    }

    private void GuardarOcupacionCauces()
    {
        CargarOcupacionCauces();
        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EFO_FECHA_CREACION"] = Fecha;
        dr["EFO_NOMBRE_DRENAJE"] = this.txtNombreDrenaje.Text;
        dr["EFO_TIPO_DRENAJE"] = this.txtTipoDrenaje.Text;
        dsDatos.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsDatos, "EIH_INFO_FUENTES_OCUPACION_CAUCES");
        CargarOcupacionCauces();

        GuardarCoordenadasOcupacionCauces(dsDatos.Tables[0].Rows[dsDatos.Tables[0].Rows.Count - 1]["EFO_ID"].ToString());


        VisualizarOcupacionCauces();
    }

    private void CargarCoordenadasOcupacionCauces(string efoId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EFO_ID", SqlDbType.Int, 21, "EFO_ID");
        par.Value = efoId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_COOR_INFO_FUENT_OCUP");
    }

    private void GuardarCoordenadasOcupacionCauces(string efoId)
    {
        CargarCoordenadasOcupacionCauces(efoId);
        foreach (DataRow row in this.UbicacionPoliCauces.Coordenadas.Rows)
        {
            DataRow dr = dsDatos.Tables[0].NewRow();
            dr["EFO_ID"] = efoId;
            dr["ECI_COOR_NORTE"] = row["COORNORTE"].ToString();
            dr["ECI_COOR_ESTE"] = row["COORESTE"].ToString();
            dsDatos.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsDatos, "EIH_COOR_INFO_FUENT_OCUP");
    }

    protected void btnAgregarInfoCauces_Click(object sender, EventArgs e)
    {
        DataTable dt = this.UbicacionPoliCauces.Coordenadas;
        if (dt != null)
        {
            if (dt.Rows.Count == 5)
            {
                GuardarOcupacionCauces();
                this.UbicacionPoliCauces.LimpiarObjetos();
                this.txtNombreDrenaje.Text = "";
                this.txtTipoDrenaje.Text = "";
            }
            else
                Mensaje.MostrarMensaje(this.Page, "Debe Ingresar 5 Coordenadas");
        }
        else
            Mensaje.MostrarMensaje(this.Page, "No se han agregado Coordenadas");
    }

    protected void btnInfoCauces_Click(object sender, EventArgs e)
    {
        this.plhInfoCauces.Visible = true;
    }
    protected void btnCancelarInfoCauces_Click(object sender, EventArgs e)
    {
        this.plhInfoCauces.Visible = false;
        this.txtNombreDrenaje.Text = "";
        this.txtTipoDrenaje.Text = "";
        DataTable dt = this.UbicacionPoliCauces.Coordenadas;
        if (dt != null)
        {
            this.UbicacionPoliCauces.LimpiarObjetos();
        }

    }

    private void EliminarInfoCauces(int index)
    {
        CargarOcupacionCauces();
        EliminarCoordenadasInfoCauces(dsDatos.Tables[0].Rows[index]["EFO_ID"].ToString());
        CargarOcupacionCauces();
        dsDatos.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsDatos, "EIH_INFO_FUENTES_OCUPACION_CAUCES");

        VisualizarOcupacionCauces();
    }
    private void EliminarCoordenadasInfoCauces(string efoid)
    {
        CargarCoordenadasOcupacionCauces(efoid);
        int i = 0;
        DataSet dsTemp = dsDatos;
        for (i = dsTemp.Tables[0].Rows.Count - 1; i >= 0; i--)
        {
            dsDatos.Tables[0].Rows[i].Delete();
        }
        Contexto.guardarTabla(dsDatos, "EIH_COOR_INFO_FUENT_OCUP");
    }

    protected void grvInfoCauces_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarInfoCauces(e.RowIndex);

    }

    #endregion

    #region 4.3.2 Ocupación de los Cauces de las Fuentes
    private void cargarcboTipoOcupacion()
    {
        DataSet ds = new DataSet();
        SoftManagement.Persistencia.Contexto.cargarTabla(ds, "EIB_TIPO_OCUPACION_CAUCE");
        this.cboTipoOcupacion.DataSource = ds.Tables[0];
        this.cboTipoOcupacion.DataTextField = "ETO_NOMBRE_OCUPACION";
        this.cboTipoOcupacion.DataValueField = "ETO_ID";
        this.cboTipoOcupacion.DataBind();
        cboTipoOcupacion.Items.Insert(0, new ListItem("Seleccione..", "-1"));

    }
    private void cargarTipoObra()
    {
        DataSet ds = new DataSet();
        Contexto.cargarTabla(ds, "EIB_TIPO_OBRA");
        this.cboTipoObraOcupacion.DataSource = ds.Tables[0];
        this.cboTipoObraOcupacion.DataTextField = "ETB_TIPO_NOMBRE";
        this.cboTipoObraOcupacion.DataValueField = "ETB_ID";
        this.cboTipoObraOcupacion.DataBind();
        cboTipoObraOcupacion.Items.Insert(0, new ListItem("Seleccione..", "-1"));

    }
    private void CargarOcupacionFuentes()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_OCUPACION_CAUCES_FUENTES");
    }
    private void CargarOcupacionVistaFuentes()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIV_OCUPACION_CAUCES_FUENTES");
    }
    private void VisualizarOcupacionCaucesFuentes()
    {
        CargarOcupacionVistaFuentes();
        this.grvOcupacionFuentes.DataSource = dsDatos;
        this.grvOcupacionFuentes.DataBind();
    }
    private void GuardarOcupacionCaucesFuentes()
    {
        CargarOcupacionFuentes();
        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EOC_ID"] =
        dr["EFO_ID"] =
        dr["EOC_INFO_VEL_SOC_ESTIMADA"] = txtInfoVelocSoca.Text;
        dr["EOC_DIMENSION_LOG"] = txtDimensionesLongitud.Text;
        dr["EOC_DIMENSION_ANCHO"] = txtDimensionesAncho.Text;
        dr["EOC_DIMENSION_OBRA_LOG"] = txtDimensionesObra.Text;
        dr["EIP_ID"] = IDProyecto;
        dr["ETO_ID"] = cboTipoOcupacion.SelectedValue;
        dr["ETB_ID"] = cboTipoObraOcupacion.SelectedValue;
        dr["EOC_DIMENSION_OBRA_ANCHO"] = txtDimensionObraAnho.Text;
        dsDatos.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsDatos, "EIH_OCUPACION_CAUCES_FUENTES");
        CargarOcupacionFuentes();
        GuardarCoordenadasOcupacionCaucesFuentes(dsDatos.Tables[0].Rows[dsDatos.Tables[0].Rows.Count - 1]["EOC_ID"].ToString());
        VisualizarOcupacionCaucesFuentes();
    }
    private void CargarCoordenadasOcupacionCaucesFuentes(string efoId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EOC_ID", SqlDbType.Int, 21, "EOC_ID");
        par.Value = efoId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_COOR_OCUP_CAU_FUENTES");
    }
    private void GuardarCoordenadasOcupacionCaucesFuentes(string efoId)
    {
        CargarCoordenadasOcupacionCaucesFuentes(efoId);
        foreach (DataRow row in this.ctrUbicacionPoliOcu.Coordenadas.Rows)
        {
            DataRow dr = dsDatos.Tables[0].NewRow();
            dr["EOC_ID"] = efoId;
            dr["ECC_COOR_NORTE"] = row["COORNORTE"].ToString();
            dr["ECC_COOR_ESTE"] = row["COORESTE"].ToString();
            dsDatos.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsDatos, "EIH_COOR_OCUP_CAU_FUENTES");
    }
    protected void btnAgregarOcupacionFuentes_Click(object sender, EventArgs e)
    {
        DataTable dt = this.ctrUbicacionPoliOcu.Coordenadas;
        if (dt != null)
        {
            if (dt.Rows.Count == 5)
            {
                GuardarOcupacionCaucesFuentes();
                this.ctrUbicacionPoliOcu.LimpiarObjetos();
                limpiarPlaceHolder(plhOcupacionFuentes);
            }
            else
                Mensaje.MostrarMensaje(this.Page, "Debe Ingresar 5 Coordenadas");
        }
        else
            Mensaje.MostrarMensaje(this.Page, "No se han agregado Coordenadas");
    }
    protected void btnOcupacionFuentes_Click(object sender, EventArgs e)
    {
        this.plhOcupacionFuentes.Visible = true;
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
    protected void btnCancelarOcupacionFuentes_Click(object sender, EventArgs e)
    {
        this.plhOcupacionFuentes.Visible = false;
        limpiarPlaceHolder(plhOcupacionFuentes);

        DataTable dt = this.ctrUbicacionPoliOcu.Coordenadas;
        if (dt != null)
        {
            this.ctrUbicacionPoliOcu.LimpiarObjetos();
        }

    }
    private void EliminarOcupacionFuentes(int index)
    {
        CargarOcupacionFuentes();
        EliminarCoordenadasOcupacionFuentes(dsDatos.Tables[0].Rows[index]["EOC_ID"].ToString());
        CargarOcupacionFuentes();
        dsDatos.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsDatos, "EIH_OCUPACION_CAUCES_FUENTES");

        VisualizarOcupacionCaucesFuentes();
    }
    private void EliminarCoordenadasOcupacionFuentes(string efoid)
    {
        CargarCoordenadasOcupacionCaucesFuentes(efoid);
        int i = 0;
        DataSet dsTemp = dsDatos;
        for (i = dsTemp.Tables[0].Rows.Count - 1; i >= 0; i--)
        {
            dsDatos.Tables[0].Rows[i].Delete();
        }
        Contexto.guardarTabla(dsDatos, "EIH_COOR_OCUP_CAU_FUENTES");
    }
    protected void grvOcupacionFuentes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarOcupacionFuentes(e.RowIndex);

    }
    #endregion

    #region 4.4.1 fuentes autorizadas

    private void CargarfuentesAutorizadas()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_FUENTES_AUTO_APROV_MATERIALES");
    }
    private void GuardarfuentesAutorizadas()
    {
        CargarfuentesAutorizadas();
        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EFM_ID"] =
        dr["EIP_ID"] = IDProyecto;
        dr["EFM_FECHA_CREACION"] = Fecha;
        dr["EFM_NOMBRE_FUENTE"] = txtNombreFuenteConstr.Text;
        dr["EFM_RESOL_AUTOR_MIN"] = txtAutMinResolucion.Text;
        dr["EFM_FECHA_EXP_AUTOR_MIN"] = txtAutMinFechaExp.Text;
        dr["EFM_VIGENCIA_AUTOR_MIN"] = txtAutMinVigencia.Text;
        dr["EFM_RESOL_AUTOR_AMB"] = txtAutAmbResolicion.Text;
        dr["EFM_FECHA_EXP_AUTOR_AMB"] = txtAutAmbFechaExpedicion.Text;
        dr["EFM_VIGENCIA_AUTOR_AMB"] = txtAutAmbVigencia.Text;
        dr["EFM_VOLUMEN"] = txtVolumenConstruccion.Text;
        dsDatos.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsDatos, "EIH_FUENTES_AUTO_APROV_MATERIALES");
        CargarfuentesAutorizadas();
        GuardarCoordenadasfuentesAutorizadas(dsDatos.Tables[0].Rows[dsDatos.Tables[0].Rows.Count - 1]["EFM_ID"].ToString());
        VisualizarfuentesAutorizadas();
    }

    private void VisualizarfuentesAutorizadas()
    {
        CargarfuentesAutorizadas();
        this.grvFuentesAut.DataSource = dsDatos.Tables[0];
        this.grvFuentesAut.DataBind();

    }
    private void CargarCoordenadasfuentesAutorizadas(string efoId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EFM_ID", SqlDbType.Int, 21, "EFM_ID");
        par.Value = efoId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_COOR_FUENT_AUTO_APROV");
    }
    private void GuardarCoordenadasfuentesAutorizadas(string efoId)
    {
        CargarCoordenadasfuentesAutorizadas(efoId);
        foreach (DataRow row in this.ctrUbicacionPoliConst.Coordenadas.Rows)
        {
            DataRow dr = dsDatos.Tables[0].NewRow();
            dr["EFM_ID"] = efoId;
            dr["ECF_COOR_NORTE"] = row["COORNORTE"].ToString();
            dr["ECF_COOR_ESTE"] = row["COORESTE"].ToString();
            dsDatos.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsDatos, "EIH_COOR_FUENT_AUTO_APROV");
    }
    private void EliminarfuentesAutorizadas(int index)
    {
        CargarfuentesAutorizadas();
        EliminarCoordenadasfuentesAutorizadas(dsDatos.Tables[0].Rows[index]["EFM_ID"].ToString());
        CargarfuentesAutorizadas();
        dsDatos.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsDatos, "EIH_FUENTES_AUTO_APROV_MATERIALES");
        VisualizarfuentesAutorizadas();
    }
    private void EliminarCoordenadasfuentesAutorizadas(string efoid)
    {
        CargarCoordenadasfuentesAutorizadas(efoid);
        int i = 0;
        DataSet dsTemp = dsDatos;
        for (i = dsTemp.Tables[0].Rows.Count - 1; i >= 0; i--)
        {
            dsDatos.Tables[0].Rows[i].Delete();
        }
        Contexto.guardarTabla(dsDatos, "EIH_COOR_FUENT_AUTO_APROV");
    }

    protected void btnAgregarfuentesAutorizadas_Click(object sender, EventArgs e)
    {
        DataTable dt = this.ctrUbicacionPoliConst.Coordenadas;
        if (dt != null)
        {
            if (dt.Rows.Count == 5)
            {
                GuardarfuentesAutorizadas();
                this.ctrUbicacionPoliConst.LimpiarObjetos();
                limpiarPlaceHolder(plhFuentesAut);
            }
            else
                Mensaje.MostrarMensaje(this.Page, "Debe Ingresar 5 Coordenadas");
        }
        else
            Mensaje.MostrarMensaje(this.Page, "No se han agregado Coordenadas");
    }
    protected void btnCancelarfuentesAutorizadas_Click(object sender, EventArgs e)
    {
        this.plhFuentesAut.Visible = false;
        limpiarPlaceHolder(plhFuentesAut);

        DataTable dt = this.ctrUbicacionPoliConst.Coordenadas;
        if (dt != null)
        {
            this.ctrUbicacionPoliOcu.LimpiarObjetos();
        }

    }

    protected void grvfuentesAutorizadas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarfuentesAutorizadas(e.RowIndex);

    }

    protected void btnAgrefarInfoFuentesAutorizadas_Click(object sender, EventArgs e)
    {
        plhFuentesAut.Visible = true;
    }
    #endregion

    #region 4.4.2 materiales de construccion


    private void cargarcboMatConTip()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EFM_ACTIVO", SqlDbType.Bit, 1, "EFM_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIB_TIPO_FUENTE_MATERIALES");
        cboMatConTip.DataSource = ds.Tables[0];
        cboMatConTip.DataTextField = "EFM_TIPO_FUENTE_NOMBRE";
        cboMatConTip.DataValueField = "EFM_ID";
        cboMatConTip.DataBind();
        this.cboMatConTip.Items.Insert(0, new ListItem("Seleccione..", "-1"));

    }

    private void CargarCboUnidadArea()
    {

        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EUA_ACTIVO", SqlDbType.Bit, 1, "EUA_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "eib_unidad_Area");
        cboUnidadArea.DataSource = ds.Tables[0];
        cboUnidadArea.DataTextField = "EUA_UNIDAD_AREA";
        cboUnidadArea.DataValueField = "EUA_ID";
        cboUnidadArea.DataBind();
        this.cboUnidadArea.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    private void VisualizarGrvMaterialesConstruccion()
    {
        CargarMaterialesConstruccion();
        this.grvMaterialesConstruccion.DataSource = dsDatos;
        this.grvMaterialesConstruccion.DataBind();
    }

    private void CargarMaterialesConstruccion()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_MATERIALES_CONSTRUCCION");

    }
    private void GuardarMaterialesConstruccion()
    {
        CargarMaterialesConstruccion();
        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EMC_ID"] =
        dr["EUA_ID"] = cboUnidadArea.SelectedValue;
        dr["EIP_ID"] = IDProyecto;
        dr["EMC_FECHA_CREACION"] = Fecha;
        dr["EMC_DISP_TITULO_MINERO"] = chkDisponibleTitulo.Checked;
        dr["EFM_ID"] = cboMatConTip.SelectedValue;
        dr["EMC_VOLUMEN"] = txtMatConVol.Text;
        dr["EMC_AREA_INTERVENIR"] = txtAreaInterven.Text;
        dsDatos.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsDatos, "EIH_MATERIALES_CONSTRUCCION");
        CargarMaterialesConstruccion();
        GuardarCoordenadasMaterialesConstruccion(dsDatos.Tables[0].Rows[dsDatos.Tables[0].Rows.Count - 1]["EMC_ID"].ToString());
        VisualizarGrvMaterialesConstruccion();
    }

    private void CargarCoordenadasMaterialesConstruccion(string efoId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EMC_ID", SqlDbType.Int, 21, "EMC_ID");
        par.Value = efoId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_COOR_MAT_CONSTRUC");
    }
    private void GuardarCoordenadasMaterialesConstruccion(string efoId)
    {
        CargarCoordenadasMaterialesConstruccion(efoId);
        foreach (DataRow row in this.ctrUbicacionPoliConstr.Coordenadas.Rows)
        {
            DataRow dr = dsDatos.Tables[0].NewRow();
            dr["EMC_ID"] = efoId;
            dr["EC_COOR_NORTE"] = row["COORNORTE"].ToString();
            dr["EC_COOR_ESTE"] = row["COORESTE"].ToString();
            dsDatos.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsDatos, "EIH_COOR_MAT_CONSTRUC");
        this.cboUnidadArea.SelectedValue = "-1";
        this.chkDisponibleTitulo.Checked = false;
        this.cboMatConTip.SelectedValue = "-1";
        this.txtMatConVol.Text = "";
        this.txtAreaInterven.Text = "";
        DataTable dt = this.ctrUbicacionPoliOcu.Coordenadas;
        if (dt != null)
        {
            this.ctrUbicacionPoliOcu.LimpiarObjetos();
        }
    }
    protected void btnAgregarMaterialesConstruccion_Click(object sender, EventArgs e)
    {
        DataTable dt = this.ctrUbicacionPoliConstr.Coordenadas;
        if (dt != null)
        {
            if (dt.Rows.Count == 5)
            {
                GuardarMaterialesConstruccion();
                this.ctrUbicacionPoliConstr.LimpiarObjetos();
                limpiarPlaceHolder(plhOcupacionFuentes);
            }
            else
                Mensaje.MostrarMensaje(this.Page, "Debe Ingresar 5 Coordenadas");
        }
        else
            Mensaje.MostrarMensaje(this.Page, "No se han agregado Coordenadas");
    }
    protected void btnMaterialesConstruccion_Click(object sender, EventArgs e)
    {
        this.plhMaterialesConstruccion.Visible = true;
    }
    protected void btnCancelarMaterialesConstruccion_Click(object sender, EventArgs e)
    {
        this.plhMaterialesConstruccion.Visible = false;
        limpiarPlaceHolder(plhMaterialesConstruccion);

        DataTable dt = this.ctrUbicacionPoliOcu.Coordenadas;
        if (dt != null)
        {
            this.ctrUbicacionPoliOcu.LimpiarObjetos();
        }
        this.cboUnidadArea.SelectedValue = "-1";
        this.chkDisponibleTitulo.Checked = false;
        this.cboMatConTip.SelectedValue = "-1";
        this.txtMatConVol.Text = "";
        this.txtAreaInterven.Text = "";

    }
    private void EliminarMaterialesConstruccion(int index)
    {
        CargarMaterialesConstruccion();
        EliminarCoordenadasMaterialesConstruccion(dsDatos.Tables[0].Rows[index]["EMC_ID"].ToString());
        CargarMaterialesConstruccion();
        dsDatos.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsDatos, "EIH_MATERIALES_CONSTRUCCION");
        VisualizarGrvMaterialesConstruccion();
    }
    private void EliminarCoordenadasMaterialesConstruccion(string efoid)
    {
        CargarCoordenadasMaterialesConstruccion(efoid);
        int i = 0;
        DataSet dsTemp = dsDatos;
        for (i = dsTemp.Tables[0].Rows.Count - 1; i >= 0; i--)
        {
            dsDatos.Tables[0].Rows[i].Delete();
        }
        Contexto.guardarTabla(dsDatos, "EIH_COOR_MAT_CONSTRUC");
    }
    protected void grvMaterialesConstruccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarMaterialesConstruccion(e.RowIndex);
    }

    #endregion

    #region 4.5.1 Coberturas Vegetales a Aprovechar

    protected void btnAgregarCobertVegetales_Click(object sender, EventArgs e)
    {
        if (!this.ctrPoligonos1.Validar())
            return;

        GuardarCoberturasVegetales();
        this.ctrPoligonos1.Limpiar();
        limpiarPlaceHolder(plhCobertVegetales);

    }

    private void CargarCoberturasVegetales()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_COBERTURA_VEG_APROVECHADA");

    }

    private void VisualizarCoberturasVegetales()
    {
        CargarCoberturasVegetales();
        this.grvCobertVegetales.DataSource = dsDatos;
        this.grvCobertVegetales.DataBind();
    }


    private void GuardarCoberturasVegetales()
    {
        CargarCoberturasVegetales();
        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EMC_ID"] =
        dr["EIP_ID"] = IDProyecto;
        dr["EVA_FECHA_CREACION"] = Fecha;
        dr["EVA_TIPO_UNIDAD"] = this.txtTipoCobertura.Text;
        dr["EVA_AREA_INTERV"] = this.txtAreaInterv.Text;
        dsDatos.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsDatos, "EIH_COBERTURA_VEG_APROVECHADA");
        CargarCoberturasVegetales();
        GuardarPoligonosCoberturaVegetales(dsDatos.Tables[0].Rows[dsDatos.Tables[0].Rows.Count - 1]["EVA_ID"].ToString());
        VisualizarCoberturasVegetales();
    }



    private void GuardarPoligonosCoberturaVegetales(string evaId)
    {
        DataTable dt = this.ctrPoligonos1.Coordenadas;

        DataTable dtPolig = new DataTable();
        dtPolig.Columns.Add("NumPoligono");
        foreach (DataRow row in dt.Rows)
        {
            if (dtPolig.Select("NumPoligono=" + row["NumPoligono"].ToString()).Length == 0)
            {
                DataRow row2 = dtPolig.NewRow();
                row2["NumPoligono"] = row["NumPoligono"].ToString();
                dtPolig.Rows.Add(row2);

                CargarPoligonosCoberturaVegetales(evaId);
                DataRow dr = dsDatos.Tables[0].NewRow();
                dr["EVA_ID"] = evaId;
                dr["EDV_INFRAC_PROY"] = row["DescPoligono"];
                dsDatos.Tables[0].Rows.Add(dr);
                Contexto.guardarTabla(dsDatos, "EIH_DET_COBERTURA_VEG_APROV");
                CargarPoligonosCoberturaVegetales(evaId);
                GuardarCoordenadasCoberturasVegetales(dsDatos.Tables[0].Rows[dsDatos.Tables[0].Rows.Count - 1]["EDV_ID"].ToString(), row["NumPoligono"].ToString());
            }
        }
    }

    private void CargarPoligonosCoberturaVegetales(string evaId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EVA_ID", SqlDbType.Int, 21, "EVA_ID");
        par.Value = evaId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_DET_COBERTURA_VEG_APROV");
    }

    private void GuardarCoordenadasCoberturasVegetales(string edvId, string numPolig)
    {
        CargarCoordenadasCoberturasVegetales(edvId);
        foreach (DataRow row in this.ctrPoligonos1.Coordenadas.Select("NumPoligono=" + numPolig))
        {
            DataRow dr = dsDatos.Tables[0].NewRow();
            dr["EDV_ID"] = edvId;
            dr["ECC_COOR_NORTE"] = row["COORNORTE"];
            dr["ECC_COOR_ESTE"] = row["COORESTE"];
            dsDatos.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsDatos, "EIH_COOR_DET_COBER");
    }

    private void CargarCoordenadasCoberturasVegetales(string edvId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EDV_ID", SqlDbType.Int, 21, "EDV_ID");
        par.Value = edvId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_COOR_DET_COBER");
    }

    protected void grvCobertVegetales_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarCoberturasVegetales(e.RowIndex);
    }

    private void EliminarCoberturasVegetales(int index)
    {
        Label lblevaId = (Label)this.grvCobertVegetales.Rows[index].FindControl("lblEvaId");
        string evaId = lblevaId.Text;
        CargarPoligonosCoberturaVegetales(evaId);
        DataSet dsPoligonos = dsDatos;
        int i = 0;
        foreach (DataRow dr in dsPoligonos.Tables[0].Rows)
        {
            string edvId = dr["EDV_ID"].ToString();
            CargarCoordenadasCoberturasVegetales(edvId);
            DataSet dsTempCoor = dsDatos;
            for (i = dsTempCoor.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                dsDatos.Tables[0].Rows[i].Delete();
            }
            Contexto.guardarTabla(dsDatos, "EIH_COOR_DET_COBER");
        }
        CargarPoligonosCoberturaVegetales(evaId);
        for (i = dsPoligonos.Tables[0].Rows.Count - 1; i >= 0; i--)
        {
            dsDatos.Tables[0].Rows[i].Delete();
        }
        Contexto.guardarTabla(dsDatos, "EIH_DET_COBERTURA_VEG_APROV");
        CargarCoberturasVegetales();
        dsDatos.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsDatos, "EIH_COBERTURA_VEG_APROVECHADA");
        VisualizarCoberturasVegetales();

    }

    protected void btnCancelarCobertVegetales_Click(object sender, EventArgs e)
    {
        this.ctrPoligonos1.Limpiar();
        limpiarPlaceHolder(plhCobertVegetales);
        this.plhCobertVegetales.Visible = false;
    }

    protected void btnCoberVeget_Click(object sender, EventArgs e)
    {
        this.plhCobertVegetales.Visible = true;
    }
    #endregion


    #region 4.5.2 Aprovechamiento a Realizar


    protected void btnAgregarAprobRealicar_Click(object sender, EventArgs e)
    {
        GuardarAprovechamientoRealizar();

        limpiarPlaceHolder(plhAprobRealicar);

    }

    private void GuardarAprovechamientoRealizar()
    {
        CargarAprovechamientoRealizar();
        DataRow dr = dsDatos.Tables[0].NewRow();

        dr["EIP_ID"] = IDProyecto;
        dr["EVC_FECHA_CREACION"] = Fecha;
        dr["EVC_TIPO_MUESTREO"] = this.txtTipoMuestreo.Text;
        dr["EVC_NO_PARCELAS"] = this.txtParcelasUnid.Text;
        dr["EVC_TAREA_MUESTREADA"] = this.txtTotalMuestra.Text;
        dr["EVC_ERROR_MUESTREO"] = this.txtErrorMuestreoAprov.Text;
        dr["EVC_PROB_ERROR"] = this.txtProbError.Text;
        dr["EVC_VOL_APROVECHAR"] = this.txtVolTotApro.Text;
        dr["EVC_VOL_COMER_APROVECHAR"] = this.txtVolComApro.Text;

        dsDatos.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsDatos, "EIH_VOL_COBERT_VEG_APROVECHAR");

        CargarAprovechamientoRealizar();

    }

    private void CargarAprovechamientoRealizar()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_VOL_COBERT_VEG_APROVECHAR");

        this.grvAprobRealicar.DataSource = dsDatos;
        this.grvAprobRealicar.DataBind();

    }

    protected void grvAprobRealicar_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarAprovechamientoRealizar(e.RowIndex);
    }

    private void EliminarAprovechamientoRealizar(int index)
    {
        CargarAprovechamientoRealizar();

        dsDatos.Tables[0].Rows[index].Delete();

        Contexto.guardarTabla(dsDatos, "EIH_VOL_COBERT_VEG_APROVECHAR");

        CargarAprovechamientoRealizar();

    }


    protected void btnCancelarAprobRealicar_Click(object sender, EventArgs e)
    {
        this.plhAprobRealicar.Visible = false;
        limpiarPlaceHolder(plhAprobRealicar);
    }
    protected void btnAprobRealizar_Click(object sender, EventArgs e)
    {
        this.plhAprobRealicar.Visible = true;
    }

    #endregion

    #region 4.5.3 Aprovechamiento Forestal Asociado a Obras Complementarias

    protected void btnAgregarAprovForest_Click(object sender, EventArgs e)
    {
        if (this.ctrUbicacionPoliAprovForest.Validar(1, 5))
        {
            GuardarAprovechamientoForestal();
            this.ctrUbicacionPoliAprovForest.LimpiarObjetos();
            limpiarPlaceHolder(plhAprovForest);
            VisualizarAprovechamientoForestal();
        }
    }

    private void GuardarAprovechamientoForestal()
    {
        CargarAprovechamientoForestal();
        DataRow dr = dsDatos.Tables[0].NewRow();

        dr["EIP_ID"] = IDProyecto;
        dr["EFO_FECHA_CREACION"] = Fecha;
        dr["EFO_TIPO_OBRA"] = this.txtTipoObra.Text;
        dr["EFO_PREDIO"] = this.txtPredio.Text;
        dr["EFO_AREA"] = this.txtAreaAprovForest.Text;
        dr["EFO_VOLUMEN_TOTAL"] = this.txtVolumenTotAprovForest.Text;
        dr["EFO_VOLUMEN_COMERCIAL"] = this.txtVolCoberAprovForest.Text;
        dr["EFO_UNIDAD_COBERTURA"] = this.txtUnidadCobertura.Text;

        dsDatos.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsDatos, "EIH_APROV_FOREST_ASOC_OBRAS_COMP");

        CargarAprovechamientoForestal();

        GuardarCoordenadasAprovechamientoForestal(dsDatos.Tables[0].Rows[dsDatos.Tables[0].Rows.Count - 1]["EFO_ID"].ToString());

    }

    private void GuardarCoordenadasAprovechamientoForestal(string efoId)
    {
        ConsultarCoordenadasAprovechamientoForestal(efoId);
        foreach (DataRow row in this.ctrUbicacionPoliAprovForest.Coordenadas.Rows)
        {
            DataRow dr = dsDatos.Tables[0].NewRow();
            dr["EFO_ID"] = efoId;
            dr["ECA_COOR_NORTE"] = row["COORNORTE"].ToString();
            dr["ECA_COOR_ESTE"] = row["COORESTE"].ToString();
            dsDatos.Tables[0].Rows.Add(dr);
        }

        Contexto.guardarTabla(dsDatos, "EIH_COOR_APROV_FOREST");

    }

    private void ConsultarCoordenadasAprovechamientoForestal(string efoId)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EFO_ID", SqlDbType.Int, 21, "EFO_ID");
        par.Value = int.Parse(efoId);
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_COOR_APROV_FOREST");

    }

    private void CargarAprovechamientoForestal()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_APROV_FOREST_ASOC_OBRAS_COMP");

    }

    private void VisualizarAprovechamientoForestal()
    {
        CargarAprovechamientoForestal();
        this.grvAprovForest.DataSource = dsDatos;
        this.grvAprovForest.DataBind();
    }

    protected void grvAprovForest_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarAprovechamientoForestal(e.RowIndex);
    }

    private void EliminarAprovechamientoForestal(int index)
    {
        Label lblefoId = (Label)this.grvAprovForest.Rows[index].FindControl("lblefoId");
        ConsultarCoordenadasAprovechamientoForestal(lblefoId.Text);

        DataTable tmp = dsDatos.Tables[0];
        int i = 0;
        for (i = tmp.Rows.Count - 1; i >= 0; i--)
        {
            dsDatos.Tables[0].Rows[i].Delete();
        }

        Contexto.guardarTabla(dsDatos, "EIH_COOR_APROV_FOREST");

        CargarAprovechamientoForestal();

        dsDatos.Tables[0].Rows[index].Delete();

        Contexto.guardarTabla(dsDatos, "EIH_APROV_FOREST_ASOC_OBRAS_COMP");

        VisualizarAprovechamientoForestal();

    }

    protected void btnCancelarAprovForest_Click(object sender, EventArgs e)
    {
        this.ctrUbicacionPoliAprovForest.LimpiarObjetos();
        limpiarPlaceHolder(plhAprovForest);
        this.plhAprovForest.Visible = false;
    }

    protected void btnAprovForet_Click(object sender, EventArgs e)
    {
        this.plhAprovForest.Visible = true;
    }


    protected void cboAplica_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboAplica.SelectedValue == "1")
        {
            this.ctrUbicacionPoliAprovForest.LimpiarObjetos();
            limpiarPlaceHolder(plhAprovForest);
            this.plhAprovForest.Visible = false;
            this.btnAprovForet.Visible = false;
            this.grvAprovForest.Visible = false;
        }
        else if (this.cboAplica.SelectedValue == "0")
        {
            this.btnAprovForet.Visible = true;
            this.grvAprovForest.Visible = true;
        }
        else
        {
            this.ctrUbicacionPoliAprovForest.LimpiarObjetos();
            limpiarPlaceHolder(plhAprovForest);
            this.plhAprovForest.Visible = false;
            this.btnAprovForet.Visible = false;
            this.grvAprovForest.Visible = false;
        }

    }

    #endregion

    #region 4.6.1 Emisiones

    private void CargarCboTipoFuente()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ETF_ACTIVO", SqlDbType.Bit, 1, "ETF_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIB_TIPO_FUENTE_EMISIONES");
        cboTipoFuente.DataSource = ds.Tables[0];
        cboTipoFuente.DataTextField = "ETF_TIPO_FUENTE_EMISIONES";
        cboTipoFuente.DataValueField = "ETF_ID";
        cboTipoFuente.DataBind();
        this.cboTipoFuente.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    private void CargarCboTipoDucto()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ETD_ACTIVO", SqlDbType.Bit, 1, "ETD_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIB_TIPO_DUCTO");
        cboTipoDucto.DataSource = ds.Tables[0];
        cboTipoDucto.DataTextField = "ETD_NOMBRE_DUCTO";
        cboTipoDucto.DataValueField = "ETD_ID";
        cboTipoDucto.DataBind();
        this.cboTipoDucto.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    protected void btnAgregarEmisiones_Click(object sender, EventArgs e)
    {
        GuardarEmisiones();
    }
    private void GuardarEmisiones()
    {
        CargarEmisiones();

        DataRow row = dsDatos.Tables[0].NewRow();

        row["EIP_ID"] = IDProyecto;
        row["EEA_FECHA_CREACION"] = Fecha;
        row["ETF_ID"] = this.cboTipoFuente.SelectedValue;
        row["EEA_DESC_PTO_DESC_PREV"] = this.txtDescPuntoDesc.Text;
        row["EEA_EMISION_ESTIMADA"] = this.txtEmisionEstimada.Text;
        row["EEA_MEC_CONTROL_PART"] = this.txtMecParticulas.Text;
        row["EEA_MEC_CONTROL_GASV"] = this.txtMecControlGas.Text;
        row["ETD_ID"] = this.cboTipoDucto.SelectedValue;

        dsDatos.Tables[0].Rows.Add(row);

        Contexto.guardarTabla(dsDatos, "EIH_EMISIONES_ATMOSFERICAS");

        limpiarPlaceHolder(plhEmisiones);

        VisualizarEmisiones();

    }

    private void VisualizarEmisiones()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIV_EMISIONES_ATMOSFERICAS");

        this.grvEmisiones.DataSource = dsDatos;
        this.grvEmisiones.DataBind();
    }
    private void CargarEmisiones()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_EMISIONES_ATMOSFERICAS");
    }

    protected void grvEmisiones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarEmisiones(e.RowIndex);
    }

    private void EliminarEmisiones(int index)
    {
        CargarEmisiones();

        dsDatos.Tables[0].Rows[index].Delete();

        Contexto.guardarTabla(dsDatos, "EIH_EMISIONES_ATMOSFERICAS");

        VisualizarEmisiones();
    }

    protected void btnCancelarEmisiones_Click(object sender, EventArgs e)
    {
        this.plhEmisiones.Visible = false;
        limpiarPlaceHolder(plhEmisiones);
    }
    protected void btnEmisiones_Click(object sender, EventArgs e)
    {
        this.plhEmisiones.Visible = true;
    }

    #endregion

    #region 4.6.2 Ruido

    protected void btnRuido_Click(object sender, EventArgs e)
    {
        this.plhRuido.Visible = true;
    }

    private void GuardarRuido()
    {
        CargarRuido();
        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EER_FECHA_CREACION"] = Fecha;
        dr["EER_SITIOS_EMI_RUIDO"] = this.txtEmisionRuido.Text;
        dr["EER_NIV_PRES_RUIDO"] = this.txtNivelesPresion.Text;

        dsDatos.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsDatos, "EIH_EMISIONES_RUIDO");

        CargarRuido();

    }

    private void CargarRuido()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_EMISIONES_RUIDO");

        this.grvRuido.DataSource = dsDatos;
        this.grvRuido.DataBind();
    }

    protected void grvRuido_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarRuido(e.RowIndex);
    }
    private void EliminarRuido(int index)
    {
        CargarRuido();

        dsDatos.Tables[0].Rows[index].Delete();

        Contexto.guardarTabla(dsDatos, "EIH_EMISIONES_RUIDO");

        CargarRuido();
    }

    protected void btnCancelarRuido_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhRuido);
        this.plhRuido.Visible = false;
    }
    protected void btnAgregarRuido_Click(object sender, EventArgs e)
    {
        GuardarRuido();
        limpiarPlaceHolder(plhRuido);
    }

    #endregion


    #region 4.7.1 Residuos no Peligrosos

    private void CargarCboQuienManeja()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EQM_ACTIVO", SqlDbType.Bit, 1, "EQM_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIB_QUIEN_MANEJA");

        cboQuienManejaNo.DataSource = ds.Tables[0];
        cboQuienManejaNo.DataTextField = "EQM_QUIEN_MANEJA";
        cboQuienManejaNo.DataValueField = "EQM_ID";
        cboQuienManejaNo.DataBind();
        this.cboQuienManejaNo.Items.Insert(0, new ListItem("Seleccione..", "-1"));


        cboQuienManeja.DataSource = ds.Tables[0];
        cboQuienManeja.DataTextField = "EQM_QUIEN_MANEJA";
        cboQuienManeja.DataValueField = "EQM_ID";
        cboQuienManeja.DataBind();
        this.cboQuienManeja.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    private void CargarCboTipoAprovechamientoNoPel()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EAR_ACTIVO", SqlDbType.Bit, 1, "EAR_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@EAR_PELIGROSOS", SqlDbType.Bit, 1, "EAR_PELIGROSOS");
        par2.Value = 0;
        parametros.Add(par2);
        Contexto.cargarTabla(parametros, ds, "EIB_TIPO_APROVEC_RESIDUOS_SOLIDOS");

        cboTipoAprovPreNo.DataSource = ds.Tables[0];
        cboTipoAprovPreNo.DataTextField = "EAR_TIPO_APROVEC_RESIDUOS";
        cboTipoAprovPreNo.DataValueField = "EAR_ID";
        cboTipoAprovPreNo.DataBind();
        this.cboTipoAprovPreNo.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    private void CargarCboTipoTratamientoNoPel()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ETR_ACTIVO", SqlDbType.Bit, 1, "ETR_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@ETR_PELIGROSOS", SqlDbType.Bit, 1, "ETR_PELIGROSOS");
        par2.Value = 0;
        parametros.Add(par2);
        Contexto.cargarTabla(parametros, ds, "EIB_TIPO_TRATAT_RESIDUOS_SOLIDOS");

        cboTipoTraPrevNo.DataSource = ds.Tables[0];
        cboTipoTraPrevNo.DataTextField = "ETR_TIPO_TRATAM_RESIDUOS";
        cboTipoTraPrevNo.DataValueField = "ETR_ID";
        cboTipoTraPrevNo.DataBind();
        this.cboTipoTraPrevNo.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }


    private void CargarCboTipoDispNoPel()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EDR_ACTIVO", SqlDbType.Bit, 1, "EDR_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@EDR_PELIGROSOS", SqlDbType.Bit, 1, "EDR_PELIGROSOS");
        par2.Value = 0;
        parametros.Add(par2);
        Contexto.cargarTabla(parametros, ds, "EIB_TIPO_DISP_RESIDUOS_SOLIDOS");

        cboTipoDespPrevNo.DataSource = ds.Tables[0];
        cboTipoDespPrevNo.DataTextField = "EDR_TIPO_DIPS_RESIDUOS";
        cboTipoDespPrevNo.DataValueField = "EDR_ID";
        cboTipoDespPrevNo.DataBind();
        this.cboTipoDespPrevNo.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    protected void cboTipoAprovPreNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoAprovPreNo.SelectedItem.Text.Contains("Otro"))
            this.trTipoTraNo.Visible = true;
        else
        {
            this.trTipoTraNo.Visible = false;
            this.txtTipoAprovPreNoOtro.Text = "";
        }

    }

    protected void cboTipoTraPrevNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoTraPrevNo.SelectedItem.Text.Contains("Otro"))
            this.trTraPreNo.Visible = true;
        else
        {
            this.trTraPreNo.Visible = false;
            this.txtTipoTraPrevNoOtro.Text = "";
        }

    }

    protected void cboTipoDespPrevNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoDespPrevNo.SelectedItem.Text.Contains("Otro"))
            this.trDespPrevNo.Visible = true;
        else
        {
            this.trDespPrevNo.Visible = false;
            this.txtTipoDespPrevNoOtro.Text = "";
        }
    }

    protected void btnAgreagarResiduosnoPeligrosos_Click(object sender, EventArgs e)
    {
        GuardarResiduosNoPeligrosos();
    }

    private void GuardarResiduosNoPeligrosos()
    {
        CargarResiduosNoPeligrosos();
        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EMN_FECHA_CREACION"] = Fecha;
        dr["EMN_TIPO_RESIDUOS"] = this.txtTipoResiduos.Text;
        dr["EMN_SITIO_ALMACENAMIENTO"] = this.txtSitioAlmacenNo.Text;
        dr["EQM_ID"] = this.cboQuienManejaNo.SelectedValue;
        dr["EAR_ID"] = this.cboTipoAprovPreNo.SelectedValue;
        dr["EMN_OTRO_EAR"] = this.txtTipoAprovPreNoOtro.Text;
        dr["ETR_ID"] = this.cboTipoTraPrevNo.SelectedValue;
        dr["EMN_OTRO_ETR"] = this.txtTipoTraPrevNoOtro.Text;
        dr["EDR_ID"] = this.cboTipoDespPrevNo.SelectedValue;
        dr["EMN_OTRO_EDR"] = this.txtTipoDespPrevNoOtro.Text;

        dsDatos.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsDatos, "EIH_MANEJO_RES_SOLIDOS_NO_PEL");

        limpiarPlaceHolder(plhResiduosnoPeligrosos);
        this.txtTipoAprovPreNoOtro.Text = "";
        this.txtTipoTraPrevNoOtro.Text = "";
        this.txtTipoDespPrevNoOtro.Text = "";
        this.trTipoTraNo.Visible = false;
        this.trTraPreNo.Visible = false;
        this.trDespPrevNo.Visible = false;
        VisualizarResiduosNoPeligrosos();

    }

    private void VisualizarResiduosNoPeligrosos()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIV_MANEJO_RES_SOLIDOS_NO_PEL");

        this.grvResiduosnoPeligrosos.DataSource = dsDatos;
        this.grvResiduosnoPeligrosos.DataBind();
    }

    private void CargarResiduosNoPeligrosos()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_MANEJO_RES_SOLIDOS_NO_PEL");

    }

    protected void grvResiduosnoPeligrosos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarResiduosNoPeligrosos(e.RowIndex);
    }

    private void EliminarResiduosNoPeligrosos(int index)
    {
        CargarResiduosNoPeligrosos();

        dsDatos.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsDatos, "EIH_MANEJO_RES_SOLIDOS_NO_PEL");

        VisualizarResiduosNoPeligrosos();
    }

    protected void btnCancelarResiduosnoPeligrosos_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhResiduosnoPeligrosos);
        this.plhResiduosnoPeligrosos.Visible = false;
        this.txtTipoAprovPreNoOtro.Text = "";
        this.txtTipoTraPrevNoOtro.Text = "";
        this.txtTipoDespPrevNoOtro.Text = "";
        this.trTipoTraNo.Visible = false;
        this.trTraPreNo.Visible = false;
        this.trDespPrevNo.Visible = false;
    }

    protected void btnResiduosnoPeligrosos_Click(object sender, EventArgs e)
    {
        this.plhResiduosnoPeligrosos.Visible = true;
    }

    #endregion

    #region 4.7.2 Residuos Peligrosos

    private void CargarCboTipoAprovechamientoPel()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EAR_ACTIVO", SqlDbType.Bit, 1, "EAR_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@EAR_PELIGROSOS", SqlDbType.Bit, 1, "EAR_PELIGROSOS");
        par2.Value = 1;
        parametros.Add(par2);
        Contexto.cargarTabla(parametros, ds, "EIB_TIPO_APROVEC_RESIDUOS_SOLIDOS");

        cboTipoAprovPre.DataSource = ds.Tables[0];
        cboTipoAprovPre.DataTextField = "EAR_TIPO_APROVEC_RESIDUOS";
        cboTipoAprovPre.DataValueField = "EAR_ID";
        cboTipoAprovPre.DataBind();
        this.cboTipoAprovPre.Items.Insert(0, new ListItem("Seleccione..", "-1"));

    }

    private void CargarCboTipoTratamientoPel()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ETR_ACTIVO", SqlDbType.Bit, 1, "ETR_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@ETR_PELIGROSOS", SqlDbType.Bit, 1, "ETR_PELIGROSOS");
        par2.Value = 1;
        parametros.Add(par2);
        Contexto.cargarTabla(parametros, ds, "EIB_TIPO_TRATAT_RESIDUOS_SOLIDOS");

        cboTipoTraPre.DataSource = ds.Tables[0];
        cboTipoTraPre.DataTextField = "ETR_TIPO_TRATAM_RESIDUOS";
        cboTipoTraPre.DataValueField = "ETR_ID";
        cboTipoTraPre.DataBind();
        this.cboTipoTraPre.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    private void CargarCboTipoDispPel()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EDR_ACTIVO", SqlDbType.Bit, 1, "EDR_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SqlParameter par2 = new SqlParameter("@EDR_PELIGROSOS", SqlDbType.Bit, 1, "EDR_PELIGROSOS");
        par2.Value = 1;
        parametros.Add(par2);
        Contexto.cargarTabla(parametros, ds, "EIB_TIPO_DISP_RESIDUOS_SOLIDOS");

        cboTipoDespPrev.DataSource = ds.Tables[0];
        cboTipoDespPrev.DataTextField = "EDR_TIPO_DIPS_RESIDUOS";
        cboTipoDespPrev.DataValueField = "EDR_ID";
        cboTipoDespPrev.DataBind();
        this.cboTipoDespPrev.Items.Insert(0, new ListItem("Seleccione..", "-1"));
    }

    protected void cboTipoAprovPre_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoAprovPre.SelectedItem.Text.Contains("Otro"))
            this.trTipoApro.Visible = true;
        else
        {
            this.trTipoApro.Visible = false;
            this.txtTipoAprovPreNo.Text = "";
        }

    }

    protected void cboTipoTraPre_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoTraPre.SelectedItem.Text.Contains("Otro"))
            this.trTipoTra.Visible = true;
        else
        {
            this.trTipoTra.Visible = false;
            this.txtTipoTraPrevOtro.Text = "";
        }
    }

    protected void cboTipoDespPrev_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoDespPrev.SelectedItem.Text.Contains("Otro"))
            this.trTipoDisp.Visible = true;
        else
        {
            this.trTipoDisp.Visible = false;
            this.txtTipoDespPrevOtro.Text = "";
        }
    }

    protected void btnAgregarResiduosPeligrosos_Click(object sender, EventArgs e)
    {
        CargarResiduosPeligrosos();
        DataRow dr = dsDatos.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EMP_FECHA_CREACION"] = Fecha;
        dr["EMP_TIPO_RESIDUOS"] = this.txtTipoResiduos2.Text;
        dr["EMP_SITIO_ALMACENAMIENTO"] = this.txtSitioAlmacen.Text;
        dr["EQM_ID"] = this.cboQuienManeja.SelectedValue;
        dr["EAR_ID"] = this.cboTipoAprovPre.SelectedValue;
        dr["EMP_OTRO_EAR"] = this.txtTipoAprovPreNo.Text;
        dr["ETR_ID"] = this.cboTipoTraPre.SelectedValue;
        dr["EMP_OTRO_ETR"] = this.txtTipoTraPrevOtro.Text;
        dr["EDR_ID"] = this.cboTipoDespPrev.SelectedValue;
        dr["EMP_OTRO_EDR"] = this.txtTipoDespPrevOtro.Text;

        dsDatos.Tables[0].Rows.Add(dr);

        Contexto.guardarTabla(dsDatos, "EIH_MANEJO_RES_SOLIDOS_PEL");

        limpiarPlaceHolder(plhResiduosPeligrosos);
        this.txtTipoAprovPreNo.Text = "";
        this.txtTipoTraPrevOtro.Text = "";
        this.txtTipoDespPrevOtro.Text = "";
        this.trTipoApro.Visible = false;
        this.trTipoTra.Visible = false;
        this.trTipoDisp.Visible = false;
        VisualizarResiduosPeligrosos();
    }

    private void VisualizarResiduosPeligrosos()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIV_MANEJO_RES_SOLIDOS_PEL");

        this.grvResiduosPeligrosos.DataSource = dsDatos;
        this.grvResiduosPeligrosos.DataBind();
    }

    private void CargarResiduosPeligrosos()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 21, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_MANEJO_RES_SOLIDOS_PEL");

    }

    protected void grvResiduosPeligrosos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarResiduosPeligrosos(e.RowIndex);
    }

    private void EliminarResiduosPeligrosos(int index)
    {
        CargarResiduosPeligrosos();

        dsDatos.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsDatos, "EIH_MANEJO_RES_SOLIDOS_PEL");

        VisualizarResiduosPeligrosos();
    }

    protected void btnCancelarResiduosPeligrosos_Click(object sender, EventArgs e)
    {
        limpiarPlaceHolder(plhResiduosPeligrosos);
        this.plhResiduosPeligrosos.Visible = false;
        this.txtTipoAprovPreNo.Text = "";
        this.txtTipoTraPrevOtro.Text = "";
        this.txtTipoDespPrevOtro.Text = "";
        this.trTipoApro.Visible = false;
        this.trTipoTra.Visible = false;
        this.trTipoDisp.Visible = false;
    }
    protected void btnResiduosPeligrosos_Click(object sender, EventArgs e)
    {
        this.plhResiduosPeligrosos.Visible = false;
    }

    #endregion

    


}

