using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Encuestas.Contingencias.Entity
{
    [Serializable]
    public class TipoSolicitudContingenciasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del tipo de solicitud
            /// </summary>
            public int TipoSolicitudID { get; set; }

            /// <summary>
            /// Nombre del tipo de solicitud
            /// </summary>
            public string TipoSolicitud { get; set; }

            /// <summary>
            /// Indica si la etapa se encuentra activa
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
