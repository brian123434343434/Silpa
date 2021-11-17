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
    public class UnidadMedidaDalc
    {
        private Configuracion objConfiguracion;

        public UnidadMedidaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<UnidadMedidaIdentity> ListaUnidadMedidaTipoProducto(int TipoProductoID)
        {
            try
            {
                List<UnidadMedidaIdentity> ListUnidadMedida = new List<UnidadMedidaIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_UNIDAD_MEDIDA_TIPO_PRODUCTO");
                db.AddInParameter(cmd, "P_TIPO_PRODUCTO_ID", DbType.Int32, TipoProductoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListUnidadMedida.Add(new UnidadMedidaIdentity { UnidadMedidaId = Convert.ToInt32(reader["UNIDAD_MEDIDA_ID"]), UnidadMedidad = reader["UNIDAD_MEDIDAD"].ToString(), TipoProductoID = Convert.ToInt32(reader["TIPO_PRODUCTO_ID"]), Factor = Convert.ToDouble(reader["FACTOR"]) });
                    }
                }
                return ListUnidadMedida;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
