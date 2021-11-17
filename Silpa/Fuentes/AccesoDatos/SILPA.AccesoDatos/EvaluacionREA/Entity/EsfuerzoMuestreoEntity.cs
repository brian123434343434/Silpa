using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class EsfuerzoMuestreoEntity
    {
        #region Propiedades
        /// <summary>
        /// Indentificador de esfuerzo de muestreo
        /// </summary>
        public int EsfuerzoMuestreoID { get; set; }
        /// <summary>
        /// nombre de esfuerzo muestreo
        /// </summary>
        public string EsfuerzoMuestreo { get; set; }
        #endregion
    }
}
