using System.Collections.Generic;
using System;
using SILPA.Comun;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class PermisoLiquidacion
    {
        #region  Objetos

            private PermisoLiquidacionDalc _objPermisoDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public PermisoLiquidacion()
            {
                //Creary cargar configuración
                this._objPermisoDalc = new PermisoLiquidacionDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los permisos que se pueden asociar a una liquidación
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los Permisos activas o inactivas. Opcional </param>
            /// <returns>List con la información de los permisos</returns>
            public List<PermisoLiquidacionEntity> ConsultarPermisos(bool? p_blnActivo = null)
            {
                return this._objPermisoDalc.ConsultarPermisos(p_blnActivo);
            }

        #endregion

    }
}
