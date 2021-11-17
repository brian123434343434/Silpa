using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccesoDatos.Monitoreo_AA_PDI;

namespace SILPA.LogicaNegocio.Log_AA_PDI
{
    public class MOH_Log_AA_PDI
    {
        private MOH_Log_AA_PDI_DALC objMOHLogDALC;
       
        public MOH_Log_AA_PDI()
        {
            objMOHLogDALC = new MOH_Log_AA_PDI_DALC();
        }

        /// <summary>
        /// Realiza la consulta del log para AA y PDI
        /// </summary>
        /////// <param name="strFechaIni">Parametro de la fecha inicial</param>
        /////// <param name="strFechaFin">Parametro de la fecha final</param>
        /////// <param name="sNombreWS">Parametro Nombre Webservices</param>
        /////// <param name="sNomMetodo">Parametro Nombre del Metodo</param>
        /////// <param name="iSeveridad">Parametro Severidad</param>
        /////// <param name="sMensaje">Parametro Mensaje</param>
        /////// <param name="sNoVital">Parametro Número Vital</param>
        /////// <param name="iAA">Parametro Autoridad Ambiental</param>
        /////// <param name="iIdPadre">Parametro Id Padre</param>
        /// <returns></returns>
        public DataTable ConsultaLog(string sFechaIni, string sFechaFin,string sNombreWS,string sNomMetodo,int iSeveridad,string sMensaje,string sNoVital,int iAA,int iIdPadre)
        {
            return objMOHLogDALC.ConsultaLogDALC(sFechaIni, sFechaFin, sNombreWS, sNomMetodo, iSeveridad, sMensaje,sNoVital,iAA,iIdPadre);
        }

        ///////// <summary>
        ///////// Metodo para la consulta de la severidad y cargar el combo
        ///////// </summary>
        ///////// <returns></returns>
        //////public DataTable ConsultaSeveridad()
        //////{
        //////    return objSMHLogDALC.ConsultaSeveridadDALC();
        //////}
    }
}
