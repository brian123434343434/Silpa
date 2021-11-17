using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.Servicios.Liquidacion.Entidades
{
    public class AutoliquidacionArchivoEntity : EntidadSerializable
    {
        /// <summary>
        /// Archivo en string base 64
        /// </summary>
        public string Archivo { get; set; }

        /// <summary>
        /// Respuesta del proceso
        /// </summary>
        public AutoliquidacionRespuestaEntity Respuesta { get; set; }
    }
}
