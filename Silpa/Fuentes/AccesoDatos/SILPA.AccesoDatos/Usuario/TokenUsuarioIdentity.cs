using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Usuario
{
    public class TokenUsuarioIdentity
    {
        /// <summary>
        /// Atributo y propiedad con la Entidad inmersa de usuario
        /// </summary>
        private UsuarioIdentity _idUsuario;
        public UsuarioIdentity IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        /// <summary>
        /// Atributo y propiedad con el token resultante de su conexión
        /// </summary>
        private string _token;
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}
