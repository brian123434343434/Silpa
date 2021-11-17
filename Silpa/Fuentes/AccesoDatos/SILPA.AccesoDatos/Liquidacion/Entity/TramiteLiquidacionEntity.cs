using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class TramiteLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Clase de solicitud de liquidacion a la cual pertenece el tramite
            /// </summary>
            public ClaseSolicitudLiquidacionEntity ClaseSolicitud { get; set; }

            /// <summary>
            /// Identificador del tramite de liquidación
            /// </summary>
            public int TramiteID { get; set; }

            /// <summary>
            /// Tramite de liquidación
            /// </summary>
            public string Tramite { get; set; }

        #endregion
        
    }
}
