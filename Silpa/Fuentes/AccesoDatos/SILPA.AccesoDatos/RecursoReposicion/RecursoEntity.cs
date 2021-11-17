using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.RecursoReposicion
{
    /// <summary>
    /// Clase para manejar los registros de Recursos de Reposición aplicados a Actos Administrativos
    /// </summary>
    public class RecursoEntity
    {
        private decimal idRecurso;
        
        private Notificacion.NotificacionEntity _acto;

        private string _descripcion;

        private RecursoEstadoEntity _estado;

        private decimal _idActoNotificacion;

        private List<RecursoDocumentoEntity> _listaDocumentos;
        
        /// <summary>
        /// Lista de Documentos asociados a este recurso
        /// </summary>
        public List<RecursoDocumentoEntity> ListaDocumentos
        {   
            get { return _listaDocumentos; }
            set { _listaDocumentos = value; }
        }

        /// <summary>
        /// Estado del Recurso
        /// </summary>
        public RecursoEstadoEntity Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        /// <summary>
        /// Descripción del Recurso
        /// </summary>
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        /// <summary>
        /// Identificador Del Recurso
        /// </summary>
        public decimal IDRecurso
        {
            get { return idRecurso; }
            set { idRecurso = value; }
        }
        private string _numeroIdentificacion;

        public string NumeroIdentificacion
        {
            get { return _numeroIdentificacion; }
            set { _numeroIdentificacion = value; }
        }


        /// <summary>
        /// Identificador del Acto administrativo al cual esta asociado este recurso
        /// </summary>
        public Notificacion.NotificacionEntity Acto
        {
            get { return _acto; }
            set { _acto = value; }
        }

                public decimal IdActoNotificacion
        {
            get { return _idActoNotificacion; }
            set { _idActoNotificacion = value; }
        }


    }
}
