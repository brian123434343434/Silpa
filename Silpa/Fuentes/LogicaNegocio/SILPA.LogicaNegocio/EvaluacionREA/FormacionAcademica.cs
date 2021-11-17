using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class FormacionAcademica
    {
         #region  Objetos

        private FormacionAcademicaDalc _objFormacionAcademicaDalc;

        #endregion
        

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public FormacionAcademica()
        {
            //Creary cargar configuración
            this._objFormacionAcademicaDalc = new FormacionAcademicaDalc();
        }

        #endregion

        #region  Metodos Publicos
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<FormacionAcademicaProfesionalEntity> ListaFormacionAcademicaXGrupoBiologico(int p_intGrupoBiologicoID)
        {
            return this._objFormacionAcademicaDalc.ListaFormacionAcademicaXGrupoBiologico(p_intGrupoBiologicoID);
        }

        #endregion
    }
}
