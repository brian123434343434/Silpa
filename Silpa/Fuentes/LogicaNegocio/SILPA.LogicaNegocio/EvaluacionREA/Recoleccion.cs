using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class Recoleccion
    {
        #region  Objetos

        private RecoleccionDalc _objRecoleccionDalc;

        #endregion
        

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public Recoleccion()
        {
            //Creary cargar configuración
            this._objRecoleccionDalc = new RecoleccionDalc();
        }

        #endregion

        #region  Metodos Publicos
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<RecoleccionEntity> ListaRecoleccionXGrupoBiologicoXTecnicaMuestreo(int p_intGrupoBiologicoID, int p_intTecnicaMuestreoID)
        {
            return this._objRecoleccionDalc.ListaRecoleccionXGrupoBiologicoXTecnicaMuestreo(p_intGrupoBiologicoID, p_intTecnicaMuestreoID);
        }

        #endregion
    }
}
