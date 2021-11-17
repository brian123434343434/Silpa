using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class CoordenadaUbicacionLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la coordenada
            /// </summary>
            public int CoordenadaUbicacionLiquidacionID { get; set; }

            /// <summary>
            /// Ubicación a la cual pertenece la coordenada
            /// </summary>
            public int UbicacionLiquidacionID { get; set; }

            /// <summary>
            /// Identificador del identificador de solicitud
            /// </summary>
            public int SolicitudLiquidacionID { get; set; }

            /// <summary>
            /// Identificador del tipo de solicitud de liquidacion
            /// </summary>
            public string Localizacion { get; set; }

            /// <summary>
            /// Tipo de geometria de la coordenada
            /// </summary>
            public TipoGeometriaCoordenadaLiquidacionEntity TipoGeometria { get; set; }

            /// <summary>
            /// Tipo de solicitud de liquidación
            /// </summary>
            public TipoCoordenadaUbicacionLiquidacionEntity TipoCoordenada { get; set; }

            /// <summary>
            /// Origen magna de la coordenda
            /// </summary>
            public OrigenMagnaCoordenadaLiquidacionEntity OrigenMagna { get; set; }

            /// <summary>
            /// Coordenada norte
            /// </summary>
            public string Norte { get; set; }

            /// <summary>
            /// Coordenda este
            /// </summary>
            public string Este { get; set; }

        #endregion
        
    }
}
