using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccesoDatos.LOG;

namespace SILPA.LogicaNegocio.LOG
{
    public class SHMLog
    {
        private SMHLogDALC objSMHLogDALC;
        public SHMLog()
        {
            objSMHLogDALC = new SMHLogDALC();
        }

        /// <summary>
        /// Realiza la consulta del log
        /// </summary>
        /// <param name="strFechaIni">Parametro de la fecha inicial</param>
        /// <param name="strFechaFin">Parametro de la fecha final</param>
        /// <param name="strUsuarios">Parametro del usuario</param>
        /// <param name="strMaquina">Parametro del nombre de la maquina</param>
        /// <param name="iSeveridad">Parametro del id de la severidad</param>
        /// <returns></returns>
        public DataTable ConsultaLog(String strFechaIni, String strFechaFin, String strUsuarios, String strMaquina, Int32 iSeveridad)
        {
            return objSMHLogDALC.ConsultaLogDALC(strFechaIni, strFechaFin, strUsuarios, strMaquina, iSeveridad);
        }

        /// <summary>
        /// Metodo para la consulta de la severidad y cargar el combo
        /// </summary>
        /// <returns></returns>
        public DataTable ConsultaSeveridad()
        {
            return objSMHLogDALC.ConsultaSeveridadDALC();
        }
    }
}
