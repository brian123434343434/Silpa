using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    [Serializable]
    public class EstadoNotificacionEntity
    {        
        #region Propiedades

            /// <summary>
            /// Identificador del estado
            /// </summary>
            public int ID { get; set; }

            /// <summary>
            /// Nombre del estado
            /// </summary>
            public string Estado { get; set; }

            /// <summary>
            /// Descripción del estado
            /// </summary>
            public string Descripcion { get; set; }

            /// <summary>
            /// Indica si el estado es PDI
            /// </summary>
            public bool EstadoPDI { get; set; }

            /// <summary>
            /// Indica si el estado se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

        #endregion

    }
}
