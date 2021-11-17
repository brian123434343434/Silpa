using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;

namespace SILPA.AccesoDatos.Encuestas.Contingencias.Entity
{
    [Serializable]
    public class SolicitudContingenciasEntity
    {
        #region Propiedades

        /// <summary>
        /// Identificador de la solicitud
        /// </summary>
        public int SolicitudContingenciasID { get; set; }
        
        /// <summary>
        /// Identificador de la solicitud
        /// </summary>
        public int FormularioID { get; set; }

        /// <summary>
        /// Identificador de la solicitud
        /// </summary>
        public int AutoridadID { get; set; }

        /// <summary>
        /// Identificador del solicitante ID
        /// </summary>
        public long SolicitanteID { get; set; }

        /// <summary>
        /// Expediente al cual pertenece la solicitud
        /// </summary>
        public ExpedienteEncuestasEntity Expediente { get; set; }

        /// <summary>
        /// Identificador de la solicitud
        /// </summary>
        public int SectorID{ get; set; }

        /// <summary>
        /// nombre  de proyecto 
        /// </summary>
        public string nombreProyecto { get; set; }

        /// <summary>
        /// Número Vital
        /// </summary>
        public string NumeroVital { get; set; }

        /// <summary>
        /// Número Vital de Reporte Inicial
        /// </summary>
        public string NumeroVitalPadre { get; set; }

        /// <summary>
        /// Nombre del responable
        /// </summary>
        public string NombreResponsable { get; set; }

        /// <summary>
        /// Numero Telefonico del Responsable
        /// </summary>
        public string NumeroTelefonicoResponsable { get; set; }

        /// <summary>
        /// Email del Responsable
        /// </summary>
        public string EmailResponsable { get; set; }

        /// <summary>
        /// Etapa del proyecto
        /// </summary>
        public EtapaProyectoContingenciasEntity EtapaProyecto { get; set; }

        /// <summary>
        /// Etapa del proyecto
        /// </summary>
        public string EtapaProyectoOtro { get; set; }

        /// <summary>
        /// Lista de preguntas de la solicitud
        /// </summary>            
        public List<PreguntaSolicitudContingenciasEntity> Preguntas { get; set; }

        /// <summary>
        /// Etapa de proyecto abierta
        /// </summary>
        public string NivelEmergenciaContingenciaID { get; set; }

        /// <summary>
        /// Fecha de creacion de la solicitud
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de ultima modificación de la solicitud
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Indica si el registro se encuentra activo
        /// </summary>
        public bool Activo { get; set; }

        #endregion

    }
}
