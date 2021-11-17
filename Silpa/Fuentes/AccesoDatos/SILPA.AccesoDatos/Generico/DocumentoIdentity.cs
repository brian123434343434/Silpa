using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.AccesoDatos.Generico
{
    
    /// <summary>
    /// Documento que se emite
    /// </summary>
    public class DocumentoIdentity : EntidadSerializable
    {
        
        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public DocumentoIdentity(){}


        /// <summary>
        /// Constructor con parametros...
        /// </summary>
        /// <param name="strNumeroSilpa"></param>
        /// <param name="strNumeroExpediente"></param>
        /// <param name="strNumeroDocumento"></param>
        /// <param name="tdcTipoDocumento"></param>
        /// <param name="strObjetoDocumento"></param>
        /// <param name="dteFechaDocumento"></param>
        /// <param name="strTextoDocumento"></param>
        /// <param name="lstArchivosAdjuntos"></param>
        public DocumentoIdentity
        (string strNumeroSilpa,string strNumeroExpediente,string strNumeroDocumento,
         TipoDocumento tdcTipoDocumento,string strObjetoDocumento,DateTime dteFechaDocumento,
         string strTextoDocumento, List<Byte[]> lstArchivosAdjuntos 
        )
        {
                 this._numeroSilpa = strNumeroSilpa;
                 this._numeroExpediente = strNumeroExpediente;
                 this._numeroDocumento= strNumeroDocumento;
                 this._tipoDocumento = tdcTipoDocumento;
                 this._objetoDocumento = strObjetoDocumento;
                 this._fechaDocumento = dteFechaDocumento;
                 this._textoDocumento = strTextoDocumento;
                 this._archivosAdjuntos = lstArchivosAdjuntos;
        }
        
        #region Declaración de campos...


        /// <summary>
        /// numero silpa (número del tramite en silpa)
        /// </summary>
        private string _numeroSilpa;

        /// <summary>
        /// Numero de expediente
        /// </summary>
        private string _numeroExpediente;

        /// <summary>
        /// Numero de documento
        /// </summary>
        private string _numeroDocumento;

        /// <summary>
        /// Tipo de documento / Oficio Acto
        /// </summary>
        private TipoDocumento _tipoDocumento;

        /// <summary>
        /// Objeto del docuemento
        /// </summary>
        private string _objetoDocumento;

        /// <summary>
        /// Fecha del documento
        /// </summary>
        private DateTime _fechaDocumento;

        /// <summary>
        /// Texto del documento
        /// </summary>  
        private string _textoDocumento;
        /// <summary>
        /// Listado de archivos firmados (PZ7 si aplica) Actos,documentos
        /// </summary>
        private List<Byte[]> _archivosAdjuntos;

        /// <summary>
        /// Listado de archivos nombres de Actos,documentos
        /// </summary>
        private List<String> _NombresArchivos; 

        /// <summary>
        /// Datos de formulario de pago - ( Cobro )
        /// </summary>
        //private CobroIdentity _cobro;
        //public CobroIdentity Cobro 
        //{ 
        //    get { return this._cobro; }
        //    set { this._cobro = value; } 
        //}

        /// <summary>
        /// Notificación
        /// </summary>
        private NotificacionEntity _notificacion;
        public NotificacionEntity Notificacion 
        { 
            get { return this._notificacion; } 
            set { this._notificacion = value; } 
        }

        #endregion

        #region Declaracion de propiedades...
        public string NumeroSilpa { get { return this._numeroSilpa; } set { this._numeroSilpa = value; } }
        public string NumeroExpediente { get { return this._numeroExpediente; } set { this._numeroExpediente = value; } }
        public string NumeroDocumento { get { return this._numeroDocumento; } set { this._numeroDocumento = value; } }
        public TipoDocumento TipoDocumento { get { return this._tipoDocumento; } set { this._tipoDocumento = value; } }
        public string ObjetoDocumento { get { return this._objetoDocumento; } set { this._objetoDocumento = value; } }
        public DateTime FechaDocumento { get { return this._fechaDocumento; } set { this._fechaDocumento = value; } }
        public string TextoDocumento { get { return this._textoDocumento; } set { this._textoDocumento = value; } }
        public List<Byte[]> ArchivosAdjuntos { get { return this._archivosAdjuntos; } set { this._archivosAdjuntos = value; } }
        public List<String> NombresArchivos { get { return this._NombresArchivos; } set { this._NombresArchivos = value; } }
        #endregion


    }
}
