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
    public class LogicaEstadosDALC
    {
        private Configuracion objConfiguracion;
        public LogicaEstadosDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "LOGICA_ESTADOS"

        /// <summary>
        /// Listar la tabla de logica de estados
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Logica_Estados(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase (objConfiguracion.SilpaCnx.ToString());
                object [] parametros = new object [] {strDescripcion};
                DbCommand cmd = db.GetStoredProcCommand("DAA_LISTA_LOGICA_ESTADOS", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Funcion para consultar la tabla de tipo de estados de los procesos
        /// </summary>
        /// <returns></returns>
        public DataTable Carga_Combo_Estados()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("GEN_CARGA_COMBO_ESTADOS");
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Inserta registros en la tabla logica estados
        /// </summary>
        /// <param name="boPertenece"></param>
        /// <param name="boRequiere"></param>
        /// <param name="boConflicto"></param>
        /// <param name="boFijos"></param>
        /// <param name="iEstado"></param>
        public void Insertar_Logica_Estados(bool boPertenece, bool boRequiere, bool boConflicto, bool boFijos, Int32 iEstado)
        {
            try
            {
                SqlDatabase  db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object []{boPertenece, boRequiere, boConflicto, boFijos, iEstado};
                DbCommand cmd = db.GetStoredProcCommand("DAA_INSERTAR_LOGICA_ESTADOS", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Actualiza el registro de la tabla logica estados
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="boPertenece"></param>
        /// <param name="boRequiere"></param>
        /// <param name="boConflicto"></param>
        /// <param name="boFijos"></param>
        /// <param name="iEstado"></param>
        public void Actualizar_Logica_Estados(int iID, bool boPertenece, bool boRequiere, bool boConflicto, bool boFijos, Int32 iEstado)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, boPertenece, boRequiere, boConflicto, boFijos, iEstado};
                DbCommand cmd = db.GetStoredProcCommand("DAA_ACTUALIZAR_LOGICA_ESTADOS", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Elimina el registro de la tabla
        /// </summary>
        /// <param name="iID">Id de la plantilla</param>
        public void Eliminar_Logica_Estados(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("DAA_ELIMINAR_LOGICA_ESTADOS", parametros);
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
