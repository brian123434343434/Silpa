using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.REDDS
{
    [Serializable]
    public class ReddsEstandarMercado
    {
        public int EstandarID { get; set; }
        public int ReddsID { get; set; }
        public string NombreEstandar { get; set; }
        public List<ReddsMetodologiaEstandar> LstMetodologiaEstandar { get; set; }
    }
}
