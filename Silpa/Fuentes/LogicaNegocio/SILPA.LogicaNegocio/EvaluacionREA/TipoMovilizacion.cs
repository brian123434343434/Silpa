using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class TipoMovilizacion
    {
        #region  Objetos

        private TipoMovilizacionDalc _objTipoMovilizacionDalc;

        #endregion
        

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public TipoMovilizacion()
        {
            //Creary cargar configuración
            this._objTipoMovilizacionDalc = new TipoMovilizacionDalc();
        }

        #endregion

        #region  Metodos Publicos
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<TipoMovilizacionEntity> ListaTipoMovilizacionXGrupoBiologicoXTipoSacrificioXFijaPreserv(int p_intGrupoBiologicoID, int p_intTipoSacrificioID, int p_intFijacionPreservID)
        {
            return this._objTipoMovilizacionDalc.ListaTipoMovilizacionXGrupoBiologicoXTipoSacrificioXFijaPreserv(p_intGrupoBiologicoID, p_intTipoSacrificioID, p_intFijacionPreservID);
        }

        #endregion
    }
}
