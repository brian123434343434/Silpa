using System;

namespace SILPA.LogicaNegocio.Recurso
{

    public class RecursoReposicionPresentacion
    {
        /// <summary>
        /// Identificador del Acto_notificación
        /// </summary>
        private decimal _idActoNotificacion;

        public decimal IdActoNotificacion
        {
            get { return _idActoNotificacion; }
            set { _idActoNotificacion = value; }
        }

        /// <summary>
        /// Número SILPA al cuál se encuentra asociado este acto
        /// </summary>
        private string _numeroSILPA;

        public string NumeroSILPA
        {
            get { return _numeroSILPA; }
            set { _numeroSILPA = value; }
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
        /// Número del Acto Administrativo de Esta Notificación
        /// </summary>
        private string _numeroActoAdministrativo;

        public string NumeroActoAdministrativo
        {
            get { return _numeroActoAdministrativo; }
            set { _numeroActoAdministrativo = value; }
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

        private DateTime? _fechaNotificacion;

        public DateTime? FechaNotificacion
        {
            get { return _fechaNotificacion; }
            set { _fechaNotificacion = value; }
        }

        private string _campo_borrar;

        public string CampoBorrar
        {
            get { return _campo_borrar; }
            set { _campo_borrar = value; }
        }


        private string _rutaDocumento;

        public string RutaDocumento
        {
            get { return _rutaDocumento; }
            set { _rutaDocumento = value; }
        }
    }
}