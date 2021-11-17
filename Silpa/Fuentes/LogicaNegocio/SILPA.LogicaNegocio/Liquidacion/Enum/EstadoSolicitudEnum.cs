using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Liquidacion.Enum
{
    /// <summary>
    /// Estados que puede tomar la solicitud de liquidación
    /// </summary>
    public enum EstadoSolicitudEnum
    {
	    Solicitud_Pendiente_Radicacion_Solicitud = 1,
        Solicitud_Pendiente_Generación_Cobro = 2,
        Solicitud_Radicada = 3,
        Solicitud_Pendiente_Pago = 4,
        Solicitud_Pagada = 5
    }
}
