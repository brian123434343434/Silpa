using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class PersonaFirmaIdentity
    {
        /// <summary>
        /// Identificador de la Persona
        /// </summary>
        public int UsuarioID { get; set; }

        /// <summary>
        /// Nit de la persona
        /// </summary>
        public string Nit { get; set; }

        /// <summary>
        /// Nombre de la persona a la cual pertenece la firma autorizada
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Cargo de la persona a la cual pertenece la firma autorizada
        /// </summary>
        public string Cargo { get; set; }

        /// <summary>
        /// Imagen que contiene la firma
        /// </summary>
        public byte[] Imagen { get; set; }

        /// <summary>
        /// Imagen en base 64
        /// </summary>
        public string ImagenBase64 { get; set; }

        /// <summary>
        /// Nombre de la imagen que contiene la firma
        /// </summary>
        public string NombreImagen { get; set; }

        /// <summary>
        /// Tipo de la imagen que contiene la firma
        /// </summary>
        public string TipoImagen { get; set; }

        /// <summary>
        /// Longitud de la imagen que contiene la firma
        /// </summary>
        public int LongitudImagen { get; set; }
    }
}
