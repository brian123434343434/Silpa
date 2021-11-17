using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Excepciones;

namespace SILPA.LogicaNegocio.Excepciones
{
    public class DocumentoSolicitudContingenciaException : SolicitudContingenciaException
    {
        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        public DocumentoSolicitudContingenciaException() : base() { }

        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">string que contiene el mensaje de error presentado</param>
        public DocumentoSolicitudContingenciaException(string message) : base(message) { }

        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">string que contiene el mensaje de error presentado</param>
        /// <param name="inner">Exception con la información de la excepción presentada</param>
        public DocumentoSolicitudContingenciaException(string message, System.Exception inner) : base(message, inner) { }


        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">PDVDalcException con la información obtenida</param>
        public DocumentoSolicitudContingenciaException(PDVDalcException p_objException) : base(p_objException.Message, p_objException.InnerException) { }
    }
}
