using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.ServiciosREST.Servicios.ANLA.Notificaciones.Entidades
{
    public class FormaConsultaActoParaRecursoEntity
    {
        #region Objetos

            /// <summary>
            /// Fecha de rango desde
            /// </summary>
            private DateTime _objFechaActoDesde;
            /// <summary>
            /// Fecha de rango hasta
            /// </summary>
            private DateTime _objFechaActoHasta;

        #endregion


        #region Propiedades
        /// <summary>
        /// Id application user
        /// </summary>
        public long IDApplicationUser { get; set; }
        /// <summary>
        /// Numero VITAL
        /// </summary>
        public string NumeroVITAL { get; set; }
        /// <summary>
        /// Codigo del expediente
        /// </summary>
        public string Expediente { get; set; }
        /// <summary>
        /// Numero del acto administrativo
        /// </summary>
        public string NumeroActoAdministrativo { get; set; }
        /// <summary>
        /// Fecha de rango desde
        /// </summary>
        public DateTime FechaActoDesde
        {
            get { return this._objFechaActoDesde; }
            set { this._objFechaActoDesde = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
        }
        /// <summary>
        /// Fecha de rango hasta
        /// </summary>
        public DateTime FechaActoHasta
        {
            get { return this._objFechaActoHasta; }
            set { this._objFechaActoHasta = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
        }
        #endregion
    }
}
