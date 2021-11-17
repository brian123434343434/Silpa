using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.LOG
{
    public class SMHLogDALC
    {
        private Configuracion objConfiguracion;
        public SMHLogDALC()
        {
            objConfiguracion = new Configuracion();
        }

        public DataTable ConsultaLogDALC(String  strFechaIni, String strFechaFin, String  strUsuarios, String strMaquina, Int32 iSeveridad)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strFechaIni, strFechaFin, strUsuarios, strMaquina, iSeveridad };
                DbCommand cmd = db.GetStoredProcCommand("SMH_CONSULTA_LOG", parametros);
                cmd.CommandTimeout = 3000;
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);

            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        public DataTable ConsultaSeveridadDALC()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SMH_CONSULTA_SEVERIDAD");
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        
        }

        public bool EliminaLog(bool truncarTabla)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros;
                if (truncarTabla)
                    parametros = new object[] {0};
                else
                    parametros = new object[] {-1};
                DbCommand cmd = db.GetStoredProcCommand("SMH_ELIMINA_LOG");
                db.ExecuteNonQuery(cmd);
                return true;
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }
    }
}
