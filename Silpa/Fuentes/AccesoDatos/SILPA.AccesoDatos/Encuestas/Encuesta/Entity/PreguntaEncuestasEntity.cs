using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Encuestas.Encuesta.Entity
{
    [Serializable]
    public class PreguntaEncuestasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la pregunta
            /// </summary>
            public int PreguntaID { get; set; }

            /// <summary>
            /// Tipo de pregunta
            /// </summary>
            public TipoPreguntaEncuestaEntity TipoPregunta { get; set; }

            /// <summary>
            /// Identificador de la seccion a la cual pertenece la pregunta
            /// </summary>
            public int SeccionID { get; set; }

            /// <summary>
            /// Identificador del formulario al cual pertenece la pregunta
            /// </summary>
            public int FormularioID { get; set; }

            /// <summary>
            /// Identificador del sector al cual pertenece la pregunta
            /// </summary>
            public int SectorID { get; set; }

            /// <summary>
            /// Identificador de la pregunta padre
            /// </summary>
            public int PreguntaPadreID { get; set; }

            /// <summary>
            /// Texto de la pregunta
            /// </summary>
            public string Pregunta { get; set; }

            /// <summary>
            /// Texto ayuda de la pregunta
            /// </summary>
            public string AyudaPregunta { get; set; }

            /// <summary>
            /// Indica si la pregunta es oblgatoria
            /// </summary>
            public bool Obligatorio { get; set; }

            /// <summary>
            /// Indica el orden de la pregunta en la seccion
            /// </summary>
            public int Orden { get; set; }

            /// <summary>
            /// Indica el númeral a imprimir en pantalla
            /// </summary>
            public string Numeral { get; set; }

            /// <summary>
            /// Indica el nivel en el cual se encuentra la pregunta
            /// </summary>
            public int Nivel { get; set; }

            /// <summary>
            /// Listado de opciones relacionadas a la pregunta
            /// </summary>
            public List<OpcionPreguntaEncuestasEntity> OpcionesPregunta { get; set; }

            /// <summary>
            /// Listado de identificadores de opciones que habilitan la pregunta
            /// </summary>
            public List<PreguntaHabilitaOpcionEncuestasEntity> OpcionesHabilitanPregunta { get; set; }

            /// <summary>
            /// Indica si la pregunta se encuentra activa
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
