using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Encuestas.Contingencias.Entity
{
    [Serializable]
    public class EtapaProyectoContingenciasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la etapa del proyecto
            /// </summary>
            public int EtapaProyectoID { get; set; }

            /// <summary>
            /// Nombre de la etapa del proyecto
            /// </summary>
            public string EtapaProyecto { get; set; }

            /// <summary>
            /// Indica si la etapa se encuentra activa
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
