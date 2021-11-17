using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.BPMProcess
{
    public class FormularioIdentity:EntidadSerializable
    {
        public FormularioIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador del Formulario
        /// </summary>
        private Int64 _idForm;

              
        /// <summary>
        /// Nombre del Formulario
        /// </summary>
        private string _nombre;        

        #endregion


        #region Propiedades....

        public Int64 Id
        {
            get { return _idForm; }
            set { _idForm = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }        

        #endregion
    }
}
