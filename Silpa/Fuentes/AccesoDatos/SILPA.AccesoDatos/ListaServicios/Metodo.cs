using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun; 

namespace SILPA.AccesoDatos.ListaServicios
{

    /// <summary>
    /// Creada Dic 23 2009 MIRM
    /// Clase que contiene las propiedades y metodos
    /// de la Entidad Metodo
    /// </summary>
    public class Metodo : EntidadSerializable   
    {
        /// <summary>
        /// Constructor vacio necesrio para serializar la clase
        /// </summary>
        public Metodo()
        { 
        }

        /// <summary>
        /// Atributo y propiedad con el identificador de la Clase Metodo
        /// </summary>
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Atributo y propiedad con el Servicio asociado al metodo
        /// </summary>
        private Servicio _ser;
        public Servicio Ser
        {
            get { return _ser; }
            set { _ser = value; }
        }

        /// <summary>
        /// Atributo y propiedad con el nombre del metodo de la clase
        /// </summary>
        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        /// <summary>
        /// Atributo y propiedad con el indicador de activacion del metodo en la clase
        /// </summary>
        private int _activo;
        public int Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
 
    }
}
