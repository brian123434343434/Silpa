using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class PersonaIndentity :EntidadSerializable
    {
        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public PersonaIndentity() { }


        public PersonaIndentity
            (
            int intPersonaId, string strIdUsuario, string strCorreoElectronico
            ) 
        { 

            this._correoElectronico = strCorreoElectronico;
            this._idUsuario = strIdUsuario;
            this._personaId = intPersonaId;
            //this._primerApellido
            //this._segundoApellido
            //this._primerNombre
            //this._segundoNombre
            //this._telefono
            //this._tipoSolicitante
            //this._idUsuario
        }


        #region declaracion de campos ...

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
        /// Tipo de identificación documento de la persona
        /// </summary>
        private TipoIdentificacionEntity _tipoDocumento;

        /// <summary>
        /// Lugar de expedición documento
        /// </summary>
        private string _lugarExpediciónDocumento;

        /// <summary>
        /// País
        /// </summary>
        private int _pais;
        
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
        private int _idApplicationUser;

        /// <summary>
        /// Razon Social
        /// </summary>
        private string _razonSocial;

        /// <summary>
        /// Calidad en que actua
        /// </summary>
        private TipoPersonaIdentity _calidadActua;

        /// <summary>
        /// Tipo Persona
        /// </summary>
        private TipoPersonaIdentity _tipoPersona;

        /// <summary>
        /// Otro dato
        /// </summary>
        private string _otro;

        /// <summary>
        /// Tarjeta profesional
        /// </summary>
        private string _tarjetaProfesional;

        /// <summary>
        /// Dirección de la persona
        /// </summary>
        private DireccionPersonaIdentity _direccion;

        /// <summary>
        /// Identifiacador del solicitante en caso de ser un tercero, representante o apoderado
        /// </summary>
        private Int64 _IdSolicitante;


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
            public string Telefono
            {
                get { return _telefono; }
                set { _telefono = value; }
            }
            public string NumeroIdentificacion { get { return this._numeroIdentificacion; } set { this._numeroIdentificacion = value; } }
            public TipoIdentificacionEntity TipoDocumento { get { return this._tipoDocumento; } set { this._tipoDocumento = value; } }
            public string LugarExpediciónDocumento { get { return this._lugarExpediciónDocumento; } set { this._lugarExpediciónDocumento = value; } }
            public int Pais { get { return this._pais; } set { this._pais = value; } }
            public string Celular { get { return this._celular; } set { this._celular = value; } }
            public string Fax { get { return this._fax; } set { this._fax = value; } }
            public List<PersonaIndentity> ListaPersona;
            public int IdApplicationUser { get { return this._idApplicationUser; } set { this._idApplicationUser = value; } }
            public string RazonSocial { get { return this._razonSocial; } set { this._razonSocial = value; } }
            public TipoPersonaIdentity CalidadActua { get { return this._calidadActua; } set { this._calidadActua = value; } }
            public TipoPersonaIdentity TipoPersona { get { return this._tipoPersona; } set { this._tipoPersona = value; } }
            public string Otro { get { return this._otro; } set { this._otro = value; } }
            public string TarjetaProfesional { get { return this._tarjetaProfesional; } set { this._tarjetaProfesional = value; } }
            public DireccionPersonaIdentity Direccion { get { return this._direccion; } set { this._direccion = value; } }
            public Int64 IdSolicitante { get { return _IdSolicitante; } set { _IdSolicitante = value; } }

        #endregion
        }
}
