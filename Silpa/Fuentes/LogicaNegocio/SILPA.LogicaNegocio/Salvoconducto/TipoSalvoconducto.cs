using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Salvoconducto;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class TipoSalvoconducto
    {
        private TipoSalvoconductoDalc vTipoSalvoconductoDalc;

        public TipoSalvoconducto()
        {
            vTipoSalvoconductoDalc = new TipoSalvoconductoDalc();
        }

        public List<TipoSalvoconductoIdentity> ListaTipoSalvoconducto()
        {
            return vTipoSalvoconductoDalc.ListaTipoSalvoconducto();
        }
    }


}
