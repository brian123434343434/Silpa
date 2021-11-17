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

public partial class Validacion_Validacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCancelarValidacion_Click(object sender, EventArgs e)
    {
        txtDescripcionValidacion.Text = "";
        txtSentenciaValidacion.Text = "";
    }

    protected void btnAgregarValidacion_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAgregarValidacion_Click.Inicio");
            SILPA.LogicaNegocio.ValidadoresBPM.Validacion _validacion = new SILPA.LogicaNegocio.ValidadoresBPM.Validacion();
            _validacion.InsertarValidacion(txtDescripcionValidacion.Text, txtSentenciaValidacion.Text);
            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("VALIDACIÓN", 1, "Se almacenó Validación");

            txtDescripcionValidacion.Text = "";
            txtSentenciaValidacion.Text = "";
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAgregarValidacion_Click.Finalizo");
        }
    }
}
