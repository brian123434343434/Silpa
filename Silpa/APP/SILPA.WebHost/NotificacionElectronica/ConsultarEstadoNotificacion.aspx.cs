using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using SoftManagement.Log;


public partial class NotificacionElectronica_ConsultarEstadoNotificacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        llenarGrilla();
    }

    public void llenarGrilla()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".llenarGrilla.Inicio");
            DbCommand command;
            Database db = DatabaseFactory.CreateDatabase();

            command = db.GetStoredProcCommand("GEN_CONSULTAR_DOCUMENTOS_NOTIFICACION");

            db.AddInParameter(command, "P_NUMERO_SILPA", DbType.String);
            db.AddInParameter(command, "P_USUARIO", DbType.String);

            DataSet ds = db.ExecuteDataSet(command);

            this.gvDocumentos.DataSource = ds;
            gvDocumentos.DataBind();
            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("NOTIFICACIÓN ELECTRONICA", 2, "Se consulto estado de notificaciones");
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".llenarGrilla.Finalizo");
        }
    }

    protected void gvDocumentos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".gvDocumentos_RowCommand.Inicio");
            Int32 doc_id = Convert.ToInt32(gvDocumentos.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString());
            if (e.CommandName == "Boton")
            {
                DbCommand command;
                Database db = DatabaseFactory.CreateDatabase();

                command = db.GetStoredProcCommand("GEN_ACTUALIZAR_ESTADO_NOTIFICACION");

                db.AddInParameter(command, "DFI_ID", DbType.Int32, doc_id);

                db.ExecuteNonQuery(command);

            }
            llenarGrilla();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".gvDocumentos_RowCommand.Finalizo");
        }
    }
}
