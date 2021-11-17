using SILPA.Servicios.DAASolicitud.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.Servicios.DAASolicitud.Entidades
{
    public class DAASolicitudAutoridades : DAASolicitudRespuesta
    {

        #region Creadoras

        /// <summary>
        /// Creadora
        /// </summary>
        public DAASolicitudAutoridades()
        {
        }

        /// <summary>
        /// Creadora
        /// </summary>
        /// <param name="p_objCodigoRespuestaEnum">CodigoRespuestaEnum con el codigo de respuesta</param>
        /// <param name="p_strMensaje">string con el mensaje</param>
        /// <param name="p_objAutoridades">List con la informacion de las autoridades ambientales</param>
        public DAASolicitudAutoridades(CodigoRespuestaEnum p_objCodigoRespuestaEnum, string p_strMensaje = "", List<DAASolicitudAutoridad> p_objAutoridades = null)
            : base(p_objCodigoRespuestaEnum, p_strMensaje)
        {
            this.Autoridades = p_objAutoridades;
        }

        #endregion


        #region Propiedades

            /// <summary>
            /// Listado de autoridades ambientales
            /// </summary>
            public List<DAASolicitudAutoridad> Autoridades { get; set; }

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