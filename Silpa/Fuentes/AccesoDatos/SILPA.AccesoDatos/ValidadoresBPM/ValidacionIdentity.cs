using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.ValidadoresBPM
{
    public class ValidacionIdentity : EntidadSerializable
    {
        public ValidacionIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador de la validación
        /// </summary>
        private int _idValidacion;

        /// <summary>
        /// Descripción de la validación
        /// </summary>
        private string _descripcionValidacion;

        /// <summary>
        /// Sentencia de la validación
        /// </summary>
        private string _sentenciaValidacion;
               

        #endregion

        #region Propiedades....

        public int IdValidacion
        {
            get { return _idValidacion; }
            set { _idValidacion = value; }
        }

        public string DescripcionValidacion
        {
            get { return _descripcionValidacion; }
            set { _descripcionValidacion = value; }
        }

        public string SentenciaValidacion
        {
            get { return _sentenciaValidacion; }
            set { _sentenciaValidacion = value; }
        }

        #endregion

    }
}
