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
    public class Tipo_SalvoconductoDALC
    {
        private Configuracion objConfiguracion;
        public Tipo_SalvoconductoDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "TIPO_DOCUMENTO"

        /// <summary>
        /// Listar la informacion del tipo de salvoconducto
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Tipo_Salvoconducto(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase (objConfiguracion.SilpaCnx.ToString());
                object [] parametros = new object [] {strDescripcion};
                DbCommand cmd = db.GetStoredProcCommand("LISTAR_TIPO_SALVOCONDUCTO", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Resgistra un nuevo tipo de salvoconducto
        /// </summary>
        /// <param name="strNombre"></param>
        /// <param name="boActivo"></param>
        public void Insertar_Tipo_Salvoconducto(string strNombre, bool boActivo)
        {
            try
            {
                SqlDatabase  db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strNombre, boActivo };
                DbCommand cmd = db.GetStoredProcCommand("INSERTAR_TIPO_SALVOCONDUCTO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Actualiza un tipo de salvoconducto
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="strNombre"></param>
        /// <param name="boActivo"></param>
        public void Actualizar_Tipo_Salvoconducto(int iID, string strNombre, bool boActivo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strNombre, boActivo };
                DbCommand cmd = db.GetStoredProcCommand("ACTUALIZAR_TIPO_SALVOCONDUCTO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Elimina un tipo de salvoconducto
        /// </summary>
        /// <param name="iID">Id del registro</param>
        public void Eliminar_Tipo_Salvoconducto(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("ELIMINAR_TIPO_SALVOCONDUCTO", parametros);
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
