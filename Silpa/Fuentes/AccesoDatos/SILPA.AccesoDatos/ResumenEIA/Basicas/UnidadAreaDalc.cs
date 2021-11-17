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
    public class UnidadAreaDalc
    {
        private Configuracion objConfiguracion;

        public UnidadAreaDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<UnidadAreaEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                UnidadAreaEntity _objDatos;
                List<UnidadAreaEntity> listaDatos = new List<UnidadAreaEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_UNIDADES_AREA");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new UnidadAreaEntity();
                    _objDatos["EUA_ID"] = dr["EUA_ID"].ToString();
                    _objDatos["EUA_UNIDAD_AREA"] = dr["EUA_UNIDAD_AREA"].ToString();
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
