using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using SoftManagement.CorreoElectronico;
using SoftManagement.Log;
using System.Configuration;

namespace ServicioSILPA
{
    public partial class ServicioSilpa : ServiceBase
    {
        #region variables de Monitoreo
            Monitoreo.Estados.EstadosMonitoreo _estadoMonitoreoPago = ServicioSILPA.Monitoreo.Estados.EstadosMonitoreo.INICIADO;
            Monitoreo.Estados.EstadosMonitoreo _estadoMonitoreoEE = ServicioSILPA.Monitoreo.Estados.EstadosMonitoreo.INICIADO;

        SILPA.LogicaNegocio.Monitoreo.Monitoreo _monitoreo;
        public bool tiempoEsperaActivo = false;

        #endregion
        public ServicioSilpa()
        {
            
            InitializeComponent();
            
        }

        private int estadoProceso;


        /// <summary>
        /// Retorna si el log indicado se encuentra activo
        /// </summary>
        /// <param name="p_strNombreLog">string con el nombre del log</param>
        /// <returns>bool indicando si se encuentra activo</returns>
        private bool LogActivo(string p_strNombreLog)
        {
            bool esActivo = false;
            string strValor = "";
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;
                
            //Obtener en que valor se encuentra el log            
            objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
            strValor = objParametrizacion.ObtenerValorParametroGeneral(-1, p_strNombreLog);

            //Verificar que se halla obtenido valor
            if (!string.IsNullOrEmpty(strValor))
                esActivo = (strValor == "0" ? false : true);

            return esActivo;
        }


        /// <summary>
        /// Obtener la hora de ejcución definalización de factura
        /// </summary>
        /// <returns>string con la hora de ejcución</returns>
        private string ObtenerHoraEjecutarFinFactura()
        {
            string strHora = "00";
            string strValor = "";
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;

            //Obtener en que valor se encuentra el log            
            objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
            strValor = objParametrizacion.ObtenerValorParametroGeneral(-1, "HORA_SILPA_FINALIZAR_FACTURAS_VENCIDAS");

            //Verificar que se halla obtenido valor
            if (!string.IsNullOrEmpty(strValor))
                strHora = strValor;

            return strHora;
        }


        /// <summary>
        /// Escribe mensaje en el log en caso de que el log se encuentre activo
        /// </summary>
        private void EscribirMensaje(Severidad severidad, string mensaje, bool logActivo)
        {
            if (logActivo)
                SMLog.Escribir(severidad, mensaje);
        }

        public void Onstart2(){

            this.OnStart(null);
        }


        protected override void OnStart(string[] args)
        {
            try
            {
                //Cargar si el log se encuentra activo
                bool blnActivo = this.LogActivo("LOG_SW_SILPA_INICIALIZAR");

                this.EscribirMensaje(Severidad.Informativo, "Se inició el ServicioSILPA", blnActivo);
                tmrTimer.Interval = ServicioSilpaConfig.Intervalo;
                tmrTimer.Enabled = true;
                TmrAlarmasActividadesPINES.Enabled = true;
                decimal min = 0m;
                int activo = 0;
                SILPA.LogicaNegocio.Parametrizacion.Parametrizacion parametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();


                this.EscribirMensaje(Severidad.Informativo, "Tiempo calculado para iniciar timer Notificación=" + parametrizacion.obtenerTiempoNotificacion(out activo), blnActivo);

                this.tiempoEsperaActivo = Convert.ToBoolean(activo);

                this.EscribirMensaje(Severidad.Informativo, "Tiempo Espera Activo: " + tiempoEsperaActivo.ToString(), blnActivo);

                min = Convert.ToInt32(parametrizacion.obtenerTiempoNotificacion(out activo));
                if (min > 0)
                {
                    //this.EscribirMensaje(Severidad.Informativo, "Se inicia el temporizador que actualiza el estado de notificación con intervalos de: " + timerActualizarEstado.Interval + " milisegundos ó " + min + " minutos");
                    //timerActualizarEstado.Interval = Convert.ToInt32(min * 60 * 1000);
                    //estadoProceso = (int)EstadosProceso.Iniciado;
                    //timerActualizarEstado.Enabled = true;
                    this.EscribirMensaje(Severidad.Informativo, "Se inicia el temporizador avanza los estados de las notificaciones de actos administrativos con intervalos de: " + timerActualizarEstado.Interval + " milisegundos ó " + min + " minutos", blnActivo);
                    TmrAvanzarEstados.Interval = Convert.ToInt32(min * 60 * 1000);
                    estadoProceso = (int)EstadosProceso.Iniciado;
                    TmrAvanzarEstados.Enabled = true;

                }

                _estadoMonitoreoPago = ServicioSILPA.Monitoreo.Estados.EstadosMonitoreo.INICIADO;
                _estadoMonitoreoEE = ServicioSILPA.Monitoreo.Estados.EstadosMonitoreo.INICIADO;
                _monitoreo = new SILPA.LogicaNegocio.Monitoreo.Monitoreo();
            }
            catch(Exception exc){
                SMLog.Escribir(Severidad.Critico, "Se ha presentado un error subiendo el servicio. " + exc.Message.ToString() + " " + exc.StackTrace.ToString());
            }
        }

