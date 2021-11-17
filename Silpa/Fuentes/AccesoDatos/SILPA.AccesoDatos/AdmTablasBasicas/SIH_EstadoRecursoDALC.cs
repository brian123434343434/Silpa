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
    public class SIH_EstadoRecursoDALC
    {
        private Configuracion objConfiguracion;
        public SIH_EstadoRecursoDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "ESTADO_RECURSO"

        /// <summary>
        /// Listar la informacion de la tabla SIH_Estado_Recurso
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Estado_Recurso(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase (objConfiguracion.SilpaCnx.ToString());
                object [] parametros = new object [] {strDescripcion};
                DbCommand cmd = db.GetStoredProcCommand("SIH_LISTA_ESTADO_RECURSO", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Registro de un nuevo estado de recurso
        /// </summary>
        /// <param name="strNombre"></param>
        /// <param name="bActivo"></param>
        public void Insertar_Estado_Recurso(string strNombreEstado, bool bActivo)
        {
            try
            {
                SqlDatabase  db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strNombreEstado, bActivo };
                DbCommand cmd = db.GetStoredProcCommand("SIH_INSERTAR_ESTADO_RECURSO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Actualiza erl estado de recurso
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="strNombreEstado"></param>
        /// <param name="bActivo"></param>
        public void Actualizar_Estado_Recurso(int iID, string strNombreEstado, bool bActivo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strNombreEstado, bActivo };
                DbCommand cmd = db.GetStoredProcCommand("SIH_ACTUALIZAR_ESTADO_RECURSO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Elimina el registro del estado del recurso
        /// </summary>
        /// <param name="iID">Id del registro</param>
        public void Eliminar_Estado_Recurso(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("SIH_ELIMINAR_ESTADO_RECURSO", parametros);
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
