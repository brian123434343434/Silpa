using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.RUIA
{
    public class OpcionSancionIdentity : EntidadSerializable
    {
        public OpcionSancionIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador de la Opci�n de Sanci�n
        /// </summary>
        private int _idOpcionSancion;

        /// <summary>
        /// Nombre de la Opci�n de Sanci�n
        /// </summary>
        private string _nombreOpcionSancion;

        /// <summary>
        /// Estado de la Opci�n de Sanci�n
        /// </summary>
        private bool _activoOpcionSancion;

        #endregion

        #region Propiedades....
        
        public int IdOpcionSancion
        {
            get { return _idOpcionSancion; }
            set { _idOpcionSancion = value; }
        }

        public string NombreOpcionSancion
        {
            get { return _nombreOpcionSancion; }
            set { _nombreOpcionSancion = value; }
        }


        public bool ActivoOpcionSancion
        {
            get { return _activoOpcionSancion; }
            set { _activoOpcionSancion = value; }
        }        

        #endregion

    }
}
