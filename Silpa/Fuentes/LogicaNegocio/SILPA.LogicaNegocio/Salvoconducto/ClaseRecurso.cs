using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class ClaseRecurso
    {
        private ClaseRecursoDalc vClaseRecursoDalc;

        public ClaseRecurso()
        {
            vClaseRecursoDalc = new ClaseRecursoDalc();
        }

        public List<ClaseRecursoIdentity> ListaClaseRecurso()
        {
            return vClaseRecursoDalc.ListaClaseRecurso();
        }
    }
}
