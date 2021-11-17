using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.RUIA
{
    public class TipoFaltaIdentity : EntidadSerializable
    {
        public TipoFaltaIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador del Tipo de Falta
        /// </summary>
        private int _idTipoFalta;

        /// <summary>
        /// Nombre del Tipo de Falta
        /// </summary>
        private string _nombreTipoFalta;

        /// <summary>
        /// Estado del Tipo de Falta
        /// </summary>
        private bool _activoTipoFalta;
        
        #endregion

        #region Propiedades....

        public int IdTipoFalta
        {
            get { return _idTipoFalta; }
            set { _idTipoFalta = value; }
        }

        public string NombreTipoFalta
        {
            get { return _nombreTipoFalta; }
            set { _nombreTipoFalta = value; }
        }

        public bool ActivoTipoFalta
        {
            get { return _activoTipoFalta; }
            set { _activoTipoFalta = value; }
        }

        #endregion
    }
}
