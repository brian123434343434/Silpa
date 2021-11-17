using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.IntegracionCorporaciones.Entidades
{
    public class IntegracionCorporacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la autoridad
            /// </summary>
            public int  IntegracionCorporacionID { get; set; }

            /// <summary>
            /// Identificador de la autoridad
            /// </summary>
            public int  AutoridadID { get; set; }

            /// <summary>
            /// Identificador de la autoridad
            /// </summary>
            public string Autoridad { get; set; }

            /// <summary>
            /// Credenciales de acceso a servicios
            /// </summary>
            public CredencialesEntity Credenciales { get; set; }

            /// <summary>
            /// URL de servicio de autorizacion
            /// </summary>
            public string ServicioAutorizacion { get; set; }

            /// <summary>
            /// URL de servicio de verificacion token
            /// </summary>
            public string ServicioverificacionToken { get; set; }

            /// <summary>
            /// URL de servicio de obtencion de menus
            /// </summary>
            public string ServicioMenu { get; set; }

            /// <summary>
            /// URL de servicio de creacion de sesion remota
            /// </summary>
            public string ServicioCrearSesion { get; set; }
        
            /// <summary>
            /// URL de servicio de cierre de sesion remota
            /// </summary>
            public string ServicioCerrarSesion { get; set; }

            /// <summary>
            /// Indica si la integracion se encuentra activa
            /// </summary>
            public bool Activo { get; set; }

            /// <summary>
            /// Fecha de creacion del registro
            /// </summary>
            public DateTime FechaCreacion { get; set; }

            /// <summary>
            /// Fecha de modificacion del registro
            /// </summary>
            public DateTime FechaUltimaModificacion { get; set; }

        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Retornar en un string la información del objeto
            /// </summary>
            /// <returns>string con la información contenida en el objeto</returns>
            public override string ToString()
            {
                string strDatos = "IntegracionCorporacionID: " + this.IntegracionCorporacionID.ToString()  + " -- " +
                                    "AutoridadID: " + this.AutoridadID.ToString()  + " -- " +
                                    "Autoridad: " + (this.Autoridad ?? "null")  + " -- " +
                                    "Credenciales: " + (this.Credenciales != null ? this.Credenciales.ToString() : "null") + " -- " +
                                    "ServicioMenu: " + (this.ServicioMenu ?? "null")  + " -- " +
                                    "ServicioCrearSesion: " + (this.ServicioCrearSesion ?? "null")  + " -- " +
                                    "ServicioCerrarSesion: " + (this.ServicioCerrarSesion ?? "null")  + " -- " +
                                    "FechaCreacion: " + this.FechaCreacion.ToString()  + " -- " +
                                    "FechaUltimaModificacion: " + this.FechaUltimaModificacion.ToString();
                
                return strDatos;
            }

        #endregion

    }
}
