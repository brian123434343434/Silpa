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

public partial class Administracion_Tablasbasicas_NotTiempo : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string script = @"function atras(){history.go(-1);}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "atras", script, true);
        lblResultado.Text = "";
        if (!Page.IsPostBack)
        {
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion parametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
            int _activo = 0;
            txtTiempo.Text = parametrizacion.obtenerTiempoNotificacion(out _activo);
            this.chkActivo.Checked = Convert.ToBoolean(_activo);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
                SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnGuardar_Click.Inicio");
                int _intActivo = Convert.ToInt32(this.chkActivo.Checked);
                SILPA.LogicaNegocio.Parametrizacion.Parametrizacion parametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
                parametrizacion.actualizarTiempoNotificacion(txtTiempo.Text, _intActivo);
                lblResultado.Text = "Información Guardada Con Éxito";

                //string strScript = "<script language='JavaScript'>" +
                //    "alert('Información Guardada Con Éxito.')" + "</script>";

                //Page.RegisterStartupScript("Emergente", strScript);

                //strScript = "<script language='JavaScript'>" + "HREF = javascript:history.go(-1);" + "</script>";
                ////Page.RegisterStartupScript("Emergente", strScript);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Atras", strScript, true);
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
