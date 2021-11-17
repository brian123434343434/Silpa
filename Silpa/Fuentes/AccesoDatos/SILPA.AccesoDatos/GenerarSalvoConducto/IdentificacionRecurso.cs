using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GenerarSalvoConducto
{
    public class IdentificacionRecurso
    {

        public IdentificacionRecurso()
        {
           
        }
        // Identificador del objeto
        public Int32? IDIdentificacionRecurso { get; set; }
        // Identificador del objeto
        public Int32? ActoAdministrativo { get; set; }
        // Identificador ClaseAprovechamiento
        public ClaseAprovechamiento ClaseAprovechamiento { get; set; }
        // Identificador ClaseRecursosTrans
        public ClaseRecursosTrans ClaseRecursosTrans { get; set; }
        // Identificador FinalidadAprovechamiento
        public FinalidadAprovechamiento FinalidadAprovechamiento { get; set; }
        // FechaCreacion Registro
        public DateTime? FechaCreacion { get; set; }
        // Identificador ListaSolicitudProducto
        public List<SolicitudProducto> ListaSolicitudProducto { get; set; }

    }
}
