using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using SILPA.LogicaNegocio;
using SILPA.Servicios;
using SoftManagement.Log;
using SoftManagement.CorreoElectronico;
using SoftManagement.ServicioCorreoElectronico;            


namespace ServicioNotificacion
{
    public partial class ServicioNotificacion : ServiceBase
    {
        private int estadoProceso;

        private enum EstadosProceso
        {
            Iniciado = 0,
            Ejecutando = 1,
            Disponible = 2
        }


        public ServicioNotificacion()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion parametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
            int _activo = 0;
            int min = Convert.ToInt32(parametrizacion.obtenerTiempoNotificacion(out _activo));
            if (min > 0)
            {
                timerActualizarEstado.Interval = Convert.ToInt32(min * 60 * 1000);
                estadoProceso = (int)EstadosProceso.Iniciado;
                timerActualizarEstado.Enabled = true;
                SMLog.Escribir(Severidad.Informativo, "Se inicia el temporizador que actualiza el estado de notificación con intervalos de: " + timerActualizarEstado.Interval + " milisegundos ó " + min + " minutos");
            }
        }

        private void timerActualizarEstado_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (estadoProceso != (int)EstadosProceso.Ejecutando)
                {
                    estadoProceso = (int)EstadosProceso.Ejecutando;
                    SILPA.Servicios.NotificacionFachada not = new SILPA.Servicios.NotificacionFachada();
                    //SILPA.LogicaNegocio.Notificacion.Notificacion notejec = new SILPA.LogicaNegocio.Notificacion.Notificacion();
                    List<string> lista = new List<string>();

                    string resultado = not.ComponenteNot("consultar", "");
                    lista = not.ActualizarProcesos();
                    SMLog.Escribir(Severidad.Informativo, "Resultado de la Operación de Notificación:" + resultado);
                }
                else
                {
                    SMLog.Escribir(Severidad.Advertencia, "Falla ejecución de consulta pues actualmente se encuentra en estado=" + estadoProceso);
                }
            }
            catch (Exception ex)
            {
                //SMLog.Escribir(Severidad.Critico, "hubo un error al actualizar los estados. Trace: " + ex.StackTrace);
                
            }
            finally
            {
                estadoProceso = (int)EstadosProceso.Disponible;
                SMLog.Escribir(Severidad.Informativo, "Finaliza proceso de consulta notificación con estado=" + estadoProceso);
            }
        }
    }
}
