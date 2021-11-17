using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.SqlClient;
using SoftManagement.Log;
//using SILPA.LogicaNegocio.PermisosAmbientales;

namespace SILPA.AccesoDatos.Generico
{
    public class RadicacionDocumentoDalc
    {
        private Configuracion objConfiguracion;

        public RadicacionDocumentoDalc()
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

        }


        /// <summary>
        /// Ingresa los datos: fecha de radicacion y numero de radicacion a un columna especifica de la tabla GEN_RADICACION.
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los valores de los campos que se van a modificiar</param>
        public void ActualizarRadicacionDocumento(ref RadicacionDocumentoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] 
                                      { 
                                          objIdentity.Id, 
                                          objIdentity.NumeroRadicacionDocumento,
                                          objIdentity.FechaRadicacion
                                      };

                DbCommand cmd = db.GetStoredProcCommand("GEN_ASOCIAR_RADICACION_AA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza el Estado Leído / No leído de la radicación
        /// </summary>
        /// <param name="objIdentity"></param>
        public void ActualizarEstadoRadicacion(ref RadicacionDocumentoIdentity objIdentity)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] 
                                      {
                                          objIdentity.Id,
                                          objIdentity.Leido
                                      };

            DbCommand cmd = db.GetStoredProcCommand("GEN_ACTUALIZAR_ESTADO_RADICACION", parametros);
            try
            {
                //DataSet dsResultado = db.ExecuteDataSet(cmd);
                int i = db.ExecuteNonQuery(cmd);

            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }

        }

        /// <summary>
        /// Metodo que asigna el valor de numero de radicacion a un objeto radicacion en particular
        /// </summary>
        /// <param name="intIdRadicacion">int: indentificador del objeto radicacion </param>
        /// <param name="strNumeroRadicacion">string: numero de radicacion asignado al documento</param>
        public void ActualizarRadicacionDocumento(Int64? intIdradicacion, string strNumeroRadicacion, Nullable<DateTime> dtFechaRadicacion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] 
                                      { 
                                          intIdradicacion,                                         
                                          strNumeroRadicacion,
                                          dtFechaRadicacion 
                                      };

                DbCommand cmd = db.GetStoredProcCommand("GEN_ASOCIAR_RADICACION_AA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// JMM
        /// Adición de metodo para actualizar el registro de radicación con el id de la autoridad ambiental seleccionada
        /// </summary>
        /// <param name="idprocessInstance"></param>
        /// <param name="AutoridadAmbiental"></param>
        public void ActualizarRadicacionAA(ref RadicacionDocumentoIdentity objIdentity)
        {
            try
            {
                int iIdRadicacion = 0;

                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] 
                                      { 
                                          objIdentity.NumeroSilpa,                                         
                                          objIdentity.IdAA,
                                          objIdentity.IdRadicacion
                                      };

                DbCommand cmd = db.GetStoredProcCommand("GEN_ACTUALIZAR_RADICACION_AA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.IdRadicacion = int.Parse(cmd.Parameters["@V_IDRADICACION"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }


        /// <summary>
        /// Radica los documentos mediante un Objeto Identity
        /// </summary>
        public void RadicarDocumento(ref RadicacionDocumentoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] 
                                      { objIdentity.NumeroSilpa, 
                                        objIdentity.NumeroRadicacionDocumento,
                                        objIdentity.ActoAdministrativo, 
                                        objIdentity.NumeroFormulario,
                                        objIdentity.IdSolicitante,
                                        objIdentity.IdAA,
                                        objIdentity.IdRadicacion,
                                        objIdentity.TipoDocumento,
                                        objIdentity.UbicacionDocumento
                                      };

                DbCommand cmd = db.GetStoredProcCommand("GEN_GENERAR_RADICACION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                
                objIdentity.IdRadicacion = int.Parse(cmd.Parameters["@ID_RADICACION"].Value.ToString());
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, ex.Message);
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// Método que permite generar solicitud de radicación al moimento de solicitar información
        /// dede autoridad ambiental a otra Autoridad ambiental
        /// </summary>
        /// <param name="objIdentity">RadicacionDocumentoIdentity objIdentity</param>
        public void RadicarDocumentoSolicitudEE(ref RadicacionDocumentoIdentity objIdentity, int idAAOrigen, bool respuesta)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] 
                                      { objIdentity.NumeroSilpa, 
                                        objIdentity.NumeroRadicacionDocumento,
                                        objIdentity.ActoAdministrativo, 
                                        objIdentity.NumeroFormulario,
                                        objIdentity.IdSolicitante,
                                        objIdentity.IdAA, 
                                        idAAOrigen,
                                        objIdentity.IdRadicacion,
                                        objIdentity.TipoDocumento,
                                        objIdentity.UbicacionDocumento,
                                        respuesta
                                      };

                DbCommand cmd = db.GetStoredProcCommand("GEN_GENERAR_RADICACION_SOLICITUD_EE", parametros);
                //DataSet dsResultado = db.ExecuteDataSet(cmd);
                int i = db.ExecuteNonQuery(cmd);

                objIdentity.IdRadicacion = int.Parse(cmd.Parameters["@ID_RADICACION"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Almacenar el detalle de los documentos Adjuntos
        /// <summary>
        /// <param name="objIdentity"></param>
        //public void RadicarDocumento(RadicacionDocumentoIdentity objIdentity)
        //{
            //try
            //{
            //    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            //    object[] parametros = new object[]{};

            //    for(int i =0; i <objIdentity.LstNombreDocumentoAdjunto.Count; i++)
            //    {
            //        parametros.SetValue(objIdentity.LstNombreDocumentoAdjunto[i].ToString(),i);
            //    }

            //    object[] parametros = new object[] { objIdentity. };

            //    DbCommand cmd = db.GetStoredProcCommand("GEN_GENERAR_RADICACION", parametros);
            //    DataSet dsResultado = db.ExecuteDataSet(cmd);
            //    objIdentity.IdRadicacion = int.Parse(cmd.Parameters["@ID_RADICACION"].Value.ToString());
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        //}



        /// <summary>
        /// Método que obtiene los datos de la radicaciones realizadas en silpa, mediante la AA
        /// </summary>
        /// <param name="intIdAA">int: indentificador de la Autoridad Ambiental </param>
        /// <returns>DataSet: Conjunto de resultados de radicación de documentos</returns>
        public DataSet ObtenerDatosRadicacionPorAA(int intIdAA)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { intIdAA };

                DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_DATOS_RADICACION_POR_AA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;

            }
            catch (SqlException sqle)
            {
                return null;
            }

        }

        /// </summary>
        /// Método que obtiene los datos de la radicaciones realizadas en silpa, mediante la AA
        /// </summary>
        /// <param name="intIdAA">int: indentificador de la Autoridad Ambiental </param>
        /// <param name="blnRadicar"></param>
        /// <returns>DataSet: Conjunto de resultados de radicación de documentos</returns>
        public DataSet ObtenerDatosRadicacionPorAA(int intIdAA, bool blnRadicar)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { intIdAA, blnRadicar };

                DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_DATOS_RADICACION_POR_AA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;

            }
            catch (SqlException sqle)
            {
                return null;
            }

        }


        /// <summary>
        /// Método que obtiene los documentos asociados al trámite que necesario adjuntar
        /// para adjuntar documentación soporte del solicitante
        /// </summary>
        /// <param name="intIdAA">int: indentificador del tipo de trámite </param>
        /// <returns>DataSet: Conjunto de resultados de radicación de documentos</returns>
        public DataSet ObtenerListaDocumentosAsociados(int intTipoTramite)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { intTipoTramite };

                DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_DOCUMENTOS_ASOCIADOS", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;

            }
            catch (SqlException sqle)
            {
                return null;
            }

        }


        /// <summary>
        /// Radica los Documentos Adjuntos
        /// Hava. 06 Noviembre de 2009
        /// </summary>
        /// <param name="strNumeroSilpa"></param>
        /// <param name="strNumeroRadicacionDocumento"></param>
        /// <param name="strActoAdministrativo"></param>
        /// <param name="intNumeroFormulario"></param>
        /// <param name="intIdSolicitante"></param>
        /// <param name="intIdRadicacion"></param>
        /// <returns></returns>
        public void RadicarDocumento(string strNumeroSilpa, string strNumeroRadicacionDocumento,
                                        string strActoAdministrativo, int intNumeroFormulario,
                                        int intIdSolicitante, string strDocumento, ref int intIdRadicacion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString);

                object[] parametros = new object[] 
                                      { strNumeroSilpa, 
                                        strNumeroRadicacionDocumento,
                                        strActoAdministrativo, 
                                        intNumeroFormulario,
                                        intIdSolicitante,
                                        strDocumento,
                                        intIdRadicacion,
                                      };

                DbCommand cmd = db.GetStoredProcCommand("GEN_GENERAR_RADICACION", parametros);
                db.ExecuteNonQuery(cmd);
                //intIdRadicacion = int.Parse(cmd.Parameters["ID_RADICACION"].Value.ToString());
                intIdRadicacion = int.Parse(cmd.Parameters[7].Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de un registro de radicación del un determinado proceso
        /// </summary>
        /// <param name="IdRadicacion">ID del registro de radicación</param>
        /// <param name="NumeroSilpa">Número de ProcessInstance - Asociado a la Solicitud y a la Radicación</param>
        /// <returns>Objeto con los datos resultado</returns>
        /// <remarks>El número de ProcessInstance no es el mismo número SILPA, es el número creado en BPM</remarks>
        public RadicacionDocumentoIdentity ObtenerDatosRadicacion(Nullable<int> IdRadicacion, Nullable<long> NumeroSilpa)
        {
            RadicacionDocumentoIdentity objIdentity = new RadicacionDocumentoIdentity();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { IdRadicacion, NumeroSilpa};
            DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_DATOS_RADICACION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            try
            {
                
            if (dsResultado!=null){
                if (dsResultado.Tables.Count>0){

                    if (dsResultado.Tables[0].Rows.Count>0)
                    {
                        objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_RADICACION"]);
                        objIdentity.NumeroSilpa = Convert.ToString(dsResultado.Tables[0].Rows[0]["NUMERO_SILPA"]);

                        objIdentity.NumeroRadicacionDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["NUMERO_RADICACION"]);
                        objIdentity.ActoAdministrativo = Convert.ToString(dsResultado.Tables[0].Rows[0]["ACTO_ADMINISTRATIVO"]);
                        objIdentity.NumeroFormulario = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_FORMULARIO"]);
                        objIdentity.IdSolicitante = Convert.ToString(dsResultado.Tables[0].Rows[0]["ID_SOLICITANTE"].ToString());
                        objIdentity.IdAA = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_AA"]);
                        objIdentity.FechaSolicitud = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FECHA"]);
                        objIdentity.TipoDocumento = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TIPO_DOCUMENTO_RADICADO"]);

                        if (dsResultado.Tables[0].Rows[0]["FECHA_RADICACION_AA"] != DBNull.Value)
                        {
                            objIdentity.FechaRadicacion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FECHA_RADICACION_AA"]);
                        }

                        objIdentity.TipoDocumento = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TIPO_DOCUMENTO_RADICADO"]);

                        if (dsResultado.Tables[0].Rows[0]["PATH_DOCUMENTO"] != DBNull.Value)
                        {
                            objIdentity.UbicacionDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["PATH_DOCUMENTO"]);
                        }

                        if (dsResultado.Tables[0].Rows[0]["NUMERO_VITAL_COMPLETO"] != DBNull.Value)
                        {
                            objIdentity.NumeroVITALCompleto = dsResultado.Tables[0].Rows[0]["NUMERO_VITAL_COMPLETO"].ToString();
                        }


                        if (dsResultado.Tables[0].Rows[0]["ID_EE"] != DBNull.Value)
                        {
                            objIdentity.ID_EE = int.Parse(dsResultado.Tables[0].Rows[0]["ID_EE"].ToString());
                        }
                        else { objIdentity.ID_EE = -1; }

                        objIdentity.Leido = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["LEIDO"]);

                        //objIdentity.InformacionAdd = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["INFORMACION_ADICIONAL"]);
                        //objIdentity.Leido = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["LEIDO"]);
                        //objIdentity.DescRadicacion = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["DESCRIPCION_RADICACION"]);
                        //return objIdentity;
                    }
                }
              }
              return objIdentity;
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


        /// <summary>
        /// Obtiene los datos de un registro de radicación del un determinado proceso
        /// </summary>
        /// <param name="IdRadicacion">ID del registro de radicación</param>
        /// <param name="NumeroSilpa">Número de ProcessInstance - Asociado a la Solicitud y a la Radicación</param>
        /// <returns>Objeto con los datos resultado</returns>
        /// <remarks>El número de ProcessInstance no es el mismo número SILPA, es el número creado en BPM</remarks>
        public RadicacionDocumentoIdentity ObtenerDatosRadicacionNumeroVital(string p_strNumeroVital)
        {
            RadicacionDocumentoIdentity objIdentity = new RadicacionDocumentoIdentity();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_strNumeroVital };
            DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_DATOS_RADICACION_NUMERO_VITAL", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            try
            {

                if (dsResultado != null)
                {
                    if (dsResultado.Tables.Count > 0)
                    {

                        if (dsResultado.Tables[0].Rows.Count > 0)
                        {
                            objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_RADICACION"]);
                            objIdentity.NumeroSilpa = Convert.ToString(dsResultado.Tables[0].Rows[0]["NUMERO_SILPA"]);

                            objIdentity.NumeroRadicacionDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["NUMERO_RADICACION"]);
                            objIdentity.ActoAdministrativo = Convert.ToString(dsResultado.Tables[0].Rows[0]["ACTO_ADMINISTRATIVO"]);
                            objIdentity.NumeroFormulario = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_FORMULARIO"]);
                            objIdentity.IdSolicitante = Convert.ToString(dsResultado.Tables[0].Rows[0]["ID_SOLICITANTE"].ToString());
                            objIdentity.IdAA = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_AA"]);
                            objIdentity.FechaSolicitud = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FECHA"]);
                            objIdentity.TipoDocumento = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TIPO_DOCUMENTO_RADICADO"]);

                            if (dsResultado.Tables[0].Rows[0]["FECHA_RADICACION_AA"] != DBNull.Value)
                            {
                                objIdentity.FechaRadicacion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FECHA_RADICACION_AA"]);
                            }

                            objIdentity.TipoDocumento = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TIPO_DOCUMENTO_RADICADO"]);

                            if (dsResultado.Tables[0].Rows[0]["PATH_DOCUMENTO"] != DBNull.Value)
                            {
                                objIdentity.UbicacionDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["PATH_DOCUMENTO"]);
                            }

                            if (dsResultado.Tables[0].Rows[0]["NUMERO_VITAL_COMPLETO"] != DBNull.Value)
                            {
                                objIdentity.NumeroVITALCompleto = dsResultado.Tables[0].Rows[0]["NUMERO_VITAL_COMPLETO"].ToString();
                            }


                            if (dsResultado.Tables[0].Rows[0]["ID_EE"] != DBNull.Value)
                            {
                                objIdentity.ID_EE = int.Parse(dsResultado.Tables[0].Rows[0]["ID_EE"].ToString());
                            }
                            else { objIdentity.ID_EE = -1; }

                            objIdentity.DescRadicacion = (dsResultado.Tables[0].Rows[0]["DESCRIPCION_RADICACION"] != System.DBNull.Value ? dsResultado.Tables[0].Rows[0]["DESCRIPCION_RADICACION"].ToString() : "");

                            objIdentity.Leido = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["LEIDO"]);

                            //objIdentity.InformacionAdd = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["INFORMACION_ADICIONAL"]);
                            //objIdentity.Leido = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["LEIDO"]);
                            
                            //return objIdentity;
                        }
                    }
                }
                return objIdentity;
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
        
        /// <summary>
        /// Obtiene la última radicación para el proceso que aún no tenga asignado un número de Radicación
        /// </summary>
        /// <param name="NumeroSilpa">Número SILPA </param>
        /// <returns>Objeto de Radicación con el último registro de radicación encontrado</returns>
        /// <remarks>Este método se usa para Finalizar las Radicaciones de FormBuilder que se realizan dentro del proceso, después de la primera radicación</remarks>
        public RadicacionDocumentoIdentity ObtenerUltimaRadicacion(string NumeroSilpa)
        {
            RadicacionDocumentoIdentity objIdentity = new RadicacionDocumentoIdentity();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { NumeroSilpa };
            DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_DATOS_ULTIMA_RADICACION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            try
            {
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_RADICACION"]);
                objIdentity.NumeroSilpa = Convert.ToString(dsResultado.Tables[0].Rows[0]["NUMERO_SILPA"]);

                objIdentity.NumeroRadicacionDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["NUMERO_RADICACION"]);
                objIdentity.ActoAdministrativo = Convert.ToString(dsResultado.Tables[0].Rows[0]["ACTO_ADMINISTRATIVO"]);
                objIdentity.NumeroFormulario = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_FORMULARIO"]);
                objIdentity.IdSolicitante = Convert.ToString(dsResultado.Tables[0].Rows[0]["ID_SOLICITANTE"]);
                objIdentity.IdAA = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_AA"]);
                objIdentity.FechaSolicitud = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FECHA"]);
                objIdentity.TipoDocumento = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TIPO_DOCUMENTO_RADICADO"]);

                if (dsResultado.Tables[0].Rows[0]["FECHA_RADICACION_AA"] != DBNull.Value)
                {
                    objIdentity.FechaRadicacion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FECHA_RADICACION_AA"]);
                }

                objIdentity.TipoDocumento = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TIPO_DOCUMENTO_RADICADO"]);

                if (dsResultado.Tables[0].Rows[0]["PATH_DOCUMENTO"] != DBNull.Value)
                {
                    objIdentity.UbicacionDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["PATH_DOCUMENTO"]);
                }
                objIdentity.Leido = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["LEIDO"]);
                //objIdentity.InformacionAdd = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["INFORMACION_ADICIONAL"]);
                //objIdentity.Leido = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["LEIDO"]);
                //objIdentity.DescRadicacion = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["DESCRIPCION_RADICACION"]);

                return objIdentity;
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProcessIntace"></param>
        /// <returns></returns>
        public bool IdInstacePuedeRadicar(int idProcessIntace)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idProcessIntace };
            DbCommand cmd = db.GetStoredProcCommand("GEN_PERMITE_RADICAR_PROCESO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                return Convert.ToBoolean(dsResultado.Tables[0].Rows[0][0]);
            }
            return false;
        }
        
        public void ActualizarRadicacionRuta(int idradicacion, string rutaDocumentos)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("GEN_ACTUALIZAR_RADICACION_RUTA");
            db.AddInParameter(cmd, "P_ID_RADICACION", DbType.Int32, idradicacion);
            db.AddInParameter(cmd, "P_PATH_DOCUMENTO", DbType.String, rutaDocumentos);
            db.ExecuteNonQuery(cmd);
        }

        public void ActualizarRadicacionNUR(string strNUR, string rutaDocumentos)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("GEN_ACTUALIZAR_RUTA_NUR");
            db.AddInParameter(cmd, "P_NUR", DbType.String, strNUR);
            db.AddInParameter(cmd, "P_PATH", DbType.String, rutaDocumentos);
            db.ExecuteNonQuery(cmd);
        }
             
        public string ObtenerProcessInstance(string NumeroVital)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { NumeroVital, String.Empty };
            DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_IDPROCESSINSTANCE_POR_NUMERO_VITAL", parametros);
            db.ExecuteNonQuery(cmd);

            string result  = db.GetParameterValue(cmd, "ID_PROCESSINSTANCE").ToString();

            //SMLog.Escribir(Severidad.Informativo, "GEN_OBTENER_IDPROCESSINSTANCE_POR_NUMERO_VITAL: " +result);

            return result;

            ///SMLog.Escribir(Severidad.Critico, ex.ToString());

        }

        /// <summary>
        ///Determina si es radicacion queja.
        /// </summary>
        /// <returns>bool:</returns>
        public bool EsRadicaionQueja(long idRadicacion) 
        {
            //SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            //object[] parametros = new object[] { NumeroVital, String.Empty };
            //DbCommand cmd = db.GetStoredProcCommand("SAN_OBTENER_NOMBRE_PERSONA_QUEJA", parametros);
            //db.ExecuteNonQuery(cmd);

            //string result = db.GetParameterValue(cmd, "ID_PROCESSINSTANCE").ToString();

            //SMLog.Escribir(Severidad.Informativo, "GEN_OBTENER_IDPROCESSINSTANCE_POR_NUMERO_VITAL: " +result);

            return true;

        }


        /// <summary>
        /// Método que obtiene el listado de archivos adjuntos
        /// 20-dic-2010
        /// </summary>
        /// <param name="int_dird_id">Identificador del directorio de documentos</param>
        public string ObtenerPathDocumentos(long idRadicacion)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idRadicacion };
            DbCommand cmd = db.GetStoredProcCommand("GEN_LST_DOCUMENTOS_RADICACION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            if (dsResultado != null)
            {
                if (dsResultado.Tables.Count > 0)
                {
                    if (dsResultado.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = (DataRow)dsResultado.Tables[0].Rows[0];

                        if (dr["PATH_DOCUMENTO"] != DBNull.Value)
                        {
                            return dr["PATH_DOCUMENTO"].ToString(); ;
                        }
                        else 
                        {
                            return string.Empty;
                        }
                    }
                    else { return string.Empty; }
                }
                else { return string.Empty; }
            }
            else { return string.Empty; }
        }


        /// <summary>
        /// Obtener el path de los documentos de la radicación pertenecientes al número vital especificado
        /// </summary>
        /// <param name="p_strNumeroVital"></param>
        /// <returns>string con el path donde se ubican los documentos</returns>
        public string ObtenerPathDocumentosNumeroVital(string p_strNumeroVital)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_strNumeroVital };
            DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_PATH_DOCUMENTOS_NUMERO_VITAL", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            if (dsResultado != null)
            {
                if (dsResultado.Tables.Count > 0)
                {
                    if (dsResultado.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = (DataRow)dsResultado.Tables[0].Rows[0];

                        if (dr["PATH_DOCUMENTO"] != DBNull.Value)
                        {
                            return dr["PATH_DOCUMENTO"].ToString(); ;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else { return string.Empty; }
                }
                else { return string.Empty; }
            }
            else { return string.Empty; }
        }


        /// <summary>
        /// Obtener todos los path de los documentos de la radicación pertenecientes al número vital especificado
        /// </summary>
        /// <param name="p_strNumeroVital">string con el número vital</param>
        /// <returns>List con todos los path donde se ubican los documentos</returns>
        public List<string> ObtenerTodosPathDocumentosNumeroVital(string p_strNumeroVital)
        {
            List<string> lstPath = null;
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_strNumeroVital };
            DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_TODOS_PATH_DOCUMENTOS_NUMERO_VITAL", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);            

            if (dsResultado != null)
            {
                if (dsResultado.Tables.Count > 0)
                {
                    if (dsResultado.Tables[0].Rows.Count > 0)
                    {
                        lstPath = new List<string>();

                        foreach(DataRow dr in dsResultado.Tables[0].Rows){
                            if (dr["PATH_DOCUMENTO"] != DBNull.Value)
                            {
                                lstPath.Add(dr["PATH_DOCUMENTO"].ToString());
                            }
                        }
                    }
                }
            }

            return lstPath;
        }


        public string ObtenerPathDocumentosNUR(string NUR)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { NUR };
            DbCommand cmd = db.GetStoredProcCommand("GEN_LST_DOCUMENTOS_NUR", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            if (dsResultado != null)
            {
                if (dsResultado.Tables.Count > 0)
                {
                    if (dsResultado.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = (DataRow)dsResultado.Tables[0].Rows[0];

                        if (dr["PATH"] != DBNull.Value)
                        {
                            return dr["PATH"].ToString(); ;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else { return string.Empty; }
                }
                else { return string.Empty; }
            }
            else { return string.Empty; }
        }


        /// <summary>
        /// Hava: 12-may-10
        /// Obtiene la ruta del fus que fué guardado al crear la solicitud
        /// </summary>
        /// <param name="int_id_radicacion">long: identificador del proceso al cual pertenece el fus</param>
        /// <returns>string: path del fus generado</returns>
        public string ObtenerPathFus(long int_id_radicacion)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { int_id_radicacion };
            DbCommand cmd = db.GetStoredProcCommand("SS_COR_LST_DOCUMENTO_FUS", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            string pathFus = "";

            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
                pathFus = dsResultado.Tables[0].Rows[0]["DOCUMENTO"].ToString();

            return pathFus;
        }


        /// <summary>
        /// hava:
        /// 20-dic-10
        /// </summary>
        /// <param name="idRadicacion">long: identificador de radicaicón</param>
        /// <returns>bool</returns>
        public bool IncluirFus(long idRadicacion) 
        {
            bool result = false;
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idRadicacion, 0 };
            DbCommand cmd = db.GetStoredProcCommand("GEN_EXP_INCLUIR_DOCUMENTOS_FUS", parametros);
            db.ExecuteNonQuery(cmd);
            int resultado = (int)(db.GetParameterValue(cmd, "@INCLUYE_FUS"));

            if (resultado == 1)
                result = true;
            else
                result = false;

            return result;
        }

        
        /// <summary>
        /// hava:
        /// 20-dic-10
        /// obtiene la ubicación del rtf generado por la solicitud.
        /// </summary>
        /// <param name="idRadicacion"></param>
        /// <returns>string de la ruta</returns>
        public string ObtenerRutaFUS(long idRadicacion)
        {
            string result = string.Empty;
            int idProcessinstance = -1;

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idRadicacion, 0 };
            DbCommand cmd = db.GetStoredProcCommand("GEN_COR_LST_DOCUMENTO_FUS", parametros);

            db.ExecuteNonQuery(cmd);
            idProcessinstance = (int)(db.GetParameterValue(cmd, "@NUMERO_SILPA"));

            string path = string.Empty;

            if (idProcessinstance != -1)
            {
                path = objConfiguracion.FileTraffic + "_" + idProcessinstance.ToString() + ".rtf";
            }
            return path;
        }

        public void ActualizarRadicacionPago(int intProcessInstace, string Radicado, DateTime dateTime)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] 
                                      { 
                                         intProcessInstace, 
                                         Radicado,
                                         dateTime
                                      };

                DbCommand cmd = db.GetStoredProcCommand("GEN_ASOCIAR_RADICACION_AA_PAGO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
