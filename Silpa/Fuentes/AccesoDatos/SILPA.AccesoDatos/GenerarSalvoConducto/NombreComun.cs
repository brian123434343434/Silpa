using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GenerarSalvoConducto
{
    public class NombreComun
    {

        public NombreComun()
        {
           
        }
        // Identificador del objeto
        public Int32? IDNombreComun { get; set; }
        // Descripcion del Objeto
        public String DesNombreComun { get; set; }
        // Identificador NombreCientifico
        public NombreCientifico NombreCientifico { get; set; }
    }
}
