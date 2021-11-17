using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Encuestas.Encuesta.Entity
{
    [Serializable]
    public class ExpedienteEncuestasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del solicitante
            /// </summary>
            public int SolicitanteID { get; set; }

            /// <summary>
            /// Codigo del expediente
            /// </summary>
            public string ExpedienteCodigo { get; set; }

            /// <summary>
            /// Nombre del expediente
            /// </summary>
            public string ExpedienteNombre { get; set; }

            /// <summary>
            /// Indica de la autoridad ambiental
            /// </summary>
            public int AutoridadID { get; set; }

        #endregion
        
    }
}
