using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.REDDS
{
    [Serializable]
    public class ReddsAutoridadAmbiental
    {
        public int ReddsID { get; set; }
        public int AutoridadID { get; set; }
        public string NombreAutoridad { get; set; }
    }
}
