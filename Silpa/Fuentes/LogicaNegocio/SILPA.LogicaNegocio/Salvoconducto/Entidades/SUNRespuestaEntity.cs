using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Salvoconducto.Entidades
{
    public class SUNRespuestaEntity : EntidadSerializable
    {
        /// <summary>
        /// Codigo de respuesta
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Mensaje de respuesta
        /// </summary>
        public string Mensaje { get; set; }
    }
}
