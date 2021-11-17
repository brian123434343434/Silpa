using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Parametrizacion
{
    public class TiempoNotificacionDalc
    {
        private Configuracion objConfiguracion = new Configuracion();

        public TiempoNotificacionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        ///Actualiza el Tiempo para Consultar Notificación en Línea - PDI
        /// </summary>
        /// <param name="tiempo">Tiempo</param>
        public void Actualizar(string tiempo, int activo)
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { tiempo, activo };
            DbCommand cmd = db.GetStoredProcCommand("NOT_UPDATE_TIEMPO", parametros);
            try
            {

                db.ExecuteNonQuery(cmd);
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        /// <summary>
        /// Retorna el tiempo actual de consulta de notificación
        /// </summary>
        /// <returns>Tiempo<TipoTramite></returns>
        public string ObtenerTiempo(out int activo)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("NOT_SELECT_TIEMPO");
            activo = 0;
            try
            {
                DataSet dtResultado = db.ExecuteDataSet(cmd);
                if (dtResultado != null)
                {
                    if (dtResultado.Tables[0].Rows.Count > 0) 
                    {
                        activo = int.Parse(dtResultado.Tables[0].Rows[0]["Activo"].ToString());
                        return Convert.ToString(dtResultado.Tables[0].Rows[0]["Tiempo"]);
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al retornar el tiempo actual de consulta de notificación.";
                throw new Exception(strException, ex);
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }



    }
}
