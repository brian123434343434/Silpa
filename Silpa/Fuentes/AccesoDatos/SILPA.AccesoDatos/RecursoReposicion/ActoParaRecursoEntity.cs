using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.RecursoReposicion
{
    [Serializable]
    /// <summary>
    /// Recurso para presentar
    /// </summary>
    
    public class ActoParaRecursoEntity
    {
        public int ActoNotificacionID { get; set; }
        public int PersonaID { get; set; }
        public int EstadoActualID  { get; set; }
        public int EstadoFuturoID { get; set; }
        public int FlujoID { get; set; }
        public string IdentificacionUsuario { get; set; }
        public int AutoridadID { get; set; }
        public string Autoridad { get; set; }
        public string Expediente { get; set; }
        public string NumeroVITAL { get; set; }
        public string NumeroActoAdministrativo { get; set; }
        public DateTime FechaActoAdministrativo { get; set; }
        public string RutaDocumento { get; set; }
        public DateTime FechaNotificacion { get; set; }
        public int EstadoPersonaActoID { get; set; }
    }
}
