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
    public class WSB_ServiciosDALC
    {
        private Configuracion objConfiguracion;
        public WSB_ServiciosDALC()
        {
            objConfiguracion = new Configuracion();
        }
        #region "WSB_METODO"

        /// <summary>
        /// Listar la informacion de los servicios web
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Servicios(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("WSB_LISTAR_SERVICIO", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Resgistra un nuevo servicio web
        /// </summary>
        /// <param name="strDescripcion"></param>
        /// <param name="strSeparador"></param>
        public void Insertar_Servicios(string strNombre, string strURL, byte bActivo, int iPrioridad)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strNombre, strURL, bActivo, iPrioridad };
                DbCommand cmd = db.GetStoredProcCommand("WSB_INSERTAR_SERVICIO", parametros);
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
        public void Actualizar_Servicios(int iID, string strNombre, string strURL, byte bActivo, int iPrioridad)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strNombre, strURL, bActivo, iPrioridad };
                DbCommand cmd = db.GetStoredProcCommand("WSB_ACTUALIZAR_SERVICIO", parametros);
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
        public void Eliminar_Servicios(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("WSB_ELIMINAR_SERVICIO", parametros);
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
