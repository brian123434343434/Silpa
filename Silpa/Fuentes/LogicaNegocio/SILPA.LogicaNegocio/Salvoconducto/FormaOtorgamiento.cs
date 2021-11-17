using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class FormaOtorgamiento
    {
        private FormaOtorgamientoDalc vFormaOtorgamientoDalc;

        public FormaOtorgamiento()
        {
            vFormaOtorgamientoDalc = new FormaOtorgamientoDalc();
        }

        public List<FormaOtorgamientoIdentity> ListaFormaOtorgamiento(int claseRecursoID, bool esSalvoconducto)
        {
            return vFormaOtorgamientoDalc.ListaFormaOtorgamiento(claseRecursoID, esSalvoconducto);
        }
    }
}
