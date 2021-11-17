using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    public class DetalleTransaccionPSEIdentity
    {

        #region Propiedades

            /// <summary>
            /// Identificador de detalle de transaccion PSE
            /// </summary>
            public long DetalleTransaccionPSEID { get; set; }

            /// <summary>
            /// Identificador de la transaccion PSE
            /// </summary>
            public long TransaccionPSEID {get; set;}

            /// <summary>
            /// Origen de llamdo del servicio
            /// </summary>
            public string Origen { get; set; }

            /// <summary>
            /// Servicio llamado
            /// </summary>
            public string Servicio { get; set; }

            /// <summary>
            /// Resultado PSE
            /// </summary>
            public string ResultadoPSE { get; set; }

            /// <summary>
            /// Estado retornado por PSE
            /// </summary>
            public string EstadoPSE { get; set; }
            
            /// <summary>
            /// Estado interno
            /// </summary>
            public int EstadoID { get; set; }

            /// <summary>
            /// Fecha de transacción bancaria
            /// </summary>
            public DateTime FechaTransaccionBancaria { get; set; }

            /// <summary>
            /// Fecha de transaccion
            /// </summary>
            public DateTime FechaTransaccion { get; set; }


        #endregion

    }
}
