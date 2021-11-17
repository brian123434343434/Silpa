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
    public class TipoMuestraDalc
    {
        private Configuracion objConfiguracion;

        public TipoMuestraDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoMuestraEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoMuestraEntity _objDatos;
                List<TipoMuestraEntity> listaDatos = new List<TipoMuestraEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_LISTA_TIPO_MUESTRA");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoMuestraEntity();
                    _objDatos["ETM_ID"] = dr["ETM_ID"].ToString();
                    _objDatos["ETM_TIPO_MUESTRA"] = dr["ETM_TIPO_MUESTRA"].ToString();
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
