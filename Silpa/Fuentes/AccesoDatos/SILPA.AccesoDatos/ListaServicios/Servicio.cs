using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun; 

namespace SILPA.AccesoDatos.ListaServicios
{
    /// <summary>
    /// Creacion Dic 23 - MIRM
    /// Clase servicio, tiene la capacidad de contener los atributos de la
    /// entidad servicio, replica de la base de datos
    /// </summary>
    public class Servicio : EntidadSerializable 
    {
        public Servicio()
        { 
        }
        /// <summary>
        /// Atributo y propiedad identificador de la entidad Servicio
        /// </summary>
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Atributo y propiedad con el nombre de la entidad Servicio
        /// </summary>
        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        /// <summary>
        /// Atributo y propiedad con la Direccion URL donde se ubicará el servicio
        /// </summary>
        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// Atributo y propiedad que indica si la Entidad rsta activa o no 1=activo, otro=No activo
        /// </summary>
        private int _activo;
        public int Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        
    }
}
