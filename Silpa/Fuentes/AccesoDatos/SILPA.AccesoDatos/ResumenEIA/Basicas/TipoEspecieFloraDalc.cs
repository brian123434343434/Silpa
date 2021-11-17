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
    public class TipoEspecieFloraDalc
    {
        private Configuracion objConfiguracion;

        public TipoEspecieFloraDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoEspecieFloraEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoEspecieFloraEntity _objDatos;
                List<TipoEspecieFloraEntity> listaDatos = new List<TipoEspecieFloraEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPO_ESPECIE_FLORA");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoEspecieFloraEntity();
                    _objDatos["EEF_ID"] = dr["EEF_ID"].ToString();
                    _objDatos["EEF_TIPO_ESPECIE"] = dr["EEF_TIPO_ESPECIE"].ToString();
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
