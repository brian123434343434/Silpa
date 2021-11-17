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
    public class EstadoCorreoDALC
    {
        private Configuracion objConfiguracion;
        public EstadoCorreoDALC ()
        {
            objConfiguracion = new Configuracion();
        }
        #region "ESTADO_CORREO"

        /// <summary>
        /// Listar la informacion de los estados de los correos
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Estado_Correo(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase (objConfiguracion.SilpaCnx.ToString());
                object [] parametros = new object [] {strDescripcion};
                DbCommand cmd = db.GetStoredProcCommand("SMB_LISTA_CORREO_ESTADO", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Registro en nuevo estado para el envio de los correos electronicos
        /// </summary>
        /// <param name="strNombreEstado">Nombre del nuevo estado</param>
        public void Insertar_Estado_Correo(string strNombreEstado)
        {
            try
            {
                SqlDatabase  db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object []{strNombreEstado};
                DbCommand cmd = db.GetStoredProcCommand ("SMB_INSERTAR_CORREO_ESTADO", parametros );
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Actualiza el nombre del estado del correo
        /// </summary>
        /// <param name="iID">ID del estado del correo</param>
        /// <param name="strNombreEstado">Nuevo nombre del estado del correo</param>
        public void Actualizar_Estado_Correo(int iID, string strNombreEstado)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strNombreEstado };
                DbCommand cmd = db.GetStoredProcCommand ("SMB_ACTUALIZAR_CORREO_ESTADO",parametros );
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Elimina el registro del estado del correo
        /// </summary>
        /// <param name="iID">Id del registro</param>
        public void Eliminar_Estado_Correo(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("SMB_ELIMINAR_CORREO_ESTADO", parametros);
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
