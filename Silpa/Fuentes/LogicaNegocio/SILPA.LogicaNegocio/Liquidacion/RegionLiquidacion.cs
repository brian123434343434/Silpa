using System.Collections.Generic;
using System;
using SILPA.Comun;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class RegionLiquidacion
    {
        #region  Objetos

            private RegionLiquidacionDalc _objRegionDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public RegionLiquidacion()
            {
                //Creary cargar configuración
                this._objRegionDalc = new RegionLiquidacionDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar las regiones en las cuales se puede llevar a cabo un proyecto asociado a una liquidación
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae las regiones activas o inactivas. Opcional </param>
            /// <returns>List con la información de las regiones</returns>
            public List<RegionLiquidacionEntity> ConsultarRegiones(bool? p_blnActivo = null)
            {
                return this._objRegionDalc.ConsultarRegiones(p_blnActivo);
            }

        #endregion

    }
}
