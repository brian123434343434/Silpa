using SILPA.AccesoDatos.DAA;
using SILPA.AccesoDatos.Publicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PINES_ArchivosSolicitud : System.Web.UI.Page
{
    public List<DetalleDocumentoIdentity> _lstDocumentos;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarArchivos();
        }
    }

    private void CargarArchivos()
    {
        if (Request.QueryString["IdProcessInstance"] != string.Empty)
        {
            this._lstDocumentos = new List<DetalleDocumentoIdentity>();
             WSPQ03.WSPQ03 wsServicio = new WSPQ03.WSPQ03();
            wsServicio.Url = ConfigurationManager.AppSettings["WSPQ03"];
            wsServicio.Credentials = Credenciales();
            SolicitudDAAEIAIdentity _solicitud = new SolicitudDAAEIAIdentity();
            SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
            int IdProcessInstance = Convert.ToInt32(Request.QueryString["IdProcessInstance"]);
            _solicitud = _solicitudDalc.ObtenerSolicitud(null, IdProcessInstance, null);
            string ArchivosServicio = wsServicio.ObtenerDocumentosRadicacion(Convert.ToInt32(_solicitud.IdRadicacion));
            string[] Archivos = ArchivosServicio.Split(';');
            int i = 0;
            for (i = 0; i < Archivos.Length; i++)
            {
                if (Archivos[i] != "")
                {
                    DetalleDocumentoIdentity doc = new DetalleDocumentoIdentity();
                    doc.NombreArchivo = Archivos[i];
                    doc.Ubicacion = _solicitud.IdRadicacion.ToString();
                    this._lstDocumentos.Add(doc);
                }
            }
            this.grvArchivos.DataSource = _lstDocumentos;
            this.grvArchivos.DataBind();
        }
        
    }
    protected void grvArchivos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Descargar")
        {
            if (e.CommandArgument != null)
            {
                int _indice = Convert.ToInt32(e.CommandArgument);

                DataKey dk = this.grvArchivos.DataKeys[_indice];
                string Archivo = dk.Values[0].ToString();
                string _ubicacion = dk.Values[1].ToString();
                if (!_ubicacion.Contains(".aspx?"))
                {

                    WSPQ03.WSPQ03 wsServicio = new WSPQ03.WSPQ03();
                    wsServicio.Url = ConfigurationManager.AppSettings["WSPQ03"];
                    wsServicio.Credentials = Credenciales();
                    string rutaArchivo = wsServicio.ObtenerPathRadicacion(long.Parse(_ubicacion), Archivo);
                    if (rutaArchivo != null)
                    {
                        FileInfo field = new FileInfo(rutaArchivo);
                        if (field.Exists)
                        {
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + field.Name);
                            Response.AddHeader("Content-Length", field.Length.ToString());
                            Response.ContentType = "application/octet-stream";
                            Response.WriteFile(field.FullName);
                            Response.End();
                        }
                        else
                            Mensaje.MostrarMensaje(this, "El archivo no existe.");
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "CheckAlert('warning','No se pudo encontrar el archivo.','Adjuntar correspondencia');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(typeof(string), "EIA", "<script>ventana = window.open('" + _ubicacion + "','OPEN','width=1200,height=800,scrollbars=yes'); ventana.focus();</script>");
                }
            }
        }
    }
    public static System.Net.NetworkCredential Credenciales()
    {
        string user = System.Configuration.ConfigurationManager.AppSettings["usuario_servicio"].ToString();
        string pass = System.Configuration.ConfigurationManager.AppSettings["clave_servicio"].ToString();
        System.Net.NetworkCredential credencial = new System.Net.NetworkCredential(user, pass);
        return credencial;
    }
}