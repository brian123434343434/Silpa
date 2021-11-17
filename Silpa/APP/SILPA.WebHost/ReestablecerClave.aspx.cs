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
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;

public partial class ReestablecerClave : System.Web.UI.Page
{
    private Persona objPersona;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = @"function atras(){history.go(-1);}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "atras", script, true);

      //string temp =  SILPA.Comun.EnDecript.Desencriptar("b5cdab51c66a2e6e763487db44f9e8c525f2079c991f778c");
    }

    protected void btnResClave_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnResClave_Click.Inicio");
            objPersona = new Persona();
            lblError.Text = objPersona.ReestablecerClave(txtUsuario.Text);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnResClave_Click.Finalizo");
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        ParametroDalc parametroDalc = new ParametroDalc();
        ParametroEntity parametro = new ParametroEntity();
        parametro.IdParametro = -1;
        parametro.NombreParametro = "login_silpa";
        parametroDalc.obtenerParametros(ref parametro);
        Response.Redirect(parametro.Parametro);
    }
}
