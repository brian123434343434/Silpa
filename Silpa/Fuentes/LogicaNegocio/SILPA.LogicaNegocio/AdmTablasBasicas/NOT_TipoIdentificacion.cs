using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class NOT_TipoIdentificacion
    {
        private NOT_TipoIdentificacionDALC objTipoIdentificacionDALC;
        public NOT_TipoIdentificacion()
        {
            objTipoIdentificacionDALC = new NOT_TipoIdentificacionDALC();
        }

        #region "TIPO_DATO"

        public DataTable Listar_Tipo_Documento(string strDescripcion)
        {
            return objTipoIdentificacionDALC.Listar_Tipo_Identificacion(strDescripcion);
        }

        /// <summary>
        /// Consultar la información de un tipo de identificación
        /// </summary>
        /// <param name="intTipoIdentificacionID">int con el identificador del tipo de identificación</param>
        /// <returns>DataTable con la información del tipo de identificacion</returns>
        public DataTable Consultar_Tipo_Identificacion(int intTipoIdentificacionID)
        {
            return objTipoIdentificacionDALC.Consultar_Tipo_Identificacion(intTipoIdentificacionID);
        }

        public void Insertar_Tipo_Documento(string strCodigoTipo, string strNombreTipo)
        {
            objTipoIdentificacionDALC.Insertar_Tipo_Identificacion(strCodigoTipo, strNombreTipo);
        }

        public void Actualizar_Tipo_Documento(int iID, string strCodigoTipo, string strNombreTipo)
        {
            objTipoIdentificacionDALC.Actualizar_Tipo_Identificacion(iID, strCodigoTipo, strNombreTipo);
        }

        public void Eliminar_Tipo_Documento(int iID)
        {
            objTipoIdentificacionDALC.Eliminar_Tipo_Identificacion(iID);
        }

        #endregion
    }
}
