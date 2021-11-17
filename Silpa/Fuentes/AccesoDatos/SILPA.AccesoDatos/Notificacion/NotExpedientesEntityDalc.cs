using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using System.Data.Common;
using SoftManagement.Log;
using SILPA.AccesoDatos.Excepciones;

namespace SILPA.AccesoDatos.Notificacion
{
    public class NotExpedientesEntityDalc
    {
         /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private string silpaConnection;

        public NotExpedientesEntityDalc()
        {
            silpaConnection = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString;
   
        }


        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public DataSet ConsultarNotificacionExpedientes(Int32 id_sol, Int32 id_aa)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { id_sol, id_aa };
                DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTA_EXPEDIENTES", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// Consultar los expedientes a los cuales se encuentra relacionado una persona en una autoridad ambiental
        /// </summary>        
        /// <param name="p_intPerId">int con el id de la persona</param>        
        /// <param name="p_intIdAutoridad">int con el id de la autoridad ambiental</param>
        /// <returns>List con los expedientes a los cuales se encuentra relacionada la persona</returns>
        public List<NotExpedientesEntity> ConsultarExpedienteAutoridadPersona(int p_intPerId, int p_intIdAutoridad)
        {
            List<NotExpedientesEntity> objResultado = null;
            int intIdn = 0;

            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { p_intPerId, p_intIdAutoridad };
                DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTA_EXPEDIENTES_PERSONA_AA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    objResultado = new List<NotExpedientesEntity>();
                    for (intIdn = 0; intIdn < dsResultado.Tables[0].Rows.Count; intIdn ++)
                    {
                        NotExpedientesEntity not = new NotExpedientesEntity();

                        not.ID_SOLICITUD = dsResultado.Tables[0].Rows[intIdn]["ID_SOLICITUD"].ToString();
                        not.ID_EXPEDIENTE = dsResultado.Tables[0].Rows[intIdn]["ID_EXPEDIENTE"].ToString();
                        not.SOL_ID_AA = Convert.ToInt32(dsResultado.Tables[0].Rows[intIdn]["SOL_ID_AA"]);
                        not.SOL_ID_SOLICITANTE = Convert.ToInt32(dsResultado.Tables[0].Rows[intIdn]["SOL_ID_SOLICITANTE"]);
                        not.DESC_SOL_ID_SOLICITANTE = dsResultado.Tables[0].Rows[intIdn]["SOL_SOLICITANTE_NOMBRE"].ToString().Trim();
                        not.SOL_NUMERO_SILPA = dsResultado.Tables[0].Rows[intIdn]["SOL_NUMERO_SILPA"].ToString();
                        not.DESC_EXPEDIENTE = (!string.IsNullOrEmpty(not.SOL_NUMERO_SILPA) ? not.SOL_NUMERO_SILPA : ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"]) + " - " + not.ID_EXPEDIENTE;
                        not.DESC_SOL_ID_AA = dsResultado.Tables[0].Rows[intIdn]["SOL_AA_NOMBRE"].ToString();
                        objResultado.Add(not);
                    }
                }

                return objResultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public bool VerificarNotificacionesPendienteSol(string id_sol, string numero_silpa, string expediente)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { id_sol, numero_silpa, expediente };
                DbCommand cmd = db.GetStoredProcCommand("NOT_VERIFICA_SOL_NOT_PENDIENTES", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public void ActualizarTipoNotificacionPer(PersonaNotExpedienteEntity per)
        {
            SqlDatabase db = new SqlDatabase(silpaConnection);
            object[] parametros = new object[] {    per.PERSONA_PER_ID,
                                                    per.ES_NOTIFICACION_ELECTRONICA,
                                                    per.ES_NOTIFICACION_ELECTRONICA_X_EXP,
                                                    per.ES_NOTIFICACION_ELECTRONICA_AA
                                                    };

            DbCommand cmd = db.GetStoredProcCommand("ACTUALIZAR_TIPO_NOTIFICACION", parametros);
            try
            {
                db.ExecuteNonQuery(cmd);

            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public void InsertarTipoNotificacionPer(ref PersonaNotExpedienteEntity per)
        {
            SqlDatabase db = new SqlDatabase(silpaConnection);
            object[] parametros = new object[] {    per.PERSONA_IDENTIFICACION,
                                                    per.PERSONA_TIPO_IDENTIFICACION,
                                                    per.PERSONA_NOMBRE_COMPLETO,
                                                    per.PERSONA_PER_ID,
                                                    per.SOL_NUMERO_SILPA_PROCESO,
                                                    per.ES_NOTIFICACION_ELECTRONICA_X_EXP,
                                                    per.ES_NOTIFICACION_ELECTRONICA_AA,
                                                    per.ES_NOTIFICACION_ELECTRONICA,
                                                    per.PERSONA_AA,
                                                    per.PERSONA_NOMBRE_AA,
                                                    per.Fecha,
                                                    per.Activo,
                                                    per.ID
                                                    };

            DbCommand cmd = db.GetStoredProcCommand("NOT_INSERTAR_PERSONA_VS_TIPO_NOTIFICACION", parametros);
            try
            {
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                per.ID = int.Parse(cmd.Parameters["@V_IDNOT"].Value.ToString());

             

        
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        
        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public void InsertarExpedientesNotificar(List<NotExpedientesEntity> exp, Int32 IDTIPNOT)
        {
            try
            {
                foreach (NotExpedientesEntity oExp in exp)
                    this.InsertarExpedienteNotificar(oExp, IDTIPNOT);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public void DesabilitarExpedientesNotificar(List<NotExpedientesEntity> exp)
        {
            try
            {
                foreach (NotExpedientesEntity oExp in exp)
                    this.DesabilitarExpedienteNotificar(oExp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public void EliminarExpedientesNotificar(List<NotExpedientesEntity> exp, Int32 IDTIPNOT)
        {
            try
            {
                foreach (NotExpedientesEntity oExp in exp)
                    this.EliminarExpedienteNotificar(oExp, IDTIPNOT);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        
        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        private void InsertarExpedienteNotificar(NotExpedientesEntity exp, Int32 IDTIPNOT)
        {
            SqlDatabase db = new SqlDatabase(silpaConnection);
            object[] parametros = new object[] {    IDTIPNOT,
                                                    exp.SOL_NUMERO_SILPA,
                                                    exp.ID_SOLICITUD,
                                                    exp.SOL_ID_AA,
                                                    exp.SOL_ID_SOLICITANTE,
                                                    exp.ID_EXPEDIENTE, 
                                                    exp.ACTIVO,
                                                    DateTime.Now
                                                    };

            DbCommand cmd = db.GetStoredProcCommand("NOT_INSERTAR_PERSONA_VS_EXPEDIENTE", parametros);
            try
            {
                db.ExecuteNonQuery(cmd);
                
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }

        }


        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        private void EliminarExpedienteNotificar(NotExpedientesEntity exp, Int32 IDTIPNOT)
        {
            SqlDatabase db = new SqlDatabase(silpaConnection);
            object[] parametros = new object[] {    IDTIPNOT,
                                                    exp.SOL_NUMERO_SILPA,
                                                    exp.ID_SOLICITUD,
                                                    exp.SOL_ID_AA,
                                                    exp.SOL_ID_SOLICITANTE,
                                                    exp.ID_EXPEDIENTE,
                                                    DateTime.Now
                                                    };

            DbCommand cmd = db.GetStoredProcCommand("NOT_INS_ELIMINAR_PERSONA_VS_EXPEDIENTE", parametros);
            try
            {
                db.ExecuteNonQuery(cmd);

            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }

        }
        

        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public void DesabilitarExpedienteNotificar(NotExpedientesEntity exp)
        {
            SqlDatabase db = new SqlDatabase(silpaConnection);
            object[] parametros = new object[] { exp.ID_SOLICITUD_EXPEDIENTE };

            DbCommand cmd = db.GetStoredProcCommand("NOT_DESACTIVAR_PERSONA_VS_EXPEDIENTE", parametros);
            try
            {
                db.ExecuteNonQuery(cmd);

            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }

        }

        
        public DataSet obtenerUsuarioSolicitudNot(string idUsuario)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { idUsuario };
                DbCommand cmd = db.GetStoredProcCommand("NOT_OBTENER_USUARIO_SOL_NOT", parametros);
                DataSet ds_usuario = new DataSet();
                ds_usuario = db.ExecuteDataSet(cmd);

                return ds_usuario;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }



        public List<NotExpedientesEntity> ConsultarExpedienteNotificar(Int32 PerID)
        {
            try
            {
                List<NotExpedientesEntity> resultado = null;
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { PerID };
                DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTAR_NOT_PERSONA_VS_EXPEDIENTE", parametros);
                DataSet dsResultado = new DataSet();
                dsResultado = db.ExecuteDataSet(cmd);

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    resultado = new List<NotExpedientesEntity>();
                    for (int ind = 0; ind < dsResultado.Tables[0].Rows.Count; ind++)
                    {
                        NotExpedientesEntity not = new NotExpedientesEntity();
                        not.ID_EXPEDIENTE = dsResultado.Tables[0].Rows[ind]["NOT_EXPEDIENTE"].ToString();
                        not.ACTIVO = Convert.ToBoolean(dsResultado.Tables[0].Rows[ind]["ACTIVO"]);
                        not.DESC_EXPEDIENTE = dsResultado.Tables[0].Rows[ind]["EXPEDIENTE"].ToString();
                        not.SOL_ID_AA = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["AA_ID"]);
                        not.SOL_ID_SOLICITANTE = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["SOL_ID"]);
                        not.SOL_NUMERO_SILPA = dsResultado.Tables[0].Rows[ind]["NOT_NUMERO_SILPA"].ToString();
                        not.ID_SOLICITUD = dsResultado.Tables[0].Rows[ind]["SOLICITUD_ID"].ToString();
                        not.ID_SOLICITUD_EXPEDIENTE = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["IDNOT"]);
                        not.IDTIPNOT = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["IDTIPNOT"]);
                        not.DESC_SOL_ID_AA = dsResultado.Tables[0].Rows[ind]["AUT_NOMBRE"].ToString();
                        not.DESC_SOL_ID_SOLICITANTE = dsResultado.Tables[0].Rows[ind]["PER_NOMBRE_COMPLETO"].ToString();
                        resultado.Add(not);
                    }
                }

                return resultado;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public List<PersonaNotExpedienteEntity> listarNotificadosExpedientes(string cod_exp, string numero_silpa_exp)
        {
            try
            {
                List<PersonaNotExpedienteEntity> resultado = null;
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { cod_exp, numero_silpa_exp};
                DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTA_NOTIFICADOS_EXPEDIENTES", parametros);
                DataSet dsResultado = new DataSet();
                dsResultado = db.ExecuteDataSet(cmd);

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    resultado = new List<PersonaNotExpedienteEntity>();
                    for (int ind = 0; ind < dsResultado.Tables[0].Rows.Count; ind++)
                    {
                        PersonaNotExpedienteEntity not = new PersonaNotExpedienteEntity();
                        not.PERSONA_IDENTIFICACION = dsResultado.Tables[0].Rows[ind]["IDE_NUMERO"].ToString();
                        not.PERSONA_TIPO_IDENTIFICACION = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["ID_TIPO_IDENTIFICACION"]);
                        not.SOL_NUMERO_SILPA_PROCESO = dsResultado.Tables[0].Rows[ind]["TIR_NUMERO_VITAL"].ToString();
                        not.PERSONA_NOMBRE_COMPLETO = dsResultado.Tables[0].Rows[ind]["PER_NOMBRE_COMPLETO"].ToString();
                        not.PERSONA_PER_ID = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["PER_ID"]);
                        resultado.Add(not);
                    }
                }

                return resultado;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public PersonaNotExpedienteEntity listarNotificadosEstadoExpedientes(string cod_exp, string numero_silpa_exp,string num_iden)
        {
            try
            {
                PersonaNotExpedienteEntity resultado = new PersonaNotExpedienteEntity();
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { cod_exp, numero_silpa_exp, num_iden };
                DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTAR_ESTADOS_NOTIFICADOS", parametros);
                DataSet dsResultado = new DataSet();
                dsResultado = db.ExecuteDataSet(cmd);
                      
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                   
                        resultado.EsNotificacionElec = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["ES_NOTIFICACION_ELECTRONICA"].ToString());
                        resultado.EstadoNotificacion = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ESTADO_NOTIFICADO"]);
                        
                    
                }

                return resultado;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }



        public PersonaNotExpedienteEntity listarTipoNotificadosPersona(Int32 per_id)
        {
            try
            {
                PersonaNotExpedienteEntity resultado = null;
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { per_id };
                DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTA_TIPO_NOTIFICADOS_EXPEDIENTES", parametros);
                DataSet dsResultado = new DataSet();
                dsResultado = db.ExecuteDataSet(cmd);

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                        resultado = new PersonaNotExpedienteEntity();
                   
                        PersonaNotExpedienteEntity not = new PersonaNotExpedienteEntity();
                        not.PERSONA_PER_ID = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_ID"]);
                        not.ES_NOTIFICACION_ELECTRONICA = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["ES_NOTIFICACION_ELECTRONICA"]);
                        not.ES_NOTIFICACION_ELECTRONICA_X_EXP = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["ES_NOTIFICACION_ELECTRONICA_X_EXP"]);
                        not.ES_NOTIFICACION_ELECTRONICA_AA = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["ES_NOTIFICACION_ELECTRONICA_AA"]);
                        not.PERSONA_AA = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AA_ID"]);
                        not.PERSONA_NOMBRE_AA = dsResultado.Tables[0].Rows[0]["NOMBRE_AA"].ToString();
                        resultado = not;
                }

                return resultado;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }



        public List<NotExpedientesEntity> ConsultarExpedientePersonaNotificar(String PerID, String codigoExpediente, String NumeroSilpa)
        {
            try
            {
                List<NotExpedientesEntity> resultado = null;
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { PerID, codigoExpediente, NumeroSilpa };
                DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTAR_NOT_EXPEDIENTE_VS_PERSONA", parametros);
                DataSet dsResultado = new DataSet();
                dsResultado = db.ExecuteDataSet(cmd);

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    resultado = new List<NotExpedientesEntity>();
                    for (int ind = 0; ind < dsResultado.Tables[0].Rows.Count; ind++)
                    {
                        NotExpedientesEntity not = new NotExpedientesEntity();
                        not.ID_EXPEDIENTE = dsResultado.Tables[0].Rows[ind]["NOT_EXPEDIENTE"].ToString();
                        not.ACTIVO = Convert.ToBoolean(dsResultado.Tables[0].Rows[ind]["ACTIVO"]);
                        not.DESC_EXPEDIENTE = dsResultado.Tables[0].Rows[ind]["EXPEDIENTE"].ToString();
                        not.SOL_ID_AA = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["AA_ID"]);
                        not.SOL_ID_SOLICITANTE = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["SOL_ID"]);
                        not.SOL_NUMERO_SILPA = dsResultado.Tables[0].Rows[ind]["NOT_NUMERO_SILPA"].ToString();
                        not.ID_SOLICITUD = dsResultado.Tables[0].Rows[ind]["SOLICITUD_ID"].ToString();
                        not.ID_SOLICITUD_EXPEDIENTE = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["IDNOT"]);
                        not.IDTIPNOT = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["IDTIPNOT"]);
                        not.DESC_SOL_ID_AA = dsResultado.Tables[0].Rows[ind]["AUT_NOMBRE"].ToString();
                        not.DESC_SOL_ID_SOLICITANTE = dsResultado.Tables[0].Rows[ind]["PER_NOMBRE_COMPLETO"].ToString();
                        resultado.Add(not);
                    }
                }

                return resultado;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Consultar Expediente Persona Notificar.";
                throw new Exception(strException, ex);
            }
        }

