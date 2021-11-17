using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class ClaseRecursoDalc
    {
        private Configuracion objConfiguracion;

        public ClaseRecursoDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<ClaseRecursoIdentity> ListaClaseRecurso()
        {
            try
            {
                List<ClaseRecursoIdentity> ListClaseRecurso = new List<ClaseRecursoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_CLASERECURSO");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListClaseRecurso.Add(new ClaseRecursoIdentity { ClaseRecursoID = Convert.ToInt32(reader["CLASE_RECURSO_ID"]), ClaseRecurso = reader["CLASE_RECURSO"].ToString() });
                    }
                }
                return ListClaseRecurso;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
