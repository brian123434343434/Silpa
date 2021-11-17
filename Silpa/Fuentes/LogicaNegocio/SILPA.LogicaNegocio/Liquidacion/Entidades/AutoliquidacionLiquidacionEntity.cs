using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Liquidacion.Entidades
{
    public class AutoliquidacionLiquidacionEntity : EntidadSerializable
    {
        /// <summary>
        /// Identificador de la lqiuidacion
        /// </summary>
        public int LiquidacionID { get; set; }

        /// <summary>
        /// Resolución 
        /// </summary>
        public string Resolucion { get; set; }

        /// <summary>
        /// Valor total de los permisos
        /// </summary>
        public string ValorPermisos { get; set; }

        /// <summary>
        /// Valor de la liquidación
        /// </summary>
        public string ValorLiquidacion { get; set; }

        /// <summary>
        /// Valor total de la liquidacion
        /// </summary>
        public string ValorTotal { get; set; }

        /// <summary>
        /// Valor pagado previamente
        /// </summary>
        public string ValorPagado { get; set; }

        /// <summary>
        /// Fecha de último pago
        /// </summary>
        public string FechaPago { get; set; }

        /// <summary>
        /// Valor Saldo Pagar
        /// </summary>
        public string ValorSaldo { get; set; }

        /// <summary>
        /// Número del expediente
        /// </summary>
        public string Expediente { get; set; }

        /// <summary>
        /// Fecha de realización de la lqiuidación
        /// </summary>
        public string Fecha { get; set; }

        /// <summary>
        /// Número VITAL
        /// </summary>
        public string NumeroVITAL { get; set; }

        /// <summary>
        /// Referencia de Pago
        /// </summary>
        public string ReferenciaPago { get; set; }

        /// <summary>
        /// Nombre del solicitante
        /// </summary>
        public string Solicitante { get; set; }

        /// <summary>
        /// Identificador de la resolución
        /// </summary>
        public int ResolucionID { get; set; }

        /// <summary>
        /// Indica si los permisos son solo de la ANLA
        /// </summary>
        public bool PermisosANLA { get; set; }

        /// <summary>
        /// Detalle de los valores de la liquidación por ley 633
        /// </summary>
        public AutoliquidacionDetalle633Entity Detalle633 { get; set; }

        /// <summary>
        /// Detalle de los valores de la liquidación por resolución 0324
        /// </summary>
        public AutoliquidacionDetalle0324Entity Detalle0324 { get; set; }
    }
}
