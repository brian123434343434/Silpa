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
    public class ModoAdquisicionRecursoDalc
    {
        private Configuracion objConfiguracion;

        public ModoAdquisicionRecursoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<ModoAdquisicionRecursoIdentity> ListaModoAdquisionRecurso(int? claseRecursoId, bool esSalvoconducto, int? formaOtorgamientoId)
        {
            try
            {
                List<ModoAdquisicionRecursoIdentity> ListModoAdquisicionRecurso = new List<ModoAdquisicionRecursoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_MODO_ADQ_RECURSO");
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, claseRecursoId);
                db.AddInParameter(cmd, "P_SALVOCONDUCTO", DbType.Boolean, esSalvoconducto);
                db.AddInParameter(cmd, "P_FORMA_OTORGAMIENTO_ID ", DbType.Int32, formaOtorgamientoId);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListModoAdquisicionRecurso.Add(new ModoAdquisicionRecursoIdentity { ModAdqRecursoID = Convert.ToInt32(reader["MODO_AQUISICION_ID"]), ClaseRecursoID = Convert.ToInt32(reader["CLASE_RECURSO_ID"]), ModAdqRecurso = reader["MODO_ADQUISICION"].ToString() });
                    }
                }
                return ListModoAdquisicionRecurso;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
