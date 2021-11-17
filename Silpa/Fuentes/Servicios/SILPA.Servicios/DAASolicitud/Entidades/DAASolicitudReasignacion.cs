using SILPA.AccesoDatos.DAA;
using SILPA.Comun;
using SILPA.Servicios.DAASolicitud.Enum;

namespace SILPA.Servicios.DAASolicitud.Entidades
{
    public class DAASolicitudReasignacion : DAASolicitudRespuesta
    {

        #region Creadoras

            /// <summary>
            /// Creadora
            /// </summary>
            public DAASolicitudReasignacion()
            {
            }

            /// <summary>
            /// Creadora
            /// </summary>
            /// <param name="p_objCodigoRespuestaEnum">CodigoRespuestaEnum con el codigo de respuesta</param>
            /// <param name="p_strMensaje">string con el mensaje</param>
            /// <param name="p_objDAASolicitudReasignacionEntity">DAASolicitudReasignacionEntity con la informacion de la solicitud de reasignacion</param>
            public DAASolicitudReasignacion(CodigoRespuestaEnum p_objCodigoRespuestaEnum, string p_strMensaje = "", DAASolicitudReasignacionEntity p_objDAASolicitudReasignacionEntity = null)
                : base(p_objCodigoRespuestaEnum, p_strMensaje)
            {
                this.SolicitudReasignacion = p_objDAASolicitudReasignacionEntity;
            }

        #endregion


        #region Propiedades

            /// <summary>
            /// Solicitud de tramite
            /// </summary>
            public DAASolicitudReasignacionEntity SolicitudReasignacion { get; set; }

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
