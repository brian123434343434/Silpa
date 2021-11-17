using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Publicacion
{
    public class PublicacionIdentity : EntidadSerializable
    {
        
        public PublicacionIdentity()
        {
        }

        /// <summary>
        /// Identificador de la publicacions
        /// </summary>
        private long? _identificadorPublicacion;

        /// <summary>
        /// Almacena la ruta del documento de la publicacion
        /// </summary>
        private string _rutaPublicacion;

        /// <summary>
        /// Almacena el titulo de la publicacion
        /// </summary>
        private string _tituloPublicacion;

        /// <summary>
        /// Almacena el tipo de vigencia de la publicacion
        /// </summary>
        //private int _vigenciaPublicacion;

        /// <summary>
        /// Identificador del tipo de tramite de la publicacion
        /// </summary>
        private int _idTramite;

        /// <summary>
        /// Identificador de la Autoridad Ambiental
        /// </summary>
        private int _idAutoridad;

        /// <summary>
        /// Identificador del sector
        /// </summary>
        private int _idSector;

        /// <summary>
        /// Almacena el nombre del expediente
        /// </summary>
        private string _nombreExpediente;

        /// <summary>
        /// 31-ago-2010
        /// Codigo del Expediente
        /// </summary>
        private string _codigoExpediente;

        
        /// <summary>
        /// Identificador del Tipo de Acto Administrativo
        /// </summary>
        private int _idTipoActoAdm;

        /// <summary>
        /// Identificador del Acto Administrativo
        /// </summary>
        private int _idActoAdm;

        /// <summary>
        /// Almacena el numero del documento
        /// </summary>
        private string _numeroDocumento;

        /// <summary>
        /// Identificador del Expediente
        /// </summary>
        private string _idExpediente;

        /// <summary>
        /// Almacena la fecha de fijacion o de Publicacion
        /// </summary>
        //private string _fechaFijacion;

        /// <summary>
        /// Almacena la fecha de desfijacion
        /// </summary>
        private string _fechaDesfijacion;
        
        /// <summary>
        /// Identificador del tipo de publicacion
        /// </summary>
        private int _idTipoPublicacion;

        /// <summary>
        /// Almacena la fecha de expedicion del documento
        /// </summary>
        //private string _fechaExpedicion;

        /// <summary>
        /// Almacena la descripcion de la publicacion
        /// </summary>
        private string _descripcionPublicacion;

        /// <summary>
        /// Almacena el codigo de la publicacion en SILA
        /// </summary>
        //private Int64 _idPublicacionAA;

        /// <summary>
        /// Identificador de la Publicacion en SILPA
        /// </summary>
        //private Int64 _idPublicacion;

        /// <summary>
        /// Almacena si se espera estado de notificacion
        /// </summary>
        private string _notificacion;

        /// <summary>
        /// contiene el listado de los documentos adjuntos.
        /// </summary>
        private ListaDocumentoAdjuntoType _lstDocumentosAdjuntos;
        public ListaDocumentoAdjuntoType ListaDocumentoAdjuntoType
        {
            get 
            { 
                return _lstDocumentosAdjuntos; 
            }
            set { _lstDocumentosAdjuntos = value; }
        }

        #region Propiedades

        /// <summary>
        /// Identificador de la publicacion
        /// </summary>
        public long? PublicacionID
        {
            get { return _identificadorPublicacion; }
            set { _identificadorPublicacion = value; }
        }


        public string RutaPublicacion
        {
            get { return _rutaPublicacion; }
            set { _rutaPublicacion = value; }
        }

        public string TitutloPublicacion
        {
            get { return _tituloPublicacion; }
            set { _tituloPublicacion = value; }
        }

        //public int VigenciaPublicacion
        //{
        //    get { return _vigenciaPublicacion; }
        //    set { _vigenciaPublicacion = value; }
        //}

        public int IdTramite
        {
            get { return _idTramite; }
            set { _idTramite = value; }
        }

        public int IdAutoridad
        {
            get { return _idAutoridad; }
            set { _idAutoridad = value; }
        }

        public int IdSector
        {
            get { return _idSector; }
            set { _idSector = value; }
        }

        public string NombreExpediente
        {
            get { return _nombreExpediente; }
            set { _nombreExpediente = value; }
        }

        public int IdTipoActoAdministrativo
        {
            get { return _idTipoActoAdm; }
            set { _idTipoActoAdm = value; }
        }

        public int IdActoAdministrativo
        {
            get { return _idActoAdm; }
            set { _idActoAdm = value; }
        }

        public string NumeroDocumento
        {
            get { return _numeroDocumento; }
            set { _numeroDocumento = value; }
        }

        public string IdExpediente
        {
            get { return _idExpediente; }
            set { _idExpediente = value; }
        }


        //public string FechaFijacion
        //{
        //    get { return _fechaFijacion; }
        //    set { _fechaFijacion = value; }
        //}
        
        public string FechaDesfijacion
        {
            get { return _fechaDesfijacion; }
            set { _fechaDesfijacion = value; }
        }

        public int IdTipoPublicacion
        {
            get { return _idTipoPublicacion; }
            set { _idTipoPublicacion = value; }
        }

        //public string FechaExpedicion
        //{
        //    get { return _fechaExpedicion; }
        //    set { _fechaExpedicion = value; }
        //}

        public string DescripcionPublicacion
        {
            get { return _descripcionPublicacion; }
            set { _descripcionPublicacion = value; }
        }

        //public Int64 IdPublicacionAA
        //{
        //    get { return _idPublicacionAA; }
        //    set { _idPublicacionAA = value; }
        //}

        //public Int64 IdPublicacion
        //{
        //    get { return _idPublicacion; }
        //    set { _idPublicacion = value; }
        //}

        /// <summary>
        /// Identificador relacionado generado al momento de consumir el servicio de notificación
        /// </summary>
        private Int64 _idRelacionaPublicacion;
        public Int64 IdRelacionaPublicacion
        {
            get { return _idRelacionaPublicacion; }
            set { _idRelacionaPublicacion = value; }
        }

        public string Notificacion
        {
            get { return _notificacion; }
            set { _notificacion = value; }
        }

        //31-ago-2010
        public string CodigoExpediente
        {
            get { return _codigoExpediente; }
            set { _codigoExpediente = value; }
        }

        #endregion
    }
}
