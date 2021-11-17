using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using SILPA.Servicios;
using SILPA.Comun;
using SILPA.LogicaNegocio.AudienciaPublica;
using SILPA.LogicaNegocio.ICorreo;
using SILPA.AccesoDatos.AudienciaPublica;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;

public partial class ListarEdictoInscripcionAudienciaPublica : System.Web.UI.Page
{
    public List<DetalleDocumento> lstDocumentos;
    private Configuracion objConfiguracion;
    private string numeroSilpa;

    protected void Page_Load(object sender, EventArgs e)
    {
        string ubicacion = Request.QueryString["ubi"];
        CargarDocumentos(ubicacion);
        this.numeroSilpa = Request.QueryString["nsilpa"];
    }


    public void CargarDocumentos(string pathDocumento) 
    {
        //this.objConfiguracion =  new Configuracion();
        this.lstDocumentos = new List<DetalleDocumento>();

        string[] ListaArchivos = null;

        if (pathDocumento != string.Empty)
        {
            if (System.IO.Directory.Exists(pathDocumento))
            {
                ListaArchivos = System.IO.Directory.GetFiles(pathDocumento);
            }
        }


        if (ListaArchivos != null)
        {
            if (ListaArchivos.Length > 0)
            {
                foreach (string str in ListaArchivos)
                {
                    DetalleDocumento doc = new DetalleDocumento();

                    doc.NombreArchivo = System.IO.Path.GetFileName(str);
                    doc.Ubicacion = str;
                    this.lstDocumentos.Add(doc);
                }
            }
        }


        this.grdVerDocumentos.DataSource = this.lstDocumentos;
        this.grdVerDocumentos.DataBind();

    }

    protected void grdPublicaciones_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Descargar")
        {
            if (e.CommandArgument != null)
            {
                int _indice = Convert.ToInt32(e.CommandArgument);

                DataKey dk = this.grdVerDocumentos.DataKeys[_indice];

                string _ubicacion = dk.Values[1].ToString();

                System.IO.FileInfo targetFile = new System.IO.FileInfo(_ubicacion);
                this.Response.Clear();
                this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                this.Response.ContentType = "application/octet-stream";
                this.Response.ContentType = "application/base64";
                this.Response.WriteFile(targetFile.FullName);
                this.Response.WriteFile(_ubicacion);
            }
        }
    }



    protected void grdPublicaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdVerDocumentos.PageIndex = e.NewPageIndex;
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect("DetallesInscripcionAudienciaPublica.aspx?Numero_Silpa="+this.numeroSilpa);
    }
}


    /// <summary>
    /// Clase que guarda los documentos
    /// </summary>
    public class DetalleDocumento 
    {
        private string nombreArchivoField;
        public string NombreArchivo 
        {
            get { return nombreArchivoField; }
            set { nombreArchivoField = value; }
        }
        
        private string ubicacionField;
        public string Ubicacion 
        {
            get { return ubicacionField; }
            set { ubicacionField = value; }
        }

    }