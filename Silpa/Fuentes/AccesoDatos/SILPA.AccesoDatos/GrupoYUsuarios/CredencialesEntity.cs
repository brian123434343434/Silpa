using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GrupoYUsuarios
{
    /// <summary>
    /// Clase que representa la entidad con los datos de credenciales Utiles para la seguridad
    /// </summary>
    public class CredencialesEntity
    {
        /// <summary>
        /// Variable y propidad con la autoridad ambiental
        /// </summary>
        private Generico.AutoridadAmbientalIdentity _autoridad;
        public Generico.AutoridadAmbientalIdentity Autoridad
        {
            get { return _autoridad; }
            set { _autoridad = value; }
        }

        /// <summary>
        /// Variable y propiedad del Logueo del usuario
        /// </summary>
        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        /// <summary>
        /// variable y propiedad del Password del usuario
        /// </summary>
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
