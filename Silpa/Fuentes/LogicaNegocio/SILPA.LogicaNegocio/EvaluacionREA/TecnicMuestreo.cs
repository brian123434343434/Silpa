using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class TecnicaMuestreo
    {
        #region  Objetos

        private TecnicaMuestreoDalc _objTecnicaMuestreoDalc;

        #endregion
        

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public TecnicaMuestreo()
        {
            //Creary cargar configuración
            this._objTecnicaMuestreoDalc = new TecnicaMuestreoDalc();
        }

        #endregion

        #region  Metodos Publicos
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<TecnicaMuestreoEntity> ListaTecnicaMuestreoPorGrupoBiologico(int p_intGrupoBiologicoID)
        {
            return this._objTecnicaMuestreoDalc.ListaTecnicaMuestreoPorGrupoBiologico(p_intGrupoBiologicoID);
        }

        #endregion
    }
}
