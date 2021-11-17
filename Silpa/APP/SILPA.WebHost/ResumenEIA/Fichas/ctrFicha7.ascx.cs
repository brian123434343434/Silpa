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
using SoftManagement.Persistencia;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class ResumenEIA_Fichas_ctrFicha7 : System.Web.UI.UserControl
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

    public DateTime fecha
    {
        get { return new DateTime(2010, 11, 25, 14, 31, 50, 767); }
    }
       
    
    protected void Page_Load(object sender, EventArgs e)   
 {
        if (!IsPostBack)
        {
            
        }
    }
    public void CargarTodaInfo()
    {
        cargarGrvProyectos();
        cargarCboprogramaManejo();
    }
    protected void btnAgregarProgramaManejo_Click(object sender, EventArgs e)
    {
        this.plhProgramaManejo.Visible = true;

    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        agregarPrograma();
        cargarCboprogramaManejo();
    }


    private void agregarPrograma()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> Parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        Parametros.Add(par);
        Contexto.cargarTabla(ds, "EIH_PROGRAMA_MANEJO_AMBIENTAL");
        DataRow dr = ds.Tables [0].NewRow ();
        //dr["EMA_ID"]=      
        dr["EIP_ID"] = IDProyecto;
        dr["EMA_FECHA_CREACION"] = DateTime.Now;      
	    dr["EMA_NOMBRE_PROGRAMA"]=this.txtNombreProgramaManejo .Text ;

        ds.Tables [0].Rows.Add (dr);
        Contexto.guardarTabla (ds,"EIH_PROGRAMA_MANEJO_AMBIENTAL");
        this.txtNombreProgramaManejo.Text = string.Empty;
        this.plhProgramaManejo.Visible = false;
       


    }
   private void cargarCboprogramaManejo()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> Parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        Parametros.Add(par);
        Contexto.cargarTabla(ds, "EIH_PROGRAMA_MANEJO_AMBIENTAL");
        this.cboProgramaManejo .DataSource = ds.Tables[0];
        this.cboProgramaManejo.DataValueField = "EMA_ID";
        this.cboProgramaManejo.DataTextField = "EMA_NOMBRE_PROGRAMA";
        this.cboProgramaManejo.DataBind();
        cboProgramaManejo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
       // cboProgramaManejo.SelectedValue  = -1;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.txtNombreProgramaManejo.Text = string.Empty;
        this.plhProgramaManejo.Visible = false;
    }
    protected void grvProgramaManejo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grvProgramaManejo_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
  
   protected void btnAgregarProyecto_Click(object sender, EventArgs e)
   {

       ctrProyectos1.AgregarProyecto();
       cargarGrvProyectos();



   }

   private void cargarGrvProyectos()
   {

       DataSet ds = new DataSet();
       List<SqlParameter> Parametros = new List<SqlParameter>();
       SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
       par.Value = IDProyecto;
       Parametros.Add(par);
       Contexto.cargarTabla(Parametros,ds, "EIV_PROGRAMA_MANEJO_AMBIENTAL");
       this.grvProyectos.DataSource = ds.Tables[0];
       this.grvProyectos.DataBind(); 




   }

   private void pintarControles()
   {
     

      
   }
    protected void btnCancelarProyecto_Click(object sender, EventArgs e)
    {
        plhProyecto.Visible = false;
    }
    protected void cboProgramaManejo_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        //if (cboProgramaManejo.SelectedItem .Value >0)
        //{
            this.ctrProyectos1.cargarCombos();
            ctrProyectos1.IDPadre = int.Parse(cboProgramaManejo.SelectedValue);
            ctrProyectos1.ProgramaManejo = cboProgramaManejo.SelectedItem.Text ;
            this.ctrProyectos1.Visible = true;
            this.plhProyecto.Visible = true;
        //}
    }
    protected void btnAgregarProyectoManejo_Click(object sender, EventArgs e)
    {
        this.plhProyecto.Visible = true;
    }
    protected void grvProyectos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnEliminarProyecto_click(object sender, EventArgs  e)
    {

    }
    protected void grvProyectos_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        
    }

}
