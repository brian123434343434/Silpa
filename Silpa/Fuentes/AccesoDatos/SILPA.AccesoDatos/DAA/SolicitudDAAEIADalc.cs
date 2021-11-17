using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Utilidades;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System.Data.Common;
using System.Data.SqlClient;
using SILPA.AccesoDatos.Generico;
using System.IO;

namespace SILPA.AccesoDatos.DAA
{
    public class SolicitudDAAEIADalc
    {
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor Vacio
        /// </summary>
        public SolicitudDAAEIADalc()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Contiene los datos de la solicitud de DAA
        /// </summary>
        public SolicitudDAAEIAIdentity Identity;

        /// <summary>
        /// Modifica en la tabla Solicitud
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los valores de los campos que se van a modificiar</param>
        public void ActualizarSolicitud(ref SolicitudDAAEIAIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] 
                                      { 
                                          objIdentity.IdSolicitud,
                                          objIdentity.IdSector,
                                          objIdentity.IdTipoProyecto,
                                          objIdentity.IdUbicacion,
                                          objIdentity.IdAutoridadAmbiental,
                                          objIdentity.IdSolicitante,
                                          objIdentity.Urbano,
                                          objIdentity.IdRadicacion,
                                          objIdentity.Conflicto,
                                          //objIdentity.FechaCreacion, -NO, saca error entonces no!
                                          objIdentity.IdProcessInstance,
                                          objIdentity.IdTipoEstadoSolicitud,
                                          objIdentity.IdTipoTramite,
                                          objIdentity.NumeroSilpa 
                                      };

