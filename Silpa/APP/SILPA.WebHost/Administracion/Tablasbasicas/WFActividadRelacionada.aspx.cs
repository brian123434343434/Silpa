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

public partial class Administracion_Tablasbasicas_WFActividadRelacionada : System.Web.UI.Page
{
    private SILPA.LogicaNegocio.AdmTablasBasicas.WorkFlowActividadRelacionada objTablasBasicas;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.WorkFlowActividadRelacionada();
            cboActividadSilpa.DataSource = objTablasBasicas.Listar_Actividad_Silpa();
            cboActividadSilpa.DataValueField = "ACTIVIDAD_SILPA_ID";
            cboActividadSilpa.DataTextField = "ACTIVIDAD_SILPA_NOMBRE";
            cboActividadSilpa.DataBind();

            CargarActividades(); 
        }
        
    }

    private void CargarActividades()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarActividades.Inicio");
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.WorkFlowActividadRelacionada();
            lstRelacionadas.DataSource = objTablasBasicas.Listar_Actividad_Relacionada(Convert.ToInt32(cboActividadSilpa.SelectedValue));
            lstRelacionadas.DataValueField = "ID";
            lstRelacionadas.DataTextField = "Activity";
            lstRelacionadas.DataBind();

            lstNoRelacionadas.DataSource = objTablasBasicas.Listar_Actividad_NoRelacionada(Convert.ToInt32(cboActividadSilpa.SelectedValue));
            lstNoRelacionadas.DataValueField = "ID";
            lstNoRelacionadas.DataTextField = "Activity";
            lstNoRelacionadas.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarActividades.Finalizo");
        }
    }

    protected void cboActividadSilpa_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarActividades();
    }

    protected void btnQuitaActividad_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnQuitaActividad_Click.Inicio");
            if (lstRelacionadas.SelectedValue != "")
            {
                objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.WorkFlowActividadRelacionada();
                objTablasBasicas.Eliminar_Actividad_Relacionada(Convert.ToInt32(lstRelacionadas.SelectedValue), Convert.ToInt32(cboActividadSilpa.SelectedValue));
                CargarActividades();
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnQuitaActividad_Click.Finalizo");
        }
    }

    protected void btnAddActividad_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAddActividad_Click.Inicio");
            if (lstNoRelacionadas.SelectedValue != "")
            {
                objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.WorkFlowActividadRelacionada();
                objTablasBasicas.Insertar_Actividad_Relacionada(Convert.ToInt32(lstNoRelacionadas.SelectedValue), Convert.ToInt32(cboActividadSilpa.SelectedValue));
                CargarActividades();
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAddActividad_Click.Finalizo");
        }
    }

    protected void lstNoRelacionadas_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstNoRelacionadas.ToolTip = Convert.ToString(lstNoRelacionadas.SelectedItem.Text);     
    }
    
    protected void lstRelacionadas_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstRelacionadas.ToolTip = Convert.ToString(lstRelacionadas.SelectedItem.Text);      
    }
}
