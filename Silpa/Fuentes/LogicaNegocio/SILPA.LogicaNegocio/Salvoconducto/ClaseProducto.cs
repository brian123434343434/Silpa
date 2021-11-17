using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class ClaseProducto
    {
        private ClaseProductoDalc vClaseProductoDalc;

        public ClaseProducto()
        {
            vClaseProductoDalc = new ClaseProductoDalc();
        }

        public List<ClaseProductoIdentity> ListaClaseProducto(int claseRecursoId, bool esSalvoconducto)
        {
            return vClaseProductoDalc.ListaClaseProducto(claseRecursoId, esSalvoconducto);
        }
    }
}
