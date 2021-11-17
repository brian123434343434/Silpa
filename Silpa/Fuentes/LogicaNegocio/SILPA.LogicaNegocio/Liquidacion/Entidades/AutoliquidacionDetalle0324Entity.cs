using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Liquidacion.Entidades
{
    public class AutoliquidacionDetalle0324Entity : EntidadSerializable
    {
        /// <summary>
        /// Identificador de la lqiuidacion
        /// </summary>
        public int LiquidacionID { get; set; }

        /// <summary>
        /// Valor liquidado para el servicio
        /// </summary>
        public string ValorServicio { get; set; }

        /// <summary>
        /// Valor liquidado de administración
        /// </summary>
        public string ValorAdministracion { get; set; }

        /// <summary>
        /// Valor de los tiquetes
        /// </summary>
        public string ValorTiquetes { get; set; }

        /// <summary>
        /// Valor de los permisos
        /// </summary>
        public string ValorPermisos { get; set; }

        /// <summary>
        /// Valor Total
        /// </summary>
        public string ValorTotal { get; set; }

        /// <summary>
        /// Número de la tabla
        /// </summary>
        public string NumeroTabla { get; set; }

        /// <summary>
        /// NOmbre de la MicroTabla
        /// </summary>
        public string NombreMicroTabla { get; set; }

        /// <summary>
        /// Informnacion de liquidación de las microtablas
        /// </summary>
        public List<AutoliquidacionDatosMicroTablaEntity> DatosMicrotabla { get; set; }

        /// <summary>
        /// Listado de permisos solicitados
        /// </summary>
        public List<AutoliquidacionDatosPermisosEntity> Permisos { get; set; }

        /// <summary>
        /// Listado de tiquetes
        /// </summary>
        public List<AutoliquidacionTiquetesEntity> Tiquetes { get; set; }
    }
}
