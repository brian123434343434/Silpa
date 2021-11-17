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

public partial class ResumenEIA_Fichas_ctrFicha6 : System.Web.UI.UserControl
{
    public int IDProyecto
    {
        get { 
            if (ViewState["IdProyecto"]!=null)
                return (int)ViewState["IdProyecto"];
            return -1;
        }
        set { ViewState["IdProyecto"] = value; }
    }

    DataSet dsZonificacionAmbiental;
    DataView VAreasInclusion;
    DataView VAreasIntervencionRestricciones;
    DataView VAreasIntervencion;

    public void cargarCombos()
    {
        DataSet dstipoIntervencion = new DataSet();
        Contexto.cargarTabla(dstipoIntervencion, "EIB_TIPO_INTERVENCION_ZONIFI_AMB");
        this.cmbGrilla.DataSource = dstipoIntervencion.Tables[0];
        this.cmbGrilla.DataTextField = "ETI_TIPO_INTERVENCION_ZONIF";
        this.cmbGrilla.DataValueField = "ETI_ID";
        this.cmbGrilla.DataBind();

    }
    public void cargarZonificacion()
    {
        dsZonificacionAmbiental = new DataSet();
        List<SqlParameter> Parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        Contexto.cargarTabla(Parametros, dsZonificacionAmbiental , "EIH_AREA_ZONIFICACION_AMBIENTAL");    
    }
    public void cargarGrillas()
    {
        VAreasInclusion= new DataView ();
        VAreasIntervencionRestricciones= new DataView ();
        VAreasIntervencion= new DataView ();

        
        VAreasInclusion.Table = dsZonificacionAmbiental.Tables[0];
        VAreasIntervencionRestricciones.Table  = dsZonificacionAmbiental.Tables[0];
        VAreasIntervencion.Table  = dsZonificacionAmbiental.Tables[0];

        VAreasInclusion.RowFilter = "ETI_ID=1";
        VAreasIntervencionRestricciones.RowFilter = "ETI_ID=2";
        VAreasIntervencion.RowFilter = "ETI_ID=3";

        this.grid1.DataSource = VAreasInclusion;
        this.grid2.DataSource = VAreasIntervencionRestricciones;
        this.grid3.DataSource = VAreasIntervencion;

        this.grid1.DataBind();
        this.grid2.DataBind();
        this.grid3.DataBind();
    }

    public void AgregarArea()
    {
        cargarZonificacion();
        this.cargarGrillas();
        DataRow dr = dsZonificacionAmbiental.Tables[0].NewRow();
        dr["EAR_AREA"] = this.txtArea.Text;
        dr["EAR_PORC_AREA_INTER"] = this.txtAreaPtge.Text;
        dr["EAR_COD_MAPA"] = this.txtCodigoMapa.Text;

        dr["EAR_DESC_UNIDAD"] = this.txtDescripcionGeneral .Text;
        dr["EAR_LINEAMIENTO_MANEJO"] = this.txtLineamientoManejo.Text;
        dr["EAR_COMP_PROYECT"] = this.txtProyectoInterviene.Text;
        dr["EAR_UNID_ZON_AMB_ASOC"] = this.txtUnidadZonificacion.Text;
        dr["EAR_VAR_AMB_DETERMINANTE"] = this.txtVarAmbiental.Text;
        dr["ETI_ID"] = this.cmbGrilla.SelectedValue;
        this.dsZonificacionAmbiental.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsZonificacionAmbiental, "EIH_AREA_ZONIFICACION_AMBIENTAL");
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { }        
        
    }

    public void CargarTodaInfo()
    {
        cargarCombos();
        cargarZonificacion();
        cargarGrillas();  
    }
  

    protected void cmbGrilla_SelectedIndexChanged(object sender, EventArgs e)
    {
  
    }
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        AgregarArea();
        cargarGrillas();
        limpiarControles();
        this.plhFormulario.Visible = false;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        plhFormulario.Visible = false;
        limpiarControles();
    }
    protected void btnAgregarItem_Click(object sender, EventArgs e)
    {
        plhFormulario.Visible = true;
        limpiarControles();

    }

    private void limpiarControles()
    {
        this.txtArea.Text  = string.Empty;
        this.txtAreaPtge.Text = string.Empty;
        txtCodigoMapa.Text=string.Empty;
        txtDescripcionGeneral .Text = string.Empty;
        txtLineamientoManejo.Text = string.Empty;
        txtProyectoInterviene.Text = string.Empty; ;
        txtUnidadZonificacion.Text = string.Empty; ;
        txtVarAmbiental.Text = string.Empty; ;
    }
    protected void grid1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        VAreasInclusion.Delete(e.RowIndex);
        Contexto.guardarTabla(dsZonificacionAmbiental, "EIH_AREA_ZONIFICACION_AMBIENTAL");
        cargarGrillas();
    }


        protected void grid2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        VAreasIntervencionRestricciones.Delete(e.RowIndex);
        Contexto.guardarTabla(dsZonificacionAmbiental, "EIH_AREA_ZONIFICACION_AMBIENTAL");
        cargarGrillas();
    }
    protected void grid3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        VAreasIntervencion.Delete(e.RowIndex);
        Contexto.guardarTabla(dsZonificacionAmbiental, "EIH_AREA_ZONIFICACION_AMBIENTAL");
        cargarGrillas();
    }
}
