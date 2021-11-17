using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SoftManagement.CorreoElectronico.Entidades;
using SoftManagement.CorreoElectronico;

namespace SoftManagement.CorreoElectronico.AccesoDatos
{
    public static class CorreoElectronicoDao
    {
        public static void InsertarCorreoElectronico(string asunto, string de,string para, string cc, string cco, string mensaje, int correoServidorId, string anexos, int plantillaId, int IdCorreoPadre)
        {
            try
            {
	            Database db;
	            DbCommand command;
	            if (String.IsNullOrEmpty(CorreoElectronicoConfig.Conexion))
	                db = DatabaseFactory.CreateDatabase();
	            else
	                db = DatabaseFactory.CreateDatabase(CorreoElectronicoConfig.Conexion);

	            command = db.GetStoredProcCommand(CorreoElectronicoConfig.ProcedimientoInsertarCorreo);
	            db.AddInParameter(command, "P_ASUNTO", DbType.String, asunto);
	            db.AddInParameter(command, "P_DE", DbType.String, de);
	            db.AddInParameter(command, "P_PARA", DbType.String, para);
	            db.AddInParameter(command, "P_CC", DbType.String, cc);
	            db.AddInParameter(command, "P_CCO", DbType.String, cco);
	            db.AddInParameter(command, "P_MENSAJE", DbType.String, mensaje);
	            db.AddInParameter(command, "P_ANEXOS", DbType.String, anexos);
	            db.AddInParameter(command, "P_CORREO_SERVIDOR_ID", DbType.Int32, correoServidorId);
	            db.AddInParameter(command, "P_CORREO_ESTADO_ID", DbType.Int32, CorreoEstado.NoEnviado);
	            db.AddInParameter(command, "P_CORREO_PLANTILLA_ID", DbType.Int32, plantillaId);
	            db.AddInParameter(command, "P_CORREO_ID_PADRE", DbType.Int32, IdCorreoPadre);
	            db.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Insertar Correo Electrónico.";
                throw new Exception(strException, ex);
            }
        }


        public static void InsertarCorreoElectronicoInHabilitado(string asunto, string de, string para, string cc, string cco, string mensaje, int correoServidorId, string anexos, int plantillaId, int idRadicacion)
        {
            Database db;
            DbCommand command;
            if (String.IsNullOrEmpty(CorreoElectronicoConfig.Conexion))
                db = DatabaseFactory.CreateDatabase();
            else
                db = DatabaseFactory.CreateDatabase(CorreoElectronicoConfig.Conexion);

            command = db.GetStoredProcCommand(CorreoElectronicoConfig.ProcedimientoInsertarCorreoInhabilitado);
            db.AddInParameter(command, "P_ASUNTO", DbType.String, asunto);
            db.AddInParameter(command, "P_DE", DbType.String, de);
            db.AddInParameter(command, "P_PARA", DbType.String, para);
            db.AddInParameter(command, "P_CC", DbType.String, cc);
            db.AddInParameter(command, "P_CCO", DbType.String, cco);
            db.AddInParameter(command, "P_MENSAJE", DbType.String, mensaje);
            db.AddInParameter(command, "P_ANEXOS", DbType.String, anexos);
            db.AddInParameter(command, "P_CORREO_SERVIDOR_ID", DbType.Int32, correoServidorId);
            db.AddInParameter(command, "P_CORREO_ESTADO_ID", DbType.Int32, CorreoEstado.Inhabilitado);
            db.AddInParameter(command, "P_CORREO_PLANTILLA_ID", DbType.Int32, plantillaId);
            db.AddInParameter(command, "P_ID_RADICACION", DbType.Int32, idRadicacion);
            
            int i = db.ExecuteNonQuery(command);
        }


        public static List<CorreoElectronico.Entidades.CorreoElectronico> ConsultarCorreosPendientesEnvio()
        {
            List<CorreoElectronico.Entidades.CorreoElectronico> correoElectronico = new List<CorreoElectronico.Entidades.CorreoElectronico>();
            Database db;
            DbCommand command;
            DataTable datos;
            if (String.IsNullOrEmpty(CorreoElectronicoConfig.Conexion))
                db = DatabaseFactory.CreateDatabase();
            else
                db = DatabaseFactory.CreateDatabase(CorreoElectronicoConfig.Conexion);

            command = db.GetStoredProcCommand(CorreoElectronicoConfig.ProcedimientoConsultarCorreosAEnviar);
            datos = db.ExecuteDataSet(command).Tables[0];
             foreach (DataRow row in datos.Rows)
             {
                 correoElectronico.Add(new SoftManagement.CorreoElectronico.Entidades.CorreoElectronico(Convert.ToInt32(row["CORREO_ID"]), Convert.ToDateTime(row["FECHA_CREACION"]), row["ASUNTO"].ToString(), row["DE"].ToString(), row["PARA"].ToString(), row["CC"].ToString(), row["CCO"].ToString(), row["MENSAJE"].ToString(), row["ANEXOS"].ToString(), Convert.ToInt32(row["CORREO_SERVIDOR_ID"]), Convert.ToInt32(row["CORREO_PLANTILLA_ID"]), Convert.ToBoolean(row["CONFIRMAR_ENVIO"])));
             }
            return correoElectronico;
        }

        public static void ActualizarEstadoCorreoElectronico(int correoElectronicoId, CorreoEstado estado)
        {
            Database db;
            DbCommand command;
            if (String.IsNullOrEmpty(CorreoElectronicoConfig.Conexion))
                db = DatabaseFactory.CreateDatabase();
            else
                db = DatabaseFactory.CreateDatabase(CorreoElectronicoConfig.Conexion);

            command = db.GetStoredProcCommand(CorreoElectronicoConfig.ProcedimientoActualizarCorreo);
            db.AddInParameter(command, "P_CORREO_ID", DbType.Int32, correoElectronicoId);
            db.AddInParameter(command, "P_CORREO_ESTADO_ID", DbType.Int32, (int)estado);
            db.ExecuteNonQuery(command);
        }
    }
}
