using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Publicacion
{
    
    public class DetalleDocumentoIdentity
    {
        
        /// <summary>
        /// nombre del archivo
        /// </summary>
        private string _nombreArchivo;

        /// <summary>
        /// ubicacion del archivo
        /// </summary>
        private string _ubicacion;

        public string NombreArchivo { get { return this._nombreArchivo; } set { this._nombreArchivo = value; } }
        public string Ubicacion { get { return this._ubicacion; } set { this._ubicacion = value; } }

        public DetalleDocumentoIdentity() 
        { 
        }
    }
}
