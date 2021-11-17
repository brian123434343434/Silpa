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
    public class WorkFlowActividadSilpaDALC
    {
        private Configuracion objConfiguracion;
        public WorkFlowActividadSilpaDALC()
        {
            objConfiguracion = new Configuracion();
        }
        #region "ACTIVIDAD_SILPA"

        /// <summary>
        /// Listar la informacion de las actividades SILPA
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Actividades_Silpa(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTAR_ACTIVIDAD_SILPA", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Registra una nueva actividad SILPA
        /// </summary>
        /// <param name="strNombreEstado">Nombre de la nueva actividad</param>
        public void Insertar_Actividades_Silpa(string strNombreActividad)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strNombreActividad };
                DbCommand cmd = db.GetStoredProcCommand("GEN_INSERTAR_ACTIVIDAD_SILPA", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Actualiza una Actividad SILPA
        /// </summary>
        /// <param name="iID">ID de la actividad</param>
        /// <param name="strNombreActividad">Nombre de la actividad</param>
        public void Actualizar_Actividades_Silpa(int iID, string strNombreActividad)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strNombreActividad};
                DbCommand cmd = db.GetStoredProcCommand("GEN_ACTUALIZAR_ACTIVIDAD_SILPA", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Elimina una actividad
        /// </summary>
        /// <param name="iID">Id del registro</param>
        public void Eliminar_Actividades_Silpa(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("GEN_ELIMINAR_ACTIVIDAD_SILPA", parametros);
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
