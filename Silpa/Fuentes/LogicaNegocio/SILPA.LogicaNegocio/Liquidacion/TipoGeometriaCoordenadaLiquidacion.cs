using System.Collections.Generic;
using System;
using SILPA.Comun;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class TipoGeometriaCoordenadaLiquidacion
    {
        #region  Objetos

            private TipoGeometriaCoordenadaLiquidacionDalc _objTipoGeometriaCoordenadaDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public TipoGeometriaCoordenadaLiquidacion()
            {
                //Creary cargar configuración
                this._objTipoGeometriaCoordenadaDalc = new TipoGeometriaCoordenadaLiquidacionDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los tipos de geometria para una coordenada
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los tipos de geometria activos o inactivos. Opcional </param>
            /// <returns>List con la información de los tipos de geometria</returns>
            public List<TipoGeometriaCoordenadaLiquidacionEntity> ConsultarTiposGeometria(bool? p_blnActivo = null)
            {
                return this._objTipoGeometriaCoordenadaDalc.ConsultarTiposGeometria(p_blnActivo);
            }

        #endregion

    }
}
