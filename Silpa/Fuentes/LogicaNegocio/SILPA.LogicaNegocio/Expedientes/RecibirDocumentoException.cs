using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Excepciones
{
    public class RecibirDocumentoException : Exception
    {
         /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        public RecibirDocumentoException() : base() { }

        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">string que contiene el mensaje de error presentado</param>
        public RecibirDocumentoException(string message) : base(message) { }

        /// <summary>
        /// Exepcion ocurrida durante el acceso a datos
        /// </summary>
        /// <param name="message">string que contiene el mensaje de error presentado</param>
        /// <param name="inner">Exception con la información de la excepción presentada</param>
        public RecibirDocumentoException(string message, System.Exception inner) : base(message, inner) { }
    }
}
