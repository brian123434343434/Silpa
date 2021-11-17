using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;


namespace SILPA.AccesoDatos.Notificacion
{
    [Serializable]
    public class NotificacionEntity : EntidadSerializable
    {

        #region Declaraci�n de campos y Propiedades

        /// <summary>
        /// Identificador del Acto_notificaci�n
        /// </summary>
        private decimal _idActoNotificacion;

        public decimal IdActoNotificacion
        {
            get { return _idActoNotificacion; }
            set { _idActoNotificacion = value; }
        }

        /// <summary>
        /// Identificador que  relaciona a los datos de los serivios de notificacion y publicacion electronica 
        /// cuando existe una publicacion que depende de la respuesta del servicio de publicaci�n
        /// </summary>
        //private Int64 idRelacionaPublicacionField;

        //public Int64 IdRelacionaPublicacion
        //{
        //    get
        //    {
        //        return this.idRelacionaPublicacionField;
        //    }
        //    set
        //    {
        //        this.idRelacionaPublicacionField = value;
        //    }
        //}

        /// <summary>
        /// Determina si el Acto requiere publicarse despues de que el estado sea Notificado
        /// </summary>
        private bool _requierePublicacion;

        public bool RequierePublicacion
        {
            get { return _requierePublicacion; }
            set { _requierePublicacion = value; }
        }

        /// <summary>
        /// Aqui se registra el numeri de proceso
        /// </summary>
        private string _procesoAdministracion;

        public string ProcesoAdministracion
        {
            get { return _procesoAdministracion; }
            set { _procesoAdministracion = value; }
        }

        /// <summary>
        /// Aqui se registra la parte resoluctiva de la entidad
        /// </summary>
        private string _parteResolutiva;

        public string ParteResolutiva
        {
            get { return _parteResolutiva; }
            set { _parteResolutiva = value; }
        }



        /// <summary>
        /// Determina el tipo de Documento para activar una actividad en BPM que sea relacionada con este
        /// </summary>
        private TipoDocumentoIdentity _idTipoActo;

        public TipoDocumentoIdentity IdTipoActo
        {
            get { return _idTipoActo; }
            set { _idTipoActo = value; }
        }


        /// <summary>
        /// N�mero del Acto Administrativo de Esta Notificaci�n
        /// </summary>
        private string _numeroActoAdministrativo;

        public string NumeroActoAdministrativo
        {
            get { return _numeroActoAdministrativo; }
            set { _numeroActoAdministrativo = value; }
        }

        /// <summary>
        /// C�digo de la Entidad P�blica
        /// </summary>
        private int _codigoEntidadPublica;

        public int CodigoEntidadPublica
        {
            get { return _codigoEntidadPublica; }
            set { _codigoEntidadPublica = value; }
        }


        /// <summary>
        /// Dependencia de la Entidad
        /// </summary>
        private DependenciaEntidadEntity _dependenciaEntidad;

        public DependenciaEntidadEntity DependenciaEntidad
        {
            get { return _dependenciaEntidad; }
            set { _dependenciaEntidad = value; }
        }

        /// <summary>
        /// Sistema de la Entidad P�blica
        /// </summary>
        private int _sistemaEntidadPublica;

        public int SistemaEntidadPublica
        {
            get { return _sistemaEntidadPublica; }
            set { _sistemaEntidadPublica = value; }
        }

        /// <summary>
        /// Tipo de Identificaci�n del Funcionario de la Entidad
        /// </summary>
        private TipoIdentificacionEntity _tipoIdentificacionFuncionario;

        public TipoIdentificacionEntity TipoIdentificacionFuncionario
        {
            get { return _tipoIdentificacionFuncionario; }
            set { _tipoIdentificacionFuncionario = value; }
        }

        /// <summary>
        /// N�mero de Identificaci�n del Funcionario
        /// </summary>
        private string _identificacionFuncionario;

        public string IdentificacionFuncionario
        {
            get { return _identificacionFuncionario; }
            set { _identificacionFuncionario = value; }
        }

        /// <summary>
        /// C�digo de Plantilla utilizada
        /// </summary>
        private string _idPlantilla;

        public string IdPlantilla
        {
            get { return _idPlantilla; }
            set { _idPlantilla = value; }
        }

        /// <summary>
        /// Nombre de Plantilla Utilizada
        /// </summary>
        private string _nombrePlantilla;

        public string NombrePlantilla
        {
            get { return _nombrePlantilla; }
            set { _nombrePlantilla = value; }
        }

        /// <summary>
        /// Acto Administrativo Asociado a este Acto
        /// </summary>
        private string _numeroActoAdministrativoAsociado;

