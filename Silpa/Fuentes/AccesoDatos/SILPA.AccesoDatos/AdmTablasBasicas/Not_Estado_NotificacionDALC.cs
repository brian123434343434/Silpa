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
    public class Not_Estado_NotificacionDALC
    {
        private Configuracion objConfiguracion;
        public Not_Estado_NotificacionDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "NOT_ESTADO_NOTIFICACION"

        /// <summary>
        /// Listar la informacion de los tipos de persona
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion</param>
        /// <returns></returns>
        public DataTable Listar_Not_Estado_Notificacion(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_NOT_ESTADO_NOTIFICACION", parametros);
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
        /// Registra un nuevo estado de notificaci�n
        /// </summary>
        /// <param name="strDescripcion">string con el nombre del estado</param>
        /// <param name="byEstado">byte con el estado del estado de notificaci�n</param>
        /// <param name="byEstadoPDI">byte con el estado pdi</param>
        /// <param name="intDiasVencimiento">int con los d�as de vencimiento</param>
        /// <param name="mostrarInfo">bool indicando si se muestra informai�n</param>
        /// <param name="enviaCorreo">bool indicando si se env�a correo</param>
        /// <param name="mensajeCorreo">string con el mensaje de correo</param>
        /// <param name="espublico">bool indicando si es publico</param>
        /// <param name="p_strDescripcionMostrar">string con la descripci�n a mostrar</param>
        public void Insertar_Not_Estado_Notificacion(string strDescripcion, byte byEstado, byte byEstadoPDI, int intDiasVencimiento, bool mostrarInfo, bool enviaCorreo, string mensajeCorreo, bool espublico, string p_strDescripcionMostrar)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion, byEstado, byEstadoPDI, intDiasVencimiento, mostrarInfo, enviaCorreo, mensajeCorreo, espublico, p_strDescripcionMostrar };
                DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_NOT_ESTADO_NOTIFICACION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Modifica la informaci�n de un estado de notificaci�n
        /// </summary>
        /// <param name="iID">Identificador del estado a modificar</param>
        /// <param name="strDescripcion">string con el nombre del estado</param>
        /// <param name="byEstado">byte con el estado del estado de notificaci�n</param>
        /// <param name="byEstadoPDI">byte con el estado pdi</param>
        /// <param name="intDiasVencimiento">int con los d�as de vencimiento</param>
        /// <param name="mostrarInfo">bool indicando si se muestra informai�n</param>
        /// <param name="enviaCorreo">bool indicando si se env�a correo</param>
        /// <param name="mensajeCorreo">string con el mensaje de correo</param>
        /// <param name="espublico">bool indicando si es publico</param>
        /// <param name="p_strDescripcionMostrar">string con la descripci�n a mostrar</param>
        public void Actualizar_Not_Estado_Notificacion(int iID, string strDescripcion, byte byEstado, byte byEstadoPDI, int intDiasVencimiento, bool mostrarInfo, bool enviaCorreo, string mensajeCorreo, bool espublico, string p_strDescripcionMostrar)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strDescripcion, byEstado, byEstadoPDI, intDiasVencimiento, mostrarInfo, enviaCorreo, mensajeCorreo, espublico, p_strDescripcionMostrar };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_NOT_ESTADO_NOTIFICACION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Eliminar la informacion de los tipos de persona
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        public void Eliminar_Not_Estado_Notificacion(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_NOT_ESTADO_NOTIFICACION", parametros);
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
