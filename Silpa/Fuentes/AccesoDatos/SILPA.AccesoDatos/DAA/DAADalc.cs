using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SoftManagement.Log;
using System.Data.SqlClient;
using SILPA.Comun;


namespace SILPA.AccesoDatos.DAA
{
    /// <summary>
    /// Clase encargada de las comunicaciones con la base de datos para el caso de uso 
    /// CU-DAA01.
    /// </summary>
    public class DAADalc
    {
        private string silpaConnection;
        private string silaConnection;

        /// <summary>
        /// Contructor de  la clases
        /// </summary>
        public DAADalc()
        {
            silpaConnection = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString;
            silaConnection = ConfigurationManager.ConnectionStrings["SILAMCConnectionString"].ConnectionString;
        }

        /// <summary>
        /// Lista todos los sectores
        /// </summary>
        /// <returns>Dataset con todos los sectores</returns>
        public DataSet ListarSectores()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                DbCommand cmd = db.GetStoredProcCommand("DAA_LISTAR_SECTOR");
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista todos los tipos de proyectos para 
        /// </summary>
        /// <param name="idSector">Define el sector o sector padre con el comodin 0 se listan todos</param>
        /// <param name="idTipoProyecto">Define el SubSector o TipoProyecto con el comodin -1 se listan todos</param>
        /// <returns>Una lista con los sectores que cumplan las condiciones</returns>
        public DataSet ListarTiposProyectosXSector(int idSector, int idTipoProyecto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silaConnection);
                object[] parametros = new object[] { idTipoProyecto, idSector };
                DbCommand cmd = db.GetStoredProcCommand("SS_SEC_RELACIONES_SUB_SEC", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }




        /// <summary>
        /// Lista las autoridades ambientales que tienen jurisdicción sobre un municipio
        /// </summary>
        /// <param name="int_id_mun">Id del municipio del cual se quiere conocer la(s) Autoridad Ambiental que tienen jurisdicción sobre el</param>
        /// <returns>Un DataSet con los identificadores de las autoridades ambientales de un municipio</returns>
        public DataSet ListarAutAmbientalesJurisdicion(int int_id_municipio)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silaConnection);
                object[] parametros = new object[] { int_id_municipio };
                DbCommand cmd = db.GetStoredProcCommand("DAA_MC_LST_AA_X_MUN", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTipoProyecto"></param>
        /// <returns></returns>
        public DataSet ListarIDAutoridadesAmbientales(int idTipoProyecto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { idTipoProyecto };
                DbCommand cmd = db.GetStoredProcCommand("DAA_LISTAR_AA_DEL_T_PROYECTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista en un DataSet los departamentos y los municipios asociados
        /// </summary>
        /// <param name="idDepto">Identificador del Departamento se puede usar el comodin -1 para imprimir todos</param>
        /// <param name="idMun">Identificador del municipio se puede usar el comodin -1 para imprimir todos</param>
        /// <returns>Retorna un DataSet con todos los departamentos y sus municipios definidos en los cireterios de busqueda</returns>
        public DataSet ListarDeptosMunicipios(int idDepto, int idMun)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silaConnection);
                object[] parametros = new object[] { idDepto, idMun };
                DbCommand cmd = db.GetStoredProcCommand("SS_DEPTO_RELACIONES_MUN", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista en un DataSet los departamentos
        /// </summary>
        /// <param name="idDepto">Identificador del Departamento se puede usar el comodin -1 para imprimir todos</param>
        /// <returns>Retorna un DataSet con todos los departamentos</returns>
        public DataSet ListarDeptos(int idDepto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silaConnection);
                object[] parametros = new object[] { idDepto };
                DbCommand cmd = db.GetStoredProcCommand("SS_LISTAR_DEPTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// hava:14-may-10
        /// Reasigna la solicitud de radicación a otra Autoridad Ambiental
        /// </summary>
        /// <param name="objReenviarSolicitud">Objeto reenviar solicitud </param>
        /// <returns>long: identificador del nuevo radicado</returns>
        public long RecibirSolicitudReenvio(ReenviarSolicitud obj)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);

                object[] parametros = new object[] { obj.autIdAsignada, obj.autIdEntrega, obj.numRadicacion, obj.NumeroSilpa, 0 };
                
                DbCommand cmd = db.GetStoredProcCommand("COR_REASIGNAR_AUTORIDAD_RADICACION", parametros);

                int i = db.ExecuteNonQuery(cmd);

                long result  = (long)db.GetParameterValue(cmd, "ID_Radicacion_Nuevo");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// hava:14-may-10
        /// Reasigna la solicitud de radicación a otra Autoridad Ambiental
        /// </summary>
        /// <param name="objReenviarSolicitud">Objeto reenviar solicitud </param>
        /// <returns>long: identificador del nuevo radicado</returns>
        public long RecibirSolicitudReenvio(ReenviarSolicitud obj, ref string ruta, string filetraffic)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);

                object[] parametros = new object[] { obj.autIdAsignada, obj.autIdEntrega, obj.numRadicacion, obj.NumeroSilpa, 0, string.Empty, filetraffic };

                DbCommand cmd = db.GetStoredProcCommand("COR_REASIGNAR_AUTORIDAD_RADICACION", parametros);

                int i = db.ExecuteNonQuery(cmd);

                long result = (long)db.GetParameterValue(cmd, "@ID_Radicacion_Nuevo");
                ruta = (string)db.GetParameterValue(cmd, "@RutaDocumento");
                return result;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al reasignar la solicitud de radicación a otra Autoridad Ambiental.";
                throw new Exception(strException, ex);
                return 0;
            }
        }

