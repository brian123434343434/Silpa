using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.RegistroMinero
{
    [Serializable]
    public struct Coordenada
    {
        public int ID { get; set; }
        public int LocalizacionID { get; set; }
        public  double CoordenadaNorte { get; set; }
        public double CoordenadaEste { get;set;}

    }
}
