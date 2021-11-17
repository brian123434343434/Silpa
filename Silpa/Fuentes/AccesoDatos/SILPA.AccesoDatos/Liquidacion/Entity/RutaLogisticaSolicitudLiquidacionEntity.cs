using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Generico;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class RutaLogisticaSolicitudLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la ruta asociada a la solicitud de liquidación
            /// </summary>
            public int RutaLogisticaSolicitudID { get; set; }

            /// <summary>
            /// Identificador de la solicitud
            /// </summary>
            public int SolicitudLiquidacionID { get; set; }

            /// <summary>
            /// Medio de transporte
            /// </summary>
            public MedioTransporteLiquidacionEntity MedioTransporte { get; set; }

            /// <summary>
            /// Departamento desde el cual se sale
            /// </summary>
            public DepartamentoIdentity DepartamentoOrigen { get; set; }

            /// <summary>
            /// Municipio desde el cual se sale
            /// </summary>
            public MunicipioIdentity MunicipioOrigen { get; set; }

            /// <summary>
            /// Departamento al cual se llega
            /// </summary>
            public DepartamentoIdentity DepartamentoDestino { get; set; }

            /// <summary>
            /// Municipio donde al cual se llega
            /// </summary>
            public MunicipioIdentity MunicipioDestino { get; set; }

            /// <summary>
            /// Tiempo aproximado de trayecto
            /// </summary>
            public string TiempoAproximadoTrayecto { get; set; }

        #endregion
        
    }
}
