using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class FirmaAutoridadPlantillaNotificacion
    {
        
        
        #region Metodos Publicos


            /// <summary>
            /// Retotna el listado de firmas que cumplan con las opciones de busqueda
            /// </summary>
            /// <param name="p_intAutoridadId">int con el id de la autoridad. Opcional</param>
            /// <param name="p_intPlantillaID">int con el id de la plantilla. Opcional</param>
            /// <param name="p_strNombreAutorizado">string con el nombre del autorizado.</param>
            /// <param name="p_blnActivo">bool que indica si se obtienen los activos o los inactivos. Opcional</param>
            /// <returns>List con la informacion de las firmas</returns>
            public List<FirmaAutoridadNotificacionEntity> ListarFirmasPlantilla(int p_intPlantillaID)
            {
                FirmaAutoridadPlantillaNotificacionDalc objFirmaAutoridadPlantillaNotificacionDalc = new FirmaAutoridadPlantillaNotificacionDalc();
                return objFirmaAutoridadPlantillaNotificacionDalc.ListarFirmasPlantilla(p_intPlantillaID);
            }


            /// <summary>
            /// Crear una nueva firma a una plantilla
            /// </summary>
            /// <param name="p_intFirmaAutoridadID">int con el identificador de la firma</param>
            /// <param name="p_intPlantillaID">int con el identificador de la plantilla</param>
            public void CrearFirmaAutoridadPlantilla(int p_intFirmaAutoridadID, int p_intPlantillaID)
            {
                FirmaAutoridadPlantillaNotificacionDalc objFirmaAutoridadPlantillaNotificacionDalc = new FirmaAutoridadPlantillaNotificacionDalc();
                objFirmaAutoridadPlantillaNotificacionDalc.CrearFirmaAutoridadPlantilla(p_intFirmaAutoridadID, p_intPlantillaID);
            }


            /// <summary>
            /// Eliminar las firmas de una plantilla
            /// </summary>
            /// <param name="p_intPlantillaID">int con el identificador de la plantilla</param>
            public void EliminarFirmasPlantilla(int p_intPlantillaID)
            {
                FirmaAutoridadPlantillaNotificacionDalc objFirmaAutoridadPlantillaNotificacionDalc = new FirmaAutoridadPlantillaNotificacionDalc();
                objFirmaAutoridadPlantillaNotificacionDalc.EliminarFirmasPlantilla(p_intPlantillaID);
            }

        #endregion
        
    }
}
