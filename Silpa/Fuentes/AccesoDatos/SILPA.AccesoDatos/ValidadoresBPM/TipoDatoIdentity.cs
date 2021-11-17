using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.ValidadoresBPM
{
    public class TipoDatoIdentity : EntidadSerializable
    {
        public TipoDatoIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador del tipo de dato
        /// </summary>
        private int _idTipoDato;

        /// <summary>
        /// Descripción del tipo de dato
        /// </summary>
        private string _descripcionTipoDato;

        /// <summary>
        /// Separador del tipo de dato
        /// </summary>
        private string _separadorTipoDato;              

        #endregion

        #region Propiedades....


        public int IdTipoDato
        {
            get { return _idTipoDato; }
            set { _idTipoDato = value; }
        }

        public string DescripcionTipoDato
        {
            get { return _descripcionTipoDato; }
            set { _descripcionTipoDato = value; }
        }

        public string SeparadorTipoDato
        {
            get { return _separadorTipoDato; }
            set { _separadorTipoDato = value; }
        }

        #endregion


    }
}
