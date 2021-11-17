using System.Collections.Generic;
using System;
using SILPA.Comun;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class SectorLiquidacion
    {
        #region  Objetos

            private SectorLiquidacionDalc _objSectorDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public SectorLiquidacion()
            {
                //Creary cargar configuración
                this._objSectorDalc = new SectorLiquidacionDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los sectores que se pueden asociar a una liquidación
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los sectores activas o inactivas. Opcional </param>
            /// <returns>List con la información de los sectores</returns>
            public List<SectorLiquidacionEntity> ConsultarSectores(bool? p_blnActivo = null)
            {
                return this._objSectorDalc.ConsultarSectores(p_blnActivo);
            }

        #endregion

    }
}
