using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;
using SILPA.AccesoDatos.Encuestas.Encuesta.Dalc;

namespace SILPA.LogicaNegocio.Encuestas.Encuesta
{
    public class FormularioEncuestas
    {
         #region  Objetos

            private FormularioEncuestasDalc _objFormularioDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public FormularioEncuestas()
            {
                //Creary cargar configuración
                this._objFormularioDalc = new FormularioEncuestasDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Obtener la información del formulario relacionado a un sector especifico
            /// </summary>
            /// <param name="p_intFormularioID">int con el identificador del formulario</param>
            /// <param name="p_intSectorID">int con el identificador del sector</param>
            /// <returns>FormularioCambioMenorEntity con la información del formulario</returns>
            public FormularioEncuestasEntity ConsultarFormularioSector(int p_intFormularioID, int p_intSectorID)
            {
                return this._objFormularioDalc.ConsultarFormularioSector(p_intFormularioID, p_intSectorID);
            }

        #endregion

    }
}
