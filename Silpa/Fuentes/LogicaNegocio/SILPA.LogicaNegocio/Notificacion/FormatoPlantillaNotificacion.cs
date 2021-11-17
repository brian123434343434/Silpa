using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class FormatoPlantillaNotificacion
    {
        
        
        #region Metodos Publicos


            /// <summary>
            /// Retorna el listado de formatos existentes para asociar a las plantillas
            /// </summary>
            /// <param name="p_strNombre">string con el nombre del formato. Opcional</param>
            /// <param name="p_blnActivo">bool indica si se lista solo los formatos activos. Opcional</param>
            /// <returns>List con la información de los formatos</returns>
            public List<FormatoPlantillaNotificacionEntity> ListarFormatos(string p_strNombre = "", bool? p_blnActivo = null)
            {
                FormatoPlantillaNotificacionDalc objFormatoPlantillaNotificacionDalc = new FormatoPlantillaNotificacionDalc();
                return objFormatoPlantillaNotificacionDalc.ListarFormatos(p_strNombre, p_blnActivo);
            }


            /// <summary>
            /// Crea un nuevo formato
            /// </summary>
            /// <param name="p_objFormato">FormatoPlantillaNotificacionEntity con la información del formato</param>
            public void CrearFormato(FormatoPlantillaNotificacionEntity p_objFormato)
            {
                FormatoPlantillaNotificacionDalc objFormatoPlantillaNotificacionDalc = new FormatoPlantillaNotificacionDalc();
                objFormatoPlantillaNotificacionDalc.CrearFormato(p_objFormato);
            }


            /// <summary>
            /// Editar la información del formato
            /// </summary>
            /// <param name="p_objFormato">FormatoPlantillaNotificacionEntity con la información del formato</param>
            public void EditarFormato(FormatoPlantillaNotificacionEntity p_objFormato)
            {
                FormatoPlantillaNotificacionDalc objFormatoPlantillaNotificacionDalc = new FormatoPlantillaNotificacionDalc();
                objFormatoPlantillaNotificacionDalc.EditarFormato(p_objFormato);
            }


        #endregion


    }
}
