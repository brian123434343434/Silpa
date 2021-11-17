using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    [Serializable]
    public class TipoTransporteIdentity
    {
        public int TipoTransporteID { get; set; }
        public int ModoTransporteID { get; set; }
        public string TipoTransporte { get; set; }
        public double? CapacidadPromedioCarga { get; set; }
    }
}
