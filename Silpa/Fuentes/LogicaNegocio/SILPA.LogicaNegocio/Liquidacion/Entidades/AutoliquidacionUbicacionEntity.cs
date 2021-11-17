using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Liquidacion.Entidades
{
    public class AutoliquidacionUbicacionEntity : EntidadSerializable
    {
        /// <summary>
        /// Departamento de ubicación
        /// </summary>
        public string Departamento { get; set; }

        /// <summary>
        /// Municipio de ubicación
        /// </summary>
        public string Municipio { get; set; }
    }
}
