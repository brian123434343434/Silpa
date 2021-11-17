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
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;
using System.Collections.Generic;
using System.Linq;

public partial class AdquisicionDocumentos_ConsultaDocumentos : System.Web.UI.Page
{
    DataTable dtEntidad = new DataTable();
    protected ConsultaDocumentos objConsultaDocs;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg.Visible = false;

        if (!IsPostBack)
        {
            txtFechaDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFechaHasta.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            CargarEntidades();
            CargarDocumentos();      
        }
    }

    /// <summary>
    /// Carga las entidades externas
    /// </summary>
    private void CargarEntidades()
    {     
        //Realiza el procedimiento para cargar las entidades externas
        objConsultaDocs = new ConsultaDocumentos(); 
        dtEntidad = objConsultaDocs.Listar_DocumentosEntidades();  
        
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
    /// Carga los documentos
    /// </summary>
    private void CargarDocumentos() {

        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarDocumentos.Inicio");
            objConsultaDocs = new ConsultaDocumentos();

            DataTable dtDocumentos = null;
            long idUsuario = 0;
            try
            {
                idUsuario = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario);
            }
            catch (Exception ex)
            {
                //SMLog.Escribir(Severidad.Informativo,"ADQUISICION_DOCS:"+ ex.Message.ToString());
                //no existe el id usuario
                //idUsuario = -90
            }



            //dtDocumentos = objConsultaDocs.Listar_Documentos(
            //     idUsuario,
            //     (cboEntidad.SelectedValue != "0" ? int.Parse(cboEntidad.SelectedValue) : 0),
            //     (txtNumeroSilpa.Text != "" ? txtNumeroSilpa.Text : "0"),
            //     (txtFechaDesde.Text != "" ? DateTime.Parse(txtFechaDesde.Text) : DateTime.Now.AddMonths(-1)),
            //     (txtFechaHasta.Text != "" ? DateTime.Parse(txtFechaHasta.Text).AddDays(1) : DateTime.Now.AddDays(1))
            //);

            DateTime? fd = DateTime.Now.AddDays(1);
            DateTime? fh = DateTime.Now;

            if (!String.IsNullOrEmpty(txtFechaDesde.Text))
            {
                fd = DateTime.Parse(txtFechaDesde.Text);
            }

            if (!String.IsNullOrEmpty(txtFechaHasta.Text))
            {
                fh = DateTime.Parse(txtFechaHasta.Text);
            }

            //txtFechaDesde.Text != "" ? DateTime.Parse(txtFechaDesde.Text) : DateTime.Now.AddMonths(-1);

            dtDocumentos = objConsultaDocs.Listar_Documentos(
                idUsuario,
                (cboEntidad.SelectedValue != "0" ? int.Parse(cboEntidad.SelectedValue) : 0),
                (txtNumeroSilpa.Text != "" ? txtNumeroSilpa.Text : "0"),
                fd,
                fh
           );


            string filePath = "";
            ArrayList arrConsultaDocumento = new ArrayList();
            ConsultaDocumentoIdentity tmpConsultaDocumentoIdentity = null;

            if (dtDocumentos != null && dtDocumentos.Rows.Count > 0)
            {
                foreach (DataRow drDocumentos in dtDocumentos.Rows)
                {
                    filePath = (drDocumentos["PATH_DOCUMENTO"] == DBNull.Value ? "" : drDocumentos["PATH_DOCUMENTO"].ToString());

                    if (filePath != "")
                    {
                        if (!System.IO.Directory.Exists(filePath))
                            continue;

                        string[] arrFiles = System.IO.Directory.GetFiles(filePath);
                        List<ArchivoType> lstArchivos = new List<ArchivoType>();
                        foreach (var archivo in arrFiles)
	                    {
                            var infoField = new System.IO.FileInfo(archivo);
                            ArchivoType Archivotype = new ArchivoType { nombreArchivo = archivo.Substring(archivo.LastIndexOf("\\") + 1, archivo.Length - archivo.LastIndexOf("\\") - 1), fechaCreacionArchivoTicks = infoField.LastAccessTime.Ticks };
                            lstArchivos.Add(Archivotype);
	                    }

                        var lastField = lstArchivos.OrderBy(x => x.fechaCreacionArchivoTicks).First();

                        if (!lastField.nombreArchivo.Substring(lastField.nombreArchivo.LastIndexOf("\\") + 1, lastField.nombreArchivo.Length - lastField.nombreArchivo.LastIndexOf("\\") - 1).ToUpper().Contains("INFOADICIONAL"))
                        {
                            tmpConsultaDocumentoIdentity = new ConsultaDocumentoIdentity();
                            tmpConsultaDocumentoIdentity.Path = drDocumentos["PATH_DOCUMENTO"].ToString();
                            tmpConsultaDocumentoIdentity.FileName = lastField.nombreArchivo.Substring(lastField.nombreArchivo.LastIndexOf("\\") + 1, lastField.nombreArchivo.Length - lastField.nombreArchivo.LastIndexOf("\\") - 1);

                            if (drDocumentos["EEX_ID"] != null)
                                tmpConsultaDocumentoIdentity.EexId = int.Parse(drDocumentos["EEX_ID"].ToString());
                            if (drDocumentos["EEX_NOMBRE"] != null)
                                tmpConsultaDocumentoIdentity.EexNombre = drDocumentos["EEX_NOMBRE"].ToString();
                            if (drDocumentos["NUMERO_SILPA"] != null)
                                tmpConsultaDocumentoIdentity.NumeroSilpa = drDocumentos["NUMERO_SILPA"].ToString();
                            if (drDocumentos["FECHA"] != null)
                                tmpConsultaDocumentoIdentity.Fecha = System.DateTime.Parse(drDocumentos["FECHA"].ToString());
                            if (drDocumentos["SOL_ID_SOLICITANTE"] != null)
                                tmpConsultaDocumentoIdentity.IdSolicitante = int.Parse(drDocumentos["SOL_ID_SOLICITANTE"].ToString());

                            tmpConsultaDocumentoIdentity.UrlName = "ConsultaDocumentos.aspx";
                            arrConsultaDocumento.Add(tmpConsultaDocumentoIdentity);
                        }
                        
                        //foreach (string strFile in arrFiles)
                        //{
                        //    if (!strFile.Substring(strFile.LastIndexOf("\\") + 1, strFile.Length - strFile.LastIndexOf("\\") - 1).ToUpper().Contains("INFOADICIONAL") && !strFile.Substring(strFile.LastIndexOf("\\") + 1, strFile.Length - strFile.LastIndexOf("\\") - 1).ToUpper().Contains("rtf"))
                        //    {
                        //        tmpConsultaDocumentoIdentity = new ConsultaDocumentoIdentity();
                        //        tmpConsultaDocumentoIdentity.Path = drDocumentos["PATH_DOCUMENTO"].ToString();
                        //        tmpConsultaDocumentoIdentity.FileName = strFile.Substring(strFile.LastIndexOf("\\") + 1, strFile.Length - strFile.LastIndexOf("\\") - 1);

                        //        if (drDocumentos["EEX_ID"] != null)
                        //            tmpConsultaDocumentoIdentity.EexId = int.Parse(drDocumentos["EEX_ID"].ToString());
                        //        if (drDocumentos["EEX_NOMBRE"] != null)
                        //            tmpConsultaDocumentoIdentity.EexNombre = drDocumentos["EEX_NOMBRE"].ToString();
                        //        if (drDocumentos["NUMERO_SILPA"] != null)
                        //            tmpConsultaDocumentoIdentity.NumeroSilpa = drDocumentos["NUMERO_SILPA"].ToString();
                        //        if (drDocumentos["FECHA"] != null)
                        //            tmpConsultaDocumentoIdentity.Fecha = System.DateTime.Parse(drDocumentos["FECHA"].ToString());
                        //        if (drDocumentos["SOL_ID_SOLICITANTE"] != null)
                        //            tmpConsultaDocumentoIdentity.IdSolicitante = int.Parse(drDocumentos["SOL_ID_SOLICITANTE"].ToString());

                        //        tmpConsultaDocumentoIdentity.UrlName = "ConsultaDocumentos.aspx";
                        //        arrConsultaDocumento.Add(tmpConsultaDocumentoIdentity);
                        //    }
                        //}
                    }
                }

            }
            else
            {
                lblMsg.Text = "No se encontraron registros.";
                lblMsg.Visible = true;
            }

            grdDocumentos.DataSource = arrConsultaDocumento;
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
    /// Método para verificar si el quejoso es usuario registrado.
    /// </summary>
    /// <returns></returns>
    private bool ValidacionToken()
    {
        if (DatosSesion.Usuario == string.Empty)
        {
            return false;
        }
        string idUsuario = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

        Session["IDForToken"] = (object)idUsuario;

        Session["Usuario"] = Session["IDForToken"];

        SILPA.LogicaNegocio.Usuario.TokenUsuario token = new SILPA.LogicaNegocio.Usuario.TokenUsuario();

        Session["Token"] = token.TomarTokenUsuario(Int32.Parse(Session["IDForToken"].ToString()));

        using (WSValidacionToken.GattacaSecurityServices9000 servicio = new WSValidacionToken.GattacaSecurityServices9000())
        {
            servicio.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSValidacionToken");
            string mensaje = servicio.GetUserInfoByToken("Softmanagement", Session["Token"].ToString());
            //string mensaje = "Token valido";

            if (mensaje.IndexOf("Token invalido") > 0)
                return false;
        }
        return true;
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarDocumentos();
    }

    protected void grdDocumentos_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
         
        GridView source = (GridView)sender;
        Label lblFileName = (Label)source.Rows[int.Parse( e.CommandArgument.ToString() )].Cells[2].Controls[0];

        writeFile(lblFileName.Text);

    }

    private void writeFile(string file ){
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".writeFile.Inicio");
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();

            string filename = file.Substring(file.LastIndexOf("\\") + 1, file.Length - file.LastIndexOf("\\") - 1);


            Response.AddHeader("Content-Type", "application/x-download");
            Response.AddHeader("Content-disposition", "attachment; filename=\"" + filename + "\"");
            Response.AddHeader("Cache-Control", "private, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "public");

            Response.WriteFile(file);
            Response.End();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".writeFile.Finalizo");
        }
    }
    protected void txtFechaDesde_TextChanged(object sender, EventArgs e)
    {

    }
}
