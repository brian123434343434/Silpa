using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Liquidacion.Entidades
{
    public class AutoliquidacionDetalle633Entity : EntidadSerializable
    {
        /// <summary>
        /// Identificador de la lqiuidacion
        /// </summary>
        public int LiquidacionID { get; set; }

        /// <summary>
        /// Valor del proyecto a realizar 
        /// </summary>
        public string Valorproyecto { get; set; }

        /// <summary>
        /// Valor del salario minimo
        /// </summary>
        public string ValorSalario { get; set; }

        /// <summary>
        /// Relacion costo proyecto salario minimo
        /// </summary>
        public string Relacion { get; set; }

        /// <summary>
        /// Tarifa a Aplicar
        /// </summary>
        public string TarifaAplicar { get; set; }

        /// <summary>
        /// Valor Total
        /// </summary>
        public string ValorTotal { get; set; }
    }
}
