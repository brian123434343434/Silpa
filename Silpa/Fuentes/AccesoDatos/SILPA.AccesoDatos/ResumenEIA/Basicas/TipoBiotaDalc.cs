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
    public class TipoBiotaDalc
    {
        private Configuracion objConfiguracion;

        public TipoBiotaDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoBiotaEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoBiotaEntity _objDatos;
                List<TipoBiotaEntity> listaDatos = new List<TipoBiotaEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPO_BIOTA");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoBiotaEntity();
                    _objDatos["ETB_ID"] = dr["ETB_ID"].ToString();
                    _objDatos["ETB_TIPO_BIOTA"] = dr["ETB_TIPO_BIOTA"].ToString();
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
