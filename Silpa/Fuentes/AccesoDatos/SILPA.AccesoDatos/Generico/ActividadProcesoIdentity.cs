using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    public class ActividadProcesoIdentity
    {
        /// <summary>
        /// atributo y Propiedad que representa el identificación
        /// de la entidad.
        /// </summary>
        private int _idActividadProceso = -1;
        public int IdActividadProceso
        {
            get { return _idActividadProceso; }
            set { _idActividadProceso = value; }
        }
        /// <summary>
        /// Atributo y propiedad que representa
        /// al identificador de la actividad de la entidad Actividad Proceso
        /// </summary>
        private int _idActividad = -1;
        public int IdActividad
        {
            get { return _idActividad; }
            set { _idActividad = value; }
        }
        /// <summary>
        /// Atributo y propiedad que representa
        /// el identificador del proceso de la entidad actividad proceso
        /// </summary>
        private TipoEstadoProcesoIdentity _idEstadoProceso = null;
        public TipoEstadoProcesoIdentity IdEstadoProceso
        {
            get { return _idEstadoProceso; }
            set { _idEstadoProceso = value; }
        }
        /// <summary>
        /// Atributo y propiedad que represneta la fecha de creacion de la entidad
        /// </summary>
        private DateTime _fechaInsercion = DateTime.MinValue;
        public DateTime FechaInsercion
        {
            get { return _fechaInsercion; }
            set { _fechaInsercion = value; }
        }
        /// <summary>
        /// Atributo y propiedad que representa la descripcion de la entidad
        /// </summary>
        private string _descripcion = "";
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
    }
}
