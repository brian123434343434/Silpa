using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Liquidacion.Entidades
{
    public class AutoliquidacionPermisoCobroEntity : EntidadSerializable
    {
        /// <summary>
        /// Nombre del permiso
        /// </summary>
        public string Permiso { get; set; }

        /// <summary>
        /// Autoridad a la cual pertenece
        /// </summary>
        public string Autoridad { get; set; }

        /// <summary>
        /// Numero de permisos
        /// </summary>
        public int NumeroPermisos { get; set; }

        /// <summary>
        /// Valor de la administración
        /// </summary>
        public decimal ValorAdministracion { get; set; }

        /// <summary>
        /// Valor del servicio
        /// </summary>
        public decimal ValorServicio { get; set; }

        /// <summary>
        /// Valor Total de los permisos
        /// </summary>
        public decimal ValorTotal { get; set; }

    }
}
