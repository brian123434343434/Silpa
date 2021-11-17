using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;
using SILPA.AccesoDatos.Encuestas.Encuesta.Dalc;

namespace SILPA.LogicaNegocio.Encuestas.Encuesta
{
    public class AutoridadAmbientalEncuestas
    {
        #region  Objetos

            private AutoridadAmbientalEncuestasDalc _objAutoridadAmbientalDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public AutoridadAmbientalEncuestas()
            {
                //Creary cargar configuración
                this._objAutoridadAmbientalDalc = new AutoridadAmbientalEncuestasDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar el listado de autoridades ambientales
            /// </summary>
            /// <returns>List con la información de las autoridades ambientales</returns>
            public List<AutoridadAmbientalEncuestasEntity> ConsultarAutoridadAmbientales()
            {
                return this._objAutoridadAmbientalDalc.ConsultarAutoridadAmbientales();
            }

        #endregion
    }
}
