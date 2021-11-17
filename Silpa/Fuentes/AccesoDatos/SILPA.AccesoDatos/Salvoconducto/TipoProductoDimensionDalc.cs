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
    public class TipoProductoDimensionDalc
    {
        private Configuracion objConfiguracion;

        public TipoProductoDimensionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<TipoProductoDimensionIdentity> ListaDimensionesTipoProducto(int tipoProductoID)
        {
            try
            {
                List<TipoProductoDimensionIdentity> ListDimensionesTipoProducto = new List<TipoProductoDimensionIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSPS_CONSULTAR_DIMENSION_TIPO_PRODUCTO");
                db.AddInParameter(cmd, "P_TIPO_PRODUCTO_ID", DbType.Int32, tipoProductoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListDimensionesTipoProducto.Add(new TipoProductoDimensionIdentity { Alto = Convert.ToDecimal(reader["ALTO"]), Largo = Convert.ToDecimal(reader["LARGO"]), Ancho = Convert.ToDecimal(!DBNull.Value.Equals(reader["ANCHO"])?reader["ANCHO"]:null)});
                    }
                }
                return ListDimensionesTipoProducto;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
