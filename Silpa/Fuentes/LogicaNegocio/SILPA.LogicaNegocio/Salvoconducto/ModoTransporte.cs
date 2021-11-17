using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class ModoTransporte
    {
        private ModoTransporteDalc vModoTransporteDalc;

        public ModoTransporte()
        {
            vModoTransporteDalc = new ModoTransporteDalc();
        }

        public List<ModoTransporteIdentity> ListaModoTransporte()
        {
            return vModoTransporteDalc.ListaModoTransporte();
        }
    }
}
