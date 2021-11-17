using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.DAA
{
    public class JurisdiccionIdentity : EntidadSerializable
    {
        public JurisdiccionIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador de la jurisdicci�n
        /// </summary>
        private Int64 _idJurisdiccion;

        /// <summary>
        /// Identificador municipio de la jurisdicci�n
        /// </summary>
        private int _idMunicipio;

        /// <summary>
        /// Identificador de la autoridad de la jurisidicci�n
        /// </summary>
        private int _autoridadAmbiental;

        /// <summary>
        /// Fecha de inserci�n de la jurisdicci�n
        /// </summary>
        private string _fechaInsercion;

        #endregion

        #region Propiedades....

        public Int64 IdJurisdiccion
        {
            get { return _idJurisdiccion; }
            set { _idJurisdiccion = value; }
        }

        public int IdMunicipio
        {
            get { return _idMunicipio; }
            set { _idMunicipio = value; }
        }

        public int AutoridadAmbiental
        {
            get { return _autoridadAmbiental; }
            set { _autoridadAmbiental = value; }
        }

        public string FechaInsercion
        {
            get { return _fechaInsercion; }
            set { _fechaInsercion = value; }
        }

        #endregion
    }
}
