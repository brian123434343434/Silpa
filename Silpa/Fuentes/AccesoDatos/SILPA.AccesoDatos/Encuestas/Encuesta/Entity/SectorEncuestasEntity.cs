using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Encuestas.Encuesta.Entity
{
    [Serializable]
    public class SectorEncuestasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del sector de liquidación
            /// </summary>
            public int SectorID { get; set; }

            /// <summary>
            /// Sector de liquidación
            /// </summary>
            public string Sector { get; set; }

            /// <summary>
            /// Identificador del sector en SILA
            /// </summary>
            public int SectorSilaID { get; set; }

            /// <summary>
            /// Indica si el sector se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
