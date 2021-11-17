using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class TecnicaMuestreoEntity
    {
        #region propiedades
        /// <summary>
        /// Identificador de la tecnica de muestreo
        /// </summary>
        public int TecnicaMuestreoID { get; set; }
        /// <summary>
        /// nombre de la tecnica de muestreo
        /// </summary>
        public string TecnicaMuestreo { get; set; }
        #endregion propiedades
    }
}
