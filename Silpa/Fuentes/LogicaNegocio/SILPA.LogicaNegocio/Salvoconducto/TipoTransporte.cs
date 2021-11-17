using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class TipoTransporte
    {
        private TipoTransporteDalc vTipoTransporteDalc;

        public TipoTransporte()
        {
            vTipoTransporteDalc = new TipoTransporteDalc();
        }

        public List<TipoTransporteIdentity> ListaTipoTransportePorModoTransporte(int modoTransporteID)
        {
            return vTipoTransporteDalc.ListaTipoTransportePorModoTransporte(modoTransporteID);
        }
    }
}
