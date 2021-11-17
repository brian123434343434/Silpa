using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.REDDS
{
    [Serializable]
    public class ReddsLocalizacion
    {
        public int ReddsID { get; set; }
        public int LocID { get; set; }
        public string LocNombre { get; set; }
        public int GeoID { get; set; }
        public List<ReddsCoordenadasLocalizacion> LstCoordenadas { get; set; }
    }
}
