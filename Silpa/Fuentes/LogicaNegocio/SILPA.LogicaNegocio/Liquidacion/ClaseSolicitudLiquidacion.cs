using System.Collections.Generic;
using System;
using SILPA.Comun;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class ClaseSolicitudLiquidacion
    {
        #region  Objetos

            private ClaseSolicitudLiquidacionDalc _objSolicitudDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public ClaseSolicitudLiquidacion()
            {
                //Creary cargar configuración
                this._objSolicitudDalc = new ClaseSolicitudLiquidacionDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar las clases de solicitudes pertenecientes a un tipo de liquidación
            /// </summary>
            /// <param name="p_intTipoSolicitudID">int con el identificador del tipo de solicitud</param>
            /// <returns>List con la información de las clases solicitudes</returns>
            public List<ClaseSolicitudLiquidacionEntity> ConsultarClaseSolicitudesTipoSolicitud(int p_intTipoSolicitudID)
            {
                return this._objSolicitudDalc.ConsultarClaseSolicitudesTipoSolicitud(p_intTipoSolicitudID);
            }

        #endregion

    }
}
