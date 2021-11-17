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
using Auditoria;
using SoftManagement.Log;

public partial class Auditoria_LogAuditoria : System.Web.UI.Page
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
        

        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Actualizar.Inicio");
            grdResultado.Visible = true;



            grdResultado.DataSource = ConsultarLog();
            grdResultado.DataBind();

        }
        catch (Exception ex)
        {
            grdResultado.Visible = false;
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Actualizar.Finalizo");
        }

        UPauditoria.Update();
    }

    protected void CargarCombos()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarCombos.Inicio");
            Log LogAuditoria = new Log();

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

    protected void grdResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Cambiar la pagina con la información
        this.grdResultado.PageIndex = e.NewPageIndex;
        grdResultado.DataSource = ConsultarLog();
        grdResultado.DataBind();
    }

    private DataSet ConsultarLog()
    {
        Log LogAuditoria = new Log();
        DataSet _dsDatos;

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

        _dsDatos = LogAuditoria.Buscar(txtFechaIni.Text.Trim(), txtFechaFin.Text.Trim());

        return _dsDatos;
    }
}