        public DataSet ConsultarFormularioJurisdiccion()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                DbCommand cmd = db.GetStoredProcCommand("SMH_CONSULTAR_FORMULARIO_JUR");


                return db.ExecuteDataSet(cmd); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);                
            }
        }


        public DataSet ConsultarListaFormularios()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                DbCommand cmd = db.GetStoredProcCommand("SMH_CONSULTAR_LISTA_FORMULARIOS");

                return db.ExecuteDataSet(cmd); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

      

        public string InsertarFormularioJurisdiccion(int idPadre,int idJur)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);

                object[] parametros = new object[] { idPadre, idJur ,""};

                DbCommand cmd = db.GetStoredProcCommand("SMH_INSERTAR_FORMULARIO_JUR", parametros);

                int i = db.ExecuteNonQuery(cmd);

                string result = db.GetParameterValue(cmd, "@res").ToString();                
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }
        }

        public string EliminarFormularioJurisdiccion(int idReg)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);

                object[] parametros = new object[] { idReg ,""};

                DbCommand cmd = db.GetStoredProcCommand("SMH_ELIMINAR_FORMULARIO_JUR", parametros);

                int i = db.ExecuteNonQuery(cmd);

                string result = db.GetParameterValue(cmd, "@res").ToString();                
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);                
            }
        }




        public DataTable InfoFormularioPrincipal(string numeroSilpa,string tipo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { numeroSilpa, tipo };
                DbCommand cmd = db.GetStoredProcCommand("BPM_CONSULTAR_DATOS_FORMULARIO",parametros);
                cmd.CommandTimeout = 0;
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable InfoTablasHijas(string numeroSilpa)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { numeroSilpa };
                DbCommand cmd = db.GetStoredProcCommand("BPM_CONSULTAR_FORMULARIOS_HIJOS", parametros);
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet InfoTablaHija(int idForm, int entrydata,string tipo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { idForm, entrydata,tipo };
                DbCommand cmd = db.GetStoredProcCommand("BPM_CONSULTAR_FORMULARIO_HIJO", parametros);
                return db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable InfoTablasHijasHijas(string IdEntrydata)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { IdEntrydata };
                DbCommand cmd = db.GetStoredProcCommand("BPM_CONSULTAR_FORMULARIOS_HIJOS_HIJOS", parametros);
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene el listado de autoridades de una solicitud
        /// </summary>
        /// <param name="numeroVital">string con el numero vital</param>
        /// <returns>string con la informacion de las autoridades</returns>
        public DataSet ObtenerAutoridadesSolicitud(string numeroVital)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { numeroVital };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTAR_AUTORIDADES_SOLICITUD_NO_SILPA", parametros);
                return db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene el listado de comunidades de una solicitud
        /// </summary>
        /// <param name="numeroVital">string con el numero vital</param>
        /// <returns>string con la informacion de las comunidades</returns>
        public DataSet ObtenerComunidadesSolicitud(string numeroVital)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { numeroVital };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTAR_COMUNIDADES_SOLICITUD_NO_SILPA", parametros);
                return db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el listado de los registros que se encuentran pendientes de radicación en SIGPRO - (12082020 - FRAMIREZ)
        /// </summary>
        /// <param name="identificadorAutoridadAmbiental">int con el identificador de la autoridad ambiental</param>
        /// <param name="fechaInicial">string con la fecha inicial de busqueda</param>
        /// <param name="fechaFinal">string con la fecha final de busqueda</param>
        /// <returns>dataset con la informacion de los registros que se encuentran pendientes de radicar en SIGPRO</returns>
        public DataSet ObtenerRegistrosPendientesRadicacionSigpro(int identificadorAutoridadAmbiental, string fechaInicial, string fechaFinal)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { identificadorAutoridadAmbiental,fechaInicial,fechaFinal };
                DbCommand cmd = db.GetStoredProcCommand("CONSULTAR_REGISTROS_RADICAR_SIGPRO", parametros);
                return db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el listado de los registros que se encuentran en la tabla WS_TIPO_ARCHIVO_RADICAR_TRAMITE - (14082020 - FRAMIREZ)
        /// </summary>
        /// <param name="idTramite">int con el identificador del tramite</param>
        /// <returns>datatable con la informacion de los registros que coinciden con el identificador del tramite</returns>
        public DataTable ValidarNombreArchivo(int idTramite)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[]
                                          {
                                                idTramite
                                          };
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_NOMBRE_ARCHIVO_TRAMITE", parametros);
                ds = db.ExecuteDataSet(cmd);

                return  ds.Tables[0];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtener la informacion de una solicitud relacionada al numero VITAL indicado
        /// </summary>
        /// <param name="p_strNumeroVITAL">string con el numero VITAL</param>
        /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental. Opcional</param>
        /// <returns>DAASolicitudEntity con la informacion de la solicitud</returns>
        public DAASolicitudEntity ObtenerSolicitudNumeroVITAL(string p_strNumeroVITAL, int p_intAutoridadID = -1)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objSolicitud = null;
            Configuracion objConfiguracion;
            DAASolicitudEntity objDAASolicitudEntity = null;

            try
            {
                //Cargar la cadena de conexion
                objConfiguracion = new Configuracion();

                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("DAA_LISTA_SOLICITUD_NUMERO_VITAL_AUTORIDAD");
                objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVITAL);
                if (p_intAutoridadID > 0)
                    objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_intAutoridadID);

                //Consultar registro
                objSolicitud = objDataBase.ExecuteDataSet(objCommand);

                if (objSolicitud != null && objSolicitud.Tables.Count > 0 && objSolicitud.Tables[0].Rows.Count > 0)
                {
                    //Estado del flujo
                    objDAASolicitudEntity = new DAASolicitudEntity
                    {
                        SolicitudID = (objSolicitud.Tables[0].Rows[0]["ID_SOLICITUD"] != DBNull.Value ? Convert.ToInt64(objSolicitud.Tables[0].Rows[0]["ID_SOLICITUD"]) : -1),
                        TipoTramiteID = (objSolicitud.Tables[0].Rows[0]["TIPO_TRAMITE_ID"] != DBNull.Value ? Convert.ToInt32(objSolicitud.Tables[0].Rows[0]["TIPO_TRAMITE_ID"]) : -1),
                        TipoTramite = (objSolicitud.Tables[0].Rows[0]["TIPO_TRAMITE"] != DBNull.Value ? objSolicitud.Tables[0].Rows[0]["TIPO_TRAMITE"].ToString() : ""),
                        AutoridadID = (objSolicitud.Tables[0].Rows[0]["SOL_ID_AA"] != DBNull.Value ? Convert.ToInt32(objSolicitud.Tables[0].Rows[0]["SOL_ID_AA"]) : -1),
                        Autoridad = (objSolicitud.Tables[0].Rows[0]["SOL_AA"] != DBNull.Value ? objSolicitud.Tables[0].Rows[0]["SOL_AA"].ToString() : ""),
                        SolicitanteID = (objSolicitud.Tables[0].Rows[0]["SOL_ID_SOLICITANTE"] != DBNull.Value ? Convert.ToInt32(objSolicitud.Tables[0].Rows[0]["SOL_ID_SOLICITANTE"]) : -1),
                        Solicitante = (objSolicitud.Tables[0].Rows[0]["SOL_SOLICITANTE"] != DBNull.Value ? objSolicitud.Tables[0].Rows[0]["SOL_SOLICITANTE"].ToString() : ""),
                        RadicacionID = (objSolicitud.Tables[0].Rows[0]["SOL_ID_RADICACION"] != DBNull.Value ? Convert.ToInt64(objSolicitud.Tables[0].Rows[0]["SOL_ID_RADICACION"]) : -1),
                        InstanciProcesoID = (objSolicitud.Tables[0].Rows[0]["SOL_IDPROCESSINSTANCE"] != DBNull.Value ? Convert.ToInt32(objSolicitud.Tables[0].Rows[0]["SOL_IDPROCESSINSTANCE"]) : -1),
                        NumeroVITAL = (objSolicitud.Tables[0].Rows[0]["NUMERO_VITAL"] != DBNull.Value ? objSolicitud.Tables[0].Rows[0]["NUMERO_VITAL"].ToString() : ""),
                        FechaCreacion = (objSolicitud.Tables[0].Rows[0]["SOL_FECHA_CREACION"] != DBNull.Value ? Convert.ToDateTime(objSolicitud.Tables[0].Rows[0]["SOL_FECHA_CREACION"]) : default(DateTime)),
                        TieneSolicitudAsignacionPendiente = (objSolicitud.Tables[0].Rows[0]["TIENE_REASIGNACION_PENDIENTE"] != DBNull.Value ? Convert.ToBoolean(objSolicitud.Tables[0].Rows[0]["TIENE_REASIGNACION_PENDIENTE"]) : false)
                    };
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ObtenerSolicitudNumeroVITAL -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ObtenerSolicitudNumeroVITAL -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return objDAASolicitudEntity;
        }


        /// <summary>
        /// Obtener la informacion de una solicitud relacionada al numero VITAL indicado
        /// </summary>
        /// <param name="p_strNumeroVITAL">string con el numero VITAL</param>
        /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental. Opcional</param>
        /// <returns>DAASolicitudEntity con la informacion de la solicitud</returns>
        public void ReasignarSolicitud(string p_strNumeroVITAL, int p_intAutoridadReasignarID, int p_intSolicitudReasignacionID = -1, int p_SolicitanteID = -1)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            Configuracion objConfiguracion;

            try
            {
                //Cargar la cadena de conexion
                objConfiguracion = new Configuracion();

                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("GEN_REASIGNAR_SOLICITUD");
                objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVITAL);
                objDataBase.AddInParameter(objCommand, "@P_ID_AUT_REASIGNAR", DbType.Int32, p_intAutoridadReasignarID);
                if (p_intSolicitudReasignacionID > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_SOLICITUD_REASIGNACION", DbType.Int32, p_intSolicitudReasignacionID);
                if (p_SolicitanteID > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_SOLICITANTE", DbType.Int32, p_SolicitanteID);

                //Realizar reasignacion
                objDataBase.ExecuteNonQuery(objCommand);

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ReasignarSolicitud -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ReasignarSolicitud -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
        /// Obtiene el listado de estados que puede tener una solicitud de reasignacion
        /// </summary>
        /// <returns>List con la información de los estados</returns>
        public List<DAASolicitudEstadoReasignacionEntity> ObtenerEstadosSolicitudReasignacion()
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objEstados = null;
            Configuracion objConfiguracion;
            List<DAASolicitudEstadoReasignacionEntity> objLstEstados = new List<DAASolicitudEstadoReasignacionEntity>();
            DAASolicitudEstadoReasignacionEntity objDAASolicitudEstadoReasignacionEntity = null;

            try
            {
                //Cargar la cadena de conexion
                objConfiguracion = new Configuracion();

                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("GEN_LISTA_SOLICITUD_REASIGNACION");

                //Consultar registro
                objEstados = objDataBase.ExecuteDataSet(objCommand);

                if (objEstados != null && objEstados.Tables.Count > 0 && objEstados.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow objEstado in objEstados.Tables[0].Rows)
                    {
                        //Estado
                        objDAASolicitudEstadoReasignacionEntity = new DAASolicitudEstadoReasignacionEntity
                        {
                            EstadoID = (objEstado["ID_ESTADO_SOLICITUD_REASIGNACION"] != DBNull.Value ? Convert.ToInt32(objEstado["ID_ESTADO_SOLICITUD_REASIGNACION"]) : -1),
                            Estado = (objEstado["ESTADO_SOLICITUD_REASIGNACION"] != DBNull.Value ? objEstado["ESTADO_SOLICITUD_REASIGNACION"].ToString() : "")                          
                        };

                        objLstEstados.Add(objDAASolicitudEstadoReasignacionEntity);
                    }


                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ObtenerEstadosSolicitudReasignacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ObtenerEstadosSolicitudReasignacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
        /// Crea una solicitud de reasignacion
        /// </summary>
        /// <param name="p_objDAASolicitudReasignacionEntity">DAASolicitudReasignacionEntity con la información de la solicitud de reasignación</param>
        /// <returns>int con el identificador de la solicitud de reasignación</returns>
        public int InsertarSolicitudReasignacion(DAASolicitudReasignacionEntity p_objDAASolicitudReasignacionEntity)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            Configuracion objConfiguracion;
            DataSet objResultado;
            int intSolicitudReasignacionID = -1;

            try
            {
                //Cargar la cadena de conexion
                objConfiguracion = new Configuracion();

                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("GEN_INSERTAR_SOLICITUD_REASIGNACION");
                objDataBase.AddInParameter(objCommand, "@P_ID_AUT_SOLICITANTE", DbType.Int32, p_objDAASolicitudReasignacionEntity.AutoridadAmbientalSolicitanteID);
                objDataBase.AddInParameter(objCommand, "@P_ID_AUT_REASIGNAR", DbType.Int32, p_objDAASolicitudReasignacionEntity.AutoridadAmbientalReasignarID);
                objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.String, p_objDAASolicitudReasignacionEntity.NumeroVITAL);
                if (p_objDAASolicitudReasignacionEntity.SolicitanteAutoridadVerificaID != null && p_objDAASolicitudReasignacionEntity.SolicitanteAutoridadVerificaID > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_USUARIO_AUTORIDAD_SOLICITA", DbType.Int32, p_objDAASolicitudReasignacionEntity.SolicitanteAutoridadVerificaID);

                //Realizar reasignacion
                objResultado = objDataBase.ExecuteDataSet(objCommand);

                //Cargar el codigo 
                if (objResultado != null && objResultado.Tables.Count > 0 && objResultado.Tables[0].Rows.Count > 0)
                {
                    intSolicitudReasignacionID = Convert.ToInt32(objResultado.Tables[0].Rows[0]["ID_SOLICITUD_REASIGNACION"]);
                }

                if (intSolicitudReasignacionID <= 0)
                    throw new Exception("Problema durante ingreso de la solicitud. No se generó código.");
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: InsertarSolicitudReasignacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: InsertarSolicitudReasignacion -> Error inesperado: " + exc.Message + " " + (exc.StackTrace != null ? exc.StackTrace.ToString() : ""));

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

            return intSolicitudReasignacionID;
        }

        /// <summary>
        /// Realiza la actualizacion de la solicitud de reasignacion
        /// </summary>
        /// <param name="p_objDAASolicitudReasignacionEntity">DAASolicitudReasignacionEntity con la informacion que se actualizará</param>
        public void ActualizarSolicitudReasignacion(DAASolicitudReasignacionEntity p_objDAASolicitudReasignacionEntity)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            Configuracion objConfiguracion;

            try
            {
                //Cargar la cadena de conexion
                objConfiguracion = new Configuracion();

                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("GEN_ACTUALIZAR_SOLICITUD_REASIGNACION");
                if (p_objDAASolicitudReasignacionEntity.SolicitudReasignacionID > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_SOLICITUD_REASIGNACION", DbType.Int32, p_objDAASolicitudReasignacionEntity.SolicitudReasignacionID);
                if (!string.IsNullOrEmpty(p_objDAASolicitudReasignacionEntity.CodigoReasignacion))
                    objDataBase.AddInParameter(objCommand, "@P_CODIGO_REASIGNACION", DbType.String, p_objDAASolicitudReasignacionEntity.CodigoReasignacion);
                if (p_objDAASolicitudReasignacionEntity.SolicitanteAutoridadVerificaID != null && p_objDAASolicitudReasignacionEntity.SolicitanteAutoridadVerificaID > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_USUARIO_AUTORIDAD_VERIFICA", DbType.Int32, p_objDAASolicitudReasignacionEntity.SolicitanteAutoridadVerificaID);
                objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_SOLICITUD_REASIGNACION", DbType.Int32, p_objDAASolicitudReasignacionEntity.EstadoSolicitudID);

                //Realizar actualización
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ActualizarSolicitudReasignacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ActualizarSolicitudReasignacion -> Error inesperado: " + exc.Message + " " + (exc.StackTrace != null ? exc.StackTrace.ToString() : ""));

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
        /// Obtener solicitud de reasignacion
        /// </summary>
        /// <param name="p_intSolicitudReasignacionID">int con el identificador de solicitud de reasignacion</param>
        /// <returns>DAASolicitudReasignacionEntity con la información de las solicitud de reasignación</returns>
        public DAASolicitudReasignacionEntity ObtenerSolicitudReasignacion(int p_intSolicitudReasignacionID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objSolicitudes = null;
            Configuracion objConfiguracion;
            DAASolicitudReasignacionEntity objDAASolicitudReasignacionEntity = null;

            try
            {
                //Cargar la cadena de conexion
                objConfiguracion = new Configuracion();

                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("GEN_CONSULTA_SOLICITUD_REASIGNACION_AUTORIDAD");
                objDataBase.AddInParameter(objCommand, "@P_ID_SOLICITUD_REASIGNACION", DbType.Int32, p_intSolicitudReasignacionID);

                //Consultar registro
                objSolicitudes = objDataBase.ExecuteDataSet(objCommand);

                if (objSolicitudes != null && objSolicitudes.Tables.Count > 0 && objSolicitudes.Tables[0].Rows.Count > 0)
                {
                    //Solicitud
                    objDAASolicitudReasignacionEntity = new DAASolicitudReasignacionEntity
                    {
                        SolicitudReasignacionID = (objSolicitudes.Tables[0].Rows[0]["ID_SOLICITUD_REASIGNACION"] != DBNull.Value ? Convert.ToInt32(objSolicitudes.Tables[0].Rows[0]["ID_SOLICITUD_REASIGNACION"]) : -1),
                        CodigoReasignacion = (objSolicitudes.Tables[0].Rows[0]["CODIGO_REASIGNACION"] != DBNull.Value ? objSolicitudes.Tables[0].Rows[0]["CODIGO_REASIGNACION"].ToString() : ""),
                        AutoridadAmbientalSolicitanteID = (objSolicitudes.Tables[0].Rows[0]["ID_AUT_SOLICITANTE"] != DBNull.Value ? Convert.ToInt32(objSolicitudes.Tables[0].Rows[0]["ID_AUT_SOLICITANTE"]) : -1),
                        AutoridadAmbientalSolicitante = (objSolicitudes.Tables[0].Rows[0]["AUT_SOLICITANTE"] != DBNull.Value ? objSolicitudes.Tables[0].Rows[0]["AUT_SOLICITANTE"].ToString() : ""),
                        AutoridadAmbientalReasignarID = (objSolicitudes.Tables[0].Rows[0]["ID_AUT_REASIGNAR"] != DBNull.Value ? Convert.ToInt32(objSolicitudes.Tables[0].Rows[0]["ID_AUT_REASIGNAR"]) : -1),
                        AutoridadAmbientalReasignar = (objSolicitudes.Tables[0].Rows[0]["AUT_REASIGNAR"] != DBNull.Value ? objSolicitudes.Tables[0].Rows[0]["AUT_REASIGNAR"].ToString() : ""),
                        NumeroVITAL = (objSolicitudes.Tables[0].Rows[0]["NUMERO_VITAL"] != DBNull.Value ? objSolicitudes.Tables[0].Rows[0]["NUMERO_VITAL"].ToString() : ""),
                        SolicitudID = (objSolicitudes.Tables[0].Rows[0]["ID_SOLICITUD"] != DBNull.Value ? Convert.ToInt64(objSolicitudes.Tables[0].Rows[0]["ID_SOLICITUD"]) : -1),
                        FechaSolicitud = (objSolicitudes.Tables[0].Rows[0]["FECHA_SOLICITUD"] != DBNull.Value ? Convert.ToDateTime(objSolicitudes.Tables[0].Rows[0]["FECHA_SOLICITUD"]) : default(DateTime)),
                        InstanciaProcesoID = (objSolicitudes.Tables[0].Rows[0]["IDProcessInstance"] != DBNull.Value ? Convert.ToInt32(objSolicitudes.Tables[0].Rows[0]["IDProcessInstance"]) : -1),
                        SolicitanteID = (objSolicitudes.Tables[0].Rows[0]["ID_SOLICITANTE"] != DBNull.Value ? Convert.ToInt32(objSolicitudes.Tables[0].Rows[0]["ID_SOLICITANTE"]) : -1),
                        Solicitante = (objSolicitudes.Tables[0].Rows[0]["SOLICITANTE"] != DBNull.Value ? objSolicitudes.Tables[0].Rows[0]["SOLICITANTE"].ToString() : ""),
                        SolicitanteAutoridadID = (objSolicitudes.Tables[0].Rows[0]["ID_USUARIO_AUTORIDAD_SOLICITA"] != DBNull.Value ? Convert.ToInt32(objSolicitudes.Tables[0].Rows[0]["ID_USUARIO_AUTORIDAD_SOLICITA"]) : -1),
                        SolicitanteAutoridadVerificaID = (objSolicitudes.Tables[0].Rows[0]["ID_USUARIO_AUTORIDAD_VERIFICA"] != DBNull.Value ? Convert.ToInt32(objSolicitudes.Tables[0].Rows[0]["ID_USUARIO_AUTORIDAD_VERIFICA"]) : -1),
                        EstadoSolicitudID = (objSolicitudes.Tables[0].Rows[0]["ID_ESTADO_SOLICITUD_REASIGNACION"] != DBNull.Value ? Convert.ToInt32(objSolicitudes.Tables[0].Rows[0]["ID_ESTADO_SOLICITUD_REASIGNACION"]) : -1),
                        EstadoSolicitud = (objSolicitudes.Tables[0].Rows[0]["ESTADO_SOLICITUD_REASIGNACION"] != DBNull.Value ? objSolicitudes.Tables[0].Rows[0]["ESTADO_SOLICITUD_REASIGNACION"].ToString() : ""),
                        FechaVerificacionSolicitudReasignacion = (objSolicitudes.Tables[0].Rows[0]["FECHA_VERIFICACION"] != DBNull.Value ? Convert.ToDateTime(objSolicitudes.Tables[0].Rows[0]["FECHA_VERIFICACION"]) : default(DateTime)),
                        FechaCreacionSolicitudReasignacion = (objSolicitudes.Tables[0].Rows[0]["FECHA_CREACION"] != DBNull.Value ? Convert.ToDateTime(objSolicitudes.Tables[0].Rows[0]["FECHA_CREACION"]) : default(DateTime)),
                        FechaActualizacionSolicitudReasignacion = (objSolicitudes.Tables[0].Rows[0]["FECHA_ULTIMA_MODIFICACION"] != DBNull.Value ? Convert.ToDateTime(objSolicitudes.Tables[0].Rows[0]["FECHA_ULTIMA_MODIFICACION"]) : default(DateTime))
                    };
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ObtenerSolicitudReasignacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ObtenerSolicitudReasignacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return objDAASolicitudReasignacionEntity;
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
        /// <returns>List con la información de las solicitudes de reasignación</returns>
        public List<DAASolicitudReasignacionEntity> ObtenerSolicitudesReasignacionRealizadasAutoridad(int p_intAutoridadID, string p_strNumeroVITAL, int p_intAutoridadReasignar, int p_intEstadoSolicitudID, DateTime p_objFechaSolicitudInicial, DateTime p_objFechaSolicitudFinal)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objSolicitudes = null;
            Configuracion objConfiguracion;
            List<DAASolicitudReasignacionEntity> objLstDAASolicitudReasignaciones = new List<DAASolicitudReasignacionEntity>();
            DAASolicitudReasignacionEntity objDAASolicitudReasignacionEntity = null;

            try
            {
                //Cargar la cadena de conexion
                objConfiguracion = new Configuracion();

                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("GEN_LISTA_SOLICITUD_REASIGNACION_AUTORIDAD");
                objDataBase.AddInParameter(objCommand, "@P_ID_AUT", DbType.Int32, p_intAutoridadID);
                if (!string.IsNullOrEmpty(p_strNumeroVITAL))
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVITAL);
                if (p_intAutoridadReasignar > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_AUT_REASIGNAR", DbType.Int32, p_intAutoridadReasignar);
                if (p_intEstadoSolicitudID > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_SOLICITUD_REASIGNACION", DbType.Int32, p_intEstadoSolicitudID);
                objDataBase.AddInParameter(objCommand, "@P_FECHA_SOLICITUD_INICIAL", DbType.DateTime, p_objFechaSolicitudInicial);
                objDataBase.AddInParameter(objCommand, "@P_FECHA_SOLICITUD_FINAL", DbType.DateTime, p_objFechaSolicitudFinal);

                //Consultar registro
                objSolicitudes = objDataBase.ExecuteDataSet(objCommand);

                if (objSolicitudes != null && objSolicitudes.Tables.Count > 0 && objSolicitudes.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow objSolicitud in objSolicitudes.Tables[0].Rows)
                    {
                        //Solicitud
                        objDAASolicitudReasignacionEntity = new DAASolicitudReasignacionEntity
                        {
                            SolicitudReasignacionID = (objSolicitud["ID_SOLICITUD_REASIGNACION"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_SOLICITUD_REASIGNACION"]) : -1),
                            CodigoReasignacion = (objSolicitud["CODIGO_REASIGNACION"] != DBNull.Value ? objSolicitud["CODIGO_REASIGNACION"].ToString() : ""),
                            AutoridadAmbientalSolicitanteID = (objSolicitud["ID_AUT_SOLICITANTE"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_AUT_SOLICITANTE"]) : -1),
                            AutoridadAmbientalSolicitante = (objSolicitud["AUT_SOLICITANTE"] != DBNull.Value ? objSolicitud["AUT_SOLICITANTE"].ToString() : ""),
                            AutoridadAmbientalReasignarID = (objSolicitud["ID_AUT_REASIGNAR"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_AUT_REASIGNAR"]) : -1),
                            AutoridadAmbientalReasignar = (objSolicitud["AUT_REASIGNAR"] != DBNull.Value ? objSolicitud["AUT_REASIGNAR"].ToString() : ""),
                            NumeroVITAL = (objSolicitud["NUMERO_VITAL"] != DBNull.Value ? objSolicitud["NUMERO_VITAL"].ToString() : ""),
                            SolicitudID = (objSolicitud["ID_SOLICITUD"] != DBNull.Value ? Convert.ToInt64(objSolicitud["ID_SOLICITUD"]) : -1),
                            FechaSolicitud = (objSolicitud["FECHA_SOLICITUD"] != DBNull.Value ? Convert.ToDateTime(objSolicitud["FECHA_SOLICITUD"]) : default(DateTime)),
                            InstanciaProcesoID = (objSolicitud["IDProcessInstance"] != DBNull.Value ? Convert.ToInt32(objSolicitud["IDProcessInstance"]) : -1),
                            SolicitanteID = (objSolicitud["ID_SOLICITANTE"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_SOLICITANTE"]) : -1),
                            Solicitante = (objSolicitud["SOLICITANTE"] != DBNull.Value ? objSolicitud["SOLICITANTE"].ToString() : ""),
                            SolicitanteAutoridadID = (objSolicitud["ID_USUARIO_AUTORIDAD_SOLICITA"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_USUARIO_AUTORIDAD_SOLICITA"]) : -1),
                            SolicitanteAutoridadVerificaID = (objSolicitud["ID_USUARIO_AUTORIDAD_VERIFICA"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_USUARIO_AUTORIDAD_VERIFICA"]) : -1),
                            EstadoSolicitudID = (objSolicitud["ID_ESTADO_SOLICITUD_REASIGNACION"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_ESTADO_SOLICITUD_REASIGNACION"]) : -1),
                            EstadoSolicitud = (objSolicitud["ESTADO_SOLICITUD_REASIGNACION"] != DBNull.Value ? objSolicitud["ESTADO_SOLICITUD_REASIGNACION"].ToString() : ""),
                            FechaVerificacionSolicitudReasignacion = (objSolicitud["FECHA_VERIFICACION"] != DBNull.Value ? Convert.ToDateTime(objSolicitud["FECHA_VERIFICACION"]) : default(DateTime)),
                            FechaCreacionSolicitudReasignacion = (objSolicitud["FECHA_CREACION"] != DBNull.Value ? Convert.ToDateTime(objSolicitud["FECHA_CREACION"]) : default(DateTime)),
                            FechaActualizacionSolicitudReasignacion = (objSolicitud["FECHA_ULTIMA_MODIFICACION"] != DBNull.Value ? Convert.ToDateTime(objSolicitud["FECHA_ULTIMA_MODIFICACION"]) : default(DateTime))
                        };

                        objLstDAASolicitudReasignaciones.Add(objDAASolicitudReasignacionEntity);
                    }

                    
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ObtenerSolicitudesReasignacionRealizadasAutoridad -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ObtenerSolicitudesReasignacionRealizadasAutoridad -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return objLstDAASolicitudReasignaciones;
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
        /// <returns>List con la información de las solicitudes de reasignación</returns>
        public List<DAASolicitudReasignacionEntity> ObtenerSolicitudesReasignacionAsignadasAutoridad(int p_intAutoridadID, string p_strNumeroVITAL, int p_intAutoridadSolicitante, int p_intEstadoSolicitudID, DateTime p_objFechaSolicitudInicial, DateTime p_objFechaSolicitudFinal)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objSolicitudes = null;
            Configuracion objConfiguracion;
            List<DAASolicitudReasignacionEntity> objLstDAASolicitudReasignaciones = new List<DAASolicitudReasignacionEntity>();
            DAASolicitudReasignacionEntity objDAASolicitudReasignacionEntity = null;

            try
            {
                //Cargar la cadena de conexion
                objConfiguracion = new Configuracion();

                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("GEN_LISTA_SOLICITUD_REASIGNACION_AUTORIDAD_RECIBIDAS");
                objDataBase.AddInParameter(objCommand, "@P_ID_AUT", DbType.Int32, p_intAutoridadID);
                if (!string.IsNullOrEmpty(p_strNumeroVITAL))
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVITAL);
                if (p_intAutoridadSolicitante > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_AUT_SOLICITANTE", DbType.Int32, p_intAutoridadSolicitante);
                if (p_intEstadoSolicitudID > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_SOLICITUD_REASIGNACION", DbType.Int32, p_intEstadoSolicitudID);
                objDataBase.AddInParameter(objCommand, "@P_FECHA_SOLICITUD_INICIAL", DbType.DateTime, p_objFechaSolicitudInicial);
                objDataBase.AddInParameter(objCommand, "@P_FECHA_SOLICITUD_FINAL", DbType.DateTime, p_objFechaSolicitudFinal);

                //Consultar registro
                objSolicitudes = objDataBase.ExecuteDataSet(objCommand);

                if (objSolicitudes != null && objSolicitudes.Tables.Count > 0 && objSolicitudes.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow objSolicitud in objSolicitudes.Tables[0].Rows)
                    {
                        //Solicitud
                        objDAASolicitudReasignacionEntity = new DAASolicitudReasignacionEntity
                        {
                            SolicitudReasignacionID = (objSolicitud["ID_SOLICITUD_REASIGNACION"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_SOLICITUD_REASIGNACION"]) : -1),
                            CodigoReasignacion = (objSolicitud["CODIGO_REASIGNACION"] != DBNull.Value ? objSolicitud["CODIGO_REASIGNACION"].ToString() : ""),
                            AutoridadAmbientalSolicitanteID = (objSolicitud["ID_AUT_SOLICITANTE"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_AUT_SOLICITANTE"]) : -1),
                            AutoridadAmbientalSolicitante = (objSolicitud["AUT_SOLICITANTE"] != DBNull.Value ? objSolicitud["AUT_SOLICITANTE"].ToString() : ""),
                            AutoridadAmbientalReasignarID = (objSolicitud["ID_AUT_REASIGNAR"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_AUT_REASIGNAR"]) : -1),
                            AutoridadAmbientalReasignar = (objSolicitud["AUT_REASIGNAR"] != DBNull.Value ? objSolicitud["AUT_REASIGNAR"].ToString() : ""),
                            NumeroVITAL = (objSolicitud["NUMERO_VITAL"] != DBNull.Value ? objSolicitud["NUMERO_VITAL"].ToString() : ""),
                            SolicitudID = (objSolicitud["ID_SOLICITUD"] != DBNull.Value ? Convert.ToInt64(objSolicitud["ID_SOLICITUD"]) : -1),
                            FechaSolicitud = (objSolicitud["FECHA_SOLICITUD"] != DBNull.Value ? Convert.ToDateTime(objSolicitud["FECHA_SOLICITUD"]) : default(DateTime)),
                            InstanciaProcesoID = (objSolicitud["IDProcessInstance"] != DBNull.Value ? Convert.ToInt32(objSolicitud["IDProcessInstance"]) : -1),
                            SolicitanteID = (objSolicitud["ID_SOLICITANTE"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_SOLICITANTE"]) : -1),
                            Solicitante = (objSolicitud["SOLICITANTE"] != DBNull.Value ? objSolicitud["SOLICITANTE"].ToString() : ""),
                            SolicitanteAutoridadID = (objSolicitud["ID_USUARIO_AUTORIDAD_SOLICITA"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_USUARIO_AUTORIDAD_SOLICITA"]) : -1),
                            SolicitanteAutoridadVerificaID = (objSolicitud["ID_USUARIO_AUTORIDAD_VERIFICA"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_USUARIO_AUTORIDAD_VERIFICA"]) : -1),
                            EstadoSolicitudID = (objSolicitud["ID_ESTADO_SOLICITUD_REASIGNACION"] != DBNull.Value ? Convert.ToInt32(objSolicitud["ID_ESTADO_SOLICITUD_REASIGNACION"]) : -1),
                            EstadoSolicitud = (objSolicitud["ESTADO_SOLICITUD_REASIGNACION"] != DBNull.Value ? objSolicitud["ESTADO_SOLICITUD_REASIGNACION"].ToString() : ""),
                            FechaVerificacionSolicitudReasignacion = (objSolicitud["FECHA_VERIFICACION"] != DBNull.Value ? Convert.ToDateTime(objSolicitud["FECHA_VERIFICACION"]) : default(DateTime)),
                            FechaCreacionSolicitudReasignacion = (objSolicitud["FECHA_CREACION"] != DBNull.Value ? Convert.ToDateTime(objSolicitud["FECHA_CREACION"]) : default(DateTime)),
                            FechaActualizacionSolicitudReasignacion = (objSolicitud["FECHA_ULTIMA_MODIFICACION"] != DBNull.Value ? Convert.ToDateTime(objSolicitud["FECHA_ULTIMA_MODIFICACION"]) : default(DateTime))
                        };

                        objLstDAASolicitudReasignaciones.Add(objDAASolicitudReasignacionEntity);
                    }


                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ObtenerSolicitudesReasignacionAsignadasAutoridad -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAADalc :: ObtenerSolicitudesReasignacionAsignadasAutoridad -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return objLstDAASolicitudReasignaciones;
        }
    }
}
