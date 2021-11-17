using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class EstadoFlujoNotificacion
    {
        
        
        #region Metodos Publicos


            /// <summary>
            /// Retorna el listado de estados
            /// </summary>
            /// <param name="p_intFlujoID">int con el identificador del flujo</param>
            /// <param name="p_intEstadoFlujoID">int con el identificador del estado del flujo</param>
            /// <returns></returns>
            public List<EstadoFlujoNotificacionEntity> ListarEstadosNotificacionElectronica(int p_intFlujoID, int p_intEstadoFlujoID = 0)
            {
                EstadoFlujoNotificacionDalc objEstadoNotificacionDalc = new EstadoFlujoNotificacionDalc();
                return objEstadoNotificacionDalc.ListarEstadosNotificacionElectronica(p_intFlujoID, p_intEstadoFlujoID);
            }

            /// <summary>
            /// Retornar el listado de estados que no se encuentran asociados al flujo
            /// </summary>
            /// <param name="p_intFlujoID">int con el identificador del flujo</param>
            /// <returns>List con la información de los estados</returns>
            public List<EstadoNotificacionEntity> ListarEstadosNoFlujoNotificacionElectronica(int p_intFlujoID)
            {
                EstadoFlujoNotificacionDalc objEstadoNotificacionDalc = new EstadoFlujoNotificacionDalc();
                return objEstadoNotificacionDalc.ListarEstadosNoFlujoNotificacionElectronica(p_intFlujoID);
            }


            /// <summary>
            /// Retorna la configuración para un estado y flujo especifico
            /// </summary>
            /// <param name="p_intFlujoID">int con el id del flujo</param>
            /// <param name="p_intEstadoID">int copn el identificador del estado</param>
            /// <returns>EstadoFlujoNotificacionEntity con la configuración del estado</returns>
            public EstadoFlujoNotificacionEntity ConsultarConfiguracionEstadoNotificacionElectronica(int p_intFlujoID, int p_intEstadoID)
            {
                EstadoFlujoNotificacionDalc objEstadoNotificacionDalc = new EstadoFlujoNotificacionDalc();
                return objEstadoNotificacionDalc.ConsultarConfiguracionEstadoNotificacionElectronica(p_intFlujoID, p_intEstadoID);
            }


            /// <summary>
            /// Crea un nuevo estado asociado al flujo
            /// </summary>
            /// <param name="p_objEstadoFlujo">EstadoFlujoNotificacionEntity con la información del flujo</param>
            public void CrearEstadoFlujo(EstadoFlujoNotificacionEntity p_objEstadoFlujo)
            {
                EstadoFlujoNotificacionDalc objEstadoNotificacionDalc = new EstadoFlujoNotificacionDalc();
                objEstadoNotificacionDalc.CrearEstadoFlujo(p_objEstadoFlujo);
            }


            /// <summary>
            /// Editar la información del estado asociado al flujo
            /// </summary>
            /// <param name="p_objEstadoFlujo">EstadoFlujoNotificacionEntity con la información del estado</param>
            public void EditarEstadoFlujo(EstadoFlujoNotificacionEntity p_objEstadoFlujo)
            {
                EstadoFlujoNotificacionDalc objEstadoNotificacionDalc = new EstadoFlujoNotificacionDalc();
                objEstadoNotificacionDalc.EditarEstadoFlujo(p_objEstadoFlujo);
            }


        #endregion


    }
}
