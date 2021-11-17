using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Salvoconducto.Entidades
{
    public class ResolucionEntity : EntidadSerializable
    {
        /// <summary>
        /// Identificador de la autoridad ambiental
        /// </summary>
        public int AutoridadAmbientalID { get; set; }

        /// <summary>
        /// Identificador del solicitante al cual se emitio la resolución
        /// </summary>
        public int SolicitanteID { get; set; }

        /// <summary>
        /// Número de la resolución asociada al salvoconducto
        /// </summary>
        public string NumeroResolucion { get; set; }

        /// <summary>
        /// Fecha de la resolución
        /// </summary>
        public DateTime FechaResolucion { get; set; }

    }
}
