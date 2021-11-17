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

public partial class LicenciasAmbientales_LPA_Diligenciar_FUS_Electronico : System.Web.UI.Page
{
    public string silpaCod = "SILPA-001232100";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // Segunda vista del multiview
    public void MostrarViewSeccionIII()
    {
        this.MVW_FUS.ActiveViewIndex = 1;
    }
 
    public void Limpiar()
    {
        this.TXT_CODIGO_SILPA.Text = string.Empty;
        this.PNL_FUS.Visible = false;
    }

    protected void BTN_BUSCAR_DATOS_Click(object sender, EventArgs e)
    {
        if (silpaCod == this.TXT_CODIGO_SILPA.Text && this.TXT_CODIGO_SILPA.Text != string.Empty )
        {
            //this.PNL_FUS.Visible = true;
            this.MVW_FUS.ActiveViewIndex = 0;
        }
        else 
        {
            this.Limpiar();
        }
        //this.UpdatePanel1.Update();
    }
    protected void BTN_LIMPIAR_Click(object sender, EventArgs e)
    {
        this.Limpiar();

   }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        this.MVW_FUS.ActiveViewIndex = 1;
    }
}
