using SILPA.AccesoDatos.DAA;
using SILPA.Comun;
using SILPA.Servicios.DAASolicitud.Enum;
using System.Collections.Generic;

namespace SILPA.Servicios.DAASolicitud.Entidades
{
    public class DAASolicitudesReasignacion : DAASolicitudRespuesta
    {

        #region Creadoras

        /// <summary>
        /// Creadora
        /// </summary>
        public DAASolicitudesReasignacion()
        {
        }

        /// <summary>
        /// Creadora
        /// </summary>
        /// <param name="p_objCodigoRespuestaEnum">CodigoRespuestaEnum con el codigo de respuesta</param>
        /// <param name="p_strMensaje">string con el mensaje</param>
        /// <param name="p_objLstSolicitudesReasignacion">List con la informacion de las solicitudes. Opcional</param>
        public DAASolicitudesReasignacion(CodigoRespuestaEnum p_objCodigoRespuestaEnum, string p_strMensaje = "", List<DAASolicitudReasignacionEntity> p_objLstSolicitudesReasignacion = null)
            : base(p_objCodigoRespuestaEnum, p_strMensaje)
        {
            this.SolicitudesReasignacion = p_objLstSolicitudesReasignacion;
        }

        #endregion


        #region Propiedades

            /// <summary>
            /// Solicitud de tramite
            /// </summary>
            public List<DAASolicitudReasignacionEntity> SolicitudesReasignacion { get; set; }

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
