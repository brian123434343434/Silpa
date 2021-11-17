using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class RegionLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la region donde se ubica la liquidación
            /// </summary>
            public int RegionID { get; set; }

            /// <summary>
            /// Region donde se ubica el proyecto de liquidación
            /// </summary>
            public string Region { get; set; }


            /// <summary>
            /// Indica si la Region se encuentra activa
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
