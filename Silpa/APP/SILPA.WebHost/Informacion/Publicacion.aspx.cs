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
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using SoftManagement.Log;

public partial class Informacion_Publicacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)

        llenarLista();
        this.txtFechaPub.Text = System.DateTime.Now.ToLocalTime().ToShortDateString().ToString();

    }

    private void llenarLista()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".llenarLista.Inicio");
            DbCommand command;

            Database db = DatabaseFactory.CreateDatabase();

            command = db.GetStoredProcCommand("GEN_LISTAR_ACTOS_ADMIN");

            DataSet ds = db.ExecuteDataSet(command);

            this.ddlActoAdmin.DataSource = ds;
            ddlActoAdmin.DataValueField = "ID_ACTO_ADMINISTRATIVO";
            ddlActoAdmin.DataTextField = "DESCRIPCION";
            ddlActoAdmin.DataBind();
            ddlActoAdmin.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".llenarLista.Finalizo");
        }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAceptar_Click.Inicio");
            DbCommand command;

            Database db = DatabaseFactory.CreateDatabase();

            command = db.GetStoredProcCommand("GEN_ADICIONAR_PUBLICACION");

            string path = Request.PhysicalApplicationPath;
            string nomArchivo = fupArchivo.FileName;
            string documentos = "documentos\\publicaciones";
            fupArchivo.SaveAs(path + documentos + "\\" + nomArchivo);
            db.AddInParameter(command, "TITULO_PUB", DbType.String, this.txtTituloPublicacion.Text);
            db.AddInParameter(command, "TIPO_PUB", DbType.Int32, 1);
            db.AddInParameter(command, "TEXTO_PUB", DbType.String, this.txtTextoPublicacion.Text);
            db.AddInParameter(command, "ADJUNTO", DbType.String, this.fupArchivo.FileName);
            db.AddInParameter(command, "ID_ACTO_ADMIN", DbType.Int32, this.ddlActoAdmin.SelectedValue);
            db.AddInParameter(command, "FECHA_PUBLICACION", DbType.DateTime, this.txtFechaPub.Text);

            db.ExecuteNonQuery(command);

            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("INFORMACIÓN", 1, "Se almaceno publicación");

            limpiarControles();
            Response.Redirect("~/Informacion/Publicaciones.aspx");
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAceptar_Click.Finalizo");
        }
    }

    protected void ddlActoAdmin_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ddlActoAdmin_SelectedIndexChanged.Inicio");
            DbCommand command;

            Database db = DatabaseFactory.CreateDatabase();

            command = db.GetStoredProcCommand("GEN_LISTAR_ACTOS_ADMIN");
            db.AddInParameter(command, "ID_ACTO", DbType.String, ddlActoAdmin.SelectedValue);

            DataSet ds = db.ExecuteDataSet(command);

            this.txtFechaExp.Text = ds.Tables[0].Rows[0]["FECHA_ACTO_ADMINISTRATIVO"].ToString();

            this.txtNumeroDoc.Text = ds.Tables[0].Rows[0]["NUMERO_ACTO_ADMINISTRATIVO"].ToString();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ddlActoAdmin_SelectedIndexChanged.Finalizo");
        }
    }

    protected void limpiarControles()
    {
        this.ddlActoAdmin.SelectedValue = "-1";
        this.txtNumeroDoc.Text = "";
        this.txtFechaExp.Text = "";
        this.txtTituloPublicacion.Text = "";
        this.txtTextoPublicacion.Text = "";
        this.txtFechaPub.Text = "";
        
    }
}
