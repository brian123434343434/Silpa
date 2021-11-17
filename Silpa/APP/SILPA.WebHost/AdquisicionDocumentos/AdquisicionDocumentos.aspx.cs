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
using SILPA.LogicaNegocio.Parametrizacion;
using SoftManagement.Log;

public partial class AdquisicionDocumentos_AdquisicionDocumentos : System.Web.UI.Page
{
    ParametrizacionDocumentos objAdquisicion = new ParametrizacionDocumentos();
    DataTable dtEntidad = new DataTable();
    DataTable dtAdquisicionDocumento = new DataTable(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            grdDocumentos.AllowPaging = true;
            grdDocumentos.PageSize = 5;

            CargarEntidades();
            CargarDocumentos(); 
        }

    }

    /// <summary>
    /// Carga las entidades externas
    /// </summary>
    public void CargarEntidades()
    {      
            //Realiza el procedimiento para cargar las entidades externas
            dtEntidad = objAdquisicion.ListarEntidadesExternas();
                
        if (dtEntidad != null)
            if (dtEntidad.Rows.Count > 0)
            {
                cboEntidad.DataSource = dtEntidad;
                cboEntidad.DataTextField = "EEX_NOMBRE";
                cboEntidad.DataValueField = "EEX_ID";
                cboEntidad.DataBind();
                cboEntidad.Items.Insert(0, new ListItem("Seleccione...", "0"));
            }
    }

    /// <summary>
    /// Cargar la lista de documentos parametriazados 
    /// </summary>
    public void CargarDocumentos()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarDocumentos.Inicio");
            int? intEmpresa = null;

            if (cboEntidad.SelectedIndex != 0)
                intEmpresa = int.Parse(cboEntidad.SelectedValue);
            else
                intEmpresa = null;


            //Realiza el procedimiento para listar los documentos parametrizados
            dtAdquisicionDocumento = objAdquisicion.ListarParametrizacionDocumento(null, intEmpresa);

            grdDocumentos.DataSource = dtAdquisicionDocumento;
            grdDocumentos.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarDocumentos.Finalizo");
        }
      
    }

    protected void cboEntidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDocumentos(); 
    }

    protected void grdDocumentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocumentos.PageIndex = e.NewPageIndex;
        CargarDocumentos();
        grdDocumentos.DataBind();
    }
    protected void grdDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image imgDocumento = (Image)e.Row.FindControl("imgDocumento");
            HiddenField hdfImagen = (HiddenField)e.Row.FindControl("hdfImagen");
            if (hdfImagen.Value != string.Empty)
                imgDocumento.ImageUrl = "~/App_Themes/Img/" + hdfImagen.Value;
        }
    }
}
