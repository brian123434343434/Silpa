using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
   public class EmitirDocumentoIdentity
    {
        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public EmitirDocumentoIdentity() {  }
       
        #region Declaración de variables..

        /// <summary>
        /// Identificador de la clase
        /// </summary>

       private long _documentoId;


        /// <summary>
        /// Identificador de la autoridad ambiental
        /// </summary>
        private int _autoridadId;

        /// <summary>
        /// Número silpa del trámite
        /// </summary>
        private string _numeroSilpa;

        /// <summary>
        /// Número del proceso administrativo o numero de expediente
        /// </summary>
        private string _numeroExpediente;

        /// <summary>
        /// Numero del acto administrativo relacionado
        /// </summary>
        private string _actoAdministrativo;

        /// <summary>
        /// Fecha del acto administrativo relacionado
        /// </summary>
        private DateTime _fechaActoAdministrativo;

        /// <summary>
        /// Descripcion del acto administrativo relacionado
        /// </summary>
        private string _descripcionActoAdministrativo;

        /// <summary>
        /// Identificador del funcionario
        /// </summary>
        private long _personaId;

        /// <summary>
        /// Identificador del tipo de documento
        /// </summary>
        private int _tipoDocumentoId;
      
       // /// <summary>
       // /// Identificador de la radicación en silpa ( tabla GEN_RADICACON)
       // /// </summary>
       //private int _radicacionId;
       
        #endregion


        #region declaración de propiedades ...

       public long DocumentoId { get { return _documentoId; } set { _documentoId = value; } }
        public int AutoridadId { get { return _autoridadId; } set { _autoridadId = value; } }
        public string NumeroSilpa { get { return _numeroSilpa; } set { _numeroSilpa = value; } }
        public string NumeroExpediente { get { return _numeroExpediente; } set { _numeroExpediente = value; } }
        public string ActoAdministrativo { get { return _actoAdministrativo; } set { _actoAdministrativo = value; } }
        public DateTime FechaActoAdministrativo { get { return _fechaActoAdministrativo; } set { _fechaActoAdministrativo = value; } }
        public string DescripcionActoAdministrativo { get { return _descripcionActoAdministrativo; } set { _descripcionActoAdministrativo = value; } }
       public long PersonaId { get { return _personaId; } set { _personaId = value; } }
        public int TipoDocumentoId { get { return _tipoDocumentoId; } set { _tipoDocumentoId = value; } }
       //public int RadicacionId { get { return _radicacionId; } set { _radicacionId = value; } }       

        #endregion

    }
}
