using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    [Serializable]
    public class CargueEnvioNotificacionEntity
    {        

        #region Propiedades

            /// <summary>
            /// Identificador de la firma
            /// </summary>
            public int CargueID { get; set; }

            /// <summary>
            /// Identificador de la autoridad
            /// </summary>
            public int AutoridadID { get; set; }
            
            /// <summary>
            /// Nombre de la autoridad
            /// </summary>
            public string Autoridad { get; set; }

            /// <summary>
            /// Usuario que realizo el cargue
            /// </summary>
            public int UsuarioId { get; set; }

            /// <summary>
            /// Nombre del usuario que realizo el cargue
            /// </summary>
            public string NombreUsuario { get; set; }

            /// <summary>
            /// Fecha en la cual se realizo el cargue
            /// </summary>
            public DateTime FechaCargue { get; set; }
        
            /// <summary>
            /// Descripcion del cargue
            /// </summary>
            public string Descripcion { get; set; }

            /// <summary>
            /// Número de registros cargados
            /// </summary>
            public int RegistrosCargados { get; set; }

            /// <summary>
            /// Número de registros relacionados
            /// </summary>
            public int RegistrosRelacionados { get; set; }

        #endregion

    }
}