                DbCommand cmd = db.GetStoredProcCommand("DAA_UPDATE_SOLICITUD", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Actualiza el estado de proceso de la solicitud DAA
        /// </summary>
        /// <param name="objIdentity">Object: SolicitudDAAEIAIdentity</param>
        public void ActualizarTipoEstadoProceso(SolicitudDAAEIAIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { objIdentity.IdSolicitud, objIdentity.IdProcessInstance };

                DbCommand cmd = db.GetStoredProcCommand("LOG_CAMBIA_TIPO_ESTADO_PROCESO", parametros);
                int intResultado = db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene el listado de las autoridades ambientales en conflicto de competencia
        /// </summary>
        /// <param name="objIdentity">Object: int64ProcessInstance </param>
        public void DeterminarConflicto(ref SolicitudDAAEIAIdentity objIdentity)
        {
            objIdentity.Conflicto = false;
        }

        /// <summary>
        /// Obtiene el estado del proceso
        /// </summary>
        /// <param name="objIdentity">SolicitudDAAEIAIdentity: objIdentity </param>
        public void ObtenerEstadoProceso(SectorIdentity objIdentity, ref SolicitudDAAEIAIdentity objSolicitudIdentity)
        {
            try
            {
                SoftManagement.Log.SMLog.Escribir(SoftManagement.Log.Severidad.Informativo,"++++ObtenerEstadoProceso");
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                
                object[] parametros = new object[] { objSolicitudIdentity.Conflicto, objSolicitudIdentity.IdProcessInstance, objSolicitudIdentity.IdTipoEstadoSolicitud };
                DbCommand cmd = db.GetStoredProcCommand("DAA_OBTENER_ESTADO_PROCESO", parametros);

                db.ExecuteDataSet(cmd);

                /// se toma el identificador del registro de la solicitud DAA
                objSolicitudIdentity.IdTipoEstadoSolicitud = Int32.Parse(cmd.Parameters["@IdEstado"].Value.ToString());

            }
            catch (Exception ex)
            {
                SoftManagement.Log.SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "++++ObtenerEstadoProceso Error:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Insertar en la tabla Expediente_Ubicacion
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los valores de los campos que se van a insertar</param>

        /// </summary>
        /// jmartinez para la insercion de la solicitud devuelvo el numero de vital y el id de solicitud que me sirve de base para extraer la informacion restante para llamar el servicio we de sigpro
        public SolicitudDAAEIAIdentity InsertarSolicitud(ref SolicitudDAAEIAIdentity objIdentity)
        //public void InsertarSolicitud(ref SolicitudDAAEIAIdentity objIdentity) -- jmartinez
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] 
                                      { 
                                          objIdentity.IdSector,
                                          objIdentity.IdTipoProyecto,
                                          objIdentity.IdUbicacion, // ?
                                          objIdentity.IdAutoridadAmbiental, // Si la jurisdicción es de 1 AA se inserta inmediatamente, si son hidrocarburos se inserta el MAVDT, para las demás opciones se inserta lo que escoja el usuario más adelante
                                          objIdentity.IdSolicitante,
                                          objIdentity.Urbano, // ?
                                          objIdentity.IdRadicacion, // ?
                                          objIdentity.Conflicto, // ?
                                          objIdentity.FechaCreacion, 
                                          objIdentity.IdProcessInstance, 
                                          objIdentity.IdTipoEstadoSolicitud,
                                          objIdentity.IdTipoTramite,
                                          objIdentity.IdSolicitud,
                                          objIdentity.NumeroSilpa
                                      };


                DbCommand cmd = db.GetStoredProcCommand("DAA_INSERT_SOLICITUD", parametros);

                db.ExecuteDataSet(cmd);

                /// se toma el identificador del registro de la solicitud DAA
                objIdentity.IdSolicitud = Int64.Parse(cmd.Parameters["@ID_SOLICITUD"].Value.ToString());
                objIdentity.NumeroSilpa = cmd.Parameters["@P_NUMERO_SILPA"].Value.ToString();
                return objIdentity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// Insertar en la tabla Expediente_Ubicacion
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los valores de los campos que se van a insertar</param>
        public void InsertarSolicitudEE(ref SolicitudDAAEIAIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] 
                                      { 
                                          objIdentity.IdSector,
                                          objIdentity.IdTipoProyecto,
                                          objIdentity.IdUbicacion, // ?
                                          objIdentity.IdAutoridadAmbiental, // Si la jurisdicción es de 1 AA se inserta inmediatamente, si son hidrocarburos se inserta el MAVDT, para las demás opciones se inserta lo que escoja el usuario más adelante
                                          objIdentity.IdSolicitante,
                                          objIdentity.Urbano, // ?
                                          objIdentity.IdRadicacion, // ?
                                          objIdentity.Conflicto, // ?
                                          objIdentity.FechaCreacion, 
                                          objIdentity.IdProcessInstance, 
                                          objIdentity.IdTipoEstadoSolicitud,
                                          objIdentity.IdTipoTramite,
                                          objIdentity.IdSolicitud
                                      };


                DbCommand cmd = db.GetStoredProcCommand("DAA_INSERT_SOLICITUD_EE", parametros);

                db.ExecuteDataSet(cmd);

                /// se toma el identificador del registro de la solicitud DAA
                objIdentity.IdSolicitud = Int64.Parse(cmd.Parameters["@ID_SOLICITUD"].Value.ToString());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Elimina un registro de la tabla Solicitud
        /// </summary>
        /// <param name="objIdentity.IdSolicitud">Identificador del registro del tipo estado proceso a eliminar</param>
        public void EliminarTipoEstadoProceso(ref SolicitudDAAEIAIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { objIdentity.IdSolicitud };

                DbCommand cmd = db.GetStoredProcCommand("DAA_DELETE_SOLICITUD", parametros);
                db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// Retorna un DataSet con los datos de una o varias solicitudes segun los parametros de filtrado
        /// </summary>
        /// <param name="lngIdSolicitud">Indentificador de la solicitud</param>
        /// <param name="IntIdTipoEstadoSol">indentificador del tipo de estado de la solicud</param>
        /// <param name="lngIdProcessInstance">indentificador del instance process</param>
        /// <returns>DataSet: Conjunto de resultados de la solicictud con las siguientes columnas: 
        /// ID_SOLICITUD, SOL_ID_SECTOR, SOL_ID_TIPO_PROYECTO, SOL_ID_UBICACION, SOL_ID_AA, SOL_ID_SOLICITANTE, 
        /// SOL_ID_RADICACION, SOL_BOL_CONFLICTO, SOL_FECHA_CREACION, SOL_IDPROCESSINSTANCE, TES_ID_TIPO_ESTADO_SOLICITUD</returns>
        public DataSet ObtenerSolicitud(Nullable<Int64> lngIdSolicitud, Nullable<Int32> IntIdTipoEstadoSol, Nullable<Int64> lngIdProcessInstance, Nullable<Int32> lngTipoTramite, Nullable<Int32> lngNumeroSilpa)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { lngIdSolicitud, IntIdTipoEstadoSol, lngIdProcessInstance, lngTipoTramite, lngNumeroSilpa };

                DbCommand cmd = db.GetStoredProcCommand("DAA_LISTA_SOLICITUD", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }

        }


        /// <summary>
        /// Retorna un Objeto con la primera solicitud encontrada con los datos insertados
        /// </summary>
        /// <param name="lngIdSolicitud">Indentificador de la solicitud</param>
        /// <param name="IntIdTipoEstadoSol">indentificador del tipo de estado de la solicud</param>
        /// <param name="lngIdProcessInstance">indentificador del instance process</param>
        /// <returns>DataSet: Conjunto de resultados de la solicictud con las siguientes columnas: 
        /// ID_SOLICITUD, SOL_ID_SECTOR, SOL_ID_TIPO_PROYECTO, SOL_ID_UBICACION, SOL_ID_AA, SOL_ID_SOLICITANTE, 
        /// SOL_ID_RADICACION, SOL_BOL_CONFLICTO, SOL_FECHA_CREACION, SOL_IDPROCESSINSTANCE, TES_ID_TIPO_ESTADO_SOLICITUD
        /// GTT_ID , SOL_NUMERO_SILPA</returns>
        public SolicitudDAAEIAIdentity ObtenerSolicitud(Nullable<Int64> lngIdSolicitud, Nullable<Int64> lngIdProcessInstance, string numeroSILPA)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { lngIdSolicitud, null, lngIdProcessInstance, null, numeroSILPA };

                DbCommand cmd = db.GetStoredProcCommand("DAA_LISTA_SOLICITUD", parametros);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        SolicitudDAAEIAIdentity objResultado = new SolicitudDAAEIAIdentity();

                        objResultado.IdSector = null;
                        objResultado.IdTipoProyecto = null;

                        objResultado.IdSolicitud = Convert.ToInt64(reader["ID_SOLICITUD"]);
                        if (reader["SOL_ID_SECTOR"] != DBNull.Value)
                            objResultado.IdSector = Convert.ToInt32(reader["SOL_ID_SECTOR"]);
                        if (reader["SOL_ID_TIPO_PROYECTO"] != DBNull.Value)
                            objResultado.IdTipoProyecto = Convert.ToInt32(reader["SOL_ID_TIPO_PROYECTO"]);
                        objResultado.IdUbicacion = Convert.ToInt32(reader["SOL_ID_UBICACION"]);
                        if (reader["SOL_ID_AA"] != DBNull.Value)
                            objResultado.IdAutoridadAmbiental = Convert.ToInt32(reader["SOL_ID_AA"]);
                        if (reader["SOL_ID_RADICACION"] != DBNull.Value)
                            objResultado.IdRadicacion = Convert.ToInt64(reader["SOL_ID_RADICACION"]);
                        objResultado.Conflicto = Convert.ToBoolean(reader["SOL_BOL_CONFLICTO"]);
                        objResultado.FechaCreacion = Convert.ToDateTime(reader["SOL_FECHA_CREACION"]);
                        objResultado.IdProcessInstance = Convert.ToInt32(reader["SOL_IDPROCESSINSTANCE"]);
                        objResultado.IdTipoEstadoSolicitud = Convert.ToInt32(reader["TES_ID_TIPO_ESTADO_SOLICITUD"]);
                        objResultado.IdSolicitante = Convert.ToInt32(reader["SOL_ID_SOLICITANTE"]);
                        objResultado.IdTipoTramite = Convert.ToInt32(reader["GTT_ID"]);
                        if (reader["SOL_NUMERO_SILPA"] != DBNull.Value)
                            objResultado.NumeroSilpa = reader["SOL_NUMERO_SILPA"].ToString();
                        objResultado.FormularioID = Convert.ToInt32(reader["ENTRYDATA"]);
                        return objResultado;
                    }
                }
                return null;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Obtener Solicitud.";
                throw new Exception(strException, ex);
            }

        }

