using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable] 
    public class PermisoSolicitudLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del permiso
            /// </summary>
            public int PermisoID { get; set; }

            /// <summary>
            /// Identificador de la solicitud
            /// </summary>
            public int SolicitudLiquidacionID { get; set; }

            /// <summary>
            /// Permiso
            /// </summary>
            public PermisoLiquidacionEntity Permiso { get; set; }

            /// <summary>
            /// Identificador de la autoridad ambiental a la cual pertenece el permiso
            /// </summary>
            public int AutoridadID { get; set; }

            /// <summary>
            /// Autoridad ambiental a la cual pertenece el permiso
            /// </summary>
            public string Autoridad { get; set; }

            /// <summary>
            /// Número de permisos solicitados
            /// </summary>
            public int NumeroPermisos { get; set; }


        #endregion
        
    }
}
