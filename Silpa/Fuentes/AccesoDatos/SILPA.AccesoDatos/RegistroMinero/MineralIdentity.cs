using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.RegistroMinero
{
    [Serializable]
    public class MineralIdentity : EntidadSerializable
    {
        private Configuracion objConfiguracion;

        public int MineralID { get; set; }
        public string NombreMineral { get; set; }

        public MineralIdentity()
        {
            objConfiguracion = new Configuracion();
        }

        public List<MineralIdentity> ListaMinerales()
        {
            List<MineralIdentity> LstMinerales = new List<MineralIdentity>();
            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SP_LISTA_MINERALES");
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MineralIdentity mineral = new MineralIdentity();
                    mineral.MineralID = Convert.ToInt32(reader["MINERAL_ID"]);
                    mineral.NombreMineral = reader["NOMBRE_MINERAL"].ToString();
                    LstMinerales.Add(mineral);
                }
            }
            return LstMinerales;
        }
    }
}
