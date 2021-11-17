using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class ActividadLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la actividad
            /// </summary>
            public int ActividadID { get; set; }

            /// <summary>
            /// Actividad
            /// </summary>
            public string Actividad { get; set; }

        #endregion

    }
}
