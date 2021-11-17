using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class TipoCoordenadaUbicacionLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del tipo de coordenada a utilizar para una coordenada
            /// </summary>
            public int TipoCoordenadaID { get; set; }

            /// <summary>
            /// Tipo de coordenada a utilizar para una coordenada
            /// </summary>
            public string TipoCoordenada { get; set; }

            /// <summary>
            /// Indica si se encuentra activo el tipo de geometria
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
