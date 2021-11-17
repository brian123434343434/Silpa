using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.SqlClient;
using SILPA.AccesoDatos.Notificacion;
using SoftManagement.Log;

namespace SILPA.AccesoDatos.Notificacion
{
    public class EstadoNotificacionDalc
    {
        private Configuracion objConfiguracion;
        
        public EstadoNotificacionDalc()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Consultar el listado de estados existentes de acuerdo a los parametros de busqueda
        /// </summary>
        /// <param name="p_strNombre">string con el nombre del estado</param>
        /// <param name="p_strDescripcion">string con la descripción del estado</param>
        /// <returns></returns>
        public List<EstadoNotificacionEntity> ObtenerEstadosNotificacion(string p_strNombre, string p_strDescripcion)
        {
            List<EstadoNotificacionEntity> objLstEstados = null;
            EstadoNotificacionEntity objEstado = null;
            DataSet objEstados = null;
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_ESTADO_NOTIFICACION");
                if (!string.IsNullOrEmpty(p_strNombre))
                    objDataBase.AddInParameter(objCommand, "@P_ESTADO", DbType.String, p_strNombre);
                if (!string.IsNullOrEmpty(p_strDescripcion))
                    objDataBase.AddInParameter(objCommand, "@P_DESCRIPCION", DbType.String, p_strDescripcion);

                //Ejecutar la consulta
                objEstados = objDataBase.ExecuteDataSet(objCommand);

                //Cargar los datos
                if (objEstados != null && objEstados.Tables.Count > 0 && objEstados.Tables[0].Rows.Count > 0)
                {
                    //Crear listado de estados
                    objLstEstados = new List<EstadoNotificacionEntity>();

                    //Ciclo que carga la información
                    foreach (DataRow objDatosEstado in objEstados.Tables[0].Rows)
                    {
                        //Crear estado
                        objEstado = new EstadoNotificacionEntity
                        {
                            ID = Convert.ToInt32(objDatosEstado["ID_ESTADO"]),
                            Estado = objDatosEstado["ESTADO"].ToString(),
                            EstadoPDI = Convert.ToBoolean(objDatosEstado["ESTADO_PDI"]),
                            Activo = Convert.ToBoolean(objDatosEstado["ESTADO_ACTIVO"]),
                            Descripcion = objDatosEstado["DESCRIPCION"].ToString()
                        };

                        //Agregar al listado
                        objLstEstados.Add(objEstado);
                    }
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "EstadoNotificacionDalc :: ObtenerEstadosNotificacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "EstadoNotificacionDalc :: ObtenerEstadosNotificacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objEstados != null)
                {
                    objEstados.Dispose();
                    objEstados = null;
                }
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if(objDataBase != null)
                    objDataBase = null;
            }

            return objLstEstados;
        }


        /// <summary>
        /// Consultar informacion de estado indicado
        /// </summary>
        /// <param name="p_intEstadoID">int con el identificador del estado</param>
        /// <returns>EstadoNotificacionEntity con la informacion del estado</returns>
        public EstadoNotificacionEntity ObtenerEstadoNotificacion(int p_intEstadoID)
        {
            EstadoNotificacionEntity objEstado = null;
            DataSet objEstados = null;
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_ESTADO_NOTIFICACION");
                objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO", DbType.Int32, p_intEstadoID);

                //Ejecutar la consulta
                objEstados = objDataBase.ExecuteDataSet(objCommand);

                //Cargar los datos
                if (objEstados != null && objEstados.Tables.Count > 0 && objEstados.Tables[0].Rows.Count > 0)
                {
                    //Crear estado
                    objEstado = new EstadoNotificacionEntity
                    {
                        ID = Convert.ToInt32(objEstados.Tables[0].Rows[0]["ID_ESTADO"]),
                        Estado = objEstados.Tables[0].Rows[0]["ESTADO"].ToString(),
                        EstadoPDI = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ESTADO_PDI"]),
                        Activo = Convert.ToBoolean(objEstados.Tables[0].Rows[0]["ESTADO_ACTIVO"]),
                        Descripcion = objEstados.Tables[0].Rows[0]["DESCRIPCION"].ToString()
                    };
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "EstadoNotificacionDalc :: ObtenerEstadoNotificacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "EstadoNotificacionDalc :: ObtenerEstadoNotificacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objEstados != null)
                {
                    objEstados.Dispose();
                    objEstados = null;
                }
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }

