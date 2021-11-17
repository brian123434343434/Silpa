using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class PublicidadEstados
    {
        #region Metodos Publicos

            /// <summary>
            /// Obtiene los estados publicados de acuerdo a los parametros de busqueda especificados
            /// </summary>
            /// <param name="p_intTipoTramiteID">int con el identificador del tipo de trámite</param>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental</param>
            /// <param name="p_strNumeroVital">string con el numero vital</param>
            /// <param name="p_strExpediente">string con el numero de expediente</param>
            /// <param name="p_intTipoActo">int con el identificador del tipo de actoa dministrativo</param>
            /// <param name="p_strNumeroActo">string con el numero de acto administrativo</param>
            /// <param name="p_strNumeroIdentificacion">string con el número de identificación</param>
            /// <param name="p_blnIncluirHistorico">bool que indica si se incluye publicaciones desfijadas</param>
            /// <param name="p_dteFechaInicio">DataTime con la fecha de inicio de fijación de la publicación</param>
            /// <param name="p_dteFechaFin">DateTime con la fecha de incio de fijación de la publicacion</param>
            /// <returns></returns>
            public DataTable ConsultarInformacionEstadosPublicados(int p_intTipoTramiteID, int p_intAutoridadID, string p_strNumeroVital, string p_strExpediente, int p_intTipoActo,
                                                                   string p_strNumeroActo, string p_strNumeroIdentificacion, bool p_blnIncluirHistorico, DateTime p_dteFechaInicio, DateTime p_dteFechaFin)
            {
                PublicidadEstadosDalc objPublicidadEstadosDalcDalc = new PublicidadEstadosDalc();
                return objPublicidadEstadosDalcDalc.ConsultarInformacionEstadosPublicados(p_intTipoTramiteID, p_intAutoridadID, p_strNumeroVital, p_strExpediente, p_intTipoActo,
                                                                                          p_strNumeroActo, p_strNumeroIdentificacion, p_blnIncluirHistorico, p_dteFechaInicio, p_dteFechaFin);
            }


            /// <summary>
            /// Retorna la información de un estado de publicidad especifico
            /// </summary>
            /// <param name="p_lngPublicacionEstadoPersonaActoID">long con el identificador de publicación</param>
            /// <returns>DataTable con la información de la publicación</returns>
            public DataTable ConsultarInformacionPublicacion(long p_lngPublicacionEstadoPersonaActoID)
            {
                PublicidadEstadosDalc objPublicidadEstadosDalcDalc = new PublicidadEstadosDalc();
                return objPublicidadEstadosDalcDalc.ConsultarInformacionPublicacion(p_lngPublicacionEstadoPersonaActoID);
            }

        #endregion
    }
}
