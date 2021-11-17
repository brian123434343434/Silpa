using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Liquidacion.Entidades
{
    public class AutoliquidacionTiqueteEntity : EntidadSerializable
    {
        /// <summary>
        /// Identificador departamento de origen
        /// </summary>
        public int DepartamentoOrigenID { get; set; }

        /// <summary>
        /// Identificador municipio de origen
        /// </summary>
        public int MunicipioOrigenID { get; set; }

        /// <summary>
        /// Identificador departamento de destino
        /// </summary>
        public int DepartamentoDestinoID { get; set; }

        /// <summary>
        /// Identificador municipio de destino
        /// </summary>
        public int MunicipioDestinoID { get; set; }

        /// <summary>
        /// Departamento de origen
        /// </summary>
        public string DepartamentoOrigen { get; set; }

        /// <summary>
        /// Municipio de origen
        /// </summary>
        public string MunicipioOrigen { get; set; }

        /// <summary>
        /// Departamento de destino
        /// </summary>
        public string DepartamentoDestino { get; set; }

        /// <summary>
        /// Municipio de destino
        /// </summary>
        public string MunicipioDestino { get; set; }

        /// <summary>
        /// Indica si el tiquete es ida y vuelta
        /// </summary>
        public bool EsIdaVuelta { get; set; }

        /// <summary>
        /// Valor del tiquete
        /// </summary>
        public decimal ValorTiquete { get; set; }
    }
}
