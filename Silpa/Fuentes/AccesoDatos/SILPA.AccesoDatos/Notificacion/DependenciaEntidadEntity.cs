using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    public class DependenciaEntidadEntity
    {
        #region Atributos
        /// <summary>
        /// Identificador de la Dependencia
        /// </summary>
        private string _id;

        /// <summary>
        /// Nombre de la Dependencia
        /// </summary>
        private string _nombre; 
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        } 
        #endregion


    }
}
