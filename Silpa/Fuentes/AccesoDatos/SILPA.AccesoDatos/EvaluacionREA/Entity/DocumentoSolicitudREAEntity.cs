using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    public class DocumentoSolicitudREAEntity
    {
        public int DocumentoID { get; set; }
        public int TipoDocumentoID { get; set; }
        public string NombreDocumento { get; set; }
        public string NombreDocumentoUsuario { get; set; }
        public string Ubicacion { get; set; }
    }
}
