using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SoftManagement.Log;

namespace Silpa.Workflow.AccesoDatos
{
    public static class BandejaTareasDao
    {
        public static DataTable ConsultarTareasSinIniciar(string usuario, string numeroVital, string numeroExpediente, int? tipoTramite, DateTime? fechaInicio, DateTime? fechaFinal)
        {
            Database db;
            DbCommand command;
            db = DatabaseFactory.CreateDatabase(BandejaTareasConfig.ConexionSilpa);
            command = db.GetStoredProcCommand("GEN_BANDEJA_TAREAS_SIN_INICIAR_CONSULTAR");
            db.AddInParameter(command, "P_USUARIO", DbType.String, usuario);
            db.AddInParameter(command, "P_SOL_NUMERO_SILPA", DbType.String, BandejaTareasDao.ConvertNull(numeroVital));
            db.AddInParameter(command, "P_TIPO_TRAMITE", DbType.Int32,BandejaTareasDao.ConvertNull( tipoTramite));
            db.AddInParameter(command, "P_NUMERO_EXPEDIENTE", DbType.String, BandejaTareasDao.ConvertNull(numeroExpediente));
            db.AddInParameter(command, "P_FECHA_INICIO", DbType.DateTime, BandejaTareasDao.ConvertNull(fechaInicio));
            db.AddInParameter(command, "P_FECHA_FINAL", DbType.DateTime, BandejaTareasDao.ConvertNull(fechaFinal));
            return db.ExecuteDataSet(command).Tables[0];
        }

        public static DataTable ConsultarTareasFinalizadas(string usuario, string numeroVital, string numeroExpediente,  int? tipoTramite, DateTime? fechaInicio, DateTime? fechaFinal)
        {
            Database db;
            DbCommand command;
            db = DatabaseFactory.CreateDatabase(BandejaTareasConfig.ConexionSilpa);
            command = db.GetStoredProcCommand("GEN_BANDEJA_TAREAS_FINALIZADAS_CONSULTAR");
            db.AddInParameter(command, "P_USUARIO", DbType.String, usuario);
            db.AddInParameter(command, "P_SOL_NUMERO_SILPA", DbType.String, BandejaTareasDao.ConvertNull(numeroVital));
            db.AddInParameter(command, "P_TIPO_TRAMITE", DbType.Int32, BandejaTareasDao.ConvertNull(tipoTramite));
            db.AddInParameter(command, "P_NUMERO_EXPEDIENTE", DbType.String, BandejaTareasDao.ConvertNull(numeroExpediente));
            db.AddInParameter(command, "P_FECHA_INICIO", DbType.DateTime, BandejaTareasDao.ConvertNull(fechaInicio));
            db.AddInParameter(command, "P_FECHA_FINAL", DbType.DateTime, BandejaTareasDao.ConvertNull(fechaFinal));
           return db.ExecuteDataSet(command).Tables[0];
        }

        public static void ActualizarActividadesParalelas(string usuario)
        {
            Database db;
            DbCommand command;
            db = DatabaseFactory.CreateDatabase(ServicioWorkflowConfig.ConexionSilpa);
            command = db.GetStoredProcCommand("GEN_ACTUALIZAR_ACTIVIDADES_PARALELA");
            db.AddInParameter(command, "P_USUARIO", DbType.String, usuario);
            db.ExecuteScalar(command);

            SMLog.Escribir(Severidad.Informativo, String.Format("ValidarActividadesParalelas()"));


        }

        
        private static object ConvertNull(string valor)
        {
            if (valor == null)
                return System.DBNull.Value;
            if (valor.Length == 0)
                return System.DBNull.Value;
            return valor;
        }

        private static object ConvertNull(int? valor)
        {
            if (valor == null)
                return System.DBNull.Value;
            return valor.Value;
        }

        public static object ConvertNull(DateTime? valor)
        {
            if (valor == null)
                return System.DBNull.Value;
            return valor;
        }

        public static DataTable ConsultarTareasSinIniciarPINES(string usuario, string numeroVital, DateTime? fechaInicio, DateTime? fechaFinal)
        {
            Database db;
            DbCommand command;
            db = DatabaseFactory.CreateDatabase(BandejaTareasConfig.ConexionSilpa);
            command = db.GetStoredProcCommand("GEN_BANDEJA_TAREAS_SIN_INICIAR_PINES");
            db.AddInParameter(command, "P_USUARIO", DbType.String, usuario);
            db.AddInParameter(command, "P_SOL_NUMERO_SILPA", DbType.String, BandejaTareasDao.ConvertNull(numeroVital));
            db.AddInParameter(command, "P_FECHA_INICIO", DbType.DateTime, BandejaTareasDao.ConvertNull(fechaInicio));
            db.AddInParameter(command, "P_FECHA_FINAL", DbType.DateTime, BandejaTareasDao.ConvertNull(fechaFinal));
            return db.ExecuteDataSet(command).Tables[0];
        }

        public static DataTable ConsultarTareasFinalizadasPINES(string usuario, string numeroVital, string numeroExpediente, int? tipoTramite, DateTime? fechaInicio, DateTime? fechaFinal)
        {
            Database db;
            DbCommand command;
            db = DatabaseFactory.CreateDatabase(BandejaTareasConfig.ConexionSilpa);
            command = db.GetStoredProcCommand("GEN_BANDEJA_TAREAS_FINALIZADAS_PINES");
            db.AddInParameter(command, "P_USUARIO", DbType.String, usuario);
            db.AddInParameter(command, "P_SOL_NUMERO_SILPA", DbType.String, BandejaTareasDao.ConvertNull(numeroVital));
            db.AddInParameter(command, "P_FECHA_INICIO", DbType.DateTime, BandejaTareasDao.ConvertNull(fechaInicio));
            db.AddInParameter(command, "P_FECHA_FINAL", DbType.DateTime, BandejaTareasDao.ConvertNull(fechaFinal));
            return db.ExecuteDataSet(command).Tables[0];
        }
        

  
    }
}
