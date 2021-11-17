using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.REDDS
{
    [Serializable]
    public class ReddsDeptoMunicipio
    {
        public int ReddsID { get; set; }
        public int DeptoID { get; set; }
        public string NombreDepartamento { get; set; }
        public int MunpioID { get; set; }
        public string NombreMunicipio { get; set; }
    }
}
