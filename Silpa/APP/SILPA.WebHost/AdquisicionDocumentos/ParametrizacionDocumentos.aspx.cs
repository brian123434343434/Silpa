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
using SILPA.AccesoDatos.Parametrizacion;

public partial class AdquisicionDocumentos_ParametrizacionDocumentos : System.Web.UI.Page
{
    ParametrizacionDocumentos objAdquisicion = new ParametrizacionDocumentos();
    DataTable dtAquisicion = new DataTable();
    DataTable dtEntidad = new DataTable();
    DataTable dtParametrizacion = new DataTable();  

    protected void Page_Load(object sender, EventArgs e)
    {
        Mensaje.LimpiarMensaje(this);
       
        if (!IsPostBack)
        {
            grdDocumentos.AllowPaging = true;
            grdDocumentos.PageSize = 5;

            cargarAdquisicion();
            CargarEntidades();
            CargarDocumentos();                        
          }
    }

    /// <summary>
    /// Boton Guardar el registro de los documentos parametrizados
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cmdGuardar_Click.Inicio");
            ParametrizacionDocumentos objParametrizacion = new ParametrizacionDocumentos();

            if (cboTipoAdquisicion.SelectedIndex != 0)
            {
                if (cboEntidad.SelectedIndex != 0)
                {

                    //Reliza el procedimiento para ingresar los datos de parametrización
                    if (
                        objAdquisicion.crearParametrizacionDocumento(txtNombreDocumento.Text.Trim(), txtEnlace.Text.Trim(),
                        int.Parse(cboTipoAdquisicion.SelectedValue), int.Parse(cboEntidad.SelectedValue), txtCodigoProceso.Text)
                    )
                    {
                        Mensaje.MostrarMensaje(this, "La parametrización para el documento fue ingresada correctamente");
                        CargarDocumentos();
                        GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
                        CrearLogAuditoria.Insertar("ADQUISICIÓN DOCUMENTOS", 1, "Se almaceno parametrización documentos");

                    }
                    else
                    {
                        Mensaje.MostrarMensaje(this, "No se logro ingresar el registro del documento, intente nuevamente");
                    }
                }
                else
                {
                    Mensaje.MostrarMensaje(this, "Debe seleccionar una Entidad Externa");
                }
            }
            else
            {
                Mensaje.MostrarMensaje(this, "Debe seleccionar un tipo de adquisición");
            }
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cmdGuardar_Click.Finalizo");
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
            int? intAdquisicion = null;
            int? intEmpresa = null;

            if (cboTipoAdquisicion.SelectedIndex != 0)
                intAdquisicion = int.Parse(cboTipoAdquisicion.SelectedValue);
            else
                intAdquisicion = null;

            if (cboEntidad.SelectedIndex != 0)
                intEmpresa = int.Parse(cboEntidad.SelectedValue);
            else
                intEmpresa = null;

            //Realiza el procedimiento para listar los documentos parametrizados
            dtParametrizacion = objAdquisicion.ListarParametrizacionDocumento(intAdquisicion, intEmpresa);

            grdDocumentos.DataSource = dtParametrizacion;
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

    /// <summary>
    /// Cargar las lista de tipos de adquisición
    /// </summary>
    public void cargarAdquisicion()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cargarAdquisicion.Inicio");
            //Ejecuta el procedimiento para cargar los tipo de adquisición
            dtAquisicion = objAdquisicion.ListarTipoAdquisicion();

