using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    [Serializable]
    public class PersonaNotificarEntity
    {
        #region Atributos
        /// <summary>
        /// Identificador de la Persona a Notificar
        /// </summary>
        private decimal _id;

        public decimal Id
        {
            get { return _id; }
            set { _id = value; }
        }


        /// <summary>
        /// Identificador del Acto y Proceso de Notificación (en SILPA)
        /// </summary>

        private bool _esNotificacionElectronica;

        public bool EsNotificacionElectronica
        {
            get { return _esNotificacionElectronica; }
            set { _esNotificacionElectronica = value; }
        }

        /// <summary>
        /// Identificador del Acto y Proceso de Notificación (en SILPA)
        /// </summary>

        private decimal _idActoNotificar;

        public decimal IdActoNotificar
        {
            get { return _idActoNotificar;  }
            set { _idActoNotificar = value; }
        }
        /*private NotificacionEntity _idActoNotificar;

        public NotificacionEntity IdActoNotificar
        {
            get { return _idActoNotificar; }
            set { _idActoNotificar = value; }
        }*/

        /// <summary>
        /// Número de Identificación de la Persona
        /// </summary>
        private string _numeroIdentificacion;

        public string NumeroIdentificacion
        {
            get { return _numeroIdentificacion; }
            set { _numeroIdentificacion = value; }
        }

        /// <summary>
        /// Tipo de Identificación de la Persona
        /// </summary>
        /// <remarks>La lista completa se encuentra en GEL-XML</remarks>
        private TipoIdentificacionEntity _tipoIdentificacion;

        public TipoIdentificacionEntity TipoIdentificacion
        {
            get { return _tipoIdentificacion; }
            set { _tipoIdentificacion = value; }
        }


        /// <summary>
        /// Tipo de persona
        /// </summary>
        /// <remarks>En general es Natural o Jurídica (valores aceptados para GEL-XML)</remarks>
        private TipoPersonaEntity _tipoPersona;

        public TipoPersonaEntity TipoPersona
        {
            get { return _tipoPersona; }
            set { _tipoPersona = value; }
        }

        /// <summary>
        /// Número del NIT, es opcional (para el caso de que la persona no sea Jurídica)
        /// </summary>
        private int _numeroNIT;

        public int NumeroNIT
        {
            get { return _numeroNIT; }
            set { _numeroNIT = value; }
        }

        /// <summary>
        /// Dígito de verificación, en caso de haber escrito el NIT
        /// </summary>
        private int _digitoVerificacionNIT;

        public int DigitoVerificacionNIT
        {
            get { return _digitoVerificacionNIT; }
            set { _digitoVerificacionNIT = value; }
        }

        /// <summary>
        /// Primer Apellido de la Persona
        /// </summary>
        private string _primerApellido;

        public string PrimerApellido
        {
            get { return _primerApellido; }
            set { _primerApellido = value; }
        }

        /// <summary>
        /// Segundo Apellido de la Persona (es opcional)
        /// </summary>
        private string _segundoApellido;

        public string SegundoApellido
        {
            get { return _segundoApellido; }
            set { _segundoApellido = value; }
        }

        /// <summary>
        /// Primer Nombre de la Persona
        /// </summary>
        private string _primerNombre;

        public string PrimerNombre
        {
            get { return _primerNombre; }
            set { _primerNombre = value; }
        }

        /// <summary>
        /// Segundo Nombre de la Persona
        /// </summary>
        private string _segundoNombre;

        public string SegundoNombre
        {
            get { return _segundoNombre; }
            set { _segundoNombre = value; }
        }

        /// <summary>
        /// Razón Social (opcional)
        /// </summary>
        private string _razonSocial;

        public string RazonSocial
        {
            get { return _razonSocial; }
            set { _razonSocial = value; }
        }

        /// <summary>
        /// Determina el estado actual de Notificado
        /// </summary>
        private EstadoNotificacionEntity _estadoNotificado;

        public EstadoNotificacionEntity EstadoNotificado
        {
            get { return _estadoNotificado; }
            set { _estadoNotificado = value; }
        }

        /// <summary>
        /// Aqui se registra el consecutivo de notificacion
        /// </summary>
        private string _consecutivoNotificacion;

        public string ConsecutivoNotificacion
        {
            get { return _consecutivoNotificacion; }
            set { _consecutivoNotificacion = value; }
        }

        /// <summary>
        /// Año de Notificaicón
        /// </summary>
        private string _anoNotificacion;

        public string AnoNotificacion
        {
            get { return _anoNotificacion; }
            set { _anoNotificacion = value; }
        }

        private DateTime? _fechaNotificado;

        public DateTime? FechaNotificado
        {
            get { return _fechaNotificado; }
            set { _fechaNotificado = value; }
        }

        //22-sept-2010 - aegb
        private DateTime? _fechaEstadoNotificado;

        public DateTime? FechaEstadoNotificado
        {
            get { return _fechaEstadoNotificado; }
            set { _fechaEstadoNotificado = value; }
        }

        //Permite conocer si el estado es el ultimo
        public bool EstadoActualNotificado { get; set; }


        /// <summary>
        /// Se registra el flujo de notificación que debe realizar
        /// </summary>
        private int _flujoNotificacionId;

        public int FlujoNotificacionId
        {
            get { return _flujoNotificacionId; }
            set { _flujoNotificacionId = value; }
        }
        
        /// <summary>
        /// Se registra el tipo de notificación que presenta la persona
        /// </summary>
        private int _tipoNotificacionId;

        public int TipoNotificacionId
        {
            get { return _tipoNotificacionId; }
            set { _tipoNotificacionId = value; }
        }

        /// <summary>
        /// Estado de persona a notificar
        /// </summary>
        private int _estadoPersonaId;

        public int EstadoPersonaID
        {
            get { return _estadoPersonaId; }
            set { _estadoPersonaId = value; }
        }

        #endregion
    }
}
