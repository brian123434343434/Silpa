using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    [Serializable]
    public class MarcaPlantillaNotificacionEntity
    {        
        #region Propiedades

            /// <summary>
            /// Identificador de la marca
            /// </summary>
            public int MarcaID { get; set; }

            /// <summary>
            /// Marca
            /// </summary>
            public string Marca { get; set; }

            /// <summary>
            /// Tabla en la cual se encuentra la información
            /// </summary>
            public string Tabla { get; set; }

            /// <summary>
            /// CAmpo en la tabla en la cual se encuentra la información
            /// </summary>
            public string Campo { get; set; }

            /// <summary>
            /// Indica si la marca se encuentra activa
            /// </summary>
            public bool Activo { get; set; }

        #endregion

    }
}
