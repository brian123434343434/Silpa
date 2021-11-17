using SILPA.AccesoDatos.DAA;
using SILPA.Servicios.DAASolicitud.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.Servicios.DAASolicitud.Entidades
{
    public class DAASolicitudEstadosReasignacion : DAASolicitudRespuesta
    {

        #region Creadoras

            /// <summary>
            /// Creadora
            /// </summary>
            public DAASolicitudEstadosReasignacion()
            {
            }

            /// <summary>
            /// Creadora
            /// </summary>
            /// <param name="p_objCodigoRespuestaEnum">CodigoRespuestaEnum con el codigo de respuesta</param>
            /// <param name="p_strMensaje">string con el mensaje</param>
            /// <param name="p_objEstados">List con la informacion de los estados</param>
            public DAASolicitudEstadosReasignacion(CodigoRespuestaEnum p_objCodigoRespuestaEnum, string p_strMensaje = "", List<DAASolicitudEstadoReasignacionEntity> p_objEstados = null)
                : base(p_objCodigoRespuestaEnum, p_strMensaje)
            {
                this.Estados = p_objEstados;
            }

        #endregion


        #region Propiedades

            /// <summary>
            /// Listado de estados de reasignacion
            /// </summary>
            public List<DAASolicitudEstadoReasignacionEntity> Estados { get; set; }

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
