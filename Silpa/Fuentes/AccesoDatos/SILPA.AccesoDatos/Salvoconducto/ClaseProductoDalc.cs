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
    public class ClaseProductoDalc
    {
        private Configuracion objConfiguracion;

        public ClaseProductoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<ClaseProductoIdentity> ListaClaseProducto(int claseRecursoID, bool esSalvoconducto)
        {
            try
            {
                List<ClaseProductoIdentity> ListClaseProducto = new List<ClaseProductoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_CLASE_PRODUCTO");
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, claseRecursoID);
                db.AddInParameter(cmd, "P_SALVOCONDUCTO", DbType.Boolean, esSalvoconducto);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListClaseProducto.Add(new ClaseProductoIdentity { ClaseProductoID = Convert.ToInt32(reader["CLASE_PRODUCTO_ID"]), ClaseProducto = reader["CLASE_PRODUCTO"].ToString(), ClaseRecursoID = Convert.ToInt32(reader["CLASE_RECURSO_ID"]) });
                    }
                }
                return ListClaseProducto;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
