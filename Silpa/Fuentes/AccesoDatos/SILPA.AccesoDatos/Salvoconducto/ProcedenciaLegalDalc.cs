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
    public class ProcedenciaLegalDalc
    {
        private Configuracion objConfiguracion;

        public ProcedenciaLegalDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<ProcedenciaLegalIdentity> ListaProcedenciaLegal()
        {
            try
            {
                List<ProcedenciaLegalIdentity> ListProcedenciaLegal = new List<ProcedenciaLegalIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_PROCEDENCIA_LEGAL");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListProcedenciaLegal.Add(new ProcedenciaLegalIdentity { ProcedenciaLegalID = Convert.ToInt32(reader["PROCEDENCIA_LEGAL_ID"]), ProcedenciaLegal = reader["PROCEDENCIA_LEGAL"].ToString() });
                    }
                }
                return ListProcedenciaLegal;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
