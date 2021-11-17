using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class FinalidadAprovechamiento
    {
        private FinalidadRecursoDalc vFinalidadRecursoDalc;

        public FinalidadAprovechamiento()
        {
            vFinalidadRecursoDalc = new FinalidadRecursoDalc();
        }

        public List<FinalidadRecursoIdentity> ListaFinalidadAprovechamiento(int? claseRecursoId)
        {
            return vFinalidadRecursoDalc.ListaFinalidadRecurso(claseRecursoId);
        }
    }
}
