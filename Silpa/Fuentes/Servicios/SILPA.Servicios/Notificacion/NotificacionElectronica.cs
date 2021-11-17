using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Notificacion;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;
using System.Data;
using System.Configuration;

namespace SILPA.Servicios.Notificacion
{
    public class NotificacionElectronica
    {

        /// <summary>
        /// Avanzar los estados pendientes de un flujo a su estado siguiente de manera automatica
        /// </summary>
        /// <param name="p_blnLogActivo">bool que indica si se encuentra activo el log</param>
        public void AvanzarEstadosNotificacion(bool p_blnLogActivo = false)
        {
            SILPA.LogicaNegocio.Notificacion.Notificacion objNotificacion = null;
            SILPA.Servicios.NotificacionFachada objNotificacionFachada = null;
            SILPA.LogicaNegocio.Notificacion.PersonaNotificar objPersonaNotificar = null;
            DataTable objEstadosAvanzar = null;
            bool blnEnviarCorreo = false;
            string strTextoCorreo = "";
            DataSet objListaCorreos = null;
            List<CorreoNotificacionEntity> objLstCorreos = null;

            try
            {
                if (p_blnLogActivo)
                    SMLog.Escribir(Severidad.Informativo, "-- Inicia proceso AvanzarEstadosNotificacion() --");

                if (p_blnLogActivo)
                    SMLog.Escribir(Severidad.Informativo, "-- Consultar el listado de estados que deben avanzar a siguiente estado de manera automatica --");
                objPersonaNotificar = new LogicaNegocio.Notificacion.PersonaNotificar();
                objEstadosAvanzar = objPersonaNotificar.ObtenerListadoEstadosPendientesAvanceAutomatico();

                //Verificar que existea información
                if (objEstadosAvanzar != null && objEstadosAvanzar.Rows.Count > 0)
                {
                    //Se crean objetos transacciones
                    objNotificacionFachada = new SILPA.Servicios.NotificacionFachada();
                    objNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();

                    if (p_blnLogActivo)
                        SMLog.Escribir(Severidad.Informativo, "-- Ingresa a ciclo que realiza avance de estados de acuerdo a su configuración--");
                    foreach (DataRow objEstadoAvanzar in objEstadosAvanzar.Rows)
                    {
                        try
                        {
                            if (p_blnLogActivo)
                                SMLog.Escribir(Severidad.Informativo, string.Format("-- Verificar envío de correo automatico. ID_PERSONA: {0} - CORREO AUTOMATICO: {1}", objEstadoAvanzar["ID_PERSONA"].ToString(), objEstadoAvanzar["ENVIO_CORREO_AVANCE_AUTOMATICO"].ToString()));

                            //Validar si envía correo automativo
                            if (objEstadoAvanzar["ENVIO_CORREO_AVANCE_AUTOMATICO"] != System.DBNull.Value && Convert.ToBoolean(objEstadoAvanzar["ENVIO_CORREO_AVANCE_AUTOMATICO"]))
                            {
                                //Consultar listado de direcciones de correo
                                if (p_blnLogActivo)
                                    SMLog.Escribir(Severidad.Informativo, string.Format("-- Consulta listado de correos posibles. ID_PERSONA: {0}", objEstadoAvanzar["ID_PERSONA"].ToString()));

                                objListaCorreos = objPersonaNotificar.ObtenerListadoCorreosNotificar(Convert.ToInt64(objEstadoAvanzar["ID_PERSONA"]));

                                //Validar que si se tiene correo
                                if (objListaCorreos != null && objListaCorreos.Tables.Count > 0 && objListaCorreos.Tables[0].Rows.Count > 0)
                                {
                                    //Consultar listado de direcciones de correo
                                    if (p_blnLogActivo)
                                        SMLog.Escribir(Severidad.Informativo, string.Format("-- Se carga correo. ID_PERSONA: {0} - CORREO: {1}", objEstadoAvanzar["ID_PERSONA"].ToString(),  objListaCorreos.Tables[0].Rows[0]["PER_CORREO_ELECTRONICO"].ToString()));

                                    //Adicionar correo
                                    objLstCorreos = new List<CorreoNotificacionEntity>();
                                    objLstCorreos.Add(new CorreoNotificacionEntity { PersonaID = Convert.ToInt64(objEstadoAvanzar["ID_PERSONA"]), Correo = objListaCorreos.Tables[0].Rows[0]["PER_CORREO_ELECTRONICO"].ToString() });

                                    //Cargar texto de correo
                                    strTextoCorreo = (objEstadoAvanzar["TEXTO_CORREO_AVANCE_AUTOMATICO"] != System.DBNull.Value ? objEstadoAvanzar["TEXTO_CORREO_AVANCE_AUTOMATICO"].ToString() : "");

                                    //Marcar como envío de correo
                                    blnEnviarCorreo = true;
                                }
                                else
                                {
                                    throw new Exception("No se encontro un correo configurado para la persona " + objEstadoAvanzar["ID_PERSONA"].ToString());
                                }
                            }
                            else
                            {
                                blnEnviarCorreo = false;
                                strTextoCorreo = "";
                                objLstCorreos = null;
                            }

                            //Avanzar estado
                            objNotificacion.CrearEstadoPersonaActo(Convert.ToInt64(objEstadoAvanzar["ID_ACTO_NOTIFICACION"]), Convert.ToInt32(objEstadoAvanzar["ID_FLUJO_NOT_ELEC"]), Convert.ToInt32(objEstadoAvanzar["ID_ESTADO_PADRE"]), Convert.ToInt32(objEstadoAvanzar["ID_ESTADO"]),
                                                                   Convert.ToInt64(objEstadoAvanzar["ID_PERSONA"]), Convert.ToInt32(objEstadoAvanzar["ID_AUTORIDAD"]), Convert.ToDateTime(objEstadoAvanzar["FECHA_ACTUAL"]), objEstadoAvanzar["NOT_NUMERO_SILPA"].ToString(), "Avance autómatico Publicidad Actos Administrativos - VITAL", "",
                                                                   objEstadoAvanzar["NOT_PROCESO_ADMINISTRACION"].ToString(), objEstadoAvanzar["NOT_NUMERO_ACTO_ADMINISTRATIVO"].ToString(), "", null, false, null,
                                                                   blnEnviarCorreo, objLstCorreos, strTextoCorreo, (objEstadoAvanzar["PERMITIR_ANEXAR_ACTO"] != System.DBNull.Value ? Convert.ToBoolean(objEstadoAvanzar["PERMITIR_ANEXAR_ACTO"]) : false), (objEstadoAvanzar["PERMITIR_ANEXAR_CONCEPTOS"] != System.DBNull.Value ? Convert.ToBoolean(objEstadoAvanzar["PERMITIR_ANEXAR_CONCEPTOS"]) : false), "", null, "", default(DateTime), -1, false,
                                                                   -1, -1, "", "", "");

                            //Actualizar proceso                
                            objNotificacionFachada.ActualizarProcesos(Convert.ToInt32(objEstadoAvanzar["ID_ESTADO"]), objEstadoAvanzar["ESTADO"].ToString(), "NO", Convert.ToInt64(objEstadoAvanzar["ID_ACTO_NOTIFICACION"]), Convert.ToInt64(objEstadoAvanzar["ID_PERSONA"]));
                        }
                        catch(Exception exc){
                            //Escribir error
                            SMLog.Escribir(Severidad.Critico, string.Format("Servicio - Automatico -> Error realizando avance ID_ACTO: {0} - ID_PERSONA: {1}, Error: {2}", objEstadoAvanzar["ID_ACTO_NOTIFICACION"].ToString(), objEstadoAvanzar["ID_PERSONA"].ToString(), exc.InnerException));
                        }
                    }

                }

                if (p_blnLogActivo)
                    SMLog.Escribir(Severidad.Informativo, "-- Finaliza proceso AvanzarEstadosNotificacion() --");
            }
            catch(Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Servicio - Automatico -> Error realizando avance. Error: " + exc.InnerException);
            }

        }

    }
}
