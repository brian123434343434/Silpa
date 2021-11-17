using System;
using System.Collections.Generic;
using System.Text;

namespace IdentidadAutoridad.Entidades
{
    /// <summary>
    /// Entidad que representa las identidades de las Autoridades ambientales
    /// </summary>
    public class IdentidadAutoridad
    {
        /// <summary>
        /// Variable y Propiedad con el Identificador de la entidad
        /// </summary>
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Variable y Propiedad con el Identificador de la Autoridad ambiental
        /// </summary>
        private int _idAA;
        public int IdAA
        {
            get { return _idAA; }
            set { _idAA = value; }
        }

        /// <summary>
        /// Variable y Propiedad con el usuario asociado a la entidad
        /// </summary>
        private string _usuario;
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        /// <summary>
        /// Variable y Propiedad con el password asociado al usuario de la entidad
        /// </summary>
        private string _password;
        public string Password
        {
            get { return _password; }
            set { 
                    _password = Identidad.DesEncriptarMensaje(value); 
                }
        }
    }
}
