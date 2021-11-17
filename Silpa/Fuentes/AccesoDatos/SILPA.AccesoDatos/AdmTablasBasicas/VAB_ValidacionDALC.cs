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
    public class VAB_ValidacionDALC
    {
        private Configuracion objConfiguracion;
        public VAB_ValidacionDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "Validacion"

        /// <summary>
        /// Listar la informacion de la tabla VAB_Validacion
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Validacion(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {strDescripcion};
                DbCommand cmd = db.GetStoredProcCommand("VAB_LISTAR_VALIDACION", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Resgistra una nueva validacion
        /// </summary>
        /// <param name="strDescripcion"></param>
        /// <param name="strSentencia"></param>
        public void Insertar_Validacion(string strDescripcion, string strSentencia)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion, strSentencia };
                DbCommand cmd = db.GetStoredProcCommand("VAB_INSERTAR_VALIDACION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Actualizar validacion
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="strDescripcion"></param>
        /// <param name="strSentencia"></param>
        public void Actualizar_Validacion(int iID, string strDescripcion, string strSentencia)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strDescripcion, strSentencia };
                DbCommand cmd = db.GetStoredProcCommand("VAB_ACTUALIZAR_VALIDACION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Elimina una validacion
        /// </summary>
        /// <param name="iID">Id del registro</param>
        public void Eliminar_Validacion(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("VAB_ELIMINAR_VALIDACION", parametros);
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
