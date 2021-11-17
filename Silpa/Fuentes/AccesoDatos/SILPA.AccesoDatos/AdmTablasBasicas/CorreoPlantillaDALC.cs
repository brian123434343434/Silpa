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
    public class CorreoPlantillaDALC
    {
        private Configuracion objConfiguracion;
        public CorreoPlantillaDALC ()
        {
            objConfiguracion = new Configuracion();
        }

        #region "ESTADO_CORREO"

        /// <summary>
        /// Listar las plantillas de los correos
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
        /// <returns></returns>
        public DataTable Listar_Correo_Plantilla(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase (objConfiguracion.SilpaCnx.ToString());
                object [] parametros = new object [] {strDescripcion};
                DbCommand cmd = db.GetStoredProcCommand("SMB_LISTA_CORREO_PLANTILLA", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Funcion para consultar la tabla de servidores y cargar el combo
        /// </summary>
        /// <returns></returns>
        public DataTable Carga_Combo_Servidores()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SMB_CARGA_COMBO_SERVIDOR");
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Registra un nueva plantilla de correo
        /// </summary>
        /// <param name="strDe">Quien envia el correo</param>
        /// <param name="strCC">A quien se le envia el correo</param>
        /// <param name="strPlantilla">Plantilla del correo</param>
        /// <param name="strAsunto">Asunto del correo</param>
        /// <param name="iIdCorreoServidor">Id del servidor por donde se envia el correo</param>
        /// <param name="iConfirmarEnvio">Id de confirmacion de envio del correo</param>
        public void Insertar_Correo_Plantilla(string strDe, string strCC, string strPlantilla, string strAsunto, Int32 iIdCorreoServidor, Int32 iConfirmarEnvio)
        {
            try
            {
                SqlDatabase  db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object []{strDe ,strCC ,strPlantilla ,strAsunto ,iIdCorreoServidor ,iConfirmarEnvio};
                DbCommand cmd = db.GetStoredProcCommand("SMB_INSERTAR_CORREO_PLANTILLA", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }

        }

        /// <summary>
        /// Actualiza la plantilla del correo
        /// </summary>
        /// <param name="iID">Id de la plantilla</param>
        /// <param name="strDe">Quien envia el correo</param>
        /// <param name="strCC"> A quien le envia el correo</param>
        /// <param name="strPlantilla">Plantilla del correo</param>
        /// <param name="strAsunto">Asunto del correo</param>
        /// <param name="iIdCorreoServidor">Id del servidor por donde se envia el correo</param>
        /// <param name="iConfirmarEnvio">confirma si desea enviar o no el correo</param>
        public void Actualizar_Correo_Plantilla(int iID, string strDe, string strCC, string strPlantilla, string strAsunto, Int32 iIdCorreoServidor, Int32 iConfirmarEnvio)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strDe, strCC, strPlantilla, strAsunto, iIdCorreoServidor, iConfirmarEnvio};
                DbCommand cmd = db.GetStoredProcCommand("SMB_ACTUALIZAR_CORREO_PLANTILLA", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Elimina la plantilla del correo
        /// </summary>
        /// <param name="iID">Id de la plantilla</param>
        public void Eliminar_Correo_Plantilla(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("SMB_ELIMINAR_CORREO_PLANTILLA", parametros);
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
