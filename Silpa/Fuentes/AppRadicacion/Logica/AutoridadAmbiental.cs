using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data; 
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;


namespace AppRadicacion.Logica
{
    public class AutoridadAmbiental
    {
        public AutoridadAmbiental()
        {
            
        }

        public DataTable DatosAA()
        {
            SqlDatabase db = new SqlDatabase(Comun.Utilidades.utl.cadena);
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AUT_AMB");
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado.Tables[0]; 
            }
            finally
            {
                db = null;
            }
        }

    }
}
