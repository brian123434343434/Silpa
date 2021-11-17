using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Contingencias.Entity;
using SILPA.AccesoDatos.Encuestas.Contingencias.Dalc;

namespace SILPA.LogicaNegocio.Encuestas.Encuesta
{
    public class NivelEmergenciaContingencias
    {
        #region  Objetos

            private NivelEmergenciaContingenciasDalc _objExpedienteDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public NivelEmergenciaContingencias()
            {
                //Creary cargar configuración
                this._objExpedienteDalc = new NivelEmergenciaContingenciasDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar el listado de niveles de emergencia que se encuentran activas
            /// </summary>
            /// <returns>List con la información de los niveles de emergencia</returns>
            public List<NivelEmergenciaContingenciasEntity> ConsultarNivelesEmergenciaContingencias()
            {
                return this._objExpedienteDalc.ConsultarNivelesEmergenciaContingencias();
            }


            /// <summary>
            /// Consultar la información de un nivel de emergencia especificado
            /// </summary>
            /// <param name="p_intNivelEmergenciaID">int con el identificador del nivel de emergencia</param>
            /// <returns>NivelEmergenciaContingenciasEntity con la información del nivel de emergencia</returns>
            public NivelEmergenciaContingenciasEntity ConsultarNivelEmergenciaContingencias(int p_intNivelEmergenciaID)
            {
                return this._objExpedienteDalc.ConsultarNivelEmergenciaContingencias(p_intNivelEmergenciaID);
            }

        #endregion
    }
}
