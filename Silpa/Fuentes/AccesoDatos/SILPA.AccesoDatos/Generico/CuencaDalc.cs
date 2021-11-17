using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.SqlClient;


namespace SILPA.AccesoDatos.Generico
{
    public class CuencaDalc
    {
        Configuracion objConfiguracion;

        public CuencaDalc()
        {
            objConfiguracion = new Configuracion();  
        }

        public DataSet ListarCuencas()
        {
            SqlDatabase db = null;
            DbCommand cmd = null;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                cmd = db.GetStoredProcCommand("SS_RES_LST_CUENCAS");
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

    }
}