            return objEstado;
        }


        /// <summary>
        /// Crear un nuevo estado de notificación
        /// </summary>
        ///<param name="p_objEstadoNotificacion">EstadoNotificacionEntity con la información del estado a crear</param>
        public void CrearEstadoNotificacion(EstadoNotificacionEntity p_objEstadoNotificacion)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_INSERT_ESTADO_NOTIFICACION");
                objDataBase.AddInParameter(objCommand, "@P_ESTADO", DbType.String, p_objEstadoNotificacion.Estado);
                objDataBase.AddInParameter(objCommand, "@P_ESTADO_ACTIVO", DbType.String, p_objEstadoNotificacion.Activo);
                objDataBase.AddInParameter(objCommand, "@P_ESTADO_PDI", DbType.String, p_objEstadoNotificacion.EstadoPDI);
                objDataBase.AddInParameter(objCommand, "@P_DESCRIPCION", DbType.String, p_objEstadoNotificacion.Descripcion);

                //Crear registro
                objDataBase.ExecuteNonQuery(objCommand);

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "EstadoNotificacionDalc :: CrearEstadoNotificacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "EstadoNotificacionDalc :: CrearEstadoNotificacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
        /// Modifica un estado de notificación
        /// </summary>
        ///<param name="p_objEstadoNotificacion">EstadoNotificacionEntity con la información del estado a crear</param>
        public void ModificarEstadoNotificacion(EstadoNotificacionEntity p_objEstadoNotificacion)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_UPDATE_ESTADO_NOTIFICACION");
                objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO", DbType.String, p_objEstadoNotificacion.ID);
                objDataBase.AddInParameter(objCommand, "@P_ESTADO", DbType.String, p_objEstadoNotificacion.Estado);
                objDataBase.AddInParameter(objCommand, "@P_ESTADO_ACTIVO", DbType.String, p_objEstadoNotificacion.Activo);
                objDataBase.AddInParameter(objCommand, "@P_ESTADO_PDI", DbType.String, p_objEstadoNotificacion.EstadoPDI);
                objDataBase.AddInParameter(objCommand, "@P_DESCRIPCION", DbType.String, p_objEstadoNotificacion.Descripcion);

                //Crear registro
                objDataBase.ExecuteNonQuery(objCommand);

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "EstadoNotificacionDalc :: ModificarEstadoNotificacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "EstadoNotificacionDalc :: ModificarEstadoNotificacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
        /// Obtener el listado de estados existentes
        /// </summary>
        /// <param name="p_intUsuarioID">int con el identificador del usuario que realiza la consulta</param>
        /// <param name="p_intFlujoNotificacion">int con el identificador del flujo</param>
        /// <returns>List con la informacion de los estados</returns>
        public List<EstadoNotificacionEntity> ListarEstadosNotificacion(int p_intUsuarioID = -1, int p_intFlujoNotificacion = -1)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            EstadoNotificacionEntity estado;
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ESTADO");
            if (p_intUsuarioID > 0)
                db.AddInParameter(cmd, "@P_ID_APPLICATION_USER", DbType.Int32, p_intUsuarioID);
            if(p_intFlujoNotificacion > 0)
                db.AddInParameter(cmd, "@P_ID_FLUJO_NOT_ELEC", DbType.Int32, p_intFlujoNotificacion);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<EstadoNotificacionEntity> lista;

            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    lista = new List<EstadoNotificacionEntity>();
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        estado = new EstadoNotificacionEntity();
                        estado.ID = Convert.ToInt32(dt["ID_ESTADO"]);
                        estado.Estado = dt["ESTADO"].ToString();
                        estado.Activo = Convert.ToBoolean(dt["ESTADO_ACTIVO"]);
                        estado.Descripcion = dt["DESCRIPCION"].ToString();
                        lista.Add(estado);
                    }
                    return lista;
                }
                return null;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        public List<EstadoNotificacionEntity> ListarEstadosNotificacionPublico()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            EstadoNotificacionEntity estado;
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ESTADO_PUBLICO");
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<EstadoNotificacionEntity> lista;

            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    lista = new List<EstadoNotificacionEntity>();
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        estado = new EstadoNotificacionEntity();
                        estado.ID = Convert.ToInt32(dt["ID_ESTADO"]);
                        estado.Estado = dt["ESTADO"].ToString();
                        estado.Activo = Convert.ToBoolean(dt["ESTADO_ACTIVO"]);
                        estado.Descripcion = dt["DESCRIPCION"].ToString();

                        lista.Add(estado);
                    }
                    return lista;
                }
                return null;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        /// <summary>
        /// HAVA
        /// 27-Enero-2011
        /// </summary>
        /// <returns></returns>
        public List<EstadoNotificacionEntity> ListarEstadosNotificacionManual()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            EstadoNotificacionEntity estado;
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ESTADO_NOT_MANUAL");
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<EstadoNotificacionEntity> lista;

            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    lista = new List<EstadoNotificacionEntity>();
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        estado = new EstadoNotificacionEntity();
                        estado.ID = Convert.ToInt32(dt["ID_ESTADO"]);
                        estado.Estado = dt["ESTADO"].ToString();
                        estado.Activo = Convert.ToBoolean(dt["ESTADO_ACTIVO"]);
                        lista.Add(estado);
                    }
                    return lista;
                }
                return null;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        public EstadoNotificacionEntity ListarEstadoNotificacion(object[] parametros )
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            EstadoNotificacionEntity estado;
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ESTADO_TOTAL", parametros);
            DataSet dsResultado = new DataSet();

            try
            {
                dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    estado = new EstadoNotificacionEntity();
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        estado.ID = Convert.ToInt32(dt["ID_ESTADO"]);
                        estado.Estado = dt["ESTADO"].ToString();
                        estado.Activo =Convert.ToBoolean(dt["ESTADO_ACTIVO"].ToString());
                        return estado;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Listar Estado Notificación.";
                throw new Exception(strException, ex);
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        /// <summary>
        /// Inserta un estado nuevo en el históico de estados por acto, por persona y por estado
        /// </summary>
        /// <param name="parametros">object[]: lista de parametros</param>
        public string CrearEstadoPersonaActo(object[] parametros)
        {
            try
            {
	            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
	            DbCommand cmd = db.GetStoredProcCommand("INS_NOT_ESTADO_PERSONA_ACTO", parametros);
	            int i = db.ExecuteNonQuery(cmd);
	
	            string result = db.GetParameterValue(cmd, "@Resultado").ToString();
	            return result;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al insertar un estado nuevo en el históico de estados por acto, por persona y por estado.";
                throw new Exception(strException, ex);
            }
        }

        /// <summary>
        /// Elimina un registro asociado al identificador de la tabla
        /// </summary>
        /// <param name="id">long: identificador del registro</param>
        public void EliminarEstadoPersonaActo(long id)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros =  { id };
            DbCommand cmd = db.GetStoredProcCommand("DEL_NOT_ESTADO_PERSONA_ACTO", parametros);
            int i = db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// Obtiene un conjunto de resultado de estados notificacion por usuario
        /// </summary>
        /// <returns>List<EstadosNotificacionSelect> </returns>
        public List<EstadosNotificacionSelect> ObtenerEstadosNotificacion(object[] parametros) 
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTAR_ESTADOS_NOTIFICACION_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<EstadosNotificacionSelect> lstResult = new List<EstadosNotificacionSelect>();

            try
            {
                if (dsResultado.Tables.Count > 0) 
                {
                    foreach (DataRow dr in dsResultado.Tables[0].Rows)
                    {
                        EstadosNotificacionSelect estadoNotificacion = new EstadosNotificacionSelect(); ;

                        if (dr["ID"] != DBNull.Value) { estadoNotificacion.ID = Int64.Parse(dr["ID"].ToString()); }
                        if (dr["SOL_ID_AA"] != DBNull.Value) { estadoNotificacion.IdSolicitud = Int64.Parse(dr["SOL_ID_AA"].ToString()); }
                        if (dr["TIPO_ACTO_ADMINISTRATIVO"] != DBNull.Value) { estadoNotificacion.TipoActoAdministrativo = dr["TIPO_ACTO_ADMINISTRATIVO"].ToString(); }
                        if (dr["ID_TIPO_ACTO_ADMINISTRATIVO"] != DBNull.Value) { estadoNotificacion.IdTipoActoAdministrativo = Int32.Parse(dr["ID_TIPO_ACTO_ADMINISTRATIVO"].ToString()); }
                        if (dr["ID_PROCESO_NOTIFICACION"] != DBNull.Value) { estadoNotificacion.IdProcesoNotificacion = dr["ID_PROCESO_NOTIFICACION"].ToString(); }
                        if (dr["ID_ACTO_NOTIFICACION"] != DBNull.Value) { estadoNotificacion.IdActoNotificacion = Int64.Parse(dr["ID_ACTO_NOTIFICACION"].ToString()); }
                        if (dr["ARCHIVO"] != DBNull.Value) { estadoNotificacion.Archivo = dr["ARCHIVO"].ToString();}
                        if (dr["NOT_NUMERO_SILPA"] != DBNull.Value) { estadoNotificacion.NumeroSilpa = dr["NOT_NUMERO_SILPA"].ToString(); }
                        if (dr["EXPEDIENTE"] != DBNull.Value) { estadoNotificacion.Expediente = dr["EXPEDIENTE"].ToString(); }
                        if (dr["NOT_FECHA_ACTO"] != DBNull.Value) { estadoNotificacion.FechaActo = Convert.ToDateTime(dr["NOT_FECHA_ACTO"].ToString()); }
                        if (dr["NOT_NUMERO_ACTO_ADMINISTRATIVO"] != DBNull.Value) { estadoNotificacion.NumeroActoAdministrativo = dr["NOT_NUMERO_ACTO_ADMINISTRATIVO"].ToString(); }
                        if(dr["USUARIO_NOTIFICAR"]!=DBNull.Value){ estadoNotificacion.UsuarioNotificar = dr["USUARIO_NOTIFICAR"].ToString();}
                        if(dr["PER_CORREO_ELECTRONICO"]!=DBNull.Value){estadoNotificacion.CorreoElectronico = dr["PER_CORREO_ELECTRONICO"].ToString();}
                        if(dr["NPE_NUMERO_IDENTIFICACION"]!=DBNull.Value){ estadoNotificacion.NumeroIdentificacionUsuario =  dr["NPE_NUMERO_IDENTIFICACION"].ToString();}
                        if(dr["ESTADO_NOTIFICADO"]!=DBNull.Value){estadoNotificacion.IdEstadoNotificado= Int32.Parse(dr["ESTADO_NOTIFICADO"].ToString()); }
                        if(dr["ESTADO"]!=DBNull.Value){ estadoNotificacion.EstadoNotificado = dr["ESTADO"].ToString();}
                        if(dr["NPE_FECHA_ESTADO_NOTIFICADO"]!=DBNull.Value){estadoNotificacion.FechaEstadoNotificado = Convert.ToDateTime(dr["NPE_FECHA_ESTADO_NOTIFICADO"].ToString()); }
                        if(dr["DIAS_PARA_VENCIMIENTO"]!=DBNull.Value){ estadoNotificacion.DiasVencimiento = Int32.Parse(dr["DIAS_PARA_VENCIMIENTO"].ToString()); }
                        if (dr["NPE_ESTADO_CAMBIO_PDI"] != DBNull.Value) { estadoNotificacion.EstadoCambioPDI = Convert.ToBoolean(dr["NPE_ESTADO_CAMBIO_PDI"].ToString()); }
                        if (dr["SISTEMA"] != DBNull.Value) { estadoNotificacion.Sistema = dr["SISTEMA"].ToString(); }

                        if (dr["ID_PERSONA"] != DBNull.Value) { estadoNotificacion.IdPersonaNotificar = Convert.ToDecimal(dr["ID_PERSONA"].ToString()); }

                        if (dr["FECHA_ENVIO_CORREO"] != DBNull.Value) { estadoNotificacion.FechaEnvioCorreo = dr["FECHA_ENVIO_CORREO"].ToString(); }

                        // hava: 21-dic-2010
                        if (dr["NOMBRE_AUTORIDAD"] != DBNull.Value) { estadoNotificacion.NombreAutoridad = dr["NOMBRE_AUTORIDAD"].ToString(); }
                        if (dr["ID_AUTORIDAD"] != DBNull.Value) { estadoNotificacion.IdAutoridad = Int32.Parse(dr["ID_AUTORIDAD"].ToString()); }
                        //
                        if (dr["ACTO_ES_NOTIFICACION_ELECTRONICA"] != DBNull.Value) { estadoNotificacion.ActoEsNotificacionElectronica_EXP = bool.Parse(dr["ACTO_ES_NOTIFICACION_ELECTRONICA"].ToString()); }
                      
                        if (dr["ES_NOTIFICACION_ELECTRONICA"] != DBNull.Value) { estadoNotificacion.EsNotificacionElectronica = bool.Parse(dr["ES_NOTIFICACION_ELECTRONICA"].ToString()); }
                        if (dr["F_ES_NOTIFICACION_ELECTRONICA_AA"] != DBNull.Value) { estadoNotificacion.EsNotificacionElectronica_AA = bool.Parse(dr["F_ES_NOTIFICACION_ELECTRONICA_AA"].ToString()); }
                        if (dr["F_ES_NOTIFICACION_ELECTRONICA_EXP"] != DBNull.Value) 
                        {
                            NotExpedientesEntityDalc dalc = new NotExpedientesEntityDalc();
                            List<NotExpedientesEntity> not = dalc.ConsultarExpedientePersonaNotificar(estadoNotificacion.NumeroIdentificacionUsuario.ToString(), estadoNotificacion.Expediente, estadoNotificacion.NumeroSilpa);

                            if (not != null)
                            {
                                if (not.Count != 0)
                                {
                                    estadoNotificacion.EsNotificacionElectronica_EXP = true;
                                }
                                else
                                {
                                    estadoNotificacion.EsNotificacionElectronica_EXP = false;
                                }
                            }
                            else
                            {
                                estadoNotificacion.EsNotificacionElectronica_EXP = false;
                            }
                        }
                     
                        if (dr["TIENE_ESTADO_SIGUIENTE"] != DBNull.Value) { estadoNotificacion.TieneActividadSiguiente = Int32.Parse(dr["TIENE_ESTADO_SIGUIENTE"].ToString()); }
                        if (dr["MODIFICABLE"] != DBNull.Value) { estadoNotificacion.EsModificable = Convert.ToBoolean(dr["MODIFICABLE"]); }
                        if (dr["ESTADO_ACTUAL"] != DBNull.Value) { estadoNotificacion.EstadoActual = Convert.ToBoolean(dr["ESTADO_ACTUAL"]); }
                      
                        lstResult.Add(estadoNotificacion);
                    }
                }
                return lstResult;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        public List<EstadosNotificacionSelect> ObtenerNotificacionPublico(object[] parametros)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTAR_NOTIFICACION_PERSONA_PUBLICO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<EstadosNotificacionSelect> lstResult = new List<EstadosNotificacionSelect>();

            try
            {
                if (dsResultado.Tables.Count > 0)
                {
                    foreach (DataRow dr in dsResultado.Tables[0].Rows)
                    {
                        EstadosNotificacionSelect estadoNotificacion = new EstadosNotificacionSelect();

                        if (dr["ID"] != DBNull.Value) { estadoNotificacion.ID = Int64.Parse(dr["ID"].ToString()); }
                        if (dr["SOL_ID_AA"] != DBNull.Value) { estadoNotificacion.IdSolicitud = Int64.Parse(dr["SOL_ID_AA"].ToString()); }
                        if (dr["TIPO_ACTO_ADMINISTRATIVO"] != DBNull.Value) { estadoNotificacion.TipoActoAdministrativo = dr["TIPO_ACTO_ADMINISTRATIVO"].ToString(); }
                        if (dr["ID_TIPO_ACTO_ADMINISTRATIVO"] != DBNull.Value) { estadoNotificacion.IdTipoActoAdministrativo = Int32.Parse(dr["ID_TIPO_ACTO_ADMINISTRATIVO"].ToString()); }
                        if (dr["ID_PROCESO_NOTIFICACION"] != DBNull.Value) { estadoNotificacion.IdProcesoNotificacion = dr["ID_PROCESO_NOTIFICACION"].ToString(); }
                        if (dr["ID_ACTO_NOTIFICACION"] != DBNull.Value) { estadoNotificacion.IdActoNotificacion = Int64.Parse(dr["ID_ACTO_NOTIFICACION"].ToString()); }
                        if (dr["ARCHIVO"] != DBNull.Value) { estadoNotificacion.Archivo = dr["ARCHIVO"].ToString(); }
                        if (dr["NOT_NUMERO_SILPA"] != DBNull.Value) { estadoNotificacion.NumeroSilpa = dr["NOT_NUMERO_SILPA"].ToString(); }
                        if (dr["EXPEDIENTE"] != DBNull.Value) { estadoNotificacion.Expediente = dr["EXPEDIENTE"].ToString(); }
                        if (dr["NOT_FECHA_ACTO"] != DBNull.Value) { estadoNotificacion.FechaActo = Convert.ToDateTime(dr["NOT_FECHA_ACTO"].ToString()); }
                        if (dr["NOT_NUMERO_ACTO_ADMINISTRATIVO"] != DBNull.Value) { estadoNotificacion.NumeroActoAdministrativo = dr["NOT_NUMERO_ACTO_ADMINISTRATIVO"].ToString(); }
                        if (dr["USUARIO_NOTIFICAR"] != DBNull.Value) { estadoNotificacion.UsuarioNotificar = dr["USUARIO_NOTIFICAR"].ToString(); }
                        if (dr["PER_CORREO_ELECTRONICO"] != DBNull.Value) { estadoNotificacion.CorreoElectronico = dr["PER_CORREO_ELECTRONICO"].ToString(); }
                        if (dr["NPE_NUMERO_IDENTIFICACION"] != DBNull.Value) { estadoNotificacion.NumeroIdentificacionUsuario = dr["NPE_NUMERO_IDENTIFICACION"].ToString(); }
                        if (dr["ESTADO_NOTIFICADO"] != DBNull.Value) { estadoNotificacion.IdEstadoNotificado = Int32.Parse(dr["ESTADO_NOTIFICADO"].ToString()); }
                        if (dr["ESTADO"] != DBNull.Value) { estadoNotificacion.EstadoNotificado = dr["ESTADO"].ToString(); }
                        if (dr["NPE_FECHA_ESTADO_NOTIFICADO"] != DBNull.Value) { estadoNotificacion.FechaEstadoNotificado = Convert.ToDateTime(dr["NPE_FECHA_ESTADO_NOTIFICADO"].ToString()); }
                        if (dr["DIAS_PARA_VENCIMIENTO"] != DBNull.Value) 
                        {

                            if (Int32.Parse(dr["DIAS_PARA_VENCIMIENTO"].ToString()) > 0)
                            {
                                estadoNotificacion.DiasVencimiento = Int32.Parse(dr["DIAS_PARA_VENCIMIENTO"].ToString());
                                estadoNotificacion.ValidarDiasVencimiento = dr["DIAS_PARA_VENCIMIENTO"].ToString();
                    
                            }
                            else
                            {
                                estadoNotificacion.DiasVencimiento = 0;
                                estadoNotificacion.ValidarDiasVencimiento = null;
                            }
                        }
                      
                        if (dr["NPE_ESTADO_CAMBIO_PDI"] != DBNull.Value) { estadoNotificacion.EstadoCambioPDI = Convert.ToBoolean(dr["NPE_ESTADO_CAMBIO_PDI"].ToString()); }
                        if (dr["SISTEMA"] != DBNull.Value) { estadoNotificacion.Sistema = dr["SISTEMA"].ToString(); }

                        if (dr["ID_PERSONA"] != DBNull.Value) { estadoNotificacion.IdPersonaNotificar = Convert.ToDecimal(dr["ID_PERSONA"].ToString()); }

                        if (dr["FECHA_ENVIO_CORREO"] != DBNull.Value) { estadoNotificacion.FechaEnvioCorreo = dr["FECHA_ENVIO_CORREO"].ToString(); }

                        // hava: 21-dic-2010
                        if (dr["NOMBRE_AUTORIDAD"] != DBNull.Value) { estadoNotificacion.NombreAutoridad = dr["NOMBRE_AUTORIDAD"].ToString(); }
                        if (dr["ID_AUTORIDAD"] != DBNull.Value) { estadoNotificacion.IdAutoridad = Int32.Parse(dr["ID_AUTORIDAD"].ToString()); }
                        if (dr["TIENE_ESTADO_SIGUIENTE"]!= DBNull.Value) {estadoNotificacion.TieneActividadSiguiente = Int32.Parse(dr["TIENE_ESTADO_SIGUIENTE"].ToString());}
                        if (dr["MOSTRAR_INFO"] != DBNull.Value) { estadoNotificacion.MostrarInfomacion = Convert.ToBoolean(dr["MOSTRAR_INFO"].ToString()); }
                        if (dr["ARCHIVOS_ADJUNTOS"] != DBNull.Value) { estadoNotificacion.ArchivosAdjuntos = dr["ARCHIVOS_ADJUNTOS"].ToString(); }
                        //
                        lstResult.Add(estadoNotificacion);
                    }
                }
                return lstResult;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        public List<EstadosNotificacionSelect> ObtenerNotificacionesParaAvanzarFlujoAutomatico()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTA_NOTIFICACION_TRANSICION");
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<EstadosNotificacionSelect> lstResult = new List<EstadosNotificacionSelect>();

            try
            {
                if (dsResultado.Tables.Count > 0)
                {
                    foreach (DataRow dr in dsResultado.Tables[0].Rows)
                    {
                        EstadosNotificacionSelect estadoNotificacion = new EstadosNotificacionSelect(); ;

                        if (dr["ID"] != DBNull.Value) { estadoNotificacion.ID = Int64.Parse(dr["ID"].ToString()); }
                        if (dr["SOL_ID_AA"] != DBNull.Value) { estadoNotificacion.IdSolicitud = Int64.Parse(dr["SOL_ID_AA"].ToString()); }
                        if (dr["TIPO_ACTO_ADMINISTRATIVO"] != DBNull.Value) { estadoNotificacion.TipoActoAdministrativo = dr["TIPO_ACTO_ADMINISTRATIVO"].ToString(); }
                        if (dr["ID_TIPO_ACTO_ADMINISTRATIVO"] != DBNull.Value) { estadoNotificacion.IdTipoActoAdministrativo = Int32.Parse(dr["ID_TIPO_ACTO_ADMINISTRATIVO"].ToString()); }
                        if (dr["ID_PROCESO_NOTIFICACION"] != DBNull.Value) { estadoNotificacion.IdProcesoNotificacion = dr["ID_PROCESO_NOTIFICACION"].ToString(); }
                        if (dr["ID_ACTO_NOTIFICACION"] != DBNull.Value) { estadoNotificacion.IdActoNotificacion = Int64.Parse(dr["ID_ACTO_NOTIFICACION"].ToString()); }
                        if (dr["ARCHIVO"] != DBNull.Value) { estadoNotificacion.Archivo = dr["ARCHIVO"].ToString(); }
                        if (dr["NOT_NUMERO_SILPA"] != DBNull.Value) { estadoNotificacion.NumeroSilpa = dr["NOT_NUMERO_SILPA"].ToString(); }
                        if (dr["EXPEDIENTE"] != DBNull.Value) { estadoNotificacion.Expediente = dr["EXPEDIENTE"].ToString(); }
                        if (dr["NOT_FECHA_ACTO"] != DBNull.Value) { estadoNotificacion.FechaActo = Convert.ToDateTime(dr["NOT_FECHA_ACTO"].ToString()); }
                        if (dr["NOT_NUMERO_ACTO_ADMINISTRATIVO"] != DBNull.Value) { estadoNotificacion.NumeroActoAdministrativo = dr["NOT_NUMERO_ACTO_ADMINISTRATIVO"].ToString(); }
                        if (dr["USUARIO_NOTIFICAR"] != DBNull.Value) { estadoNotificacion.UsuarioNotificar = dr["USUARIO_NOTIFICAR"].ToString(); }
                        if (dr["PER_CORREO_ELECTRONICO"] != DBNull.Value) { estadoNotificacion.CorreoElectronico = dr["PER_CORREO_ELECTRONICO"].ToString(); }
                        if (dr["NPE_NUMERO_IDENTIFICACION"] != DBNull.Value) { estadoNotificacion.NumeroIdentificacionUsuario = dr["NPE_NUMERO_IDENTIFICACION"].ToString(); }
                        if (dr["ESTADO_NOTIFICADO"] != DBNull.Value) { estadoNotificacion.IdEstadoNotificado = Int32.Parse(dr["ESTADO_NOTIFICADO"].ToString()); }
                        if (dr["ESTADO"] != DBNull.Value) { estadoNotificacion.EstadoNotificado = dr["ESTADO"].ToString(); }
                        if (dr["NPE_FECHA_ESTADO_NOTIFICADO"] != DBNull.Value) { estadoNotificacion.FechaEstadoNotificado = Convert.ToDateTime(dr["NPE_FECHA_ESTADO_NOTIFICADO"].ToString()); }
                        if (dr["DIAS_PARA_VENCIMIENTO"] != DBNull.Value) { estadoNotificacion.DiasVencimiento = Int32.Parse(dr["DIAS_PARA_VENCIMIENTO"].ToString()); }
                        if (dr["NPE_ESTADO_CAMBIO_PDI"] != DBNull.Value) { estadoNotificacion.EstadoCambioPDI = Convert.ToBoolean(dr["NPE_ESTADO_CAMBIO_PDI"].ToString()); }
                        if (dr["SISTEMA"] != DBNull.Value) { estadoNotificacion.Sistema = dr["SISTEMA"].ToString(); }

                        if (dr["ID_PERSONA"] != DBNull.Value) { estadoNotificacion.IdPersonaNotificar = Convert.ToDecimal(dr["ID_PERSONA"].ToString()); }

                        if (dr["FECHA_ENVIO_CORREO"] != DBNull.Value) { estadoNotificacion.FechaEnvioCorreo = dr["FECHA_ENVIO_CORREO"].ToString(); }

                        // hava: 21-dic-2010
                        if (dr["NOMBRE_AUTORIDAD"] != DBNull.Value) { estadoNotificacion.NombreAutoridad = dr["NOMBRE_AUTORIDAD"].ToString(); }
                        if (dr["ID_AUTORIDAD"] != DBNull.Value) { estadoNotificacion.IdAutoridad = Int32.Parse(dr["ID_AUTORIDAD"].ToString()); }
                        if (dr["TIENE_ESTADO_SIGUIENTE"] != DBNull.Value) { estadoNotificacion.TieneActividadSiguiente = Int32.Parse(dr["TIENE_ESTADO_SIGUIENTE"].ToString()); }
                        if (dr["MOSTRAR_INFO"] != DBNull.Value) { estadoNotificacion.MostrarInfomacion = Convert.ToBoolean(dr["MOSTRAR_INFO"].ToString()); }
                        if (dr["ID_FLUJO_NOT_ELEC"] != DBNull.Value)
                        {
                            estadoNotificacion.IdFlujoNotElec = Convert.ToInt32(dr["ID_FLUJO_NOT_ELEC"].ToString());
                        }
                        else
                        {
                            estadoNotificacion.IdFlujoNotElec = 1;
                        }
                        
                        //
                        lstResult.Add(estadoNotificacion);
                    }
                }
                return lstResult;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        /// <summary>
        /// Actualiza un estado por persona por acto
        /// </summary>
        /// <param name="idActo"></param>
        /// <param name="idEstado"></param>
        /// <param name="idPersona"></param>
        public string ActualizarEstadoNotificacionPersona(object[] parametros)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("UPD_NOT_ESTADO_PERSONA_ACTO", parametros);
            int i = db.ExecuteNonQuery(cmd);

            string result = db.GetParameterValue(cmd, "@Resultado").ToString();
            return result;
        }
        /// <summary>
        /// Actualiza la fecha del estado de notificacion de una persona
        /// </summary>
        /// <param name="IdPersonaNotificar">ID Perfona notificacion</param>
        /// <param name="dtmFechaEstado">Fecha estado Notificacion</param>
        public void ModificarFechaEstadoPersonNotificacion(decimal IdPersonaNotificar, DateTime dtmFechaEstado, int estadoID)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("UPD_NOT_MODIFICAR_FECHA_ESTADO_NOTIFICACION");
            db.AddInParameter(cmd, "P_ID_PERSONA", DbType.Decimal, IdPersonaNotificar);
            db.AddInParameter(cmd, "P_NPE_FECHA_NOTIFICADO", DbType.DateTime, dtmFechaEstado);
            db.AddInParameter(cmd, "@P_ID_ESTADO", DbType.Int32, estadoID);
            db.ExecuteNonQuery(cmd);
        }


        public EstadosNotificacionSelect ObtenerEstadoPersonaActoPorId(long id) 
        {
            //SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            //object[] parametros =  { id };
            //DbCommand cmd = db.GetStoredProcCommand("OBTENER_ESTADO_PERSONA_ACTO_POR_ID", parametros);
            //int i = db.ExecuteDataSet(cmd);
            return null;
        }

        /// <summary>
        /// Hava:17-Nov-2010
        /// </summary>
        /// <param name="idPersona">long: identificador de la persona</param>
        /// <param name="idActo">long: identificador del acto</param>
        /// <returns>string: nombre del estado actual</returns>
        public string ObtenerEstadoActual(long idPersona, long idActo, out int idEstado, out DateTime? fechaEstado)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            fechaEstado = null;
            object[] parametros = { idPersona, idActo, String.Empty, 0, fechaEstado };
            DbCommand cmd = db.GetStoredProcCommand("OBTENER_ESTADO_ACTUAL", parametros);
            int i = db.ExecuteNonQuery(cmd);
            idEstado = int.Parse(db.GetParameterValue(cmd, "@ID_ESTADO").ToString());
            fechaEstado = (DateTime.Parse(db.GetParameterValue(cmd, "@Fecha_Estado_Actual").ToString()));
            return db.GetParameterValue(cmd, "@NOMBRE_ESTADO").ToString();
        }

        public List<EstadoFlujoEntity> ListaSiguienteEstadoAdm(int? estadoID, bool parausuariopublico, bool? esPublico, int IdFLujoNotElec)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            EstadoFlujoEntity estado;
            object[] parametros = { estadoID, parausuariopublico, esPublico, IdFLujoNotElec };
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTA_ESTADOS_SIGUIENTES_FLUJO_ADM", parametros);

            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<EstadoFlujoEntity> lista = new List<EstadoFlujoEntity>();

            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        estado = new EstadoFlujoEntity();
                        estado.EstadoID = Convert.ToInt32(dt["ID_ESTADO"]);
                        estado.NombreEstado = dt["ESTADO"].ToString();
                        estado.EstadoPadreID = Convert.ToInt32(dt["ID_ESTADO_PADRE"]);
                        estado.NombreEstadoPadre = dt["ESTADO_PADRE"].ToString();
                        estado.URL = dt["URL_FORMULARIO"].ToString();
                        estado.NroDiasTransicion = Convert.ToInt32(dt["NRO_DIAS_TRANSICION"]);
                        estado.EstadoEjecutoria = Convert.ToBoolean(dt["ESTADO_EJECUTORIA"]);
                        estado.EstadoNotificacion = Convert.ToBoolean(dt["ESTADO_NOTIFICACION"]);
                        estado.EstadoAnulacion = Convert.ToBoolean(dt["ESTADO_ANULACION"]);
                        estado.EstadoFinalPublicidad = Convert.ToBoolean(dt["ESTADO_FINAL_PUBLICIDAD"]);
                        estado.GeneraRecurso = Convert.ToBoolean(dt["GENERA_RECURSO"]);
                        lista.Add(estado);
                    }
                }
                return lista;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        public List<EstadoFlujoEntity> ListaSiguienteEstado(int? estadoID, bool parausuariopublico, bool? esPublico, int IdFLujoNotElec)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            EstadoFlujoEntity estado;
            object[] parametros = { estadoID, parausuariopublico, esPublico, IdFLujoNotElec};
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTA_ESTADOS_SIGUIENTES_FLUJO", parametros);

            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<EstadoFlujoEntity> lista = new List<EstadoFlujoEntity>();

            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        estado = new EstadoFlujoEntity();
                        estado.EstadoID = Convert.ToInt32(dt["ID_ESTADO"]);
                        estado.NombreEstado = dt["ESTADO"].ToString();
                        estado.EstadoPadreID = Convert.ToInt32(dt["ID_ESTADO_PADRE"]);
                        estado.NombreEstadoPadre = dt["ESTADO_PADRE"].ToString();
                        estado.URL = dt["URL_FORMULARIO"].ToString();
                        estado.NroDiasTransicion = Convert.ToInt32(dt["NRO_DIAS_TRANSICION"]);
                        lista.Add(estado);
                    }
                }
                return lista;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        public EstadoFlujoEntity ConsultaEstadoFlujo(int estadoID, int estadoPadreId)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            EstadoFlujoEntity estado = new EstadoFlujoEntity();
            object[] parametros = { estadoID, estadoPadreId};
            DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTA_ESTADO_FLUJO", parametros);
            try
            {
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        estado.EstadoID = Convert.ToInt32(reader["ID_ESTADO"]);
                        estado.NombreEstado = reader["ESTADO"].ToString();
                        estado.EstadoPadreID = Convert.ToInt32(reader["ID_ESTADO_PADRE"]);
                        estado.NombreEstadoPadre = reader["ESTADO_PADRE"].ToString();
                        estado.URL = reader["URL_FORMULARIO"].ToString();
                        estado.NroDiasTransicion = Convert.ToInt32(reader["NRO_DIAS_TRANSICION"]);
                    }
                }

                return estado;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        /// <summary>
        /// Realizar la consulta de información del reporte de notificaciones
        /// </summary>
        /// <param name="p_strNumeroVital">string con el número vital</param>
        /// <param name="p_strNumeroExpediente">string con el número de expediente</param>
        /// <param name="p_strNumeroIdentificacion">string con el número de identificación</param>
        /// <param name="p_strNombreUsuario">string con el nombre del usuario</param>
        /// <param name="p_strNumeroActoAdministrativo">string con el número de acto administrativo</param>
        /// <param name="p_intTipoActoAdministrativo">int con el tipo de acto administrativo</param>
        /// <param name="p_objFechaInicio">DateTime con la fecha de inicio de busqueda</param>
        /// <param name="p_objFechaFinal">DateTime con la fecha final de busqueda</param>
        /// <param name="p_intUsuarioID">int con el id del usuario que realiza la consulta</param>
        /// <param name="p_blnConsultarNotificaciones">bool indica si se consulta notificaciones</param>
        /// <param name="p_blnConsultarComunicaciones">bool indica si se consulta comunicaciones</param>
        /// <param name="p_blnConsultarCumplase">bool que indica si se consultan cumplase</param>
        /// <param name="p_blnConsultarPublicacion">bool que indica si se consulta publicaciones</param>
        /// <returns>DataSet con la información del reporte</returns>
        public DataSet ConsultarReporteNotificaciones(string p_strNumeroVital, string p_strNumeroExpediente,
                                                    string p_strNumeroIdentificacion, string p_strNombreUsuario,
                                                    string p_strNumeroActoAdministrativo, int p_intTipoActoAdministrativo,
                                                    DateTime p_objFechaInicio, DateTime p_objFechaFinal,
                                                    int p_intUsuarioID, bool p_blnConsultarNotificaciones,
                                                    bool p_blnConsultarComunicaciones, bool p_blnConsultarCumplase,
                                                    bool p_blnConsultarPublicacion)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objDatosReporte = null;

            try
            {
                //Cargar conexión
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar sentencia y parametros
                objCommand = objDataBase.GetStoredProcCommand("NOT_RPT_NOTIFICACIONES_ACTO_X_EXPEDIENTE");

                //Cargar los parametros
                if(!string.IsNullOrEmpty(p_strNumeroVital))
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVital);
                if(!string.IsNullOrEmpty(p_strNumeroExpediente))
                    objDataBase.AddInParameter(objCommand, "@P_EXPEDIENTE", DbType.String, p_strNumeroExpediente);
                if(!string.IsNullOrEmpty(p_strNumeroIdentificacion))
                    objDataBase.AddInParameter(objCommand, "@P_IDENTIFICACION_USUARIO", DbType.String, p_strNumeroIdentificacion);
                if(!string.IsNullOrEmpty(p_strNombreUsuario))
                    objDataBase.AddInParameter(objCommand, "@P_USUARIO_NOTIFICAR", DbType.String, p_strNombreUsuario);
                if(!string.IsNullOrEmpty(p_strNumeroActoAdministrativo))
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_ACTO_ADMINISTRATIVO", DbType.String, p_strNumeroActoAdministrativo);
                if(p_intTipoActoAdministrativo > 0)
                    objDataBase.AddInParameter(objCommand, "@P_TIPO_ACTO_ADMINISTRATIVO", DbType.Int32, p_intTipoActoAdministrativo);
                objDataBase.AddInParameter(objCommand, "@P_FECHA_DESDE", DbType.DateTime, p_objFechaInicio);
                objDataBase.AddInParameter(objCommand, "@P_FECHA_HASTA", DbType.DateTime, p_objFechaFinal);                
                if(p_blnConsultarNotificaciones)
                    objDataBase.AddInParameter(objCommand, "@P_NOTIFICACIONES", DbType.Boolean, p_blnConsultarNotificaciones);
                if(p_blnConsultarComunicaciones)
                    objDataBase.AddInParameter(objCommand, "@P_COMUNICACION", DbType.Boolean, p_blnConsultarComunicaciones);
                if(p_blnConsultarCumplase)
                    objDataBase.AddInParameter(objCommand, "@P_CUMPLASE", DbType.Boolean, p_blnConsultarCumplase);
                if(p_blnConsultarPublicacion)
                    objDataBase.AddInParameter(objCommand, "@P_PUBLICACION", DbType.Boolean, p_blnConsultarPublicacion);
                objDataBase.AddInParameter(objCommand, "@P_ID_APPLICATION_USER", DbType.Int32, p_intUsuarioID);

                //Realizar la consulta
                objDatosReporte = objDataBase.ExecuteDataSet(objCommand);

                //Dar nombre a las tablas
                if (objDatosReporte != null && objDatosReporte.Tables != null && objDatosReporte.Tables.Count > 0)
                {
                    objDatosReporte.Tables[0].TableName = "TablaExpedientes";
                    objDatosReporte.Tables[1].TableName = "TablaDetalleExpedientes";
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                objCommand.Dispose();
                objCommand = null;
                objDataBase = null;
            }

            return objDatosReporte;
        }


        /// <summary>
        /// Consultar la información del acto administrativo a notificar
        /// </summary>
        /// <param name="p_lngActoAdministrativoID">long con el identificador del acto administrativo</param>
        /// <returns>DataTable con la información de la notificación</returns>
        public DataTable ConsultaActoNotificacion(long p_lngActoAdministrativoID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objDatosActo = null;
            DataTable objActo = null;

            try
            {
                //Cargar conexión
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar sentencia y parametros
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_ACTO_ADMINISTRATIVO_NOTIFICACION");

                //Cargar los parametros
                objDataBase.AddInParameter(objCommand, "@P_ID_ACTO_NOTIFICACION", DbType.String, p_lngActoAdministrativoID);

                //Realizar la consulta
                objDatosActo = objDataBase.ExecuteDataSet(objCommand);

                if (objDatosActo != null && objDatosActo.Tables.Count > 0 && objDatosActo.Tables[0].Rows.Count > 0)
                {
                    objActo = objDatosActo.Tables[0];
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                objCommand.Dispose();
                objCommand = null;
                objDataBase = null;
            }

            return objActo;
        }

        /// <summary>
        /// Realizar la consulta de información del reporte de notificaciones
        /// </summary>
        /// <param name="p_strNumeroVital">string con el número vital</param>
        /// <param name="p_strNumeroExpediente">string con el número de expediente</param>
        /// <param name="p_strNumeroIdentificacion">string con el número de identificación</param>
        /// <param name="p_strNombreUsuario">string con el nombre del usuario</param>
        /// <param name="p_strNumeroActoAdministrativo">string con el número de acto administrativo</param>
        /// <param name="p_intTipoActoAdministrativo">int con el tipo de acto administrativo</param>
        /// <param name="p_objFechaInicio">DateTime con la fecha de inicio de busqueda</param>
        /// <param name="p_objFechaFinal">DateTime con la fecha final de busqueda</param>
        /// <param name="p_intAutoridadAmbiental">int con el id de la autoridad ambiental</param>
        /// <returns>DataSet con la información del reporte</returns>
        public DataSet ConsultaActoNostificacion(string p_strNumeroVital, string p_strNumeroExpediente,
                                                    string p_strNumeroIdentificacion, string p_strNombreUsuario,
                                                    string p_strNumeroActoAdministrativo, int p_intTipoActoAdministrativo,
                                                    DateTime p_objFechaInicio, DateTime p_objFechaFinal,
                                                    int p_intAutoridadAmbiental)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objDatosReporte = null;

            try
            {
                //Cargar conexión
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar sentencia y parametros
                objCommand = objDataBase.GetStoredProcCommand("NOT_NOTIFICACIONES_ACTO");

                //Cargar los parametros
                if (!string.IsNullOrEmpty(p_strNumeroVital))
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVital);
                if (!string.IsNullOrEmpty(p_strNumeroExpediente))
                    objDataBase.AddInParameter(objCommand, "@P_EXPEDIENTE", DbType.String, p_strNumeroExpediente);
                if (!string.IsNullOrEmpty(p_strNumeroIdentificacion))
                    objDataBase.AddInParameter(objCommand, "@P_IDENTIFICACION_USUARIO", DbType.String, p_strNumeroIdentificacion);
                if (!string.IsNullOrEmpty(p_strNombreUsuario))
                    objDataBase.AddInParameter(objCommand, "@P_USUARIO_NOTIFICAR", DbType.String, p_strNombreUsuario);
                if (!string.IsNullOrEmpty(p_strNumeroActoAdministrativo))
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_ACTO_ADMINISTRATIVO", DbType.String, p_strNumeroActoAdministrativo);
                if (p_intTipoActoAdministrativo > 0)
                    objDataBase.AddInParameter(objCommand, "@P_TIPO_ACTO_ADMINISTRATIVO", DbType.Int32, p_intTipoActoAdministrativo);
                objDataBase.AddInParameter(objCommand, "@P_FECHA_DESDE", DbType.DateTime, p_objFechaInicio);
                objDataBase.AddInParameter(objCommand, "@P_FECHA_HASTA", DbType.DateTime, p_objFechaFinal);
                objDataBase.AddInParameter(objCommand, "@P_ID_AUTORIDAD_AMBIENTAL", DbType.Int32, p_intAutoridadAmbiental);
                
                //Realizar la consulta
                objDatosReporte = objDataBase.ExecuteDataSet(objCommand);

                //Dar nombre a las tablas
                if (objDatosReporte != null && objDatosReporte.Tables != null && objDatosReporte.Tables.Count > 0)
                {
                    objDatosReporte.Tables[0].TableName = "TablaActos";
                    objDatosReporte.Tables[1].TableName = "TablaActosDetalles";
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                objCommand.Dispose();
                objCommand = null;
                objDataBase = null;
            }

            return objDatosReporte;
        }


        /// <summary>
        /// Consultar la información registrada en bitacora sobre cambios de estados
        /// </summary>
        /// <param name="p_lngActoNotificacionId">long con el id de notificación</param>
        /// <returns>DataSet con la información de la bitacora</returns>
        public DataSet ConsultarBitacoraEstadosActoAdministrativo(long p_lngActoNotificacionId)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objDatosBitacora = null;

            try
            {
                //Cargar conexión
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar sentencia y parametros
                objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_BITACORA_ESTADO_ACTO");

                //Cargar los parametros
                objDataBase.AddInParameter(objCommand, "@P_ID_ACTO_NOTIFICACION", DbType.Int64, p_lngActoNotificacionId);
                
                //Realizar la consulta
                objDatosBitacora = objDataBase.ExecuteDataSet(objCommand);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                objCommand.Dispose();
                objCommand = null;
                objDataBase = null;
            }

            return objDatosBitacora;
        }
        

        /// <summary>
        /// Consultar el reporte detallado de notificaciones
        /// </summary>
        /// <param name="p_lngActoNotificacionId">long con el id de notificación</param>
        /// <param name="p_intTipoNotificacion">int con el tipo de notificación</param>
        /// <returns>DataSet con la información detallada de notificaciones</returns>
        public DataSet ConsultarReporteDetalleNotificaciones(long p_lngActoNotificacionId, int p_intTipoNotificacion)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objDatosReporte = null;

            try
            {
                //Cargar conexión
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar sentencia y parametros
                objCommand = objDataBase.GetStoredProcCommand("NOT_RPT_DETALLE_NOTIFICACIONES_ACTO_X_EXPEDIENTE");

                //Cargar los parametros
                objDataBase.AddInParameter(objCommand, "@P_ID_ACTO_NOTIFICACION", DbType.Int64, p_lngActoNotificacionId);
                objDataBase.AddInParameter(objCommand, "@P_ID_TIPO_NOTIFICACION", DbType.Int32, p_intTipoNotificacion);
                
                //Realizar la consulta
                objDatosReporte = objDataBase.ExecuteDataSet(objCommand);

                //Dar nombre a las tablas
                if (objDatosReporte != null && objDatosReporte.Tables != null && objDatosReporte.Tables.Count > 0)
                {
                    objDatosReporte.Tables[0].TableName = "TablaNotificaciones";
                    objDatosReporte.Tables[1].TableName = "TablaDetalleNotificaciones";
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                objCommand.Dispose();
                objCommand = null;
                objDataBase = null;
            }

            return objDatosReporte;
        }


        /// <summary>
        /// Crea un nuevo estado para la persona para un acto administrativo especifico
        /// </summary>
        /// <param name="p_lngIdActo">long con el identificador del acto</param>
        /// <param name="p_intFlujoID">int con el identificador del flujo</param>
        /// <param name="p_intIdEstadoNuevo">int con el identificador del estado nuevo</param>
        /// <param name="p_intIdPersona">int con el identificador de la persona</param>
        /// <param name="p_objFechaEstadoNuevo">DatTime con la fecha del nuevo estado</param>
        /// <param name="p_strObservacion">string con la observación realizad al estado</param>
        /// <param name="p_strRutaDocumentoAdicional">string con la ruta del documento adicional</param>
        /// <param name="p_blnEnviaDireccion">bool que indica si se especifico dirección de envío</param>
        /// <param name="p_blnEnviaCorreo">bool que indica si se envía correo electrónico</param>
        /// <param name="p_strRutaAdjunto">string con la ruta del documento adjunto</param>
        /// <param name="p_blAdjuntoIncluyeActo">bool que indica si los adjuntos incluyen actos administrativos</param>
        /// <param name="p_blAdjuntoIncluyeConceptosActo">bool que indica si los adjuntos incluyen los conceptos de los actos administrativos</param>
        /// <param name="p_strNumeroRadicado">string con el número de radicado SIGPRO</param>
        /// <param name="p_strRutaDocumentoPlantilla">string con la ruta donde se almaceno la plantilla</param>
        /// <param name="p_strReferenciaRecepcion">string con la referencia de recepción del documento</param>
        /// <param name="p_objFechaRecepcion">DateTime con la fecha en la cual se recepciono el documento</param>
        /// <param name="p_intIdUsuario">int con el identificador del usuario que realizo la operación</param>
        /// <param name="p_blnEsModificable">bool que indica si el estado es modificable</param>
        /// <param name="p_intTipoIdentficacionPersonaNotificar">int con el tipo de identificación persona notificada</param>
        /// <param name="p_strNumeroIdentificacionPersonaNotificar">string con el número de identificacion persona notificada</param>
        /// <param name="p_strNombrePersonaNotificar">string con el nombre de la persona notificada</param>
        /// <param name="p_strCalidadPersonaNotificar">string con la calidad de la persona notificada</param>
        /// <returns>long con el identificador del estado creado</returns>
        public long CrearEstadoPersonaActo(long p_lngIdActo, int p_intFlujoID, int p_intIdEstadoNuevo, long p_intIdPersona, DateTime p_objFechaEstadoNuevo, string p_strObservacion, string p_strRutaDocumentoAdicional,
                                           bool p_blnEnviaDireccion, bool p_blnEnviaCorreo, string p_strRutaAdjunto, bool p_blAdjuntoIncluyeActo, bool p_blAdjuntoIncluyeConceptosActo,
                                           string p_strNumeroRadicado, string p_strRutaDocumentoPlantilla, string p_strReferenciaRecepcion, DateTime p_objFechaRecepcion,
                                           int p_intIdUsuario, bool p_blnEsModificable, int p_intTipoIdentficacionPersonaNotificar, string p_strNumeroIdentificacionPersonaNotificar, string p_strNombrePersonaNotificar, string p_strCalidadPersonaNotificar)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            long lngEstadoPersonaActoID = 0;

            try
            {
                //Cargar conexión
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar sentencia y parametros
                objCommand = objDataBase.GetStoredProcCommand("NOT_INSERTAR_NOT_ESTADO_PERSONA_ACTO");

                //Cargar los parametros
                objDataBase.AddInParameter(objCommand, "@P_ID_NOT_ACTO", DbType.Int64, p_lngIdActo);
                objDataBase.AddInParameter(objCommand, "@P_ID_FLUJO_NOT_ELEC", DbType.Int32, p_intFlujoID);
                objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO", DbType.Int32, p_intIdEstadoNuevo);
                objDataBase.AddInParameter(objCommand, "@P_ID_PERSONA", DbType.Int32, p_intIdPersona);
                objDataBase.AddInParameter(objCommand, "@P_FECHA_ESTADO", DbType.DateTime, p_objFechaEstadoNuevo);
                if (!string.IsNullOrEmpty(p_strObservacion))
                    objDataBase.AddInParameter(objCommand, "@P_OBSERVACION", DbType.String, p_strObservacion);
                if (!string.IsNullOrEmpty(p_strRutaDocumentoAdicional))
                    objDataBase.AddInParameter(objCommand, "@P_RUTA_DOCUMENTO_ADICIONAL", DbType.String, p_strRutaDocumentoAdicional);                
                objDataBase.AddInParameter(objCommand, "@P_ENVIA_DIRECCION", DbType.Boolean, p_blnEnviaDireccion);
                objDataBase.AddInParameter(objCommand, "@P_ENVIA_CORREO", DbType.Int32, (p_blnEnviaCorreo ? 1 : 0));
                if (p_blnEnviaCorreo)
                {
                    objDataBase.AddInParameter(objCommand, "@P_ADJUNTO_INCLUYE_ACTO", DbType.Boolean, p_blAdjuntoIncluyeActo);
                    objDataBase.AddInParameter(objCommand, "@P_ADJUNTO_INCLUYE_CONCEPTOS_ACTO", DbType.Boolean, p_blAdjuntoIncluyeConceptosActo);                    
                    if (!string.IsNullOrEmpty(p_strRutaAdjunto))
                        objDataBase.AddInParameter(objCommand, "@P_RUTA_DOCUMENTO", DbType.String, p_strRutaAdjunto);
                }
                if (!string.IsNullOrEmpty(p_strNumeroRadicado))
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_RADICADO", DbType.String, p_strNumeroRadicado);
                if (!string.IsNullOrEmpty(p_strRutaDocumentoPlantilla))
                    objDataBase.AddInParameter(objCommand, "@P_RUTA_DOCUMENTO_PLANTILLA", DbType.String, p_strRutaDocumentoPlantilla);
                if (!string.IsNullOrEmpty(p_strReferenciaRecepcion))
                {
                    objDataBase.AddInParameter(objCommand, "@P_REFERENCIA", DbType.String, p_strReferenciaRecepcion);                    
                }
                if (p_objFechaRecepcion != default(DateTime))
                {
                    objDataBase.AddInParameter(objCommand, "@P_FECHA_REFERENCIA", DbType.DateTime, p_objFechaRecepcion);
                }
                if (p_intTipoIdentficacionPersonaNotificar > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_TIPO_IDENTIFICACION_PERSONA_NOTIFICADA", DbType.Int32, p_intTipoIdentficacionPersonaNotificar);
                if (!string.IsNullOrEmpty(p_strNumeroIdentificacionPersonaNotificar))
                    objDataBase.AddInParameter(objCommand, "@P_IDENTIFICACION_PERSONA_NOTIFICADA", DbType.String, p_strNumeroIdentificacionPersonaNotificar);
                if (!string.IsNullOrEmpty(p_strNombrePersonaNotificar))
                    objDataBase.AddInParameter(objCommand, "@P_NOMBRE_PERSONA_NOTIFICADA", DbType.String, p_strNombrePersonaNotificar);
                if (!string.IsNullOrEmpty(p_strCalidadPersonaNotificar))
                    objDataBase.AddInParameter(objCommand, "@P_CALIDAD_PERSONA_NOTIFICADA", DbType.String, p_strCalidadPersonaNotificar);
                objDataBase.AddInParameter(objCommand, "@P_APPLICATION_USER_ID", DbType.Int32, p_intIdUsuario);
                objDataBase.AddInParameter(objCommand, "@P_MODIFICABLE", DbType.Boolean, p_blnEsModificable);                

                //Ejecuta sentencia
                using (IDataReader reader = objDataBase.ExecuteReader(objCommand))
                {
                    //Cargar id del certificado
                    if (reader.Read())
                    {
                        lngEstadoPersonaActoID = Convert.ToInt64(reader["ID_ESTADO_PERSONA_ACTO"]);
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                objCommand.Dispose();
                objCommand = null;
                objDataBase = null;
            }

            return lngEstadoPersonaActoID;
        }


        /// <summary>
        /// Crea un nuevo estado para la persona para un acto administrativo especifico
        /// </summary>
        /// <param name="p_lngEstadoPersonaActoID">long con el identificador del estado de persona a editar</param>
        /// <param name="p_objFechaEstadoNuevo">DatTime con la fecha del nuevo estado</param>
        /// <param name="p_strObservacion">string con la observación realizad al estado</param>
        /// <param name="p_strRutaDocumentoAdicional">string con la ruta del documento adicional</param>
        /// <param name="p_blnEnviaDireccion">bool que indica si se especifico dirección de envío</param>
        /// <param name="p_strDepartamentoDireccion">string con el departamento al cual se envío la notificación</param>
        /// <param name="p_strMunicipioDireccion">string con el municipio al cual se envío la dirección</param>
        /// <param name="p_strDireccion">string con la dirección de notificación</param>
        /// <param name="p_blnEnviaCorreo">bool que indica si se envía correo electrónico</param>
        /// <param name="p_strCorreo">string con la dirección de correo</param>
        /// <param name="p_strTextoCorreo">string con el texto del correo</param>
        /// <param name="p_strRutaAdjunto">string con la ruta del documento adjunto</param>
        /// <param name="p_blAdjuntoIncluyeActo">bool que indica si los adjuntos incluyen actos administrativos</param>
        /// <param name="p_blAdjuntoIncluyeConceptosActo">bool que indica si los adjuntos incluyen los conceptos de los actos administrativos</param>
        /// <param name="p_strNumeroRadicado">string con el número de radicado SIGPRO</param>
        /// <param name="p_strRutaDocumentoPlantilla">string con la ruta donde se almaceno la plantilla</param>
        /// <param name="p_strReferenciaRecepcion">string con la referencia de recepción del documento</param>
        /// <param name="p_objFechaRecepcion">DateTime con la fecha en la cual se recepciono el documento</param>
        /// <param name="p_intIdUsuario">int con el identificador del usuario que realizo la operación</param>
        /// <param name="p_blnEsModificable">bool que indica si el estado es modificable</param>
        /// <param name="p_intTipoIdentficacionPersonaNotificar">int con el tipo de identificación persona notificada</param>
        /// <param name="p_strNumeroIdentificacionPersonaNotificar">string con el número de identificacion persona notificada</param>
        /// <param name="p_strNombrePersonaNotificar">string con el nombre de la persona notificada</param>
        /// <param name="p_strCalidadPersonaNotificar">string con la calidad de la persona notificada</param>
        public void EditarEstadoPersonaActo(long p_lngEstadoPersonaActoID, DateTime p_objFechaEstado, string p_strObservacion, string p_strRutaDocumentoAdicional,
                                            bool p_blnEnviaDireccion, bool p_blnEnviaCorreo, string p_strRutaAdjunto, bool p_blAdjuntoIncluyeActo, bool p_blAdjuntoIncluyeConceptosActo,
                                            string p_strNumeroRadicado, string p_strRutaDocumentoPlantilla, string p_strReferenciaRecepcion, DateTime p_objFechaRecepcion,
                                            int p_intIdUsuario, int p_intTipoIdentficacionPersonaNotificar, string p_strNumeroIdentificacionPersonaNotificar, string p_strNombrePersonaNotificar, string p_strCalidadPersonaNotificar)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;

            try
            {
                //Cargar conexión
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar sentencia y parametros
                objCommand = objDataBase.GetStoredProcCommand("NOT_ACTUALIZAR_NOT_ESTADO_PERSONA_ACTO");

                //Cargar los parametros
                objDataBase.AddInParameter(objCommand, "@P_ID", DbType.Int64, p_lngEstadoPersonaActoID);
                objDataBase.AddInParameter(objCommand, "@P_FECHA_ESTADO", DbType.DateTime, p_objFechaEstado);
                if (!string.IsNullOrEmpty(p_strObservacion))
                    objDataBase.AddInParameter(objCommand, "@P_OBSERVACION", DbType.String, p_strObservacion);
                if (!string.IsNullOrEmpty(p_strRutaDocumentoAdicional))
                    objDataBase.AddInParameter(objCommand, "@P_RUTA_DOCUMENTO_ADICIONAL", DbType.String, p_strRutaDocumentoAdicional);
                objDataBase.AddInParameter(objCommand, "@P_ENVIA_DIRECCION", DbType.Boolean, p_blnEnviaDireccion);                
                objDataBase.AddInParameter(objCommand, "@P_ENVIA_CORREO", DbType.Int32, (p_blnEnviaCorreo ? 1 : 0));
                if (p_blnEnviaCorreo)
                {
                    objDataBase.AddInParameter(objCommand, "@P_ADJUNTO_INCLUYE_ACTO", DbType.Boolean, p_blAdjuntoIncluyeActo);
                    objDataBase.AddInParameter(objCommand, "@P_ADJUNTO_INCLUYE_CONCEPTOS_ACTO", DbType.Boolean, p_blAdjuntoIncluyeConceptosActo);
                    if (!string.IsNullOrEmpty(p_strRutaAdjunto))
                        objDataBase.AddInParameter(objCommand, "@P_RUTA_DOCUMENTO", DbType.String, p_strRutaAdjunto);                    
                }
                if (!string.IsNullOrEmpty(p_strRutaDocumentoPlantilla))
                    objDataBase.AddInParameter(objCommand, "@P_RUTA_DOCUMENTO_PLANTILLA", DbType.String, p_strRutaDocumentoPlantilla);
                if (!string.IsNullOrEmpty(p_strNumeroRadicado))
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_RADICADO", DbType.String, p_strNumeroRadicado);
                if (!string.IsNullOrEmpty(p_strReferenciaRecepcion))
                {
                    objDataBase.AddInParameter(objCommand, "@P_REFERENCIA", DbType.String, p_strReferenciaRecepcion);
                    objDataBase.AddInParameter(objCommand, "@P_FECHA_REFERENCIA", DbType.DateTime, p_objFechaRecepcion);
                }
                if (p_intTipoIdentficacionPersonaNotificar > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_TIPO_IDENTIFICACION_PERSONA_NOTIFICADA", DbType.Int32, p_intTipoIdentficacionPersonaNotificar);
                if (!string.IsNullOrEmpty(p_strNumeroIdentificacionPersonaNotificar))
                    objDataBase.AddInParameter(objCommand, "@P_IDENTIFICACION_PERSONA_NOTIFICADA", DbType.String, p_strNumeroIdentificacionPersonaNotificar);
                if (!string.IsNullOrEmpty(p_strNombrePersonaNotificar))
                    objDataBase.AddInParameter(objCommand, "@P_NOMBRE_PERSONA_NOTIFICADA", DbType.String, p_strNombrePersonaNotificar);
                if (!string.IsNullOrEmpty(p_strCalidadPersonaNotificar))
                    objDataBase.AddInParameter(objCommand, "@P_CALIDAD_PERSONA_NOTIFICADA", DbType.String, p_strCalidadPersonaNotificar);
                objDataBase.AddInParameter(objCommand, "@P_APPLICATION_USER_ID", DbType.Int32, p_intIdUsuario);
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                objCommand.Dispose();
                objCommand = null;
                objDataBase = null;
            }
        }


        /// <summary>
        /// Crear un nuevo correo relacionado a un estado
        /// </summary>
        /// <param name="p_objCorreo">CorreoNotificacionEntity con la información del correo</param>
        /// <param name="p_strTextoCorreo">string con el texto del correo</param>
        public void CrearCorreoEstadoPersonaActo(CorreoNotificacionEntity p_objCorreo, string p_strTextoCorreo)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;

            try
            {
                //Cargar conexión
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar sentencia y parametros
                objCommand = objDataBase.GetStoredProcCommand("NOT_INSERTAR_CORREO_ESTADO_PERSONA_ACTO");

                //Cargar los parametros
                objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_PERSONA_ACTO", DbType.Int64, p_objCorreo.EstadoPersonaActoID);
                objDataBase.AddInParameter(objCommand, "@P_DIRECCION_CORREO", DbType.String, p_objCorreo.Correo);
                objDataBase.AddInParameter(objCommand, "@P_TEXTO_CORREO", DbType.String, p_strTextoCorreo);
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                objCommand.Dispose();
                objCommand = null;
                objDataBase = null;
            }
        }


        /// <summary>
        /// Consultar los correos pertenecientes a un estado especifico
        /// </summary>
        /// <param name="p_lngEstadoPersonaActoID">long con el identificador del estado</param>
        /// <returns>List con la informacion de los correos</returns>
        public List<CorreoNotificacionEntity> ConsultarCorreosEstadoPersonaActo(long p_lngEstadoPersonaActoID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objDatosCorreos = null;
            List<CorreoNotificacionEntity> objLstCorreos = null;
            CorreoNotificacionEntity objCorreo = null;

            try
            {
                //Cargar conexión
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar sentencia y parametros
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTA_CORREO_ESTADO_PERSONA_ACTO");
                objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_PERSONA_ACTO", DbType.Int64, p_lngEstadoPersonaActoID);
                objDatosCorreos = objDataBase.ExecuteDataSet(objCommand);

                //Cargar datos
                if (objDatosCorreos != null && objDatosCorreos.Tables.Count > 0 && objDatosCorreos.Tables[0].Rows.Count > 0)
                {
                    //Crear listado
                    objLstCorreos = new List<CorreoNotificacionEntity>();

                    //Ciclo que carga datos
                    foreach (DataRow objInformacionCorreo in objDatosCorreos.Tables[0].Rows)
                    {
                        objCorreo = new CorreoNotificacionEntity
                        {
                            EstadoPersonaActoID = p_lngEstadoPersonaActoID,
                            Correo = objInformacionCorreo["DIRECCION_CORREO"].ToString(),
                            Texto = objInformacionCorreo["TEXTO_CORREO"].ToString(),
                            FechaEnvío = Convert.ToDateTime(objInformacionCorreo["FECHA_ENVÍO"])
                        };

                        //Adicionar al listado
                        objLstCorreos.Add(objCorreo);
                    }
                }


            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                objCommand.Dispose();
                objCommand = null;
                objDataBase = null;
            }

            return objLstCorreos;
        }


        /// <summary>
        /// Eliminar los correos de un estado
        /// </summary>
        /// <param name="p_lngEstadoPersonaActoID">long con la identificacion del estado</param>
        public void EliminarCorreosEstadoPersonaActo(long p_lngEstadoPersonaActoID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;

            try
            {
                //Cargar conexión
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar sentencia y parametros
                objCommand = objDataBase.GetStoredProcCommand("NOT_ELIMINAR_CORREOS_ESTADO_PERSONA_ACTO");

                //Cargar los parametros
                objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_PERSONA_ACTO", DbType.Int64, p_lngEstadoPersonaActoID);
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                objCommand.Dispose();
                objCommand = null;
                objDataBase = null;
            }
        }


        /// <summary>
        /// Crear una nueva dirección relacionada a un estado
        /// </summary>
        /// <param name="p_objDireccion">DireccionNotificacionEntity con la información de la dirección</param>        
        public void CrearDireccionEstadoPersonaActo(DireccionNotificacionEntity p_objDireccion)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;

            try
            {
                //Cargar conexión
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar sentencia y parametros
                objCommand = objDataBase.GetStoredProcCommand("NOT_INSERTAR_DIRECCION_ESTADO_PERSONA_ACTO");

                //Cargar los parametros
                objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_PERSONA_ACTO", DbType.Int64, p_objDireccion.EstadoPersonaActoID);
                objDataBase.AddInParameter(objCommand, "@P_DEPARTAMENTO", DbType.String, p_objDireccion.Departamento);
                objDataBase.AddInParameter(objCommand, "@P_MUNICIPIO", DbType.String, p_objDireccion.Municipio);
                objDataBase.AddInParameter(objCommand, "@P_DIRECCION", DbType.String, p_objDireccion.Direccion);
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                objCommand.Dispose();
                objCommand = null;
                objDataBase = null;
            }
        }


        /// <summary>
        /// Consultar las direcciones pertnecientes a un estado especifico
        /// </summary>
        /// <param name="p_lngEstadoPersonaActoID">long con el identificador del estado</param>
        /// <returns>List con la informacion de las direcciones</returns>
        public List<DireccionNotificacionEntity> ConsultarDireccionesEstadoPersonaActo(long p_lngEstadoPersonaActoID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objDatosDirecciones = null;
            List<DireccionNotificacionEntity> objLstDirecciones = null;
            DireccionNotificacionEntity objDireccion = null;

            try
            {
                //Cargar conexión
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar sentencia y parametros
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTA_DIRECCION_ESTADO_PERSONA_ACTO");
                objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_PERSONA_ACTO", DbType.Int64, p_lngEstadoPersonaActoID);
                objDatosDirecciones = objDataBase.ExecuteDataSet(objCommand);

                //Cargar datos
                if (objDatosDirecciones != null && objDatosDirecciones.Tables.Count > 0 && objDatosDirecciones.Tables[0].Rows.Count > 0)
                {
                    //Crear listado
                    objLstDirecciones = new List<DireccionNotificacionEntity>();

                    //Ciclo que carga datos
                    foreach (DataRow objInformacionDireccion in objDatosDirecciones.Tables[0].Rows)
                    {
                        objDireccion = new DireccionNotificacionEntity
                        {
                            EstadoPersonaActoID = p_lngEstadoPersonaActoID,
                            Departamento = objInformacionDireccion["DEPARTAMENTO"].ToString(),
                            Municipio = objInformacionDireccion["MUNICIPIO"].ToString(),
                            Direccion = objInformacionDireccion["DIRECCION"].ToString()
                        };

                        //Adicionar al listado
                        objLstDirecciones.Add(objDireccion);
                    }
                }


            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                objCommand.Dispose();
                objCommand = null;
                objDataBase = null;
            }

            return objLstDirecciones;
        }


        /// <summary>
        /// Eliminar las direcciones de un estado
        /// </summary>
        /// <param name="p_lngEstadoPersonaActoID">long con la identificacion del estado</param>
        public void EliminarDireccionesEstadoPersonaActo(long p_lngEstadoPersonaActoID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;

            try
            {
                //Cargar conexión
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar sentencia y parametros
                objCommand = objDataBase.GetStoredProcCommand("NOT_ELIMINAR_DIRECCIONES_ESTADO_PERSONA_ACTO");

                //Cargar los parametros
                objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_PERSONA_ACTO", DbType.Int64, p_lngEstadoPersonaActoID);
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                objCommand.Dispose();
                objCommand = null;
                objDataBase = null;
            }
        }

    }

}
