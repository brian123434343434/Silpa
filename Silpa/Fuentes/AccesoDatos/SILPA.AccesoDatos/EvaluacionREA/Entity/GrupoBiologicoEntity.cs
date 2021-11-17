using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class GrupoBiologicoEntity
    {
        #region Propiedades
        /// <summary>
        /// Identificador del grupo biologico
        /// </summary>
        public int GrupoBiologicoID { get; set; }
        /// <summary>
        /// nombre del grupo biologico
        /// </summary>
        public string GrupoBiologico { get; set; }
        #endregion Propiedades
    }
}
