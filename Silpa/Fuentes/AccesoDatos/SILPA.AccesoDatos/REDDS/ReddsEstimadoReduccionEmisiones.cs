using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.REDDS
{
    [Serializable]
    public class ReddsEstimadoReduccionEmisiones
    {
        public int ReddsID { get; set; }
        public int Año { get; set; }
        public int Valor { get; set; }
        public int ValorVerificacion { get; set; }
    }
}
