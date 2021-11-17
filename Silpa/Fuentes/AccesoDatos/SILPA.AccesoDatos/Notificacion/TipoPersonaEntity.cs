using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    [Serializable]
    public class TipoPersonaEntity
    {
        #region Atributos
        /// <summary>
        /// Identificador Interno (SILPA)
        /// </summary>
        private int _id;

        /// <summary>
        /// C�digo del tipo de Persona
        /// </summary>
        /// <remarks>La lista de c�digos se puede encontrar en GEL-XML</remarks>
        private string _codigo;

        /// <summary>
        /// Nombre del tipo de Persona
        /// </summary>
        /// <remarks>la lista de nombres est� en GEL-XML y coincide con la lista de c�digos</remarks>
        private string _nombre;

        #endregion


        #region Propiedades
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }


        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }


        public int ID
        {
            get { return _id; }
            set { _id = value; }
        } 
        #endregion


    }
}
