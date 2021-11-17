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
using SoftManagement.Persistencia;
using System.Data.SqlClient;

public partial class ResumenEIA_Fichas_Ficha5 : System.Web.UI.UserControl
{

    public int IDProyecto
    {
        get { return (int)ViewState["IdProyecto"]; }
        set { ViewState["IdProyecto"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            
        }
       // CargarVistaGeneral();
    }

    public void CargarTodaInfo()
    {
        cargarComboTipoEval();
        CargarComboTipoImpacto();
    }

    DataView vgr1;
    DataView vgr2;
    DataView vgr3;
    DataView vgr4;
    DataView vgr5;
    DataView vgr6;
    DataView vgr7;
    DataView vgr8;
    DataView vgr9;
    DataView vgr10;


    private int seleccion;

    Dictionary<int, string> dlbl1 = new Dictionary<int, string>();
    Dictionary<int, string> dlbl2 = new Dictionary<int, string>();
    Dictionary<int, string> dlbl3 = new Dictionary<int, string>();

    private void cargarLabels()
    {
        /*
         * inicializacion de codigo
         */

        dlbl1.Add(1, "Codigo mapa");
        dlbl1.Add(2, "Codigo mapa");
        dlbl1.Add(5, "Codigo mapa");
        dlbl1.Add(6, "Codigo mapa");
        dlbl1.Add(7, "Codigo mapa");
        dlbl1.Add(8, "Codigo mapa");
        dlbl1.Add(3, "No");
        dlbl1.Add(4, "No");
        dlbl1.Add(9, "No");
        dlbl1.Add(10, "No");

        /*
         * carga de label 2*/

        dlbl2.Add(1, "Unidad A intervenir");
        dlbl2.Add(2, "Unidad A intervenir");
        dlbl2.Add(3, "Unidad A intervenir");
        dlbl2.Add(5, "Unidad A intervenir");
        dlbl2.Add(6, "Unidad A intervenir");
        dlbl2.Add(7, "Unidad A intervenir");
        dlbl2.Add(8, "Unidad A intervenir");

        dlbl2.Add(4, "fuentes de emisiones vapores o ruido");
        dlbl2.Add(10, "Componente a ser afectado");
        dlbl2.Add(9, "Dimension socioeconomica");
        /*
         * inicializacion de label 2
         * 
         * **/
        dlbl3.Add(1, "Infraestructura del proyecto que la intervienen");
        dlbl3.Add(2, "Infraestructura del proyecto que la intervienen");
        dlbl3.Add(3, "Infraestructura del proyecto que la intervienen");
        dlbl3.Add(5, "Infraestructura del proyecto que la intervienen");
        dlbl3.Add(6, "Infraestructura del proyecto que la intervienen");
        dlbl3.Add(7, "Infraestructura del proyecto que la intervienen");
        dlbl3.Add(8, "Infraestructura del proyecto que la intervienen");
        dlbl3.Add(4, "Infraestructura del proyecto que la genera");
        dlbl3.Add(9, "Actividades del proyecto que lo afectan");
        dlbl3.Add(10, "Actividades del proyecto que lo interviene");



    }

    private void cargarComboTipoEval()
    {
        DataSet dstipos = new DataSet();
        Contexto.cargarTabla(dstipos, "EIB_TIPOS_EVALUACION_AMBIENTAL");
        this.cboTipoEvaluacion.DataSource = dstipos.Tables[0];

        this.cboTipoEvaluacion.DataTextField = "ETV_TIPOS_EVALUACIONES_AMB";
        this.cboTipoEvaluacion.DataValueField = "ETV_ID";
        this.cboTipoEvaluacion.DataBind();
    }

    void CargarComboTipoImpacto()
    { 
        DataSet dstipos= new DataSet ();
        Contexto.cargarTabla(dstipos, "EIB_TIPO_IMPACTO_AMBIENTAL");
        this.cbo7.DataSource = dstipos.Tables[0];

        this.cbo7.DataTextField = "ETA_TIPO_IMPACTO_AMBIENTAL";
        this.cbo7.DataValueField = "ETA_ID";
        this.cbo7.DataBind();   
    }

    void CargarVistaGeneral()
    {
       
        
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIV_EVALUACION_AMBIENTAL");

        vgr1 = new DataView();
        vgr2 = new DataView();
        vgr3 = new DataView();
        vgr4 = new DataView();
        vgr5 = new DataView();
        vgr6 = new DataView();
        vgr7 = new DataView();
        vgr8 = new DataView();
        vgr9 = new DataView();
        vgr10 = new DataView();

        vgr1.Table = ds.Tables[0];
        vgr2.Table = ds.Tables[0];
        vgr3.Table = ds.Tables[0];
        vgr4.Table = ds.Tables[0];
        vgr5.Table = ds.Tables[0];
        vgr6.Table = ds.Tables[0];
        vgr7.Table = ds.Tables[0];
        vgr8.Table = ds.Tables[0];
        vgr9.Table = ds.Tables[0];
        vgr10.Table = ds.Tables[0];

        vgr1.RowFilter = "ETV_ID=1";
        vgr2.RowFilter = "ETV_ID=2";
        vgr3.RowFilter = "ETV_ID=3";
        vgr4.RowFilter = "ETV_ID=4";
        vgr5.RowFilter = "ETV_ID=5";
        vgr6.RowFilter = "ETV_ID=6";
        vgr7.RowFilter = "ETV_ID=7";
        vgr8.RowFilter = "ETV_ID=8";
        vgr9.RowFilter = "ETV_ID=9";
        vgr10.RowFilter = "ETV_ID=10";

        grid1.DataSource = vgr1;
        grid1.DataBind();
        
        grid2.DataSource = vgr2;
        grid2.DataBind();

        grid2.DataSource = vgr2;
        grid2.DataBind();
        
        grid3.DataSource = vgr3;
        grid3.DataBind();

        grid4.DataSource = vgr4;
        grid4.DataBind();
        
        grid5.DataSource = vgr5;
        grid5.DataBind();
        
        grid6.DataSource = vgr6;
        grid6.DataBind();

        grid7.DataSource = vgr7;
        grid7.DataBind();

        grid8.DataSource = vgr8;
        grid8.DataBind();

        grid9.DataSource = vgr9;
        grid9.DataBind();

        grid10.DataSource = vgr10;
        grid10.DataBind();

    }




    protected void grid10_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto.cargarTabla(parametros,ds, "EIH_EVALUACION_AMBIENTAL");

        DataRow dr = ds.Tables[0].NewRow();
        dr["EEA_COD_MAPA"] = txtCodigo.Text;
        dr["EEA_INFO_EVAL"] = txt2.Text;
        dr["EEA_INFRAC_INTERVIENE"] = txt3.Text;
        dr["EEA_AREA"] = txt4.Text;
        dr["EEA_PORC_AREA_TOTAL"] = txt5.Text;
        dr["EEA_IMPACTO_POTENCIAL"] = txt6.Text;
        dr["ETA_ID"] = cbo7.SelectedValue;
        dr["EIP_ID"] = IDProyecto;
        dr["ETV_ID"] = cboTipoEvaluacion.SelectedValue;

        ds.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(ds, "EIH_EVALUACION_AMBIENTAL");
        CargarVistaGeneral();
        this.plhInsersion.Visible = false;
    }
    protected void btnRegistrar_Click1(object sender, EventArgs e)
    {

    }


    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        this.plhInsersion.Visible = true;
    }
    protected void cboTipoEvaluacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarLabels();
        string slabel1;
        string slabel2;
        string slabel3;
        int seleccion = int.Parse (this.cboTipoEvaluacion.SelectedValue);
        dlbl1.TryGetValue(int.Parse(this.cboTipoEvaluacion.SelectedValue), out slabel1);
        dlbl2.TryGetValue(int.Parse (this.cboTipoEvaluacion.SelectedValue), out slabel2);
        dlbl3.TryGetValue(int.Parse (this.cboTipoEvaluacion.SelectedValue), out slabel3);

        this.lblCodigo .Text = slabel1;
        this.lbl2.Text = slabel2;
        this.lbl3.Text = slabel3;

        
        if (seleccion == 4 || seleccion == 9 )
        { this.lbl4.Visible = false; this.txt4.Visible = false; }
        else
        { this.lbl4.Visible = true; this.txt4.Visible = true; }

        if (seleccion == 4 || seleccion == 9 || seleccion == 10)
        { this.lbl5.Visible = false; this.txt5.Visible = false; }
        else
        { this.lbl5.Visible = true; this.txt5.Visible = true; }
    }
    protected void grid1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.plhInsersion.Visible = false;
    }
    protected void grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EEA_ID", SqlDbType.Int, 10, "EEA_ID");
        par.Value = vgr1[e.RowIndex].Row["EEA_ID"];
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_EVALUACION_AMBIENTAL");
        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIH_EVALUACION_AMBIENTAL");
        CargarVistaGeneral();



    }
    protected void grid2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EEA_ID", SqlDbType.Int, 10, "EEA_ID");
        par.Value = vgr2[e.RowIndex].Row["EEA_ID"];
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_EVALUACION_AMBIENTAL");
        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIH_EVALUACION_AMBIENTAL");
        CargarVistaGeneral();

    }
    protected void grid3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EEA_ID", SqlDbType.Int, 10, "EEA_ID");
        par.Value = vgr3[e.RowIndex].Row["EEA_ID"];
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_EVALUACION_AMBIENTAL");
        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIH_EVALUACION_AMBIENTAL");
        CargarVistaGeneral();

    }
    protected void grid4_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EEA_ID", SqlDbType.Int, 10, "EEA_ID");
        par.Value = vgr4[e.RowIndex].Row["EEA_ID"];
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_EVALUACION_AMBIENTAL");
        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIH_EVALUACION_AMBIENTAL");
        CargarVistaGeneral();

    }
    protected void grid5_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EEA_ID", SqlDbType.Int, 10, "EEA_ID");
        par.Value = vgr5[e.RowIndex].Row["EEA_ID"];
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_EVALUACION_AMBIENTAL");
        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIH_EVALUACION_AMBIENTAL");
        CargarVistaGeneral();

    }
    protected void grid6_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EEA_ID", SqlDbType.Int, 10, "EEA_ID");
        par.Value = vgr6[e.RowIndex].Row["EEA_ID"];
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_EVALUACION_AMBIENTAL");
        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIH_EVALUACION_AMBIENTAL");
        CargarVistaGeneral();

    }
    protected void grid7_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EEA_ID", SqlDbType.Int, 10, "EEA_ID");
        par.Value = vgr7[e.RowIndex].Row["EEA_ID"];
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_EVALUACION_AMBIENTAL");
        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIH_EVALUACION_AMBIENTAL");
        CargarVistaGeneral();

    }
    protected void grid8_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EEA_ID", SqlDbType.Int, 10, "EEA_ID");
        par.Value = vgr8[e.RowIndex].Row["EEA_ID"];
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_EVALUACION_AMBIENTAL");
        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIH_EVALUACION_AMBIENTAL");
        CargarVistaGeneral();

    }
    protected void grid9_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EEA_ID", SqlDbType.Int, 10, "EEA_ID");
        par.Value = vgr9[e.RowIndex].Row["EEA_ID"];
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_EVALUACION_AMBIENTAL");
        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIH_EVALUACION_AMBIENTAL");
        CargarVistaGeneral();

    }
    protected void grid10_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EEA_ID", SqlDbType.Int, 10, "EEA_ID");
        par.Value = vgr10[e.RowIndex].Row["EEA_ID"];
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_EVALUACION_AMBIENTAL");
        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIH_EVALUACION_AMBIENTAL");
        CargarVistaGeneral();

    }
}
