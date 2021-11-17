using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.Servicios.Generico.Entidades
{
    public class PersonaRespuestaEntity : EntidadSerializable
    {
        #region Propiedades

            /// <summary>
            /// Código de respuesta
            /// </summary>
            public string Codigo { get; set; }

            /// <summary>
            /// Mensaje de respuesta
            /// </summary>
            public string Mensaje { get; set; }

        #endregion
    }
}
