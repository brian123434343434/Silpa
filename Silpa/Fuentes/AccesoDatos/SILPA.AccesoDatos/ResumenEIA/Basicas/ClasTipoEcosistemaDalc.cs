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
    public class ClasTipoEcosistemaDalc
    {
        private Configuracion objConfiguracion;

        public ClasTipoEcosistemaDalc()
        {
            objConfiguracion = new Configuracion();
        }


        public List<ClasTipoEcosistemaEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                ClasTipoEcosistemaEntity _objDatos;
                List<ClasTipoEcosistemaEntity> listaDatos = new List<ClasTipoEcosistemaEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_EIB_CLASIFI_ECOSIST_TERR");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new ClasTipoEcosistemaEntity();
                    _objDatos["ECE_ID"] = dr["ECE_ID"].ToString();
                    _objDatos["ECE_CLASIFICACION_TERRESTRE"] = dr["ECE_CLASIFICACION_TERRESTRE"].ToString();
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
