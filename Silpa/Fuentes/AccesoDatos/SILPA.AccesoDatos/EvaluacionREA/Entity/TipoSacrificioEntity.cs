using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class TipoSacrificioEntity
    {
        #region propiedades
        /// <summary>
        /// Indentificador del tipo de sacrificio
        /// </summary>
        public int TipoSacrificioID { get; set; }
        /// <summary>
        /// nombre del tipo de sacrificio
        /// </summary>
        public string TipoSacrificio { get; set; }
        /// <summary>
        /// nomenclaruta del sacrificio
        /// </summary>
        public string Nomenclatura { get; set; }
        #endregion
    }
}
