using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.PINES
{
    public class ComentarioActividadIdentity
    {
        public int? IDComentario { get; set; }
        public int IDProcessInstance { get; set; }
        public int IDActivityInstance { get; set; }
        public int IDActivity { get; set; }
        public string Comments { get; set; }
        public string Field { get; set; }
        public string Usuario { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public bool ActividadRealizada { get; set; }
        public int? AutoridadId { get; set; }
        public int IDAccion { get; set; }
        public bool EsGerente { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public bool? TieneProrroga { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaProrroga { get; set; }
        public DateTime? FechaRealFinalizacion { get; set; }
        public string NombreAccion { get; set; }
        public bool? DetieneFlujo { get; set; }
        public int IDProgresoAccion { get; set; }
        public string NombreAutoridad { get; set; }
    }
}
