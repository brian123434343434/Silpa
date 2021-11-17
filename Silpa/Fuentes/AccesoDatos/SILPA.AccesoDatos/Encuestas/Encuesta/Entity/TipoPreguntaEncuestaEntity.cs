using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Encuestas.Encuesta.Entity
{
    [Serializable]
    public class TipoPreguntaEncuestaEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del tipo de pregunta
            /// </summary>
            public int TipoPreguntaID { get; set; }

            /// <summary>
            /// Nombre del tipo de pregunta
            /// </summary>
            public string TipoPregunta { get; set; }

            /// <summary>
            /// Indica si el tipo de pregunta se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
