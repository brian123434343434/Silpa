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
    public class ModoTransporteDalc
    {
        private Configuracion objConfiguracion;

        public ModoTransporteDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<ModoTransporteIdentity> ListaModoTransporte()
        {
            try
            {
                List<ModoTransporteIdentity> ListModoTransporte = new List<ModoTransporteIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_MODO_TRANSPORTE");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListModoTransporte.Add(new ModoTransporteIdentity { ModoTransporteID = Convert.ToInt32(reader["MODO_TRANSPORTE_ID"]), ModoTransporte = reader["MODO_TRANSPORTE"].ToString() });
                    }
                }
                return ListModoTransporte;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
