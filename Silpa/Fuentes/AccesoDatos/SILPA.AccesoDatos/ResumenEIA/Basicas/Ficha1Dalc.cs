using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using SILPA.Comun;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class Ficha1Dalc
    {
        
        public Ficha1Dalc()
        {
           
        }
        public static DataSet cargarInformacionEmpresa(int IdUsuario)
        {
            Configuracion  objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            DataSet ds;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]{ IdUsuario };
                DbCommand cmd = db.GetStoredProcCommand("EIA_CONSULTAR_EMPRESA_POR_APPUSER", parametros);
                ds = db.ExecuteDataSet (cmd);

                return ds;
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
