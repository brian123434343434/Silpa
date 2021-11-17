using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Liquidacion.Entidades
{
    public class AutoliquidacionPermisoEntity : EntidadSerializable
    {
        /// <summary>
        /// Identificador del permiso
        /// </summary>
        public int PermisoID { get; set; }

        /// <summary>
        /// Descripcion del permiso
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Identificador de la entidad
        /// </summary>
        public int EntidadID { get; set; }

        /// <summary>
        /// Nombre de la entidad
        /// </summary>
        public string Entidad { get; set; }

        /// <summary>
        /// Número de permisos a solicitar
        /// </summary>
        public int NumeroPermisos { get; set; }
    }
}