            if (dtAquisicion != null)
                if (dtAquisicion.Rows.Count > 0)
                {

                    cboTipoAdquisicion.DataSource = dtAquisicion;
                    cboTipoAdquisicion.DataTextField = "ADQ_DESCRIPCION";
                    cboTipoAdquisicion.DataValueField = "ADQ_ID";
                    cboTipoAdquisicion.DataBind();
                    cboTipoAdquisicion.Items.Insert(0, new ListItem("Seleccione...", "0"));

                    cboTipoAdquisicionEdit.DataSource = dtAquisicion;
                    cboTipoAdquisicionEdit.DataTextField = "ADQ_DESCRIPCION";
                    cboTipoAdquisicionEdit.DataValueField = "ADQ_ID";
                    cboTipoAdquisicionEdit.DataBind();
                    cboTipoAdquisicionEdit.Items.Insert(0, new ListItem("Seleccione...", "0"));

                }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cargarAdquisicion.Finalizo");
        }
    }

    /// <summary>
    /// Cargar la lista de entidades externas
    /// </summary>
    public void CargarEntidades()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarEntidades.Inicio");
            //Ejectua el procedimiento para cargar la lista de entidades externas
            dtEntidad = objAdquisicion.ListarEntidadesExternas();

            if (dtEntidad != null)
                if (dtEntidad.Rows.Count > 0)
                {

                    cboEntidad.DataSource = dtEntidad;
                    cboEntidad.DataTextField = "EEX_NOMBRE";
                    cboEntidad.DataValueField = "EEX_ID";
                    cboEntidad.DataBind();
                    cboEntidad.Items.Insert(0, new ListItem("Seleccione...", "0"));

                    cboEntidadEdit.DataSource = dtEntidad;
                    cboEntidadEdit.DataTextField = "EEX_NOMBRE";
                    cboEntidadEdit.DataValueField = "EEX_ID";
                    cboEntidadEdit.DataBind();
                    cboEntidadEdit.Items.Insert(0, new ListItem("Seleccione...", "0"));

                }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarEntidades.Finalizo");
        }
    }

    protected void cboEntidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDocumentos(); 

    }

    protected void cboTipoAdquisicion_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDocumentos(); 
    }

    protected void grdDocumentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocumentos.PageIndex = e.NewPageIndex;
        CargarDocumentos();
        grdDocumentos.DataBind();  
    }

    private void CargarEdicionDocumento(int DocID)
    {

        ParametrizacionDocumentos objParametrizacion = new ParametrizacionDocumentos();
        ParametrizacionDocumentoEntity objParametrizacionDocumentoEntity = objParametrizacion.ConsultarDocumentoXDocID(DocID);
        this.hdDocID.Value = DocID.ToString();
        this.txtNombreEdit.Text = objParametrizacionDocumentoEntity.DocNombre;
        this.cboEntidadEdit.SelectedValue = objParametrizacionDocumentoEntity.EntidadExternaID.ToString();
        this.txtEnlaceEdit.Text = objParametrizacionDocumentoEntity.EnlaceAplicativo;
        this.cboTipoAdquisicionEdit.SelectedValue = objParametrizacionDocumentoEntity.TipoAdquisicionID.ToString();
        this.txtCodigoProceosEdit.Text = objParametrizacionDocumentoEntity.CodigoProceso;
        this.mpeEdicionDocumento.Show();
    }
    protected void grdDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Editar":
                int vDocId = Convert.ToInt32(e.CommandArgument);
                CargarEdicionDocumento(vDocId);
                break;
        }
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {
                ParametrizacionDocumentos objParametrizacion = new ParametrizacionDocumentos();
                ParametrizacionDocumentoEntity objParametrizacionDocumentoEntity = new ParametrizacionDocumentoEntity();
                objParametrizacionDocumentoEntity.DocID = Convert.ToInt32(this.hdDocID.Value);
                objParametrizacionDocumentoEntity.DocNombre = this.txtNombreEdit.Text;
                objParametrizacionDocumentoEntity.EntidadExternaID = Convert.ToInt32(this.cboEntidadEdit.SelectedValue);
                objParametrizacionDocumentoEntity.EnlaceAplicativo = this.txtEnlaceEdit.Text;
                objParametrizacionDocumentoEntity.TipoAdquisicionID = Convert.ToInt32(this.cboTipoAdquisicionEdit.SelectedValue);
                objParametrizacionDocumentoEntity.CodigoProceso = this.txtCodigoProceosEdit.Text;
                objParametrizacionDocumentoEntity.ImagenUrl = this.fluArchivoImagenEdit.FileName;
                objParametrizacion.ActualizarDocumento(objParametrizacionDocumentoEntity);
                this.fluArchivoImagenEdit.SaveAs(Server.MapPath("~/App_Themes/Img/"+objParametrizacionDocumentoEntity.ImagenUrl));
                CargarDocumentos(); 
            }
            catch (Exception ex)
            {
                Mensaje.MostrarMensaje(this, ex.ToString());
            }
        }
        else
        {
            this.mpeEdicionDocumento.Show();
        }
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
