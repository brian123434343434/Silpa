using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;


namespace SILPA.AccesoDatos.PINES
{
    public class AccionActividadDALC
    {
        private Configuracion objConfiguracion;

        public AccionActividadDALC()
        {
            objConfiguracion = new Configuracion();
        }
        public void Insertar(AccionActividadIdentity vAccionActividadIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_INSERTAR_ACCION_ACTIVIDAD");
                db.AddInParameter(cmd, "P_IDACTIVITY", DbType.Int32, vAccionActividadIdentity.IdActivity);
                db.AddInParameter(cmd, "P_IDACCION", DbType.Int32, vAccionActividadIdentity.IdAccion);
                db.AddInParameter(cmd, "P_ORDEN", DbType.Int32, vAccionActividadIdentity.Orden);
                db.AddInParameter(cmd, "P_DIAS_EJECUCION", DbType.Int32, vAccionActividadIdentity.DiasEjecucion);
                db.AddInParameter(cmd, "P_OBLIGATORIA", DbType.Boolean, vAccionActividadIdentity.Obligatoria);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void Consultar(ref AccionActividadIdentity vAccionActividadIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTAR_ACCION_ACTIVIDAD");
                db.AddInParameter(cmd, "P_IDACTIVITY", DbType.Int32, vAccionActividadIdentity.IdActivity);
                db.AddInParameter(cmd, "P_IDACCION", DbType.Int32, vAccionActividadIdentity.IdAccion);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        if (reader["ORDEN"].ToString() != string.Empty)
                            vAccionActividadIdentity.Orden = Convert.ToInt32(reader["ORDEN"]);
                        vAccionActividadIdentity.DiasEjecucion = Convert.ToInt32(reader["DIAS_EJECUCION"]);
                        vAccionActividadIdentity.Obligatoria = Convert.ToBoolean(reader["OBLIGATORIA"]);
                    }
                }
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void Eliminar(AccionActividadIdentity vAccionActividadIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_ELIMINAR_ACCION_ACTIVIDAD");
                db.AddInParameter(cmd, "P_IDACTIVITY", DbType.Int32, vAccionActividadIdentity.IdActivity);
                db.AddInParameter(cmd, "P_IDACCION", DbType.Int32, vAccionActividadIdentity.IdAccion);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void Actualizar(AccionActividadIdentity vAccionActividadIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_ACTUALIZAR_ACCION_ACTIVIDAD");
                db.AddInParameter(cmd, "P_IDACTIVITY", DbType.Int32, vAccionActividadIdentity.IdActivity);
                db.AddInParameter(cmd, "P_IDACCION", DbType.Int32, vAccionActividadIdentity.IdAccion);
                db.AddInParameter(cmd, "P_ORDEN", DbType.Int32, vAccionActividadIdentity.Orden);
                db.AddInParameter(cmd, "P_DIAS_EJECUCION", DbType.Int32, vAccionActividadIdentity.DiasEjecucion);
                db.AddInParameter(cmd, "P_OBLIGATORIA", DbType.Boolean, vAccionActividadIdentity.Obligatoria);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DataTable ConsultaEstadoActivityProcessInstance(int? intIdProcessInstance, int? intIdAccion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTA_ESTADO_ACTIVITY_IDPROCESSINSTANCE");
                db.AddInParameter(cmd, "P_IDACTIVITY", DbType.Int32, intIdAccion);
                db.AddInParameter(cmd, "P_IDPROCESSINSTANCE", DbType.Int32, intIdProcessInstance);
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
    }
}
