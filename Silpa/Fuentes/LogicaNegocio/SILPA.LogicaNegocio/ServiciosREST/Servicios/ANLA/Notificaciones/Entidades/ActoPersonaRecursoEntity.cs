using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.ServiciosREST.Servicios.ANLA.Notificaciones.Entidades
{
    public class ActoPersonaRecursoEntity
    {
         #region Objetos

            /// <summary>
            /// Fecha de acto
            /// </summary>
            private DateTime _objFechaActo;
            /// <summary>
            /// Fecha de notificacion
            /// </summary>
            private DateTime _objFechaNotificacion;

        #endregion

        #region Propiedades

            /// <summary>
            /// Id del acto de notificacion
            /// </summary>
            public string NumeroVITAL { get; set; }

            /// <summary>
            /// Id de la autoridad ambiental
            /// </summary>
            public int AutoridadID { get; set; }

            /// <summary>
            /// Nombre de la autoridad ambiental
            /// </summary>
            public string NombreAutoridad { get; set; }

            /// <summary>
            /// Codigo del expediente
            /// </summary>
            public string Expediente { get; set; }

            /// <summary>
            /// Numero del acto administrativo
            /// </summary>
            public string NumeroActo { get; set; }

            /// <summary>
            /// Fecha de acto administrativo
            /// </summary>
            public DateTime FechaActo
            {
                get { return this._objFechaActo; }
                set { this._objFechaActo = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
            }

            /// <summary>
            /// Fecha de Notificacion
            /// </summary>
            public DateTime FechaNotificacion
            {
                get { return this._objFechaNotificacion; }
                set { this._objFechaNotificacion = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
            }


        #endregion

        #region Metodo publico

            /// <summary>
            /// Retornar en un string la información del objeto
            /// </summary>
            /// <returns>string con la información contenida en el objeto</returns>
            public override string ToString()
            {
                string strDatos = "NumeroVITAL: " + (this.NumeroVITAL ?? "null") + " -- " +
                                   "AutoridadID: " + this.AutoridadID.ToString() + " -- " +
                                   "NombreAutoridad: " + (this.NombreAutoridad ?? "null") + " -- " +
                                   "Expediente: " + (this.Expediente ?? "null") + " -- " +
                                   "NumeroActo: " + (this.NumeroActo ?? "null") + " -- " +
                                   "Expediente: " + (this.Expediente ?? "null") + " -- " +
                                   "FechaActo: " + this.FechaActo.ToString() + " -- " +
                                    "FechaNotificacion: " + this.FechaNotificacion.ToString();

                return strDatos;
            }

        #endregion
    }
}