        protected override void OnStop()
        {
            tmrTimer.Enabled = false;
            timerActualizarEstado.Enabled = false;
            TmrActualizarPago.Enabled = false;
            TmrAlarmasActividadesPINES.Enabled = false;
        }

        private void timerActualizarEstado_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool blnActivo = false;

            try
            {
                //Cargar si el log se encuentra activo
                blnActivo = this.LogActivo("LOG_SW_SILPA_ACTUALIZAR_ESTADO");

                this.EscribirMensaje(Severidad.Informativo, "Se inicia el Proceso de cambios de estados de notifiacion", blnActivo);
                this.EscribirMensaje(Severidad.Informativo, "-- Estado del proceso: " + estadoProceso.ToString(), blnActivo);
                this.EscribirMensaje(Severidad.Informativo, "-- EstadosProceso.Ejecutando: " + EstadosProceso.Ejecutando.ToString(), blnActivo);
                if (estadoProceso != (int)EstadosProceso.Ejecutando)
                {
                    estadoProceso = (int)EstadosProceso.Ejecutando;
                    // si el tiempo de espera se enuentra activo: 
                    if (this.tiempoEsperaActivo) 
                    { 

                        SILPA.Servicios.NotificacionFachada not = new SILPA.Servicios.NotificacionFachada();
                        //SILPA.LogicaNegocio.Notificacion.Notificacion notejec = new SILPA.LogicaNegocio.Notificacion.Notificacion();
                        List<string> lista = new List<string>();

                        string resultado = not.ComponenteNot("consultar", "", blnActivo);
                        this.EscribirMensaje(Severidad.Informativo, "Se inicia el Proceso de cambios de estados de notifiacion", blnActivo);
                        lista = not.ActualizarProcesos(blnActivo);
                        this.EscribirMensaje(Severidad.Informativo, "Resultado de la Operación de Notificación:" + resultado, blnActivo);
                    }
                    else
                    {
                        SMLog.Escribir(Severidad.Advertencia, "El tiempo de espera se ecuentra inactivo ");
                    }

                }
                else
                {
                    SMLog.Escribir(Severidad.Advertencia, "Falla ejecución de consulta pues actualmente se encuentra en estado=" + estadoProceso);
                }


            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "hubo un error al actualizar los estados. Trace: " + ex.StackTrace);
            }
            finally
            {
                estadoProceso = (int)EstadosProceso.Disponible;
                this.EscribirMensaje(Severidad.Informativo, "Finaliza proceso de consulta notificación con estado=" + estadoProceso, blnActivo);
            }
            
        }

        private enum EstadosProceso
        {
            Iniciado=0,
            Ejecutando=1,
            Disponible=2
        }
        
        private void EnviarCorreoElectronico()
        {
            ServicioEnvio envioCorreo = new ServicioEnvio();
            envioCorreo.Enviar();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                tmrTimer.Enabled = false;                
                EnviarCorreoElectronico();
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "Error en el Servicio Correo Silpa" + ex.ToString());
            }
            finally
            {
                tmrTimer.Enabled = true;
            }
        }


        /// <summary>
        /// Actualizar el estado de los pagos PSE
        /// </summary>
        private void TmrActualizarPago_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool blnActivo = false;

            try
            {
                //Cargar si el log se encuentra activo
                blnActivo = this.LogActivo("LOG_PSE_S_Resultado_Pago");

                if(blnActivo)
                    SMLog.Escribir(Severidad.Informativo, "PSE:: Inicia Proceso Verificacion Pagos");

                //Consultar intervalo de timer de ejecución de proceso
                _monitoreo = new SILPA.LogicaNegocio.Monitoreo.Monitoreo();
                TmrActualizarPago.Interval = Monitoreo.Estados.ConvertirTiempoAMinutos(_monitoreo.TomarTiempoEsperaPago());
                TmrActualizarPago.Enabled = true;
                _monitoreo.ValidarPagos(blnActivo);

                if (blnActivo)
                    SMLog.Escribir(Severidad.Informativo, "PSE :: Finaliza Proceso Verificacion Pagos");
                
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "Error en el Servicio Silpa Pago" + ex.ToString());
            }
            finally
            {
                TmrActualizarPago.Enabled = true;
            }        
        }

        private void TmrActualizaEE_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool blnActivo = false;

            try
            {
                //Cargar si el log se encuentra activo
                blnActivo = this.LogActivo("LOG_SW_SILPA_ACTUALIZA_EE");

                if (_estadoMonitoreoEE == ServicioSILPA.Monitoreo.Estados.EstadosMonitoreo.INICIADO || _estadoMonitoreoEE == ServicioSILPA.Monitoreo.Estados.EstadosMonitoreo.LIBERADO)
                {
                    //Cambia de estado
                    _estadoMonitoreoEE = ServicioSILPA.Monitoreo.Estados.EstadosMonitoreo.ENPROCESO;
                    //Debe especificar las el nuevo parametro de Timer para el ciclo
                    TmrActualizaEE.Interval = Monitoreo.Estados.ConvertirTiempoAMinutos(_monitoreo.TomarTiempoEsperaPago());
                    TmrActualizaEE.Enabled = true;
                    this.EscribirMensaje(Severidad.Informativo, "Inicia el proceso de validación de EE", blnActivo);
                    if (!_monitoreo.ValidarEE())
                        SMLog.Escribir(Severidad.Advertencia, "No se realizó la validacion del pago; Por favor revisar el proceso validación EE");
                    _estadoMonitoreoEE = ServicioSILPA.Monitoreo.Estados.EstadosMonitoreo.LIBERADO;
                }
            }
            catch (Exception ex)
            {
                SMLog.Escribir(ex);
                _monitoreo = new SILPA.LogicaNegocio.Monitoreo.Monitoreo();
                SMLog.Escribir(Severidad.Critico, "Se ha sobre escrito el Objeto de monitoreo EE");
                _estadoMonitoreoEE = ServicioSILPA.Monitoreo.Estados.EstadosMonitoreo.LIBERADO;
            }
        }

        private void TmrAvanzarEstados_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool blnActivo = false;

            try
            {
                //Cargar si el log se encuentra activo
                blnActivo = this.LogActivo("LOG_SW_SILPA_AVANZAR_ESTADOS");

                if (estadoProceso != (int)EstadosProceso.Ejecutando)
                {
                    estadoProceso = (int)EstadosProceso.Ejecutando;
                    // si el tiempo de espera se enuentra activo: 
                    if (this.tiempoEsperaActivo)
                    {
                        ///TODO: ACA VA LA LOGICA PARA EL AVANCE AUTOMATICO DE LOS ESTADOS DE NOTIFICACION DE LOS ACTOS ADMINISTRATIVO QUE SE ENCUENTRAN EN ESTADO PENDIENTE_DE_ACUSE_DE_NOTIFICACIÓN
                        ///ID_ESTADO: 1
                        this.EscribirMensaje(Severidad.Informativo, "Se inicia el Proceso de cambios de estados de notifiacion Electronica", blnActivo);
                        this.EscribirMensaje(Severidad.Informativo, "-- Estado del proceso: " + estadoProceso.ToString(), blnActivo);
                        this.EscribirMensaje(Severidad.Informativo, "-- EstadosProceso.Ejecutando: " + EstadosProceso.Ejecutando.ToString(), blnActivo);
                        SILPA.Servicios.Notificacion.NotificacionElectronica notelectronica = new SILPA.Servicios.Notificacion.NotificacionElectronica();
                        notelectronica.AvanzarEstadosNotificacion(blnActivo);
                        this.EscribirMensaje(Severidad.Informativo, "Finaliza el Proceso de cambios de estados de notifiacion Electronica", blnActivo);
                    }
                    else
                    {
                        SMLog.Escribir(Severidad.Advertencia, "El tiempo de espera se ecuentra inactivo ");
                    }

                }
                else
                {
                    SMLog.Escribir(Severidad.Advertencia, "Falla ejecución de consulta pues actualmente se encuentra en estado=" + estadoProceso);
                }


            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "hubo un error al actualizar los estados de notifiacion Electronica. Trace: " + ex.StackTrace);
            }
            finally
            {
                estadoProceso = (int)EstadosProceso.Disponible;
                this.EscribirMensaje(Severidad.Informativo, "Finaliza proceso de de notifiacion Electronica con estado=" + estadoProceso, blnActivo);
            }
        }

        private void TmrAlarmasActividadesPINES_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool blnActivo = false;

            try
            {
                //Cargar si el log se encuentra activo
                blnActivo = this.LogActivo("LOG_SW_SILPA_ALARMAS_ACTIVAS_PINES");

                if (Convert.ToBoolean(ConfigurationManager.AppSettings["GeneraAlertasPINES"]))
                {
                    int diasAlarmaVencimiento = Convert.ToInt32(ConfigurationManager.AppSettings["DIAS_ALARMA_VENCIMIENTO_PINES"]);
                    string horaEjecucionProceso = ConfigurationManager.AppSettings["HORA_EJECUCION_PROCESO_PINES"].ToString();
                    DateTime hoy = DateTime.Now;
                    if (hoy.Hour == Convert.ToInt32(horaEjecucionProceso.Split(':')[0]) && hoy.Minute == Convert.ToInt32(horaEjecucionProceso.Split(':')[1]))
                    {
                        this.EscribirMensaje(Severidad.Informativo, "Se inicia el Proceso de AlarmasActividadesPINES", blnActivo);
                        this.EscribirMensaje(Severidad.Informativo, "-- Estado del proceso: " + estadoProceso.ToString(), blnActivo);
                        this.EscribirMensaje(Severidad.Informativo, "-- EstadosProceso.Ejecutando: " + EstadosProceso.Ejecutando.ToString(), blnActivo);

                        SILPA.LogicaNegocio.PINES.Alarmas alarmas = new SILPA.LogicaNegocio.PINES.Alarmas();
                        alarmas.NotificarActividadesProximasAVencerce(diasAlarmaVencimiento);
                        this.EscribirMensaje(Severidad.Informativo, "Finaliza el Proceso de AlarmasActividadesPINES", blnActivo);
                    }
                }
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "hubo un error al ejecutar el proceso AlarmasActividadesPINES. Trace: " + ex.StackTrace);
            }
            finally
            {
                this.EscribirMensaje(Severidad.Informativo, "Finaliza proceso de AlarmasActividadesPINES", blnActivo);
            }
        }

    }
}
