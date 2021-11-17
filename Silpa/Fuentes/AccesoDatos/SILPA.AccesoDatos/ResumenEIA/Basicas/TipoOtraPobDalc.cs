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
    public class TipoOtraPobDalc
    {
        private Configuracion objConfiguracion;

        public TipoOtraPobDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoOtraPobEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoOtraPobEntity _objDatos;
                List<TipoOtraPobEntity> listaDatos = new List<TipoOtraPobEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPO_OTRA_POB");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoOtraPobEntity();
                    _objDatos["ETO_ID"] = dr["ETO_ID"].ToString();
                    _objDatos["ETO_TIPO_OTRA_POBLACION"] = dr["ETO_TIPO_OTRA_POBLACION"].ToString();
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
