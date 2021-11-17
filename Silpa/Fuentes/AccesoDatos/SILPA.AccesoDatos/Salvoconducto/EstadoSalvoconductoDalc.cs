using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class EstadoSalvoconductoDalc
    {
        private Configuracion objConfiguracion;

        public EstadoSalvoconductoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<EstadoSalvoconductoIdentity> ListaEstadoSalvoconducto()
        {
            try
            {
                List<EstadoSalvoconductoIdentity> ListEstadoSalvoconducto = new List<EstadoSalvoconductoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_ESTADO_SALVOCONDUCTO");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListEstadoSalvoconducto.Add(new EstadoSalvoconductoIdentity { EstadoID = Convert.ToInt32(reader["ESTADO_ID"]), Estado = reader["ESTADO"].ToString() });
                    }
                }
                return ListEstadoSalvoconducto;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
