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
    public class TratamientoSilviculturaDalc
    {
        private Configuracion objConfiguracion;
        public TratamientoSilviculturaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<TratamientoSilviculturaIdentity> ListarTratamientoSilvicultura()
        {
            try
            {
                List<TratamientoSilviculturaIdentity> LstTratamientoSilviculturaIdentity = new List<TratamientoSilviculturaIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_LISTAR_TRATAMIENTO_SILVICULTURA");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstTratamientoSilviculturaIdentity.Add(new TratamientoSilviculturaIdentity() { CodTratamientoSilvicultura= Convert.ToInt32(reader["TRATAMIENTO_SILVICULTURA_ID"]), Descripcion = reader["DESCRIPCION"].ToString(), CodigoIdeam = reader["CODIGO_IDEAM"].ToString() });
                    }
                }
                return LstTratamientoSilviculturaIdentity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
