using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SoftManagement.Log;

namespace SILPA.AccesoDatos.Notificacion
{
    public class EstadoFlujoNotificacionDalc
    {
        private Configuracion objConfiguracion;

        #region CReadoras
        
            /// <summary>
            /// Creadora
            /// </summary>
            public EstadoFlujoNotificacionDalc()
            {
                this.objConfiguracion = new Configuracion();
            }

        #endregion


        #region Metodos Publicos


            /// <summary>
            /// Retorna el listado de estados
            /// </summary>
            /// <param name="p_intFlujoID">int con el identificador del flujo</param>
            /// <param name="p_intEstadoFlujoID">int con el identificador del estado del flujo</param>
            /// <returns>List con la informacion del estado</returns>
            public List<EstadoFlujoNotificacionEntity> ListarEstadosNotificacionElectronica(int p_intFlujoID = 0, int p_intEstadoFlujoID = 0)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objEstados = null;
                List<EstadoFlujoNotificacionEntity> objLstEstados = null;
                EstadoFlujoNotificacionEntity objEstadoFlujo = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_ESTADO_FLUJO_NOTIFICACION_ELECTRONICA");
                    if (p_intFlujoID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_ID_FLUJO_NOT_ELEC", DbType.Int32, p_intFlujoID);
                    if (p_intEstadoFlujoID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_FLUJO_NOT_ELEC", DbType.Int32, p_intEstadoFlujoID);

                    //Crear registro
                    objEstados = objDataBase.ExecuteDataSet(objCommand);

                    if (objEstados != null && objEstados.Tables.Count > 0 &&  objEstados.Tables[0].Rows.Count > 0)
                    {
                        objLstEstados = new List<EstadoFlujoNotificacionEntity>();

                        foreach (DataRow objEstado in objEstados.Tables[0].Rows)
                        {
                            //Estado del flujo
                            objEstadoFlujo = new EstadoFlujoNotificacionEntity
                            { 
                                EstadoFlujoID = Convert.ToInt32(objEstado["ID_ESTADO_FLUJO_NOT_ELEC"]),
                                EstadoID = Convert.ToInt32(objEstado["ID_ESTADO"]),
                                FlujoID = Convert.ToInt32(objEstado["ID_FLUJO_NOT_ELEC"]),
                                Estado = objEstado["ESTADO"].ToString().Trim(),
                                EstadoDescripcion = objEstado["DESCRIPCION_ESTADO"].ToString().Trim(),
                                Descripcion = objEstado["DESCRIPCION"].ToString().Trim(),
                                DiasVencimiento = (objEstado["DIAS_VENCIMIENTO"] != System.DBNull.Value ? Convert.ToInt32(objEstado["DIAS_VENCIMIENTO"]) : 0),
                                GeneraPlantilla = Convert.ToBoolean(objEstado["GENERA_PLANTILLA"]),
                                PlantillaID = (objEstado["ID_PLANTILLA"] != System.DBNull.Value ? Convert.ToInt32(objEstado["ID_PLANTILLA"]) : 0),
                                DocumentoAdicional = Convert.ToBoolean(objEstado["DOCUMENTO_ADICIONAL"]),
                                EnviaCorreoAvanceManual = Convert.ToBoolean(objEstado["ENVIO_CORREO_AVANCE_MANUAL"]),
                                EnviaNotificacionFisica = Convert.ToBoolean(objEstado["ENVIO_NOTIFICACION_FISICA"]),
                                AnexaAdjunto = Convert.ToBoolean(objEstado["ANEXA_ADJUNTO"]),
                                EnviaCorreoAvance = Convert.ToBoolean(objEstado["ENVIO_CORREO_AVANCE_AUTOMATICO"]),
                                TextoCorreoAvance = ( objEstado["TEXTO_CORREO_AVANCE_AUTOMATICO"] != System.DBNull.Value ? objEstado["TEXTO_CORREO_AVANCE_AUTOMATICO"].ToString().Trim() : ""),
                                TipoAnexoCorreoID = (objEstado["ID_TIPO_ANEXO_CORREO"] != System.DBNull.Value ? Convert.ToInt32(objEstado["ID_TIPO_ANEXO_CORREO"]) : 0),
                                PermitiAnexarActoAdministrativo = (objEstado["PERMITIR_ANEXAR_ACTO"] != System.DBNull.Value ? Convert.ToBoolean(objEstado["PERMITIR_ANEXAR_ACTO"]) : false),
                                PermitiAnexarConceptosActoAdministrativo = (objEstado["PERMITIR_ANEXAR_CONCEPTOS"] != System.DBNull.Value ? Convert.ToBoolean(objEstado["PERMITIR_ANEXAR_CONCEPTOS"]) : false),
                                PublicarEstado = Convert.ToBoolean(objEstado["PUBLICA_ESTADO"]),
                                PublicarPlantilla = Convert.ToBoolean(objEstado["PUBLICA_PLANTILLA"]),
                                PublicarAdjunto = Convert.ToBoolean(objEstado["PUBLICA_ADJUNTO"]),
                                SolicitarReferenciaRecepcionNotificacion = Convert.ToBoolean(objEstado["SOLICITAR_REFERENCIA"]),
                                ReferenciaRecepcionNotificacionObligatoria = Convert.ToBoolean(objEstado["REFERENCIA_OBLIGATORIA"]),
                                EsEstadoEspera = Convert.ToBoolean(objEstado["ESTADO_ESPERA"]),
                                EsNotificacion = Convert.ToBoolean(objEstado["ESTADO_NOTIFICACION"]),
                                EsEjecutoria = Convert.ToBoolean(objEstado["ESTADO_EJECUTORIA"]),
                                GeneraRecurso = Convert.ToBoolean(objEstado["GENERA_RECURSO"]),
                                EsCitacion = Convert.ToBoolean(objEstado["ESTADO_CITACION"]),
                                EsEdicto = Convert.ToBoolean(objEstado["ESTADO_EDICTO"]),
                                EsAceptacionNotificacion = Convert.ToBoolean(objEstado["ESTADO_ACEPTACION_NOTIFICACION"]),
                                EsRechazoNotificacion = Convert.ToBoolean(objEstado["ESTADO_NEGACION_NOTIFICACION"]),
                                EsAceptacionCitacion = Convert.ToBoolean(objEstado["ESTADO_ACEPTACION_CITACION"]),
                                EsRechazoCitacion = Convert.ToBoolean(objEstado["ESTADO_NEGACION_CITACION"]),
                                EsAnulacion = Convert.ToBoolean(objEstado["ESTADO_ANULACION"]),
                                EsFinalPublicidad = Convert.ToBoolean(objEstado["ESTADO_FINAL_PUBLICIDAD"]),
                                EstadoDependienteID = (objEstado["ID_ESTADO_DEPENDIENTE"] != System.DBNull.Value ? Convert.ToInt32(objEstado["ID_ESTADO_DEPENDIENTE"]) : 0),
                                SolicitarInformacionPersonaNotificar = Convert.ToBoolean(objEstado["SOLICITAR_DATOS_PERSONA_NOTIFICADA"]),
                                Activo = Convert.ToBoolean(objEstado["ACTIVO"])
                            };

                            //Adicionar al listado
                            objLstEstados.Add(objEstadoFlujo);
                        }
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoFlujoNotificacionDalc :: ListarEstadosNotificacionElectronica -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoFlujoNotificacionDalc :: ListarEstadosNotificacionElectronica -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
                finally
                {
                    if (objCommand != null)
                    {
                        objCommand.Dispose();
                        objCommand = null;
                    }
                    if (objDataBase != null)
                        objDataBase = null;
                }

                return objLstEstados;
            }


            /// <summary>
            /// Retorna la configuración para un estado y flujo especifico
            /// </summary>
            /// <param name="p_intFlujoID">int con el id del flujo</param>
            /// <param name="p_intEstadoID">int copn el identificador del estado</param>
            /// <returns>EstadoFlujoNotificacionEntity con la configuración del estado</returns>
            public EstadoFlujoNotificacionEntity ConsultarConfiguracionEstadoNotificacionElectronica(int p_intFlujoID, int p_intEstadoID)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objEstados = null;
                EstadoFlujoNotificacionEntity objEstadoFlujo = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_CONFIGURACION_ESTADO_FLUJO_NOTIFICACION_ELECTRONICA");
                    objDataBase.AddInParameter(objCommand, "@P_ID_FLUJO_NOT_ELEC", DbType.Int32, p_intFlujoID);
                    objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO", DbType.Int32, p_intEstadoID);

                    //Crear registro
                    objEstados = objDataBase.ExecuteDataSet(objCommand);

                    if (objEstados != null && objEstados.Tables.Count > 0 && objEstados.Tables[0].Rows.Count > 0)
                    {
                        //Estado del flujo
                        objEstadoFlujo = new EstadoFlujoNotificacionEntity
                        {
                            EstadoFlujoID = Convert.ToInt32(objEstados.Tables[0].Rows[0]["ID_ESTADO_FLUJO_NOT_ELEC"]),
                            EstadoID = Convert.ToInt32(objEstados.Tables[0].Rows[0]["ID_ESTADO"]),
                            FlujoID = Convert.ToInt32(objEstados.Tables[0].Rows[0]["ID_FLUJO_NOT_ELEC"]),
                            Estado = objEstados.Tables[0].Rows[0]["ESTADO"].ToString().Trim(),
                            EstadoDescripcion = objEstados.Tables[0].Rows[0]["DESCRIPCION_ESTADO"].ToString().Trim(),
                            Descripcion = objEstados.Tables[0].Rows[0]["DESCRIPCION"].ToString().Trim(),
                            DiasVencimiento = (objEstados.Tables[0].Rows[0]["DIAS_VENCIMIENTO"] != System.DBNull.Value ? Convert.ToInt32(objEstados.Tables[0].Rows[0]["DIAS_VENCIMIENTO"]) : 0),
                            GeneraPlantilla = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["GENERA_PLANTILLA"]),
                            PlantillaID = (objEstados.Tables[0].Rows[0]["ID_PLANTILLA"] != System.DBNull.Value ? Convert.ToInt32(objEstados.Tables[0].Rows[0]["ID_PLANTILLA"]) : 0),
                            DocumentoAdicional = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["DOCUMENTO_ADICIONAL"]),
                            EnviaCorreoAvanceManual = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ENVIO_CORREO_AVANCE_MANUAL"]),
                            EnviaNotificacionFisica = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ENVIO_NOTIFICACION_FISICA"]),
                            AnexaAdjunto = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ANEXA_ADJUNTO"]),
                            EnviaCorreoAvance = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ENVIO_CORREO_AVANCE_AUTOMATICO"]),
                            TextoCorreoAvance = (objEstados.Tables[0].Rows[0]["TEXTO_CORREO_AVANCE_AUTOMATICO"] != System.DBNull.Value ? objEstados.Tables[0].Rows[0]["TEXTO_CORREO_AVANCE_AUTOMATICO"].ToString().Trim() : ""),
                            TipoAnexoCorreoID = (objEstados.Tables[0].Rows[0]["ID_TIPO_ANEXO_CORREO"] != System.DBNull.Value ? Convert.ToInt32(objEstados.Tables[0].Rows[0]["ID_TIPO_ANEXO_CORREO"]) : 0),
                            PermitiAnexarActoAdministrativo = (objEstados.Tables[0].Rows[0]["PERMITIR_ANEXAR_ACTO"] != System.DBNull.Value ? Convert.ToBoolean(objEstados.Tables[0].Rows[0]["PERMITIR_ANEXAR_ACTO"]) : false),
                            PermitiAnexarConceptosActoAdministrativo = (objEstados.Tables[0].Rows[0]["PERMITIR_ANEXAR_CONCEPTOS"] != System.DBNull.Value ? Convert.ToBoolean(objEstados.Tables[0].Rows[0]["PERMITIR_ANEXAR_CONCEPTOS"]) : false),
                            PublicarEstado = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["PUBLICA_ESTADO"]),
                            PublicarPlantilla = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["PUBLICA_PLANTILLA"]),
                            PublicarAdjunto = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["PUBLICA_ADJUNTO"]),
                            SolicitarReferenciaRecepcionNotificacion = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["SOLICITAR_REFERENCIA"]),
                            ReferenciaRecepcionNotificacionObligatoria = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["REFERENCIA_OBLIGATORIA"]),
                            EsEjecutoria = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ESTADO_EJECUTORIA"]),
                            EsEstadoEspera =  Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ESTADO_ESPERA"]),
                            EsNotificacion = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ESTADO_NOTIFICACION"]),
                            GeneraRecurso = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["GENERA_RECURSO"]),
                            EsCitacion = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ESTADO_CITACION"]),
                            EsEdicto = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ESTADO_EDICTO"]),
                            EsAceptacionNotificacion = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ESTADO_ACEPTACION_NOTIFICACION"]),
                            EsRechazoNotificacion = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ESTADO_NEGACION_NOTIFICACION"]),
                            EsAceptacionCitacion = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ESTADO_ACEPTACION_CITACION"]),
                            EsRechazoCitacion = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ESTADO_NEGACION_CITACION"]),
                            EsAnulacion = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ESTADO_ANULACION"]),
                            EsFinalPublicidad = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ESTADO_FINAL_PUBLICIDAD"]),
                            EstadoDependienteID = (objEstados.Tables[0].Rows[0]["ID_ESTADO_DEPENDIENTE"] != System.DBNull.Value ? Convert.ToInt32(objEstados.Tables[0].Rows[0]["ID_ESTADO_DEPENDIENTE"]) : 0),
                            SolicitarInformacionPersonaNotificar = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["SOLICITAR_DATOS_PERSONA_NOTIFICADA"]),
                            Activo = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ACTIVO"])
                        };
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoFlujoNotificacionDalc :: ConsultarConfiguracionEstadoNotificacionElectronica -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoFlujoNotificacionDalc :: ConsultarConfiguracionEstadoNotificacionElectronica -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
                finally
                {
                    if (objCommand != null)
                    {
                        objCommand.Dispose();
                        objCommand = null;
                    }
                    if (objDataBase != null)
                        objDataBase = null;
                }

                return objEstadoFlujo;
            }


            /// <summary>
            /// Retornar el listado de estados que no se encuentran asociados al flujo
            /// </summary>
            /// <param name="p_intFlujoID">int con el identificador del flujo</param>
            /// <returns>List con la información de los estados</returns>
            public List<EstadoNotificacionEntity> ListarEstadosNoFlujoNotificacionElectronica(int p_intFlujoID)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objEstados = null;
                List<EstadoNotificacionEntity> objLstEstados = null;
                EstadoNotificacionEntity objEstadoNoFlujo = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_ESTADO_NOTIFICACION_FLUJO");
                    objDataBase.AddInParameter(objCommand, "@P_ID_FLUJO_NOT_ELEC", DbType.Int32, p_intFlujoID);

                    //Crear registro
                    objEstados = objDataBase.ExecuteDataSet(objCommand);

                    if (objEstados != null && objEstados.Tables.Count > 0 && objEstados.Tables[0].Rows.Count > 0)
                    {
                        objLstEstados = new List<EstadoNotificacionEntity>();

                        foreach (DataRow objEstado in objEstados.Tables[0].Rows)
                        {
                            //Estado del flujo
                            objEstadoNoFlujo = new EstadoNotificacionEntity
                            {
                                ID = Convert.ToInt32(objEstado["ID_ESTADO"]),
                                Descripcion = objEstado["DESCRIPCION"].ToString()
                            };

                            //Adicionar al listado
                            objLstEstados.Add(objEstadoNoFlujo);
                        }
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoFlujoNotificacionDalc :: ListarEstadosNoFlujoNotificacionElectronica -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoFlujoNotificacionDalc :: ListarEstadosNoFlujoNotificacionElectronica -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
                finally
                {
                    if (objCommand != null)
                    {
                        objCommand.Dispose();
                        objCommand = null;
                    }
                    if (objDataBase != null)
                        objDataBase = null;
                }

                return objLstEstados;
            }


            /// <summary>
            /// Crea un nuevo estado asociado al flujo
            /// </summary>
            /// <param name="p_objEstadoFlujo">EstadoFlujoNotificacionEntity con la información del flujo</param>
            public void CrearEstadoFlujo(EstadoFlujoNotificacionEntity p_objEstadoFlujo)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_INSERT_ESTADO_FLUJO_NOTIFICACION_ELECTRONICA");
                    objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO", DbType.Int32, p_objEstadoFlujo.EstadoID);
                    objDataBase.AddInParameter(objCommand, "@P_ID_FLUJO_NOT_ELEC", DbType.Int32, p_objEstadoFlujo.FlujoID);
                    objDataBase.AddInParameter(objCommand, "@P_DESCRIPCION", DbType.String, p_objEstadoFlujo.Descripcion.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_DIAS_VENCIMIENTO", DbType.Int32, p_objEstadoFlujo.DiasVencimiento);
                    objDataBase.AddInParameter(objCommand, "@P_GENERA_PLANTILLA", DbType.Boolean, p_objEstadoFlujo.GeneraPlantilla);
                    if(p_objEstadoFlujo.PlantillaID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_ID_PLANTILLA", DbType.Int32, p_objEstadoFlujo.PlantillaID);
                    objDataBase.AddInParameter(objCommand, "@P_DOCUMENTO_ADICIONAL", DbType.Boolean, p_objEstadoFlujo.DocumentoAdicional);
                    objDataBase.AddInParameter(objCommand, "@P_ENVIO_CORREO_AVANCE_MANUAL", DbType.Boolean, p_objEstadoFlujo.EnviaCorreoAvanceManual);
                    objDataBase.AddInParameter(objCommand, "@P_ENVIO_NOTIFICACION_FISICA", DbType.Boolean, p_objEstadoFlujo.EnviaNotificacionFisica);
                    objDataBase.AddInParameter(objCommand, "@P_ANEXA_ADJUNTO", DbType.Boolean, p_objEstadoFlujo.AnexaAdjunto);
                    objDataBase.AddInParameter(objCommand, "@P_ENVIO_CORREO_AVANCE_AUTOMATICO", DbType.Boolean, p_objEstadoFlujo.EnviaCorreoAvance);
                    if(!string.IsNullOrEmpty(p_objEstadoFlujo.TextoCorreoAvance))
                        objDataBase.AddInParameter(objCommand, "@P_TEXTO_CORREO_AVANCE_AUTOMATICO", DbType.String, p_objEstadoFlujo.TextoCorreoAvance.Trim());
                    if (p_objEstadoFlujo.TipoAnexoCorreoID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_ID_TIPO_ANEXO_CORREO", DbType.Int32, p_objEstadoFlujo.TipoAnexoCorreoID);
                    objDataBase.AddInParameter(objCommand, "@P_PERMITIR_ANEXAR_ACTO", DbType.Boolean, p_objEstadoFlujo.PermitiAnexarActoAdministrativo);
                    objDataBase.AddInParameter(objCommand, "@P_PERMITIR_ANEXAR_CONCEPTOS", DbType.Boolean, p_objEstadoFlujo.PermitiAnexarConceptosActoAdministrativo);
                    objDataBase.AddInParameter(objCommand, "@P_PUBLICA_ESTADO", DbType.Boolean, p_objEstadoFlujo.PublicarEstado);
                    objDataBase.AddInParameter(objCommand, "@P_PUBLICA_PLANTILLA", DbType.Boolean, p_objEstadoFlujo.PublicarPlantilla);
                    objDataBase.AddInParameter(objCommand, "@P_PUBLICA_ADJUNTO", DbType.Boolean, p_objEstadoFlujo.PublicarAdjunto);
                    objDataBase.AddInParameter(objCommand, "@P_SOLICITAR_REFERENCIA", DbType.Boolean, p_objEstadoFlujo.SolicitarReferenciaRecepcionNotificacion);
                    objDataBase.AddInParameter(objCommand, "@P_REFERENCIA_OBLIGATORIA", DbType.Boolean, p_objEstadoFlujo.ReferenciaRecepcionNotificacionObligatoria);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_ESPERA", DbType.Boolean, p_objEstadoFlujo.EsEstadoEspera);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_NOTIFICACION", DbType.Boolean, p_objEstadoFlujo.EsNotificacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_EJECUTORIA", DbType.Boolean, p_objEstadoFlujo.EsEjecutoria);
                    objDataBase.AddInParameter(objCommand, "@P_GENERA_RECURSO", DbType.Boolean, p_objEstadoFlujo.GeneraRecurso);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_CITACION", DbType.Boolean, p_objEstadoFlujo.EsCitacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_EDICTO", DbType.Boolean, p_objEstadoFlujo.EsEdicto);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_ACEPTACION_NOTIFICACION", DbType.Boolean, p_objEstadoFlujo.EsAceptacionNotificacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_NEGACION_NOTIFICACION", DbType.Boolean, p_objEstadoFlujo.EsRechazoNotificacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_ACEPTACION_CITACION", DbType.Boolean, p_objEstadoFlujo.EsAceptacionCitacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_NEGACION_CITACION", DbType.Boolean, p_objEstadoFlujo.EsRechazoCitacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_ANULACION", DbType.Boolean, p_objEstadoFlujo.EsAnulacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_FINAL_PUBLICIDAD", DbType.Boolean, p_objEstadoFlujo.EsFinalPublicidad);
                    if (p_objEstadoFlujo.EstadoDependienteID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_DEPENDIENTE", DbType.Int32, p_objEstadoFlujo.EstadoDependienteID);
                    objDataBase.AddInParameter(objCommand, "@P_SOLICITAR_DATOS_PERSONA_NOTIFICADA", DbType.Boolean, p_objEstadoFlujo.SolicitarInformacionPersonaNotificar);

                    objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_objEstadoFlujo.Activo);

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoFlujoNotificacionDalc :: CrearEstadoFlujo -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoFlujoNotificacionDalc :: CrearEstadoFlujo -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
                finally
                {
                    if (objCommand != null)
                    {
                        objCommand.Dispose();
                        objCommand = null;
                    }
                    if (objDataBase != null)
                        objDataBase = null;
                }
            }


            /// <summary>
            /// Editar la información del estado asociado al flujo
            /// </summary>
            /// <param name="p_objEstadoFlujo">EstadoFlujoNotificacionEntity con la información del estado</param>
            public void EditarEstadoFlujo(EstadoFlujoNotificacionEntity p_objEstadoFlujo)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_UPDATE_ESTADO_FLUJO_NOTIFICACION_ELECTRONICA");
                    objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_FLUJO_NOT_ELEC", DbType.Int32, p_objEstadoFlujo.EstadoFlujoID);
                    objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO", DbType.Int32, p_objEstadoFlujo.EstadoID);
                    objDataBase.AddInParameter(objCommand, "@P_ID_FLUJO_NOT_ELEC", DbType.Int32, p_objEstadoFlujo.FlujoID);
                    objDataBase.AddInParameter(objCommand, "@P_DESCRIPCION", DbType.String, p_objEstadoFlujo.Descripcion.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_DIAS_VENCIMIENTO", DbType.Int32, p_objEstadoFlujo.DiasVencimiento);
                    objDataBase.AddInParameter(objCommand, "@P_GENERA_PLANTILLA", DbType.Boolean, p_objEstadoFlujo.GeneraPlantilla);
                    if (p_objEstadoFlujo.PlantillaID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_ID_PLANTILLA", DbType.Int32, p_objEstadoFlujo.PlantillaID);
                    objDataBase.AddInParameter(objCommand, "@P_DOCUMENTO_ADICIONAL", DbType.Boolean, p_objEstadoFlujo.DocumentoAdicional);
                    objDataBase.AddInParameter(objCommand, "@P_ENVIO_CORREO_AVANCE_MANUAL", DbType.Boolean, p_objEstadoFlujo.EnviaCorreoAvanceManual);
                    objDataBase.AddInParameter(objCommand, "@P_ENVIO_NOTIFICACION_FISICA", DbType.Boolean, p_objEstadoFlujo.EnviaNotificacionFisica);
                    objDataBase.AddInParameter(objCommand, "@P_ANEXA_ADJUNTO", DbType.Boolean, p_objEstadoFlujo.AnexaAdjunto);
                    objDataBase.AddInParameter(objCommand, "@P_ENVIO_CORREO_AVANCE_AUTOMATICO", DbType.Boolean, p_objEstadoFlujo.EnviaCorreoAvance);
                    if (!string.IsNullOrEmpty(p_objEstadoFlujo.TextoCorreoAvance))
                        objDataBase.AddInParameter(objCommand, "@P_TEXTO_CORREO_AVANCE_AUTOMATICO", DbType.String, p_objEstadoFlujo.TextoCorreoAvance.Trim());
                    if (p_objEstadoFlujo.TipoAnexoCorreoID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_ID_TIPO_ANEXO_CORREO", DbType.Int32, p_objEstadoFlujo.TipoAnexoCorreoID);
                    objDataBase.AddInParameter(objCommand, "@P_PERMITIR_ANEXAR_ACTO", DbType.Boolean, p_objEstadoFlujo.PermitiAnexarActoAdministrativo);
                    objDataBase.AddInParameter(objCommand, "@P_PERMITIR_ANEXAR_CONCEPTOS", DbType.Boolean, p_objEstadoFlujo.PermitiAnexarConceptosActoAdministrativo);
                    objDataBase.AddInParameter(objCommand, "@P_PUBLICA_ESTADO", DbType.Boolean, p_objEstadoFlujo.PublicarEstado);
                    objDataBase.AddInParameter(objCommand, "@P_PUBLICA_PLANTILLA", DbType.Boolean, p_objEstadoFlujo.PublicarPlantilla);
                    objDataBase.AddInParameter(objCommand, "@P_PUBLICA_ADJUNTO", DbType.Boolean, p_objEstadoFlujo.PublicarAdjunto);
                    objDataBase.AddInParameter(objCommand, "@P_SOLICITAR_REFERENCIA", DbType.Boolean, p_objEstadoFlujo.SolicitarReferenciaRecepcionNotificacion);
                    objDataBase.AddInParameter(objCommand, "@P_REFERENCIA_OBLIGATORIA", DbType.Boolean, p_objEstadoFlujo.ReferenciaRecepcionNotificacionObligatoria);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_ESPERA", DbType.Boolean, p_objEstadoFlujo.EsEstadoEspera);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_NOTIFICACION", DbType.Boolean, p_objEstadoFlujo.EsNotificacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_EJECUTORIA", DbType.Boolean, p_objEstadoFlujo.EsEjecutoria);
                    objDataBase.AddInParameter(objCommand, "@P_GENERA_RECURSO", DbType.Boolean, p_objEstadoFlujo.GeneraRecurso);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_CITACION", DbType.Boolean, p_objEstadoFlujo.EsCitacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_EDICTO", DbType.Boolean, p_objEstadoFlujo.EsEdicto);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_ACEPTACION_NOTIFICACION", DbType.Boolean, p_objEstadoFlujo.EsAceptacionNotificacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_NEGACION_NOTIFICACION", DbType.Boolean, p_objEstadoFlujo.EsRechazoNotificacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_ACEPTACION_CITACION", DbType.Boolean, p_objEstadoFlujo.EsAceptacionCitacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_NEGACION_CITACION", DbType.Boolean, p_objEstadoFlujo.EsRechazoCitacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_ANULACION", DbType.Boolean, p_objEstadoFlujo.EsAnulacion);
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO_FINAL_PUBLICIDAD", DbType.Boolean, p_objEstadoFlujo.EsFinalPublicidad);
                    if (p_objEstadoFlujo.EstadoDependienteID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_DEPENDIENTE", DbType.Int32, p_objEstadoFlujo.EstadoDependienteID);
                    objDataBase.AddInParameter(objCommand, "@P_SOLICITAR_DATOS_PERSONA_NOTIFICADA", DbType.Boolean, p_objEstadoFlujo.SolicitarInformacionPersonaNotificar);
                    objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_objEstadoFlujo.Activo);

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoFlujoNotificacionDalc :: EditarEstadoFlujo -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoFlujoNotificacionDalc :: EditarEstadoFlujo -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
                finally
                {
                    if (objCommand != null)
                    {
                        objCommand.Dispose();
                        objCommand = null;
                    }
                    if (objDataBase != null)
                        objDataBase = null;
                }
            }

        #endregion

    }
}
