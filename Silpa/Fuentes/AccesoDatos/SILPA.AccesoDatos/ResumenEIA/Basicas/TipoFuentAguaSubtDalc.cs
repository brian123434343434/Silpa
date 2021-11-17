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
    public class TipoFuentAguaSubtDalc
    {
        private Configuracion objConfiguracion;

        public TipoFuentAguaSubtDalc()
        {
            objConfiguracion = new Configuracion();
        }


        public List<TipoFuentAguaSubtEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoFuentAguaSubtEntity _objDatos;
                List<TipoFuentAguaSubtEntity> listaDatos = new List<TipoFuentAguaSubtEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPOS_FUENTES_AGUA_SUBT");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoFuentAguaSubtEntity();
                    _objDatos["ETS_ID"] = dr["ETS_ID"].ToString();
                    _objDatos["ETS_TIPO_AGUA_SUBT"] = dr["ETS_TIPO_AGUA_SUBT"].ToString();
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
