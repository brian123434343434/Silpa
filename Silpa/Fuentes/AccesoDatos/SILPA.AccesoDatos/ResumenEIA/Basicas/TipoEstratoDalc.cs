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
    public class TipoEstratoDalc
    {
        private Configuracion objConfiguracion;

        public TipoEstratoDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoEstratoEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoEstratoEntity _objDatos;
                List<TipoEstratoEntity> listaDatos = new List<TipoEstratoEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPO_ESTRATO_ECOTERR");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoEstratoEntity();
                    _objDatos["ETE_ID"] = dr["ETE_ID"].ToString();
                    _objDatos["ETE_TIPO_ESTRATO_ECOTERR"] = dr["ETE_TIPO_ESTRATO_ECOTERR"].ToString();
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
