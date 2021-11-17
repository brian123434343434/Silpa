using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    public class EnlaceDocumentoEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del enlace creado
            /// </summary>
            public string EnlaceID { get; set; }

            /// <summary>
            /// Identificador del enlace creado
            /// </summary>
            public int AutoridadID { get; set; }

            /// <summary>
            /// Identificador del acto de notificación
            /// </summary>
            public long ActoNotificacionID { get; set; }

            /// <summary>
            /// Identificador de la persona
            /// </summary>
            public long PersonaID { get; set; }

            /// <summary>
            /// identificador del estado
            /// </summary>
            public long EstadoPersonaID { get; set; }

            /// <summary>
            /// Llave enviada en el enlace
            /// </summary>
            public string LlaveEnviada { get; set; }

            /// <summary>
            /// Indica si se incluye el acto administrativo
            /// </summary>
            public bool IncluirActoAdministrativo { get; set; }

            /// <summary>
            /// Indica si se incluye los conceptos del acto administrativo
            /// </summary>
            public bool IncluirConceptosActoAdministrativo { get; set; }

            /// <summary>
            /// Fecha de creación del enlace
            /// </summary>
            public DateTime FechaCreacion { get; set; }

            /// <summary>
            /// Fecha devigencia del enlace
            /// </summary>
            public DateTime FechaVigencia { get; set; }

        #endregion

    }
}
