using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Liquidacion.Enum
{
    /// <summary>
    /// Lista de respuestas asociadas a la autoliquidacion
    /// </summary>
    public enum RespuestaAutoLiquidacionEnum
    {
        OK = 0,
        ERROR_PROCESO = 1,
        LIQUIDACION_EXISTENTE = 2,
        NO_EXISTEN_TABLAS_CONFIGURADAS = 3,
        EXISTE_MAS_UNA_TABLA__CONFIGURADA = 4,
        USUARIO_NO_EXISTE_SILA = 5

    }
}
