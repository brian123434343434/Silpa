using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class LogSalvoconducto
    {
        private LogSalvoconductoDalc vLogSalvoconductoDalc;

        public LogSalvoconducto()
        {
            vLogSalvoconductoDalc = new LogSalvoconductoDalc();
        }

        public int InsertarLogSalvoconducto(string strMetodo, string strStackTrace)
        {
            return vLogSalvoconductoDalc.InsertarLogSalvoconducto(strMetodo, strStackTrace);
        }
    }
}
