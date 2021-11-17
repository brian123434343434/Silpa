using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.Servicios.Generico.RadicarDocumento
{

    /// <summary>
    /// 
    /// </summary>
    public class AnexarDocumentacionFachada
    {

        public AnexarDocumentacionFachada() { }

        /// <summary>
        /// Método que permite anexar la información 
        /// </summary>
        public bool SolicitarDocumentacion() 
        {
            RadicacionDocumentoFachada objRadFachada= new RadicacionDocumentoFachada();
            return true;
        }    
    }
}
