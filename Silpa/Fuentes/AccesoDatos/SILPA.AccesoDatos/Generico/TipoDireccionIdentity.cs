using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{

    /// <summary>
    /// clase tipo dirección
    /// </summary>
    public class TipoDireccionIdentity : EntidadSerializable
    {
        /// <summary>
        /// Constructor sin parámetros
        /// </summary>
        public TipoDireccionIdentity(){}

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        /// <param name="intId">int: identificador del tipo de dirección</param>
        /// <param name="strNombre">string: Nombre del tipo de dirección</param>
        /// <param name="blnActivo"> bool: Tipo de dirección activo / inactivo </param>
        public TipoDireccionIdentity(int intId, string strNombre, bool blnActivo) 
        {
            this._id = intId;
            this._nombre = strNombre;
            this._activo = blnActivo;
        }

        #region Declaración de campos ...

        /// <summary>
        /// Indentificador del tipo de dirección.
        /// </summary>
        private int _id;

        /// <summary>
        /// Nombre del tipo de dirección
        /// </summary>
        private string _nombre;

        /// <summary>
        /// Tipo de dirección activa o inactiva
        /// </summary>
        private bool _activo;

        #endregion

        #region Declaración de propiedades ...
        public int Id { get { return this._id; } set { this._id = value; } }
        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public bool Activo { get { return this._activo; } set { this._activo = value; } }
        #endregion
    }
}
