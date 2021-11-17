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
    public class TipoInstitucionDalc
    {
        private Configuracion objConfiguracion;

        public TipoInstitucionDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoInstitucionEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoInstitucionEntity _objDatos;
                List<TipoInstitucionEntity> listaDatos = new List<TipoInstitucionEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPO_INSTITUCION");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoInstitucionEntity();
                    _objDatos["ETI_ID"] = dr["ETI_ID"].ToString();
                    _objDatos["ETI_TIPO_INSTITUCION"] = dr["ETI_TIPO_INSTITUCION"].ToString();
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
