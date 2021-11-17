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

public partial class Administracion_AdministracionDocumentos_insertarDocumento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion parametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
            ddlParametro.DataSource = parametrizacion.listarParametrosBPM(3);
            ddlParametro.DataValueField = "ID";
            ddlParametro.DataTextField = "NOMBRE";
            ddlParametro.DataBind();
            ListItem li = new ListItem("Seleccionar...", "-1");
            li.Selected = true;
            ddlParametro.Items.Add(li);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnGuardar_Click.Inicio");
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion parametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
            int? seleccionado = Convert.ToInt32(ddlParametro.SelectedItem.Value);
            seleccionado = (seleccionado == -1) ? null : seleccionado;
            parametrizacion.insertarTipoDocumento(txtTipoDocumento.Text, seleccionado);
            txtTipoDocumento.Text = "";
            ddlParametro.SelectedValue = "-1";
            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("DOCUMENTOS", 1, "Se almaceno Tipo de Documento");
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnGuardar_Click.Finalizo");
        }
    }
}
