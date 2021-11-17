using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Silpa.Workflow.Entidades;

namespace Silpa.Workflow.AccesoDatos
{
    internal static class ActividadCondicionDao
    {
        public static List<Condicion> ConsultarCondicionActividad(int actividadId)
        {
            try
            {
	            Database db;
	            DbCommand command;
	            List<Condicion> condiciones = new List<Condicion>();
	            db = DatabaseFactory.CreateDatabase(ServicioWorkflowConfig.ConexionSilpa);
	            command = db.GetStoredProcCommand("GEN_WORFLOW_CONDICIONES_ACTIVIDAD_CONSULTAR");
	            db.AddInParameter(command, "P_IDACTIVITY", DbType.Int32, actividadId);
	            using (IDataReader reader = db.ExecuteReader(command))
	            {
	                while (reader.Read())
	                    condiciones.Add(new Condicion(Convert.ToInt32(reader["IDCondition"]), reader["CODE"].ToString()));
	            }
                return condiciones;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Consultar la Condición de la Actividad.";
                throw new Exception(strException, ex);
            }
        }
    }
}
