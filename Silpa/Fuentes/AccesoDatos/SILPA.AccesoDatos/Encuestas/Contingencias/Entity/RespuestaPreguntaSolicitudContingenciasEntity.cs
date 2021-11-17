using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;

namespace SILPA.AccesoDatos.Encuestas.Contingencias.Entity
{
    [Serializable]
    public class RespuestaPreguntaSolicitudContingenciasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la Respuesta de la pregunta
            /// </summary>
            public int RespuestaPreguntaSolicitudID { get; set; }

            /// <summary>
            /// Identificador de la pregunta perteneciente a la solicitud
            /// </summary>
            public int PreguntaSolicitudID { get; set; }

            /// <summary>
            /// Identificador de la solicitud al cual pertnece la pregunta
            /// </summary>
            public int SolicitudID { get; set; }

            /// <summary>
            /// Pregunta asociada a la solicitud
            /// </summary>
            public PreguntaEncuestasEntity Pregunta { get; set; }
            
            /// <summary>
            /// Respuesta a la pregunta abierta
            /// </summary>
            public string Respuesta { get; set; }

            /// <summary>
            /// Fecha de creacion de la respuesta
            /// </summary>
            public DateTime FechaCreacion { get; set; }

            /// <summary>
            /// Fecha de ultima modificación de la respuesta
            /// </summary>
            public DateTime FechaModificacion { get; set; }

        #endregion
        
    }
}
