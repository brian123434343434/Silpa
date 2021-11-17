using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class FijacionPreservacion
    {
        #region  Objetos

        private TipoFijacionPreservacionDalc _objTipoFijacionPreservacionDalc;

        #endregion
        

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public FijacionPreservacion()
        {
            //Creary cargar configuración
            this._objTipoFijacionPreservacionDalc = new TipoFijacionPreservacionDalc();
        }

        #endregion

        #region  Metodos Publicos
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<TipoFijacionPreservacionEntity> ListaTipoFijacionPreservacionXGrupoBiologicoXTipoSacrificio(int p_intGrupoBiologicoID, int p_intTipoSacrificioID)
        {
            return this._objTipoFijacionPreservacionDalc.ListaTipoFijacionPreservacionXGrupoBiologicoXTipoSacrificio(p_intGrupoBiologicoID, p_intTipoSacrificioID);
        }

        #endregion
    }
}
