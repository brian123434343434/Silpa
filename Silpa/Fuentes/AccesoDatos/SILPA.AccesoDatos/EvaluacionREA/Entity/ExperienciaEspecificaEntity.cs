using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class ExperienciaEspecificaEntity
    {
        #region Propiedades
        /// <summary>
        /// Indentificador de la Experiencia Especifica
        /// </summary>
        public int ExperienciaEspecificaID { get; set; }
        /// <summary>
        /// nombre de la Experiencia Especifica
        /// </summary>
        public string ExperienciaEspecifica { get; set; }
        #endregion
    }
}
