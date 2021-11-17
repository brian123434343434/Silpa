using System;
using System.Collections.Generic;
using System.Text;

namespace Silpa.Workflow
{

    public enum ActividadSilpa
    {
        /// <summary>
        /// Actividades relacionadas en las tablas : GEN_WORKFLOW_ACTIVIDAD_SILPA - GEN_WORKFLOW_ACTIVIDAD_RELACIONADA de Silpa
        /// </summary>
        ValidarRequiereDAA = 1,
        ValidarRespuestaAutoridadAmbiental = 2,
        Radicar = 3,
        RegistrarInformacionDocumento = 4,
        ConsultarPago = 5,
        RecibirDatosSalvoconducto = 7
    }

}
