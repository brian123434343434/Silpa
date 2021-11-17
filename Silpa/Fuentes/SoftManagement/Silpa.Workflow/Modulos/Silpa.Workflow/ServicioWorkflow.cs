using System;
using System.Collections.Generic;
using System.Text;
using Silpa.Workflow.AccesoDatos;
using System.Data;
using System.Collections.Specialized;
using Silpa.Workflow.BpmServices;
using Silpa.Workflow.Entidades;
using SoftManagement.Log;

namespace Silpa.Workflow
{
    public class ServicioWorkflow
    {
        public ActividadInfo ConsultarActividadActual(long processInstance)
        {
            try
            {
	            List<ActividadInfo> actividadInfo = ActividadSilpaDao.ConsultarActividadActual(processInstance);
	            if (actividadInfo.Count > 1)
	                throw new InvalidOperationException("El processInstance " + processInstance + " tiene más de una actividad actual.");
	            if (actividadInfo.Count == 1)
	                return actividadInfo[0];
	            return null;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Consultar Actividad Actual.";
                throw new Exception(strException, ex);
            }
        }

        private ActividadInfo ConsultarActividadInfoActual(long processInstance)
        {
            try
            {
	            List<ActividadInfo> actividadInfo = ActividadSilpaDao.ConsultarActividadActual(processInstance);
	            if (actividadInfo.Count > 1)
	                throw new InvalidOperationException("El processInstance " + processInstance + " tiene más de una actividad actual.");
	            if (actividadInfo.Count == 0)
	                throw new InvalidOperationException("El processInstance " + processInstance + " no tiene una actividad actual.");
	            if (actividadInfo.Count == 1)
	                return actividadInfo[0];
	            return null;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Consultar Actividad Info. Actual.";
                throw new Exception(strException, ex);
            }
        }

        public string ValidarActividadActual(long processInstance, string usuario, long actividadSilpa)
        {
            String mensaje = String.Empty;
            try
            {               
                ActividadInfo actividad = ConsultarActividadInfoActual(processInstance);
                
                if (actividad.ActividadSilpaId == null)
                {
                    mensaje = String.Format("VITAL. La actividad que se intenta finalizar no es la siguiente en el proceso. El sistema espera {0}. ", actividad.ActividadDescripcion);
                }
                else if (actividad.ActividadSilpaId.Value != (int)actividadSilpa)
                {
                    mensaje = String.Format("VITAL. El documento que intenta enviar no corresponede con la actividad actual -{0}-. ", actividad.ActividadDescripcion);
                }               
               
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, String.Format("ValidarActividadActual: {0} {1} {2}", processInstance, actividadSilpa, ex.ToString()));
                mensaje = ex.ToString();
            }
            return mensaje;
        }

