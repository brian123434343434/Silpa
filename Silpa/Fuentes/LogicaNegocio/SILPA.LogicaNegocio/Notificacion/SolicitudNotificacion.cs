using System;
using System.Collections.Generic;
using System.Text;
using SILPA.LogicaNegocio.BPMProcessL;
using System.Data;
using SILPA.Comun;
using SILPA.AccesoDatos.Notificacion;
using SILPA.LogicaNegocio.Excepciones;
using SoftManagement.Log;
using SILPA.AccesoDatos.Excepciones;
using SILPA.AccesoDatos.BPMProcess;
using System.IO;
using System.Xml.Serialization;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class SolicitudNotificacion
    {

        #region Metodos Privados


            /// <summary>
            /// Cargar los valores de un campo del formulario
            /// </summary>
            /// <param name="p_intId">int con el ID del campo</param>
            /// <param name="p_strGrupo">string con el nombre del grupo al cual pertenece</param>
            /// <param name="p_strValor">string con el valor</param>
            /// <param name="p_intOrden">int con el orden del campo</param>
            /// <param name="p_objArchivo">Byte[] con el archivo relacionado</param>
            /// <returns>ValoresIdentity con los valores cargados</returns>
            private ValoresIdentity CargarValoresFormulario(int p_intId, string p_strGrupo, string p_strValor, int p_intOrden, Byte[] p_objArchivo)
            {
                ValoresIdentity objValores = new ValoresIdentity();
                objValores.Id = p_intId;
                objValores.Grupo = p_strGrupo;
                objValores.Valor = p_strValor;
                objValores.Orden = p_intOrden;
                objValores.Archivo = p_objArchivo;
                return objValores;
            }


            /// <summary>
            /// Registrar el formulario de solicitud de notificación en VITAL
            /// </summary>
            /// <param name="p_objDatosNotificacion">PersonaNotExpedienteEntity que contiene los datos basicos de la notificacion</param>
            /// <param name="p_objListaExpedientes">List con la información de los expedientes que deben ser notificados</param>
            /// <param name="p_objListaExpedientesEliminados">List con la información de los expedientes que fueron eliminados</param>
            /// <returns>string con el número VITAL del registro de la solicitud de notificación</returns>
            private string RegistrarFormularioSolicitudNotificacion(PersonaNotExpedienteEntity p_objDatosNotificacion, List<NotExpedientesEntity> p_objListaExpedientes, List<NotExpedientesEntity> p_objListaExpedientesEliminados)
            {
                List<ValoresIdentity> objValoresList = null;
                MemoryStream objMemoryStream = null;
                XmlSerializer objSerializador = null;
                NotExpedientesEntityDalc objDalc = null;
                DataTable objParametrosFormulario = null;
                string strNumeroVital = "";
                string strXmlValores = "";
                int intCont1, intCont2 = 1;

                try
                {
                    //Crear e inicializar listado de parametros
                    objValoresList = new List<ValoresIdentity>();
                    objValoresList.Add(CargarValoresFormulario(1, "Bas1", p_objDatosNotificacion.PERSONA_IDENTIFICACION, 1, new Byte[1]));
                    objValoresList.Add(CargarValoresFormulario(2, "Bas1", p_objDatosNotificacion.PERSONA_TIPO_IDENTIFICACION.ToString(), 1, new Byte[1]));

                    //Dependiendo el tipo de notificación se cargan los parametros para enviar al formulario de VITAL
                    if (p_objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA)
                    {                        
                        objValoresList.Add(CargarValoresFormulario(3, "Bas1", "Notificacion Completa", 1, new Byte[1]));
                        objValoresList.Add(CargarValoresFormulario(4, "Bas1", "ANLA", 1, new Byte[1]));
                        objValoresList.Add(CargarValoresFormulario(5, "List1", "", 1, new Byte[1]));
                        objValoresList.Add(CargarValoresFormulario(6, "List1", "", 1, new Byte[1]));
                        objValoresList.Add(CargarValoresFormulario(7, "List1", "", 1, new Byte[1]));
                        objValoresList.Add(CargarValoresFormulario(8, "List1", "", 1, new Byte[1]));
                        objValoresList.Add(CargarValoresFormulario(9, "List1", "", 1, new Byte[1]));
                    }
                    else if (p_objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA_X_EXP)
                    {
                        objValoresList.Add(CargarValoresFormulario(3, "Bas1", "Notificacion por Expediente", 1, new Byte[1]));
                        objValoresList.Add(CargarValoresFormulario(4, "Bas1", "ANLA", 1, new Byte[1]));

                        //Verificar que tiene expedientes registrados
                        if (p_objListaExpedientes != null && p_objListaExpedientes.Count > 0)
                        {
                            //Registrar expedientes registrados
                            for (intCont1 = 1; intCont1 <= p_objListaExpedientes.Count; intCont1++)
                            {
                                objValoresList.Add(CargarValoresFormulario(5, "List1", p_objListaExpedientes[intCont1 - 1].SOL_NUMERO_SILPA.ToString(), intCont2, new Byte[1]));
                                objValoresList.Add(CargarValoresFormulario(6, "List1", p_objListaExpedientes[intCont1 - 1].DESC_SOL_ID_SOLICITANTE.ToString(), intCont2, new Byte[1]));
                                objValoresList.Add(CargarValoresFormulario(7, "List1", "SI", intCont2, new Byte[1]));
                                objValoresList.Add(CargarValoresFormulario(8, "List1", "NO", intCont2, new Byte[1]));
                                objValoresList.Add(CargarValoresFormulario(9, "List1", p_objListaExpedientes[intCont1 - 1].ID_EXPEDIENTE, intCont2, new Byte[1]));
                                intCont2++;
                            }
                        }

                        //Verificar si tiene expedientes eliminados
                        if (p_objListaExpedientesEliminados != null && p_objListaExpedientesEliminados.Count > 0)
                        {
                            //Registrar eliminación de expediente
                            for (intCont1 = 1; intCont1 <= p_objListaExpedientesEliminados.Count; intCont1++)
                            {
                                objValoresList.Add(CargarValoresFormulario(5, "List1", p_objListaExpedientesEliminados[intCont1 - 1].SOL_NUMERO_SILPA.ToString(), intCont2, new Byte[1]));
                                objValoresList.Add(CargarValoresFormulario(6, "List1", p_objListaExpedientesEliminados[intCont1 - 1].DESC_SOL_ID_SOLICITANTE.ToString(), intCont2, new Byte[1]));
                                objValoresList.Add(CargarValoresFormulario(7, "List1", "NO", intCont2, new Byte[1]));
                                objValoresList.Add(CargarValoresFormulario(8, "List1", "SI", intCont2, new Byte[1]));
                                objValoresList.Add(CargarValoresFormulario(9, "List1", p_objListaExpedientesEliminados[intCont1 - 1].ID_EXPEDIENTE, intCont2, new Byte[1]));
                                intCont2++;
                            }
                        }
                    }
                    else if (!p_objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA && !p_objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA_AA && !p_objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA_X_EXP)
                    {
                        objValoresList.Add(CargarValoresFormulario(3, "Bas1", "Notificacion Presencial", 1, new Byte[1]));
                        objValoresList.Add(CargarValoresFormulario(4, "Bas1", "ANLA", 1, new Byte[1]));

                        //Verificar si tiene expedientes eliminados
                        if (p_objListaExpedientesEliminados != null && p_objListaExpedientesEliminados.Count > 0)
                        {
                            //Registrar Eliminación de Expediente
                            for (intCont1 = 1; intCont1 <= p_objListaExpedientesEliminados.Count; intCont1 ++)
                            {
                                objValoresList.Add(CargarValoresFormulario(5, "List1", p_objListaExpedientesEliminados[intCont1 - 1].SOL_NUMERO_SILPA.ToString(), intCont2, new Byte[1]));
                                objValoresList.Add(CargarValoresFormulario(6, "List1", p_objListaExpedientesEliminados[intCont1 - 1].DESC_SOL_ID_SOLICITANTE.ToString(), intCont2, new Byte[1]));
                                objValoresList.Add(CargarValoresFormulario(7, "List1", "NO", intCont2, new Byte[1]));
                                objValoresList.Add(CargarValoresFormulario(8, "List1", "SI", intCont2, new Byte[1]));
                                objValoresList.Add(CargarValoresFormulario(9, "List1", p_objListaExpedientesEliminados[intCont1 - 1].ID_EXPEDIENTE, intCont2, new Byte[1]));
                                intCont2 ++;
                            }
                        }
                        else
                        {
                            objValoresList.Add(CargarValoresFormulario(5, "List1", "", 1, new Byte[1]));
                            objValoresList.Add(CargarValoresFormulario(6, "List1", "", 1, new Byte[1]));
                            objValoresList.Add(CargarValoresFormulario(7, "List1", "", 1, new Byte[1]));
                            objValoresList.Add(CargarValoresFormulario(8, "List1", "", 1, new Byte[1]));
                            objValoresList.Add(CargarValoresFormulario(9, "List1", "", 1, new Byte[1]));
                        }
                    }


                    //Convertir en XML los valores del formulario
                    objMemoryStream = new MemoryStream();
                    objSerializador = new XmlSerializer(typeof(List<ValoresIdentity>));
                    objSerializador.Serialize(objMemoryStream, objValoresList);
                    strXmlValores = System.Text.UTF8Encoding.UTF8.GetString(objMemoryStream.ToArray());

                    //Consultar los parametros del formulario
                    objDalc = new NotExpedientesEntityDalc();
                    objParametrosFormulario = objDalc.obtenerUsuarioSolicitudNot(p_objDatosNotificacion.PERSONA_PER_ID.ToString()).Tables[0];

                    //Crear el proceso en vital                    
                    strNumeroVital = this.CreaProcesoSolicitudNot(objParametrosFormulario.Rows[0]["CLIENT_ID"].ToString(), 
                                                                  Int64.Parse(objParametrosFormulario.Rows[0]["FORM_ID"].ToString()), 
                                                                  p_objDatosNotificacion.PERSONA_PER_ID, 
                                                                  strXmlValores);
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "SolicitudNotificacion :: RegistrarFormularioSolicitudNotificacion -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

                    throw new NotificacionException("SolicitudNotificacion :: RegistrarFormularioSolicitudNotificacion -> Error Inesperado: " + exc.Message, exc.InnerException);
                }

                return strNumeroVital;
            }


        #endregion


        #region Metodos Publicos


            public string CreaProcesoSolicitudNot(string ClientId, Int64 FormularioQueja, Int64 UsuarioQueja, string ValoresXml)
            {
                BpmProcessLn objProceso = new BpmProcessLn();
                return objProceso.crearProceso(ClientId, FormularioQueja, UsuarioQueja, ValoresXml);
            }


            /// <summary>
            /// Consulta un estado de Notificación para la Autoridad Ambiental en SILPA
            /// </summary>
            /// <param name="xmlDatos">Datos del Acto Administrativo para la Prueba</param>
            /// <returns>XML con el acto, y la lista de personas con el estado Notificado para cada persona</returns>
            public string ConsultarUsuariosNotificarExpdiente(string codigoExpediente,string numero_silpa_exp)
            {
                XmlSerializador _objSer = new XmlSerializador();
                NotExpedientesEntityDalc dalc = new NotExpedientesEntityDalc();
                List<PersonaNotExpedienteEntity> not = new List<PersonaNotExpedienteEntity>();
                not = dalc.listarNotificadosExpedientes(codigoExpediente, numero_silpa_exp);
           
                string salida = _objSer.serializar(not);
                return salida;

            }


            /// <summary>
            /// Consulta un estado de Notificación para la Autoridad Ambiental en SILPA
            /// </summary>
            /// <param name="xmlDatos">Datos del Acto Administrativo para la Prueba</param>
            /// <returns>XML con el acto, y la lista de personas con el estado Notificado para cada persona</returns>
            public bool ConsultarExpedientePersonaNotificar(String PerID, String codigoExpediente, String NumeroSilpa)
            {
	            try
	            {
	                NotExpedientesEntityDalc dalc = new NotExpedientesEntityDalc();
	                List<NotExpedientesEntity> not = dalc.ConsultarExpedientePersonaNotificar(PerID, codigoExpediente, NumeroSilpa);
	                if (not != null)
	                {
	                    if (not.Count != 0)
	                    {
	                        return true;
	                    }
	                    else
	                    {
	                        return false;
	                    }
	                }
	                else
	                {
	                    return false;
	                }
				}
	            catch (Exception ex)
	            {
	                string strException = "Validar los pasos efectuados al Consultar Expediente Persona Notificar.";
	                throw new Exception(strException, ex);
	            }
            }

            /// <summary>
            /// Consulta un estado de Notificación para la Autoridad Ambiental en SILPA
            /// </summary>
            /// <param name="xmlDatos">Datos del Acto Administrativo para la Prueba</param>
            /// <returns>XML con el acto, y la lista de personas con el estado Notificado para cada persona</returns>
            public bool ConsultarExpedientePersonaNotificarxAA(String PerID, String codigoExpediente, String CodigoAA)
            {
                NotExpedientesEntityDalc dalc = new NotExpedientesEntityDalc();
                List<NotExpedientesEntity> not = dalc.ConsultarExpedientePersonaNotificarxAA(PerID, codigoExpediente, CodigoAA);
                if (not != null)
                {
                    if (not.Count != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }


            /// <summary>
            /// Obtener los tipos de notificación que posee un usuario
            /// </summary>
            /// <param name="p_intPerId">int con el id de la persona</param>
            /// <returns>PersonaNotExpedienteEntity con la información del tipo de notificaciones que posee el usuario</returns>
            public PersonaNotExpedienteEntity ListarTipoNotificadosPersona(int p_intPerId)
            {
                NotExpedientesEntityDalc dalc = new NotExpedientesEntityDalc();
                return dalc.listarTipoNotificadosPersona(p_intPerId);
            }


            /// <summary>
            /// Consulta el listado de expedientes a notificar para una persona
            /// </summary>
            /// <param name="p_intPerId">int con el id de la persona</param>
            /// <returns>List con los expedientes a notificar para una persona</returns>
            public List<NotExpedientesEntity> ConsultarExpedienteNotificarPersona(int p_intPerId)
            {
                NotExpedientesEntityDalc objDalc = null;
                List<NotExpedientesEntity> objExpedientesNotificar = null;
                PersonaNotExpedienteEntity objPersonaExpediente = null;

                try
                {
                    //Realizar la consulta
                    objDalc = new NotExpedientesEntityDalc();

                    //Consultar el tipo de notificación actual
                    objPersonaExpediente = objDalc.listarTipoNotificadosPersona(p_intPerId);

                    //Dependiendo el tipo de notificación consultar los expedientes relacionados
                    if (objPersonaExpediente != null)
                    {

                        //Si es por expedientes consultar solo los expedientes registrados
                        //Si es todos consultar todos los expedientes a los cuales se encuentra relacionado
                        if (objPersonaExpediente.ES_NOTIFICACION_ELECTRONICA_X_EXP)
                        {
                            objExpedientesNotificar = objDalc.ConsultarExpedienteNotificar(p_intPerId);
                        }
                        else if (objPersonaExpediente.ES_NOTIFICACION_ELECTRONICA)
                        {
                            objExpedientesNotificar = objDalc.ConsultarExpedienteAutoridadPersona(p_intPerId, 0);
                        }
                    }
                    else
                    {
                        objExpedientesNotificar = objDalc.ConsultarExpedienteNotificar(p_intPerId);
                    }
                }
                catch(Exception exc){
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "SolicitudNotificacion :: ConsultarExpedienteNotificarPersona -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

                    throw new NotificacionException("SolicitudNotificacion :: ConsultarExpedienteNotificarPersona -> Error Inesperado: " + exc.Message, exc.InnerException);
                }

                return objExpedientesNotificar;
            }


            /// <summary>
            /// Consultar los expedientes a los cuales se encuentra relacionado una persona en una autoridad ambiental
            /// </summary>        
            /// <param name="p_intPerId">int con el id de la persona</param>        
            /// <param name="p_intIdAutoridad">int con el id de la autoridad ambiental</param>
            /// <returns>List con los expedientes a los cuales se encuentra relacionada la persona</returns>
            public List<NotExpedientesEntity> ConsultarExpedienteAutoridadPersona(int p_intPerId, int p_intIdAutoridad)
            {
                NotExpedientesEntityDalc objDalc = null;
                List<NotExpedientesEntity> objExpedientes = null;

                try
                {
                    //Realizar la consulta
                    objDalc = new NotExpedientesEntityDalc();

                    //Consultar expedientes
                    objExpedientes = objDalc.ConsultarExpedienteAutoridadPersona(p_intPerId, p_intIdAutoridad);
                
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "SolicitudNotificacion :: ConsultarExpedienteAutoridadPersona -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

                    throw new NotificacionException("SolicitudNotificacion :: ConsultarExpedienteAutoridadPersona -> Error Inesperado: " + exc.Message, exc.InnerException);
                }

                return objExpedientes;
            }


            /// <summary>
            /// Guardar la información de la solicitud de notificación electronica
            /// </summary>
            /// <param name="p_objDatosNotificacion">PersonaNotExpedienteEntity que contiene los datos basicos de la notificacion</param>
            /// <param name="p_objListaExpedientes">List con la información de los expedientes que deben ser notificados</param>
            /// <param name="p_objListaExpedientesEliminados">List con la información de los expedientes que fueron eliminados</param>
            /// <returns>string con el número VITAL del registro de la solicitud de notificación</returns>
            public string GuardarSolicitudNotificacion(PersonaNotExpedienteEntity p_objDatosNotificacion, List<NotExpedientesEntity> p_objListaExpedientes, List<NotExpedientesEntity> p_objListaExpedientesEliminados)
            {
                NotExpedientesEntityDalc objDalc = null;
                string strNumeroVital = "";
                int intNumeroSolicitud = 0;

                try
                {
                    //Realizar actualización en base de datos
                    objDalc = new NotExpedientesEntityDalc();
                    intNumeroSolicitud = objDalc.GuardarSolicitudNotificacion(p_objDatosNotificacion, p_objListaExpedientes, p_objListaExpedientesEliminados);

                    //Registrar formulario obtener número vital
                    strNumeroVital = this.RegistrarFormularioSolicitudNotificacion(p_objDatosNotificacion, p_objListaExpedientes, p_objListaExpedientesEliminados);

                    //Actualizar numero VITAL
                    objDalc.ActualizarNumeroSilpaTipoNotificacionPer(intNumeroSolicitud, strNumeroVital);

                }
                catch (NotificacionException exc)
                {
                    throw exc;
                }
                catch(NotExpedientesException exc){
                    throw new NotificacionException("SolicitudNotificacion :: GuardarSolicitudNotificacion -> Error Inesperado: " + exc.Message, exc.InnerException);
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "SolicitudNotificacion :: GuardarSolicitudNotificacion -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

                    throw new NotificacionException("SolicitudNotificacion :: GuardarSolicitudNotificacion -> Error Inesperado: " + exc.Message, exc.InnerException);
                }

                return strNumeroVital;
            }

        #endregion

    }
}
