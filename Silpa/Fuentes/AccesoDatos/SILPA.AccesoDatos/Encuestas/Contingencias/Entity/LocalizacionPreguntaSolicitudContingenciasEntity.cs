using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;

namespace SILPA.AccesoDatos.Encuestas.Contingencias.Entity
{
    [Serializable]
    public class LocalizacionPreguntaSolicitudContingenciasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la Localizacion asociada a la pregunta
            /// </summary>
            public int LocalizacionPreguntaSolicitudID { get; set; }

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
            /// Departamento de localizacion
            /// </summary>
            public string Departamento { get; set; }

            /// <summary>
            /// Ciudad de localizacion
            /// </summary>
            public string Ciudad { get; set; }

            /// <summary>
            /// Fecha de creacion de la localizacion
            /// </summary>
            public DateTime FechaCreacion { get; set; }

            /// <summary>
            /// Fecha de ultima modificación de la localizacion
            /// </summary>
            public DateTime FechaModificacion { get; set; }

        #endregion
        
    }
}
