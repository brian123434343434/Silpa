using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class PlantillaNotificacion
    {
        
        
        #region Metodos Publicos


            /// <summary>
            /// Retorna el listado de Plantillas existentes para asociar a las plantillas
            /// </summary>
            /// <param name="p_strNombre">string con el nombre del Plantilla. Opcional</param>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad. Opcional</param>
            /// <param name="p_blnActivo">bool indica si se lista solo los Plantillas activos. Opcional</param>
            /// <returns>List con la información de los Plantillas</returns>
            public List<PlantillaNotificacionEntity> ListarPlantillas(string p_strNombre = "", int p_intAutoridadID = 0,  bool? p_blnActivo = null)
            {
                PlantillaNotificacionDalc objPlantillaNotificacionDalc = new PlantillaNotificacionDalc();
                return objPlantillaNotificacionDalc.ListarPlantillas(p_strNombre, p_intAutoridadID, p_blnActivo);
            }


            /// <summary>
            /// Obtener la información de una plantilla
            /// </summary>
            /// <param name="p_intPlantillaID">int con el identificador de la plantilla</param>
            /// <returns>PlantillaNotificacionEntity con la información de la plantilla</returns>
            public PlantillaNotificacionEntity ObtenerPlantilla(int p_intPlantillaID)
            {
                PlantillaNotificacionDalc objPlantillaNotificacionDalc = new PlantillaNotificacionDalc();
                return objPlantillaNotificacionDalc.ObtenerPlantilla(p_intPlantillaID);
            }


            /// <summary>
            /// Crea un nueva plantilla
            /// </summary>
            /// <param name="p_objPlantilla">PlantillaNotificacionEntity con la información de la plantilla</param>
            /// <returns>int con el identificador de la plantilla</returns>
            public int CrearPlantilla(PlantillaNotificacionEntity p_objPlantilla)
            {
                PlantillaNotificacionDalc objPlantillaNotificacionDalc = new PlantillaNotificacionDalc();
                return objPlantillaNotificacionDalc.CrearPlantilla(p_objPlantilla);
            }


            /// <summary>
            /// Editar la información del Plantilla
            /// </summary>
            /// <param name="p_objPlantilla">PlantillaNotificacionEntity con la información del Plantilla</param>
            public void EditarPlantilla(PlantillaNotificacionEntity p_objPlantilla)
            {
                PlantillaNotificacionDalc objPlantillaNotificacionDalc = new PlantillaNotificacionDalc();
                objPlantillaNotificacionDalc.EditarPlantilla(p_objPlantilla);
            }


            /// <summary>
            /// Obtener el listado de marcas configuradas
            /// </summary>
            /// <returns>List con la información de las marcas</returns>
            public List<MarcaPlantillaNotificacionEntity> ObtenerListadoMarcas()
            {
                PlantillaNotificacionDalc objPlantillaNotificacionDalc = new PlantillaNotificacionDalc();
                return objPlantillaNotificacionDalc.ObtenerListadoMarcas();
            }

        #endregion


    }
}
