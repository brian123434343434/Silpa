using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.Common;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoEcosistemaDalc
    {
        private Configuracion objConfiguracion;

        public TipoEcosistemaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista los ecosistemas
        /// </summary>
        public List<TipoEcosistemaEntity> Listar(int? idClasEcosistema)
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoEcosistemaEntity _objDatos;
                List<TipoEcosistemaEntity> listaDatos = new List<TipoEcosistemaEntity>();
                /*
                 * Se pasan parámetros nulos para obtener todos los departamentos
                 */
                object[] parametros = new object[] { idClasEcosistema };
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPO_ECOSIST_TERRESTRE", parametros);                
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoEcosistemaEntity();
                    _objDatos["ETE_ID"] = dr["ETE_ID"].ToString();
                    _objDatos["ETE_TIPO_ECOSISTEMA_TERRESTRE"] = dr["ETE_TIPO_ECOSISTEMA_TERRESTRE"].ToString();
                    listaDatos.Add(_objDatos);
                }
                return listaDatos;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
            finally
            {
                dr = null;
                db = null;
            }
        }
    }
}
