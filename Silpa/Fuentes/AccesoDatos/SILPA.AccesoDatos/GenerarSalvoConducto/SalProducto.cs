using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GenerarSalvoConducto
{
    public class SalProducto
    {

        public SalProducto()
        {
           
        }
        // Identificador del objeto
        public Int32? IDSalProducto { get; set; }
        // Descripcion del Objeto
        public String DescSalProducto { get; set; }
        // Identificador de Tipo Producto
        public ClaseRecursosTrans ClaseRecursosTrans { get; set; }
    }
}
