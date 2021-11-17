using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class AreaHidrograficaIndenitty : EntidadSerializable
    {

        /// <summary>
        /// Constructor sin parámetros
        /// </summary>
        public AreaHidrograficaIndenitty() { }

        
        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        /// <param name="intId">int: identificador del area </param>
        /// <param name="strNombre">string: nombre del area</param>
        /// <param name="blnActivo">area activa / inactiva</param>
        public AreaHidrograficaIndenitty(int intId, string strNombre, bool blnActivo) 
        {
            this._id = intId;
            this._nombre = strNombre;
            this._activo = blnActivo;
        }

        #region Declaración de campos... 

        /// <summary>
        /// Identificador del area Hidrografica
        /// </summary>
        private int _id;

        /// <summary>
        /// Nombre del area hidrografica
        /// </summary>
        private string _nombre;

        /// <summary>
        /// Area hidrigrafica activa / inactiva
        /// </summary>
        private bool _activo;
        #endregion

        #region Declaración de propiedades...
        public int Id { get { return _id; } set { this._id = value;  } }
        public string Nombre { get { return _nombre; } set { this._nombre = value; } }
        public bool Activo { get { return _activo; } set { this._activo = value; } }
        #endregion


    }
}
