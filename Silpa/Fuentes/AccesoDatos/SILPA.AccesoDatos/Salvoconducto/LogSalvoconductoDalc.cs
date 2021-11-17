using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class LogSalvoconductoDalc
    {
        private Configuracion objConfiguracion;

        public LogSalvoconductoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public int InsertarLogSalvoconducto(string strMetodo, string strStackTrace)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_INSERTA_LOG_SALVOCONDUCTO");
                db.AddOutParameter(cmd, "P_LOG_ID", DbType.Int32, 10);
                db.AddInParameter(cmd, "P_METODO", DbType.String, strMetodo);
                db.AddInParameter(cmd, "P_STACKTRACE", DbType.String, strStackTrace);
                db.ExecuteNonQuery(cmd);
                return Convert.ToInt32(db.GetParameterValue(cmd, "@P_LOG_ID"));
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
