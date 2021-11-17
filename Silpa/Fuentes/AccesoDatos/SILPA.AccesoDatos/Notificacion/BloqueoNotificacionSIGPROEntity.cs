using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    public class BloqueoNotificacionSIGPROEntity
    {
        #region Propiedades

            /// <summary>
            /// Indica si se encuentra bloqueada la persona
            /// </summary>
            public bool Bloqueado { get; set; }


            /// <summary>
            /// Tipo de bloqueo presentado
            /// </summary>
            public int TipoBloqueoID { get; set; }

            
            /// <summary>
            /// Descripción del tipo de bloqueo
            /// </summary>
            public string TipoBloqueo { get; set; }

        #endregion
    }
}
