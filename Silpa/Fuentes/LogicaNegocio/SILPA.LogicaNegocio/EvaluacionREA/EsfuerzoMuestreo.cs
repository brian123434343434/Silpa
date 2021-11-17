using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class EsfuerzoMuestreo
    {
        #region  Objetos

        private EsfuerzoMuestreoDalc _objEsfuerzoMuestreoDalc;

        #endregion
        

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public EsfuerzoMuestreo()
        {
            //Creary cargar configuración
            this._objEsfuerzoMuestreoDalc = new EsfuerzoMuestreoDalc();
        }

        #endregion

        #region  Metodos Publicos
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<EsfuerzoMuestreoEntity> ListaEsfuerzoMuestreoXGrupoBiologicoXTecnicamuestreo(int p_intGrupoBiologicoID, int p_intTecnicaMuestreoID, int? p_intEsfuerzoMuestreoPadreID)
        {
            return this._objEsfuerzoMuestreoDalc.ListaEsfuerzoMuestreoXGrupoBiologicoXTecnicamuestreo(p_intGrupoBiologicoID, p_intTecnicaMuestreoID, p_intEsfuerzoMuestreoPadreID);
        }

        #endregion
    }
}
