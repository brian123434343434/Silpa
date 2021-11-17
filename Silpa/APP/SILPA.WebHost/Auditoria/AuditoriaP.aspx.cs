using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Auditoria;
using SoftManagement.Log;

public partial class AuditoriaP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarCombos();
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        Log LogAuditoria = new Log();

        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnBuscar_Click.Inicio");
            grdResultado.Visible = true;

            LogAuditoria.Login_Usuario = txtLoginUsuario.Text.Trim();
            LogAuditoria.Identificacion_Usuario = txtIdentificacion.Text.Trim();
            LogAuditoria.Nombre_Usuario = txtNombreUsuario.Text.Trim();
            LogAuditoria.Autoridad_Ambiental = cboAutoridadAmbiental.SelectedValue.Trim();
            LogAuditoria.Modulo = cboModulo.SelectedValue.Trim();

            if (cboAccionRealizada.SelectedValue != "")
                LogAuditoria.Accion_Realizada = int.Parse(cboAccionRealizada.SelectedValue);
            else
                LogAuditoria.Accion_Realizada = -1;

            LogAuditoria.Detalle_Accion_Realizada = txtDetalleAccion.Text.Trim();

            grdResultado.DataSource = LogAuditoria.Buscar(txtFechaIni.Text.Trim(), txtFechaFin.Text.Trim());
            grdResultado.DataBind();
        }
        catch (Exception ex)
        {
            grdResultado.Visible = false;
            SMLog.Escribir(ex);
        }
        finally 
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnBuscar_Click.Finalizo");
        }

    }

    protected void CargarCombos()
    {
        try
        {
            Log LogAuditoria = new Log();
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarCombos.Inicio");
            cboAutoridadAmbiental.DataTextField = "DESCRIPCION";
            cboAutoridadAmbiental.DataValueField = "DESCRIPCION";
            cboAutoridadAmbiental.DataSource = LogAuditoria.ListarFiltrosDisponibles(1);
            cboAutoridadAmbiental.DataBind();
            cboAutoridadAmbiental.Items.Insert(0, new ListItem("", ""));
            cboAutoridadAmbiental.Items.IndexOf(cboAutoridadAmbiental.Items.FindByValue(""));
            cboModulo.DataTextField = "DESCRIPCION";
            cboModulo.DataValueField = "DESCRIPCION";
            cboModulo.DataSource = LogAuditoria.ListarFiltrosDisponibles(2);
            cboModulo.DataBind();
            cboModulo.Items.Insert(0, new ListItem("", ""));
            cboModulo.Items.IndexOf(cboAutoridadAmbiental.Items.FindByValue(""));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally 
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarCombos.Finalizo");
        }
    }
}
