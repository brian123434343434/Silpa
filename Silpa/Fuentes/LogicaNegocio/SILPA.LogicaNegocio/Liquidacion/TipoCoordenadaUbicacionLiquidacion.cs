using System.Collections.Generic;
using System;
using SILPA.Comun;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class TipoCoordenadaUbicacionLiquidacion
    {
        #region  Objetos

            private TipoCoordenadaUbicacionLiquidacionDalc _objTipoCoordenadaUbicacionDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public TipoCoordenadaUbicacionLiquidacion()
            {
                //Creary cargar configuración
                this._objTipoCoordenadaUbicacionDalc = new TipoCoordenadaUbicacionLiquidacionDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los tipos de coordenada
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los tipos de coordenada activos o inactivos. Opcional </param>
            /// <returns>List con la información de los tipo de coordenada</returns>
            public List<TipoCoordenadaUbicacionLiquidacionEntity> ConsultarTiposCoordenada(bool? p_blnActivo = null)
            {
                return this._objTipoCoordenadaUbicacionDalc.ConsultarTiposCoordenada(p_blnActivo);
            }

        #endregion

    }
}
