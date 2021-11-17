using System;
using SILPA.Comun;
using System.Collections.Generic;

namespace SILPA.AccesoDatos.DAA
{
    public class RegistroRadicarSigproEntity : EntidadSerializable
    { 
        #region Propiedades

        public string NumeroVital { get; set; }
        public DateTime FechaRadicacionVital { get; set; }
        public int IdRadicacionVital { get; set; }
        public string NumeroVitalPadre { get; set; }
        public int IdAutoridadAmbiental { get; set; }
        public string SectorPadre { get; set; }
        public string SectorHijo { get; set; }
        public int NumeroSilpa { get; set; }
        public string Expediente { get; set; }
        public string PathDocumento { get; set; }
        public int IdSolicitante { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Solicitante { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Solicitud  { get; set; }
        public string CodigoTipoTramiteSigpro { get; set; }
        public string NombreTipoTramiteSigpro { get; set; }
        public string CodigoTipoDocumentalSigpro { get; set; }
        public string NombreTipoDocumentalSigpro { get; set; }
        public string MedioEnvio { get; set; }
        public string CodigoDependencia { get; set; }
        public string NombreProyecto { get; set; }
        public int EnviarTodosLosArchivos { get; set; }
        public int TramiteVitalId { get; set; }
        public int SolicitudVitalId { get; set; }
        public string DescripcionRadicacion { get; set; }
        public List<string> RutasArchivosTodosCopiar { get; set; }
        public List<string> RutasArchivosBaseCopiar { get; set; }
        public List<string> RutasArchivosAdicionalesCopiar { get; set; } 
        #endregion
    }
}
