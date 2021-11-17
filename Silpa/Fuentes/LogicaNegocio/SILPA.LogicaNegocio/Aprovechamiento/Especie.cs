using SILPA.AccesoDatos.Aprovechamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Aprovechamiento
{
    public class Especie
    {
        private EspecieDalc vEspecieDalc;

        public Especie()
        {
            vEspecieDalc = new EspecieDalc();
        }
        public List<EspecieIdentity> ListaEspecie(string nombreCientifico, int claseRecursoId)
        {
            return vEspecieDalc.ListaEspecie(nombreCientifico, claseRecursoId);
        }
    }
}
