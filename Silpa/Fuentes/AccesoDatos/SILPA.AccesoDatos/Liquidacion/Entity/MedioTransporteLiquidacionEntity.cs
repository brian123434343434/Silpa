using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class MedioTransporteLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del medio de transporte
            /// </summary>
            public int MedioTransporteID { get; set; }

            /// <summary>
            /// Nombre del medio de transporte
            /// </summary>
            public string MedioTransporte { get; set; }


            /// <summary>
            /// Indica si el medio de transporte se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
