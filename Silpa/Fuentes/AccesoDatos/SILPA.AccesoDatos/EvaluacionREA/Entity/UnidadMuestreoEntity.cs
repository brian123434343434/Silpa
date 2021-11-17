using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class UnidadMuestreoEntity
    {
        #region Propiedades
        /// <summary>
        /// Indentificador de la unidad de muestreo
        /// </summary>
        public int UnidadMuestreoID { get; set; }
        /// <summary>
        /// Nombre de la unidad de muestreo
        /// </summary>
        public string UnidadMuestreo { get; set; }
        #endregion
    }
}
