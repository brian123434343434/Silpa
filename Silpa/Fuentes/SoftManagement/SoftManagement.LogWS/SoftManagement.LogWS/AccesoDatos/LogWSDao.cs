using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace SoftManagement.LogWS.AccesoDatos
{
    public static class LogWSDao
    {
        public static Int64 GuardarLog(string strNombreWS, string strNombreMetodo, int iTipoMensaje, string strMensaje, string strNoVital, int iAutoridad, Int64 iIdPadre)
        {
            Int64 iIdLog = 0;
            Database db;
            DbCommand command;
            if (String.IsNullOrEmpty(LogWSConfig.Conexion))
                db = DatabaseFactory.CreateDatabase();
            else
                db = DatabaseFactory.CreateDatabase(LogWSConfig.Conexion);

            object[] parametros = new object[] { iIdLog, strNombreWS, strNombreMetodo, iTipoMensaje, strMensaje, strNoVital, iAutoridad, iIdPadre };
            command = db.GetStoredProcCommand(LogWSConfig.ProcedimientoAlmacenado,parametros);
            db.ExecuteScalar(command);

            iIdLog = Int64.Parse(command.Parameters["@ID"].Value.ToString());
            return iIdLog;
        }

        private static object ConvertNull(string valor)
        {
            if (valor == null)
                return System.DBNull.Value;
            if (valor.Length == 0)
                return System.DBNull.Value;

            return valor;
        }

    }
}
