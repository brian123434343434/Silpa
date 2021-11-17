using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class Caracteristica
    {
        #region  Objetos

        private CaracteriticaDalc _objCaracteriticaDalc;

        #endregion
        

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public Caracteristica()
        {
            //Creary cargar configuración
            this._objCaracteriticaDalc = new CaracteriticaDalc();
        }

        #endregion

        #region  Metodos Publicos
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<CaracteristicaEntity> ListaCaracteristicasXGrupoBiologicoXTecnicamuestreo(int p_intGrupoBiologicoID, int p_intTecnicaMuestreoID, int? p_intCaracteristicaPadreID)
        {
            return this._objCaracteriticaDalc.ListaCaracteristicasXGrupoBiologicoXTecnicamuestreo(p_intGrupoBiologicoID, p_intTecnicaMuestreoID, p_intCaracteristicaPadreID);
        }

        #endregion
    }
}
