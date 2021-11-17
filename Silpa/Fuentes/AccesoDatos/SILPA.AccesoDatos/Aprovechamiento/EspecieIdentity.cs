using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Aprovechamiento
{
    [Serializable]
    public class EspecieIdentity
    {
        public int EspecieID { get; set; }
        public string NombreComun { get; set; }
        public string NombreCientifico { get; set; }
        public int ClaseRecurso { get; set; }
    }
}
