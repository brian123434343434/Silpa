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

namespace SILPA.AccesoDatos.CesionDeDerechos
{
    public class InformacionInstanciaFormularioDalc
    {
        public DataSet ListarInformacionInstanciaFormulario(string instancia)
        {
            object[] obj = new object[] { instancia }; 
            SqlDatabase db = new SqlDatabase(ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString);
            DbCommand cmd = db.GetStoredProcCommand("BPM_LISTAR_TRAZA_INSTANCIA", obj);
            try
            {
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
                if (db != null)
                    db = null;
            }
        }
    }
}

