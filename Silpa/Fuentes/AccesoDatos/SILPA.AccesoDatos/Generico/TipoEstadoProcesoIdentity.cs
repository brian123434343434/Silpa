using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    /// clase que contiene los datos de la tabla tipo estado proceso
    /// </summary>
    public class TipoEstadoProcesoIdentity:EntidadSerializable
    {
        
        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public TipoEstadoProcesoIdentity(){}

        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="intId">int: identificador del estado de proceso</param>
        /// <param name="strNombre">string: nombde del estado de proceso</param>
        /// <param name="strDescripcion">string : descripción del estado de proceso</param>
        /// <param name="blnActivo">bool: Estado de priceso activo om inactivo</param>
        public TipoEstadoProcesoIdentity
            (
              int intId, string strNombre, string strDescripcion, bool blnActivo
            ) 
        { 
            this._id = intId;
            this._nombre = strNombre;
            this._descripcion = strDescripcion;
            this._activo = blnActivo;
        }
        
        
        /// <summary>
        /// Identificador del estado del proceso
        /// </summary>
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Nombre del estado de proceso
        /// </summary>
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        
        /// <summary>
        /// Descripcion del estado de proceso
        /// </summary>
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        /// <summary>
        /// Estado de proceso activo / inactivo
        /// </summary>
        private bool _activo;

        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
    }
}
