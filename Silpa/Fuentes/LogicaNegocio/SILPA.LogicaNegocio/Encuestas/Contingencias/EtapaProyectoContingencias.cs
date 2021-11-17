using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Contingencias.Entity;
using SILPA.AccesoDatos.Encuestas.Contingencias.Dalc;

namespace SILPA.LogicaNegocio.Encuestas.Encuesta
{
    public class EtapaProyectoContingencias
    {
        #region  Objetos

            private EtapaProyectoContingenciasDalc _objExpedienteDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public EtapaProyectoContingencias()
            {
                //Creary cargar configuración
                this._objExpedienteDalc = new EtapaProyectoContingenciasDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar el listado de etapas del proyecto que se encuentran activas
            /// </summary>
            /// <returns>List con la información de las etapas del proyecto que se encuentran activas</returns>
            public List<EtapaProyectoContingenciasEntity> ConsultarEtapasProyectoContingencias()
            {
                return this._objExpedienteDalc.ConsultarEtapasProyectoContingencias();
            }


            /// <summary>
            /// Consultar la información de una etapa del proyecto especificad
            /// </summary>
            /// <param name="p_intEtapaID">int con el identificador de la etapa</param>
            /// <returns>EtapaProyectoContingenciasEntity con la información de la etapa</returns>
            public EtapaProyectoContingenciasEntity ConsultarEtapaProyectoContingencias(int p_intEtapaID)
            {
                return this._objExpedienteDalc.ConsultarEtapaProyectoContingencias(p_intEtapaID);
            }

        #endregion
    }
}
