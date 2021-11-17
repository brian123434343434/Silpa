using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GenerarSalvoConducto
{
    public class Vereda
    {
        public Vereda()
        {
           
        }
        // Identificador del objeto
        public Int32? IDVereda { get; set; }
        // Descripcion del Objeto
        public String DescVereda { get; set; }
        // Identificador del Municipio al que pertenece la Vereda
        public Int32? IDMunicipio { get; set; }
    }
}
