using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.RecursoReposicion
{
    /// <summary>
    /// Se encarga de Manejar los Documentos asociados al Recurso de Reposición
    /// </summary>
    public class RecursoDocumentoEntity
    {
        private decimal _idRecursoDocumento;

        private string _rutaDocumento;

        private decimal? _idRadicado;

        private decimal _idRecursoReposicion;

        private string _nombreDocumento;

        private string _tipoDocumento;

        private byte[] _archivo;

        private string _numeroRadicado;
        
        /// <summary>
        /// Número de Radicado del Documento
        /// </summary>
        public string NumeroRadicado
        {
            get { return _numeroRadicado; }
            set { _numeroRadicado = value; }
        }

        /// <summary>
        /// Archivo Adjunto
        /// </summary>
        public byte[] Archivo
        {
            get { return _archivo; }
            set { _archivo = value; }
        }

        /// <summary>
        /// Nombre del Documento
        /// </summary>
        public string NombreDocumento
        {
            get { return _nombreDocumento; }
            set { _nombreDocumento = value; }
        }

        /// <summary>
        /// Tipo del Documento
        /// </summary>
        public string TipoDocumento
        {
            get { return _tipoDocumento; }
            set { _tipoDocumento = value; }
        }

        /// <summary>
        /// Número de Radicado
        /// </summary>
        public decimal? IDRadicado
        {
            get { return _idRadicado; }
            set { _idRadicado = value; }
        }

        /// <summary>
        /// Ruta del Documento
        /// </summary>
        public string RutaDocumento
        {
            get { return _rutaDocumento; }
            set { _rutaDocumento = value; }
        }
        
        /// <summary>
        /// ID del Recurso Documento
        /// </summary>
        public decimal IDRecursoDocumento
        {
            get { return _idRecursoDocumento; }
            set { _idRecursoDocumento = value; }
        }

        public decimal IdRecursoReposicion
        {
            get { return _idRecursoReposicion; }
            set { _idRecursoReposicion = value;}
        }
    }
}
