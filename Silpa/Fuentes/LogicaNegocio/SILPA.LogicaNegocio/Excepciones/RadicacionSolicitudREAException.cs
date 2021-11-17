using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Excepciones;

namespace SILPA.LogicaNegocio.Excepciones
{
    public class RadicacionSolicitudREAException : SolicitudREAException
    {
        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        public RadicacionSolicitudREAException() : base() { }

        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">string que contiene el mensaje de error presentado</param>
        public RadicacionSolicitudREAException(string message) : base(message) { }

        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">string que contiene el mensaje de error presentado</param>
        /// <param name="inner">Exception con la información de la excepción presentada</param>
        public RadicacionSolicitudREAException(string message, System.Exception inner) : base(message, inner) { }
    }
}
