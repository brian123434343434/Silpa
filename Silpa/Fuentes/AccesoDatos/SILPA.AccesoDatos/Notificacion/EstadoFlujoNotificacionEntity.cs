using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    [Serializable]
    public class EstadoFlujoNotificacionEntity
    {        
        #region Propiedades

            /// <summary>
            /// Identificador del estado flujo
            /// </summary>
            public int EstadoFlujoID { get; set; }

            /// <summary>
            /// Identificador del estado
            /// </summary>
            public int EstadoID { get; set; }

            /// <summary>
            /// Identificador del flujo
            /// </summary>
            public int FlujoID { get; set; }

            /// <summary>
            /// Nombre del estado
            /// </summary>
            public string Estado { get; set; }

            /// <summary>
            /// Descripcion del estado
            /// </summary>
            public string EstadoDescripcion { get; set; }

            /// <summary>
            /// Descripción del estado en el flujo
            /// </summary>
            public string Descripcion { get; set; }

            /// <summary>
            /// Número de días de vencimiento
            /// </summary>
            public int DiasVencimiento { get; set; }

            /// <summary>
            /// Indica si se genera plantilla
            /// </summary>
            public bool GeneraPlantilla { get; set; }

            /// <summary>
            /// Identificador de la plantilla
            /// </summary>
            public int PlantillaID { get; set; }

            /// <summary>
            /// Indica si se perite adjuntar un documento al estado
            /// </summary>
            public bool DocumentoAdicional { get; set; }

            /// <summary>
            /// Indica si se envía correo de avance manual
            /// </summary>
            public bool EnviaCorreoAvanceManual { get; set; }

            /// <summary>
            /// Indica si se envía notificación fisica
            /// </summary>
            public bool EnviaNotificacionFisica { get; set; }

            /// <summary>
            /// Indica si se adjunta anexo
            /// </summary>
            public bool AnexaAdjunto { get; set; }

            /// <summary>
            /// Indica si se envía correo de avance
            /// </summary>
            public bool EnviaCorreoAvance { get; set; }

            /// <summary>
            /// Texto de correo de avance
            /// </summary>
            public string TextoCorreoAvance { get; set; }

            /// <summary>
            /// identificador del tipo de anexo a utilizar
            /// </summary>
            public int TipoAnexoCorreoID { get; set; }

            /// <summary>
            /// Indica si se permite anexar el acto administrativo
            /// </summary>
            public bool PermitiAnexarActoAdministrativo { get; set; }

            /// <summary>
            /// Indica si se permite anexar el conceptos relacionados al acto administrativo
            /// </summary>
            public bool PermitiAnexarConceptosActoAdministrativo { get; set; }

            /// <summary>
            /// Indica si publica el estado
            /// </summary>
            public bool PublicarEstado { get; set; }

            /// <summary>
            /// Indica si publica plantilla
            /// </summary>
            public bool PublicarPlantilla { get; set; }

            /// <summary>
            /// Indica si publica adjunto
            /// </summary>
            public bool PublicarAdjunto { get; set; }

            /// <summary>
            /// Solicitar Información de persona a notificar
            /// </summary>
            public bool SolicitarInformacionPersonaNotificar { get; set; }

            /// <summary>
            /// Solicitar Referencia Recepción Notificación
            /// </summary>
            public bool SolicitarReferenciaRecepcionNotificacion { get; set; }

            /// <summary>
            /// Indica si la referencia es obligatoria
            /// </summary>
            public bool ReferenciaRecepcionNotificacionObligatoria { get; set; }

            /// <summary>
            /// Indica si es un estado de espera
            /// </summary>
            public bool EsEstadoEspera { get; set; }

            /// <summary>
            /// Indica si es notificacion
            /// </summary>
            public bool EsNotificacion { get; set; }

            /// <summary>
            /// Indica si es citacion
            /// </summary>
            public bool EsCitacion { get; set; }

            /// <summary>
            /// Indica si es ejecutoria
            /// </summary>
            public bool EsEjecutoria { get; set; }

            /// <summary>
            /// Indica si es anulación
            /// </summary>
            public bool EsAnulacion { get; set; }

            /// <summary>
            /// Indica si es final de publicidad
            /// </summary>
            public bool EsFinalPublicidad { get; set; }

            /// <summary>
            /// Indica si es edicto
            /// </summary>
            public bool EsEdicto { get; set; }

            /// <summary>
            /// Indica si genera recurso de reposición
            /// </summary>
            public bool GeneraRecurso { get; set; }

            /// <summary>
            /// Indica si es un estado de aceptación de notificación
            /// </summary>
            public bool EsAceptacionNotificacion { get; set; }

            /// <summary>
            /// Indica si es un estado de rechazo de notificacion
            /// </summary>
            public bool EsRechazoNotificacion { get; set; }

            /// <summary>
            /// Indica si es un estado de aceptación de citacion
            /// </summary>
            public bool EsAceptacionCitacion { get; set; }

            /// <summary>
            /// Indica si es un estado de rechazo de citacion
            /// </summary>
            public bool EsRechazoCitacion { get; set; }

            /// <summary>
            /// Identificador del estado del cual se depende
            /// </summary>
            public int EstadoDependienteID { get; set; }
   

            /// <summary>
            /// Indica si el estado se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

        #endregion

    }
}
