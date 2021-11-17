using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SILPA.AccesoDatos;
using SoftManagement.Log;

public partial class NotificacionElectronica_Documento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Page_Load.Inicio");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server = netco-srv1; Database= silpa; user=sa; pwd= Colombia2010";
            SqlCommand comm = new SqlCommand();

            comm.Connection = conn;

            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "GEN_CONSULTAR_DOCUMENTOS_FIRMA";

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@P_NUMERO_SILPA";
            param.SqlDbType = SqlDbType.VarChar;
            param.Value = "";

            comm.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@P_USUARIO";
            param.SqlDbType = SqlDbType.VarChar;
            param.Value = "";

            comm.Parameters.Add(param);


            SqlDataReader reader;
            conn.Open();

            reader = comm.ExecuteReader();


            DataTable tbl = new DataTable();
            tbl.Load(reader);

            conn.Close();


            this.gvDocumentos.DataSource = tbl;
            gvDocumentos.DataBind();

        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Page_Load.Finalizo");
        }
    }
}
