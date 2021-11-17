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

public partial class LicenciasAmbientales_LPA_DILIGENCIAR_DATOS_DAA_SEC_II : System.Web.UI.Page
{
    string strUrl;
    string strProcSelected;
    protected void Page_Load(object sender, EventArgs e)
    {
        strProcSelected = ConfigurationManager.AppSettings["PROC_SELECTED"].ToString();
        strUrl = ConfigurationManager.AppSettings[strProcSelected].ToString();
    }
    protected void BTN_ANTEROR_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect("LPA_Diligenciar_Datos_DAA_Sec_I.aspx");
    }
    
    protected void BTN_GUARDAR_Click(object sender, EventArgs e)
    {
        string msg = "Envia el FUS.xml al SILA Consumiendo el WS: http://ServerSILA/WSSilaGetFUS.asmx \\n";
        msg = msg + "Sus datos han sido guardados exitosamente .. ";
        GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
        CrearLogAuditoria.Insertar("LICENCIASAMBIENTALES – RADICAR FU", 1, "Se almaceno Formulario único nacional de solicitud de permisos ambientales");


        //ClientScript.RegisterStartupScript(this.GetType(), "Alerta", "<script> alert('"+msg+"'); </script>");
        Response.Redirect(strUrl);
        
    }
    protected void BTN_ANEXAR_DOCUMENTOS_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect("LPA_05_Anexar_Documentacion_Soporte_Solicitante.aspx");
    }
    protected void BTN_CANCELAR_Click(object sender, EventArgs e)
    {

    }
}
