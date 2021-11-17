using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GenerarSalvoConducto
{
    public class TipoProducto
    {

        public TipoProducto()
        {
           
        }
        // Identificador del objeto
        public Int32? IDTipoProducto { get; set; }
        // Descripcion del Objeto
        public String DescTipoProducto { get; set; }
        // Identificador de la Unidad de Medida
        public UnidadMetrica UnidadMetrica { get; set; }
    }
}
