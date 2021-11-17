using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class OceanoLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del oceano
            /// </summary>
            public int OceanoID { get; set; }

            /// <summary>
            /// Nombre del oceano
            /// </summary>
            public string Oceano { get; set; }


            /// <summary>
            /// Indica si el oceano se encuentra activa
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
