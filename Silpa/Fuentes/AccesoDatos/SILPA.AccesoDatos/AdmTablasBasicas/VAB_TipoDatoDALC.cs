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
    public class VAB_TipoDatoDALC
    {
        private Configuracion objConfiguracion;
        public VAB_TipoDatoDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "Tipo_Dato"

        /// <summary>
        /// Listar la informacion de los tipos de datos
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Tipo_Datos(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("VAB_LISTAR_TIPO_DATO", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Resgistra un nuevo tipo de dato
        /// </summary>
        /// <param name="strDescripcion"></param>
        /// <param name="strSeparador"></param>
        public void Insertar_Tipo_Datos(string strDescripcion, string strSeparador)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {strDescripcion, strSeparador};
                DbCommand cmd = db.GetStoredProcCommand("VAB_INSERTAR_TIPO_DATO", parametros);
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
        public void Actualizar_Tipo_Datos(int iID, string strDescripcion, string strSeparador)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {iID, strDescripcion, strSeparador};
                DbCommand cmd = db.GetStoredProcCommand("VAB_ACTUALIZAR_TIPO_DATO", parametros);
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
        public void Eliminar_Tipo_Datos(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("VAB_ELIMINAR_TIPO_DATO", parametros);
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
