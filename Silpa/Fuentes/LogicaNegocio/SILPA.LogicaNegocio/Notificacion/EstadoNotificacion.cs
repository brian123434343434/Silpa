using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class EstadoNotificacion
    {
        
        
        #region Metodos Publicos


            /// <summary>
            /// Consultar el listado de estados existentes de acuerdo a los parametros de busqueda
            /// </summary>
            /// <param name="p_strNombre">string con el nombre del estado</param>
            /// <param name="p_strDescripcion">string con la descripción del estado</param>
            /// <returns>Listado con los estado existentes</returns>
            public List<EstadoNotificacionEntity> ObtenerEstadosNotificacion(string p_strNombre, string p_strDescripcion)
            {
                EstadoNotificacionDalc objEstadoNotificacionDalc = new EstadoNotificacionDalc();
                return objEstadoNotificacionDalc.ObtenerEstadosNotificacion(p_strNombre, p_strDescripcion);
            }


            /// <summary>
            /// Consultar informacion de estado indicado
            /// </summary>
            /// <param name="p_intEstadoID">int con el identificador del estado</param>
            /// <returns>EstadoNotificacionEntity con la informacion del estado</returns>
            public EstadoNotificacionEntity ObtenerEstadoNotificacion(int p_intEstadoID)
            {
                EstadoNotificacionDalc objEstadoNotificacionDalc = new EstadoNotificacionDalc();
                return objEstadoNotificacionDalc.ObtenerEstadoNotificacion(p_intEstadoID);
            }


            /// <summary>
            /// Obtener el listado de estados existentes
            /// </summary>
            /// <param name="p_intUsuarioID">int con el identificador del usuario que realiza la consulta</param>
            /// <param name="p_intFlujoNotificacion">int con el identificador del flujo</param>
            /// <returns>List con la informacion de los estados</returns>
            public List<EstadoNotificacionEntity> ListarEstadosNotificacion(int p_intUsuarioID = -1, int p_intFlujoNotificacion = -1)
            {
                EstadoNotificacionDalc objEstadoNotificacionDalc = new EstadoNotificacionDalc();
                return objEstadoNotificacionDalc.ListarEstadosNotificacion(p_intUsuarioID, p_intFlujoNotificacion);
            }


            /// <summary>
            /// Crear un nuevo estado de notificación
            /// </summary>
            ///<param name="p_objEstadoNotificacion">EstadoNotificacionEntity con la información del estado a crear</param>
            public void CrearEstadoNotificacion(EstadoNotificacionEntity p_objEstadoNotificacion)
            {
                EstadoNotificacionDalc objEstadoNotificacionDalc = new EstadoNotificacionDalc();
                objEstadoNotificacionDalc.CrearEstadoNotificacion(p_objEstadoNotificacion);
            }

            /// <summary>
            /// Modifica un estado de notificación
            /// </summary>
            ///<param name="p_objEstadoNotificacion">EstadoNotificacionEntity con la información del estado a crear</param>
            public void ModificarEstadoNotificacion(EstadoNotificacionEntity p_objEstadoNotificacion)
            {
                EstadoNotificacionDalc objEstadoNotificacionDalc = new EstadoNotificacionDalc();
                objEstadoNotificacionDalc.ModificarEstadoNotificacion(p_objEstadoNotificacion);
            }


            /// <summary>
            /// Consultar los correos pertenecientes a un estado especifico
            /// </summary>
            /// <param name="p_lngEstadoPersonaActoID">long con el identificador del estado</param>
            /// <returns>List con la informacion de los correos</returns>
            public List<CorreoNotificacionEntity> ConsultarCorreosEstadoPersonaActo(long p_lngEstadoPersonaActoID)
            {
                EstadoNotificacionDalc objEstadoNotificacionDalc = new EstadoNotificacionDalc();
                return objEstadoNotificacionDalc.ConsultarCorreosEstadoPersonaActo(p_lngEstadoPersonaActoID);
            }


            /// <summary>
            /// Consultar las direcciones pertnecientes a un estado especifico
            /// </summary>
            /// <param name="p_lngEstadoPersonaActoID">long con el identificador del estado</param>
            /// <returns>List con la informacion de las direcciones</returns>
            public List<DireccionNotificacionEntity> ConsultarDireccionesEstadoPersonaActo(long p_lngEstadoPersonaActoID)
            {
                EstadoNotificacionDalc objEstadoNotificacionDalc = new EstadoNotificacionDalc();
                return objEstadoNotificacionDalc.ConsultarDireccionesEstadoPersonaActo(p_lngEstadoPersonaActoID);
            }


        #endregion


    }
}
