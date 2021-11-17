using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;

namespace SILPA.AccesoDatos.Encuestas.Contingencias.Entity
{
    [Serializable]
    public class PreguntaSolicitudContingenciasEntity
    {
        #region Propiedades

        /// <summary>
        /// Identificador de la pregunta perteneciente a la solicitud
        /// </summary>
        public int PreguntaSolicitudID { get; set; }

        /// <summary>
        /// Identificador de la solicitud al cual pertnece la pregunta
        /// </summary>
        public int PreguntaSolicitudContingenciasID { get; set; }

        /// <summary>
        /// Pregunta asociada a la solicitud
        /// </summary>
        public PreguntaEncuestasEntity Pregunta { get; set; }

        /// <summary>
        /// Listado de las opciones de una pregunta
        /// </summary>
        public List<OpcionPreguntaSolicitudContingenciasEntity> OpcionesPregunta { get; set; }

        /// <summary>
        /// Listado de las respuestas de una pregunta
        /// </summary>
        public List<RespuestaPreguntaSolicitudContingenciasEntity> RespuestasPregunta { get; set; }

        /// <summary>
        /// Listado de las localizaciones de una pregunta
        /// </summary>
        public List<LocalizacionPreguntaSolicitudContingenciasEntity> LocalizacionesPregunta { get; set; }

        /// <summary>
        /// Listado de las coordenadas de una pregunta
        /// </summary>
        public List<CoordenadasPreguntaSolicitudContingenciasEntity> CoordenadasPregunta { get; set; }

        /// <summary>
        /// Listado de las documentos de una pregunta
        /// </summary>
        public List<DocumentoPreguntaSolicitudContingenciasEntity> DocumentosPregunta { get; set; }

        /// <summary>
        /// Fecha de creacion de la pregunta
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de ultima modificación de la pregunta
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        #endregion

    }

}
