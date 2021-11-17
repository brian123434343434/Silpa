using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.BPMProcess
{
    public class CampoIdentity:EntidadSerializable
    {
        public CampoIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Numero y orden de los campos
        /// </summary>
        private Int64 _numRow;


        /// <summary>
        /// Identificador del campo
        /// </summary>
        private Int64 _idField;


        /// <summary>
        /// Grupo al cual pertenece el campo
        /// </summary>
        private string _grupo;


        /// <summary>
        /// Nombre del campo
        /// </summary>
        private string _nombre;


        /// <summary>
        /// Tipo de dato
        /// </summary>
        private string _tipoDato;

        /// <summary>
        /// Es requerido?
        /// </summary>
        private bool _requerido;

        /// <summary>
        /// Tipo de Objeto textbox,label etc...
        /// </summary>
        private string _tipo;

        

        #endregion


        #region Propiedades....

        public Int64 NumRow
        {
            get { return _numRow; }
            set { _numRow = value; }
        }

        public Int64 Id
        {
            get { return _idField; }
            set { _idField = value; }
        }

        public string Grupo
        {
            get { return _grupo; }
            set { _grupo = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }


        public string TipoDato
        {
            get { return _tipoDato; }
            set { _tipoDato = value; }
        }

        public bool Requerido
        {
            get { return _requerido; }
            set { _requerido = value; }
        }

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        #endregion
    }
}
