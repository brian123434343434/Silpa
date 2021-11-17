using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Notificacion
{
    public class EnlaceDocumentoSilaEntity : EntidadSerializable
    {
        #region Propiedades

            /// <summary>
            /// Identificador del enlace creado
            /// </summary>
            public string EnlaceID { get; set; }

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
            /// identificador del documento en SILA
            /// </summary>
            public int DocumentoID { get; set; }

            /// <summary>
            /// Llave generada en el enlace
            /// </summary>
            public string Llave { get; set; }

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
