using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class FirmaAutoridadNotificacion
    {
        
        
        #region Metodos Publicos


            /// <summary>
            /// Retotna el listado de firmas que cumplan con las opciones de busqueda
            /// </summary>
            /// <param name="p_intAutoridadId">int con el id de la autoridad. Opcional</param>
            /// <param name="p_strNombreAutorizado">string con el nombre del autorizado.</param>
            /// <param name="p_blnActivo">bool que indica si se obtienen los activos o los inactivos. Opcional</param>
            /// <returns>List con la informacion de las firmas</returns>
            public List<FirmaAutoridadNotificacionEntity> ListarFirmas(int? p_intAutoridadId = null,
                                                                       string p_strNombreAutorizado = "", bool? p_blnActivo = null)
            {
                FirmaAutoridadNotificacionDalc objFirmaAutoridadNotificacionDalc = new FirmaAutoridadNotificacionDalc();
                return objFirmaAutoridadNotificacionDalc.ListarFirmas(p_intAutoridadId, p_strNombreAutorizado, p_blnActivo);
            }


            /// <summary>
            /// Obtener la información de la firma
            /// </summary>
            /// <param name="p_intFirmaAutoridadID">int con el identificador de la firma</param>
            /// <returns>FirmaAutoridadNotificacionEntity con la informacion de la firma</returns>
            public FirmaAutoridadNotificacionEntity ObtenerFirma(int p_intFirmaAutoridadID)
            {
                FirmaAutoridadNotificacionDalc objFirmaAutoridadNotificacionDalc = new FirmaAutoridadNotificacionDalc();
                return objFirmaAutoridadNotificacionDalc.ObtenerFirma(p_intFirmaAutoridadID);
            }


            /// <summary>
            /// Crea un nueva firma
            /// </summary>
            /// <param name="p_objFirma">FirmaAutoridadNotificacionEntity con la información de la firma</param>
            public void CrearFirma(FirmaAutoridadNotificacionEntity p_objFirma)
            {
                FirmaAutoridadNotificacionDalc objFirmaAutoridadNotificacionDalc = new FirmaAutoridadNotificacionDalc();
                objFirmaAutoridadNotificacionDalc.CrearFirma(p_objFirma);
            }


            /// <summary>
            /// Editar la información de la firma
            /// </summary>
            /// <param name="p_objFirma">FirmaAutoridadNotificacionEntity con la información de la firma</param>
            public void EditarFirma(FirmaAutoridadNotificacionEntity p_objFirma)
            {
                FirmaAutoridadNotificacionDalc objFirmaAutoridadNotificacionDalc = new FirmaAutoridadNotificacionDalc();
                objFirmaAutoridadNotificacionDalc.EditarFirma(p_objFirma);
            }


        #endregion


    }
}
