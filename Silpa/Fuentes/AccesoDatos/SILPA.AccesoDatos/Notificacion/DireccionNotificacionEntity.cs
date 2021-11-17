using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    [Serializable]
    public class DireccionNotificacionEntity
    {        
        #region Propiedades

            /// <summary>
            /// Identificador del estado
            /// </summary>
            public long EstadoPersonaActoID { get; set; }

            /// <summary>
            /// Identificador de la persona
            /// </summary>
            public long PersonaID { get; set; }

            /// <summary>
            /// Identificador direccion
            /// </summary>
            public long DireccionID { get; set; }

            /// <summary>
            /// Inidca a quien pertenece
            /// </summary>
            public string Pertenece { get; set; }

            /// <summary>
            /// Nombre del departamento
            /// </summary>
            public string Departamento { get; set; }

            /// <summary>
            /// Nombre del municipio
            /// </summary>
            public string Municipio { get; set; }

            /// <summary>
            /// Direccion
            /// </summary>
            public string Direccion { get; set; }

        #endregion

    }
}
