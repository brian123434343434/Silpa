using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Liquidacion.Entidades
{
    public class AutoliquidacionRespuestaCobroEntity : EntidadSerializable
    {
        #region Propiedades

            /// <summary>
            /// Identificador del cobro generado
            /// </summary>
            public int CobroID { get; set; }

            /// <summary>
            /// Información de respuesta
            /// </summary>
            public AutoliquidacionRespuestaEntity Respuesta { get; set; }

        #endregion
    }
}
