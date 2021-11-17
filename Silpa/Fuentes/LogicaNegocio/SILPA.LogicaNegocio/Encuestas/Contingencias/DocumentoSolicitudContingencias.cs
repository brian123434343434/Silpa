using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Contingencias.Entity;
using SILPA.AccesoDatos.Encuestas.Contingencias.Dalc;

namespace SILPA.LogicaNegocio.Encuestas.Contingencias
{
    public class DocumentoSolicitudContingencias
    {
        #region  Objetos

        private DocumentoPreguntaSolicitudContingenciasDalc _objDocumentoSolicitudDalc;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public DocumentoSolicitudContingencias()
        {
            //Creary cargar configuración
            this._objDocumentoSolicitudDalc = new DocumentoPreguntaSolicitudContingenciasDalc();
        }

        #endregion

        #region  Metodos Publicos

        /// <summary>
        /// Actualizar el número de vital asociado a una solicitud
        /// </summary>
        /// <param name="p_intSolicitudID">int con el identificador de la solicitud</param>
        /// <param name="p_strNumeroVital">string con el número vital</param>
        public void ActualizarUbicacionDocumentoSolicitudContingencia(int p_intDocumentoPreguntaContingenciaSolicitudID, string p_strUbicacion)
        {
            this._objDocumentoSolicitudDalc.ActualizarUbicacionDocumentoSolicitudContingencia(p_intDocumentoPreguntaContingenciaSolicitudID, p_strUbicacion);
        }

        #endregion
    }
}
