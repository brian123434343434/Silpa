using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class ClaseSalvoconducto
    {
        private ClaseSalvoconductoDalc vClaseSalvoconductoDalc;

        public ClaseSalvoconducto()
        {
            vClaseSalvoconductoDalc = new ClaseSalvoconductoDalc();
        }

        public List<ClaseSalvoconductoIdentity> ListaClaseSalvoconducto(int autID)
        {
            if(autID != 125 && autID != 117 && autID != 119)
                return vClaseSalvoconductoDalc.ListaClaseSalvoconducto().Where(x => x.ClaseSalvoconductoID != 1).ToList();
            else
                return vClaseSalvoconductoDalc.ListaClaseSalvoconducto();
        }

    }
}
