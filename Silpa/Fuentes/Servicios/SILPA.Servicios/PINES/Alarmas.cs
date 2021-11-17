using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.LogicaNegocio.PINES;

namespace SILPA.Servicios.PINES
{
    public class Alarmas
    {
        SILPA.LogicaNegocio.PINES.Alarmas alarmaBLL;

        public Alarmas()
        {
            alarmaBLL = new LogicaNegocio.PINES.Alarmas();
        }

        public void NotificarCrearcionActivityInstance(Int64 UserID, Int64 IdProcessInstance)
        {
            // Consultamos si el proyecto pertence a PINES
            alarmaBLL.NotificarCrearcionActivityInstance(UserID, IdProcessInstance);
        }
        public void diaAlarmaVencimiento(int diaAlarmaVencimiento)
        {
            alarmaBLL.NotificarActividadesProximasAVencerce(diaAlarmaVencimiento);
        }
    }
}
