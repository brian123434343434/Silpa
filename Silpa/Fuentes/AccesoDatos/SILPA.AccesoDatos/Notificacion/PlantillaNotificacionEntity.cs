using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    [Serializable]
    public class PlantillaNotificacionEntity
    {        
        #region Propiedades

            /// <summary>
            /// Identificador del formato
            /// </summary>
            public int PlantillaID { get; set; }

            /// <summary>
            /// Identificador de la autoridad
            /// </summary>
            public int AutoridadID { get; set; }

            /// <summary>
            /// Nombre de la autoridad
            /// </summary>
            public string Autoridad { get; set; }

            /// <summary>
            /// Nombre de la plantilla
            /// </summary>
            public string Nombre { get; set; }

            /// <summary>
            /// Formato asociado a la plantilla
            /// </summary>
            public FormatoPlantillaNotificacionEntity Formato { get; set; }

            /// <summary>
            /// Encabezado de la plantilla
            /// </summary>
            public string Encabezado { get; set; }

            /// <summary>
            /// Cuerpo de la plantilla
            /// </summary>
            public string Cuerpo { get; set; }

            /// <summary>
            /// Mensaje Pie de Firma de la plantilla
            /// </summary>
            public string PieFirma { get; set; }

            /// <summary>
            /// Pie de la plantilla
            /// </summary>
            public string Pie { get; set; }

            /// <summary>
            /// Indica si la plantilla se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

        #endregion

    }
}
