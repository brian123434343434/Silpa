using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.AudienciaPublica
{
  public  class SolicitudAudienciaIdentity
    {
  /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public SolicitudAudienciaIdentity() { }

        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// </summary>
        /// <param name="intCodigoTipoPersona">int: identificador del Solicitud Audiencia Pública</param>
        /// <param name="strNombreTipoPersona">string: nombre del Solicitud Audiencia Pública</param>
        /// <param name="strExtensionSolicitudAudiencia">string: extension del nombre del Solicitud Audiencia Pública</param>
      public SolicitudAudienciaIdentity
        (
            int intCodigoSolicitudAudiencia, 
            string strNombreSolicitudAudiencia,
           string strExtensionSolicitudAudiencia
        )
        {
            this._idSolicitudAudiencia = intCodigoSolicitudAudiencia;
            this._nombreProyecto = strNombreSolicitudAudiencia;
            this._titularProyecto = strExtensionSolicitudAudiencia;
        }


        #region Declaracion de campos...
        
        /// <summary>
        /// identificador del Solicitud Audiencia público
        /// </summary>
        private long _idSolicitudAudiencia;

        /// <summary>
        /// Nombre del proyecto de 
        /// </summary>
        private string _nombreProyecto;

        /// <summary>
        /// titular del proyecto 
        /// </summary>
        private string _titularProyecto;

        /// <summary>
        ///numero silpa  
        /// </summary>
        private string _numeroSilpa;
        /// <summary>
        ///numero silpa del proyecto 
        /// </summary>
        private string _numeroSilpaProyecto;
        /// <summary>
        /// numero de expediente del proyecto
        /// </summary>
        private string _numeroExpediente;
        /// <summary>
        /// identificador de la autoridad competente
        /// </summary>
        private int _idAutoridad;
        /// <summary>
        /// identificador del tipo de proceso de persona
        /// </summary>
        //private int _idTipoPersona;
        /// <summary>
        /// identificador del persona solicitante de la audiencia
        /// </summary>
        private int _idTipoFuncionario;
        /// <summary>
        /// identificador del tipo de solicitante cuando es funcionario publico
        /// </summary>
      private long _idPersona;
        /// <summary>
        /// ruta del documento adjunto
        /// </summary>
        //private string _rutaDocumento;
        /// <summary>
        /// motivo de solicitud
        /// </summary>
        private string _motivoSolicitud;
        /// <summary>
        /// identificador de la audicencia cuando es generada
        /// </summary>
      private long _idAudiencia;
        #endregion

        #region Declaracion de las propiedades ... 
      public long IdSolicitudAudiencia
        {
            get { return this._idSolicitudAudiencia; }
            set { this._idSolicitudAudiencia = value; }
        }

        public string NombreProyecto
        {
            get { return this._nombreProyecto; }
            set { this._nombreProyecto = value; }
        }
        public string TitularProyecto
        {
          get { return this._titularProyecto; }
          set { this._titularProyecto = value; }
         }
      public string NumeroSilpa
      {
          get { return this._numeroSilpa; }
          set { this._numeroSilpa = value; }
      }
      public string NumeroSilpaProyecto
      {
          get { return this._numeroSilpaProyecto; }
          set { this._numeroSilpaProyecto = value; }
      }
      public string NumeroExpediente
      {
          get { return this._numeroExpediente; }
          set { this._numeroExpediente = value; }
      }
      public int IdAutoridad
      {
          get { return this._idAutoridad; }
          set { this._idAutoridad = value; }
      }
      //public int IdTipoPersona
      //{
      //    get { return this._idTipoPersona; }
      //    set { this._idTipoPersona = value; }
      //}
      public long IdPersona
      {
          get { return this._idPersona; }
          set { this._idPersona = value; }
      }
      public int IdTipoFuncionario
      {
          get { return this._idTipoFuncionario; }
          set { this._idTipoFuncionario = value; }
      }
      //public string RutaDocumento
      //{
      //    get { return this._rutaDocumento; }
      //    set { this._rutaDocumento = value; }
      //}
      public string MotivoSolicitud
      {
          get { return this._motivoSolicitud; }
          set { this._motivoSolicitud = value; }
      }
      public long IdAudiencia
      {
          get { return this._idAudiencia; }
          set { this._idAudiencia = value; }
      }
        #endregion

    }
}


