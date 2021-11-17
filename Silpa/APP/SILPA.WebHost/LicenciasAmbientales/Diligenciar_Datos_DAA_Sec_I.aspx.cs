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

public partial class LicenciasAmbientales_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //((Label)this.Ctl_BASICDATA_RAZON_SOCIAL.FindControl("LBL_TITULO")).Text = "NOMBRE O RAZON SOCIAL:";
        //((TextBox)this.Ctl_BASICDATA_RAZON_SOCIAL.FindControl("TXT_VALOR_TITULO")).Text = "SOFTMANAGEMENT";
        //((TextBox)this.Ctl_BASICDATA_RAZON_SOCIAL.FindControl("TXT_NUMERO_DOCUMENTO")).Text = "12345678";
        //((RadioButtonList)this.Ctl_BASICDATA_RAZON_SOCIAL.FindControl("RDG_LST_TIPO_DOCUMENTO")).Items[1].Selected = true;

        //((Label)this.Ctl_BASICDATA_REPRESENTANTE_LEGAL.FindControl("LBL_TITULO")).Text = "REPRESENTANTE LEGAL:";
        //((TextBox)this.Ctl_BASICDATA_REPRESENTANTE_LEGAL.FindControl("TXT_VALOR_TITULO")).Text = "MARIA DEL PILAR";
        //((TextBox)this.Ctl_BASICDATA_REPRESENTANTE_LEGAL.FindControl("TXT_NUMERO_DOCUMENTO")).Text = "65412398";

        //((Label)this.Ctl_BASICDATA_APODERADO.FindControl("LBL_TITULO")).Text = "APODERADO:";
        //((TextBox)this.Ctl_BASICDATA_APODERADO.FindControl("TXT_VALOR_TITULO")).Text = "JUAN FELIPE ORDOÑEZ";
        //((TextBox)this.Ctl_BASICDATA_APODERADO.FindControl("TXT_NUMERO_DOCUMENTO")).Text = "85236914";

    }

    protected void BTN_SIGUIENTE_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("Diligenciar_Datos_DAA_Sec_II.aspx");
    }
    
    protected void Ctl_BASICDATA_RAZON_SOCIAL_Load(object sender, EventArgs e)
    {

    }
}
