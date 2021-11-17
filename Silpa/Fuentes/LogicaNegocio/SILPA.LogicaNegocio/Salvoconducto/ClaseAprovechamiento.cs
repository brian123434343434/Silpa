using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class ClaseAprovechamiento
    {
        private ClaseAprovechamientoDalc vClaseAprovechamiento;

        public ClaseAprovechamiento()
        {
            vClaseAprovechamiento = new ClaseAprovechamientoDalc();
        }

        public List<ClaseAprovechamientoIdentity> ListaClaseAprovechamiento(int? claseRecursoId)
        {
            return vClaseAprovechamiento.ListaClaseAprovechamiento(claseRecursoId);
        }
    }
}
