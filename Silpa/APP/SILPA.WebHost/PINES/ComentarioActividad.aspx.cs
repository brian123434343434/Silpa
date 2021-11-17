using SILPA.AccesoDatos.PINES;
using SILPA.LogicaNegocio.PINES;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PINES_ComentarioActividad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarComentarioActividad();
        }
    }

    private void CargarComentarioActividad()
    {
        if (Request.QueryString["IdActivityInstance"] != string.Empty)
        {
            ComentariosActividad clsComentariosActividad = new ComentariosActividad();
            List<ComentarioActividadIdentity> ListComentario = clsComentariosActividad.ListaComentarioActividad(new ComentarioActividadIdentity { IDActivityInstance = Convert.ToInt32(Request.QueryString["IdActivityInstance"]) });
            this.grvComentarios.DataSource = ListComentario;
            this.grvComentarios.DataBind();
        }
    }
    protected void grvComentarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "VerArchivo":
                string rutarchivoPINES = ConfigurationManager.AppSettings["FILE_TRAFFIC"] + ConfigurationManager.AppSettings["ARCHIVOS_PINES"];
                FileInfo Archivo = new FileInfo(rutarchivoPINES+e.CommandArgument);
                if (Archivo.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Archivo.Name);
                    Response.AddHeader("Content-Length", Archivo.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(Archivo.FullName);
                    Response.End();
                }
                else
                     Mensaje.MostrarMensaje(this, "El archivo no existe.");
                break;
        }
    }
}