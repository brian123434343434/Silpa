using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class ExperienciaEspecifica
    {
        #region  Objetos

        private ExperienciaEspecificaDalc _objExperienciaEspecificaDalc;

        #endregion
        

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public ExperienciaEspecifica()
        {
            //Creary cargar configuración
            this._objExperienciaEspecificaDalc = new ExperienciaEspecificaDalc();
        }

        #endregion

        #region  Metodos Publicos
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<ExperienciaEspecificaEntity> ListaExperienciaEspecificaXGrupoBiologicoXFormacionAcademica(int p_intGrupoBiologicoID, int p_intFormacionAcademicaID)
        {
            return this._objExperienciaEspecificaDalc.ListaExperienciaEspecificaXGrupoBiologicoXFormacionAcademica(p_intGrupoBiologicoID, p_intFormacionAcademicaID);
        }

        #endregion
    }
}
