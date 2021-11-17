using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Encuestas.Encuesta.Entity
{
    [Serializable]
    public class SeccionEncuestasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la seccion
            /// </summary>
            public int SeccionID { get; set; }

            /// <summary>
            /// Nombre de la seccion
            /// </summary>
            public string Seccion { get; set; }

            /// <summary>
            /// Listado de preguntas que componen a la sección
            /// </summary>
            public List<PreguntaEncuestasEntity> Preguntas { get; set; }

            /// <summary>
            /// Indica si la seccion se encuentra activa
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
