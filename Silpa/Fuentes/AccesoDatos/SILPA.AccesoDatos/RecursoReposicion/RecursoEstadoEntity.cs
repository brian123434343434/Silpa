using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.RecursoReposicion
{
    /// <summary>
    /// Se encarga de manejar los Estados del Recurso
    /// </summary>
    public class RecursoEstadoEntity
    {
        private int _idEstadoRecurso;

        private string _nombreEstado;

        private bool _activo;

        private List<RecursoEntity> _recursoReposicion;
        /// <summary>
        /// Nombre del Estado del Recurso
        /// </summary>
        public string Nombre
        {
            get { return _nombreEstado; }
            set { _nombreEstado = value; }
        }

        /// <summary>
        /// Identificador del Estado del Recurso
        /// </summary>
        public int IDEstadoRecurso
        {
            get { return _idEstadoRecurso; }
            set { _idEstadoRecurso = value; }
        }

        public bool activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        public List<RecursoEntity> RecursoReposicion
        {
            get{ return _recursoReposicion;}
            set { _recursoReposicion = value; }
        }
         
    }
}
