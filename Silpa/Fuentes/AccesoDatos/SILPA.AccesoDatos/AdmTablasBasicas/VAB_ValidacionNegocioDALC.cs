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
    public class VAB_ValidacionNegocioDALC
    {
        private Configuracion objConfiguracion;
        public VAB_ValidacionNegocioDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "Validacion"

        /// <summary>
        /// Listar la informacion de la tabla VAB_Validacion_Negocio
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_ValidacionNegocio(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {strDescripcion};
                DbCommand cmd = db.GetStoredProcCommand("VAB_LISTAR_VALIDACION_NEGOCIO", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Registra una nueva configuracion para la validacion del negocio
        /// </summary>
        /// <param name="strValidacion"></param>
        /// <param name="strProcedimiento"></param>
        public void Insertar_ValidacionNegocio(string strValidacion, string strProcedimiento, int iActivo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strValidacion, strProcedimiento, iActivo };
                DbCommand cmd = db.GetStoredProcCommand("VAB_INSERTAR_VALIDACION_NEGOCIO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Actualiza los datos de la validacion del negocio
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="strValidacion"></param>
        /// <param name="strProcedimiento"></param>
        public void Actualizar_ValidacionNegocio(int iID, string strValidacion, string strProcedimiento, int iActivo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strValidacion, strProcedimiento, iActivo };
                DbCommand cmd = db.GetStoredProcCommand("VAB_UPDATE_VALIDACION_NEGOCIO", parametros);
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
        public void Eliminar_ValidacionNegocio(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("VAB_DELETE_VALIDACION_NEGOCIO", parametros);
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
