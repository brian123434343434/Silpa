using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class ProyectoLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del proyecto
            /// </summary>
            public int ProyectoID { get; set; }

            /// <summary>
            /// Proyecto
            /// </summary>
            public string Proyecto { get; set; }

            /// <summary>
            /// Indica si el proyecto se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

        #endregion

    }
}
