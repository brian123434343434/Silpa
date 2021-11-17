using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class TipoSacrificio
    {
        #region  Objetos

        private TipoSacrificioDalc _objTipoSacrificioDalc;

        #endregion
        

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public TipoSacrificio()
        {
            //Creary cargar configuración
            this._objTipoSacrificioDalc = new TipoSacrificioDalc();
        }

        #endregion

        #region  Metodos Publicos
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<TipoSacrificioEntity> ListaTipoSacrificioXGrupoBiologico(int p_intGrupoBiologicoID)
        {
            return this._objTipoSacrificioDalc.ListaTipoSacrificioXGrupoBiologico(p_intGrupoBiologicoID);
        }

        #endregion
    }
}
