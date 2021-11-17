using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.AdmTablasBasicas
{
    public class WorkFlowActividadRelacionadaDALC
    {
        private Configuracion objConfiguracion;
        public WorkFlowActividadRelacionadaDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "ACTIVIDAD_RELACIONADA"

        /// <summary>
        /// Lista las actividades silpa
        /// </summary>
        /// <returns></returns>
        public DataTable Listar_Actividades_Silpa()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_WFACTIVIDAD_SILPA");
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Lista las actividades de la tabla Activity con las relacionadas en la tabla GEN_WORKFLOW_ACTIVIDAD_RELACIONADA
        /// </summary>
        /// <param name="iIdActividadSilpa">Id de la actividad Silpa</param>
        /// <returns></returns>
        public DataTable Listar_Actividades_Relacionadas(Int32 ACTIVIDAD_SILPA_ID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { ACTIVIDAD_SILPA_ID };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_WFACTIVIDAD_RELACIONADA", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Lista las actividades de la tabla Activity con las no relacionadas en la tabla GEN_WORKFLOW_ACTIVIDAD_RELACIONADA
        /// </summary>
        /// <param name="iIdActividadSilpa">Id de la actividad Silpa</param>
        /// <returns></returns>
        public DataTable Listar_Actividades_NoRelacionadas(Int32 ACTIVIDAD_SILPA_ID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { ACTIVIDAD_SILPA_ID };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_WFACTIVIDAD_NORELACIONADA", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Registra una nueva relacion de actividades.
        /// </summary>
        /// <param name="ACTIVITY_ID"></param>
        /// <param name="ACTIVIDAD_SILPA_ID"></param>
        public void Insertar_Actividades_Relacionadas(int ACTIVITY_ID, int ACTIVIDAD_SILPA_ID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {ACTIVITY_ID, ACTIVIDAD_SILPA_ID};
                DbCommand cmd = db.GetStoredProcCommand("GEN_INSERTAR_ACTIVIDAD_RELACIONADA", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Elimina una relacion de actividades.
        /// </summary>
        /// <param name="ACTIVITY_ID"></param>
        /// <param name="ACTIVIDAD_SILPA_ID"></param>
        public void Eliminar_Actividades_Relacionadas(int ACTIVITY_ID, int ACTIVIDAD_SILPA_ID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {ACTIVITY_ID, ACTIVIDAD_SILPA_ID };
                DbCommand cmd = db.GetStoredProcCommand("GEN_ELIMINAR_ACTIVIDAD_RELACIONADA", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        #endregion
    }
}
