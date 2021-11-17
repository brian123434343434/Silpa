using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class PersonaIdentity : EntidadSerializable
    {
        /// <summary>
        /// Constructor sin parametros
        /// </summary>

        public PersonaIdentity()
        {
            this._tipoDocumentoIdentificacion = new TipoIdentificacionEntity();
            this._tipoPersona = new TipoPersonaIdentity();
            this._direccionPersona = new DireccionPersonaIdentity();
            this._listaPersona = new List<PersonaIndentity>();
            this._tipoSolicitante = new TipoPersonaIdentity();
            //this._calidadActua = new TipoPersonaIdentity();
        }
        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="intPersonaId">Int64: identificador de la persona</param>
        /// <param name="strIdUsuario">string: identificador del usuario de la persona</param>
        /// <param name="strPrimerApellido">string: Primer Apellido</param>
        /// <param name="strSegundoApellido">string: Segundo Apellido</param>
        /// <param name="strPrimerNombre">string: Primer Nombre</param>
        /// <param name="strSegundoNombre">string: Segundo Nombre</param>
        /// <param name="strNumeroIdentificaion">string: numero de identificación</param>
        /// <param name="objTipoDocumentoIdentificacion">string: tipo de indentificación del documento</param>
        /// <param name="strLugarExpedicion">string lugar de expedición</param>
        /// <param name="intPais">int: identificador del país</param>
        /// <param name="strTelefono">string: telefono </param>
        /// <param name="strCelular">string: celular</param>
        /// <param name="strFax">string: Fax</param>
        /// <param name="strCorreoElectronico">string: email</param>
        /// <param name="intIdApplicationUser">int: Identificador del usuario en la aplicación</param>
        /// <param name="strRazonSocial">string: Razón social</param>
        /// <param name="objTipoPersona">string: tipo de persona</param>
        /// <param name="intIdAutoridadAmbiental">int: identificador autoridad ambiental</param>        
        /// <param name="strTarjetaProfesional">string: tarjeta profesional</param>
        /// <param name="objDireccionPersonaIdentity">Dirección de la persona</param>
        public PersonaIdentity(
                int intPersonaId,
                string strIdUsuario,
                string strPrimerApellido,
                string strSegundoApellido,
                string strPrimerNombre,
                string strSegundoNombre,
                string strNumeroIdentificaion,
                TipoIdentificacionEntity objTipoDocumentoIdentificacion,
                string strLugarExpedicion,
                int intPais,
                string strTelefono,
                string strCelular,
                string strFax,
                string strCorreoElectronico,
                Int64 intIdApplicationUser,
                string strRazonSocial,
                TipoPersonaIdentity objTipoPersona,
                int intIdAutoridadAmbiental,
            //string strOtro,
                string strTarjetaProfesional,
                Int64 int64IdSolicitante,
                TipoPersonaIdentity objTipoSolicitante,
                bool strAutorizaCorreo, //jmartinez 2-11/2017 adiciono campo autoriza notificacion 
				//JMARTINEZ SALVOCONDUCTO FASE 2
	            string str_TipoIdentificacionIDEAM,
	            string str_TipoPersonaIDEAM

            )
        {
            this._idUsuario = strIdUsuario;
            this._personaId = intPersonaId;
            this._primerApellido = strPrimerApellido;
            this._segundoApellido = strSegundoApellido;
            this._primerNombre = strPrimerNombre;
            this._segundoNombre = strSegundoNombre;
            this._numeroIdentificacion = strNumeroIdentificaion;
            this._tipoDocumentoIdentificacion = objTipoDocumentoIdentificacion;
            this._lugarExpedicionDocumento = strLugarExpedicion;
            this._pais = intPais;
            this._telefono = strTelefono;
            this._celular = strCelular;
            this._fax = strFax;
            this._correoElectronico = strCorreoElectronico;
            this._idApplicationUser = intIdApplicationUser;
            this._razonSocial = strRazonSocial;
            this._tipoPersona = objTipoPersona;
            this.IdAutoridadAmbiental = intIdAutoridadAmbiental;
            //this._otro = strOtro;
            this._tarjetaProfesional = strTarjetaProfesional;
            this._idSolicitante = int64IdSolicitante;
            this._tipoSolicitante = objTipoSolicitante;
            this._autorizaCorreo = strAutorizaCorreo;

            //jmartinez Salvoconducto Fase 2
            this._TipoIdentificacionIDEAM = str_TipoIdentificacionIDEAM;
            this._TipoPersonaIDEAM = str_TipoPersonaIDEAM;

        }

        #region declaracion de campos ...


        private bool _autorizaCorreo;

        private List<PersonaIndentity> _listaPersona;

        /// <summary>
        /// Identificador del usuario en el sistema
        /// UserId: texto
        /// </summary>
        private string _idUsuario;

        /// <summary>
        /// Identificador de la Persona
        /// </summary>
        private Int64 _personaId;
        /// <summary>
        /// Primer Nombre de la Persona
        /// </summary>
        private string _primerNombre;
        /// <summary>
        /// Segundo Nombre de la Persona
        /// </summary>
        private string _segundoNombre;
        /// <summary>
        /// Primer Apellido de la Persona
        /// </summary>
        private string _primerApellido;
        /// <summary>
        /// Segundo Apellido de la Persona
        /// </summary>
        private string _segundoApellido;

        /// <summary>
        /// Numero de indetificación de la persona
        /// </summary>
        private string _numeroIdentificacion;

        /// <summary>
        /// Pregunta para cuando se olvida la contraseña
        /// </summary>
        private string _pregunta;


        /// <summary>
        /// Respuesta a la pregunta para cuando se olvida la contraseña
        /// </summary>
        private string _respuesta;


        /// <summary>
        /// Identificación Nacional de Persona a notificar
        /// </summary>
        private TipoIdentificacionEntity _tipoDocumentoIdentificacion;

        /// <summary>
        /// Identificacion del solicitante si existe relación
        /// </summary>
        private Int64 _idSolicitante;


        /// <summary>
        /// Objeto que contiene la informacion de la direccion de la persona
        /// direccion de contacto.
        /// </summary>
        public DireccionPersonaIdentity _direccionPersona;

        /// <summary>
        /// hava
        /// Lista de direcciones Relacionadas a la persona
        /// </summary>
        private List<DireccionPersonaIdentity> _direcciones;

        private string _rutaRtf;

        // 01-jul-2010 - aegb
        private string _numeroVital;

        /*
         * //-	Persona a Notificar: corresponde al tipo GEL-XML para tipo de persona, que incluye:
//-	Tipo de Persona a Notificar, Código y nombre de tipo de persona - ok
//-	Identificación Nacional de Persona a notificar, que incluye:
//-	Código y nombre de tipo de persona
//-	Grupo Número Identificación Nacional. Que incluye uno de.
//-	NIT (Persona Notificar) / Número NIT / Dígito Verificación NIT o Número de
//-	Identificación


         */


        /// <summary>
        /// Lugar de expedición documento
        /// </summary>
        private string _lugarExpedicionDocumento;

        /// <summary>
        /// País
        /// </summary>
        private int _pais;

        /// <summary>
        /// Nombre Pais
        /// </summary>
        private string _nombrepais;

        /// <summary>
        /// Teléfono de la Persona
        /// </summary>
        private string _telefono;

        /// <summary>
        /// celular de la Persona
        /// </summary>
        private string _celular;

        /// <summary>
        /// Fax de la persona
        /// </summary>
        private string _fax;

        /// <summary>
        /// Correo Electrónico de la Persona
        /// </summary>
        private string _correoElectronico;

        /// <summary>
        /// ID_ApplicationUser
        /// </summary>
        private Int64 _idApplicationUser;

        /// <summary>
        /// Razon Social
        /// </summary>
        private string _razonSocial;

        /// <summary>
        /// Descripcion del usuario
        /// </summary>
        private string _descripcion;

        /// <summary>
        /// Autoridad Ambiental
        /// </summary>
        private int _idAutoridadAmbiental;

        /// <summary>
        /// Tipo Persona
        /// </summary>
        private TipoPersonaIdentity _tipoPersona;

        /// <summary>
        /// Tipo Solcitante
        /// </summary>
        private TipoPersonaIdentity _tipoSolicitante;

        /*
        /// <summary>
        /// Otro dato
        /// </summary>
        private string _otro;*/

        /// <summary>
        /// Tarjeta profesional
        /// </summary>
        private string _tarjetaProfesional;



        /// <summary>
        /// Habilita o inhabilita el usuario
        /// </summary>
        private bool _activo;

        /// <summary>
        /// Indica si el usuario esta en proceso 
        /// </summary>
        private int _enProceso;

        /// <summary>
        /// almacena el password del usuario
        /// </summary>
        private string _password;
        
        /// <summary>
        /// Almacena el nombre del usuario
        /// </summary>
        private string _username;


        //jmartinez salvoconducto Fase 2
        /// <summary>
        /// Tipo identificacion IDEAM
        /// </summary>
        private string _TipoIdentificacionIDEAM;
        //jmartinez salvoconducto Fase 2
        /// <summary>
        /// Tipo Persona IDEAM
        /// </summary>
        private string _TipoPersonaIDEAM;

        #endregion

        #region declaracion de Propiedades ...
        public Int64 PersonaId { get { return this._personaId; } set { this._personaId = value; } }
        public string IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }
        public string PrimerNombre { get { return this._primerNombre; } set { this._primerNombre = value; } }
        public string SegundoNombre { get { return this._segundoNombre; } set { this._segundoNombre = value; } }
        public string PrimerApellido { get { return this._primerApellido; } set { this._primerApellido = value; } }
        public string SegundoApellido { get { return this._segundoApellido; } set { this._segundoApellido = value; } }
        public string CorreoElectronico
        {
            get { return _correoElectronico; }
            set { _correoElectronico = value; }
        }

        //jmartinez 2-11-2017 adiciono campo correo
        public bool AutorizaCorreo { get { return _autorizaCorreo; } set { _autorizaCorreo = value; }
        }
        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        public string NumeroIdentificacion { get { return this._numeroIdentificacion; } set { this._numeroIdentificacion = value; } }
        public TipoIdentificacionEntity TipoDocumentoIdentificacion { get { return this._tipoDocumentoIdentificacion; } set { this._tipoDocumentoIdentificacion = value; } }
        public string LugarExpediciónDocumento { get { return this._lugarExpedicionDocumento; } set { this._lugarExpedicionDocumento = value; } }
        public int Pais { get { return this._pais; } set { this._pais = value; } }
        public string NombrePais { get { return this._nombrepais; } set { this._nombrepais = value; } }
        public string Celular { get { return this._celular; } set { this._celular = value; } }
        public string Fax { get { return this._fax; } set { this._fax = value; } }
        public List<PersonaIndentity> ListaPersona;
        public Int64 IdApplicationUser { get { return this._idApplicationUser; } set { this._idApplicationUser = value; } }
        public string RazonSocial { get { return this._razonSocial; } set { this._razonSocial = value; } }
        public string Descripcion { get { return this._descripcion; } set { this._descripcion = value; } }
        public int IdAutoridadAmbiental { get { return this._idAutoridadAmbiental; } set { this._idAutoridadAmbiental = value; } }
        public TipoPersonaIdentity TipoPersona { get { return this._tipoPersona; } set { this._tipoPersona = value; } }
        public TipoPersonaIdentity TipoSolicitante { get { return this._tipoSolicitante; } set { this._tipoSolicitante = value; } }
        //public string Otro { get { return this._otro; } set { this._otro = value; } }
        public string TarjetaProfesional { get { return this._tarjetaProfesional; } set { this._tarjetaProfesional = value; } }
        public Int64 IdSolicitante { get { return _idSolicitante; } set { _idSolicitante = value; } }
        public string Respuesta { get { return _respuesta; } set { _respuesta = value; } }
        public string Pregunta { get { return _pregunta; } set { _pregunta = value; } }
        public bool Activo { get { return _activo; } set { _activo = value; } }
        public DireccionPersonaIdentity DireccionPersona { get { return _direccionPersona; } set { _direccionPersona = value; } }

        //V 1.0.0.42
        public int EnProceso { get { return this._enProceso; } set { this._enProceso = value; } }
        
        public string Password { get { return _password; } set { _password = value; } }
        public string Username { get { return _username; } set { _username = value; } }

        public List<DireccionPersonaIdentity> Direcciones 
        {
            get { return this._direcciones; }
            set { this._direcciones = value; }
        }

        public string RutaRtf
        {
            get { return _rutaRtf; }
            set { _rutaRtf = value; }
        }

        // 01-jul-2010 - aegb
        public string NumeroVital
        {
            get { return _numeroVital; }
            set { _numeroVital = value; }
        }
		//jmartinez salvoconducto fase 2
        /// <summary>
        /// Variable tipo persona IDEAM
        /// </summary>
        public string TipoPersonaIDEAM { get { return _TipoPersonaIDEAM; } set { _TipoPersonaIDEAM = value; } }
        /// <summary>
        /// Variable tipo identificacion IDEAM
        /// </summary>
        public string TipoIdentificacionIDEAM { get { return _TipoIdentificacionIDEAM; } set { _TipoIdentificacionIDEAM = value; } }
        #endregion





    }
}
