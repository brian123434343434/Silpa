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

public partial class LicenciasAmbientales_LPA_05_Anexar_Documentacion_Soporte_Solicitante : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool validarControles()
    {
        bool blnResult = true;

        foreach (Control currentControl in Page.Controls)
        {
            foreach (Control child in currentControl.Controls)
            {
                foreach (Control schild in child.Controls)
                {
                    foreach (Control sschild in schild.Controls)
                    {
                        string tipo = child.UniqueID;
                        if (sschild is CheckBox)
                        {
                            if (((CheckBox)(sschild)).Checked == false)
                            {
                                blnResult = false;
                            }
                        }
                    }
                }

            }
        }
        return blnResult;
    }

    protected void BTN_GUARDAR_Click(object sender, EventArgs e)
    {
        //this.Dispose();

        //string Mensaje = "Documentos agregados satisfactoriamente.";
        //    ClientScript.RegisterStartupScript(this.GetType(), "alert",
        //    "<script>alert('" + Mensaje + "');</script>");
            //return;
        //    Response.Redirect("/Silpa/Default2.aspx");


        if (FUP_ARCHIVO.FileName == string.Empty || this.FileUpload2.FileName == string.Empty)
        {
            string Mensaje = "Debe agregar todos los documentos de la lista.";
            ClientScript.RegisterStartupScript(this.GetType(), "alert",
            "<script>alert('" + Mensaje + "');</script>");
        }
        else 
        {
            //this.CheckBox2.Checked = true;
            //this.CHK_DOC.Checked = true;

            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("LICENCIASAMBIENTALES - ANEXARDOCUMENTOS", 1, "Se almaceno Documento Anexo");

            string Mensaje = "Documentos agregados satisfactoriamente.";
            ClientScript.RegisterStartupScript(this.GetType(), "alert",
            "<script>alert('" + Mensaje + "');</script>");
        }

       // 1053769215

        //if (validarControles())
        //{
        //   // Response.Redirect("LPA_Diligenciar_Datos_DAA_Sec_II.aspx");
        //    string Mensaje = "Documentos agregados satisfactoriamente.";
        //    ClientScript.RegisterStartupScript(this.GetType(), "alert",
        //    "<script>alert('" + Mensaje + "');</script>");
        //}
        //else
        //{
        //    string Mensaje = "Debe diligenciar todos los documentos.";
        //    ClientScript.RegisterStartupScript(this.GetType(), "alert",
        //    "<script>alert('" + Mensaje + "');</script>");
        //}

    }
    protected void FUP_ARCHIVO_Load(object sender, EventArgs e)
    {
        this.CheckBox2.Checked = false;
        this.CHK_DOC.Checked = false;
    }
    protected void FUP_ARCHIVO_Init(object sender, EventArgs e)
    {
        //this.CHK_DOC.Checked = false;
    }
    protected void FileUpload2_Init(object sender, EventArgs e)
    {
        //this.CheckBox2.Checked = false;
    }
    protected void FileUpload2_Load(object sender, EventArgs e)
    {
        //this.CheckBox2.Checked = false;
    }
    protected void FUP_ARCHIVO_DataBinding(object sender, EventArgs e)
    {
        //this.CHK_DOC.Checked = true;
    }
}
