using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class OrigenMagnaCoordenadaLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del origen magna de la coordenada
            /// </summary>
            public int OrigenMagnaID { get; set; }

            /// <summary>
            /// Origen magna de la coordenada
            /// </summary>
            public string OrigenMagna { get; set; }

            /// <summary>
            /// Indica si se encuentra activo el tipo de geometria
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
