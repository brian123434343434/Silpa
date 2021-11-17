using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Generico;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class CobroSolicitudLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del campo asociado a la solicitud
            /// </summary>
            public int CobroSolicitudLiquidacionEntityID { get; set; }

            /// <summary>
            /// Identificador de la solicitud
            /// </summary>
            public int SolicitudLiquidacionID { get; set; }

            /// <summary>
            /// Autoridad a la cual pertenece el cobro
            /// </summary>
            public AutoridadAmbientalIdentity AutoridadAmbiental { get; set; }

            /// <summary>
            /// Concepto del cobro realizado
            /// </summary>
            public string Concepto { get; set; }

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
            public int CobroID { get; set; }

            /// <summary>
            /// Valor del cobro
            /// </summary>
            public decimal ValorCobro { get; set; }

            /// <summary>
            /// Fecha de vencimiento del cobro
            /// </summary>
            public DateTime FechaVencimiento { get; set; }

            /// <summary>
            /// Estado del cobro
            /// </summary>
            public EstadoCobroLiquidacionEntity EstadoCobro { get; set; }

        #endregion
        
    }
}
