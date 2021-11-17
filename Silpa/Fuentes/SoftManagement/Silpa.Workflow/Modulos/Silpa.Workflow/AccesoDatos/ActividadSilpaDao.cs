using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Silpa.Workflow.Entidades;
using SoftManagement.Log;

namespace Silpa.Workflow.AccesoDatos
{
    internal static class ActividadSilpaDao
    {
        public static List<ActividadInfo> ConsultarActividadActual(long processInstanceId)
        {
            try
            {
	            Database db;
	            List<ActividadInfo> actividadInfo = new List<ActividadInfo>();
	            DbCommand command;
	            db = DatabaseFactory.CreateDatabase(ServicioWorkflowConfig.ConexionSilpa);
	            command = db.GetStoredProcCommand("GEN_WORFLOW_ACTIVIDAD_ACTUAL_CONSULTAR");
	            db.AddInParameter(command, "P_ID_PROCESS_INSTANCE", DbType.Int64, processInstanceId);
	            using (IDataReader reader = db.ExecuteReader(command))
	            {
	                while (reader.Read())
	                {
	                    actividadInfo.Add(new ActividadInfo(ConvertInt32(reader["ID"]), ConvertInt32(reader["IDActivity"]), reader["NAME"].ToString(), ConvertInt32(reader["ACTIVIDAD_SILPA_ID"]), reader["ACTIVIDAD_SILPA_NOMBRE"].ToString(), reader["PARTICIPANT_ID"].ToString()));
	                }
	            }
	            return actividadInfo;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Consultar Actividad Actual.";
                throw new Exception(strException, ex);
            }
        }

        private static int? ConvertInt32(object valor)
        {
            if (valor == DBNull.Value)
                return null;
            return Convert.ToInt32(valor);
        }

        public static bool ConsultarActividadCumpleCondicion(int? idactividad, string condicion)
        {
            try
            {
	            Database db;
	            List<ActividadInfo> actividadInfo = new List<ActividadInfo>();
	            DbCommand command;
	            db = DatabaseFactory.CreateDatabase(ServicioWorkflowConfig.ConexionSilpa);
	            command = db.GetStoredProcCommand("GEN_WORFLOW_ACTIVIDAD_CUMPLE_CONDICION");
	            db.AddInParameter(command, "P_ID_ACTIVITY", DbType.Int32, idactividad);
	            db.AddInParameter(command, "P_CONDICION", DbType.String, condicion);
	
	            DataTable dt = db.ExecuteDataSet(command).Tables[0];
	            if (dt.Rows[0]["RESPUESTA"].ToString() == "S")
	                return true;
	            else
	                return false;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Consultar Actividad Cumple Condición.";
                throw new Exception(strException, ex);
            }
        }


        public static void ActualizarEstadoProcesosCiclicos(Int64 IDProcessInstance)
        {
            try
            {
	            Database db;
	            List<ActividadInfo> actividadInfo = new List<ActividadInfo>();
	            DbCommand command;
	            db = DatabaseFactory.CreateDatabase(ServicioWorkflowConfig.ConexionSilpa);
	            command = db.GetStoredProcCommand("GEN_ACTUALIZAR_ESTADO_ACTIVITYINSTANCE_CICLICOS");
	            db.AddInParameter(command, "IDProcessInstance", DbType.Int32, IDProcessInstance);
	
	            db.ExecuteScalar(command);
	
	            SMLog.Escribir(Severidad.Informativo, String.Format("Actualiza Estado Ciclico(ActualizarEstadoProcesosCiclicos)"));
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Actualizar el Estado de los Procesos Cíclicos.";
                throw new Exception(strException, ex);
            }
        }

        public static void InsertarHistorialFinalizarTareas(Int64 IDProcessInstance, int IdActivity,string IDCondicion, int ACT_Instance)
        {
            try
            {
	            Database db;
	            List<ActividadInfo> actividadInfo = new List<ActividadInfo>();
	            DbCommand command;
	            db = DatabaseFactory.CreateDatabase(ServicioWorkflowConfig.ConexionSilpa);
	            command = db.GetStoredProcCommand("BPM_INSERTAR_HISTORIAL_FINALIZAR_TAREA");
	            db.AddInParameter(command, "PROC_ID", DbType.Int32, IDProcessInstance);
	            db.AddInParameter(command, "ACT_ID", DbType.Int32, IdActivity);
	            db.AddInParameter(command, "CONDICION", DbType.String, IDCondicion);
	            db.AddInParameter(command, "ACT_INSTANCE", DbType.Int32, ACT_Instance);
	
	            db.ExecuteScalar(command);
	
	            SMLog.Escribir(Severidad.Informativo, String.Format("InsertarHistorialFinalizarTareas()"));
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Insertar Historial Finalizar Tareas.";
                throw new Exception(strException, ex);
            }
        }
    }



}
