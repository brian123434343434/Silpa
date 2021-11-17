using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Sancionatorio
{
    public class PersonaQuejaIdentity :  EntidadSerializable
    {
        public PersonaQuejaIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador de la persona
        /// </summary>
        private Int64 _idPersonaQueja;

        /// <summary>
        /// Identificador de la queja
        /// </summary>
        private Int64 _idQueja;

        /// <summary>
        /// Identificador del tipo de persona (Quejosa - Infractor)
        /// </summary>
        private int _idTipoPersona;

        /// <summary>
        /// Primer nombre de la persona
        /// </summary>
        private string _primerNombre;

        /// <summary>
        /// Segundo nombre de la persona
        /// </summary>
        private string _segundoNombre;

        /// <summary>
        /// Primer apellido de la persona
        /// </summary>
        private string _primerApellido;

        /// <summary>
        /// Segundo apellido de la persona
        /// </summary>
        private string _segundoApellido;

        /// <summary>
        /// Identificador del tipo de identificacion
        /// </summary>
        private Nullable<int> _idTipoIdentificacion;

        /// <summary>
        /// Numero de identificacion
        /// </summary>
        private string _numeroIdentificacion;

        /// <summary>
        /// Identificador del municipio de origen de la identificacion
        /// </summary>
        private Nullable<int> _idMunicipioOrigen;

        /// <summary>
        /// Direccion de la persona
        /// </summary>
        private string _direccion;

        /// <summary>
        /// Identificador del municipio de la persona
        /// </summary>
        private Nullable<int> _idMunicipio;

        /// <summary>
        /// Identificador del corregimiento
        /// </summary>
        private Nullable<int> _idCorregimiento;

        /// <summary>
        /// Identificador de la vereda
        /// </summary>
        private Nullable<int> _idVereda;

        /// <summary>
        /// Telefono de la persona
        /// </summary>
        private string _telefono;

        /// <summary>
        /// Correo Electrónico de la persona
        /// </summary>
        private string _correoElectronico;

        private bool _activoPersonaQueja;
               
                        
        #endregion

        #region Propiedades....

        public Int64 IdPersonaQueja
        {
            get { return _idPersonaQueja; }
            set { _idPersonaQueja = value; }
        }

        public Int64 IdQueja
        {
            get { return _idQueja; }
            set { _idQueja = value; }
        }

        public int IdTipoPersona
        {
            get { return _idTipoPersona; }
            set { _idTipoPersona = value; }
        }

        public string PrimerNombre
        {
            get { return _primerNombre; }
            set { _primerNombre = value; }
        }

        public string SegundoNombre
        {
            get { return _segundoNombre; }
            set { _segundoNombre = value; }
        }

        public string PrimerApellido
        {
            get { return _primerApellido; }
            set { _primerApellido = value; }
        }

        public string SegundoApellido
        {
            get { return _segundoApellido; }
            set { _segundoApellido = value; }
        }

        public Nullable<int> IdTipoIdentificacion
        {
            get { return _idTipoIdentificacion; }
            set { _idTipoIdentificacion = value; }
        }

        public string NumeroIdentificacion
        {
            get { return _numeroIdentificacion; }
            set { _numeroIdentificacion = value; }
        }

        public Nullable<int> IdMunicipioOrigen
        {
            get { return _idMunicipioOrigen; }
            set { _idMunicipioOrigen = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public Nullable<int> IdMunicipio
        {
            get { return _idMunicipio; }
            set { _idMunicipio = value; }
        }

        public Nullable<int> IdCorregimiento
        {
            get { return _idCorregimiento; }
            set { _idCorregimiento = value; }
        }

        public Nullable<int> IdVereda
        {
            get { return _idVereda; }
            set { _idVereda = value; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public string CorreoElectronico
        {
            get { return _correoElectronico; }
            set { _correoElectronico = value; }
        }

        public bool ActivoPersonaQueja
        {
            get { return _activoPersonaQueja; }
            set { _activoPersonaQueja = value; }
        }

        #endregion

    }

}
