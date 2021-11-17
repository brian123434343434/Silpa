using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;

namespace SILPA.AccesoDatos.Encuestas.Contingencias.Entity
{
    [Serializable]
    public class OpcionPreguntaSolicitudContingenciasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la opcion de la pregunta
            /// </summary>
            public int OpcionPreguntaSolicitudID { get; set; }

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
            /// Opcion asociada a la pregunta de una solicitud
            /// </summary>
            public OpcionPreguntaEncuestasEntity OpcionPregunta { get; set; }

            /// <summary>
            /// Indica si la opcion fue seleccionada
            /// </summary>
            public bool? Selecciono { get; set; }            

            /// <summary>
            /// Respuesta a una pregunta abierta
            /// </summary>
            public string RespuestaOpcion { get; set; }

            /// <summary>
            /// Fecha de creacion de la opcion
            /// </summary>
            public DateTime FechaCreacion { get; set; }

            /// <summary>
            /// Fecha de ultima modificación de la opcion
            /// </summary>
            public DateTime FechaModificacion { get; set; }

        #endregion
        
    }
}
