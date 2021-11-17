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

public partial class EIA_Consultar_Formularios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            SILPA.LogicaNegocio.EIA.EIA objEIA = new SILPA.LogicaNegocio.EIA.EIA();
            cboNumeroVital.DataSource = objEIA.ConsultaListaNumeroVital(194);
            cboNumeroVital.DataValueField = "NUMERO_VITAL";
            cboNumeroVital.DataTextField = "NUMERO_VITAL";
            cboNumeroVital.DataBind();
            cboNumeroVital.Items.Insert(0, new ListItem("Seleccione.", "-1"));
        }

    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {       
        Consultar();
    }
    protected void grdReporte_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grdReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //grdReporte.PageIndexChanged();
        grdReporte.PageIndex = e.NewPageIndex;
        Consultar();        
        grdReporte.PageSize = 5;
        //CargarSector();
    }

    private void Consultar()
    {
         SILPA.LogicaNegocio.EIA.EIA objEIA = new SILPA.LogicaNegocio.EIA.EIA();
        this.grdReporte.DataSource = objEIA.ConsultarFormularios("",194,null,null);
        this.grdReporte.EmptyDataText = "No se encontraron registros para esta consulta.";
        this.grdReporte.DataBind();
    }
}
