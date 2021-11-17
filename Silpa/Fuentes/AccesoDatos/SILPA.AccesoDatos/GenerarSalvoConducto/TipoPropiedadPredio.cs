using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GenerarSalvoConducto
{
    public class TipoPropiedadPredio
    {
        public TipoPropiedadPredio()
        {
           
        }
        // Identificador del objeto
        public Int32? IDTipoPropiedadPredio { get; set; }
        // Descripcion del Objeto
        public String DescTipoPropiedadPredio { get; set; }
        // Si requiere que se escriba en un campo texto
        public bool? RequiereEspecificar { get; set; }
    }
}
