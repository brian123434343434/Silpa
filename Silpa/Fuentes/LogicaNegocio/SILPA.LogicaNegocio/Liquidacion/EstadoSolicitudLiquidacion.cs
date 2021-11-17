using System.Collections.Generic;
using System;
using SILPA.Comun;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class EstadoSolicitudLiquidacion
    {
        #region  Objetos

            private EstadoSolicitudLiquidacionDalc _objEstadoDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public EstadoSolicitudLiquidacion()
            {
                //Creary cargar configuración
                this._objEstadoDalc = new EstadoSolicitudLiquidacionDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los estados que puede tomar una solicitud de liquidación
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los estados activos o inactivos. Opcional </param>
            /// <returns>List con la información de los estados</returns>
            public List<EstadoSolicitudLiquidacionEntity> ConsultarEstadosSolicitud(bool? p_blnActivo = null)
            {
                return this._objEstadoDalc.ConsultarEstadosSolicitud(p_blnActivo);
            }

        #endregion

    }
}
