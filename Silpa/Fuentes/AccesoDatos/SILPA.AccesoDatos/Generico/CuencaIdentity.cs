using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    /// Representa la entidad de Cuenca Hidrogr�fica
    /// </summary>
    public class CuencaIdentity : EntidadSerializable
    {
        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public CuencaIdentity() { }

        /// <summary>
        /// Variable y atributo con el identificador de la entidad Cuenca
        /// </summary>
        private int _id;
        public int Id {
            set { _id = value;}
            get { return _id; }
        }

        /// <summary>
        /// variable y atributo con la descripci�n de la entidad Cuenca
        /// </summary>
        private string _nombre;
        public string Nombre{
            set { _nombre = value; }
            get { return _nombre; }
        }

        /// <summary>
        /// Variable y atributo que indica si la entidad Cuenca est� activo S o N
        /// </summary>
        private bool _activo;
        public bool Activo {
            set { _activo = value; }
            get { return _activo;}
        }
    }
}
