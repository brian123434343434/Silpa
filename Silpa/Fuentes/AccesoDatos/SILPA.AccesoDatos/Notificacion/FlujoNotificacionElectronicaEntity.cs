using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    [Serializable]
    public class FlujoNotificacionElectronicaEntity
    {
        public int FlujoNotificacionElectronicaID { get; set; }
        public string FlujoNotificacionElectronica { get; set; }
        public int AutoridadAmbientalID { get; set; }
        public int EstadoInicialFlujoID { get; set; }
        public string EstadoInicialFlujo { get; set; }
    }
}
