using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class RegionSolicitudLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la region de la solicitud
            /// </summary>
            public int RegionSolicitudID { get; set; }

            /// <summary>
            /// Identificador de la solicitud
            /// </summary>
            public int SolicitudLiquidacionID { get; set; }

            /// <summary>
            /// Region donde se ubica el proyecto de liquidación
            /// </summary>
            public RegionLiquidacionEntity Region { get; set; }

        #endregion
        
    }
}
