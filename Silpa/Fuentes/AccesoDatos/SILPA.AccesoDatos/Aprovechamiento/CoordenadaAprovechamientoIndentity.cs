using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Aprovechamiento
{
    [Serializable]
    public class CoordenadaAprovechamientoIndentity
    {
        public int CoordenadaID { get; set; }
        public int AprovechamientoID { get; set; }
        public double Norte { get; set; }
        public double Este { get; set; }
        
    }
}
