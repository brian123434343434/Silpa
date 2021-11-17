using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Encuestas.Encuesta.Entity
{
    [Serializable]
    public class FormularioEncuestasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del formulario
            /// </summary>
            public int FormularioID { get; set; }

            /// <summary>
            /// Nombre del formulario
            /// </summary>
            public string Formulario { get; set; }

            /// <summary>
            /// Secciones que componen el formulario
            /// </summary>
            public List<SeccionEncuestasEntity> Secciones { get; set; }

            /// <summary>
            /// Indica si el formulario se encuentra activo
            /// </summary>
            public bool Activo { get; set; }

        #endregion
        
    }
}
