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
    public class TipoVarClimaticaDalc
    {
        private Configuracion objConfiguracion;

        public TipoVarClimaticaDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoVarClimaticaEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoVarClimaticaEntity _objDatos;
                List<TipoVarClimaticaEntity> listaDatos = new List<TipoVarClimaticaEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_LISTA_TIPO_VAR_CLIMATICA");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoVarClimaticaEntity();
                    _objDatos["ETV_ID"] = dr["ETV_ID"].ToString();
                    _objDatos["ETV_TIPO_VAR_CLIMATICA"] = dr["ETV_TIPO_VAR_CLIMATICA"].ToString();
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
