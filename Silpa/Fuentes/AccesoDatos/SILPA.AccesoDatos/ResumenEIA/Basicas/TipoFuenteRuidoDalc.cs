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
    public class TipoFuenteRuidoDalc
    {
        private Configuracion objConfiguracion;

        public TipoFuenteRuidoDalc()
        {
            objConfiguracion = new Configuracion();
        }


        public List<TipoFuenteRuidoEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoFuenteRuidoEntity _objDatos;
                List<TipoFuenteRuidoEntity> listaDatos = new List<TipoFuenteRuidoEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPOS_FUENT_RUIDO");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoFuenteRuidoEntity();
                    _objDatos["ETR_ID"] = dr["ETR_ID"].ToString();
                    _objDatos["ETR_TIPO_FUENT_RUIDO"] = dr["ETR_TIPO_FUENT_RUIDO"].ToString();
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
