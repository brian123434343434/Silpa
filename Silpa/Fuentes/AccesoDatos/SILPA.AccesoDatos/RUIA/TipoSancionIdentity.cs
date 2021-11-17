using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.RUIA
{
    public class TipoSancionIdentity : EntidadSerializable
    {
        public TipoSancionIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador del Tipo de Sanción
        /// </summary>
        private int _idTipoSancion;

        /// <summary>
        /// Nombre del Tipo de Sanción
        /// </summary>
        private string _nombreTipoSancion;

        /// <summary>
        /// Estado del Tipo de Sanción
        /// </summary>
        private bool _activoTipoSancion;

        #endregion

        #region Propiedades....

        public int IdTipoSancion
        {
            get { return _idTipoSancion; }
            set { _idTipoSancion = value; }
        }

        public string NombreTipoSancion
        {
            get { return _nombreTipoSancion; }
            set { _nombreTipoSancion = value; }
        }


        public bool ActivoTipoSancion
        {
            get { return _activoTipoSancion; }
            set { _activoTipoSancion = value; }
        }

        #endregion
    }
}
