using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class PersonaLogoIdentity
    {
        /// <summary>
        /// Identificador de la Persona
        /// </summary>
        public int UsuarioID { get; set; }

        /// <summary>
        /// Imagen del Logo
        /// </summary>
        public byte[] Logo { get; set; }

        /// <summary>
        /// Imagen del Logo en base 64
        /// </summary>
        public string LogoBase64 { get; set; }

        /// <summary>
        /// Nombre del Logo
        /// </summary>
        public string NombreLogo { get; set; }

        /// <summary>
        /// Tipo de archivo del Logo
        /// </summary>
        public string TipoLogo { get; set; }

        /// <summary>
        /// Longitud de la imagen del Logo
        /// </summary>
        public int LongitudLogo { get; set; }
    }
}
