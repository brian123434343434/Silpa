using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class ModoAdquisicionRecurso
    {
        private ModoAdquisicionRecursoDalc vModoAdquisicionRecursoDalc;

        public ModoAdquisicionRecurso()
        {
            vModoAdquisicionRecursoDalc = new ModoAdquisicionRecursoDalc();
        }

        public List<ModoAdquisicionRecursoIdentity> ListaModoAdquisicionRecurso(int? claseRecursoId, bool esSalvoconducto, int? formaOtorgamientoId)
        {
            return vModoAdquisicionRecursoDalc.ListaModoAdquisionRecurso(claseRecursoId, esSalvoconducto, formaOtorgamientoId);
        }
    }
}
