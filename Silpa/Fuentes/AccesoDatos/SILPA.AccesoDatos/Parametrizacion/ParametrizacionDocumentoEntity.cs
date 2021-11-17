using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Parametrizacion
{
    public class ParametrizacionDocumentoEntity
    {

        public int DocID { get; set; }
        public string DocNombre { get; set; }
        public string EnlaceAplicativo { get; set; }
        public int TipoAdquisicionID { get; set; }
        public int EntidadExternaID { get; set; }
        public string CodigoProceso { get; set; }
        public string ImagenUrl { get; set; }

    }
}
