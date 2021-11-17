using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Encuestas.Contingencias.Entity
{
    [Serializable]
    public class NivelEmergenciaContingenciasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del tipo de solicitud
            /// </summary>
            public int NivelEmergenciaID { get; set; }

            /// <summary>
            /// Nombre del tipo de solicitud
            /// </summary>
            public string NivelEmergencia { get; set; }

            /// <summary>
            /// Indica si la etapa se encuentra activa
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