        public string ValidarActividadActualCondicion(long processInstance, string usuario, string condicion)
        {
            String mensaje = String.Empty;
            try
            {
                ActividadInfo actividad = ConsultarActividadInfoActual(processInstance);

                if (actividad.ActividadSilpaId == null)
                {
                    mensaje = String.Format("VITAL. La actividad que se intenta finalizar no es la siguiente en el proceso. El sistema espera finalizar Condicion:[{0}].", actividad.ActividadDescripcion);
                }                                
                else if(!ConsultarActividadCumpleCondicion(actividad.ActivityId,condicion))
                {
                    mensaje = String.Format("VITAL. El documento que intenta enviar no corresponede con la actividad actual. Condicion:[{0}].", actividad.ActividadDescripcion);
                }

            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, String.Format("ValidarActividadActualCondicion: {0} {1} ", processInstance, ex.ToString()));
                mensaje = "Validar los pasos efectuados al Validar Actividad Actual Condición. " + ex.ToString();
            }
            return mensaje;
        }

        private bool ConsultarActividadCumpleCondicion(int? idactividad, string condicion)
        {
            return ActividadSilpaDao.ConsultarActividadCumpleCondicion(idactividad, condicion);
        }


        public void FinalizarTarea(long processInstance, ActividadSilpa actividadSilpa, string usuario)
        {
            this.FinalizarTarea(processInstance, actividadSilpa, usuario, null);
        }

        public void FinalizarTarea(long processInstance, ActividadSilpa actividadSilpa, string usuario, string condicion)
        {
            try
            {
                String mensaje = String.Empty;
                string respuesta;
                
                GattacaBPMServices9000 servicioBPM = new GattacaBPMServices9000();
                //servicioBPM.Credentials = Credenciales();

                servicioBPM.Url = SILPA.Comun.DireccionamientoWS.UrlWS("GattacaBpm");

                ActividadInfo actividad = this.ConsultarActividadInfoActual(processInstance);

                if (actividad.ActividadSilpaId == null)
                {
                    mensaje = String.Format("El processInstance {0} no se encuentra actualmente en la actividad Silpa {1}.  La actividad actual es {2} - {3} y NO es una actividad SILPA.", processInstance, actividadSilpa, actividad.ActivityId, actividad.ActividadDescripcion);
                }
                else
                {
                    if (actividad.ActividadSilpaId.Value != (int)actividadSilpa)
                    {
                        mensaje = String.Format("El processInstance {0} no se encuentra actualmente en la actividad Silpa {1}.  La actividad actual es {2} - {3}.  La actividad SILPA es {4} - {5}.", processInstance, actividadSilpa, actividad.ActivityId, actividad.ActividadDescripcion, actividad.ActividadSilpaId, actividad.ActividadSilpaDescripcion);
                    }
                }
                if (!String.IsNullOrEmpty(mensaje))
                {
                    throw new InvalidOperationException(mensaje);
                }
                string outComments = "CONDICION: " + condicion == "" ? "SIN CONDICION" : condicion;
                outComments += ".Usuario: " + usuario;
                string condicionTransicion = CondicionActual(actividad, condicion);
                respuesta = servicioBPM.EndActivityInstance(ServicioWorkflowConfig.Cliente, ApplicationUserDao.ObtenerIdUsuario(usuario), actividad.ActivityInstance.Value, processInstance, condicionTransicion, "TAREA FINALIZADA", outComments, "0", "0", "0");
                //JM 16/05/2013
                // Guarda Historial de la tarea finalizadad
              
                ActividadSilpaDao.InsertarHistorialFinalizarTareas(processInstance, (int)actividad.ActivityId, condicion, (int)actividad.ActivityInstance.Value);

                 //JM 23/04/2013
                 // Actualizar estado para procesos ciclicos
              
                SMLog.Escribir(Severidad.Informativo, String.Format("Actualiza Estado Ciclico: {0} {1} {2} {3}", processInstance, actividadSilpa, usuario, condicion));
              
                ActividadSilpaDao.ActualizarEstadoProcesosCiclicos(processInstance);

                //JM 04/12/2013
                // Actualizar el estado de las actividades paralelas

                //SMLog.Escribir(Severidad.Informativo, String.Format("ActualizarActividadesParalelas: {0} {1} {2} {3}", processInstance, actividadSilpa, usuario, condicion));
              
                //BandejaTareasDao.ActualizarActividadesParalelas(usuario);
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, String.Format("FinalizarTarea: {0} {1} {2} {3} {4}", processInstance, actividadSilpa, usuario, condicion, ex.ToString()));
                string strException = "Validar los pasos efectuados al Finalizar Tarea.";
                throw new Exception(strException, ex);
            }
        }

        private string CondicionActual(ActividadInfo actividad, string condicion)
        {
            try
            {
	            string codigoCondicion = "";
	            // Consultar las condiciones del ActivityId..
	            List<Condicion> condiciones = ActividadCondicionDao.ConsultarCondicionActividad(actividad.ActivityId.Value);
	            if (condiciones.Count == 0)
	                throw new InvalidOperationException("No existen condiciones para la actividad " + actividad.ActivityId + " " + actividad.ActividadDescripcion);

                if (condiciones.Count == 1 && (string.IsNullOrEmpty(condicion) || condiciones[0].CodigoCondicion == condicion))
	            {
	                return condiciones[0].IdCondicion.ToString();
	            }
                else if (condiciones.Count > 1 && !string.IsNullOrEmpty(condicion))
                {
                    foreach (Condicion cond in condiciones)
                    {
                        if (cond.CodigoCondicion == condicion)
                        {
                            codigoCondicion = cond.IdCondicion.ToString();
                            return codigoCondicion;
                        }
                    }
                    throw new InvalidOperationException("La condición " + condicion + " no existe para la actividad " + actividad.ActivityId + " " + actividad.ActividadDescripcion);
                }
                else
                {
                    if (string.IsNullOrEmpty(condicion))
                        throw new InvalidOperationException("No se especifico condición y existe mas de una condición para la actividad " + actividad.ActivityId + " " + actividad.ActividadDescripcion);
                    else
                        throw new InvalidOperationException("La condición " + condicion + " no existe para la actividad " + actividad.ActivityId + " " + actividad.ActividadDescripcion);
                }
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al definir la Consultar la Condición Actual de la Actividad.";
                throw new Exception(strException, ex);
            }
        }


        /// <summary>
        /// HAVA:05-ABR-2011
        /// Crea las credenciales para los servicios.
        /// </summary>
        /// <param name="user">usuario</param>
        /// <param name="password">clave</param>
        /// <returns></returns>
        //public static System.Net.NetworkCredential Credenciales()
        //{
        //    string user = System.Configuration.ConfigurationManager.AppSettings["usuario_servicio"].ToString();
        //    string pass = System.Configuration.ConfigurationManager.AppSettings["clave_servicio"].ToString();
        //    System.Net.NetworkCredential credencial = new System.Net.NetworkCredential(user, pass);
        //    return credencial;
        //}
    }
}
