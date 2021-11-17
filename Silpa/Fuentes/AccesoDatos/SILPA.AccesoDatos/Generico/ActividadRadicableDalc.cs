using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    /// Utilizada para acceder a la tabla ACTIVIDAD_RADICABLE de SILPA
    /// </summary>
    public class ActividadRadicableDalc
    {
        Configuracion _objConfiguracion;

        public ActividadRadicableDalc()
        {
            _objConfiguracion = new Configuracion();
        }
        /// <summary>
        /// Lista las actividades radicables
        /// </summary>
        /// <returns>Lista con actividades radicables</returns>
        /// <param name="idActividad">ID de la actividad (opcional)</param>
        public List<ActividadRadicableIdentity> ListaActividadesRadicables(int? idActividad)
        {
            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idActividad };
            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_ACTIVIDAD_RADICABLE", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            List<ActividadRadicableIdentity> listaActividades = new List<ActividadRadicableIdentity>();
            ActividadRadicableIdentity actividad;
            try
            {
                
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsResultado.Tables[0].Rows)
                    {
                        actividad = new ActividadRadicableIdentity();
                        actividad.ID = Convert.ToInt32(dr["ID_ACTIVIDAD"]);
                        actividad.Nombre = Convert.ToString(dr["NOMBRE_ACTIVIDAD"]);
                        actividad.IDForma = Convert.ToInt32(dr["FORMA"]);
                        listaActividades.Add(actividad);
                    }

                }
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
            return listaActividades;

        }

        /// <summary>
        /// Obtiene la primera condicion asociada a la Actividad
        /// </summary>
        /// <param name="idActivity">ID de la Actividad</param>
        /// <returns>ID de la Condición</returns>
        /// <remarks>No utilizar IDActivityInstance</remarks>
        public int ObtenerCondicionPorActividad(int idActivity)
        {
            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idActivity};
            DbCommand cmd = db.GetStoredProcCommand("bpm_condiciones_actividad", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    if (dsResultado.Tables[0].Rows[0]["IDCondition"] != DBNull.Value)
                        return Convert.ToInt32(dsResultado.Tables[0].Rows[0]["IDCondition"]);
                    else
                        return 0;
                }
                else
                    return 0;
            }
            finally
            {
                cmd.Dispose();
                db = null;
                cmd = null;
            }
        }

        /// <summary>
        /// Obtiene el ID de la Actividad de BPM
        /// </summary>
        /// <param name="IDActivityInstance">Instancia de la actividad</param>
        /// <returns>El ID de la Actividad ó 0 Si no encuentra la actividad</returns>
        public int ObtenerActividadPorInstancia(int IDActivityInstance)
        {
            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { IDActivityInstance, null,null };
            DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_ACTIVITY_INSTANCE", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    if (dsResultado.Tables[0].Rows[0]["IDActivity"] != DBNull.Value)
                        return Convert.ToInt32(dsResultado.Tables[0].Rows[0]["IDActivity"]);
                    else
                        return 0;
                }
                else
                    return 0;
            }
            finally
            {
                cmd.Dispose();
                db = null;
                cmd = null;
            }
        }
    }
}
