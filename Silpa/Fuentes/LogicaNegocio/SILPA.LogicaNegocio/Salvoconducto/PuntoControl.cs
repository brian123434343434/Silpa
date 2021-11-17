using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class PuntoControl
    {
        private PuntoControlDalc vPuntoControlDalc;

        public PuntoControl()
        {
            vPuntoControlDalc = new PuntoControlDalc();
        }

        public List<PuntoControlIdentity> ListaPuntosControl(int pSalvoconductoID)
        {
            return vPuntoControlDalc.ListaPuntosControl(pSalvoconductoID);
        }
        public void InsertarPuntoControl(int logID, decimal latitud, decimal longitud, int orden)
        {
            vPuntoControlDalc.InsertarPuntoControl(logID, latitud, longitud, orden);
        }
    }
}
