using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Sancionatorio
{
    public class CoordenadaIdentity : EntidadSerializable
    {
        public CoordenadaIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador de la coordenada
        /// </summary>
        private Int64 _idCoordenada;

        /// <summary>
        /// Identificador de la queja
        /// </summary>
        private Int64 _idQueja;

        /// <summary>
        /// Coordenada en X
        /// </summary>
        private string _coordenadaX;

        /// <summary>
        /// Coordenada en Y
        /// </summary>
        private string _coordenadaY;

        /// <summary>
        /// Estado de la coordenada
        /// </summary>
        private bool _activoCoordenada;
        
        #endregion

        #region Propiedades....

        public Int64 IdCoordenada
        {
            get { return _idCoordenada; }
            set { _idCoordenada = value; }
        }

        public Int64 IdQueja
        {
            get { return _idQueja; }
            set { _idQueja = value; }
        }

        public string CoordenadaX
        {
            get { return _coordenadaX; }
            set { _coordenadaX = value; }
        }

        public string CoordenadaY
        {
            get { return _coordenadaY; }
            set { _coordenadaY = value; }
        }
        
        public bool ActivoCoordenada
        {
            get { return _activoCoordenada; }
            set { _activoCoordenada = value; }
        }

        #endregion
    }
}
