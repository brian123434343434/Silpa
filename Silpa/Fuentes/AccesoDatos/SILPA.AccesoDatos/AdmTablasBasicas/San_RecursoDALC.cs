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
    public class San_RecursoDALC
    {
        private Configuracion objConfiguracion;

        public San_RecursoDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region SanRecurso
        /// <summary>
        /// Lista la informacion de los Tipos de Sancion
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable Listar_San_Recurso(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_SAN_RECURSO", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }

            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Registra la descripcion de los tipo de sancion
        /// </summary>
        /// <param name="strDescripcion"></param>
        public void Insertar_San_Recurso(string strDescripcion, decimal deEstado)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion, deEstado };
                DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_SAN_RECURSO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Actualizar los tipo de falta
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        /// <param name="strDescripcion">Descripcion adquisicion</param>
        public void Actualizar_San_Recurso(decimal iID, string strDescripcion, decimal deEstado)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strDescripcion, deEstado };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_SAN_RECURSO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }


        /// <summary>
        /// Eliminar la informacion de los tipos de falta
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        public void Eliminar_San_Recurso(decimal iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_SAN_RECURSO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        #endregion
    }


}
