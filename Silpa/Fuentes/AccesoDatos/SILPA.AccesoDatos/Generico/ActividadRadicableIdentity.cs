using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    /// Clase utilizada para manipular actividades que crean registros de radicación
    /// para Ser enviados a correspondecia
    /// </summary>
    /// <remarks>Actividades de BPM Workflow ingresadas en Tabla ACTIVIDAD_RADICABLE de SILPA</remarks>
    public class ActividadRadicableIdentity
    {
        #region Atributos
        private int _id;

        private string _nombre;

        private int _idForma;

        private int? _idInstancia; 
        #endregion


        #region Propiedades
        /// <summary>
        /// Identificador de la instancia particular que se asocia a esta actividad
        /// </summary>
        public int? IDInstancia
        {
            get { return _idInstancia; }
            set { _idInstancia = value; }
        }

        /// <summary>
        /// Identificador del Formulario de FormBuilder
        /// </summary>
        public int IDForma
        {
            get { return _idForma; }
            set { _idForma = value; }
        }


        /// <summary>
        /// Nombre de la Actividad de WorkFlow
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        /// <summary>
        /// Identificador de la Actividad de WorkFlow
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        #endregion
    }
}
