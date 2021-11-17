using System.Collections.Generic;
using System;
using SILPA.Comun;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class MedioTransporteLiquidacion
    {
        #region  Objetos

            private MedioTransporteLiquidacionDalc _objMedioTransporteDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public MedioTransporteLiquidacion()
            {
                //Creary cargar configuración
                this._objMedioTransporteDalc = new MedioTransporteLiquidacionDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los medios de transporte
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los medios de transportes activos o inactivos. Opcional </param>
            /// <returns>List con la información de los medios de transportes</returns>
            public List<MedioTransporteLiquidacionEntity> ConsultarMediosTransporte(bool? p_blnActivo = null)
            {
                return this._objMedioTransporteDalc.ConsultarMediosTransporte(p_blnActivo);
            }

        #endregion

    }
}
