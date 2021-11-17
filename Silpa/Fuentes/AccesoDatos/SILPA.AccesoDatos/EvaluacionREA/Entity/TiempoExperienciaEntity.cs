using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class TiempoExperienciaEntity
    {
        #region Propiedades
        /// <summary>
        /// Indentificador del Tiempo Experiencia
        /// </summary>
        public int TiempoExperienciaID { get; set; }
        /// <summary>
        /// nombre del Tiempo Experiencia
        /// </summary>
        public string TiempoExperiencia { get; set; }
        #endregion
    }
}
