using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Encuestas.Encuesta.Entity
{
    [Serializable]
    public class TipoOpcionPreguntaEncuestaEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del tipo de opcion
            /// </summary>
            public int TipoOpcionPreguntaID { get; set; }

            /// <summary>
            /// Nombre del tipo de opcion
            /// </summary>
            public string TipoOpcionPregunta { get; set; }

            /// <summary>
            /// Indica si el tipo de opcion se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
