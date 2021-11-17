using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftManagement.Persistencia;

public partial class ResumenEIA_Fichas_ctrFicha11 : System.Web.UI.UserControl
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

    DataSet dsPlanInversion;
    DataSet dsProyectosInversion;

    private void cargarGrillas()
    {
        cargarPlanInversion();
        cargarGrillaProyectoInversion();
    }

    private void cargarPlanInversion()
    {
        dsPlanInversion = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsPlanInversion, "EIH_PLAN_INVERSION");
        if (dsPlanInversion.Tables[0].Rows.Count > 0)
        this.txtElementos.Text = !IsPostBack ? dsPlanInversion.Tables[0].Rows[0]["EPI_ELEM_COSTOS_EST_INV"].ToString() : this.txtElementos.Text;
        this.btnProyectoIns.Enabled  =dsPlanInversion.Tables[0].Rows.Count > 0 ? true : false;
 

    }

    private void cargarGrillaProyectoInversion()
    {
        if (dsPlanInversion.Tables[0].Rows.Count > 0)
        { 
            dsProyectosInversion = new DataSet();        
            List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
            SqlParameter ParametroCarga = new SqlParameter("@EPI_ID", SqlDbType.Int, 10, "EPI_ID");
            ParametroCarga.Value = (dsPlanInversion.Tables[0].Rows[0])["EPI_ID"];
            parametrosConsulta.Add(ParametroCarga);
            Contexto.cargarTabla(parametrosConsulta, dsProyectosInversion, "EIH_PROYECTOS_INVERSION");
            this.grvProyectos.DataSource = dsProyectosInversion.Tables[0];
            this.grvProyectos.DataBind();
        }

    }


    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    public void CargarTodaInfo()
    {
        this.plhInsersion.Visible = false;
        cargarGrillas();
    }

    protected void btnProyectoIns_Click(object sender, EventArgs e)
    {
        if (!this.plhInsersion.Visible)
        {
            this.plhInsersion.Visible = true;
        }
        else
        {
            this.plhInsersion.Visible = false;
        }

        cargarGrillas();

    }
    protected void btnInsertar_Click(object sender, EventArgs e)
    {
        this.plhInsersion.Visible = false;
        cargarGrillas();
        agregarRegistroGrillaProyectos();
        cargarGrillas();

    }

    private void agregarRegistroGrillaProyectos()
    {
        DataRow dr = this.dsProyectosInversion.Tables[0].NewRow();
        dr["EPI_ID"] = (dsPlanInversion.Tables[0].Rows[0])["EPI_ID"]; ;
        dr["EPO_NOMBRE_PROYECTO"]=this.txtNombreProyecto.Text  ;
        dr["EPO_VALOR_INVERSION"]=txtValorInversion .Text; 
        dr["EPO_PORC_INVERSION_TOTAL"]=txtPtgeInversion.Text;        
        dr["EPO_DESCRIPCION"]=txtDescripcion .Text ;
        dr["EPO_CUENCA_HIDROGRAFICA"] = txtCuencaHidrografica.Text;
        this.dsProyectosInversion.Tables[0].Rows.Add(dr);        
        Contexto.guardarTabla(dsProyectosInversion, "EIH_PROYECTOS_INVERSION");
    }

    private void eliminarRegistroGrillaProyectos(int index)
    {
        this.dsProyectosInversion .Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsProyectosInversion , "EIH_PROYECTOS_INVERSION");
        cargarGrillas();
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.plhInsersion.Visible = false;
    }
    protected void btnplaninversion_Click(object sender, EventArgs e)
    {
        guardarPlanInversion();
    }

    private void guardarPlanInversion()
    {
        if (dsPlanInversion.Tables[0].Rows.Count > 0)
        {
            dsPlanInversion.Tables[0].Rows[0]["EPI_ELEM_COSTOS_EST_INV"] = this.txtElementos.Text;
        }
        else
        {
            DataRow dr = dsPlanInversion.Tables[0].NewRow();
            dr["EPI_ELEM_COSTOS_EST_INV"] = this.txtElementos.Text;
            dr["EIP_ID"] = this.IDProyecto;
            dsPlanInversion.Tables[0].Rows.Add(dr);
           // dsPlanInversion.AcceptChanges();
        }
        Contexto.guardarTabla(dsPlanInversion, "EIH_PLAN_INVERSION");
        this.btnplaninversion.Enabled = true;
    }
    protected void grvProyectos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        eliminarRegistroGrillaProyectos(e.RowIndex);
    }
}
