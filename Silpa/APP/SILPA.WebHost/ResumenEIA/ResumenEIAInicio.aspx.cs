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
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class ResumenEIA_ResumenEIAInicio : System.Web.UI.Page
{
    private bool estado = false;
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCrearResumen_Click(object sender, EventArgs e)
    {
        string strScript = "<script language='JavaScript'>" +
            "var anchura = screen.width ;"+
            "var altura = screen.height ;"+
                    "window.open('Default.aspx?IDProyecto=-1&ReadOnly=true','Pruebas','location=no,resizable=yes,scrollbars=yes',width=anchura,height=altura)" +
                    "</script>";
        Page.RegisterStartupScript("PopupScript", strScript);

       
       
    }
    protected void btnAbrirResumen_Click(object sender, EventArgs e)
    {
        this.plhResumenes.Visible = true;
        cargarResumenes();
    }

    private void cargarResumenes()
    {
       DataSet ds= new DataSet ();
       Contexto.cargarTabla(ds, "EIV_informacion_proyecto");
       this.lstProyectos.DataSource = ds.Tables[0];
       this.lstProyectos.DataValueField = "EIP_ID";
       this.lstProyectos.DataTextField = "EIP_NOMBRE_PROYECTO";
       this.lstProyectos.DataBind();

    }
    protected void btnAbrirResumenEIA_Click(object sender, EventArgs e)
    {
        bool estado = false;
        DataSet ds= new DataSet ();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = this.lstProyectos.SelectedValue;
        parametros.Add(par);
        Contexto.cargarTabla(parametros,ds, "EIh_informacion_proyecto");
        estado = ds.Tables[0].Rows[0]["EIP_NUMERO_SILPA"]==null ?false  :true;



        
        string strScript = "<script language='JavaScript'>" +
            "window.open('Default.aspx?IDProyecto=" + lstProyectos.SelectedValue + "&ReadOnly=" + estado + "','Pruebas','location=no,resizable=yes,scrollbars=yes')" +
            "</script>";
        Page.RegisterStartupScript("PopupScript", strScript);
       
    }

}
