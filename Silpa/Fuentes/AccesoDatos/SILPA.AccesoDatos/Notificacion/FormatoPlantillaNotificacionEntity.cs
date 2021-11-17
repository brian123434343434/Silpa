using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    [Serializable]
    public class FormatoPlantillaNotificacionEntity
    {        
        #region Propiedades

            /// <summary>
            /// Identificador del formato
            /// </summary>
            public int FormatoID { get; set; }

            /// <summary>
            /// Descripcion del formato
            /// </summary>
            public string Nombre { get; set; }

            /// <summary>
            /// Nombre Clase Formato
            /// </summary>
            public string Formato { get; set; }

            /// <summary>
            /// Indica si el formato se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

        #endregion

    }
}
