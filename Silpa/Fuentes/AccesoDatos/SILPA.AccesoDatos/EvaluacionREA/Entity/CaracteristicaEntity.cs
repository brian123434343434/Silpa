using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class CaracteristicaEntity
    {
        #region Propiedades
        /// <summary>
        /// Indentificador de la caracteristica
        /// </summary>
        public int CaracteristicaID { get; set; }
        /// <summary>
        /// nombre de la carateristica
        /// </summary>
        public string Caractaristica { get; set; }
        #endregion
    }
}
