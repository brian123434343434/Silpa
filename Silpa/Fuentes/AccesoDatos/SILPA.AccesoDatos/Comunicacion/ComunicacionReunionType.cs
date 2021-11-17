using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Comunicacion
{
    public class ComunicacionReunionType : EntidadSerializable
    {
        public int ReunionID { get; set; }
        public int TareaID { get; set; }
        public DateTime FechaReunion { get; set; }
        public string Sala { get; set; }
        public string Usuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string NUR { get; set; }
        public string CodExpediente { get; set; }
        public string NombreExpediente{ get; set; }
        public string[] Responsables { get; set; }
        public string NumeroVital { get; set; }
        public string AutoridadAmbiental { get; set; }
        public string Direccion { get; set; }
        public string PieMensaje { get; set; }
        public string CC { get; set; }
        public int PlantillaID { get; set; }
        public DateTime FechaFinalizacionActividad { get; set; }
        public DateTime FechaRadicacion { get; set; }
    }
}
