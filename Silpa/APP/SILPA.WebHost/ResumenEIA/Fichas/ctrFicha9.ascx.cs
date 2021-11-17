using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Persistencia;
using System.Data;
using System.Data.SqlClient;

public partial class ResumenEIA_Fichas_crtFicha9 : System.Web.UI.UserControl
{

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

    DataSet dsPlanManejo = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    public void CargarTodaInfo()
    {
        cargarGrillas();
    }

    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        if (!this.phlInsercion.Visible)
        { this.phlInsercion.Visible = true; }
        else
        { this.phlInsercion.Visible = false; }
        cargarGrillaPlanManejo();
    }

    private void cargarGrillas()
    {
        cargarGrillaPlanManejo();
    }

    private void agregarRegistrogrillaPlanManejo()
    {
        DataRow dr = dsPlanManejo.Tables[0].NewRow();
        dr["EIP_ID"] = IDProyecto;
        dr["EPC_PRINC_RIESG_EST"] = this.txtPrinRiesgos.Text;
        dr["EPC_AREAS_MAYOR_RIESG"] = this.txtAreasRiesgos.Text;
        dr["EPC_MEDIDAS_MANEJO"] = this.txtMedidasRiesgos.Text;
        dr["EPC_PTOS_CONTROL"] = this.txtPuntosControl.Text;
        dr["EPC_SOPORTE_EXTERNO"] = this.txtSoporteExterno.Text;

        dsPlanManejo.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsPlanManejo, "EIH_PLANES_CONTINGENCIAS");

        this.txtPrinRiesgos.Text = "";
        this.txtAreasRiesgos.Text = "";
        this.txtMedidasRiesgos.Text = "";
        this.txtPuntosControl.Text = "";
        this.txtSoporteExterno.Text = "";
        cargarGrillaPlanManejo();

    }

    private void cargarGrillaPlanManejo()
    {
        dsPlanManejo = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIC_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsPlanManejo, "EIH_PLANES_CONTINGENCIAS");
        this.grvPlanManejo.DataSource = dsPlanManejo.Tables[0];
        this.grvPlanManejo.DataBind();

    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.phlInsercion.Visible = false;
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        cargarGrillas();
        agregarRegistrogrillaPlanManejo();
    }
    protected void grvPlanManejo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        cargarGrillas();
        eliminarRegistroPlanManejo(e.RowIndex);
    }

    private void eliminarRegistroPlanManejo(int index)
    {
        this.dsPlanManejo.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsPlanManejo, "EIH_PLANES_CONTINGENCIAS");
        cargarGrillaPlanManejo();
    }
    protected void grvPlanManejo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
