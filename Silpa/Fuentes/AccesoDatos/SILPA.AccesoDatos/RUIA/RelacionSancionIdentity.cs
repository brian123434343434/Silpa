using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.RUIA
{
    public class RelacionSancionIdentity : EntidadSerializable
    {
        public RelacionSancionIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador de la sanción
        /// </summary>
        private Int64 _idSancion;

        /// <summary>
        /// Identificador del tipo de la sanción
        /// </summary>
        private int _idTipoSancion;

        /// <summary>
        /// Identificador de la opción de la sanción
        /// </summary>
        private int _idOpcionSancion;

        /// <summary>
        /// mardila 05/04/2010: Se crea la propiedad Sancion aplicada
        /// </summary>
        private string _sancionAplicada;

        #endregion

        #region Propiedades....

        /// <summary>
        /// mardila 05/04/2010: Propiedad Sancion aplicada
        /// </summary>
        public string SancionAplicada
        {
            get { return _sancionAplicada; }
            set { _sancionAplicada = value; }
        }

        public Int64 IdSancion
        {
          get { return _idSancion; }
          set { _idSancion = value; }
        }

        public int IdTipoSancion
        {
          get { return _idTipoSancion; }
          set { _idTipoSancion = value; }
        }

        public int IdOpcionSancion
        {
          get { return _idOpcionSancion; }
          set { _idOpcionSancion = value; }
        }

        #endregion

    }
}
