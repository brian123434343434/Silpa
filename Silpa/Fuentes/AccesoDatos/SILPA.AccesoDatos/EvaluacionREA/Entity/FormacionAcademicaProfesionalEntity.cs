using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class FormacionAcademicaProfesionalEntity
    {
        #region Propiedades
        /// <summary>
        /// Identificador de Formacion Academica Profesional 
        /// </summary>
        public int FormacionAcademicaProfesionalID { get; set; }
        /// <summary>
        /// Nombre Formacion Academica Profesional
        /// </summary>
        public string FormacionAcademicaProfesional { get; set; }
        #endregion
    }
}
