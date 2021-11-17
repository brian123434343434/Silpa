using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.PINES
{
    public class AccionActividadIdentity
    {
        public int IdActivity { get; set; }
        public int IdAccion { get; set; }
        public int? Orden { get; set; }
        public int DiasEjecucion { get; set; }
        public bool Obligatoria { get; set; }
    }
}
