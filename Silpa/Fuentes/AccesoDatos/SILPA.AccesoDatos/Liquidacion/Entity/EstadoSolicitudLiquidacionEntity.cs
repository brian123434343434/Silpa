using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class EstadoSolicitudLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del estado de solicitud
            /// </summary>
            public int EstadoSolicitudID { get; set; }

            /// <summary>
            /// Nombre del estado
            /// </summary>
            public string EstadoSolicitud { get; set; }


            /// <summary>
            /// Indica si el estado se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
