using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Encuestas.Encuesta.Entity
{
    [Serializable]
    public class OpcionPreguntaEncuestasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la opcion de la pregunta
            /// </summary>
            public int OpcionPreguntaID { get; set; }

            /// <summary>
            /// Identificador de la pregunta
            /// </summary>
            public int PreguntaID { get; set; }

            /// <summary>
            /// Tipo de opcion
            /// </summary>
            public TipoOpcionPreguntaEncuestaEntity TipoOpcion { get; set; }

            /// <summary>
            /// Texto de la opcion de la pregunta
            /// </summary>
            public string TextoOpcion { get; set; }

            /// <summary>
            /// Indica el orden de la opcion
            /// </summary>
            public int Orden { get; set; }

            /// <summary>
            /// Indica si la opcion se encuentra activa
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
