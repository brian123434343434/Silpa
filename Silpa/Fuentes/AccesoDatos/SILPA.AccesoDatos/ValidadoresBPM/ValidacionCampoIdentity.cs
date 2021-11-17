using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.ValidadoresBPM
{
    public class ValidacionCampoIdentity : EntidadSerializable
    {
        public ValidacionCampoIdentity()
        {
        }

        #region Definición de los atributos

        private int _idValidacionCampo;

        /// <summary>
        /// Identificador del campo
        /// </summary>
        private string _idCampo;

        /// <summary>
        /// Identificador de la validación
        /// </summary>
        private int _idValidacion;

        /// <summary>
        /// Fecha de insercion del campo a validar
        /// </summary>
        private string _fechaInsercion;

        /// <summary>
        /// Estado de la validacion del campo
        /// </summary>
        private string _activoValidacionCampo;

        #endregion

        #region Propiedades....

        public int IdValidacionCampo
        {
            get { return _idValidacionCampo; }
            set { _idValidacionCampo = value; }
        }

        public string IdCampo
        {
            get { return _idCampo; }
            set { _idCampo = value; }
        }

        public int IdValidacion
        {
            get { return _idValidacion; }
            set { _idValidacion = value; }
        }

        public string FechaInsercion
        {
            get { return _fechaInsercion; }
            set { _fechaInsercion = value; }
        }

        public string ActivoValidacionCampo
        {
            get { return _activoValidacionCampo; }
            set { _activoValidacionCampo = value; }
        }

        #endregion
    }
}

