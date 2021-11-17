using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using System.Collections;
using SoftManagement.Log;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;

namespace SILPA.LogicaNegocio.Monitoreo
{
    public class Monitoreo
    {
        public Monitoreo()
        {
        }

        /// <summary>
        /// Retorna el tiempo de espera para el ciclo del servicio de Pago
        /// </summary>
        /// <returns>Entero; representa minutos</returns>
        public int TomarTiempoEsperaPago()
        {
            SILPA.AccesoDatos.Monitoreo.MonitoreoPagoDalc dalc = new SILPA.AccesoDatos.Monitoreo.MonitoreoPagoDalc();
            return dalc.TiempoEsperaCiclo(); 
        }

        /// <summary>
        /// Retorna el tiempo de espera para el ciclo del servicio de EE
        /// </summary>
        /// <returns>Entero; representa minutos</returns>
        public int TomarTiempoEsperaEE()
        {
            SILPA.AccesoDatos.Monitoreo.MonitoreoEEDalc dalc = new SILPA.AccesoDatos.Monitoreo.MonitoreoEEDalc();
            return dalc.TiempoEsperaCiclo();
        }

        /// <summary>
        /// Verifica si se confirmo estado de pagos que se encuentren pendientes
        /// </summary>
        public bool ValidarPagos(bool p_blnLogActivo)
        {
            Cobro objCobro = null;
            List<TransaccionPSEIdentity> lstTransaccionesPendientes = null;
            WSPQ04.WSPQ04 objServicio = null;
            bool blnValido = true;
            string strMensaje = "";
            string strCUS = "";

            try
            {
                if (p_blnLogActivo)
                    SMLog.Escribir(Severidad.Informativo, "PSE:: Inicia Proceso Verificacion Pagos");

                if (p_blnLogActivo)
                    SMLog.Escribir(Severidad.Informativo, "PSE:: Consultar Procesos Pendientes");

                //Consultar transacciones pendientes
                objCobro = new Cobro();
                lstTransaccionesPendientes = objCobro.PagosPendientes();

                //Verificar que se obtenga datos
                if (lstTransaccionesPendientes != null && lstTransaccionesPendientes.Count > 0)
                {
                    if (p_blnLogActivo)
                        SMLog.Escribir(Severidad.Informativo, "PSE:: Inicia ciclo de verificación");

                    //Ciclo que recorre las transacciones pendientes
                    foreach (TransaccionPSEIdentity objTransaccion in lstTransaccionesPendientes)
                    {
                        if (p_blnLogActivo)
                            SMLog.Escribir(Severidad.Informativo, "PSE:: Verificar CUS: " + objTransaccion.NumeroTransaccion.ToString());

                        try
                        {
                            objServicio = new SILPA.LogicaNegocio.WSPQ04.WSPQ04();
                            objServicio.Credentials = Credenciales();
                            objServicio.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSPQ04");
                            strMensaje = objServicio.MonitorearPagoPSE(objTransaccion.NumeroTransaccion.ToString());
                        }
                        catch(Exception exc){
                            SMLog.Escribir(Severidad.Critico, "Servicio PSE - ValidarPagos :: Se presento error consultando la informacion CUS en servicio WS04: " + strCUS + " Error: " + exc.StackTrace.ToString());

                            blnValido = false;
                        }

                        if (p_blnLogActivo && blnValido)
                            SMLog.Escribir(Severidad.Informativo, "PSE:: Se realizo verificación CUS: " + objTransaccion.NumeroTransaccion.ToString() + " - Resultado: " + strMensaje);

                        //Verificar si se presento error en la  consulta de transacción
                        if (!string.IsNullOrEmpty(strMensaje))
                        {
                            SMLog.Escribir(Severidad.Critico, "Servicio PSE - ValidarPagos :: Se presento error consultando la informacion CUS: " + strCUS + " - Mensaje: " + strMensaje);
                            blnValido = false;
                        }
                    }
                }

            }
            catch(Exception exc){
                SMLog.Escribir(Severidad.Critico, "Servicio PSE - ValidarPagos :: Error realizando validacion de pagos " + exc.StackTrace.ToString());
                blnValido = false;
            }

            return blnValido;
        }


        public static System.Net.NetworkCredential Credenciales()
        {
            string user = System.Configuration.ConfigurationManager.AppSettings["usuario_servicio"].ToString();
            string pass = System.Configuration.ConfigurationManager.AppSettings["clave_servicio"].ToString();
            System.Net.NetworkCredential credencial = new System.Net.NetworkCredential(user, pass);
            return credencial;
        }

        public bool ValidarEE()
        {
            WSPQ02.WSPQ02 servicio = new SILPA.LogicaNegocio.WSPQ02.WSPQ02();
            servicio.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSPQ02");    
            return servicio.MonitorearRespuestaEE("");  
        }
    }
}
