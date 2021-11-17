using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SoftManagement.CorreoElectronico.Entidades;
using System.Configuration;

namespace SoftManagement.CorreoElectronico.AccesoDatos
{
    public static class CorreoServidorDao
    {
        public static CorreoServidor ConsultarCorreoServidor(int correoServidorId)
        {
            Database db;
            DbCommand command;
            CorreoServidor correoServidor = null;
            if (String.IsNullOrEmpty(CorreoElectronicoConfig.Conexion))
                db = DatabaseFactory.CreateDatabase();
            else
                db = DatabaseFactory.CreateDatabase(CorreoElectronicoConfig.Conexion);

            command = db.GetStoredProcCommand(CorreoElectronicoConfig.ProcedimientoConsultarServidor);
            db.AddInParameter(command, "P_CORREO_SERVIDOR_ID", DbType.Int32, correoServidorId);
            using (IDataReader reader = db.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    correoServidor = new CorreoServidor(correoServidorId, reader["NOMBRE_SERVIDOR"].ToString(), reader["HOST"].ToString(), reader["USUARIO"].ToString(), reader["CONTRASENA"].ToString(), reader["SEPARADOR"].ToString());
                }
            }
            return correoServidor;
        }

        /// <summary>
        /// Consulta los datos de correo en el Web.Config
        /// </summary>
        /// <returns></returns>
        public static CorreoServidor ConsultarCorreoServidorP(int correoServidorId)
        {
            Database db;
            DbCommand command;
            CorreoServidor correoServidor = null;
            if (String.IsNullOrEmpty(CorreoElectronicoConfig.Conexion))
                db = DatabaseFactory.CreateDatabase();
            else
                db = DatabaseFactory.CreateDatabase(CorreoElectronicoConfig.Conexion);

            command = db.GetStoredProcCommand(CorreoElectronicoConfig.ProcedimientoConsultarServidor);
            db.AddInParameter(command, "P_CORREO_SERVIDOR_ID", DbType.Int32, correoServidorId);
            using (IDataReader reader = db.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    correoServidor = new CorreoServidor(correoServidorId, reader["NOMBRE_SERVIDOR"].ToString(), reader["HOST"].ToString(), reader["USUARIO"].ToString(), reader["CONTRASENA"].ToString(), reader["SEPARADOR"].ToString(), reader["PUERTO"].ToString(), Convert.ToBoolean(reader["APLICA_SEGURIDAD"]));
                }
               
            }
            return correoServidor; 
        }
    }
}
