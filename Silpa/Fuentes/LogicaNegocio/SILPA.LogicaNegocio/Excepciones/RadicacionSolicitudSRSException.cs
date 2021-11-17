using SILPA.AccesoDatos.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Excepciones
{
    public class RadicacionSolicitudSRSException : SolicitudSRSException
    {
         /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        public RadicacionSolicitudSRSException() : base() { }

        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">string que contiene el mensaje de error presentado</param>
        public RadicacionSolicitudSRSException(string message) : base(message) { }

        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">string que contiene el mensaje de error presentado</param>
        /// <param name="inner">Exception con la información de la excepción presentada</param>
        public RadicacionSolicitudSRSException(string message, System.Exception inner) : base(message, inner) { }


        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">PDVDalcException con la información obtenida</param>
        public RadicacionSolicitudSRSException(PDVDalcException p_objException) : base(p_objException.Message, p_objException.InnerException) { }
    }
}
