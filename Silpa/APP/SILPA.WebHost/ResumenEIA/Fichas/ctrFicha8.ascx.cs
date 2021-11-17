using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SoftManagement.Persistencia;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class ResumenEIA_Fichas_ctrFicha8 : System.Web.UI.UserControl
{

    #region OBJETOS PRIVADOS

    DataSet dsEtapaAplicacion;
    DataSet dsMedioAplicacion;
    DataSet dsProgramaSeguimiento;

    
    #endregion
    #region PROPIEDADES
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
    #endregion
    #region METODOS
    void cargarCombos()
    {
        if (!IsPostBack)
        {
            dsEtapaAplicacion = new DataSet();
            Contexto.cargarTabla(dsEtapaAplicacion, "EIB_ETAPA_APLICACION_PROYECTO");
            this.cboEtapaAplicacion.DataSource = dsEtapaAplicacion.Tables[0];
            this.cboEtapaAplicacion.DataValueField = "EEA_ID";
            this.cboEtapaAplicacion.DataTextField = "EEA_ETAPA_APLICACION_PROY";
            this.cboEtapaAplicacion.DataBind();

            dsMedioAplicacion = new DataSet();
            Contexto.cargarTabla(dsMedioAplicacion, "EIB_TIPO_MEDIO_PROG_SEGUIMIENTO");
            this.cboMedio.DataSource = dsMedioAplicacion.Tables[0];
            this.cboMedio.DataValueField = "ETM_ID";
            this.cboMedio.DataTextField = "ETM_TIPO_MEDIO";
            this.cboMedio.DataBind();
        }
    }
    private void EliminarProgramaSeguimiento(int p)
    {
        int idFila = 0;
        DataRow fila = this.dsProgramaSeguimiento.Tables[0].Rows[p];
        idFila = int.Parse(fila["EPS_ID"].ToString());

        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter parametro = new SqlParameter("@EPS_ID", SqlDbType.Int, 10, "EPS_ID");
        parametro.Value = idFila;
        parametros.Add(parametro);

        DataSet dsEliminar = new DataSet();
        Contexto.cargarTabla(parametros, dsEliminar, "EIH_PROGRAMA_SEGUIMIENTO");

        dsEliminar.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsEliminar, "EIH_PROGRAMA_SEGUIMIENTO");

    }
    private void cargarGrillas()
    {
        cargarProgramaSeguimiento();
    }
    private void cargarProgramaSeguimiento()
    {
        dsProgramaSeguimiento = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter parametro = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        parametro.Value = IDProyecto;
        parametros.Add(parametro);

        Contexto.cargarTabla(parametros, dsProgramaSeguimiento, "V_PROGRAMA_SEGUIMIENTO");
        this.grvProgramaSeguimiento.DataSource = dsProgramaSeguimiento.Tables[0];
        this.grvProgramaSeguimiento.DataBind();
    }
    private void agregarProgramaSeguimiento()
    {
        DataSet dsAgregarPrograma = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter parametro = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        parametro.Value = IDProyecto;
        parametros.Add(parametro);
        Contexto.cargarTabla(dsAgregarPrograma, "EIH_PROGRAMA_SEGUIMIENTO");
        DataRow fila = dsAgregarPrograma.Tables[0].NewRow();
        fila["EPS_ID"] = 33;
        fila["EIP_ID"] = IDProyecto ;
        fila["EEA_ID"] = cboEtapaAplicacion.SelectedValue;
        fila["ETM_ID"] = cboMedio.SelectedValue;
        fila["EPS_NOMBRE_PROG"] = txtNombre.Text;
        fila["EPS_PROGRAMA"] = txtPrograma.Text;
        fila["EPS_ELEMENTOS_SEGUIMIENTO"] = txtElementosSeguimiento.Text;
        fila["EPS_UBICACION"] = txtUbicacion.Text;
        fila["EPS_ACTIVIDAD"] = txtActividadARealizar.Text;
        fila["EPS_INDICADORES"] = txtIndicadoresSeguimiento.Text;
        dsAgregarPrograma.Tables[0].Rows.Add(fila);
        Contexto.guardarTabla(dsAgregarPrograma, "EIH_PROGRAMA_SEGUIMIENTO");
        limpiarFormulario();
        cargarGrillas();

    }
    private void limpiarFormulario()
    {
        this.txtActividadARealizar.Text = string.Empty;
        txtElementosSeguimiento.Text = string.Empty;
        txtIndicadoresSeguimiento.Text = string.Empty;
        txtNombre.Text = string.Empty;
        txtPrograma.Text = string.Empty;
        txtUbicacion.Text = string.Empty;
        cboEtapaAplicacion.SelectedIndex = 1;
        cboMedio.SelectedIndex = 1;
    }
    #endregion
    #region METODOS DELEGADOS
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    public void CargarTodaInfo()
    {
        cargarCombos();
        cargarProgramaSeguimiento();
    }
    protected void cboMedio_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.phlFormulario.Visible = false;
        this.lblMedio.Text = cboMedio.SelectedItem.Text;
    }
    protected void btnAgregarMedio_Click(object sender, EventArgs e)
    {
        this.phlFormulario.Visible = true;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.phlFormulario.Visible = false;
        limpiarFormulario();
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        cargarProgramaSeguimiento();
        agregarProgramaSeguimiento();
    }
    
    protected void grvProgramaSeguimiento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarProgramaSeguimiento(e.RowIndex);
        cargarProgramaSeguimiento();
    }
    #endregion

    protected void grvProgramaSeguimiento_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
