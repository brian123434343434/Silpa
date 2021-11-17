using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class InsumoProfesionalEntity
    {
        public GrupoBiologicoEntity objGrupoBiologico { get; set; }
        public FormacionAcademicaProfesionalEntity objFormacionAcademicaProfesionalEntity { get; set; }
        public TiempoExperienciaEntity objTiempoExperienciaEntity { get; set; }
        public ExperienciaEspecificaEntity objExperienciaEspecificaEntity { get; set; }
        public string Key { get { return UniqueKey(); } }
        public string UniqueKey()
        {
            return string.Format("{0}{1}{2}{3}", this.objGrupoBiologico.GrupoBiologicoID, objFormacionAcademicaProfesionalEntity.FormacionAcademicaProfesionalID, objTiempoExperienciaEntity.TiempoExperienciaID, objExperienciaEspecificaEntity.ExperienciaEspecificaID);
        }
    }
}
