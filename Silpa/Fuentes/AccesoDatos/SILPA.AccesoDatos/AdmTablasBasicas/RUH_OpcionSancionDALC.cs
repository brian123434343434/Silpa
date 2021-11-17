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
    public class RUH_OpcionSancionDALC
    {
        private Configuracion objConfiguracion;
        public RUH_OpcionSancionDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "opcion_sancion"

        /// <summary>
        /// Listar la informacion de las opciones de sancion
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Opcion_Sancion(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("RUH_LISTAR_OPCION_SANCION", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Resgistra un nuevo registro en la tabla RUH_Opcion_Sancion
        /// </summary>
        /// <param name="strDescripcion"></param>
        /// <param name="strSeparador"></param>
        public void Insertar_Opcion_Sancion(string strNombre, bool bActivo, int iDias)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strNombre, bActivo, iDias };
                DbCommand cmd = db.GetStoredProcCommand("RUH_INSERTAR_OPCION_SANCION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Actualiza el registro del tipo de dato
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="strDescripcion"></param>
        /// <param name="strSeparador"></param>
        public void Actualizar_Opcion_Sancion(int iID, string strNombre, bool bActivo, byte byDias)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strNombre, bActivo, byDias };
                DbCommand cmd = db.GetStoredProcCommand("RUH_ACTUALIZAR_OPCION_SANCION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Elimina el registro de tipo de dato
        /// </summary>
        /// <param name="iID">Id del registro</param>
        public void Eliminar_Opcion_Sancion(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("RUH_ELIMINAR_OPCION_SANCION", parametros);
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
