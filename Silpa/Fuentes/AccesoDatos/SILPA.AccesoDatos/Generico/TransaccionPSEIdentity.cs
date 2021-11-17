using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    public class TransaccionPSEIdentity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la transaccion PSE
            /// </summary>
            public long TransaccionPSEID {get; set;}

            /// <summary>
            /// Identificador del cobro al cual pertenece la transacción
            /// </summary>
            public long CobroID { get; set; }

            /// <summary>
            /// Número de referencia de pago
            /// </summary>
            public string NumeroReferencia { get; set; }

            /// <summary>
            /// Número Vital
            /// </summary>
            public string NumeroSilpa { get; set; }            

            /// <summary>
            /// Codigo PSE de entidad
            /// </summary>
            public string CodigoPSEEntidad { get; set; }

            /// <summary>
            /// Origen del cobro
            /// </summary>
            public string OrigenCobro { get; set; }            

            /// <summary>
            /// Fecha de solicitud
            /// </summary>
            public DateTime FechaSolicitud { get; set; }

            /// <summary>
            /// Tipo de persona con el cual se realizo la transacción
            /// </summary>
            public string TipoPersona { get; set; }
            
            /// <summary>
            /// Banco contra el cual se realiza la transacción
            /// </summary>
            public string Banco { get; set; }

            /// <summary>
            /// Valor de la transacción
            /// </summary>
            public decimal Valor { get; set; }

            /// <summary>
            /// Referencia 1 enviada en la transacción contra PSE
            /// </summary>
            public string Referencia1 { get; set; }

            /// <summary>
            /// Referencia 2 enviada en la transacción contra PSE
            /// </summary>
            public string Referencia2 { get; set; }

            /// <summary>
            /// Referencia 3 enviada en la transacción contra PSE
            /// </summary>
            public string Referencia3 { get; set; }

            /// <summary>
            /// Dirección IP desde la cual se realiza la transacción
            /// </summary>
            public string IPTransaccion { get; set; }

            /// <summary>
            /// Razón social del comercial desde el cual se realiza la transacción
            /// </summary>
            public string RazonSocialComercio { get; set; }

            /// <summary>
            /// Descripción de la transacción
            /// </summary>
            public string DescripcionTransaccion { get; set; }

            /// <summary>
            /// Url al cual debe retornar la transacción
            /// </summary>
            public string UrlRetorno { get; set; }

            /// <summary>
            /// Número de transacción (CUS)
            /// </summary>
            public long NumeroTransaccion { get; set; }

            /// <summary>
            /// URL al cual se envío transacción PSE
            /// </summary>
            public string UrlPSE { get; set; }

            /// <summary>
            /// int cobn el identificador de estado interno
            /// </summary>
            public int Estado { get; set; }

            /// <summary>
            /// Fecha de registro de transacción
            /// </summary>
            public DateTime FechaRegistroTransacion { get; set; }

            /// <summary>
            /// Fecha de última actualización del registro
            /// </summary>
            public DateTime FechaUltimaActualizacion { get; set; }


        #endregion

    }
}
