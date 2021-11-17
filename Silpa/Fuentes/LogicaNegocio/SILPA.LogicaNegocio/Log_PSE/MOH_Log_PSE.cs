using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccesoDatos.Monitoreo_PSE;

namespace SILPA.LogicaNegocio.Log_PSE
{
    public class MOH_Log_PSE
    {
        private MOH_Log_PESE_DALC objMOHLogDALC;
       
        public MOH_Log_PSE()
        {
            objMOHLogDALC = new MOH_Log_PESE_DALC();
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
        public DataTable ConsultaLog(Int32 iNumTransaccion, string sFechaIniTransaccion,
                                          string sFechaFinTransaccion, string sCodBanco, int iAutoridadAmbiental,
                                          string sNumSilpa, string sNumExpediente, string sNumReferencia,
                                          string sFechaIniExpedicion, string sFechaFinExpedicion,
                                          string sFechaIniVencimiento, string sFechaFinVencimiento,
                                          Int32 iCandidato, Int32 sValor)
        {
            return objMOHLogDALC.ConsultaLogDALC(iNumTransaccion,sFechaIniTransaccion,sFechaFinTransaccion,sCodBanco,iAutoridadAmbiental,
                                          sNumSilpa,sNumExpediente,sNumReferencia,sFechaIniExpedicion,sFechaFinExpedicion,
                                          sFechaIniVencimiento,sFechaFinVencimiento,iCandidato,sValor);
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
