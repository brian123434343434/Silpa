using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GenerarSalvoConducto
{
    public class SolicitudProducto
    {
        public SolicitudProducto()
        {
           
        }
        
        // Identificador del objeto
        public Int32? IDSolicitudProducto { get; set; }
        // Identificador IdentificacionRecurso
        public Int32? IdentificacionRecurso { get; set; }
        // Identificador NombreCientifico
        public NombreCientifico NombreCientifico { get; set; }
        // Identificador NombreComun
        public NombreComun NombreComun { get; set; }
        // Identificador NombreComun
        public SalProducto Producto { get; set; }
        // Identificador Otro Producto
        public String OtroProducto { get; set; }
        // Identificador Cantidad Bruto
        public Int64? CantidadBruto { get; set; }
        // Identificador Cantidad Transformado
        public Int64? CantidadTransformado { get; set; }
        // Identificador UnidadMetrica
        public UnidadMetrica UnidadMetrica { get; set; }
        // Identificador Fecha Creacion del Registro
        public DateTime? FechaCreacion { get; set; }
    }
}
