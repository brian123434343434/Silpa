using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class PermisoLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del permiso de liquidación
            /// </summary>
            public int PermisoID { get; set; }

            /// <summary>
            /// Permiso de liquidación
            /// </summary>
            public string Permiso { get; set; }


            /// <summary>
            /// Indica si el permiso se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
