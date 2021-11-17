using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Encuestas.Encuesta.Entity
{
    [Serializable]
    public class AutoridadAmbientalEncuestasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del sector de liquidación
            /// </summary>
            public int AutoridadID { get; set; }

            /// <summary>
            /// Sector de liquidación
            /// </summary>
            public string Autoridad { get; set; }

        #endregion
        
    }
}
