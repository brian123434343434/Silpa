using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;

namespace SILPA.AccesoDatos.Encuestas.Contingencias.Entity
{
    [Serializable]
    public class CoordenadasPreguntaSolicitudContingenciasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de las coordenadas asociadas a la pregunta
            /// </summary>
            public int CoordenadasPreguntaSolicitudID { get; set; }

            /// <summary>
            /// Identificador de la pregunta asociada a la solicitud
            /// </summary>
            public int PreguntaSolicitudID { get; set; }

            /// <summary>
            /// Identificador de la solicitud
            /// </summary>
            public int SolicitudID { get; set; }

            /// <summary>
            /// Pregunta asociada a la solicitud
            /// </summary>
            public PreguntaEncuestasEntity Pregunta { get; set; }

            /// <summary>
            /// Grados de la longitud
            /// </summary>
            public decimal GradosLongitud { get; set; }

            /// <summary>
            /// Minutos de la longitud
            /// </summary>
            public decimal MinutosLongitud { get; set; }

            /// <summary>
            /// Segundos de la longitud
            /// </summary>
            public decimal SegundosLongitud { get; set; }

            /// <summary>
            /// Grados de la latitud
            /// </summary>
            public decimal GradosLatitud { get; set; }

            /// <summary>
            /// Minutos de la latitud
            /// </summary>
            public decimal MinutosLatitud { get; set; }

            /// <summary>
            /// Segundos de la latitud
            /// </summary>
            public decimal SegundosLatitud { get; set; }

            /// <summary>
            /// Fecha de creacion de las coordenadas
            /// </summary>
            public DateTime FechaCreacion { get; set; }

            /// <summary>
            /// Fecha de ultima modificación de las coordenadas
            /// </summary>
            public DateTime FechaModificacion { get; set; }

        #endregion
        
    }
}
