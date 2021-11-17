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
    public class TipoEspecieMarinaDalc
    {
        private Configuracion objConfiguracion;

        public TipoEspecieMarinaDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoEspecieMarinaEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoEspecieMarinaEntity _objDatos;
                List<TipoEspecieMarinaEntity> listaDatos = new List<TipoEspecieMarinaEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPO_ESPECIES_MARINA");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoEspecieMarinaEntity();
                    _objDatos["ETM_ID"] = dr["ETM_ID"].ToString();
                    _objDatos["ETM_TIPO_ESPECIE_MARINA"] = dr["ETM_TIPO_ESPECIE_MARINA"].ToString();
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
