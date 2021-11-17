using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class EstadoCobroIdentity : EntidadSerializable
    {
        #region Atributos


        private int _idEstadoCobroField;

        private string _nombreField;

        private string _descripcionField;

        private bool _activoField;

        #endregion

        #region Propiedades

        /// <comentarios/>
        public int IdEstadoCobro
        {
            get
            {
                return this._idEstadoCobroField;
            }
            set
            {
                this._idEstadoCobroField = value;
            }
        }

        /// <comentarios/>
        public string Nombre
        {
            get
            {
                return this._nombreField;
            }
            set
            {
                this._nombreField = value;
            }
        }

        /// <comentarios/>
        public string Descripcion
        {
            get
            {
                return this._descripcionField;
            }
            set
            {
                this._descripcionField = value;
            }
        }

        /// <comentarios/>
        public bool Activo
        {
            get
            {
                return this._activoField;
            }
            set
            {
                this._activoField = value;
            }
        }
        #endregion

    }
}

