using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.PINES;

namespace SILPA.LogicaNegocio.PINES
{
    public class ComentariosActividad
    {
        public ComentarioActividadIdentity vComentarioActividadIdentity;
        public PINESDALC vPINESDALC;
        /// <summary>
        /// objeto de configuracion
        /// </summary>
        private Configuracion _objConfiguracion;

        public ComentariosActividad()
        {
            _objConfiguracion = new Configuracion();
            vComentarioActividadIdentity = new ComentarioActividadIdentity();
            vPINESDALC = new PINESDALC();
        }

        public void InsertarComentarioActividad(ComentarioActividadIdentity pComentarioActividadIdentity, bool blContinuaProcesoAccion)
        {
            try
            {
                vPINESDALC.InsertarComentarioActividad(pComentarioActividadIdentity, blContinuaProcesoAccion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ConsultarComentarioActividad(ref ComentarioActividadIdentity pComentarioActividadIdentity)
        {
            try
            {
                vPINESDALC.ConsultarComentarioActividad(ref pComentarioActividadIdentity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ComentarioActividadIdentity> ListaComentarioActividad(ComentarioActividadIdentity pComentarioActividadIdentity)
        {
            try
            {
                return vPINESDALC.ListaComentarioActividad(pComentarioActividadIdentity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
