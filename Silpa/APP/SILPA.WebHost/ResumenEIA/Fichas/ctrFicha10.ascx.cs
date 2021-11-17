using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SoftManagement.Persistencia;
using System.Data.SqlClient;
//using SoftManagement.Persistencia;

public partial class ResumenEIA_Fichas_ctrFicha10 : System.Web.UI.UserControl
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

    private string etiquetaLabel;

    public string EtiquetaLabel
    {
        get { return etiquetaLabel;}
        set { etiquetaLabel = value;}
    }

    private int idSeleccion;
    public int IDSeleccion
    {
        get { return idSeleccion; }
        set { idSeleccion = value; }
    }


    private void cargarCombos()
    {
        dsTipoPLan = new DataSet();
        Contexto.cargarTabla(dsTipoPLan, "EIB_TIPO_PLAN_ABAN_REST_FINAL");
        this.cboPlan.DataSource = dsTipoPLan.Tables[0];
        this.cboPlan.DataValueField  = "ETM_ID";
        this.cboPlan.DataTextField = "ETM_TIPO_PLAN";
        cboPlan.DataBind();
        
    }
    DataSet dsTipoPLan;
    DataSet dsPlan;
    DataSet dsPlanView;

    private void cargarVistaPlan()
    {
        dsPlanView = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter parametro = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        parametro.Value = IDProyecto;
        parametros.Add(parametro);
        Contexto.cargarTabla(parametros,dsPlanView, "EIV_PLAN_ABANDONO_REST_FINAL");
        this.grvProgramas.DataSource = dsPlanView.Tables[0];
        this.grvProgramas.DataBind();
    }

    private void cargarPlan()
    {
        dsPlan = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter parametro = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        parametro.Value = IDProyecto;
        parametros.Add(parametro);
        Contexto.cargarTabla(parametros, dsPlan, "EIh_PLAN_ABANDONO_REST_FINAL");
        
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
        cargarGrillas();
        cargarVistaPlan();
    }

    private void cargarGrillas()
    {
        
    }


    protected void cbo_SelectedIndexChanged(object sender, EventArgs e)
    {
        limpiarControles();
    }



    protected void btnPlanAbandono_Click(object sender, EventArgs e)
    {
        this.plhitems.Visible = true;               
        limpiarControles();
    }

    private void limpiarControles()
    {
        this.txtActividad.Text = string.Empty;
        this.txtElementosSeguimiento.Text = string.Empty;
        this.txtIndicadores.Text = string.Empty;
        this.txtPrograma.Text = string.Empty;
        this.txtUbicacion .Text =string.Empty ;       
 
    }
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        this.plhitems.Visible = false;
        cargarPlan();
        DataRow dr = dsPlan.Tables[0].NewRow();
        dr["EPS_ACTIVIDAD"] = this.txtActividad.Text;
        dr["EPS_ELEMENTOS_SEGUIMIENTO"] = this.txtElementosSeguimiento.Text;
        dr["EPS_INDICADORES"] = this.txtIndicadores.Text;
        dr["EPS_PROGRAMA"] = this.txtPrograma.Text;
        dr["EPS_UBICACION"] = this.txtUbicacion.Text;
        dr["ETM_ID"] = this.cboPlan.SelectedValue;
        dr["EIP_ID"] = this.IDProyecto; 
        dsPlan.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsPlan, "EIh_PLAN_ABANDONO_REST_FINAL");
        limpiarControles();
        cargarVistaPlan();
        this.plhitems.Visible = false;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.limpiarControles();
        this.plhitems.Visible = false;
    }
    protected void grvProgramas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EliminarPlan(e.RowIndex);
        cargarVistaPlan();
    }

    private void EliminarPlan(int p)
    {
        int idFila = 0;
        cargarVistaPlan();
        DataRow fila = this.dsPlanView .Tables[0].Rows[p];
        idFila = int.Parse(fila["EPS_ID"].ToString());

        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter parametro = new SqlParameter("@EPS_ID", SqlDbType.Int, 10, "EPS_ID");
        parametro.Value = idFila;
        parametros.Add(parametro);

        DataSet dsEliminar = new DataSet();
        Contexto.cargarTabla(parametros, dsEliminar, "EIh_PLAN_ABANDONO_REST_FINAL");

        dsEliminar.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(dsEliminar, "EIh_PLAN_ABANDONO_REST_FINAL");

    }
    protected void grvProgramas_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

