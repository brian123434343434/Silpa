using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Encuestas.Encuesta.Entity
{
    [Serializable]
    public class PreguntaHabilitaOpcionEncuestasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del registro
            /// </summary>
            public int PreguntaHabilitaOpcionID { get; set; }
            
            /// <summary>
            /// Identificador de la pregunta
            /// </summary>
            public int PreguntaID { get; set; }

            /// <summary>
            /// Identificador de la opcion
            /// </summary>
            public int OpcionID { get; set; }

            /// <summary>
            /// Indica si la opcion es opcional para mostrar
            /// </summary>
            public bool EsOpcional { get; set; }

        #endregion
        
    }
}
