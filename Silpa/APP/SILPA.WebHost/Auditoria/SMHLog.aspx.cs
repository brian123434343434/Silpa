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
using SILPA.LogicaNegocio.LOG;
using SoftManagement.Log;

public partial class Auditoria_SMHLog : System.Web.UI.Page
{
    private SHMLog objSMHLog;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.Session.Timeout = 3;
            objSMHLog = new SHMLog();
            cboSeveridad.DataSource = objSMHLog.ConsultaSeveridad();
            cboSeveridad.DataValueField = "SEVERIDAD_ID";
            cboSeveridad.DataTextField = "SEVERIDAD";
            cboSeveridad.DataBind();
            cboSeveridad.Items.Insert(0,new ListItem("Seleccione...", "-1"));
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        grdResultado.DataSource = null;
        grdResultado.DataBind();
        if (ValidaDatos())
        {
            CargarGrilla();
        }
    }

    /// <summary>
    /// Metodo que le da el formato a la fecha aaaammdd para enviarlo al SP
    /// </summary>
    /// <param name="dtFecha"></param>
    /// <returns></returns>
    private String FormatoFecha(DateTime dtFecha)
    {
        String strDia;
        String strMes;
        String strAnio;
        String strFecha;

        strDia = dtFecha.Day.ToString();
        strMes = dtFecha.Month.ToString();
        strAnio = dtFecha.Year.ToString();

        if (Convert.ToInt32(strDia) < 10)
        {
            strDia = "0" + strDia;
        }

        if (Convert.ToInt32(strMes) < 10)
        {
            strMes = "0" + strMes;
        }

        strFecha = strAnio + strMes + strDia;
        return strFecha;        
    }

    /// <summary>
    /// Metodo que valida que la fecha ingresada en el texbox tenga formato de fecha
    /// </summary>
    /// <param name="strFecha"></param>
    /// <returns></returns>
    private bool ValidaFecha(string strFecha)
    {
        try
        {
            if (strFecha == "")
                return false;

            DateTime dateTime = DateTime.Parse(strFecha.ToString());
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    protected void cboSeveridad_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Metodo para la validacion de los datos 
    /// </summary>
    /// <returns></returns>
    public bool ValidaDatos()
    {
        if (txtFechaIni.Text.Trim() == "" && txtFechaFin.Text.Trim() == "" && txtMaquina.Text.Trim() == "" && txtUsuario.Text.Trim() == "" && cboSeveridad.SelectedValue == "-1")
        {
            lblMensaje.Text = "Debe seleccionar por lo menos un criterio de busqueda";
            UPauditoria.Update();
            return false;
        }
        if (txtFechaIni.Text.Trim() != "")
        {
            if (ValidaFecha(txtFechaIni.Text.Trim()) == false)
            {
                lblMensaje.Text = "Formato de fecha inicial incorrecto";
                UPauditoria.Update();
                return false;
            }
        }
        if (txtFechaFin.Text.Trim() != "")
        {
            if (ValidaFecha(txtFechaFin.Text.Trim()) == false)
            {
                lblMensaje.Text = "Formato de fecha final incorrecto";
                UPauditoria.Update();
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Metodo para la paginacion de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdResultado.PageIndex = e.NewPageIndex;
        CargarGrilla();
    }

    /// <summary>
    /// Metodo que realiza la consulta y carga la grilla con el resultado
    /// </summary>
    private void CargarGrilla()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarGrilla.Inicio");
            objSMHLog = new SHMLog();
            string strFechaIni = "";
            string strFechaFin = "";
            DataTable dtDatosLog;

            if (txtFechaIni.Text.Trim() != "")
            {
                strFechaIni = FormatoFecha(DateTime.Parse(this.txtFechaIni.Text.Trim()));
            }

            if (txtFechaFin.Text.Trim() != "")
            {
                strFechaFin = FormatoFecha(DateTime.Parse(this.txtFechaFin.Text.Trim()));
            }

            dtDatosLog = objSMHLog.ConsultaLog(strFechaIni, strFechaFin, this.txtUsuario.Text.Trim(), this.txtMaquina.Text.Trim(), Int32.Parse(cboSeveridad.SelectedValue));
            grdResultado.DataSource = dtDatosLog;
            grdResultado.DataBind();
            UPauditoria.Update();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarGrilla.Finalizo");
        }
    }
}
