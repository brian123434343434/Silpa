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
    public class ClaseAprovechamientoDalc
    {
        private Configuracion objConfiguracion;

        public ClaseAprovechamientoDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<ClaseAprovechamientoIdentity> ListaClaseAprovechamiento(int? claseRecursoId)
        {
            try
            {
                List<ClaseAprovechamientoIdentity> ListClaseAprovechamiento = new List<ClaseAprovechamientoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_CLASE_APROVECHAMIENTO");
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, claseRecursoId);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListClaseAprovechamiento.Add(new ClaseAprovechamientoIdentity { ClaseAprovechamientoId = Convert.ToInt32(reader["CLASE_APROVECHAMIENTO_ID"]), ClaseRecursoId = Convert.ToInt32(reader["CLASE_RECURSO_ID"]), ClaseAprovechamiento = reader["CLASE_APROVECHAMIENTO"].ToString() });
                    }
                }
                return ListClaseAprovechamiento;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
