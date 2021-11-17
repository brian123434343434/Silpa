using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.REDDS
{
    [Serializable]
    public class ReddsArchivos
    {
        public int ReddsID { get; set; }
        public int ArchivoID { get; set; }
        public string Archivo { get; set; }
    }
}
