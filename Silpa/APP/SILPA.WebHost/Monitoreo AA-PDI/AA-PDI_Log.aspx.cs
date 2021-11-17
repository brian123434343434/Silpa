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
using SILPA.LogicaNegocio.Log_AA_PDI;
using SILPA.LogicaNegocio.LOG;
using SoftManagement.Log;
using System.Text;
using System.IO;

public partial class Monitoreo_AA_PDI_AA_PDI_Log : System.Web.UI.Page
{
    private SHMLog objSMHLog;
    
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!Page.IsPostBack)
        {
            objSMHLog = new SHMLog();
            cboSeveridad.DataSource = objSMHLog.ConsultaSeveridad();
            cboSeveridad.DataValueField = "SEVERIDAD_ID";
            cboSeveridad.DataTextField = "SEVERIDAD";
            cboSeveridad.DataBind();
            cboSeveridad.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            SILPA.LogicaNegocio.Generico.Listas _listas = new SILPA.LogicaNegocio.Generico.Listas();
            this.cboAutoridadAmbiental.DataSource = _listas.ListarAutoridades(null);
            this.cboAutoridadAmbiental.DataValueField = "AUT_ID";
            this.cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
            this.cboAutoridadAmbiental.DataBind();
            this.cboAutoridadAmbiental.Items.Insert(0, new ListItem("Seleccione...", "-1"));


            SILPA.LogicaNegocio.AdmTablasBasicas.WSB_Servicios objWS = new SILPA.LogicaNegocio.AdmTablasBasicas.WSB_Servicios();
            this.cobNombreWS.DataSource=objWS.Listar_Servicios("");
            this.cobNombreWS.DataValueField = "WSB_ID";
            this.cobNombreWS.DataTextField = "WSB_NOMBRE_SERVICIO";
            this.cobNombreWS.DataBind();
            this.cobNombreWS.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            SILPA.LogicaNegocio.AdmTablasBasicas.WSB_Metodo objMetodo = new SILPA.LogicaNegocio.AdmTablasBasicas.WSB_Metodo();
            this.cobMetodo.DataSource = objMetodo.Listar_Metodos("");
            this.cobMetodo.DataValueField = "WSB_ID_METODO";
            this.cobMetodo.DataTextField = "WSB_NOMBRE_SERVICIO";
            this.cobMetodo.DataBind();
            this.cobMetodo.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            pnConsulta.Visible = false;
        }
  
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ConsultarLog();
        pnConsulta.Visible = true;
        grdResultado.DataSource = ConsultarLog();
        grdResultado.DataBind();
    }

    protected void grdResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Cambiar la pagina con la información
        this.grdResultado.PageIndex = e.NewPageIndex;
        grdResultado.DataSource = ConsultarLog();
        grdResultado.DataBind();
    }

    private DataTable ConsultarLog()
    {
        DataTable _dtConsulta;
        MOH_Log_AA_PDI objSMHLog = new MOH_Log_AA_PDI();
        int iIdPadre = -1;

        if (txtIdPadre.Text.Trim() != "")
            iIdPadre = int.Parse(txtIdPadre.Text.Trim());

        string sNomWS = "";

        if (cobNombreWS.SelectedValue != "-1")
            sNomWS = cobNombreWS.SelectedItem.Text.Trim();

        string sMetodo = "";

        if (cobMetodo.SelectedValue != "-1")
            sMetodo = cobMetodo.SelectedItem.Text.Trim();


        _dtConsulta = objSMHLog.ConsultaLog(txtFechaIni.Text.Trim(), txtFechaFin.Text.Trim(), sNomWS, sMetodo, int.Parse(cboSeveridad.SelectedValue), txtMensaje.Text.Trim(), txtNoVital.Text.Trim(), int.Parse(cboAutoridadAmbiental.SelectedValue), iIdPadre);
        return _dtConsulta;
    }

    protected void imb_exporta_excel_Click(object sender, ImageClickEventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page pagina  = new Page();
        GridView _grdTmpResiultado = new GridView();
        _grdTmpResiultado.EnableViewState = false;
        _grdTmpResiultado.AllowPaging = false;
        _grdTmpResiultado.DataSource = ConsultarLog();
        _grdTmpResiultado.DataBind();

        HtmlForm form = new HtmlForm();
        pagina.Controls.Add(form);
        form.Controls.Add(_grdTmpResiultado);
        pagina.RenderControl(htw);
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }
}
