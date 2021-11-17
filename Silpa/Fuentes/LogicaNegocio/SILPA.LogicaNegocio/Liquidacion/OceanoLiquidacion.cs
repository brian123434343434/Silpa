using System.Collections.Generic;
using System;
using SILPA.Comun;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class OceanoLiquidacion
    {
        #region  Objetos

            private OceanoLiquidacionDalc _objOceanoDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public OceanoLiquidacion()
            {
                //Creary cargar configuración
                this._objOceanoDalc = new OceanoLiquidacionDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los oceanos en los cuales se pueden desarrollar proyectos
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los oceanos activos o inactivas. Opcional </param>
            /// <returns>List con la información de los oceanos</returns>
            public List<OceanoLiquidacionEntity> ConsultarOceanos(bool? p_blnActivo = null)
            {
                return this._objOceanoDalc.ConsultarOceanos(p_blnActivo);
            }

        #endregion

    }
}
