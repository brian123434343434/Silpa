using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Liquidacion.Entidades
{
    public class AutoliquidacionDatosMicroTablaEntity : EntidadSerializable
    {
        /// <summary>
        /// Identificador de la lqiuidacion
        /// </summary>
        public int LiquidacionID { get; set; }

        /// <summary>
        /// Categoria del funcionario
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Valor honorarios del funcionario
        /// </summary>
        public string Honorario { get; set; }

        /// <summary>
        /// Tiempo dedicación funcionario
        /// </summary>
        public string Dedicacion { get; set; }

        /// <summary>
        /// Numero de visitas
        /// </summary>
        public string NumeroVisitas { get; set; }

        /// <summary>
        /// Duración de la visita
        /// </summary>
        public string Duracion { get; set; }

        /// <summary>
        /// Valor de los viaticos diarios
        /// </summary>
        public string ViaticosDiarios { get; set; }

        /// <summary>
        /// Valor total de los viaticos
        /// </summary>
        public string TotalViaticos { get; set; }

        /// <summary>
        /// Valor total de la liquidacion funcionario
        /// </summary>
        public string CostoTotal { get; set; }
    }
}
