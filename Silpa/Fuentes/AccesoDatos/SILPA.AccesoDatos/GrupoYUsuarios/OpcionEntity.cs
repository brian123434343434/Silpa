using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GrupoYUsuarios
{
    public class OpcionEntity
    {
        /// <summary>
        /// Identificador de la entidad.
        /// </summary>
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Descripcion textual de la entidad.
        /// </summary>
        private string _opcion;
        public string Opcion
        {
            get { return _opcion; }
            set { _opcion = value; }
        }

        /// <summary>
        /// Descripcion de valor util para armanr el menu en XML
        /// </summary>
        private string _valor;
        public string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }


        /// <summary>
        /// Direccion web en donde se encotrará la pagina que describe la opcion.
        /// </summary>
        private string _http;
        public string Http
        {
            get { return _http; }
            set { _http = value; }
        }

        /// <summary>
        /// Bit que indica si la entidad está activo a no.
        /// </summary>
        private bool _activo;
        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
    }
}
