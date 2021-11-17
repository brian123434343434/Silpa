using SILPA.AccesoDatos.DAA;
using SILPA.Comun;
using SILPA.Servicios.DAASolicitud.Enum;

namespace SILPA.Servicios.DAASolicitud.Entidades
{
    public class DAASolicitudTramite : DAASolicitudRespuesta
    {

        #region Creadoras

            /// <summary>
            /// Creadora
            /// </summary>
            public DAASolicitudTramite()
            {
            }

            /// <summary>
            /// Creadora
            /// </summary>
            /// <param name="p_objCodigoRespuestaEnum">CodigoRespuestaEnum con el codigo de respuesta</param>
            /// <param name="p_strMensaje">string con el mensaje</param>
            /// <param name="p_objDAASolicitudEntity">DAASolicitudEntity con la informacion de la solicitud</param>
            public DAASolicitudTramite(CodigoRespuestaEnum p_objCodigoRespuestaEnum, string p_strMensaje = "", DAASolicitudEntity p_objDAASolicitudEntity = null)
                : base(p_objCodigoRespuestaEnum, p_strMensaje)
            {
                this.SolicitudTramite = p_objDAASolicitudEntity;
            }

        #endregion


        #region Propiedades

            /// <summary>
            /// Solicitud de tramite
            /// </summary>
            public DAASolicitudEntity SolicitudTramite { get; set; }

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
