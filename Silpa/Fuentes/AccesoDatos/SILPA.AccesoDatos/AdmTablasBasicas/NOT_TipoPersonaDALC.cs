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
    public class NOT_TipoPersonaDALC
    {
        private Configuracion objConfiguracion;
        public NOT_TipoPersonaDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "TIPO_PERSONA"

        /// <summary>
        /// Listar la informacion de la tabla NOT_Tipo_Personas
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Tipo_Personas(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("NOT_LISTA_TIPO_PERSONA", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Registra un nuevo tipo de persona
        /// </summary>
        /// <param name="strCodigoTipo">Codigo del Tipo de Documento</param>
        /// <param name="strNombreTipo">Nombre del Tipo de Documento</param>
        public void Insertar_Tipo_Personas(string strCodigoTipo, string strNombreTipo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strCodigoTipo, strNombreTipo };
                DbCommand cmd = db.GetStoredProcCommand("NOT_INSERTAR_TIPO_PERSONA", parametros);
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
        public void Actualizar_Tipo_Personas(int iID, string strCodigoTipo, string strNombreTipo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strCodigoTipo, strNombreTipo };
                DbCommand cmd = db.GetStoredProcCommand("NOT_ACTUALIZAR_TIPO_PERSONA", parametros);
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
        public void Eliminar_Tipo_Personas(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("NOT_ELIMINAR_TIPO_PERSONA", parametros);
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