        /// <summary>
        /// Retorna un DataSet con los datos de una o varias solicitudes segun los parametros de filtrado
        /// </summary>
        /// <param name="lngIdSolicitud">Indentificador de la solicitud</param>
        /// <param name="IntIdTipoEstadoSol">indentificador del tipo de estado de la solicud</param>
        /// <param name="lngIdProcessInstance">indentificador del instance process</param>
        /// <returns>DataSet: Conjunto de resultados de la solicictud con las siguientes columnas: 
        /// ID_SOLICITUD, SOL_ID_SECTOR, SOL_ID_TIPO_PROYECTO, SOL_ID_UBICACION, SOL_ID_AA, SOL_ID_SOLICITANTE, 
        /// SOL_ID_RADICACION, SOL_BOL_CONFLICTO, SOL_FECHA_CREACION, SOL_IDPROCESSINSTANCE, TES_ID_TIPO_ESTADO_SOLICITUD</returns>
        public SolicitudDAAEIAIdentity ObtenerSolicitud(Nullable<Int32> intIdRadicacion)
        {
            try
            {
             
                
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { intIdRadicacion };

                DbCommand cmd = db.GetStoredProcCommand("DAA_CONS_SOLICITUD_RAD", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                SolicitudDAAEIAIdentity objResultado = new SolicitudDAAEIAIdentity();

                objResultado.IdSolicitud = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["ID_SOLICITUD"]);


               // objResultado.IdSector = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_SECTOR"]);
                //objResultado.IdTipoProyecto = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_TIPO_PROYECTO"]);


                if (dsResultado.Tables[0].Rows[0]["SOL_ID_SECTOR"] != DBNull.Value)
                {
                    objResultado.IdSector = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_SECTOR"]);
                }
                else { objResultado.IdSector = null; }

                if (dsResultado.Tables[0].Rows[0]["SOL_ID_TIPO_PROYECTO"] != DBNull.Value)
                {
                    objResultado.IdTipoProyecto = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_TIPO_PROYECTO"]);
                }
                else { objResultado.IdTipoProyecto = null; }



                objResultado.IdUbicacion = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_UBICACION"]);
                objResultado.IdAutoridadAmbiental = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_AA"]);
                objResultado.IdRadicacion = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["SOL_ID_RADICACION"]);
                objResultado.Conflicto = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["SOL_BOL_CONFLICTO"]);
                objResultado.FechaCreacion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["SOL_FECHA_CREACION"]);
                objResultado.IdProcessInstance = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_IDPROCESSINSTANCE"]);
                objResultado.IdTipoEstadoSolicitud = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TES_ID_TIPO_ESTADO_SOLICITUD"]);
                objResultado.IdSolicitante = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_SOLICITANTE"]);
                objResultado.Urbano = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["SOL_URBANO"]);
                objResultado.IdTipoTramite = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["GTT_ID"]);
                objResultado.NumeroSilpa = dsResultado.Tables[0].Rows[0]["SOL_NUMERO_SILPA"].ToString();
                return objResultado;

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }

        }

        /// <summary>
        /// Retorna un DataSet con las Autoridades Ambientales a las cuales le compete un proyecto en particular
        /// </summary>
        /// <param name="objResultado.IdSolicitud">Indentificador de la solicitud</param>
        /// <param name="objResultado.IdProcessInstance">indentificador del instance process</param>
        /// <returns>DataSet: Conjunto de resultados de la solicictud con las siguientes columnas: 
        /// ID_SOLICITUD, SOL_ID_SECTOR, SOL_ID_TIPO_PROYECTO, SOL_ID_UBICACION, SOL_ID_AA, SOL_ID_SOLICITANTE, 
        /// SOL_ID_RADICACION, SOL_BOL_CONFLICTO, SOL_FECHA_CREACION, SOL_IDPROCESSINSTANCE, TES_ID_TIPO_ESTADO_SOLICITUD</returns>
        public void ObtenerAAConflicto(ref SolicitudDAAEIAIdentity objResultado)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { objResultado.IdSolicitud, null, objResultado.IdProcessInstance };

                DbCommand cmd = db.GetStoredProcCommand("DAA_LISTA_SOLICITUD", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objResultado.IdSolicitud = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["ID_SOLICITUD"]);

                //objResultado.IdSector = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_SECTOR"]);
                //objResultado.IdTipoProyecto = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_TIPO_PROYECTO"]);

                if (dsResultado.Tables[0].Rows[0]["SOL_ID_SECTOR"] != DBNull.Value)
                {
                    objResultado.IdSector = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_SECTOR"]);
                }
                else { objResultado.IdSector = null; }

                if (dsResultado.Tables[0].Rows[0]["SOL_ID_TIPO_PROYECTO"] != DBNull.Value)
                {
                    objResultado.IdTipoProyecto = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_TIPO_PROYECTO"]);
                }
                else { objResultado.IdTipoProyecto = null; }



                objResultado.IdUbicacion = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_UBICACION"]);
                objResultado.IdAutoridadAmbiental = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_AA"]);
                objResultado.IdRadicacion = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["SOL_ID_RADICACION"]);
                objResultado.Conflicto = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["SOL_BOL_CONFLICTO"]);
                objResultado.FechaCreacion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["SOL_FECHA_CREACION"]);
                objResultado.IdProcessInstance = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_IDPROCESSINSTANCE"]);
                objResultado.IdTipoEstadoSolicitud = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TES_ID_TIPO_ESTADO_SOLICITUD"]);
                objResultado.IdSolicitante = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_SOLICITANTE"]);
                objResultado.Urbano = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["SOL_URBANO"]);
                objResultado.IdTipoTramite = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["GTT_ID"]);
                objResultado.NumeroSilpa = dsResultado.Tables[0].Rows[0]["SOL_NUMERO_SILPA"].ToString();
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }

        }


        /// <summary>
        /// Retorna un DataSet con las informacion de la solicitud
        /// </summary>
        /// <param name="objResultado.IdSolicitud">Indentificador de la solicitud</param>
        /// <param name="objResultado.IdProcessInstance">indentificador del instance process</param>
        /// <returns>DataSet: Conjunto de resultados de la solicictud 
        public void ObtenerSolicitud(ref SolicitudDAAEIAIdentity objResultado)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { objResultado.IdSolicitud, objResultado.IdTipoEstadoSolicitud, objResultado.IdProcessInstance, -1, objResultado.NumeroSilpa };

                DbCommand cmd = db.GetStoredProcCommand("DAA_LISTA_SOLICITUD", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
                {

                    objResultado.IdSolicitud = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["ID_SOLICITUD"]);

                    if (dsResultado.Tables[0].Rows[0]["SOL_ID_SECTOR"] != DBNull.Value)
                    {
                        objResultado.IdSector = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_SECTOR"]);
                    }
                    else { objResultado.IdSector = null; }

                    if (dsResultado.Tables[0].Rows[0]["SOL_ID_TIPO_PROYECTO"] != DBNull.Value)
                    {
                        objResultado.IdTipoProyecto = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_TIPO_PROYECTO"]);
                    }
                    else { objResultado.IdTipoProyecto = null; }

                    objResultado.IdUbicacion = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_UBICACION"]);
                    objResultado.IdAutoridadAmbiental = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_AA"]);
                    objResultado.IdRadicacion = dsResultado.Tables[0].Rows[0]["SOL_ID_RADICACION"].ToString() != "" ? Convert.ToInt64(dsResultado.Tables[0].Rows[0]["SOL_ID_RADICACION"]) : 0;
                    objResultado.Conflicto = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["SOL_BOL_CONFLICTO"]);
                    objResultado.FechaCreacion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["SOL_FECHA_CREACION"]);
                    objResultado.IdProcessInstance = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_IDPROCESSINSTANCE"]);
                    objResultado.IdTipoEstadoSolicitud = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TES_ID_TIPO_ESTADO_SOLICITUD"]);
                    objResultado.IdSolicitante = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_ID_SOLICITANTE"]);
                    objResultado.Urbano = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["SOL_URBANO"]);
                    objResultado.IdTipoTramite = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["GTT_ID"]);
                    objResultado.NumeroSilpa = dsResultado.Tables[0].Rows[0]["SOL_NUMERO_SILPA"].ToString();
                }
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Obtener Solicitud.";
                throw new Exception(strException, ex);
            }

        }

        /// <summary>
        /// Lista los tramites que tengan valores que coincidan con los parametros de entrada
        /// </summary>
        /// <param name="blNombreVacio"></param>
        /// <param name="dtFechaIni"></param>
        /// <param name="dtFechaFin"></param>
        /// <param name="intIdTpTramite"></param>
        /// <param name="strNumeroSilpa"></param>
        /// <param name="intIdAA"></param>
        /// <param name="strNombre"></param>
        /// <param name="intIdDepartamento"></param>
        /// <param name="IdMunicipio"></param>
        /// <returns></returns>
        public DataSet ListarTramitesRPT( bool blNombreVacio, DateTime dtFechaIni, DateTime dtFechaFin, int intIdTpTramite,
            string strNumeroSilpa, int intIdAA, string strNombre, int intIdDepartamento, int IdMunicipio, string expediente,
            string hidrografia) {
                if (dtFechaFin != DateTime.MinValue)
                    dtFechaFin = new DateTime(dtFechaFin.Year, dtFechaFin.Month, dtFechaFin.Day, 23, 59, 59);
            return ListarTramitesRPT( blNombreVacio, dtFechaIni, dtFechaFin, intIdTpTramite,
            strNumeroSilpa, intIdAA, strNombre, intIdDepartamento, IdMunicipio, expediente,
            hidrografia, 0);
        }

        public DataSet ListarTramitesRPT(bool blNombreVacio, DateTime? dtFechaIni, DateTime? dtFechaFin, int intIdTpTramite,
            string strNumeroSilpa, int intIdAA, string strNombre, int intIdDepartamento, int IdMunicipio, string expediente,
            string hidrografia, int UsuarioId )
        {
            if (dtFechaFin != DateTime.MinValue)
                dtFechaFin = new DateTime(dtFechaFin.Value.Year, dtFechaFin.Value.Month, dtFechaFin.Value.Day, 23, 59, 59);
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { blNombreVacio, dtFechaIni, dtFechaFin, intIdTpTramite, 
                strNumeroSilpa, intIdAA, strNombre, intIdDepartamento, IdMunicipio, hidrografia, UsuarioId};//, expediente };
            DbCommand cmd = db.GetStoredProcCommand("RPT_CONSULTA_TODOS_TRAMITES", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado;
        }

        public DataSet ListarTramitesRPT(bool blNombreVacio, DateTime dtFechaIni, DateTime dtFechaFin, int intIdTpTramite,
            string strNumeroSilpa, int intIdAA, string strNombre, int intIdDepartamento, int IdMunicipio, string expediente,
            string sectores, string hidrografia, int UsuarioId )
        {
            if (dtFechaFin != DateTime.MinValue)
                dtFechaFin = new DateTime(dtFechaFin.Year, dtFechaFin.Month, dtFechaFin.Day, 23, 59, 59);
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { blNombreVacio, dtFechaIni, dtFechaFin, intIdTpTramite, 
                strNumeroSilpa, intIdAA, strNombre, intIdDepartamento, IdMunicipio, expediente, sectores, hidrografia, UsuarioId};
            DbCommand cmd = db.GetStoredProcCommand("RPT_CONSULTA_TODOS_TRAMITES", parametros);
            //TODO: Corregir esto mejorar tiempo de respuesta
            cmd.CommandTimeout = 2600;
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado;
        }

        public DataSet ListarTramitesRPT(bool blNombreVacio, DateTime? dtFechaIni, DateTime? dtFechaFin, int intIdTpTramite,
        string strNumeroSilpa, int intIdAA, string strNombre, int intIdDepartamento, int IdMunicipio, string expediente,
        string sectores, string hidrografia, int UsuarioId,string estadoResolucion,string estadoTramite,string idSolitante)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            if (dtFechaFin != DateTime.MinValue)
                dtFechaFin = new DateTime(dtFechaFin.Value.Year, dtFechaFin.Value.Month, dtFechaFin.Value.Day, 23, 59, 59);
            object[] parametros = new object[] { blNombreVacio, dtFechaIni, dtFechaFin, intIdTpTramite, 
                strNumeroSilpa, intIdAA, strNombre, intIdDepartamento, IdMunicipio, expediente, sectores, hidrografia, UsuarioId,estadoResolucion,estadoTramite,idSolitante};
            DbCommand cmd = db.GetStoredProcCommand("RPT_CONSULTA_TODOS_TRAMITES", parametros);
            //TODO: Corregir esto mejorar tiempo de respuesta
            cmd.CommandTimeout = 2600;
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado;
        }

        public int EstadoSolicitud(bool blConflicto)
        {
            int iEstadoProceso = 0;
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { blConflicto, iEstadoProceso };
            DbCommand cmd = db.GetStoredProcCommand("GEN_ESTADO_PROCESO_ESTANDAR", parametros);
            db.ExecuteNonQuery(cmd);
            iEstadoProceso = int.Parse(cmd.Parameters["@P_IDESTADO"].Value.ToString());
            return iEstadoProceso;
        }


        /// <summary>
        /// determina si el expediente relacionado al numero silpa es correcto
        /// </summary>
        /// <param name="idExpediente">identificador del número expediente de sila</param>
        /// <param name="numeroVital">identificador del númrero vital</param>
        /// <returns>int: </returns>
        public int VerificarExpedienteRelacionado(string idExpediente, string numeroVital) 
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numeroVital, idExpediente, 0 };
            DbCommand cmd = db.GetStoredProcCommand("OBTENER_EXPEDIENTE_RELACIONADO", parametros);
            //TODO: Corregir esto mejorar tiempo de respuesta
            cmd.CommandTimeout = 2600;
            db.ExecuteNonQuery(cmd);
            int dsResultado = (int)db.GetParameterValue(cmd,"@Existe");
            return dsResultado;
        }


        /// <summary>
        /// Obtiene el número de la Autoridad Ambiental Asociada.
        /// </summary>
        /// <param name="numeroSilpa">string: número silpa</param>
        /// <returns>int: identificador de la AA</returns>
        public int ObtenerIdAAporNumeroSilpa(string numeroSilpa) 
        {
            int result = -1;
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numeroSilpa, -1 };
            DbCommand cmd = db.GetStoredProcCommand("OBTENER_IDAA_RELACIONADO", parametros);
            //TODO: Corregir esto mejorar tiempo de respuesta
            cmd.CommandTimeout = 2600;
            db.ExecuteNonQuery(cmd);
            result = (int)db.GetParameterValue(cmd, "@ID_AA");
            return result;
        }

        
        
        /// <summary>
        /// Obtiene el número de la Autoridad Ambiental Asociada.
        /// </summary>
        /// <param name="numeroSilpa">string: número silpa</param>
        /// <returns>int: identificador de la AA</returns>
        public int ObtenerDatosAAPorNumeroSilpa(string numeroSilpa, ref string nitAA, ref string telefonoAA, ref string nombreAA)
        {
            int result = -1;
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numeroSilpa, -1, string.Empty, string.Empty, string.Empty};
            DbCommand cmd = db.GetStoredProcCommand("OBTENER_DATOS_AA_RELACIONADO", parametros);
            //TODO: Corregir esto mejorar tiempo de respuesta
            cmd.CommandTimeout = 2600;
            db.ExecuteNonQuery(cmd);
            result = (int)db.GetParameterValue(cmd, "@ID_AA");
            nitAA = db.GetParameterValue(cmd, "@NIT_AA").ToString();
            telefonoAA = db.GetParameterValue(cmd, "@TELEFONO_AA").ToString();
            nombreAA = db.GetParameterValue(cmd, "@NOMBRE_AA").ToString();

            return result;
        }
        /// <summary>
        /// hava:
        /// 24-mar-11
        /// Obtiene el nombre del tipo de trámite mediante el número silpa
        /// <param name="numeroSilpa">identificador del numero vital</param>
        /// <returns>string_ nombre del trámite asociado al número silpa</returns>
        public void ObtenerNombreTipoTramite(string numeroSilpa, out string tipoTramite, out string nombreTramite, out string nombreSolicitante) 
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numeroSilpa, string.Empty, string.Empty, string.Empty };
            DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_NOMBRE_TIPO_PROCESO", parametros);
            //TODO: Corregir esto mejorar tiempo de respuesta
            //cmd.CommandTimeout = 2600;
            db.ExecuteNonQuery(cmd);
            tipoTramite = db.GetParameterValue(cmd, "@TIPO_PROCESO").ToString();
            nombreTramite = db.GetParameterValue(cmd, "@NOMBRE_TIPO_PROCESO").ToString();
            nombreSolicitante = db.GetParameterValue(cmd, "@NOMBRE_SOLICITANTE").ToString();
        }

    }
}
