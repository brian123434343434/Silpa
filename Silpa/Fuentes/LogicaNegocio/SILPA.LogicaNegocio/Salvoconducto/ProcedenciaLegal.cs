using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Salvoconducto;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class ProcedenciaLegal
    {
        private ProcedenciaLegalDalc vProcedenciaLegalDalc;

        public ProcedenciaLegal()
        {
            vProcedenciaLegalDalc = new ProcedenciaLegalDalc();
        }

        public List<ProcedenciaLegalIdentity> ListaProcedenciaLegal()
        {
            return vProcedenciaLegalDalc.ListaProcedenciaLegal();
        }
    }
}
