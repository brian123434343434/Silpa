using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.LogicaNegocio.Salvoconducto.Entidades;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class Resolucion
    {
        #region  Objetos

            private ResolucionDalc _objResolucionDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public Resolucion()
            {
                //Creary cargar configuración
                this._objResolucionDalc = new ResolucionDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Verificar si existe la resolución
            /// </summary>
            /// <param name="p_objResolucion">ResolucionEntity con la información de la resolución a verificar</param>
            /// <returns>bool indicando si la resolucion se encuentra registrada</returns>
            public bool ExisteResolucion(ResolucionEntity p_objResolucion)
            {
                return this._objResolucionDalc.ExisteResolucion(p_objResolucion.AutoridadAmbientalID, p_objResolucion.SolicitanteID, p_objResolucion.NumeroResolucion, p_objResolucion.FechaResolucion);
            }

        #endregion
    }
}
