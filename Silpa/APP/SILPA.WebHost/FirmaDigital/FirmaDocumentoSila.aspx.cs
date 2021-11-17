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

using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;

public partial class FirmaDigital_FirmaDocumento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlControl frame1 = (HtmlControl)this.FindControl("ifdoc");
        if (Request.QueryString.Count > 0)
        {
         //   frame1.Attributes["src"] = Request["url"];
        }


    }
    protected void btnFirmar_Click(object sender, EventArgs e)
    {
        
       
    }
    protected void btn_aceptar_Click(object sender, EventArgs e)
    {
        pnl_certificados.Visible = false;
        pnl_clave.Visible = true;
    }
    protected void btn_firmar_Click(object sender, EventArgs e)
    {
        pnl_certificados.Visible = true;
    }
    protected void btn_aceptar_clave_Click(object sender, EventArgs e)
    {
        pnl_clave.Visible = false;
        lbl_resultado.Text = "Documento Firmado Exitosamente";
    }
    protected void btn_volver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FirmaDigital/DocumentosFirma.aspx");
    }
    protected void btn_rechazar_Click(object sender, EventArgs e)
    {

    }
}
