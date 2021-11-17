using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class TiempoExperiencia
    {
        #region  Objetos

        private TiempoExperienciaDalc _objTiempoExperienciaDalc;

        #endregion
        

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public TiempoExperiencia()
        {
            //Creary cargar configuración
            this._objTiempoExperienciaDalc = new TiempoExperienciaDalc();
        }

        #endregion

        #region  Metodos Publicos
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<TiempoExperienciaEntity> ListaTiempoExperiencia()
        {
            return this._objTiempoExperienciaDalc.ListaTiempoExperiencia();
        }

        #endregion
    }
}
