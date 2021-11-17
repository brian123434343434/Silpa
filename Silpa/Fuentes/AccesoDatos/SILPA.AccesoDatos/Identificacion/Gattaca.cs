using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Identificacion
{
    public class Gattaca
    {
        static Configuracion objConfiguracion = new Configuracion();

        public static string Clave(string code)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetSqlStringCommand("select PASSWORD from softmanagement_esecurity.dbo.applicationuser WHERE UPPER(CODE) = UPPER('" + code + "')");
            DataTable t = new DataTable();
            t = db.ExecuteDataSet(cmd).Tables[0] ;
            if (t.Rows.Count > 0)
                return t.Rows[0][0].ToString();
            else
                return "Usuario No encontrado";
        }
    }
}
