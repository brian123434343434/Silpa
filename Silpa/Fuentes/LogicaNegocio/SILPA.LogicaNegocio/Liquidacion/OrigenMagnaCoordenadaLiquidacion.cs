using System.Collections.Generic;
using System;
using SILPA.Comun;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class OrigenMagnaCoordenadaLiquidacion
    {
        #region  Objetos

            private OrigenMagnaCoordenadaLiquidacionDalc _objOrigenMagnaCoordenadaDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public OrigenMagnaCoordenadaLiquidacion()
            {
                //Creary cargar configuración
                this._objOrigenMagnaCoordenadaDalc = new OrigenMagnaCoordenadaLiquidacionDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los tipos de origenes magna
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los tipos de origenes magna activos o inactivos. Opcional </param>
            /// <returns>List con la información de los tipo de coordenada</returns>
            public List<OrigenMagnaCoordenadaLiquidacionEntity> ConsultarOrigenesMagna(bool? p_blnActivo = null)
            {
                return this._objOrigenMagnaCoordenadaDalc.ConsultarOrigenesMagna(p_blnActivo);
            }

        #endregion

    }
}
