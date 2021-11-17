using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace SILPA.AccesoDatos.Monitoreo
{
    public class MonitoreoEEDalc
    {
        private Configuracion objConfiguracion = new Configuracion();
        private const string _idParametro = "37";

        public MonitoreoEEDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public int TiempoEsperaCiclo()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetSqlStringCommand("SELECT DBO.FUN_NOMBRE_BPM_PARAMETROS(" + _idParametro + ")");
            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }
    }
}
