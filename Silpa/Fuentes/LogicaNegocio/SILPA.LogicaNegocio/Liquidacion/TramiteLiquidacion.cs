using System.Collections.Generic;
using System;
using SILPA.Comun;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class TramiteLiquidacion
    {
        #region  Objetos

            private TramiteLiquidacionDalc _objTramiteDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public TramiteLiquidacion()
            {
                //Creary cargar configuración
                this._objTramiteDalc = new TramiteLiquidacionDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los tramites pertenecientes a una solicitud
            /// </summary>
            /// <param name="p_intTipoSolicitudID">int con el identificador del tipo de solicitud</param>
            /// <param name="p_intSolicitudID">int con el identificador de la solicitud</param>
            /// <returns>List con la información de los tramites</returns>
            public List<TramiteLiquidacionEntity> ConsultarTramites(int p_intTipoSolicitudID, int p_intSolicitudID)
            {
                return this._objTramiteDalc.ConsultarTramites(p_intTipoSolicitudID, p_intSolicitudID);
            }

        #endregion

    }
}
