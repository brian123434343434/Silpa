using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.AudienciaPublica
{
    public class SolicitudAudienciaType : EntidadSerializable
    {
        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public SolicitudAudienciaType() { }

        #region Declaracion de campos...

        /// <summary>
        /// identificador del Solicitud Audiencia público
        /// </summary>
        private long idSolicitudAudiencia;

        /// <summary>
        /// Nombre del proyecto de 
        /// </summary>
        private string nombreProyecto;

        /// <summary>
        /// titular del proyecto 
        /// </summary>
        private string titularProyecto;

        /// <summary>
        ///numero silpa del proyecto 
        /// </summary>
        private string numeroSilpa;
        /// <summary>
        /// numero de expediente del proyecto
        /// </summary>
        private string numeroExpediente;
        /// <summary>
        /// identificador de la autoridad competente
        /// </summary>
        private int idAutoridad;
        /// <summary>
        /// identificador del tipo de proceso de persona
        /// </summary>
        private int idTipoPersona;
        /// <summary>
        /// identificador del persona solicitante de la audiencia
        /// </summary>
        private int idTipoFuncionario;
        /// <summary>
        /// identificador del tipo de solicitante cuando es funcionario publico
        /// </summary>
        private long idPersona;
        /// <summary>
        /// ruta del documento adjunto
        /// </summary>
        private string rutaDocumento;
        /// <summary>
        /// motivo de solicitud
        /// </summary>
        private string motivoSolicitud;
        /// <summary>
        /// identificador de la audicencia cuando es generada
        /// </summary>
        private long idAudiencia;


        /// <summary>
        /// contiene el listado de los documentos adjuntos.
        /// </summary>
        private ListaDocumentoAdjuntoType lstDocumentosAdjuntos;

        #endregion

        #region Declaracion de las propiedades ...
        public long IdSolicitudAudiencia
        {
            get { return this.idSolicitudAudiencia; }
            set { this.idSolicitudAudiencia = value; }
        }

        public string NombreProyecto
        {
            get { return this.nombreProyecto; }
            set { this.nombreProyecto = value; }
        }
        public string TitularProyecto
        {
            get { return this.titularProyecto; }
            set { this.titularProyecto = value; }
        }
        public string NumeroSilpa
        {
            get { return this.numeroSilpa; }
            set { this.numeroSilpa = value; }
        }
        public string NumeroExpediente
        {
            get { return this.numeroExpediente; }
            set { this.numeroExpediente = value; }
        }
        public int IdAutoridad
        {
            get { return this.idAutoridad; }
            set { this.idAutoridad = value; }
        }
        public int IdTipoPersona
        {
            get { return this.idTipoPersona; }
            set { this.idTipoPersona = value; }
        }
        public long IdPersona
        {
            get { return this.idPersona; }
            set { this.idPersona = value; }
        }
        public int IdTipoFuncionario
        {
            get { return this.idTipoFuncionario; }
            set { this.idTipoFuncionario = value; }
        }
        public string RutaDocumento
        {
            get { return this.rutaDocumento; }
            set { this.rutaDocumento = value; }
        }
        public string MotivoSolicitud
        {
            get { return this.motivoSolicitud; }
            set { this.motivoSolicitud = value; }
        }
        public long IdAudiencia
        {
            get { return this.idAudiencia; }
            set { this.idAudiencia = value; }
        }
        public ListaDocumentoAdjuntoType ListaDocumentoAdjuntoType
        {
            get { return lstDocumentosAdjuntos; }
            set { lstDocumentosAdjuntos = value; }
        }
        #endregion

    }
}


