using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class RadicacionDocumentoIdentity : EntidadSerializable
    {
        /// <summary>
        /// Constructor sin parametros, útil en la serialización XML
        /// </summary>
        public RadicacionDocumentoIdentity() { this.IdRadicacion = 0; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNumeroSilpa"></param>
        /// <param name="strActoAdministrativo"></param>
        /// <param name="lstDocumentoAdjunto"></param>
        /// <param name="lstNombreDocumentoAdjunto"></param>
        /// <param name="strNumeroRadicacionDocumento"></param>
        /// <param name="intIdRadicacion"></param>
        /// <param name="strNombreFormulario"></param>
        public RadicacionDocumentoIdentity
        (
            string strNumeroSilpa, string strActoAdministrativo, List<Byte[]> lstDocumentoAdjunto,
            List<string> lstNombreDocumentoAdjunto, string strNumeroRadicacionDocumento,
            int intIdRadicacion, string strNombreFormulario, int intTipoDocumento
        )
        {
            this._numeroSilpa = strNumeroSilpa;
            this._actoAdministrativo = strActoAdministrativo;
            this._lstBteDocumentoAdjunto = lstDocumentoAdjunto;
            this._lstNombreDocumentoAdjunto = lstNombreDocumentoAdjunto;
            this._numeroRadicacionDocumento = strNumeroRadicacionDocumento;
            this._idRadicacion = intIdRadicacion;
            this._nombreFormulario = strNombreFormulario;
            this._tipoDocumento = intTipoDocumento;
        }


        #region Declaración de variables..

        /// <summary>
        /// Número silpa del trámite
        /// </summary>
        private string _numeroSilpa;

        /// <summary>
        /// Numero del acto administrativo relacionado
        /// </summary>
        private string _actoAdministrativo;

        /// <summary>
        /// Bytes del documento adjunto (cuando se adjuna un solo documento)
        /// </summary>
        private Byte[] _documentoAdjunto;

        /// <summary>
        /// Nombre del documento adjunto (cuando se adjuna un solo documento)
        /// </summary>
        private string _nombreDocumentoAdjunto;

        /// <summary>
        /// Número de radicación del documento físico
        /// </summary>

        private string _numeroRadicacionDocumento;

        private string _nombreCorporacion;


        /// <summary>
        /// Número VITAL Completo, es decir, el número generado por el sistema para el trámite (no el número de processinstance)
        /// </summary>
        private string _numeroVITALCompleto;




        /// <summary>
        /// Identificador de la radicación en silpa ( tabla GEN_RADICACON)
        /// </summary>
        private int _idRadicacion;

        /// <summary>
        /// Nombre del formulario asociado al momento de la radicaición
        /// </summary>
        private string _nombreFormulario;

        /// <summary>
        /// Fecha de la solicitud
        /// </summary>
        private DateTime _fechaSolicitud;

        /// <summary>
        /// Fecha de la radicación
        /// </summary>
        private DateTime _fechaRadicacion;

        /// <summary>
        /// Listado de nombres de archivos adjuntos en la radicación
        /// </summary>
        private List<string> _lstNombreDocumentoAdjunto;

        /// <summary>
        /// Listado de bytes por archivo adjunto
        /// </summary>
        private List<Byte[]> _lstBteDocumentoAdjunto;

        /// <summary>
        /// Tipo de documento radicado
        /// </summary>
        private int _tipoDocumento;


        /// <summary>
        /// Identificador usuario del solicitante
        /// </summary>
        private string _idSolicitante;

        /// <summary>
        /// Identifica la manera en la que se hará la radicación.
        /// </summary>
        public TipoRadicacion _tipoRadicacion;

        /// <summary>
        /// Formulario parametrizado
        /// </summary>
        private int _numeroFormulario;

        /// <summary>
        /// Contiene la descripcion de la solicitud
        /// </summary>
        private string _descRadicacion;

        /// <summary>
        /// Determina si el registro ya fue leído por una autoridad ambiental
        /// </summary>
        private bool _Leido;




        private string _nombreSolicitante;
        private string _correoSolicitante;
        private string _ciudadSolicitante;
        private string _identificacionSolicitante;
        private string _direccionCorporacion;
        private string _ciudadCorporacion;
        private string _numeroRadicadoAA;
        private string _nitCorporacion;
        private string _telefonoCorporacion;
        private string _ubicacionDocumento;
        private int _Id;
        private string _Solicitante;
        private string _radicadoDocumento;
        private int? _IdAA;

        #endregion


        #region declaración de propiedades ...
        public bool Leido
        {
            get { return _Leido; }
            set { _Leido = value; }
        }
        public string NumeroVITALCompleto
        {
            get { return _numeroVITALCompleto; }
            set { _numeroVITALCompleto = value; }
        }
        public string NumeroSilpa { get { return _numeroSilpa; } set { _numeroSilpa = value; } }
        public string NumeroRadicacionDocumento { get { return _numeroRadicacionDocumento; } set { _numeroRadicacionDocumento = value; } }
        public string ActoAdministrativo { get { return _actoAdministrativo; } set { _actoAdministrativo = value; } }
        public int NumeroFormulario { get { return _numeroFormulario; } set { _numeroFormulario = value; } }

        public string NombreDocumentoAdjunto { get { return _nombreDocumentoAdjunto; } set { _nombreDocumentoAdjunto = value; } }
        public int IdRadicacion { get { return _idRadicacion; } set { _idRadicacion = value; } }
        public int? IdAA { get { return _IdAA; } set { _IdAA = value; } }
        public DateTime FechaSolicitud { get { return _fechaSolicitud; } set { _fechaSolicitud = value; } }
        public DateTime FechaRadicacion { get { return _fechaRadicacion; } set { _fechaRadicacion = value; } }

        public string NombreFormulario { get { return this._nombreFormulario; } set { this._nombreFormulario = value; } }
        public Byte[] DocumentoAdjunto { get { return this._documentoAdjunto; } set { this._documentoAdjunto = value; } }
        public string DescRadicacion { get { return this._descRadicacion; } set { this._descRadicacion = value; } }



        public string RadicadoDocumento
        {
            get { return _radicadoDocumento; }
            set { _radicadoDocumento = value; }
        }
        public string Solicitante
        {
            get { return _Solicitante; }
            set { _Solicitante = value; }
        }

        public string IdSolicitante
        {
            get { return this._idSolicitante; }
            set { this._idSolicitante = value; }
        }

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string UbicacionDocumento
        {
            get { return _ubicacionDocumento; }
            set { _ubicacionDocumento = value; }
        }
        public string NitCorporacion
        {
            get { return _nitCorporacion; }
            set { _nitCorporacion = value; }
        }
        public string TelefonoCorporacion
        {
            get { return _telefonoCorporacion; }
            set { _telefonoCorporacion = value; }
        }
        public string NumeroRadicadoAA
        {
            get { return _numeroRadicadoAA; }
            set { _numeroRadicadoAA = value; }
        }
        public string CiudadCorporacion
        {
            get { return _ciudadCorporacion; }
            set { _ciudadCorporacion = value; }
        }
        public string DireccionCorporacion
        {
            get { return _direccionCorporacion; }
            set { _direccionCorporacion = value; }
        }
        public string IdentificacionSolicitante
        {
            get { return _identificacionSolicitante; }
            set { _identificacionSolicitante = value; }
        }

        public List<Byte[]> LstBteDocumentoAdjunto
        {
            get { return this._lstBteDocumentoAdjunto; }
            set { this._lstBteDocumentoAdjunto = value; }
        }

        public List<string> LstNombreDocumentoAdjunto
        {
            get { return _lstNombreDocumentoAdjunto; }
            set { this._lstNombreDocumentoAdjunto = value; }
        }

        public int TipoDocumento
        {
            get { return _tipoDocumento; }
            set { this._tipoDocumento = value; }
        }

        
        /// <summary>
        /// Identificador de la autoridad -Ambiental como una autoridad Externa
        /// campo necesario para el uso de la comunicación EE
        /// </summary>
        private int _id_EE;
        public int ID_EE
        {
            get { return _id_EE; }
            set { this._id_EE= value; }
        }

        #endregion

    }
}
