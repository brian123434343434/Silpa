using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Liquidacion.Entidades
{
    public class AutoliquidacionSolicitudRespuestaEntity : EntidadSerializable
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la liquidación
            /// </summary>
            public int LiquidacionID { get; set; }

            /// <summary>
            /// Identificador del cobro de la liquidación
            /// </summary>
            public int CobroLiquidacionID { get; set; }

            /// <summary>
            /// Identificador del detalle del cobro de la liquidación
            /// </summary>
            public int DetalleCobroLiquidacionID { get; set; }

            /// <summary>
            /// Identificador del cobro en VITAL
            /// </summary>
            public int CobroVITALID { get; set; }

            /// <summary>
            /// Respuesta resultado del proceso de solicitud
            /// </summary>
            public AutoliquidacionRespuestaEntity Respuesta { get; set; }

        #endregion

    }
}
