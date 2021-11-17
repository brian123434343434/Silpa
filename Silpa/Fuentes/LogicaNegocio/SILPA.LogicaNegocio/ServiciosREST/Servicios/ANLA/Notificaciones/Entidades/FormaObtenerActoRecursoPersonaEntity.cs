using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.ServiciosREST.Servicios.ANLA.Notificaciones.Entidades
{
    public class FormaObtenerActoRecursoPersonaEntity
    {
        #region Propiedades

            /// <summary>
            /// Id del acto de notificacion
            /// </summary>
            public int ActoNotificacionID { get; set; }
            /// <summary>
            /// Id de la persona
            /// </summary>
            public int PersonaID { get; set; }

        #endregion

        #region Metodo publico

            /// <summary>
            /// Retornar en un string la información del objeto
            /// </summary>
            /// <returns>string con la información contenida en el objeto</returns>
            public override string ToString()
            {
                string strDatos = "ActoNotificacionID: " + this.ActoNotificacionID.ToString() + " -- " +
                                    "PersonaID: " + this.PersonaID.ToString();

                return strDatos;
            }

        #endregion
    }
}
