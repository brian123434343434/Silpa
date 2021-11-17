using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Usuario;  

namespace SILPA.LogicaNegocio.Usuario
{
    /// <summary>
    /// Clase de Logica de negocio que permite tomar el token con el ID
    /// de un usuario dado
    /// </summary>
    public class TokenUsuario
    {
        public TokenUsuario()
        { 
        }
        /// <summary>
        /// Metodo que permite tomar el token con el ID
        /// de un usuario dado
        /// </summary>
        /// <param name="id">entero</param>
        /// <returns>UsuarioIdentity</returns>
        public string TomarTokenUsuario(int id)
        {
            TokenUsuarioDalc dalc = new TokenUsuarioDalc();
            TokenUsuarioIdentity token = new TokenUsuarioIdentity();
            try
            {
                token = dalc.ObtenerToken(id);
                return token.Token;

            }
            finally
            {
                dalc = null;
            }
        }
    }
}
