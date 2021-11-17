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
    public class ClaseSalvoconductoDalc
    {
        private Configuracion objConfiguracion;

        public ClaseSalvoconductoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<ClaseSalvoconductoIdentity> ListaClaseSalvoconducto()
        {
            try
            {
                List<ClaseSalvoconductoIdentity> ListClaseSalvocnducto = new List<ClaseSalvoconductoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_CLASE_SALVOCONDUCTO");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListClaseSalvocnducto.Add(new ClaseSalvoconductoIdentity { ClaseSalvoconductoID = Convert.ToInt32(reader["CLASE_SALVOCONDUCTO_ID"]), ClaseSalvoconducto = reader["CLASE_SALVOCONDUCTO"].ToString() });
                    }
                }
                return ListClaseSalvocnducto;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
