using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class UnidadMuestreo
    {
        #region  Objetos

        private UnidadMuestreoDalc _objUnidadMuestreoDalc;

        #endregion
        

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public UnidadMuestreo()
        {
            //Creary cargar configuración
            this._objUnidadMuestreoDalc = new UnidadMuestreoDalc();
        }

        #endregion

        #region  Metodos Publicos
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<UnidadMuestreoEntity> ListaUnidadMuestreoXGrupoBiologicoXTecnicaMuestreo(int p_intGrupoBiologicoID, int p_intTecnicaMuestreoID)
        {
            return this._objUnidadMuestreoDalc.ListaUnidadMuestreoXGrupoBiologicoXTecnicaMuestreo(p_intGrupoBiologicoID, p_intTecnicaMuestreoID);
        }

        #endregion
    }
}
