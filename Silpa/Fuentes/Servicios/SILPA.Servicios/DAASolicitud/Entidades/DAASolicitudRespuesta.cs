using SILPA.Comun;
using SILPA.Servicios.DAASolicitud.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.Servicios.DAASolicitud.Entidades
{
    public class DAASolicitudRespuesta : EntidadSerializable
    {
        #region Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public DAASolicitudRespuesta()
            {
            }

            /// <summary>
            /// Creadora
            /// </summary>
            /// <param name="p_objCodigoRespuestaEnum">CodigoRespuestaEnum con el codigo de respuesta</param>
            /// <param name="p_strMensaje">string con el mensaje</param>
            public DAASolicitudRespuesta(CodigoRespuestaEnum p_objCodigoRespuestaEnum, string p_strMensaje = "")
            {
                this.CodigoRespuesta = (int)p_objCodigoRespuestaEnum;
                this.MensajeError = p_strMensaje;
            }

        #endregion


        #region Propiedades

            /// <summary>
            /// Codigo de respuesta al proceso
            /// </summary>
            public int CodigoRespuesta { get; set; }

            /// <summary>
            /// Mensaje de error en caso de que se presente
            /// </summary>
            public string MensajeError { get; set; }

        #endregion


        #region Metodos

            /// <summary>
            /// Retorna en un string el contenido del objeto
            /// </summary>
            /// <returns>string con el contenido del objeto</returns>
            public override string ToString()
            {
                return this.GetXml();
            }


        #endregion
    }
}
