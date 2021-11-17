using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.ReporteTramite
{
    public class MisTramitesIndentity : EntidadSerializable
    {
        /// <summary>
        /// Constructor sin parametros, útil en la serialización XML
        /// </summary>
        public MisTramitesIndentity() {  }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NumeroVital"></param>
        /// <param name="FechaCreacion"></param>
        /// <param name="Descripcion"></param>
        /// <param name="PathDocumento"></param>
        /// <param name="IdExpediente"></param>
        /// <param name="EtaNombre"></param>
        /// <param name="ActReposicion"></param>
        /// <param name="AddNombre"></param>
        /// <param name="ActoEjec"></param>
        public MisTramitesIndentity
        (
            string NumeroVital, DateTime FechaCreacion, string Descripcion,
            string PathDocumento, string IdExpediente,
            string EtaNombre, int ActReposicion, string AddNombre, int ActoEjec, int ActoNot,string TipoDocumento,
            string NombreExpediente,string DescripcionExpediente,string DescripcionActo
        )
        {
            this._numeroVital = NumeroVital;
            this._fechaCreacion = FechaCreacion;
            this._descripcion = Descripcion;
            this._pathDocumento = PathDocumento;
            this._idExpediente = IdExpediente;
            this._etaNombre = EtaNombre;
            this._actReposicion = ActReposicion;
            this._addNombre = AddNombre;
            this._actoEjec = ActoEjec;
            this._actoNot = ActoNot;
            this._tipoDocumento = TipoDocumento;            
            this._descripcionActo = DescripcionActo;
        }


        #region Declaración de variables..

        /// <summary>
        /// Número VITAL del trámite
        /// </summary>
        private string _numeroVital;

        /// <summary>
        /// Fecha de Creacion de la Actividad
        /// </summary>
        private DateTime _fechaCreacion;

        /// <summary>
        /// Descripcion de la actividad
        /// </summary>
        private string _descripcion;

        /// <summary>
        /// Ruta donde el Sistema puede visualizar el Archivo Asociado
        /// </summary>
        private string _pathDocumento;

        /// <summary>
        /// Expediente asociado a la actividad
        /// </summary>

        private string _idExpediente;

        /// <summary>
        /// Nombre de la estapa Asociada a esta actifidad
        /// </summary>

        private string _etaNombre;


        /// <summary>
        /// Envia si este tramite esta asociado a un Acta de Reposicion
        /// </summary>
        private int _actReposicion;




        /// <summary>
        /// Nombre de la actividad
        /// </summary>
        private string _addNombre;


        /// <summary>
        /// Codigo de Acto ejecutoriado
        /// </summary>
        private int _actoEjec;

        /// <summary>
        /// Codigo de Acto Relacion Notificado
        /// </summary>
        private int _actoNot;

        /// <summary>
        /// Tipo de documento que esta asociado al acto
        /// </summary>
        private string _tipoDocumento;
        

        /// <summary>
        /// Descripcion del actp
        /// </summary>
        private string _descripcionActo;

        #endregion


        #region declaración de propiedades ...
        
        public string NumeroVital{ get { return _numeroVital; } set { _numeroVital = value; } }
        public DateTime FechaCreacion { get { return _fechaCreacion; } set { _fechaCreacion = value; } }
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public string PathDocumento { get { return _pathDocumento; } set { _pathDocumento = value; } }
        public string IdExpediente { get { return _idExpediente; } set { _idExpediente = value; } }
        public string EtaNombre { get { return _etaNombre; } set { _etaNombre = value; } }
        public int ActReposicion { get { return _actReposicion; } set { _actReposicion = value; } }
        public string AddNombre { get { return _addNombre; } set { _addNombre = value; } }
        public int ActoEjec { get { return _actoEjec; } set { _actoEjec = value; } }
        public int ActoNot { get { return _actoNot; } set { _actoNot = value; } }
        public string TipoDocumento { get { return _tipoDocumento; } set { _tipoDocumento = value; } }        
        public string DescripcionActo { get { return _descripcionActo; } set { _descripcionActo = value; } }


        #endregion

    }
}
