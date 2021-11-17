using SILPA.AccesoDatos.RepositorioArchivos;
using SILPA.LogicaNegocio.RepositorioArchivos;
using Subgurim.Controles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RepositorioArchivos_Archivos : System.Web.UI.Page
{
    public string TipoArchivoID { get { return (string)ViewState["TipoArchivoID"]; } set { ViewState["TipoArchivoID"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.managePost();
        if (!IsPostBack)
        {
            //Session["Usuario"] = 2424;
            if (ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                CargarPagina();
                ConsultaArchivosUsuario();
            }
        }
    }

    private void CargarPagina()
    {
        RepositorioArchivo clsRepositorioArchivo = new RepositorioArchivo();
        Utilidades.LlenarComboTabla(clsRepositorioArchivo.TablaFormularios(), cboFormulario, "NOMBRE_FORMULARIO", "FORM_ID",true);
        Utilidades.LlenarComboVacio(cboTipoArchivo);
    }
    private void managePost()
    {
        HttpPostedFileAJAX fileuplaod = new HttpPostedFileAJAX();
        RepositorioArchivo clsRepositorioArchivo = new RepositorioArchivo();
        if (fuplArchivoUsuario.IsPosting)
        {
            if (Session["TipoArchivoID"] != null)
            {
                fileuplaod = fuplArchivoUsuario.PostedFile;
                Archivo objArchivo = new Archivo();
                objArchivo.NombreArchivo = fileuplaod.FileName;
                objArchivo.TipoArchivo = Convert.ToInt32(Session["TipoArchivoID"]);
                objArchivo.DescTipoArchivo = Session["DescTipoArchivo"].ToString();
                objArchivo.Ubicacion = string.Format("RepoUsuario/{0}/", Session["Usuario"].ToString());
                objArchivo.UsuarioID = Convert.ToInt32(Session["Usuario"]);
                objArchivo.Tamaño = (fileuplaod.ContentLength / 1024);
                if (!Directory.Exists(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + objArchivo.Ubicacion))
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + objArchivo.Ubicacion);
                fuplArchivoUsuario.Save(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + objArchivo.Ubicacion, objArchivo.NombreArchivo);
                clsRepositorioArchivo.InsertarArchivo(objArchivo);
            }
            else
            {
                Validate("Archivo");
            }
        }
        if (fuplArchivoUsuario.IsDeleting)
        {
            HttpPostedFileAJAX archivo = fuplArchivoUsuario.historial.First();
            clsRepositorioArchivo.EliminarArchivo(new Archivo { NombreArchivo = archivo.FileName, UsuarioID = Convert.ToInt32(Session["Usuario"])});
        }
    }

    private void ConsultaArchivosUsuario()
    {
        RepositorioArchivo clsRepositorioArchivo = new RepositorioArchivo();
        var LstArchivos = clsRepositorioArchivo.ConsultaArchivos(null, null, Convert.ToInt32(Session["Usuario"]));
        this.grvArchivosUsuario.DataSource = LstArchivos;
        this.grvArchivosUsuario.DataBind();
    }
    protected void btnNuevoArchivo_Click(object sender, EventArgs e)
    {
        this.mpeArchivo.Show();
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        this.cboTipoArchivo.SelectedValue = "";
        this.fuplArchivoUsuario.Reset();
        Session["TipoArchivoID"] = null;
        Session["DescTipoArchivo"] = null;
        ConsultaArchivosUsuario();
        this.mpeArchivo.Hide();
    }
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
    protected void cboTipoArchivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["TipoArchivoID"] = this.cboTipoArchivo.SelectedValue;
        Session["DescTipoArchivo"] = this.cboTipoArchivo.SelectedItem.Text;
        this.fuplArchivoUsuario.Reset();
        this.mpeArchivo.Show();
    }
    protected void grvArchivosUsuario_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = grvArchivosUsuario.Rows[e.RowIndex];
        int FielID = Convert.ToInt32(row.Cells[0].Text);
        string archivo = ((LinkButton)row.FindControl("lnkbVer")).Text;
        string ubicacion = row.Cells[2].Text;
        RepositorioArchivo clsRepositorioArchivo = new RepositorioArchivo();
        clsRepositorioArchivo.EliminarArchivo(new Archivo { FileID = FielID, NombreArchivo = archivo, UsuarioID = Convert.ToInt32(Session["Usuario"]) });
        File.Delete(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + ubicacion + archivo);
        ConsultaArchivosUsuario();
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Utilidades.LlenarComboVacio(cboTipoArchivo);
        this.cboFormulario.SelectedValue = "";
        this.fuplArchivoUsuario.Reset();
        Session["TipoArchivoID"] = null;
        Session["DescTipoArchivo"] = null;
        ConsultaArchivosUsuario();
        this.mpeArchivo.Hide();
    }
    protected void grvArchivosUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Descargar")
        {
            GridViewRow row = grvArchivosUsuario.Rows[Convert.ToInt32(e.CommandArgument)];
            string archivo = ((LinkButton)row.FindControl("lnkbVer")).Text;
            string ubicacion = row.Cells[2].Text;
            Utilidades clsUtilidades = new Utilidades();
            clsUtilidades.DescargarArchivo(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + ubicacion, archivo, this.Response);
        }
    }
    protected void cboFormulario_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboFormulario.SelectedValue != "")
        {
            RepositorioArchivo clsRepositorioArchivo = new RepositorioArchivo();
            Utilidades.LlenarComboTabla(clsRepositorioArchivo.TablaTipoArchivoFormulario(Convert.ToInt32(this.cboFormulario.SelectedValue)), this.cboTipoArchivo, "TIPO", "ID", true);
        }
        else
        {
            Utilidades.LlenarComboVacio(cboTipoArchivo);
        }
    }
}