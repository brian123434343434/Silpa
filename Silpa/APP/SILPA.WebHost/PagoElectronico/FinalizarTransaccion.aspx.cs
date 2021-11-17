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
using PSEWebServicesClient3;

public partial class PagoElectronico_PaginaError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["MESFIN"] != null)
            this.lblError.Text = Session["MESFIN"].ToString();
        else
            this.lblError.Text = "Sin INFO";;
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {

        string _respuestaEnvio = "<script language='JavaScript'>" +
              "window.close()</script>";
        Page.RegisterStartupScript("PopupScript", _respuestaEnvio);
        //MainServices pse_ws = PPEController.GetPSEWebservice();
        //getTransactionInformationBodyType request = new getTransactionInformationBodyType();
        //request.entityCode = "830025267";
        //request.trazabilityCode = "3906610";
        //getTransactionInformationResponseBodyType response = pse_ws.getTransactionInformation(request);
        //Mensaje.MostrarMensaje(this.Page, response.returnCode.ToString());
        //if (response.returnCode == getTransactionInformationResponseReturnCodeList.SUCCESS)
        //{
           
        //}
    }
}
