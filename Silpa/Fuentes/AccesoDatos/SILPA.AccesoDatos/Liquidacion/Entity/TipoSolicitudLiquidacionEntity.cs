using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class TipoSolicitudLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del tipo de solicitud de liquidacion
            /// </summary>
            public int TipoSolicitudID { get; set; }

            /// <summary>
            /// Tipo de solicitud de liquidación
            /// </summary>
            public string TipoSolicitud { get; set; }

            /// <summary>
            /// Identificador del caso del proceso en vital
            /// </summary>
            public int CasoProcesoID { get; set; }

            /// <summary>
            /// Identificador del formulario en vital al cual se relaciona para registro
            /// </summary>
            public int FormularioID { get; set; }

            /// <summary>
            /// INdica si se encuentra activo el tipo de liquidación
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
