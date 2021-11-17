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
using System.Collections.Generic;
using SILPA.AccesoDatos.Publicacion;

public partial class Informacion_DetalleDocumentos : System.Web.UI.Page
{
    public List<DetalleDocumentoIdentity> ListaDocumentos;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // la lista proveniente de publicaciones
        if (Session["ListaDocumentos"] != null)
        {
            this.ListaDocumentos = (List<DetalleDocumentoIdentity>)Session["ListaDocumentos"];
            this.grdDetalleDocumento.DataSource = this.ListaDocumentos;
            this.grdDetalleDocumento.DataBind();

            if (this.ListaDocumentos.Count > 0)
            {
                this.lblExisteDocumento.Visible = false;
            }
            else
            {
                this.lblExisteDocumento.Visible = true;
            }
        }
        else {
            this.lblExisteDocumento.Visible = true;
        }

        
    }
    protected void grdDetalleDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Descargar")
        {
            int _indice = Convert.ToInt32(e.CommandArgument);
            //GridViewRow _fila = grdPublicaciones.Rows[_indice];

            DataKey dk = this.grdDetalleDocumento.DataKeys[_indice];

            //string idPublicaicon = dk.Values[0].ToString();
            string _nombreArchivo = dk.Values[1].ToString();

            //System.IO.FileInfo targetFile = new System.IO.FileInfo(Server.MapPath(_nombreArchivo));
            System.IO.FileInfo targetFile = new System.IO.FileInfo(_nombreArchivo);
            this.Response.Clear();
            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
            this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
            this.Response.ContentType = "application/octet-stream";
            this.Response.ContentType = "application/base64";
            this.Response.WriteFile(targetFile.FullName);
            this.Response.WriteFile(_nombreArchivo);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //string strScript = "<script language='JavaScript'>" + "location.href = '" + "../Informacion/Publicaciones.aspx" + "'" +"</script>";
        //Page.RegisterStartupScript("PopupScript", strScript);

        string _respuestaEnvio = "<script language='JavaScript'>" +"window.close()</script>";
        Page.RegisterStartupScript("PopupScript", _respuestaEnvio);




    }
}
