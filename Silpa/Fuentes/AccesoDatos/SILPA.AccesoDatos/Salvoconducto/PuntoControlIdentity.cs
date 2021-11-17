using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    [Serializable]
    public class PuntoControlIdentity
    {
        public int PuntoControlID { get; set; }
        public int LogID { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public int Orden { get; set; }
        public string FechaRegistro { get; set; }
        public string Depto { get; set; }
        public string Munpio { get; set; }
        public string Autoridad { get; set; }
        public string Nombre { get; set; }
        public string Identificacion { get; set; }

    }
}
