using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.ServiciosREST.Entidades
{
    public class RespuestaServicioEntity
    {
        #region Propiedades

            /// <summary>
            /// Codigo de respuesta
            /// </summary>
            public CodigosRespuestaREST CodigoRespuesta { get; set; }

            /// <summary>
            /// Objeto que contiene la respuesta del servicio
            /// </summary>
            public object ObjetoRespuesta { get; set; }

        #endregion
    }
}
