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

public partial class Administracion_Notificacion_parametrizacionNotificacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = @"function atras(){history.go(-1);}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "atras", script, true);
        lblResultado.Text = "";
        if (!Page.IsPostBack)
        {
            int _intActivo = 0;
           SILPA.LogicaNegocio.Parametrizacion.Parametrizacion parametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
           txtTiempo.Text = parametrizacion.obtenerTiempoNotificacion(out _intActivo);
           this.chkActivo.Checked = Convert.ToBoolean(_intActivo);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnGuardar_Click.Inicio");
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion parametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();

            int _intActivo = Convert.ToInt32(this.chkActivo.Checked);
            parametrizacion.actualizarTiempoNotificacion(txtTiempo.Text, _intActivo);
            lblResultado.Text = "Información Guardada Con Éxito";
            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("NOTIFICACION", 1, "Se almaceno notificación");
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
