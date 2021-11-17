using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;
using SILPA.AccesoDatos.Encuestas.Encuesta.Dalc;

namespace SILPA.LogicaNegocio.Encuestas.Encuesta
{
    public class ExpedienteEncuestas
    {
        #region  Objetos

            private ExpedienteEncuestasDalc _objExpedienteDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public ExpedienteEncuestas()
            {
                //Creary cargar configuración
                this._objExpedienteDalc = new ExpedienteEncuestasDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los expedientes asociados a un solicitante pertenecientes a un sector y entidad especificos
            /// </summary>
            /// <param name="p_intSolicitanteID">int con el identificador del solicitante</param>
            /// <param name="p_intSectorID">int con el identificador del sector. Opcional</param>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental. Opcional</param>
            /// <returns>List con la información de los expedientes</returns>
            public List<ExpedienteEncuestasEntity> ConsultarExpedientesSolicitanteSectorAutoridad(int p_intSolicitanteID, int p_intSectorID = -1, int p_intAutoridadID = -1)
            {
                return this._objExpedienteDalc.ConsultarExpedientesSolicitanteSectorAutoridad(p_intSolicitanteID, p_intSectorID, p_intAutoridadID);
            }


            /// <summary>
            /// Consultar la información del expediente especificado perteneciente a una autoridad ambiental
            /// </summary>
            /// <param name="p_strCodigoExpediente">string con el codigo del expediente</param>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad</param>
            /// <returns>ExpedienteCambioMenorEntity con la información del expediente</returns>
            public ExpedienteEncuestasEntity ConsultarExpedienteAutoridad(string p_strCodigoExpediente, int p_intAutoridadID)
            {
                return this._objExpedienteDalc.ConsultarExpedienteAutoridad(p_strCodigoExpediente, p_intAutoridadID);
            }

        #endregion
    }
}
