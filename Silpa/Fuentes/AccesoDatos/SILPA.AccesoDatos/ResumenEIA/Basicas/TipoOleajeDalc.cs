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
    public class TipoOleajeDalc
    {
        private Configuracion objConfiguracion;

        public TipoOleajeDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoOleajeEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoOleajeEntity _objDatos;
                List<TipoOleajeEntity> listaDatos = new List<TipoOleajeEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPO_OLEAJE");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoOleajeEntity();
                    _objDatos["ETO_ID"] = dr["ETO_ID"].ToString();
                    _objDatos["ETO_TIPO_OLEAJE"] = dr["ETO_TIPO_OLEAJE"].ToString();
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
