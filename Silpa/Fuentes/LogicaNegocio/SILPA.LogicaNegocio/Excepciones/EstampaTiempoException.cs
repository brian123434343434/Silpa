using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Excepciones
{
    public class EstampaTiempoException : Exception
    {
        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        public EstampaTiempoException() : base() { }

        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">string que contiene el mensaje de error presentado</param>
        public EstampaTiempoException(string message) : base(message) { }

        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">string que contiene el mensaje de error presentado</param>
        /// <param name="inner">Exception con la información de la excepción presentada</param>
        public EstampaTiempoException(string message, System.Exception inner) : base(message, inner) { }

    }

}
