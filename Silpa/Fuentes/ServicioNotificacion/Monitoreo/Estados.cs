using System;
using System.Collections.Generic;
using System.Text;

namespace ServicioNotificacion.Monitoreo
{
    public class Estados
    {
        public enum EstadosMonitoreo { INICIADO, ENPROCESO, LIBERADO };
        public static int ConvertirTiempoAMinutos(int tiempo)
        {
            return tiempo * 60 * 1000;
        }
    }
}