        public List<NotExpedientesEntity> ConsultarExpedientePersonaNotificarxAA(String PerID, String codigoExpediente, String CodigoAA)
        {
            try
            {
                List<NotExpedientesEntity> resultado = null;
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { PerID, CodigoAA, codigoExpediente };
                DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTAR_NOT_EXPEDIENTE_VS_PERSONA_AA", parametros);
                DataSet dsResultado = new DataSet();
                dsResultado = db.ExecuteDataSet(cmd);

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    resultado = new List<NotExpedientesEntity>();
                    for (int ind = 0; ind < dsResultado.Tables[0].Rows.Count; ind++)
                    {
                        NotExpedientesEntity not = new NotExpedientesEntity();
                        not.ID_EXPEDIENTE = dsResultado.Tables[0].Rows[ind]["NOT_EXPEDIENTE"].ToString();
                        not.ACTIVO = Convert.ToBoolean(dsResultado.Tables[0].Rows[ind]["ACTIVO"]);
                        not.DESC_EXPEDIENTE = dsResultado.Tables[0].Rows[ind]["EXPEDIENTE"].ToString();
                        not.SOL_ID_AA = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["AA_ID"]);
                        not.SOL_ID_SOLICITANTE = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["SOL_ID"]);
                        not.SOL_NUMERO_SILPA = dsResultado.Tables[0].Rows[ind]["NOT_NUMERO_SILPA"].ToString();
                        not.ID_SOLICITUD = dsResultado.Tables[0].Rows[ind]["SOLICITUD_ID"].ToString();
                        not.ID_SOLICITUD_EXPEDIENTE = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["IDNOT"]);
                        not.IDTIPNOT = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["IDTIPNOT"]);
                        not.DESC_SOL_ID_AA = dsResultado.Tables[0].Rows[ind]["AUT_NOMBRE"].ToString();
                        not.DESC_SOL_ID_SOLICITANTE = dsResultado.Tables[0].Rows[ind]["PER_NOMBRE_COMPLETO"].ToString();
                        resultado.Add(not);
                    }
                }

                return resultado;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        public List<NotExpedientesEntity> ConsultarNumeroSilpaPersonaNotificar(String PerID, String NumeroSilpa)
        {
            try
            {
                List<NotExpedientesEntity> resultado = null;
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { PerID, NumeroSilpa };
                DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTAR_NOT_NUMERO_SILPA_VS_PERSONA", parametros);
                DataSet dsResultado = new DataSet();
                dsResultado = db.ExecuteDataSet(cmd);

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    resultado = new List<NotExpedientesEntity>();
                    for (int ind = 0; ind < dsResultado.Tables[0].Rows.Count; ind++)
                    {
                        NotExpedientesEntity not = new NotExpedientesEntity();
                        not.ID_EXPEDIENTE = dsResultado.Tables[0].Rows[ind]["NOT_EXPEDIENTE"].ToString();
                        not.ACTIVO = Convert.ToBoolean(dsResultado.Tables[0].Rows[ind]["ACTIVO"]);
                        not.DESC_EXPEDIENTE = dsResultado.Tables[0].Rows[ind]["EXPEDIENTE"].ToString();
                        not.SOL_ID_AA = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["AA_ID"]);
                        not.SOL_ID_SOLICITANTE = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["SOL_ID"]);
                        not.SOL_NUMERO_SILPA = dsResultado.Tables[0].Rows[ind]["NOT_NUMERO_SILPA"].ToString();
                        not.ID_SOLICITUD = dsResultado.Tables[0].Rows[ind]["SOLICITUD_ID"].ToString();
                        not.ID_SOLICITUD_EXPEDIENTE = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["IDNOT"]);
                        not.IDTIPNOT = Convert.ToInt32(dsResultado.Tables[0].Rows[ind]["IDTIPNOT"]);
                        not.DESC_SOL_ID_AA = dsResultado.Tables[0].Rows[ind]["AUT_NOMBRE"].ToString();
                        not.DESC_SOL_ID_SOLICITANTE = dsResultado.Tables[0].Rows[ind]["PER_NOMBRE_COMPLETO"].ToString();
                        resultado.Add(not);
                    }
                }

                return resultado;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Actualiza el tipo de notificación realizada por el usuario
        /// </summary>
        /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
        /// <param name="p_objDatosNotificacion">PersonaNotExpedienteEntity con la información basica de la notificación</param>
        public void ActualizarTipoNotificacionPer(SqlCommand p_objCommand, PersonaNotExpedienteEntity p_objDatosNotificacion)
        {
            try
            {

                //Actualizar tipo de notificacion
                p_objCommand.CommandText = "ACTUALIZAR_TIPO_NOTIFICACION";
                p_objCommand.Parameters.Clear();
                p_objCommand.Parameters.Add("@PER_ID", SqlDbType.Int).Value = p_objDatosNotificacion.PERSONA_PER_ID;
                p_objCommand.Parameters.Add("@ES_NOTIFICACION_ELECTRONICA", SqlDbType.Bit).Value = p_objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA;
                p_objCommand.Parameters.Add("@ES_NOTIFICACION_ELECTRONICA_X_EXP", SqlDbType.Bit).Value = p_objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA_X_EXP;
                p_objCommand.Parameters.Add("@ES_NOTIFICACION_ELECTRONICA_AA", SqlDbType.Bit).Value = p_objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA_AA;
                p_objCommand.ExecuteNonQuery();
            }
            catch(Exception exc){
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotExpedientesEntityDalc :: ActualizarTipoNotificacionPer -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw new NotExpedientesException("NotExpedientesEntityDalc :: ActualizarTipoNotificacionPer -> Error inesperado: " + exc.Message, exc.InnerException);
            }
        }


        /// <summary>
        /// Inserta un nuevo registro de tipo de notificación para una persona
        /// </summary>
        /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
        /// <param name="p_objDatosNotificacion">PersonaNotExpedienteEntity con la información basica de la notificación</param>
        /// <returns>int con el id del nuevo registro creado</returns>
        public int InsertarTipoNotificacionPer(SqlCommand p_objCommand, PersonaNotExpedienteEntity p_objDatosNotificacion)
        {
            int intIdSolicitud = 0;

            try
            {

                //Insertarr registro
                p_objCommand.CommandText = "NOT_INSERTAR_PERSONA_VS_TIPO_NOTIFICACION";
                p_objCommand.Parameters.Clear();
                p_objCommand.Parameters.Add("@NPE_NUMERO_IDENTIFICACION", SqlDbType.VarChar).Value = p_objDatosNotificacion.PERSONA_IDENTIFICACION;
                p_objCommand.Parameters.Add("@ID_TIPO_IDENTIFICACION", SqlDbType.Int).Value = p_objDatosNotificacion.PERSONA_TIPO_IDENTIFICACION;
                p_objCommand.Parameters.Add("@NOMBRE_COMPLETO", SqlDbType.VarChar).Value = p_objDatosNotificacion.PERSONA_NOMBRE_COMPLETO;
                p_objCommand.Parameters.Add("@PER_ID", SqlDbType.Int).Value = p_objDatosNotificacion.PERSONA_PER_ID;
                p_objCommand.Parameters.Add("@ES_NOTIFICACION_ELECTRONICA_X_EXP", SqlDbType.Bit).Value = p_objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA_X_EXP;
                p_objCommand.Parameters.Add("@ES_NOTIFICACION_ELECTRONICA_X_AA", SqlDbType.Bit).Value = p_objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA_AA;
                p_objCommand.Parameters.Add("@ES_NOTIFICACION_ELECTRONICA", SqlDbType.Bit).Value = p_objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA;
                p_objCommand.Parameters.Add("@AA_ID", SqlDbType.Int).Value = p_objDatosNotificacion.PERSONA_AA;
                p_objCommand.Parameters.Add("@NOMBRE_AA", SqlDbType.Int).Value = p_objDatosNotificacion.PERSONA_NOMBRE_AA;
                p_objCommand.Parameters.Add("@FECHA", SqlDbType.DateTime).Value = p_objDatosNotificacion.Fecha;
                p_objCommand.Parameters.Add("@ACTIVO", SqlDbType.Bit).Value = p_objDatosNotificacion.Activo;
                p_objCommand.Parameters.Add("@V_IDNOT", SqlDbType.Int);
                p_objCommand.Parameters["@V_IDNOT"].Direction = ParameterDirection.Output;
                p_objCommand.ExecuteNonQuery();

                //Cargar el id
                intIdSolicitud = int.Parse(p_objCommand.Parameters["@V_IDNOT"].Value.ToString());
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotExpedientesEntityDalc :: InsertarTipoNotificacionPer -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw new NotExpedientesException("NotExpedientesEntityDalc :: InsertarTipoNotificacionPer -> Error inesperado: " + exc.Message, exc.InnerException);
            }

            return intIdSolicitud;
        }


        /// <summary>
        /// Inserta el listado de expedientes que deben ser notificados
        /// </summary>
        /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
        /// <param name="p_intIDSolicitudNotificacion">int con el id del registro de notificación al cual se relacionara los expedientes</param>
        /// <param name="p_objListaExpedientes">List con la información de los expedientes a insertar</param>
        private void InsertarExpedientesNotificar(SqlCommand p_objCommand, int p_intIDSolicitudNotificacion, List<NotExpedientesEntity> p_objListaExpedientes)
        {
            try
            {
                //Ciclo que inserta los expedientes
                foreach (NotExpedientesEntity objExpediente in p_objListaExpedientes)
                {
                    //Insertar expediente
                    p_objCommand.CommandText = "NOT_INSERTAR_PERSONA_VS_EXPEDIENTE";
                    p_objCommand.Parameters.Clear();
                    p_objCommand.Parameters.Add("@IDTIPNOT", SqlDbType.Int).Value = p_intIDSolicitudNotificacion;
                    p_objCommand.Parameters.Add("@NOT_NUMERO_SILPA", SqlDbType.VarChar).Value = objExpediente.SOL_NUMERO_SILPA;
                    p_objCommand.Parameters.Add("@SOLICITUD_ID", SqlDbType.VarChar).Value = objExpediente.ID_SOLICITUD;
                    p_objCommand.Parameters.Add("@AA_ID", SqlDbType.Int).Value = objExpediente.SOL_ID_AA;
                    p_objCommand.Parameters.Add("@SOL_ID", SqlDbType.Int).Value = objExpediente.SOL_ID_SOLICITANTE;
                    p_objCommand.Parameters.Add("@NOT_EXPEDIENTE", SqlDbType.VarChar).Value = objExpediente.ID_EXPEDIENTE;
                    p_objCommand.Parameters.Add("@ACTIVO", SqlDbType.Bit).Value = true;
                    p_objCommand.Parameters.Add("@FECHA", SqlDbType.DateTime).Value = DateTime.Now;
                    p_objCommand.ExecuteNonQuery();
                }
                
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotExpedientesEntityDalc :: InsertarExpedientesNotificar -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw new NotExpedientesException("NotExpedientesEntityDalc :: InsertarExpedientesNotificar -> Error inesperado: " + exc.Message, exc.InnerException);
            }

        }


        /// <summary>
        /// Registrar el listado de expedientes que fueron eliminados en el registro
        /// </summary>
        /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
        /// <param name="p_intIDSolicitudNotificacion">int con el id del registro de notificación al cual se relacionara los expedientes eliminados</param>
        /// <param name="p_objListaExpedientesEliminados">List con la información de los expedientes que fueron eliminados</param>
        private void RegistrarExpedientesEliminados(SqlCommand p_objCommand, int p_intIDSolicitudNotificacion, List<NotExpedientesEntity> p_objListaExpedientesEliminados)
        {
            try
            {
                //Ciclo que inserta los expedientes
                foreach (NotExpedientesEntity objExpediente in p_objListaExpedientesEliminados)
                {
                    //Insertar expediente
                    p_objCommand.CommandText = "NOT_INS_ELIMINAR_PERSONA_VS_EXPEDIENTE";
                    p_objCommand.Parameters.Clear();
                    p_objCommand.Parameters.Add("@IDTIPNOT", SqlDbType.Int).Value = p_intIDSolicitudNotificacion;
                    p_objCommand.Parameters.Add("@NOT_NUMERO_SILPA", SqlDbType.VarChar).Value = objExpediente.SOL_NUMERO_SILPA;
                    p_objCommand.Parameters.Add("@SOLICITUD_ID", SqlDbType.VarChar).Value = objExpediente.ID_SOLICITUD;
                    p_objCommand.Parameters.Add("@AA_ID", SqlDbType.Int).Value = objExpediente.SOL_ID_AA;
                    p_objCommand.Parameters.Add("@SOL_ID", SqlDbType.Int).Value = objExpediente.SOL_ID_SOLICITANTE;
                    p_objCommand.Parameters.Add("@NOT_EXPEDIENTE", SqlDbType.VarChar).Value = objExpediente.ID_EXPEDIENTE;
                    p_objCommand.Parameters.Add("@FECHA", SqlDbType.DateTime).Value = DateTime.Now;
                    p_objCommand.ExecuteNonQuery();
                }

            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotExpedientesEntityDalc :: RegistrarExpedientesEliminados -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw new NotExpedientesException("NotExpedientesEntityDalc :: RegistrarExpedientesEliminados -> Error inesperado: " + exc.Message, exc.InnerException);
            }
        }


        /// <summary>
        /// Guarda o actualiza la información de solicitud de la notificación
        /// </summary>
        /// <param name="p_objDatosNotificacion">PersonaNotExpedienteEntity que contiene los datos basicos de la notificacion</param>
        /// <param name="p_objListaExpedientes">List con la información de los expedientes que deben ser notificados</param>
        /// <param name="p_objListaExpedientesEliminados">List con la información de los expedientes que fueron eliminados</param>
        /// <returns>string con el id del registro de la notificación</returns>
        public int GuardarSolicitudNotificacion(PersonaNotExpedienteEntity p_objDatosNotificacion, List<NotExpedientesEntity> p_objListaExpedientes, List<NotExpedientesEntity> p_objListaExpedientesEliminados)
        {
            SqlConnection objConnection = null;
            SqlTransaction objTransaccion = null;
            SqlCommand objCommand = null;
            int intIDSolicitudNotificacion = 0;

            try
            {
                //Cargar conexion
                objConnection = new SqlConnection(this.silpaConnection);

                using (objConnection)
                {
                    //Abrir conexion
                    objConnection.Open();

                    try
                    {
                        //Comenzar transaccion
                        objTransaccion = objConnection.BeginTransaction("GuardarNotificacion");

                        //Crear comando
                        objCommand = objConnection.CreateCommand();
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.Connection = objConnection;
                        objCommand.Transaction = objTransaccion;

                        //Actualizar tipo de notificacion
                        this.ActualizarTipoNotificacionPer(objCommand, p_objDatosNotificacion);

                        //Guardar la información del certificado
                        intIDSolicitudNotificacion = this.InsertarTipoNotificacionPer(objCommand, p_objDatosNotificacion);

                        //En caso de que exista información de expedientes insertarlas
                        if (p_objListaExpedientes != null && p_objListaExpedientes.Count > 0)
                        {
                            this.InsertarExpedientesNotificar(objCommand, intIDSolicitudNotificacion, p_objListaExpedientes);
                        }

                        //En caso de que exista información de expedientes eliminados registrarla
                        if (p_objListaExpedientesEliminados != null && p_objListaExpedientesEliminados.Count > 0)
                        {
                            this.RegistrarExpedientesEliminados(objCommand, intIDSolicitudNotificacion, p_objListaExpedientesEliminados);
                        }

                        //Realizar Commit de la transaccion
                        objTransaccion.Commit();

                    }
                    catch (SqlException sqle)
                    {
                        //Realizar rollback
                        objTransaccion.Rollback();

                        //Escalar exc
                        throw sqle;
                    }
                    catch (NotExpedientesException exc)
                    {
                        //Realizar rollback
                        objTransaccion.Rollback();

                        //Escalar exc
                        throw exc;
                    }
                    catch (Exception exc)
                    {
                        //Realizar rollback
                        objTransaccion.Rollback();

                        //Escalar exc
                        throw exc;
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotExpedientesEntityDalc :: GuardarSolicitudNotificacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw new NotExpedientesException("NotExpedientesEntityDalc :: GuardarSolicitudNotificacion -> Error bd: " + sqle.Message, sqle.InnerException);
            }
            catch (NotExpedientesException exc)
            {
                throw exc;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotExpedientesEntityDalc :: GuardarSolicitudNotificacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw new NotExpedientesException("NotExpedientesEntityDalc :: GuardarSolicitudNotificacion -> Error inesperado: " + exc.Message, exc.InnerException);
            }
            finally
            {
                try
                {
                    objConnection.Close();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotExpedientesEntityDalc :: GuardarCertificado -> Error bd cerrando conexión: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw new NotExpedientesException("NotExpedientesEntityDalc :: GuardarCertificado -> Error bd cerrando conexión: " + exc.Message, exc.InnerException);
                }
            }

            return intIDSolicitudNotificacion;
        }


        /// <summary>
        /// Actualizar el número vital del registro de notificación indicado
        /// </summary>
        /// <param name="p_intIDSolicitudNotificacion">int con el id del registro de notificacion</param>
        /// <param name="p_strNumeroVital">string con el numero vital</param>
        public void ActualizarNumeroSilpaTipoNotificacionPer(int p_intIDSolicitudNotificacion, string p_strNumeroVital)
        {
            SqlDatabase objConeccion = null;
            object[] objParametros = null;
            DbCommand objCommand = null;

            try
            {

                //Cargar parametros
                objParametros = new object[] { p_intIDSolicitudNotificacion, p_strNumeroVital };

                //Realizar actualización
                objConeccion = new SqlDatabase(this.silpaConnection);
                objCommand = objConeccion.GetStoredProcCommand("NOT_ACTUALIZAR_NUM_SILPA_PERSONA_VS_TIPO_NOTIFICACION", objParametros);
                objConeccion.ExecuteNonQuery(objCommand);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotExpedientesEntityDalc :: ActualizarNumeroSilpaTipoNotificacionPer -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw new NotExpedientesException("NotExpedientesEntityDalc :: ActualizarNumeroSilpaTipoNotificacionPer -> Error inesperado: " + exc.Message, exc.InnerException);
            }

        }
    }
}
