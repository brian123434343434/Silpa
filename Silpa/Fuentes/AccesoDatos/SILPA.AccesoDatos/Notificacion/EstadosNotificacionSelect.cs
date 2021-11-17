using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Notificacion
{
    /*
        Clase que contiene los datos de la consulta de los estados de notificación por persona
     */
    public class EstadosNotificacionSelect:EntidadSerializable
    {

        public EstadosNotificacionSelect() { }

        private long _id; 
        private long _idSolicitud; 
        private int _idTipoActoAdministrativo;//TIPO_ACTO_ADMINISTRATIVO	
        private string _tipoActoAdministrativo; // Nombre del tipo de acto administrativo - desde gen_tipo_documento
        private string _idProcesoNotificacion;//ID_PROCESO_NOTIFICACION	
        private string _archivo;//ARCHIVO	
        private long _idActoNotificacion;//ID_ACTO_NOTIFICACION	
        private string _numeroSilpa;//NOT_NUMERO_SILPA	 
        private string _expediente;//EXPEDIENTE	
        private DateTime _fechaActo;//NOT_FECHA_ACTO
        private string _numeroActoAdministrativo;//NOT_NUMERO_ACTO_ADMINISTRATIVO	
        private string _usuarioNotificar;//USUARIO_NOTIFICAR	
        private long  _idApplicationUser; //ID_APPLICATION_USER 
        private string _correoElectronico; //PER_CORREO_ELECTRONICO	
        private string _numeroIdentificacionUsuario; // NPE_NUMERO_IDENTIFICACION	
        private int _idEstadoNotificado;
        private string _estadoNotificado;
        private DateTime _fechaEstadoNotificado;
        private int _diasVencimiento;
        private string _validarDiasVencimiento;
        private bool _estadoCambioPDI;
        private string _sistema;
        private decimal _idPersonaNotificar;
        private string _fechaEnvioCorreo;// fecha de envío del correo 
        private string _nombreArchivo;// fecha de envío del correo 
        private string _nombreAutoridad;// fecha de envío del correo 
        private int _idAutoridad;// idntificador de la Autoridad Ambiental
        private int _tieneActividadSiguiente;
        private bool _mostraInformacion;
        private string _archivosAdjuntos;
        private bool _esnotificacionElectronica;
        private bool _esnotificacionElectronica_AA;
        private bool _esnotificacionElectronica_EXP;
        private bool _actoEsnotificacionElectronica_EXP;
        private int _idFlujoNotElec;
        private bool _esModificable;
        private bool _estadoActual;

        // propiedades
        
        public long ID { get { return _id; } set { _id = value; } }
        public long IdSolicitud { get { return _idSolicitud; } set { _idSolicitud = value; } }
        public int IdTipoActoAdministrativo { get { return _idTipoActoAdministrativo; } set { _idTipoActoAdministrativo = value; } }
        public string TipoActoAdministrativo { get { return _tipoActoAdministrativo; } set { _tipoActoAdministrativo = value; } }
        public string IdProcesoNotificacion { get { return _idProcesoNotificacion; } set { _idProcesoNotificacion = value; } }
        public string Archivo { get { return _archivo; } set { _archivo = value; } }
        public long IdActoNotificacion { get { return _idActoNotificacion; } set { _idActoNotificacion = value; } }
        public string NumeroSilpa { get { return _numeroSilpa; } set { _numeroSilpa = value; } }
        public string Expediente { get { return _expediente; } set { _expediente = value; } }
        public DateTime FechaActo { get { return _fechaActo; } set { _fechaActo = value; } }
        public string NumeroActoAdministrativo { get { return _numeroActoAdministrativo; } set { _numeroActoAdministrativo = value; } }
        public string UsuarioNotificar { get { return _usuarioNotificar; } set { _usuarioNotificar = value; } }
        public long IdApplicationUser { get { return _idApplicationUser; } set { _idApplicationUser = value; } }
        public string CorreoElectronico { get { return _correoElectronico; } set { _correoElectronico = value; } }
        public string NumeroIdentificacionUsuario { get { return _numeroIdentificacionUsuario; } set { _numeroIdentificacionUsuario = value; } }
        public int IdEstadoNotificado { get { return _idEstadoNotificado; } set { _idEstadoNotificado = value; } }
        public string EstadoNotificado { get { return _estadoNotificado; } set { _estadoNotificado = value; } }
        public DateTime FechaEstadoNotificado { get { return _fechaEstadoNotificado; } set { _fechaEstadoNotificado = value; } }
        public int DiasVencimiento { get { return _diasVencimiento; } set { _diasVencimiento = value; } }
        public string ValidarDiasVencimiento { get { return _validarDiasVencimiento; } set { _validarDiasVencimiento = value; } }
        public bool EstadoCambioPDI { get { return _estadoCambioPDI; } set { _estadoCambioPDI = value; } }
        public string Sistema { get { return _sistema; } set { _sistema = value; } }
        public decimal IdPersonaNotificar { get { return _idPersonaNotificar; } set { _idPersonaNotificar = value; } }
        public string FechaEnvioCorreo { get { return _fechaEnvioCorreo; } set { _fechaEnvioCorreo = value; } }
        public int TieneActividadSiguiente { get { return _tieneActividadSiguiente; } set { _tieneActividadSiguiente = value; } }
        public bool MostrarInfomacion { get { return _mostraInformacion; } set { _mostraInformacion = value; } }
        public string ArchivosAdjuntos { get { return _archivosAdjuntos; } set { _archivosAdjuntos = value; } }
        //JACOSTA 20120829. SE AGREGA LA PROPIEDAD DE ESNOTIFICACIONELECTRONICA
        public bool EsNotificacionElectronica { get { return _esnotificacionElectronica; } set { _esnotificacionElectronica = value; } }
     
        public bool EsNotificacionElectronica_AA { get { return _esnotificacionElectronica_AA; } set { _esnotificacionElectronica_AA = value; } }
        public bool EsNotificacionElectronica_EXP { get { return _esnotificacionElectronica_EXP; } set { _esnotificacionElectronica_EXP = value; } }
        public bool ActoEsNotificacionElectronica_EXP { get { return _actoEsnotificacionElectronica_EXP; } set { _actoEsnotificacionElectronica_EXP = value; } }
     
        //hava: 21-dic-10
        /// <summary>
        ///  Nombre de la autoridad ambiental
        /// </summary>
        public string NombreAutoridad { get { return _nombreAutoridad; } set { _nombreAutoridad = value; } }
        
        /// <summary>
        /// Identificador de la autoridad ambiental
        /// </summary>
        public int IdAutoridad { get { return _idAutoridad; } set { _idAutoridad = value; } }
        

        public string  NombreArchivo 
        {
            get 
            {
                this._nombreArchivo = System.IO.Path.GetFileName(this._archivo);
                return _nombreArchivo; 
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public string DatoProvienePDI
        {
            get
            {
                if (_estadoCambioPDI == true) { return "SI"; }
                else { return "NO"; }
            }
        }

        /// <summary>
        /// Flujo al cual pertenece
        /// </summary>
        public int IdFlujoNotElec { get { return _idFlujoNotElec; } set { _idFlujoNotElec = value; } }


        /// <summary>
        /// Indica si el estado esmodificable
        /// </summary>
        public bool EsModificable { get { return _esModificable; } set { _esModificable = value; } }

        /// <summary>
        /// Indica si el estado esmodificable
        /// </summary>
        public bool EstadoActual { get { return _estadoActual; } set { _estadoActual = value; } }
    }
}
