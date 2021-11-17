using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Aprovechamiento
{
    public class EspecieDalc
    {
        private Configuracion objConfiguracion;

        public EspecieDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<EspecieIdentity> ListaEspecie(string nombreCientifico, int claseRecursoId)
        {
            try
            {
                List<EspecieIdentity> LstEspecieIdentity = new List<EspecieIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_LISTAR_ESPECIE_TAXONOMIA");
                db.AddInParameter(cmd, "P_NOMBRE_COMUN", DbType.String, nombreCientifico);
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, claseRecursoId);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstEspecieIdentity.Add(new EspecieIdentity() { ClaseRecurso = Convert.ToInt32(reader["CLASE_RECURSO_ID"]), EspecieID = Convert.ToInt32(reader["ESPECIE_TAXONOMIA_ID"]), NombreCientifico = reader["NOMBRE_CIENTIFICO"].ToString(), NombreComun = reader["NOMBRE_COMUN"].ToString() });
                    }
                }
                return LstEspecieIdentity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
