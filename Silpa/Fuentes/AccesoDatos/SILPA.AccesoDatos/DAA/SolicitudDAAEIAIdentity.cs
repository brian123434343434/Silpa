using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Utilidades;
using SILPA.Comun;

namespace SILPA.AccesoDatos.DAA
{
    public class SolicitudDAAEIAIdentity : EntidadSerializable
    {


        public SolicitudDAAEIAIdentity()
        {
            this.FechaCreacion = DateTime.Now;
        }
        
        /// <summary>
        /// identificador de la solicitud
        /// </summary>
        private Int64 _idSolicitud;

        /// <summary>
        /// Almacena el Identificador del sector al que esta asociada la solicitud, este valor esta relacionado con la tabla SECTOR del la BD de sila
        /// </summary>
        private int? _idSector;

        /// <summary>
        /// Almacena el Identificador del Tipo de Proyecto al que esta asociada la solicitud, este valor esta relacionado con la tabla SECTOR del la BD de silaMC
        /// </summary>
        private int? _idTipoProyecto;
        
        /// <summary>
        /// Almacena Identificador de la Autoridad Ambiental a la que esta asociada la solicitud, este valor esta relacionado con la tabla AUTORIDAD_AMBIENTAL del la BD de silaMC
        /// </summary>
        private int? _idAutoridadAmbiental;

        /// <summary>
        /// Identificador del solicitante
        /// </summary>
        private Int64 _idSolicitante;

        /// <summary>
        /// Define si si la solicitud es para un proyecto urbano o rural
        /// </summary>
        private bool _urbano;

        /// <summary>
        /// Fecha en que se crea la solicitud de DDA
        /// </summary>
        private DateTime _fechaCreacion;


        /// <summary>
        /// Identifiador de la radicacion
        /// </summary>
        private long? _idRadicacion;

        /// <summary>
        /// identificador de la ubicación
        /// </summary>
        private int _idUbicacion;

        /// <summary>
        /// Identificador de la instancia del proceso a la que pertenece la solicitud
        /// </summary>
        private Int64 _idProcessInstance;

       
        /// <summary>
        /// Identificador del estado de la solicitud
        /// </summary>
        private int _idTipoEstadoSolicitud;

        /// <summary>
        /// Lista de Ubicaciones para esta solicitud
        /// </summary>
        private List<UbicacionIdentity> _lstObjUbicacionIdentity;

        public int FormularioID { get; set; }

        /// <summary>
        /// Define si extien conflictos entre autoridades ambientales y en es necesario que la decision sea tomada por el MAVDT
        /// </summary>
        private bool _conflicto;

        public Int64 IdSolicitud
        {
            get { return _idSolicitud; }
            set { _idSolicitud = value; }
        }

        public int? IdSector
        {
            get { return _idSector; }
            set { _idSector = value; }
        }

        public int? IdTipoProyecto
        {
            get { return _idTipoProyecto; }
            set { _idTipoProyecto = value; }
        }

        public int? IdAutoridadAmbiental
        {
            get { return _idAutoridadAmbiental; }
            set { _idAutoridadAmbiental = value; }
        }

        public Int64 IdSolicitante
        {
            get { return _idSolicitante; }
            set { _idSolicitante = value; }
        }

        public bool Urbano
        {
            get { return _urbano; }
            set { _urbano = value; }
        }

        public long? IdRadicacion
        {
            get { return _idRadicacion; }
            set { _idRadicacion = value; }
        }

        public bool Conflicto
        {
            get { return _conflicto; }
            set { _conflicto = value; }
        }

        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }

        public int IdUbicacion
        {
            get { return _idUbicacion; }
            set { _idUbicacion = value; }
        }

        public List<UbicacionIdentity> LstObjUbicacionIdentity
        {
            get { return _lstObjUbicacionIdentity; }
            set { _lstObjUbicacionIdentity = value; }
        }

         public int IdTipoEstadoSolicitud 
        {
            get{return _idTipoEstadoSolicitud;} 
            set{this._idTipoEstadoSolicitud=value;}
        }

         public Int64 IdProcessInstance
        {
            get{return _idProcessInstance;} 
            set{this._idProcessInstance=value;}
        }

        /// <summary>
        /// variable y propiedad con el identificador del tipo de lado
        /// </summary>
        private int _idTipoTramite;
        public int IdTipoTramite
        {
            get { return _idTipoTramite; }
            set { _idTipoTramite = value; }
        }
        /// <summary>
        /// Identificador con el numero SILPA
        /// </summary>
        private string _numeroSilpa;

        public string NumeroSilpa
        {
            get { return _numeroSilpa; }
            set { _numeroSilpa = value; }
        }
        
    }
}