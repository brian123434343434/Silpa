using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.REDDS
{
    [Serializable]
    public class ReddsCoordenadasLocalizacion
    {
        public int CoorID { get; set; }
        public int LocID { get; set; }
        public double CoorNorte { get; set; }
        public double CoorEste { get; set; }
    }
}
