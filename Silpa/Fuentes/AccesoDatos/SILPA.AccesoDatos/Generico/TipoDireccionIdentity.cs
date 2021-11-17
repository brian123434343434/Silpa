using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{

    /// <summary>
    /// clase tipo direcci�n
    /// </summary>
    public class TipoDireccionIdentity : EntidadSerializable
    {
        /// <summary>
        /// Constructor sin par�metros
        /// </summary>
        public TipoDireccionIdentity(){}

        /// <summary>
        /// Constructor con par�metros
        /// </summary>
        /// <param name="intId">int: identificador del tipo de direcci�n</param>
        /// <param name="strNombre">string: Nombre del tipo de direcci�n</param>
        /// <param name="blnActivo"> bool: Tipo de direcci�n activo / inactivo </param>
        public TipoDireccionIdentity(int intId, string strNombre, bool blnActivo) 
        {
            this._id = intId;
            this._nombre = strNombre;
            this._activo = blnActivo;
        }

        #region Declaraci�n de campos ...

        /// <summary>
        /// Indentificador del tipo de direcci�n.
        /// </summary>
        private int _id;

        /// <summary>
        /// Nombre del tipo de direcci�n
        /// </summary>
        private string _nombre;

        /// <summary>
        /// Tipo de direcci�n activa o inactiva
        /// </summary>
        private bool _activo;

        #endregion

        #region Declaraci�n de propiedades ...
        public int Id { get { return this._id; } set { this._id = value; } }
        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public bool Activo { get { return this._activo; } set { this._activo = value; } }
        #endregion
    }
}
