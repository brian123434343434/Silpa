using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class ClaseSolicitudLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// tipo de solicitud de liquidacion
            /// </summary>
            public TipoSolicitudLiquidacionEntity TipoSolicitud { get; set; }

            /// <summary>
            /// Identificador de la clase de solicitud de liquidación
            /// </summary>
            public int ClaseSolicitudID { get; set; }

            /// <summary>
            /// Clase de solicitud de liquidación
            /// </summary>
            public string ClaseSolicitud { get; set; }

        #endregion
        
    }
}
