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
    public class PosFitoDominanteDalc
    {
        private Configuracion objConfiguracion;

        public PosFitoDominanteDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<PosFitoDominanteEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                PosFitoDominanteEntity _objDatos;
                List<PosFitoDominanteEntity> listaDatos = new List<PosFitoDominanteEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPO_POS_FITOSOC_DOM_ECOTERR");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new PosFitoDominanteEntity();
                    _objDatos["EFS_ID"] = dr["EFS_ID"].ToString();
                    _objDatos["EFS_TIPO_POS_FITOSOC_DOM"] = dr["EFS_TIPO_POS_FITOSOC_DOM"].ToString();
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
