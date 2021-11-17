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
    public class EstadosProcesoDALC
    {
        private Configuracion objConfiguracion;
        public EstadosProcesoDALC()
        {
            objConfiguracion = new Configuracion();
        }
        #region "ESTADOS_PROCESO"

        /// <summary>
        /// Listar la informacion de los estados de los procesos
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Estados_Proceso(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase (objConfiguracion.SilpaCnx.ToString());
                object [] parametros = new object [] {strDescripcion};
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTAR_ESTADO_PROCESO", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Inserta un nuevo estado de los procesos
        /// </summary>
        /// <param name="strNombreEstado"></param>
        /// <param name="strDescripcion"></param>
        /// <param name="byActivo"></param>
        public void Insertar_Estados_Proceso(string strNombreEstado, string strDescripcion, byte byActivo)
        {
            try
            {
                SqlDatabase  db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object []{strNombreEstado, strDescripcion, byActivo};
                DbCommand cmd = db.GetStoredProcCommand("GEN_INSERTAR_ESTADO_PROCESO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Actualiza el registro del estados de los procesos
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="strNombreEstado"></param>
        /// <param name="strDescripcion"></param>
        /// <param name="byActivo"></param>
        public void Actualizar_Estados_Proceso(int iID, string strNombreEstado, string strDescripcion, byte byActivo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strNombreEstado, strDescripcion, byActivo};
                DbCommand cmd = db.GetStoredProcCommand("GEN_ACTUALIZAR_ESTADO_PROCESO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Elimina el registro del estado de los correos
        /// </summary>
        /// <param name="iID">Id del registro</param>
        public void Eliminar_Estados_Proceso(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("GEN_ELIMINAR_ESTADO_PROCESO", parametros);
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
