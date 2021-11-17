using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;
using SILPA.AccesoDatos.Encuestas.Encuesta.Dalc;

namespace SILPA.LogicaNegocio.Encuestas.Encuesta
{
    public class SectorEncuestas
    {
        #region  Objetos

            private SectorEncuestasDalc _objSectorDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public SectorEncuestas()
            {
                //Creary cargar configuración
                this._objSectorDalc = new SectorEncuestasDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los sectores que se pueden asociar a una liquidación
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los sectores activas o inactivas. Opcional </param>
            /// <returns>List con la información de los sectores</returns>
            public List<SectorEncuestasEntity> ConsultarSectores(bool? p_blnActivo = null)
            {
                return this._objSectorDalc.ConsultarSectores(p_blnActivo);
            }

        #endregion
    }
}
