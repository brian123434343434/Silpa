using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Usuario
{
    public class EnlaceActivacionUsuarioEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del enlace
            /// </summary>
            public long EnlaceID { get; set; }

            /// <summary>
            /// Identificador de la persona a la cual pertenece el enlace
            /// </summary>
            public long PersonaID { get; set; }

            /// <summary>
            /// Numero de identificación de la persona a la cual pertenece el enlace
            /// </summary>
            public string NumeroIdentificacion { get; set; }

            /// <summary>
            /// Llave enviada en el enlace
            /// </summary>
            public string Llave { get; set; }

            /// <summary>
            /// Indica si se el enlace se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

            /// <summary>
            /// Fecha de creación del enlace
            /// </summary>
            public DateTime FechaCreacion { get; set; }

            /// <summary>
            /// Fecha de vigencia del enlace
            /// </summary>
            public DateTime FechaVigencia { get; set; }

            /// <summary>
            /// Fecha de utilización del enlace
            /// </summary>
            public DateTime FechaUtilizacion { get; set; }

        #endregion

    }
}
