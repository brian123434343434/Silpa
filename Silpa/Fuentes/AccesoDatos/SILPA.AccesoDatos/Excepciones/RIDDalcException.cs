using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Excepciones
{
    public class RIDDalcException : Exception
    {
        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        public RIDDalcException() : base() { }

        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">string que contiene el mensaje de error presentado</param>
        public RIDDalcException(string message) : base(message) { }

        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">string que contiene el mensaje de error presentado</param>
        /// <param name="inner">Exception con la información de la excepción presentada</param>
        public RIDDalcException(string message, System.Exception inner) : base(message, inner) { }
    }
}
