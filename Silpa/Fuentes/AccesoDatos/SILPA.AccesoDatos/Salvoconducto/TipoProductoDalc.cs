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
    public class TipoProductoDalc
    {
        private Configuracion objConfiguracion;

        public TipoProductoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<TipoProductoIdentity> ListaTipoProducto(int claseProductoID, bool esSalvoconducto)
        {
            try
            {
                List<TipoProductoIdentity> ListTipoProducto = new List<TipoProductoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_TIPO_PRODUCTO");
                db.AddInParameter(cmd, "P_CLASE_PRODUTO_ID", DbType.Int32, claseProductoID);
                db.AddInParameter(cmd, "P_SALVOCONDUCTO", DbType.Boolean, esSalvoconducto);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListTipoProducto.Add(new TipoProductoIdentity { ClaseProductoID = Convert.ToInt32(reader["CLASE_PRODUCTO_ID"]), TipoProducto = reader["TIPO_PRODUCTO"].ToString(), TipoProductoID = Convert.ToInt32(reader["TIPO_PRODUCTO_ID"]), Formula = reader["FORMULA"].ToString() });
                    }
                }
                return ListTipoProducto;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
         
    }
        
}
    