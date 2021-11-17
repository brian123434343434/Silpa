using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class TipoGeometriaCoordenadaLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del tipo de geometria a utilizar para una coordenada
            /// </summary>
            public int TipoGeometriaID { get; set; }

            /// <summary>
            /// Tipo de geometria a utilizar para una coordenada
            /// </summary>
            public string TipoGeometria { get; set; }

            /// <summary>
            /// Indica si se encuentra activo el tipo de geometria
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
