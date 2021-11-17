using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class ClasificacionInformacionGeneralIdentity : EntidadSerializable
    {
        #region Propiedades


            /// <summary>
            /// Identificador de la clasificiación de información
            /// </summary>
            public int ClasificacionInformacionID { get; set; }

            /// <summary>
            /// Descripción de la clasificación de información
            /// </summary>
            public string Descripcion { get; set; }

            /// <summary>
            /// Indica si la clasificación se encuentra activa
            /// </summary>
            public bool Activo { get; set; }


        #endregion
    }
}
