using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class EstadoCobroLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del estado de cobro
            /// </summary>
            public int EstadoCobroID { get; set; }

            /// <summary>
            /// Nombre del estado del cobro
            /// </summary>
            public string EstadoCobro { get; set; }


            /// <summary>
            /// Indica si el cobro se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
