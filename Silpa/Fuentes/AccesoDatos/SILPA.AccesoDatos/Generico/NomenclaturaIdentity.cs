using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
   public class NomenclaturaIdentity : EntidadSerializable
    {
        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public NomenclaturaIdentity() { }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        /// <param name="intId"></param>
        /// <param name="strNombre"></param>
        /// <param name="blnActivo"></param>
        public NomenclaturaIdentity(int intId, string strNombre, bool blnActivo)
        {
            this._id = intId;
            this._nombre = strNombre;
            this._activo = blnActivo;
        }

        #region  Declaración de campos ...
        private int _id;
        private string _nombre;
        private bool _activo;
        #endregion

        #region  Declaración de propiedades...
        public int Id { get { return this._id; } set { this._id = value; } }
        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public bool Activo { get { return this._activo; } set { this._activo = value; } }
        #endregion
    }
}
