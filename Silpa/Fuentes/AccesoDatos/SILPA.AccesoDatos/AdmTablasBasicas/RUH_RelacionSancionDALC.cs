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
    public class RUH_RelacionSancionDALC
    {
        private Configuracion objConfiguracion;
        public RUH_RelacionSancionDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "RELACION_SANCION"

        /// <summary>
        /// Listar la informacion de la relacion de las sanciones
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Relacion_Sancion(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("RUH_LISTAR_RELACION_SANCION", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Cosulta para cargar el combo de tipo de sancion
        /// </summary>
        /// <returns></returns>
        public DataTable CargarCombo_TipoSancion()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("RUH_CARGAR_COMBO_TIPO_SANCION");
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Consulta para cargar el combo de opcion
        /// </summary>
        /// <returns></returns>
        public DataTable CargarCombo_Opcion()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("RUH_CARGAR_COMBO_OPCION");
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Resgistra un nuevo registro en la tabla RUH_Relacion_Sancion
        /// </summary>
        /// <param name="iIdTipoSancion"></param>
        /// <param name="iIdOpcion"></param>
        /// <param name="strSancion"></param>
        public void Insertar_Relacion_Sancion(int iIdTipoSancion, int iIdOpcion, string strSancion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iIdTipoSancion, iIdOpcion, strSancion };
                DbCommand cmd = db.GetStoredProcCommand("RUH_INSERTAR_RELACION_SANCION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Actuyaliza el registro de la relacion de la sancion
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iIdTipoSancion"></param>
        /// <param name="iIdOpcion"></param>
        /// <param name="strSancion"></param>
        public void Actualizar_Relacion_Sancion(int iID, int iIdTipoSancion, int iIdOpcion, string strSancion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, iIdTipoSancion, iIdOpcion, strSancion };
                DbCommand cmd = db.GetStoredProcCommand("RUH_ACTUALIZAR_RELACION_SANCION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Elimina el registro de la relacion
        /// </summary>
        /// <param name="iID">Id del registro</param>
        public void Eliminar_Relacion_Sancion(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("RUH_ELIMINAR_RELACION_SANCION", parametros);
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
