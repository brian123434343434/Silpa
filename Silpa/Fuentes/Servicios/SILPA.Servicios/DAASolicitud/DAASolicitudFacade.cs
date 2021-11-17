using SILPA.AccesoDatos.DAA;
using SILPA.Comun;
using SILPA.LogicaNegocio.DAA;
using SILPA.LogicaNegocio.Generico;
using SILPA.Servicios.DAASolicitud.Entidades;
using SILPA.Servicios.DAASolicitud.Enum;
using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SILPA.Servicios.DAASolicitud
{
    public class DAASolicitudFacade
    {
        #region Objetos

            /// <summary>
            /// Objeto que contiene la logica para transacciones de solictudes
            /// </summary>
            private DAA _objDAA;

        #endregion


        #region Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public DAASolicitudFacade()
            {
                try
                {
                    this._objDAA = new DAA(); 
                }
                catch (Exception exc)
                {
                    //Escribir log
                    SMLog.Escribir(Severidad.Critico, "DAASolicitudFacade :: DAASolicitudFacade -> Error inesperado: " + exc.Message + " " + (exc != null ? exc.StackTrace.ToString() : ""));

                    //Escalar excepcion
                    throw exc;
                }
            }

        #endregion


        #region Metodos Privados

            /// <summary>
            /// Envia correo informativo a la autoridad ambiental que recibe la solicitud
            /// </summary>
            /// <param name="p_intAutoridadAmbiental">int con el identificador de la autoridad ambiental que solicita la reasignación</param>
            /// <param name="p_intAutoridadAmbientalReasignar">int con el identificador de la autoridad ambiental que recibe la solicitud</param>
            /// <param name="p_strNumeroVITAL">string con el número VITAL del proceso que solicita reasignación</param>
            private void EnviarCorreoAutoridadSolicitud(int p_intAutoridadAmbiental, int p_intAutoridadAmbientalReasignar, string p_strNumeroVITAL)
            {
                SILPA.LogicaNegocio.ICorreo.Correo objCorreo;
                AutoridadAmbiental objAutoridadAmbiental;
                AutoridadAmbiental objAutoridadAmbientalReasignar;

                try
                {
                    //Obtener la información de las autoridades ambientales
                    objAutoridadAmbiental = new AutoridadAmbiental(p_intAutoridadAmbiental);
                    objAutoridadAmbientalReasignar = new AutoridadAmbiental(p_intAutoridadAmbientalReasignar);

                    //Enviar correo
                    if (objAutoridadAmbientalReasignar != null && !string.IsNullOrEmpty(objAutoridadAmbientalReasignar.objAutoridadIdentity.EmailCorrespondencia))
                    {
                        objCorreo = new SILPA.LogicaNegocio.ICorreo.Correo();
                        objCorreo.EnviarCorreoRegistroSolicitudReasignacion(objAutoridadAmbientalReasignar.objAutoridadIdentity.EmailCorrespondencia, objAutoridadAmbientalReasignar.objAutoridadIdentity.Nombre_Largo, objAutoridadAmbiental.objAutoridadIdentity.Nombre_Largo, p_strNumeroVITAL);
                    }
                    else
                    {
                        throw new Exception("La autoridad ambiental " + p_intAutoridadAmbientalReasignar.ToString() + " no tiene correo registrado.");
                    }

                }
                catch (Exception exc)
                {
                    SMLog.Escribir(Severidad.Critico, "DAASolicitudFacade :: EnviarCorreoAutoridadSolicitud -> Error enviando correo: " + exc.Message + " " + exc.StackTrace);
                }
            }


            /// <summary>
            /// Envia correo informativo a la autoridad ambiental informando que la solicitud se aprobo
            /// </summary>
            /// <param name="p_intAutoridadAmbiental">int con el identificador de la autoridad ambiental que solicita la reasignación</param>
            /// <param name="p_intAutoridadAmbientalReasignar">int con el identificador de la autoridad ambiental que recibe la solicitud</param>
            /// <param name="p_strNumeroVITAL">string con el número VITAL del proceso que solicita reasignación</param>
            private void EnviarCorreoAprobacionSolicitud(int p_intAutoridadAmbiental, int p_intAutoridadAmbientalReasignar, string p_strNumeroVITAL)
            {
                SILPA.LogicaNegocio.ICorreo.Correo objCorreo;
                AutoridadAmbiental objAutoridadAmbiental;
                AutoridadAmbiental objAutoridadAmbientalReasignar;

                try
                {
                    //Obtener la información de las autoridades ambientales
                    objAutoridadAmbiental = new AutoridadAmbiental(p_intAutoridadAmbiental);
                    objAutoridadAmbientalReasignar = new AutoridadAmbiental(p_intAutoridadAmbientalReasignar);

                    //Enviar correo
                    if (objAutoridadAmbiental != null && !string.IsNullOrEmpty(objAutoridadAmbiental.objAutoridadIdentity.EmailCorrespondencia))
                    {
                        objCorreo = new SILPA.LogicaNegocio.ICorreo.Correo();
                        objCorreo.EnviarCorreoResultadoSolicitudReasignacion(objAutoridadAmbiental.objAutoridadIdentity.EmailCorrespondencia, objAutoridadAmbientalReasignar.objAutoridadIdentity.Nombre_Largo, objAutoridadAmbiental.objAutoridadIdentity.Nombre_Largo, p_strNumeroVITAL, "Aprobación", "Aprobado", "ha sido reasignada");
                    }
                    else
                    {
                        throw new Exception("La autoridad ambiental " + p_intAutoridadAmbiental.ToString() + " no tiene correo registrado.");
                    }

                }
                catch (Exception exc)
                {
                    SMLog.Escribir(Severidad.Critico, "DAASolicitudFacade :: EnviarCorreoAprobacionSolicitud -> Error enviando correo: " + exc.Message + " " + exc.StackTrace);
                }
            }


            /// <summary>
            /// Envia correo informativo a la autoridad ambiental informando que la solicitud se rechazo
            /// </summary>
            /// <param name="p_intAutoridadAmbiental">int con el identificador de la autoridad ambiental que solicita la reasignación</param>
            /// <param name="p_intAutoridadAmbientalReasignar">int con el identificador de la autoridad ambiental que recibe la solicitud</param>
            /// <param name="p_strNumeroVITAL">string con el número VITAL del proceso que solicita reasignación</param>
            private void EnviarCorreoRechazoSolicitud(int p_intAutoridadAmbiental, int p_intAutoridadAmbientalReasignar, string p_strNumeroVITAL)
            {
                SILPA.LogicaNegocio.ICorreo.Correo objCorreo;
                AutoridadAmbiental objAutoridadAmbiental;
                AutoridadAmbiental objAutoridadAmbientalReasignar;

                try
                {
                    //Obtener la información de las autoridades ambientales
                    objAutoridadAmbiental = new AutoridadAmbiental(p_intAutoridadAmbiental);
                    objAutoridadAmbientalReasignar = new AutoridadAmbiental(p_intAutoridadAmbientalReasignar);

                    //Enviar correo
                    if (objAutoridadAmbiental != null && !string.IsNullOrEmpty(objAutoridadAmbiental.objAutoridadIdentity.EmailCorrespondencia))
                    {
                        objCorreo = new SILPA.LogicaNegocio.ICorreo.Correo();
                        objCorreo.EnviarCorreoResultadoSolicitudReasignacion(objAutoridadAmbiental.objAutoridadIdentity.EmailCorrespondencia, objAutoridadAmbientalReasignar.objAutoridadIdentity.Nombre_Largo, objAutoridadAmbiental.objAutoridadIdentity.Nombre_Largo, p_strNumeroVITAL, "Rechazo", "Rechazada", "NO ha sido reasignada");
                    }
                    else
                    {
                        throw new Exception("La autoridad ambiental " + p_intAutoridadAmbiental.ToString() + " no tiene correo registrado.");
                    }

                }
                catch (Exception exc)
                {
                    SMLog.Escribir(Severidad.Critico, "DAASolicitudFacade :: EnviarCorreoRechazoSolicitud -> Error enviando correo: " + exc.Message + " " + exc.StackTrace);
                }
            }


        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Obtiene el listado de autoridades ambientales a las cuales se les puede reasignar una solicitud
            /// </summary>
            /// <returns>string con el listado de autoridades ambientales</returns>
            public string ObtenerAutoridadesAmbientalesReasignar()
            {
                AutoridadAmbiental objAutoridadAmbiental;
                DataSet objAutoridades;
                DAASolicitudAutoridades objDAASolicitudAutoridades;

                try
                {
                    //Consultar las autoridades ambientales
                    objAutoridadAmbiental = new AutoridadAmbiental();
                    objAutoridades = objAutoridadAmbiental.ListarAutoridadAmbiental(null);

                    //Crear el objeto de respuesta
                    objDAASolicitudAutoridades = new DAASolicitudAutoridades(CodigoRespuestaEnum.OK);

                    //Verificar que se obtenga informacion
                    if (objAutoridades != null && objAutoridades.Tables.Count > 0 && objAutoridades.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objDAASolicitudAutoridades.Autoridades = new List<DAASolicitudAutoridad>();

                        //Cargar el listado de autoridades
                        foreach (DataRow objAutoridad in objAutoridades.Tables[0].Rows)
                        {
                            objDAASolicitudAutoridades.Autoridades.Add(new DAASolicitudAutoridad { AutoridadID = Convert.ToInt32(objAutoridad["AUT_ID"]), Autoridad = objAutoridad["AUT_DESCRIPCION"].ToString() });
                        }

                        //Ordenar el listado
                        objDAASolicitudAutoridades.Autoridades = objDAASolicitudAutoridades.Autoridades.OrderBy(autoridad => autoridad.Autoridad).ToList();
                    }
                    else if (objAutoridades == null)
                    {
                        throw new Exception("Se presento un error a nivel de bd en la consulta de informacion de las autoridades ambientales.");
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DAASolicitudFacade :: ObtenerAutoridadesAmbientalesReasignar -> Error realizando consulta de autoridades: " + exc.Message + " " + exc.StackTrace);

                    //Cargar la respuesta
                    objDAASolicitudAutoridades = new DAASolicitudAutoridades(CodigoRespuestaEnum.ERROR, "Se presento un problema obteniendo el listado de autoridades");
                }

                return objDAASolicitudAutoridades.GetXml();
            }


            /// <summary>
            /// Obtiene el listado de estados que puede tomar una solicitud de reasignacion
            /// </summary>
            /// <returns>string con el listado de estados</returns>
            public string ObtenerEstadosSolicitudesReasignacion()
            {
                DAASolicitudEstadosReasignacion objDAASolicitudEstadosReasignacion;

                try
                {   
                    //Crear el objeto de respuesta
                    objDAASolicitudEstadosReasignacion = new DAASolicitudEstadosReasignacion(CodigoRespuestaEnum.OK, p_objEstados: this._objDAA.ObtenerEstadosSolicitudReasignacion());

                    //Ordenar el listado de estados
                    if (objDAASolicitudEstadosReasignacion.Estados != null) objDAASolicitudEstadosReasignacion.Estados = objDAASolicitudEstadosReasignacion.Estados.OrderBy(estado => estado.Estado).ToList();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DAASolicitudFacade :: ObtenerEstadosSolicitudesReasignacion -> Error realizando consulta de estados: " + exc.Message + " " + exc.StackTrace);

                    //Cargar la respuesta
                    objDAASolicitudEstadosReasignacion = new DAASolicitudEstadosReasignacion(CodigoRespuestaEnum.ERROR, "Se presento un problema obteniendo el listado de estados");
                }

                return objDAASolicitudEstadosReasignacion.GetXml();
            }


            /// <summary>
            /// Obtener la informacion de una solicitud relacionada al numero VITAL indicado
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental.</param>
            /// <param name="p_strNumeroVITAL">string con el numero VITAL</param>
            /// <returns>string con la información de la solicitud en XML.</returns>
            public string ObtenerDAASolicitudNumeroVITAL(int p_intAutoridadID, string p_strNumeroVITAL)
            {
                DAASolicitudTramite objSolicitudTramite;
                DAASolicitudEntity objDAASolicitudEntity = null;

                try
                {
                    //Verificar que la informacion sea suministrada
                    if (p_intAutoridadID > 0 && !string.IsNullOrEmpty(p_strNumeroVITAL))
                    {
                        //Realizar la consulta
                        objDAASolicitudEntity = this._objDAA.ObtenerSolicitudNumeroVITAL(p_strNumeroVITAL, p_intAutoridadID);

                        //Cargar la respuesta
                        objSolicitudTramite = new DAASolicitudTramite(CodigoRespuestaEnum.OK, p_objDAASolicitudEntity: objDAASolicitudEntity);
                    }
                    else
                    {
                        //Cargar la respuesta
                        objSolicitudTramite = new DAASolicitudTramite(CodigoRespuestaEnum.INFORMACION_INCONSISTENTE, "No se especifico toda la información requerida para la realización de la consulta");
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DAASolicitudFacade :: ObtenerDAASolicitudNumeroVITAL -> Error realizando consulta de la solicitud de trámite: " + exc.Message + " " + exc.StackTrace);

                    //Cargar la respuesta
                    objSolicitudTramite = new DAASolicitudTramite(CodigoRespuestaEnum.ERROR, "Se presento un problema en el proceso de consulta de la solicitud");
                }

                return objSolicitudTramite.GetXml();
            }


            /// <summary>
            /// Registrar una petición de reasignación de solicitud de trámite
            /// </summary>
            /// <param name="p_intAutoridadAmbientalID">int con el identificador de la autoridad ambiental que esta solicitando la reasignación</param>
            /// <param name="p_intAutoridadAmbientalReasignarID">int con el identificador de la autoridad ambiental a quien se solicita reasignar</param>
            /// <param name="p_strNumeroVITAL">string con el número VITAL al cual se reasignará la solicitud</param>
            /// <param name="p_intUsuarioSolicitanteID">int con el identificador del usuario de la autoridad ambiental que realiza la solicitud</param>
            /// <returns>string con la información de la solicitud de reasignación registrada</returns>
            public string SolicitarReasignacionDAASolicitud(int p_intAutoridadAmbientalID, int p_intAutoridadAmbientalReasignarID, string p_strNumeroVITAL, int? p_intUsuarioSolicitanteID = null)
            {
                DAASolicitudReasignacion objDAASolicitudReasignacion;
                int intSolicitudReasignacionID = -1;

                try
                {
                    //Verificar que se especifique la información
                    if (p_intAutoridadAmbientalID > 0 && p_intAutoridadAmbientalReasignarID > 0 &&
                        !string.IsNullOrEmpty(p_strNumeroVITAL))
                    {
                        //Insertar la solicitud de reasignación
                        intSolicitudReasignacionID = this._objDAA.InsertarSolicitudReasignacion(new DAASolicitudReasignacionEntity 
                                                        {
                                                            AutoridadAmbientalSolicitanteID = p_intAutoridadAmbientalID,
                                                            AutoridadAmbientalReasignarID = p_intAutoridadAmbientalReasignarID,
                                                            NumeroVITAL = p_strNumeroVITAL,
                                                            SolicitanteAutoridadID = p_intUsuarioSolicitanteID
                                                        });

                        //enviar correo a la autoridad
                        this.EnviarCorreoAutoridadSolicitud(p_intAutoridadAmbientalID, p_intAutoridadAmbientalReasignarID, p_strNumeroVITAL);

                        //Cargar respuesta
                        objDAASolicitudReasignacion = new DAASolicitudReasignacion(CodigoRespuestaEnum.OK, p_objDAASolicitudReasignacionEntity: this._objDAA.ObtenerSolicitudReasignacion(intSolicitudReasignacionID));
                    }
                    else
                        objDAASolicitudReasignacion = new DAASolicitudReasignacion(CodigoRespuestaEnum.INFORMACION_INCONSISTENTE, "No se especifico la información requerida para realizar la reasignación de la solicitud");
                    
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DAASolicitudFacade :: SolicitarReasignacionDAASolicitud -> Error registrando solicitud de reasignación: " + exc.Message + " " + exc.StackTrace);

                    //Cargar mensaje de error
                    objDAASolicitudReasignacion = new DAASolicitudReasignacion(CodigoRespuestaEnum.ERROR, "Se presento un problema durante el proceso de reistro de la solicitud de reasignación");
                }

                return objDAASolicitudReasignacion.GetXml();
            }


            /// <summary>
            /// Aprueba una solicitud de reasignación
            /// </summary>
            /// <param name="p_intSolicitudReasignacionID">int con el identificador de la solicitud de reasignación</param>
            /// <param name="p_strCodigoSolicitudReasignacion">string con el codigo de la solicitud de reasignación</param>
            /// <param name="p_intUsuarioAprobadorID">int con el identificador del usuario en la autoridad que realiza la aprobación. Opcional enviar -1</param>
            /// <returns>string con el resultado del proceso</returns>
            public string AprobarSolicitudReasignacion(int p_intSolicitudReasignacionID, string p_strCodigoSolicitudReasignacion, int p_intUsuarioAprobadorID = -1)
            {
                DAASolicitudReasignacionEntity objDAASolicitudReasignacionEntity;
                string strResultado;

                try
                {
                    //Verificar datos
                    if (p_intSolicitudReasignacionID > 0 && !string.IsNullOrEmpty(p_strCodigoSolicitudReasignacion))
                    {
                        //Obtener la informacion de la soliciitud
                        objDAASolicitudReasignacionEntity = this._objDAA.ObtenerSolicitudReasignacion(p_intSolicitudReasignacionID);

                        //Verificar que la solicitud exista
                        if (objDAASolicitudReasignacionEntity != null && objDAASolicitudReasignacionEntity.SolicitudReasignacionID > 0)
                        {
                            //Verificar que el código suministrado concuerde
                            if (objDAASolicitudReasignacionEntity.CodigoReasignacion == p_strCodigoSolicitudReasignacion)
                            {
                                //Verificar que la solicitud se encuentre en proceso de aprobación
                                if (objDAASolicitudReasignacionEntity.EstadoSolicitudID == (int)EstadoSolicitudReasignacion.Solicitado)
                                {
                                    //Cambiar el estado a aprobado
                                    objDAASolicitudReasignacionEntity.EstadoSolicitudID = (int)EstadoSolicitudReasignacion.Aprobado;
                                    objDAASolicitudReasignacionEntity.SolicitanteAutoridadVerificaID = p_intUsuarioAprobadorID;
                                    this._objDAA.ActualizarSolicitudReasignacion(objDAASolicitudReasignacionEntity);

                                    //Realizar la reasignación de la solicitud
                                    this._objDAA.ReasignarSolicitud(objDAASolicitudReasignacionEntity.NumeroVITAL, objDAASolicitudReasignacionEntity.AutoridadAmbientalReasignarID, objDAASolicitudReasignacionEntity.SolicitudReasignacionID);

                                    //Cambiar el estado a aprobado
                                    objDAASolicitudReasignacionEntity.EstadoSolicitudID = (int)EstadoSolicitudReasignacion.Reasignado;
                                    this._objDAA.ActualizarSolicitudReasignacion(objDAASolicitudReasignacionEntity);

                                    //Cargar respuesta
                                    strResultado = (new DAASolicitudRespuesta(CodigoRespuestaEnum.OK)).GetXml();
                                }
                                else if (objDAASolicitudReasignacionEntity.EstadoSolicitudID == (int)EstadoSolicitudReasignacion.Aprobado)
                                {
                                    //Realizar la reasignación de la solicitud
                                    this._objDAA.ReasignarSolicitud(objDAASolicitudReasignacionEntity.NumeroVITAL, objDAASolicitudReasignacionEntity.AutoridadAmbientalReasignarID, objDAASolicitudReasignacionEntity.SolicitudReasignacionID);

                                    //Cambiar el estado a aprobado
                                    objDAASolicitudReasignacionEntity.EstadoSolicitudID = (int)EstadoSolicitudReasignacion.Reasignado;
                                    this._objDAA.ActualizarSolicitudReasignacion(objDAASolicitudReasignacionEntity);

                                    //Enviar Correo
                                    this.EnviarCorreoAprobacionSolicitud(objDAASolicitudReasignacionEntity.AutoridadAmbientalSolicitanteID, objDAASolicitudReasignacionEntity.AutoridadAmbientalReasignarID, objDAASolicitudReasignacionEntity.NumeroVITAL);

                                    //Cargar respuesta
                                    strResultado = (new DAASolicitudRespuesta(CodigoRespuestaEnum.OK)).GetXml();
                                }
                                else
                                {
                                    strResultado = (new DAASolicitudRespuesta(CodigoRespuestaEnum.INFORMACION_INCONSISTENTE, "La solicitud de reasignación ha sido Aprobada / Rechazada previamente.")).GetXml();
                                }
                            }
                            else
                            {
                                strResultado = (new DAASolicitudRespuesta(CodigoRespuestaEnum.INFORMACION_INCONSISTENTE, "La información de la solicitud de reasignación que se desea aprobar es inconsistente")).GetXml();
                            }
                        }
                        else
                        {
                            strResultado = (new DAASolicitudRespuesta(CodigoRespuestaEnum.INFORMACION_INCONSISTENTE, "La solicitud de reasignación que desea aprobar no existe")).GetXml();
                        }
                    }
                    else
                    {
                        strResultado = (new DAASolicitudRespuesta(CodigoRespuestaEnum.INFORMACION_INCONSISTENTE, "No se suministro la información que se requiere para la aprobación de la solicitud de reasignación")).GetXml();
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DAASolicitudFacade :: AprobarSolicitudReasignacion -> Error en proceso de aprobación de solicitud: " + exc.Message + " " + exc.StackTrace);

                    //Generar mensaje de error
                    strResultado = (new DAASolicitudRespuesta(CodigoRespuestaEnum.ERROR, "Se presento un problema durante el proceso de aprobación de la solicitud de reasignación")).GetXml();
                }

                return strResultado;
            }


            /// <summary>
            /// Rechazar una solicitud de reasignación
            /// </summary>
            /// <param name="p_intSolicitudReasignacionID">int con el identificador de la solicitud de reasignación</param>
            /// <param name="p_strCodigoSolicitudReasignacion">string con el codigo de la solicitud de reasignación</param>
            /// <param name="p_intUsuarioAprobadorID">int con el identificador del usuario en la autoridad que realiza la aprobación. Opcional enviar -1</param>
            /// <returns>string con el resultado del proceso</returns>
            public string RechazarSolicitudReasignacion(int p_intSolicitudReasignacionID, string p_strCodigoSolicitudReasignacion, int p_intUsuarioAprobadorID = -1)
            {
                DAASolicitudReasignacionEntity objDAASolicitudReasignacionEntity;
                string strResultado;

                try
                {
                    //Verificar datos
                    if (p_intSolicitudReasignacionID > 0 && !string.IsNullOrEmpty(p_strCodigoSolicitudReasignacion))
                    {
                        //Obtener la informacion de la soliciitud
                        objDAASolicitudReasignacionEntity = this._objDAA.ObtenerSolicitudReasignacion(p_intSolicitudReasignacionID);

                        //Verificar que la solicitud exista
                        if (objDAASolicitudReasignacionEntity != null && objDAASolicitudReasignacionEntity.SolicitudReasignacionID > 0)
                        {
                            //Verificar que el código suministrado concuerde
                            if (objDAASolicitudReasignacionEntity.CodigoReasignacion == p_strCodigoSolicitudReasignacion)
                            {
                                //Verificar que la solicitud se encuentre en proceso de aprobación
                                if (objDAASolicitudReasignacionEntity.EstadoSolicitudID == (int)EstadoSolicitudReasignacion.Solicitado)
                                {
                                    //Cambiar el estado a aprobado
                                    objDAASolicitudReasignacionEntity.EstadoSolicitudID = (int)EstadoSolicitudReasignacion.Rechazado;
                                    objDAASolicitudReasignacionEntity.SolicitanteAutoridadVerificaID = p_intUsuarioAprobadorID;
                                    this._objDAA.ActualizarSolicitudReasignacion(objDAASolicitudReasignacionEntity);

                                    //Enviar Correo
                                    this.EnviarCorreoRechazoSolicitud(objDAASolicitudReasignacionEntity.AutoridadAmbientalSolicitanteID, objDAASolicitudReasignacionEntity.AutoridadAmbientalReasignarID, objDAASolicitudReasignacionEntity.NumeroVITAL);

                                    //Cargar respuesta
                                    strResultado = (new DAASolicitudRespuesta(CodigoRespuestaEnum.OK)).GetXml();
                                }
                                else
                                {
                                    strResultado = (new DAASolicitudRespuesta(CodigoRespuestaEnum.INFORMACION_INCONSISTENTE, "La solicitud de reasignación ha sido Aprobada / Rechazada previamente.")).GetXml();
                                }
                            }
                            else
                            {
                                strResultado = (new DAASolicitudRespuesta(CodigoRespuestaEnum.INFORMACION_INCONSISTENTE, "La información de la solicitud de reasignación que se desea rechazar es inconsistente")).GetXml();
                            }
                        }
                        else
                        {
                            strResultado = (new DAASolicitudRespuesta(CodigoRespuestaEnum.INFORMACION_INCONSISTENTE, "La solicitud de reasignación que desea rechazar no existe")).GetXml();
                        }
                    }
                    else
                    {
                        strResultado = (new DAASolicitudRespuesta(CodigoRespuestaEnum.INFORMACION_INCONSISTENTE, "No se suministro la información que se requiere para rechazar la solicitud de reasignación")).GetXml();
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DAASolicitudFacade :: RechazarSolicitudReasignacion -> Error en proceso de rechazo de solicitud: " + exc.Message + " " + exc.StackTrace);

                    //Generar mensaje de error
                    strResultado = (new DAASolicitudRespuesta(CodigoRespuestaEnum.ERROR, "Se presento un problema durante el proceso de rechazo de la solicitud de reasignación")).GetXml();
                }

                return strResultado;
            }


            /// <summary>
            /// Obtener el listado de solicitudes de reasignacion realizadas por una autoridad.
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad que realizo la solicitud</param>
            /// <param name="p_strNumeroVITAL">string con el número VITAL</param>
            /// <param name="p_intAutoridadReasignar">int con el identificador a la cual se reasigno</param>
            /// <param name="p_intEstadoSolicitudID">int con el identificador del estado de la solicitud. Opcional enviar -1</param>
            /// <param name="p_objFechaSolicitudInicial">Fecha inicial desde la cual se realiza la busqueda</param>
            /// <param name="p_objFechaSolicitudFinal">Fecha Final desde la cual se realiza la busqueda</param>
            /// <returns>string con la información de las solicitudes</returns>
            public string ObtenerSolicitudesReasignacionRealizadasAutoridad(int p_intAutoridadID, string p_strNumeroVITAL, int p_intAutoridadReasignar, int p_intEstadoSolicitudID, DateTime p_objFechaSolicitudInicial, DateTime p_objFechaSolicitudFinal)
            {
                DAASolicitudesReasignacion objDAASolicitudesReasignacion;

                try
                {
                    //Consultar informacion
                    objDAASolicitudesReasignacion = new DAASolicitudesReasignacion(CodigoRespuestaEnum.OK,
                            p_objLstSolicitudesReasignacion: this._objDAA.ObtenerSolicitudesReasignacionRealizadasAutoridad(p_intAutoridadID, p_strNumeroVITAL, p_intAutoridadReasignar, p_intEstadoSolicitudID, p_objFechaSolicitudInicial, p_objFechaSolicitudFinal)
                        );
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DAASolicitudFacade :: ObtenerSolicitudesReasignacionRealizadasAutoridad -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    objDAASolicitudesReasignacion = new DAASolicitudesReasignacion(CodigoRespuestaEnum.ERROR, "Se presento un problema durante la consulta de las solicitudes de reasignación");
                }

                return objDAASolicitudesReasignacion.GetXml();
            }


            /// <summary>
            /// Obtener el listado de solicitudes de reasignacion asignadas.
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad que recibe solicitud</param>
            /// <param name="p_strNumeroVITAL">string con el número VITAL</param>
            /// <param name="p_intAutoridadSolicitante">int con el identificador de la autoridad que realizo la solicitud</param>
            /// <param name="p_intEstadoSolicitudID">int con el identificador del estado de la solicitud. Opcional enviar -1</param>
            /// <param name="p_objFechaSolicitudInicial">Fecha inicial desde la cual se realiza la busqueda</param>
            /// <param name="p_objFechaSolicitudFinal">Fecha Final desde la cual se realiza la busqueda</param>
            /// <returns>string con la información de las solicitudes de reasignación</returns>
            public string ObtenerSolicitudesReasignacionAsignadasAutoridad(int p_intAutoridadID, string p_strNumeroVITAL, int p_intAutoridadSolicitante, int p_intEstadoSolicitudID, DateTime p_objFechaSolicitudInicial, DateTime p_objFechaSolicitudFinal)
            {
                DAASolicitudesReasignacion objDAASolicitudesReasignacion;

                try
                {
                    //Consultar informacion
                    objDAASolicitudesReasignacion = new DAASolicitudesReasignacion(CodigoRespuestaEnum.OK,
                            p_objLstSolicitudesReasignacion: this._objDAA.ObtenerSolicitudesReasignacionAsignadasAutoridad(p_intAutoridadID, p_strNumeroVITAL, p_intAutoridadSolicitante, p_intEstadoSolicitudID, p_objFechaSolicitudInicial, p_objFechaSolicitudFinal)
                        );
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DAASolicitudFacade :: ObtenerSolicitudesReasignacionAsignadasAutoridad -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objDAASolicitudesReasignacion.GetXml();
            }
            

        #endregion
    }
}
