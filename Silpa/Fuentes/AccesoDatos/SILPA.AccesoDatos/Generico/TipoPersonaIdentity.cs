using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    
    /// <summary>
    /// Clase Tipo de persona
    /// </summary>
    /// 
    [Serializable]
    public class TipoPersonaIdentity
    {

        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public TipoPersonaIdentity() { }

        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// </summary>
        /// <param name="intCodigoTipoPersona">int: identificador del tipo de persona</param>
        /// <param name="strNombreTipoPersona">string: nombre del tipo de persona</param>
        public TipoPersonaIdentity
        (
            int intCodigoTipoPersona, 
            string strNombreTipoPersona
        )
        {
            this._codigoTipoPersona = intCodigoTipoPersona;
            this._nombreTipoPersona = strNombreTipoPersona;
        }


        #region Declaracion de campos...
        
        /// <summary>
        /// codigo del tipo de persona
        /// </summary>
        private int _codigoTipoPersona;

        /// <summary>
        /// Nombre del tipo de persona
        /// </summary>
        private string _nombreTipoPersona;

        #endregion

        #region Declaracion de las propiedades ... 
        public int CodigoTipoPersona
        {
            get { return this._codigoTipoPersona; }
            set { this._codigoTipoPersona = value; }
        }

        public string NombreTipoPersona
        {
            get { return this._nombreTipoPersona; }
            set { this._nombreTipoPersona = value; }
        }        
        #endregion

    }
}

