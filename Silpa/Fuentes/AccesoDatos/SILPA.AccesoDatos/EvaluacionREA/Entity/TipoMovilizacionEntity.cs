using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class TipoMovilizacionEntity
    {
        #region Propiedades
        /// <summary>
        /// Identificador de tipo movilizacion 
        /// </summary>
        public int TipoMovilizacionID { get; set; }
        /// <summary>
        /// Nombre tipo movilizacion
        /// </summary>
        public string TipoMovilizacion { get; set; }
        /// <summary>
        /// nomemclatura del tipo de movilizacion
        /// </summary>
        public string Nomenclatura { get; set; }
        #endregion
    }
}
