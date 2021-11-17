using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Liquidacion.Entidades
{
    public class AutoliquidacionTiquetesEntity : EntidadSerializable
    {
        /// <summary>
        /// Identificador de la lqiuidacion
        /// </summary>
        public int LiquidacionID { get; set; }

        /// <summary>
        /// Tipo del tiquete
        /// </summary>
        public string TipoTiquete { get; set; }

        /// <summary>
        /// Departamento de origen
        /// </summary>
        public string DepartamentoOrigen { get; set; }

        /// <summary>
        /// Municipio de origen
        /// </summary>
        public string MunicipioOrigen { get; set; }

        /// <summary>
        /// Departamento destino
        /// </summary>
        public string DepartamentoDestino { get; set; }

        /// <summary>
        /// Municipio de destino
        /// </summary>
        public string MunicipioDestino { get; set; }

        /// <summary>
        /// Valor del tiquete
        /// </summary>
        public string ValorTiquete { get; set; }

        /// <summary>
        /// Numero Tiquetes
        /// </summary>
        public string NumeroTiquetes { get; set; }

        /// <summary>
        /// Valor Total de los Tiquetes
        /// </summary>
        public string ValorTotalTiquetes { get; set; }
    }
}
