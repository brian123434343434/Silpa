using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.REDDS
{
    [Serializable]
    public class ReddsMetodologiaEstandar
    {
        public int MetodologiaID { get; set; }
        public int EstandarID { get; set; }
        public string NombreMetodologia { get; set; }
    }
}