        public string NumeroActoAdministrativoAsociado
        {
            get { return _numeroActoAdministrativoAsociado; }
            set { _numeroActoAdministrativoAsociado = value; }
        }

        /// <summary>
        /// N�mero SILPA al cu�l se encuentra asociado este acto
        /// </summary>
        private string _numeroSILPA;

        public string NumeroSILPA
        {
            get { return _numeroSILPA; }
            set { _numeroSILPA = value; }
        }



        /// <summary>
        /// N�mero SILPA al cu�l se encuentra asociado este acto
        /// </summary>
        private string _numeroSILPAAsociado;

        public string NumeroSILPAAsociado
        {
            get { return _numeroSILPAAsociado; }
            set { _numeroSILPAAsociado = value; }
        }
        /// <summary>
        /// Fecha del Acto Administrativo
        /// </summary>
        private DateTime? _fechaActo;

        public DateTime? FechaActo
        {
            get { return _fechaActo; }
            set { _fechaActo = value; }
        }

        private List<PersonaNotificarEntity> listaPersonas;

        public List<PersonaNotificarEntity> ListaPersonas
        {
            get { return listaPersonas; }
            set { listaPersonas = value; }
        }

        private string _rutaDocumento;

        public string RutaDocumento
        {
            get { return _rutaDocumento; }
            set { _rutaDocumento = value; }
        }

        private string _nombreProyecto;

        public string NombreProyecto
        {
            get { return _nombreProyecto; }
            set { _nombreProyecto = value; }
        }

        private string _correoAutAmbNotificacion;

        public string CorreoAutAmbNotificacion
        {
            get { return _correoAutAmbNotificacion; }
            set { _correoAutAmbNotificacion = value; }
        }


        private string _codigoExpediente;

        public string CodigoExpediente
        {
            get { return _codigoExpediente; }
            set { _codigoExpediente = value; }
        }

        private string _codigoAA;

        public string CodigoAA
        {
            get { return _codigoAA; }
            set { _codigoAA = value; }
        }

        private string _estadoActo;

        public string EstadoActo
        {
            get { return _estadoActo; }
            set { _estadoActo = value; }
        }

        // <summary>
        /// N�mero de Identificaci�n del Funcionario
        /// </summary>
        private string _identificacionUsuario;

        public string IdentificacionUsuario
        {
            get { return _identificacionUsuario; }
            set { _identificacionUsuario = value; }
        }

        public object[] getObject() {
            return new object[] { this.CodigoEntidadPublica, this.DependenciaEntidad, this.FechaActo,
                this.IdActoNotificacion, this.IdentificacionFuncionario, this.IdPlantilla, this.IdTipoActo, this.ListaPersonas,
                this.NombrePlantilla, this.NumeroActoAdministrativo, this.NumeroActoAdministrativoAsociado, this.NumeroSILPA,
                this.ParteResolutiva, this.ProcesoAdministracion, this.RequierePublicacion, this.RutaDocumento, this.SistemaEntidadPublica,
                this.TipoIdentificacionFuncionario,this.NombreProyecto, this.CorreoAutAmbNotificacion
            };
        }

        private bool? _aplicaRecursoReposicion;

        /// <summary>
        /// Indica si la notificaci�n aplica para reposici�n
        /// </summary>
        public bool? AplicaRecursoReposicion
        {
            get { return _aplicaRecursoReposicion; }
            set { _aplicaRecursoReposicion = value; }
        }


        private bool? _esComunicacion;

        /// <summary>
        /// Indica si la notificaci�n aplica para reposici�n
        /// </summary>
        public bool? EsComunicacion
        {
            get { return _esComunicacion; }
            set { _esComunicacion = value; }
        }


        private bool? _esCumplase;

        /// <summary>
        /// Indica si la notificaci�n aplica para reposici�n
        /// </summary>
        public bool? EsCumplase
        {
            get { return _esCumplase; }
            set { _esCumplase = value; }
        }

        private bool? _esNotificacion;

        /// <summary>
        /// Indica si la notificaci�n aplica para reposici�n
        /// </summary>
        public bool? EsNotificacion
        {
            get { return _esNotificacion; }
            set { _esNotificacion = value; }
        }

        private bool? _esNotificacionEdicto;

        /// <summary>
        /// Indica si la notificaci�n aplica para reposici�n
        /// </summary>
        public bool? EsNotificacionEdicto
        {
            get { return _esNotificacionEdicto; }
            set { _esNotificacionEdicto = value; }
        }

        private bool? _esNotificacionEstrado;

        /// <summary>
        /// Indica si la notificaci�n aplica para reposici�n
        /// </summary>
        public bool? EsNotificacionEstrado
        {
            get { return _esNotificacionEstrado; }
            set { _esNotificacionEstrado = value; }
        }

        #endregion
    }
}
