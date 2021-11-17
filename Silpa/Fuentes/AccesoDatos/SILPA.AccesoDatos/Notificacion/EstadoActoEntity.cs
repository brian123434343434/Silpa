using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    public class EstadoActoEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del estado
            /// </summary>
            public int EstadoID { get; set; }

            /// <summary>
            /// Nombre o descripción del estado
            /// </summary>
            public string Estado { get; set; }

            /// <summary>
            /// Indica si el estado permite visualizar información en área de trabajo
            /// </summary>
            public bool PermiteVisualizarInformacion { get; set; }

            /// <summary>
            /// Indica si se encuentra activo el estado
            /// </summary>
            public bool Activo { get; set; }

        #endregion
    }
}
