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
        /// M�todo que permite anexar la informaci�n 
        /// </summary>
        public bool SolicitarDocumentacion() 
        {
            RadicacionDocumentoFachada objRadFachada= new RadicacionDocumentoFachada();
            return true;
        }    
    }
}
