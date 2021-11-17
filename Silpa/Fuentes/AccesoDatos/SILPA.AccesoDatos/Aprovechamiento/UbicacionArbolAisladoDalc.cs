using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.Aprovechamiento
{
    public class UbicacionArbolAisladoDalc
    {
        private Configuracion objConfiguracion;

        public UbicacionArbolAisladoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<UbicacionArbolAisladoIdentity> ListarUbicacionArbolAislado()
        {
            try
            {
                List<UbicacionArbolAisladoIdentity> LstUbicacionArbolAisladoIdentity = new List<UbicacionArbolAisladoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_LISTAR_UBIC_ARBOL_AISLADO");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstUbicacionArbolAisladoIdentity.Add(new UbicacionArbolAisladoIdentity() { CodUbicArbolAislado = Convert.ToInt32(reader["COD_UBIC_ARBOL_AISLADO"]), Descripcion = reader["DESCRIPCION"].ToString(), CodigoIdeam = reader["CODIGO_IDEAM"].ToString() });
                    }
                }
                return LstUbicacionArbolAisladoIdentity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
