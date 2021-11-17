using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.ServiciosREST.Servicios.ANLA.Notificaciones.Entidades
{
    public class RecursoParaPresentarEntity
    {
        #region Objetos

            /// <summary>
            /// Fecha del acto administrativo
            /// </summary>
            private DateTime _objFechaActoAdministrativo;
            /// <summary>
            /// Fecha de la notificacion
            /// </summary>
            private DateTime _objFechaNotificacion;

        #endregion

        #region Propiedades
        /// <summary>
        /// Id del acto de notificacion
        /// </summary>
        public int ActoNotificacionID { get; set; }
        /// <summary>
        /// Id de la persona
        /// </summary>
        public int PersonaID { get; set; }
        /// <summary>
        /// Id del estado actual
        /// </summary>
        public int EstadoActualID { get; set; }
        /// <summary>
        /// id del estado futuro
        /// </summary>
        public int EstadoFuturoID { get; set; }
        /// <summary>
        /// Id del flujo
        /// </summary>
        public int FlujoID { get; set; }
        /// <summary>
        /// Numero identificacion del usuario
        /// </summary>
        public string IdentificacionUsuario { get; set; }
        /// <summary>
        /// Id autoridad ambiental
        /// </summary>
        public int AutoridadID { get; set; }
        /// <summary>
        /// Nombre de la autoridad ambiental
        /// </summary>
        public string Autoridad { get; set; }
        /// <summary>
        /// Ocdigo del expediente
        /// </summary>
        public string Expediente { get; set; }
        /// <summary>
        /// Numero vital del recurso 
        /// </summary>
        public string NumeroVITAL { get; set; }
        /// <summary>
        /// Numero del acto administrativo
        /// </summary>
        public string NumeroActoAdministrativo { get; set; }
        /// <summary>
        /// Fecha del acto administrativo
        /// </summary>
        public DateTime FechaActoAdministrativo
        {
            get { return this._objFechaActoAdministrativo; }
            set { this._objFechaActoAdministrativo = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
        }
        /// <summary>
        /// Ruta del documento
        /// </summary>
        public string RutaDocumento { get; set; }
        /// <summary>
        /// Fecha de la notificacion
        /// </summary>
        public DateTime FechaNotificacion
        {
            get { return this._objFechaNotificacion; }
            set { this._objFechaNotificacion = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
        }
        /// <summary>
        /// Identificador de estado persona Acto ID
        /// </summary>
        public int EstadoPersonaActoID { get; set; }

        #endregion

        #region Metodo publico
        /// <summary>
        /// Retornar en un string la información del objeto
        /// </summary>
        /// <returns>string con la información contenida en el objeto</returns>
        public override string ToString()
        {
            string strDatos = "ActoNotificacionID: " + this.ActoNotificacionID.ToString() + " -- " +
                                "PersonaID: " + this.PersonaID.ToString() + " -- " +
                                "EstadoActualID: " + this.EstadoActualID.ToString() + " -- " +
                                "EstadoFuturoID: " + this.EstadoFuturoID.ToString() + " -- " +
                                "FlujoID: " + this.FlujoID.ToString() + " -- " +
                                "IdentificacionUsuario: " + (this.IdentificacionUsuario ?? "null") + " -- " +
                                "AutoridadID: " + this.AutoridadID.ToString() + " -- " +
                                "Autoridad: " + this.Autoridad.ToString() + " -- " +
                                "Expediente: " + this.Expediente.ToString() + " -- " +
                                "NumeroVITAL: " + this.NumeroVITAL + " -- " +
                                "NumeroActoAdministrativo: " + this.NumeroActoAdministrativo.ToString() + " -- " +
                                "FechaActoAdministrativo: " + this.FechaActoAdministrativo.ToString() + " -- " +
                                "RutaDocumento: " + this.RutaDocumento + " -- " +
                                "FechaNotificacion: " + this.FechaNotificacion.ToString() + " -- " +
                                "EstadoPersonaActoID:" + this.EstadoPersonaActoID.ToString();

            return strDatos;
        }
        #endregion
    }
}
