using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class CargueEnvioNotificacion
    {

        #region Metodos privados



        #endregion


        #region Metodos Publicos


            /// <summary>
            /// Retornar el listado de cargues realizados en el rango de fechas especificado
            /// </summary>
            /// <param name="p_objFechaDesde">DateTime con la fecha desde la cual se debe buscar</param>
            /// <param name="p_objFechaHasta">DateTime con la fecha hasta la cual se debe buscar</param>
            /// <param name="p_intUsuarioID">int con el identificador del usuario que se encuentra realizando la consulta</param>
            /// <returns>Listado con la informacion del cargue realizado</returns>
            public List<CargueEnvioNotificacionEntity> ConsultarCarguesEnvios(DateTime p_objFechaDesde, DateTime p_objFechaHasta, int p_intUsuarioID)
            {
                CargueEnvioNotificacionDalc objCargueEnvioNotificacionDalc = new CargueEnvioNotificacionDalc();
                return objCargueEnvioNotificacionDalc.ConsultarCarguesEnvios(p_objFechaDesde, p_objFechaHasta, p_intUsuarioID);
            }

        #endregion


    }
}
