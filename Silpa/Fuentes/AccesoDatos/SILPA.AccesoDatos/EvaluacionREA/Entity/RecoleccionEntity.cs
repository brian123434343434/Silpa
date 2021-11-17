using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class RecoleccionEntity
    {
        #region Propiedades
        /// <summary>
        /// Identificador de la recoleccion
        /// </summary>
        public int RecoleccionID { get; set; }
        /// <summary>
        /// Nombre de la recoleccion
        /// </summary>
        public string Recoleccion { get; set; }
        #endregion
    }
}
