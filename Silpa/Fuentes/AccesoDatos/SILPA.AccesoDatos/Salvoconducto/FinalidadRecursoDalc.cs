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
    public class FinalidadRecursoDalc
    {
        private Configuracion objConfiguracion;

        public FinalidadRecursoDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<FinalidadRecursoIdentity> ListaFinalidadRecurso(int? claseRecursoId)
        {
            try
            {
                List<FinalidadRecursoIdentity> ListFinalidadRecurso = new List<FinalidadRecursoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_FINALIDAD_RECURSO");
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, claseRecursoId);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListFinalidadRecurso.Add(new FinalidadRecursoIdentity { FinalidadRecursoId = Convert.ToInt32(reader["FINALIDAD_ID"]), ClaseRecursoId = Convert.ToInt32(reader["CLASE_RECURSO_ID"]), FinalidadRecurso = reader["FINALIDAD"].ToString() });
                    }
                }
                return ListFinalidadRecurso;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
