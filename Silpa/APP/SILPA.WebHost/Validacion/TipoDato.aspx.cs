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

public partial class Validacion_TipoDato : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAgregarTipoDato_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAgregarTipoDato_Click.Inicio");
            SILPA.LogicaNegocio.ValidadoresBPM.TipoDato _tipoDato = new SILPA.LogicaNegocio.ValidadoresBPM.TipoDato();
            _tipoDato.InsertarTipoDato(txtDescripcionTipoDato.Text);
            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("VALIDACIÓN", 1, "Se almacenó Tipo de Dato");

            txtDescripcionTipoDato.Text = "";
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAgregarTipoDato_Click.Finalizo");        
        }
    }

    protected void btnCancelarTipoDato_Click(object sender, EventArgs e)
    {
        txtDescripcionTipoDato.Text = "";
    }
}
