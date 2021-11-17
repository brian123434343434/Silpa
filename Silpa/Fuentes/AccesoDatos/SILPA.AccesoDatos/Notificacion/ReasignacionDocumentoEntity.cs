using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    public class ReasignacionDocumentoEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la reasignación realizada
            /// </summary>
            public int ReasignacionID { get; set; }

            /// <summary>
            /// Identificador de la tarea reasignada
            /// </summary>
            public int TareaID { get; set; }

            /// <summary>
            /// Identificador de la tarea en la cual se encontraba la tarea
            /// </summary>
            public int ActividadOrigenID { get; set; }

            /// <summary>
            /// Identificador del expediente en el cual se encontraba la tarea
            /// </summary>
            public int ExpedienteOrigenID { get; set; }

            /// <summary>
            /// Expediente en el cual se encontraba la tarea
            /// </summary>
            public string ExpedienteOrigen { get; set; }

            /// <summary>
            /// Identificador de la etapa en el cual se encontraba la tarea
            /// </summary>
            public int EtapaOrigenID { get; set; }

            /// <summary>
            /// Identificador de la actividad a la cual se reasigno la tarea
            /// </summary>
            public int ActividadDestinoID { get; set; }

            /// <summary>
            /// Identificador del expediente al cual se reasigno la tarea
            /// </summary>
            public int ExpedienteDestinoID { get; set; }

            /// <summary>
            /// Expediente al cual se reasigno la tarea
            /// </summary>
            public string ExpedienteDestino { get; set; }

            /// <summary>
            /// Identificador de la etapa a la cual se reasigno la tarea
            /// </summary>
            public int EtapaDestinoID { get; set; }

            /// <summary>
            /// Fecha en la cual se realizo la reasignación
            /// </summary>
            public DateTime FechaReasignacion { get; set; }

        #endregion
    }
}
