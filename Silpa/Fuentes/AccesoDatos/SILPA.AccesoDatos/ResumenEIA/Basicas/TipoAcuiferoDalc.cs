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
    public class TipoAcuiferoDalc
    {
        private Configuracion objConfiguracion;

        public TipoAcuiferoDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoAcuiferoEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoAcuiferoEntity _objDatos;
                List<TipoAcuiferoEntity> listaDatos = new List<TipoAcuiferoEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_LISTA_TIPO_ACUIFERO");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoAcuiferoEntity();
                    _objDatos["ETA_ID"] = dr["ETA_ID"].ToString();
                    _objDatos["ETA_TIPO_ACUIFERO"] = dr["ETA_TIPO_ACUIFERO"].ToString();
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
