using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class FlujoNotificacionElectronica
    {
        private FlujoNotificacionElectronicaDalc objTipoDocumentoDALC;

        #region Creadoras

            public FlujoNotificacionElectronica()
            {
                objTipoDocumentoDALC = new FlujoNotificacionElectronicaDalc();
            }

        #endregion

        #region Metodos Publicos

            /// <summary>
            /// Consultar el listado de flujos existentes que cumplan con las condiciones de busqueda
            /// </summary>
            /// <param name="intIdFlujoNot">int con el id del flujo</param>
            /// <param name="strFlujoNotNombre">string con el nombre del flujo</param>
            /// <returns>DataTable con la información de los flujos existentes</returns>
            public DataTable ConsultaFlujosNotificacion(int? intIdFlujoNot, int? p_intAutoridadID, string strFlujoNot)
            {
                return objTipoDocumentoDALC.ListarFLujosNotificacionElectronica(intIdFlujoNot, p_intAutoridadID, strFlujoNot);
            }

            /// <summary>
            /// Consultar el listado de flujos existentes que se relacionan al usuario especificado
            /// </summary>
            /// <param name="p_lngUsuarioID">long con el identificador del usuario</param>
            public DataTable ConsultaFlujosNotificacionUsuario(long p_lngUsuarioID)
            {
                return objTipoDocumentoDALC.ConsultaFlujosNotificacionUsuario(p_lngUsuarioID);
            }

            /// <summary>
            /// Crear un nuevo flujo
            /// </summary>
            /// <param name="p_intAutoridadID">int cone el identificador de la autoridad</param>
            /// <param name="p_strNombreFlujo">string con el nombre del flujo</param>
            /// <param name="p_blnActivo">bool indicando si el flujo se encuentra activo</param>
            public void CrearFlujoNotificacion(int p_intAutoridadID, string p_strNombreFlujo, bool p_blnActivo)
            {
                objTipoDocumentoDALC.CrearFlujoNotificacion(p_intAutoridadID, p_strNombreFlujo, p_blnActivo);
            }

            /// <summary>
            /// Editar la información de un flujo
            /// </summary>
            /// <param name="p_intAutoridadID">int cone el identificador de la autoridad</param>
            /// <param name="p_intFlujoID">int con el identificador del flujo</param>
            /// <param name="p_strNombreFlujo">string con el nombre del flujo</param>
            /// <param name="p_blnActivo">bool indicando si el flujo se encuentra activo</param>
            public void EditarFlujoNotificacion(int p_intAutoridadID, int p_intFlujoID, string p_strNombreFlujo, bool p_blnActivo)
            {
                objTipoDocumentoDALC.EditarFlujoNotificacion(p_intAutoridadID, p_intFlujoID, p_strNombreFlujo, p_blnActivo);
            }

        #endregion
    }
}
