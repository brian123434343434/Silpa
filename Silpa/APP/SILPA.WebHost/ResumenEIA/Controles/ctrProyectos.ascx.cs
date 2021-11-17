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

public partial class ResumenEIA_Controles_ctrProyectos : System.Web.UI.UserControl
{
    public DataSet DsProyectos
    {
        get {
            if (Session["dsProyectos"] != null)
                return (DataSet)Session["dsProyectos"];
            else
                return new DataSet();
        }
        set { Session["dsProyectos"] = value; }
    }   
    public string ProgramaManejo
    {
        set { this.lblProgramaManejo.Text = value; }
    }   
    public int IDPadre
    {
        get { return (int)ViewState ["IDPadre"]; }
        set { ViewState["IDPadre"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    public void cargarCombos()
    {
        cargarCboEtapaAplicacion();
        cargarCboTipoMedida();
    }
    private void cargarCboEtapaAplicacion() 
    {
        DataSet ds = new DataSet();
        Contexto.cargarTabla(ds, "EIB_ETAPA_APLICACION_PROYECTO");
        this.cboEtapaAplicacion.DataSource = ds;
        cboEtapaAplicacion.DataTextField = "EEA_ETAPA_APLICACION_PROY";
        cboEtapaAplicacion.DataValueField = "EEA_ID";
        cboEtapaAplicacion.DataBind();

    }
    private void cargarCboTipoMedida()
    {
        DataSet ds = new DataSet();
        Contexto.cargarTabla(ds, "EIB_TIPO_MEDIDA_PROYECTO_MAN_AMB");
        this.cboTipoMedida.DataSource = ds;
        cboTipoMedida.DataTextField = "ETM_TIPO_MEDIDA_PROY_MAN_AMB";
        cboTipoMedida.DataValueField = "ETM_ID";
        cboTipoMedida.DataBind();
    }
    private void cargarProyectos()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> Parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EMA_ID", SqlDbType.Int, 10, "EMA_ID");
        par.Value = IDPadre;
        Parametros.Add(par);
        Contexto.cargarTabla(ds, "EIH_PROYECTO_MANEJO");
        DsProyectos = ds;


     
    }
    public void AgregarProyecto()
    {
        cargarProyectos();
        DataRow dr = DsProyectos.Tables[0].NewRow();
        //dr["EPM_ID"]      
        dr["EEA_ID"]=cboEtapaAplicacion .SelectedValue ;      
        dr["ETM_ID"]=cboTipoMedida .SelectedValue ;      
        dr["EMA_ID"]=IDPadre;      
        dr["EPM_NOMBRE_PROYECTO"]=this.txtNombreProyecto .Text ;
        dr["EPM_OBJETIVO"]=txtObjetivos .Text ;                                                                                                                                                                                                                                                     
        dr["EPM_METAS"]=txtMetas .Text ;                                                                                                                                                                                                                                                        
        dr["EPM_IMPACTOS_MANEJAR"] =txtImpactoManejar .Text ;
        dr["EPM_INDICADORES_SEG_MONIT"] = txtIndicadores.Text;
        this.DsProyectos.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(DsProyectos, "EIH_PROYECTO_MANEJO");
        limpiarControles();

    }
    public void pintarRegistro()
    {
       /* cargarCombos();
        cboEtapaAplicacion.SelectedValue = dr["EEA_ID"].ToString ();
        cboTipoMedida.SelectedValue = dr["ETM_ID"].ToString ();
        this.txtNombreProyecto.Text = dr["EPM_NOMBRE_PROYECTO"].ToString(); ;
        txtObjetivos.Text=dr["EPM_OBJETIVO"].ToString () ;
        txtMetas.Text = dr["EPM_METAS"].ToString();
        txtImpactoManejar.Text = dr["EPM_IMPACTOS_MANEJAR"].ToString();
        txtIndicadores.Text = dr["EPM_INDICADORES_SEG_MONIT"].ToString();*/
    
    }

    private void limpiarControles()
    {
        txtImpactoManejar.Text = string.Empty;
        txtIndicadores.Text = string.Empty;
        txtMetas.Text = string.Empty;
        txtNombreProyecto.Text = string.Empty;
        txtObjetivos.Text = string.Empty;

    }
}
