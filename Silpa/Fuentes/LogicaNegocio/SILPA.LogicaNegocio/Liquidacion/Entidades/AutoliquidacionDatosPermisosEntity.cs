using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Liquidacion.Entidades
{
    public class AutoliquidacionDatosPermisosEntity : EntidadSerializable
    {
        /// <summary>
        /// Identificador de la lqiuidacion
        /// </summary>
        public int LiquidacionID { get; set; }

        /// <summary>
        /// Pemriso solicitado
        /// </summary>
        public string Permiso { get; set; }

        /// <summary>
        /// Entidad al cual pertenece el permiso
        /// </summary>
        public string EntidadPermiso { get; set; }   

        /// <summary>
        /// Numero de permisos solicitados
        /// </summary>
        public string NumeroPermisos { get; set; }

        /// <summary>
        /// Valor de la administracion
        /// </summary>
        public string ValorAdministracion { get; set; }

        /// <summary>
        /// Valor del servicio
        /// </summary>
        public string ValorServicio { get; set; }

        /// <summary>
        /// Valor total 
        /// </summary>
        public string ValorTotal { get; set; }

        /// <summary>
        /// Valor total en letras
        /// </summary>
        public string ValorTotalLetras { get; set; }
    }
}
