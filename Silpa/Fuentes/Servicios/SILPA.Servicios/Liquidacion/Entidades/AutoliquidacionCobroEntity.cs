using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.Servicios.Liquidacion.Entidades
{
    public class AutoliquidacionCobroEntity : EntidadSerializable
    {
        /// <summary>
        /// Numero SILPA
        /// </summary>
        public string NumeroSILPA { get; set; }

        /// <summary>
        /// Codigo del Expediente
        /// </summary>
        public string CodigoExpediente { get; set; }

        /// <summary>
        /// Concepto de Cobro
        /// </summary>
        public int ConceptoCobro { get; set; }

        /// <summary>
        /// Descripción del cobro
        /// </summary>
        public string DescripcionCobro { get; set; }

        /// <summary>
        /// Número de referencia del cobro
        /// </summary>
        public string NumeroReferencia { get; set; }

        /// <summary>
        /// Código de barras del cobro
        /// </summary>
        public string CodigoBarras { get; set; }

        /// <summary>
        /// Indicador del proceso
        /// </summary>
        public string IndicadorProceso { get; set; }

        /// <summary>
        /// Número del documento
        /// </summary>
        public string NumeroDocumento { get; set; }

        /// <summary>
        /// Valor de los viaticos diarios
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de pago oportuno factura de factura
        /// </summary>
        public DateTime FechaPagoOportuno { get; set; }

        /// <summary>
        /// Fecha de venciemiento de factura
        /// </summary>
        public DateTime FechaVencimiento { get; set; }

        /// <summary>
        /// Valor del servicio
        /// </summary>
        public decimal ValorCobroServicio { get; set; }

        /// <summary>
        /// Lista de permisos
        /// </summary>
        public List<AutoliquidacionPermisoCobroEntity> Permisos { get; set; }

        /// <summary>
        /// Origen del cobro
        /// </summary>
        public string OrigenCobro { get; set; }

    }
}
