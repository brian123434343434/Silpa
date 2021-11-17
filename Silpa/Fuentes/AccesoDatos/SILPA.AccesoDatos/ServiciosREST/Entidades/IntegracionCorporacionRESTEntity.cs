using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.ServiciosREST.Entidades
{
    public class IntegracionCorporacionRESTEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del servicio
            /// </summary>
            public int  IntegracionCorporacionRESTID { get; set; }

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
            public string URLServicioAutorizacion { get; set; }

            /// <summary>
            /// URL de servicio de verificacion token
            /// </summary>
            public string URLServicioverificacionToken { get; set; }

            /// <summary>
            /// URL de servicio
            /// </summary>
            public string URLServicio { get; set; }
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
                string strDatos = "IntegracionCorporacionRESTID: " + this.IntegracionCorporacionRESTID.ToString() + " -- " +
                                    "AutoridadID: " + this.AutoridadID.ToString()  + " -- " +
                                    "Autoridad: " + (this.Autoridad ?? "null")  + " -- " +
                                    "Credenciales: " + (this.Credenciales != null ? this.Credenciales.ToString() : "null") + " -- " +
                                    "URLServicioAutorizacion: " + (this.URLServicioAutorizacion ?? "null") + " -- " +
                                    "URLServicioverificacionToken: " + (this.URLServicioverificacionToken ?? "null") + " -- " +
                                    "URLServicio: " + (this.URLServicio ?? "null") + " -- " +
                                    "Activo: " + this.Activo.ToString() + " -- " +
                                    "FechaCreacion: " + this.FechaCreacion.ToString() + " -- " +
                                    "FechaUltimaModificacion: " + this.FechaUltimaModificacion.ToString();
                
                return strDatos;
            }

        #endregion

    }
}
