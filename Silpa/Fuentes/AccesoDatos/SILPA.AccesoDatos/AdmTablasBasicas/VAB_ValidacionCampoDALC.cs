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
    public class VAB_ValidacionCampoDALC
    {
        private Configuracion objConfiguracion;
        public VAB_ValidacionCampoDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "ValidacionCampo"

        /// <summary>
        /// Listar la informacion de la tabla VAB_ValidacionCampo
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_ValidacionCampo(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {strDescripcion};
                DbCommand cmd = db.GetStoredProcCommand("VAB_LISTAR_VALIDACION_CAMPO", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Resgistra una nueva validacion de campo
        /// </summary>
        /// <param name="iIdCampo"></param>
        /// <param name="iIdValidacion"></param>
        /// <param name="strActivo"></param>
        public void Insertar_ValidacionCampo(string strIdCampo, int iIdValidacion, string strActivo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strIdCampo, iIdValidacion, strActivo };
                DbCommand cmd = db.GetStoredProcCommand("VAB_INSERTAR_VALIDACION_CAMPO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Actualizar validacion de campo
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iIdCampo"></param>
        /// <param name="iIdValidacion"></param>
        /// <param name="strActivo"></param>
        public void Actualizar_ValidacionCampo(int iID, string strIdCampo, int iIdValidacion, string strActivo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strIdCampo, iIdValidacion, strActivo };
                DbCommand cmd = db.GetStoredProcCommand("VAB_ACTUALIZAR_VALIDACION_CAMPO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Elimina una validacion de campo
        /// </summary>
        /// <param name="iID">Id del registro</param>
        public void Eliminar_ValidacionCampo(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("VAB_ELIMINAR_VALIDACION_CAMPO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Consulta para cargar el combo de los campos
        /// </summary>
        /// <returns></returns>
        public DataTable Cargar_Combo_Campos()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("VAB_CARGAR_COMBO_CAMPOS");
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Consulta para cargar el combo de las validaciones
        /// </summary>
        /// <returns></returns>
        public DataTable Cargar_Combo_Validaciones()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("VAB_CARGAR_COMBO_VALIDACIONES");
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        #endregion

    }
}
