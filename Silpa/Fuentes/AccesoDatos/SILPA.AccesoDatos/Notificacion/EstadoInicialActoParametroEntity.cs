using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    public class EstadoInicialActoParametroEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la autoridad ambiental
            /// </summary>
            public int AutoridadAmbientalID { get; set; }

            /// <summary>
            /// Estado inicial para actos administrativos
            /// </summary>
            public int EstadoInicialActoID { get; set; }

            /// <summary>
            /// Estado inicial personas a notificar
            /// </summary>
            public int EstadoInicialPersonaID { get; set; }

        #endregion
    }
}
