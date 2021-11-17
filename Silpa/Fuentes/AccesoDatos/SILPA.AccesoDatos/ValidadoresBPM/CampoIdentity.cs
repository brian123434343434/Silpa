using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.ValidadoresBPM
{
    public class CampoIdentity : EntidadSerializable
    {
        public CampoIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador del campo
        /// </summary>
        private string _idCampo;

        /// <summary>
        /// Descripción del campo
        /// </summary>
        private string _descripcionCampo;

        /// <summary>
        /// Identificador del tipo de dato del campo
        /// </summary>
        private int _idTipoDato;


        #endregion

        #region Propiedades....

        public string IdCampo
        {
            get { return _idCampo; }
            set { _idCampo = value; }
        }

        public string DescripcionCampo
        {
            get { return _descripcionCampo; }
            set { _descripcionCampo = value; }
        }

        public int IdTipoDato
        {
            get { return _idTipoDato; }
            set { _idTipoDato = value; }
        }

        #endregion
    }
}
