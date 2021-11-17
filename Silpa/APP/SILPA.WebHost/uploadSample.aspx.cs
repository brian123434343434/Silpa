using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uploadSample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Files.Count == 1)
        {
            int iMaxFileSize = 20971520; //Tamaño de archivo, no más de 20MB en este ejemplo
            if (Request.ContentLength > iMaxFileSize)
                lblMessage.Text = "Error, el archivo no puede superar 20MB";
            else
            {
                //proceso para guardar el archivo
                //...
                Response.Write("Éxito");
                Response.End();
            }

        }


    }
    protected void btnUploadComplted_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "Proceso de Carga de Archivo completado con éxito";
    }
}
