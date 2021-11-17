using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System.Data.Common;
using System.Data;

namespace SILPA.AccesoDatos.RegistroMinero
{
    [Serializable]
    public class LimitesAutoridadAmbiental
    {
        private Configuracion objConfiguracion;

        public int AutoridadID { get; set; }
        public double? LatMax { get; set; }
        public double? LngMax { get; set; }
        public double? LatMin { get; set; }
        public double? LngMin { get; set; }

        public LimitesAutoridadAmbiental()
        {
            objConfiguracion = new Configuracion();
        }

        public LimitesAutoridadAmbiental(int autoridadID)
        {
            objConfiguracion = new Configuracion();
            this.AutoridadID = autoridadID;
        }
        public LimitesAutoridadAmbiental ConsultaLimitesAutoridadAmbiental()
        {
            LimitesAutoridadAmbiental limites = new LimitesAutoridadAmbiental();
            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { this.AutoridadID };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AUT_AMB_REGISTRO_MINERO", parametros);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    if (reader["LAT_MAX"].ToString() !="")
                        limites.LatMax = Convert.ToDouble(reader["LAT_MAX"]);
                    if (reader["LNG_MAX"].ToString() != "")
                        limites.LngMax = Convert.ToDouble(reader["LNG_MAX"]);
                    if (reader["LAT_MIN"].ToString() != "")
                        limites.LatMin = Convert.ToDouble(reader["LAT_MIN"]);
                    if (reader["LNG_MIN"].ToString() != "")
                        limites.LngMin = Convert.ToDouble(reader["LNG_MIN"]);
                }
            }
            return limites;
        }
    }
}
