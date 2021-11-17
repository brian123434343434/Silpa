using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    public class TIpoNotificacionDalc
    {
          /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public TIpoNotificacionDalc()
        {
            objConfiguracion = new Configuracion();
        }
        /// <summary>
        /// Lista de los tipos de notificacion
        /// </summary>
        /// <returns></returns>
        public List<TipoNotificacionEntity> ListarTiposNotificacion()
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            TipoNotificacionEntity tipo;
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_TIPO_NOTIFICACION");
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<TipoNotificacionEntity> lista;

            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    lista = new List<TipoNotificacionEntity>();
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        tipo = new TipoNotificacionEntity();

                        tipo.TipoNotificacionID = Convert.ToInt32(dt["ID_TIPO_NOTIFICACION"]);
                        tipo.TipoNotificacion = dt["NOMBRE"].ToString();
                        lista.Add(tipo);
                    }
                    return lista;
                }
                return null;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }
    }
}
