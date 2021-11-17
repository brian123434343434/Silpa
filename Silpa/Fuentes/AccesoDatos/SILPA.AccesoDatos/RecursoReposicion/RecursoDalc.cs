using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.AccesoDatos.RecursoReposicion
{
    /// <summary>
    /// Acceso a Datos para Recurso de Reposición
    /// </summary>
    public class RecursoDalc
    {
        private Configuracion objConfiguracion;


        public RecursoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Crea un Recurso de Reposición
        /// </summary>
        /// <param name="objRecurso">Objeto con datos del Recurso</param>
        public void InsertarRecurso(ref RecursoEntity objRecurso)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] {  objRecurso.IDRecurso, objRecurso.Descripcion, objRecurso.Estado.IDEstadoRecurso, objRecurso.Acto.IdActoNotificacion, objRecurso.NumeroIdentificacion };
            DbCommand cmd = db.GetStoredProcCommand("SIH_CREAR_RECURSO_REPOSICION", parametros);

            try
            {
                db.ExecuteNonQuery(cmd);
                objRecurso.IDRecurso = Int64.Parse(cmd.Parameters["@REC_ID"].Value.ToString());

            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        /// <summary>
        /// Insertar un nuevo recurso de reposición
        /// </summary>
        /// <param name="objRecurso">RecursoEntity con la información del recurso</param>
        /// <param name="expedientePadre">string con el expediente</param>
        /// <param name="vitalAdicional">string con el numero vital adicional</param>
        /// <param name="vitalPadre">string con el numero vital original</param>
        /// <param name="vitalGenerado">string con el numero vital de radicdo de recurso</param>
        /// <param name="numeroActo">string con el numero de acto</param>
        public void InsertarRecursoExtendido(ref RecursoEntity objRecurso, string expedientePadre, 
                                             string vitalAdicional, string vitalPadre, string vitalGenerado, string numeroActo)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
              { objRecurso.IDRecurso, objRecurso.Descripcion, objRecurso.Estado.IDEstadoRecurso,
                objRecurso.Acto.IdActoNotificacion, objRecurso.NumeroIdentificacion,
                  expedientePadre, vitalPadre, vitalAdicional, vitalGenerado, numeroActo
            };


            /*
             * 
             * @REC_ID decimal output,
	@P_SRE_DESCRIPCION varchar (500) ,
	@P_ID_ESTADO_RECURSO int ,
	@P_ID_ACTO_NOTIFICACION decimal,
	@P_NPE_NUMERO_IDENTIFICACION VARCHA
             * 
             * 	@REP_EXPEDIENTE_PADRE VARCHAR(30), 
	@REP_VITAL_ADICIONAL VARCHAR(30),
	@REP_VITAL_PADRE VARCHAR(30),
	@REP_VITAL_GENERADO VARCHAR(30),
	@REP_NUMERO_AUTO VARCHAR(30)
             */


            DbCommand cmd = db.GetStoredProcCommand("SIH_CREAR_RECURSO_REPOSICION_EXTENDIDO", parametros);

            try
            {
                db.ExecuteNonQuery(cmd);
                objRecurso.IDRecurso = Int64.Parse(cmd.Parameters["@REC_ID"].Value.ToString());

            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        /// <summary>
        /// Actualiza un Recurso de Reposición
        /// </summary>
        /// <param name="objRecurso">Objeto con los Datos del Recurso</param>
        public void ActualizarRecurso(RecursoEntity objRecurso)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objRecurso.Descripcion, objRecurso.Estado.IDEstadoRecurso, objRecurso.Acto.IdActoNotificacion };
            DbCommand cmd = db.GetStoredProcCommand("", parametros);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        public RecursoEntity ObtenerRecurso(object[] parametros)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SIH_LISTAR_RECURSO_REPOSICION", parametros);
            RecursoEntity recurso = new RecursoEntity();
            
            try
            {
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dt = ds.Tables[0].Rows[0];
                        recurso = new RecursoEntity();
                        recurso.IDRecurso = Convert.ToDecimal(dt["ID_RECURSO_REPOSICION"]);
                        recurso.Descripcion = Convert.ToString(dt["SRE_DESCRIPCION"]);
                        int idEstadoRecurso = Convert.ToInt32(dt["ID_ESTADO_RECURSO"]);
                        recurso.NumeroIdentificacion = dt["NPE_NUMERO_IDENTIFICACION"].ToString();

                        RecursoEstadoDalc recEstadoDalc = new RecursoEstadoDalc();
                        recurso.Estado = recEstadoDalc.obtenerRecursoEstado(new object[] { idEstadoRecurso,null,null });
                        
                        if (dt["SRE_DESCRIPCION"] != DBNull.Value)
                            recurso.Descripcion = Convert.ToString(dt["SRE_DESCRIPCION"]);
                        if (dt["ID_ACTO_NOTIFICACION"] != DBNull.Value)
                            recurso.IdActoNotificacion = Convert.ToDecimal(dt["ID_ACTO_NOTIFICACION"]);

                    }
                }
                return recurso;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        public List<RecursoEntity> ObtenerRecursos(object[] parametros)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SIH_LISTAR_RECURSO_REPOSICION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<RecursoEntity> lista = new List<RecursoEntity>();
            RecursoEntity recurso;
            RecursoEstadoDalc recEstadoDalc = new RecursoEstadoDalc(); ;
            NotificacionDalc notDalc = new NotificacionDalc(); ;
            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                   
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        recurso = new RecursoEntity();
                        recurso.IDRecurso= Convert.ToDecimal(dt["ID_RECURSO_REPOSICION"]);

                        if (dt["SRE_DESCRIPCION"] != DBNull.Value)
                          recurso.Descripcion = ((string)dt["SRE_DESCRIPCION"]);
                        int idEstadoRecurso = Convert.ToInt32(dt["ID_ESTADO_RECURSO"]);

                        recurso.Estado = recEstadoDalc.obtenerRecursoEstado(new object[] { idEstadoRecurso ,null,null});

                        if (dt["ID_ACTO_NOTIFICACION"] != DBNull.Value)
                        {
                            recurso.Acto = notDalc.ObtenerActo(new object[] { Convert.ToDecimal(dt["ID_ACTO_NOTIFICACION"]), null, null, null, null, null, null, null, null,null,null});
                        }
                        lista.Add(recurso);
                    }
                    return lista;
                }
                return new List<RecursoEntity>();
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }

        public DataSet ObtenerNumeroVitalRecursoReposicion(string numeroVital, string expediente, int idAutoridad)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REP_LISTAR_RECURSO_REPOSICION", new object[] { numeroVital, expediente, idAutoridad });
                return db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Obtener Número Vital Recurso Reposición.";
                throw new Exception(strException, ex);
            }
        }


        /// <summary>
        /// Retornar el listado de actos administrativos para recurso de reposición para una persona que cumpla con los parametros de busqueda
        /// </summary>
        /// <param name="p_lngIDApplicationUser">long con el identificador del usuario que realiza la consulta</param>
        /// <param name="p_strNumeroVital">string con el numero VITAL. Opcional</param>
        /// <param name="p_strExpediente">string con el codigo del expediente. Opcional</param>
        /// <param name="p_strNumeroActo">string con el numero de acto. Opcional</param>
        /// <param name="p_intAutoridadAmbientalID">int con el identificador de la autoridad ambiental. Opcional</param>
        /// <param name="p_objFechaActoDesde">DateTime con la fecha inicial del rango</param>
        /// <param name="p_objFechaActoHasta">DateTime con la fecha final del rango</param>        
        /// <returns>Lista de objeto RecursoParaPresentarEntity List<RecursoParaPresentarEntity></returns>
        public List<ActoParaRecursoEntity> ObtenerListadoActosAdministrativosRecursoPersona(long p_lngIDApplicationUser, string p_strNumeroVital, string p_strExpediente,
                                                                        string p_strNumeroActo, int p_intAutoridadAmbientalID,
                                                                        DateTime p_objFechaActoDesde, DateTime p_objFechaActoHasta)
        {
            SqlDatabase db = null;
            DbCommand cmd = null;
            List<ActoParaRecursoEntity> listRecursoparaPresentar = null;

            try
            {
                listRecursoparaPresentar = new List<ActoParaRecursoEntity>();
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                cmd = db.GetStoredProcCommand("NOT_CONSULTA_ACTOS_ADMINISTRATIVOS_RECURSO_PERSONA");
                db.AddInParameter(cmd, "@P_ID_APPLICATION_USER", DbType.Int64, p_lngIDApplicationUser);
                if (!string.IsNullOrEmpty(p_strNumeroVital))
                    db.AddInParameter(cmd, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVital);
                if (!string.IsNullOrEmpty(p_strExpediente))
                    db.AddInParameter(cmd, "@P_EXPEDIENTE", DbType.String, p_strExpediente);
                if (!string.IsNullOrEmpty(p_strNumeroActo))
                    db.AddInParameter(cmd, "@P_NUMERO_ACTO_ADMINISTRATIVO", DbType.String, p_strNumeroActo);
                if (p_intAutoridadAmbientalID > 0)
                    db.AddInParameter(cmd, "@P_AUT_ID", DbType.Int32, p_intAutoridadAmbientalID);
                db.AddInParameter(cmd, "@P_FECHA_DESDE", DbType.DateTime, p_objFechaActoDesde);
                db.AddInParameter(cmd, "@P_FECHA_HASTA", DbType.DateTime, p_objFechaActoHasta);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        listRecursoparaPresentar.Add(new ActoParaRecursoEntity()
                        {
                            ActoNotificacionID = Convert.ToInt32(reader["ID_ACTO_NOTIFICACION"]),
                            PersonaID = Convert.ToInt32(reader["ID_PERSONA"]),
                            EstadoActualID = Convert.ToInt32(reader["ESTADO_ACTUAL_ID"]),
                            EstadoFuturoID = Convert.ToInt32(reader["ESTADO_FUTURO_ID"]),
                            FlujoID = Convert.ToInt32(reader["FLUJO_ID"]),
                            IdentificacionUsuario = reader["IDENTIFICACION_USUARIO"].ToString(),
                            AutoridadID = Convert.ToInt32(reader["AUTORIDAD_ID"]),
                            Autoridad = reader["AUTORIDAD"].ToString(),
                            Expediente = reader["EXPEDIENTE"].ToString(),
                            NumeroVITAL = reader["NUMERO_VITAL"].ToString(),
                            NumeroActoAdministrativo = reader["NUMERO_ACTO_ADMINISTRATIVO"].ToString(),
                            FechaActoAdministrativo = Convert.ToDateTime(reader["FECHA_ACTO_ADMINISTRATIVO"]),
                            RutaDocumento = reader["RUTA_DOCUMENTO"].ToString(),
                            FechaNotificacion = Convert.ToDateTime(reader["FECHA_NOTIFICACION"]),
                            EstadoPersonaActoID = Convert.ToInt32(reader["ID_ESTADO_PERSONA_ACTO"])
                        });
                    }
                }
                return listRecursoparaPresentar;
               
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

    }
}
