using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.PINES
{
    public class ActividadProcesoURL
    {
        public int IdProcessInstance { get; set; }
        public int IdActivityInstance { get; set; }
        public string Usuario { get; set; }
        public string UrlProyecto { get; set; }
        public bool EsVITAL { get; set; }
        public string NroVITAL { get; set; }
    }
}
