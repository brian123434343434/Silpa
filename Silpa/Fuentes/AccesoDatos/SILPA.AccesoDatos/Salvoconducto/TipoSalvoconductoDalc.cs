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
    public class TipoSalvoconductoDalc
    {
        private Configuracion objConfiguracion;

        public TipoSalvoconductoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<TipoSalvoconductoIdentity> ListaTipoSalvoconducto()
        {
            try
            {
                List<TipoSalvoconductoIdentity> ListTipoSalvoconducto = new List<TipoSalvoconductoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_TIPOSALVOCONDUCTO");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListTipoSalvoconducto.Add(new TipoSalvoconductoIdentity { TipoSalvoconductoID = Convert.ToInt32(reader["TIPO_SALVOCONDUCTO_ID"]), TipoSalvoconducto = reader["TIPO_SALVOCONDUCTO"].ToString() });
                    }
                }
                return ListTipoSalvoconducto;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
