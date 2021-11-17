using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class TipoFijacionPreservacionEntity
    {
        #region propiedades
        /// <summary>
        /// Identificador tipo fijacion preservacion
        /// </summary>
        public int TipoFijacionPreservacionID { get; set; }
        /// <summary>
        /// nombre tipo fijacion preservacion 
        /// </summary>
        public string TipoFijacionPreservacion { get; set; }
        /// <summary>
        /// nomenclatura de la fijacion preservacion
        /// </summary>
        public string Nomenclatura { get; set; }
        #endregion
    }
}
