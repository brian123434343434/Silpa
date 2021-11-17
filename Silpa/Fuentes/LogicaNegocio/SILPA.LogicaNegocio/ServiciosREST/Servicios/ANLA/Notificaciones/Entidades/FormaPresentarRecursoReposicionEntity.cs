using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.ServiciosREST.Servicios.ANLA.Notificaciones.Entidades
{
    public class FormaPresentarRecursoReposicionEntity
    {
        #region Propiedades


        /// <summary>
        /// Id del acto administrativo
        /// </summary>
        public decimal ActoAdministrativoID { get; set; }
        /// <summary>
        /// Descripcion del recurso a presentar
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Estado actual del acto
        /// </summary>
        public int EstadoRecursoID { get; set; }
        /// <summary>
        /// Numero identificacion de la persona
        /// </summary>
        public string NumeroIdentificacion { get; set; }
        /// <summary>
        /// Codigo del expediente padre
        /// </summary>
        public string ExpedientePadre { get; set; }
        /// <summary>
        /// Numero VITAL adicional
        /// </summary>
        public string NumeroVITALAdicional { get; set; }
        /// <summary>
        /// Numero VITAL padre
        /// </summary>
        public string NumeroVITALPadre { get; set; }
        /// <summary>
        /// Numero VITAL generado
        /// </summary>
        public string NumeroVITALGenerado { get; set; }
        /// <summary>
        /// Numero del acto adminitrativo
        /// </summary>
        public string NumeroActoAdministrativo { get; set; }
        /// <summary>
        /// EstadoPersonaActoID
        /// </summary>
        public int EstadoPersonaActoID { get; set; }
        #endregion
    }
}
