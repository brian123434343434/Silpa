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
using SoftManagement.Log;

public partial class Administracion_Tablasbasicas_TablasBasicas : System.Web.UI.Page
{
    private SILPA.LogicaNegocio.AdmTablasBasicas.TablasBasicas objTablasBasicas;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ConsultarInformacion("");
        }
    }

    private void ConsultarInformacion(string strNombre)
    {
        try
        {         
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.TablasBasicas();
            grdDatos.DataSource = objTablasBasicas.Listar_Tablas_Basicas(strNombre);
            grdDatos.DataBind();
        }
        catch (Exception ex)
        {            
            SMLog.Escribir(Severidad.Critico," Tablas Basicas --ConsultarInformacion"+ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
        }        
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ConsultarInformacion(txtNombreParametro.Text);
    }

    protected void grdDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Cambiar la pagina con la información
        this.grdDatos.PageIndex = e.NewPageIndex;
        ConsultarInformacion(txtNombreParametro.Text);
    }

    protected void btCancelar_Click(object sender, EventArgs e)
    {

    }
    
}
