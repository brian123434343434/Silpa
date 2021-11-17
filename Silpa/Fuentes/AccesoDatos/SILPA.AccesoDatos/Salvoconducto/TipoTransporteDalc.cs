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
    public class TipoTransporteDalc
    {
        private Configuracion objConfiguracion;

        public TipoTransporteDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoTransporteIdentity> ListaTipoTransportePorModoTransporte(int modoTransporteID)
        {
            try
            {
                List<TipoTransporteIdentity> ListTipoTransporte = new List<TipoTransporteIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_TIPO_TRANSPORTE");
                db.AddInParameter(cmd, "P_MODO_TRANSPORTE_ID", DbType.Int32, modoTransporteID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListTipoTransporte.Add(new TipoTransporteIdentity { ModoTransporteID = Convert.ToInt32(reader["MODO_TRANSPORTE_ID"]), TipoTransporte = reader["TIPO_TRANSPORTE"].ToString(), TipoTransporteID = Convert.ToInt32(reader["TIPO_TRANSPORTE_ID"]), CapacidadPromedioCarga = reader["CAPACIDAD_PROM_CARGA"].ToString()!= string.Empty ? Convert.ToDouble(reader["CAPACIDAD_PROM_CARGA"]):-1 });
                    }
                }
                return ListTipoTransporte;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
