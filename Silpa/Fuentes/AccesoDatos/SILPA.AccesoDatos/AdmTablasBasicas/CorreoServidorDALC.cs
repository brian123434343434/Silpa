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
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;

namespace SILPA.AccesoDatos.AdmTablasBasicas
{
    public class CorreoServidorDALC
    {
        private Configuracion objConfiguracion;
        public CorreoServidorDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "CORREO_SERVIDOR"

        /// <summary>
        /// Listar los servidores de correo
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Servidor_Correo(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("SMB_LISTA_SERVIDOR_CORREO", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Registra un nuevo servidor de envio de correo
        /// </summary>
        /// <param name="strNombreServidor">Nombre del servidor</param>
        /// <param name="strHost">Direccion IP del servidor de correo</param>
        /// <param name="strUsuario">Cuento de quien envia el correo</param>
        /// <param name="strContrasena">Contraseña del correo remitente</param>
        /// <param name="strSeparador">Separador configurado</param>
        public void Insertar_Servidor_Correo(string strNombreServidor, string strHost, string strUsuario, string strContrasena, string strSeparador, int Puerto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {strNombreServidor, strHost, strUsuario, strContrasena, strSeparador, Puerto};
                DbCommand cmd = db.GetStoredProcCommand("SMB_INSERTAR_SERVIDOR_CORREO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Actualiza la informacion del servidor de correo
        /// </summary>
        /// <param name="strNombreServidor">Nombre del servidor</param>
        /// <param name="strHost">Direccion ip del servidor de correo</param>
        /// <param name="strUsuario">Correo de quien envia</param>
        /// <param name="strContrasena">Contraseña del correo remitente</param>
        /// <param name="strSeparador">separador</param>
        public void Actualizar_Servidor_Correo(int iID, string strNombreServidor, string strHost, string strUsuario, string strContrasena, string strSeparador, int Puerto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {iID, strNombreServidor, strHost, strUsuario, strContrasena, strSeparador, Puerto};
                DbCommand cmd = db.GetStoredProcCommand("SMB_ACTUALIZAR_SERVIDOR_CORREO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Elimina registro de servidor de correo
        /// </summary>
        /// <param name="iID">Id del registro</param>
        public void Eliminar_Servidor_Correo(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("SMB_ELIMINAR_SERVIDOR_CORREO", parametros);
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
