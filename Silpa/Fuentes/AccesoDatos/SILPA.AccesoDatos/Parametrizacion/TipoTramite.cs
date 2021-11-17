using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun; 

namespace SILPA.AccesoDatos.Parametrizacion
{
   // <summary>
    /// Creacion Feb 01 - aegb
    /// Clase Proceso, tiene la capacidad de contener los atributos de la
    /// entidad tipo tramite, replica de la base de datos
    /// </summary>
    public class TipoTramite : EntidadSerializable
    {
        public TipoTramite()
        {
        }
        /// <summary>
        /// Atributo y propiedad identificador de la entidad  tipo tramite
        /// </summary>
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Atributo y propiedad con el nombre de la entidad  tipo tramite
        /// </summary>
        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

       
        /// <summary>
        /// Atributo y propiedad con el indetificador externo de la entidad tipo de proceso
        /// </summary>
        private int _tipoProceso;
        public int TipoProceso
        {
            get { return _tipoProceso; }
            set { _tipoProceso = value; }
        }

        /// <summary>
        /// Atributo y propiedad identificador de la entidad  tipo tramite
        /// </summary>
        private int? _inicioProceso;
        public int? InicioProceso
        {
            get { return _inicioProceso; }
            set { _inicioProceso = value; }
        }

    }
}
