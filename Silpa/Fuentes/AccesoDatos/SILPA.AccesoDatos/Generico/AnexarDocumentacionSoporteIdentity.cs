using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    
    public class AnexarDocumentacionSoporteIdentity:EntidadSerializable
    {
        /// <summary>
        /// Constructior
        /// </summary>
        public AnexarDocumentacionSoporteIdentity() { }

        #region Atributos...

        /// <summary>
        /// Listado de bytes de los documentos adjuntos
        /// </summary>
        private List<Byte[]> _bytesDocumentosAdjuntos;
        public List<Byte[]> BytesDocumentosAdjuntos 
        {
            get { return this._bytesDocumentosAdjuntos; }
            set { this._bytesDocumentosAdjuntos = value; } 
        }

        /// <summary>
        /// Listado de los nombres de los documentos adjuntos
        /// </summary>
        private List<String> _documentosAdjuntos;
        public List<String> DocumentosAdjuntos
        {
            get { return this._documentosAdjuntos; }
            set { this._documentosAdjuntos = value; }
        }


        /// <summary>
        /// Listado de la información de cada una de los documentos adjuntos
        /// </summary>
        private List<String> _infoAdicionalDocumento;
        public List<String> InfoAdicionalDocumento
        {
            get { return this._infoAdicionalDocumento; }
            set { this._infoAdicionalDocumento = value; }
        }
        
        /// <summary>
        /// Número de Radicación
        /// </summary>
        private string _numeroRadicacion;
        public string NumeroRadicacion 
        { 
            get{ return this._numeroRadicacion; }
            set { this._numeroRadicacion = value; } 
        }

        private DateTime _fechaRadicacion;
        public DateTime FechaRadicacion 
        {
            get { return this._fechaRadicacion; }
            set { this._fechaRadicacion = value; }
        }

        private int _tiempoVidaFormulario;
        public int TiempoVidaFormulario
        {
            get { return this._tiempoVidaFormulario; }
            set { this._tiempoVidaFormulario = value; }
        }


    #endregion

    }
}
