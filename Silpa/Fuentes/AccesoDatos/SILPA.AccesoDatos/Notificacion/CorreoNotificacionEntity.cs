using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    [Serializable]
    public class CorreoNotificacionEntity
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
            /// Direeccion de correo
            /// </summary>
            public string Correo { get; set; }            

            /// <summary>
            /// Nombre del grupo
            /// </summary>
            public string Grupo { get; set; }

            /// <summary>
            /// Texto del correo
            /// </summary>
            public string Texto { get; set; }

            /// <summary>
            /// Fecha de Env�o del correo
            /// </summary>
            public DateTime FechaEnv�o { get; set; }
    

        #endregion

    }
}
