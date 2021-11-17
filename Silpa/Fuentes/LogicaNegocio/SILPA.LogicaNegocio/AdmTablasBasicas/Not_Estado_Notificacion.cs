using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class Not_Estado_Notificacion
    {
        private Not_Estado_NotificacionDALC objNot_Estado_NotificacionDALC;
        public Not_Estado_Notificacion()
        {
            objNot_Estado_NotificacionDALC = new Not_Estado_NotificacionDALC();
        }

        #region "NOT_ESTADO_NOTIFICACION"

        public DataTable Listar_Not_Estado_Notificacion(string strDescripcion)
        {
            return objNot_Estado_NotificacionDALC.Listar_Not_Estado_Notificacion(strDescripcion);
        }

        public void Insertar_Not_Estado_Notificacion(string strDescripcion, byte byEstado, byte byEstadoPDI, int intDiasVenvimiento, bool mostrarInfo, bool enviaCorreo, string mensajeCorreo, bool espublico, string p_strDescripcionMostrar)
        {
            objNot_Estado_NotificacionDALC.Insertar_Not_Estado_Notificacion(strDescripcion, byEstado, byEstadoPDI, intDiasVenvimiento, mostrarInfo, enviaCorreo, mensajeCorreo, espublico, p_strDescripcionMostrar);
        }

        public void Actualizar_Not_Estado_Notificacion(int iId, string strDescripcion, byte byEstado, byte byEstadoPDI, int intDiasVenvimiento, bool mostrarInfo, bool enviaCorreo, string mensajeCorreo, bool espublico, string p_strDescripcionMostrar)
        {
            objNot_Estado_NotificacionDALC.Actualizar_Not_Estado_Notificacion(iId, strDescripcion, byEstado, byEstadoPDI, intDiasVenvimiento, mostrarInfo, enviaCorreo, mensajeCorreo, espublico, p_strDescripcionMostrar);
        }

        public void Eliminar_Not_Estado_Notificacion(int iId)
        {
            objNot_Estado_NotificacionDALC.Eliminar_Not_Estado_Notificacion(iId);
        }

        #endregion
    }
}
