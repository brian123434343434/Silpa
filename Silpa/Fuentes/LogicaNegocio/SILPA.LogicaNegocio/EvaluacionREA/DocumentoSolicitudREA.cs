using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class DocumentoSolicitudREA
    {
        #region  Objetos

        private DocumentoSolicitudREADalc _objDocumentoSolicitudREADalc;

        #endregion

         #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public DocumentoSolicitudREA()
        {
            //Creary cargar configuración
            this._objDocumentoSolicitudREADalc = new DocumentoSolicitudREADalc();
        }

        #endregion
        #region  Metodos Publicos

        /// <summary>
        /// Actualizar el número de vital asociado a una solicitud
        /// </summary>
        /// <param name="p_intSolicitudID">int con el identificador de la solicitud</param>
        /// <param name="p_strNumeroVital">string con el número vital</param>
        public void ActualizarUbicacionDocumentoSolicitudREA(int p_intDocumentoSolicitudID, string p_strUbicacion, int p_intSolicitudREAID)
        {
            this._objDocumentoSolicitudREADalc.ActualizarUbicacionDocumentoSolicitudREA(p_intDocumentoSolicitudID, p_strUbicacion, p_intSolicitudREAID);
        }

        #endregion
    }
}
