using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace SoftManagement.Log.AccesoDatos
{
    public static class LogDao
    {
        public static void GuardarLog(string maquina, string mensaje, Severidad severidad, string usuario)
        {
            Database db;
            DbCommand command;
            if (String.IsNullOrEmpty(LogConfig.Conexion))
                db = DatabaseFactory.CreateDatabase();
            else
                db = DatabaseFactory.CreateDatabase(LogConfig.Conexion);

            command = db.GetStoredProcCommand(LogConfig.ProcedimientoAlmacenado);
            db.AddInParameter(command, "P_MAQUINA", DbType.String, maquina);
            db.AddInParameter(command, "P_MENSAJE", DbType.String, mensaje.Length>2000?mensaje.Substring(0,2000):mensaje);
            db.AddInParameter(command, "P_ID_SEVERIDAD", DbType.Int32, severidad);
            db.AddInParameter(command, "P_USUARIO", DbType.String, LogDao.ConvertNull(usuario));
            db.ExecuteNonQuery(command);
        }


        #region jmartinez creo funcionalidad para grabar el log para los correos
        public static void GuardarLogCorreo(int IdCorreo, string maquina, string mensaje, Boolean EsError ,Severidad severidad, string usuario)
        {
            Database db;
            DbCommand command;
            if (String.IsNullOrEmpty(LogConfig.Conexion))
                db = DatabaseFactory.CreateDatabase();
            else
                db = DatabaseFactory.CreateDatabase(LogConfig.Conexion);

            command = db.GetStoredProcCommand(LogConfig.ProcedimientoAlmacenadoCorreo);
            db.AddInParameter(command, "P_CORREO_ID", DbType.Int32, IdCorreo);
            db.AddInParameter(command, "P_MAQUINA", DbType.String, maquina);
            db.AddInParameter(command, "P_MENSAJE", DbType.String, mensaje.Length > 2000 ? mensaje.Substring(0, 2000) : mensaje);
            db.AddInParameter(command, "P_ES_ERROR", DbType.Boolean, EsError);
            db.AddInParameter(command, "P_SEVERIDAD_ID", DbType.Int32, severidad);
            db.AddInParameter(command, "P_USUARIO", DbType.String, LogDao.ConvertNull(usuario));
            db.ExecuteNonQuery(command);
        }
        #endregion

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
