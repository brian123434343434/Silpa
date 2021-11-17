using System.Collections.Generic;
using System;
using SILPA.Comun;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class TipoSolicitudLiquidacion
    {
        #region  Objetos

            private TipoSolicitudLiquidacionDalc _objTipoSolicitudDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public TipoSolicitudLiquidacion()
            {
                //Creary cargar configuración
                this._objTipoSolicitudDalc = new TipoSolicitudLiquidacionDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los tipos de solicitud de una liquidación
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae las solicitudes activas o inactivas. Opcional </param>
            /// <returns>List con la información de los tipos de solicitud</returns>
            public List<TipoSolicitudLiquidacionEntity> ConsultarTiposSolicitud(bool? p_blnActivo = null)
            {
                return this._objTipoSolicitudDalc.ConsultarTiposSolicitud(p_blnActivo);
            }

            /// <summary>
            /// Consultar la información de un tipo de solicitud especifico
            /// </summary>
            /// <param name="p_intTipoSolicitudID">int con el identificador de tipo de solicitud</param>
            /// <returns>TipoSolicitudLiquidacionEntity con la información del tipo de solicitud indicado</returns>
            public TipoSolicitudLiquidacionEntity ConsultarTipoSolicitud(int p_intTipoSolicitudID)
            {
                return this._objTipoSolicitudDalc.ConsultarTipoSolicitud(p_intTipoSolicitudID);
            }

        #endregion

    }
}
