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
    public class TipoEspeciesFaunaDalc
    {
        private Configuracion objConfiguracion;

        public TipoEspeciesFaunaDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoEspeciesFaunaEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoEspeciesFaunaEntity _objDatos;
                List<TipoEspeciesFaunaEntity> listaDatos = new List<TipoEspeciesFaunaEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPO_ESPECIES_FAUNA");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoEspeciesFaunaEntity();
                    _objDatos["ETF_ID"] = dr["ETF_ID"].ToString();
                    _objDatos["ETF_TIPO_ESPECIE"] = dr["ETF_TIPO_ESPECIE"].ToString();
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
