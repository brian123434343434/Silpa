using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Generico;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class UbicacionSolicitudLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la ubicación de la solicitud
            /// </summary>
            public int UbicacionSolicitudID { get; set; }

            /// <summary>
            /// Identificador de la solicitud
            /// </summary>
            public int SolicitudLiquidacionID { get; set; }

            /// <summary>
            /// Departamento donde se ubica proyecto
            /// </summary>
            public DepartamentoIdentity Departamento { get; set; }

            /// <summary>
            /// Municipio donde se ubica proyecto
            /// </summary>
            public MunicipioIdentity Municipio { get; set; }

            /// <summary>
            /// Corregimiento donde se ubica proyecto
            /// </summary>
            public string Corregimiento { get; set; }

            /// <summary>
            /// Vereda donde se ubica proyecto
            /// </summary>
            public string Vereda { get; set; }

            /// <summary>
            /// Listado de coordenadas
            /// </summary>
            public List<CoordenadaUbicacionLiquidacionEntity> Coordenadas { get; set; }

        #endregion
        
    }
}
