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
    public class TipoOlaDalc
    {
        private Configuracion objConfiguracion;

        public TipoOlaDalc()
        {
            objConfiguracion = new Configuracion();
        }


        public List<TipoOlaEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoOlaEntity _objDatos;
                List<TipoOlaEntity> listaDatos = new List<TipoOlaEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPO_OLAS");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoOlaEntity();
                    _objDatos["EOO_ID"] = dr["EOO_ID"].ToString();
                    _objDatos["EOO_TIPO_OLAS"] = dr["EOO_TIPO_OLAS"].ToString();
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
