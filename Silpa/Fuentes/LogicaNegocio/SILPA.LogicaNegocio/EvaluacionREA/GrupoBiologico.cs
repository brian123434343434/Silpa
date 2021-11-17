using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class GrupoBiologico
    {
        #region  Objetos

        private GrupoBiologicoDalc _objGrupoBiologicoDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public GrupoBiologico()
            {
                //Creary cargar configuración
                this._objGrupoBiologicoDalc = new GrupoBiologicoDalc();
            }

        #endregion

        #region  Metodos Publicos
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public List<GrupoBiologicoEntity> ListaGruposBiologicos()
        {
            return this._objGrupoBiologicoDalc.ListaGruposBiologicos();
        }

        #endregion
    }
}
