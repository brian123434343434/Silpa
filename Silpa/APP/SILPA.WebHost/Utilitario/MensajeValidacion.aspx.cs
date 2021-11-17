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

public partial class Utilitario_MensajeValidacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            if (Session["Token"] == null)
                LblMensaje.Text = "Señor usuario usted ha tratado de ingresar sin validar Token de inicio de sesion por favor desista";
            else
                LblMensaje.Text = "El token " + Session["Token"].ToString() + " generado para el usuario no es valido por favor intente nuevamente";
        }
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"http:\\www.minambiente.gov.co");  
    }
}
