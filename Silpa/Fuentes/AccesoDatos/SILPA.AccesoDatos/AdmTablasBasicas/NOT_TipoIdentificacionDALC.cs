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
    public class NOT_TipoIdentificacionDALC
    {
        private Configuracion objConfiguracion;
        public NOT_TipoIdentificacionDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "TIPO_IDENTIFICACION"

        /// <summary>
        /// Listar la informacion de los tipos de documento
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Tipo_Identificacion(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase (objConfiguracion.SilpaCnx.ToString());
                object [] parametros = new object [] {strDescripcion};
                DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_TIPO_IDENTIFICACION", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Consultar la información de un tipo de identificación
        /// </summary>
        /// <param name="intTipoIdentificacionID">int con el identificador del tipo de identificación</param>
        /// <returns>DataTable con la información del tipo de identificacion</returns>
        public DataTable Consultar_Tipo_Identificacion(int intTipoIdentificacionID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intTipoIdentificacionID };
                DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTAR_TIPO_IDENTIFICACION", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Registra un nuevo tipo de documento
        /// </summary>
        /// <param name="strCodigoTipo">Codigo del Tipo de Documento</param>
        /// <param name="strNombreTipo">Nombre del Tipo de Documento</param>
        public void Insertar_Tipo_Identificacion(string strCodigoTipo, string strNombreTipo)
        {
            try
            {
                SqlDatabase  db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strCodigoTipo, strNombreTipo };
                DbCommand cmd = db.GetStoredProcCommand("NOT_INSERTAR_TIPO_IDENTIFICACION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Actualiza informcación del tipo de documento
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="strCodigoTipo"></param>
        /// <param name="strNombreTipo"></param>
        public void Actualizar_Tipo_Identificacion(int iID, string strCodigoTipo, string strNombreTipo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strCodigoTipo, strNombreTipo };
                DbCommand cmd = db.GetStoredProcCommand("NOT_ACTUALIZAR_TIPO_IDENTIFICACION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Elimina el registro del tipo de documento
        /// </summary>
        /// <param name="iID">Id del registro</param>
        public void Eliminar_Tipo_Identificacion(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("NOT_ELIMINAR_TIPO_IDENTIFICACION", parametros);
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
