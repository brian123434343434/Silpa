using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data.SqlClient;
using SoftManagement.Log;

namespace SILPA.AccesoDatos.ConsultaPublica
{
    public class ConsultaPublicaDaLC
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public ConsultaPublicaDaLC()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista las publicaciones existententes en la base de datos segun los registros que apliquen.
        /// </summary>
        /// <param name="parametroBusqueda">Parametro de busqueda requerido por el SP</param>
        /// <returns>Listado de  publicaciones existententes</returns>
        public List<ConsultaPublicaEntity> BuscarCampo(string parametroBusqueda)
        {
            try
            {
                List<ConsultaPublicaEntity> ListaRespuesta = new List<ConsultaPublicaEntity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaAnbogwCnx.ToString());
                object[] parametros = new object[] { parametroBusqueda };
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_PUBLICA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsResultado.Tables[0].Rows)
                    {
                        ConsultaPublicaEntity _cp = new ConsultaPublicaEntity();
                        _cp.ID_CONSULTA_PUBLICA = Convert.ToInt32(r["ID_CONSULTA_PUBLICA"].ToString());
                        _cp.TAR_SOL_ID = Convert.ToInt32(r["TAR_SOL_ID"] is DBNull ? 0 : r["TAR_SOL_ID"]);
                        _cp.SOL_NUM_SILPA = r["SOL_NUM_SILPA"].ToString();
                        _cp.SEC_ID = Convert.ToInt32(r["SEC_ID"] is DBNull ? 0 : r["SEC_ID"]);
                        _cp.SEC_NOMBRE = r["SEC_NOMBRE"].ToString();
                        _cp.SEC_PADRE_ID = Convert.ToInt32(r["SEC_PADRE_ID"] is DBNull ? 0 : r["SEC_PADRE_ID"]);
                        _cp.NOMBRE_SEC_PADRE = r["NOMBRE_SEC_PADRE"].ToString();
                        _cp.AUT_NOMBRE = r["AUT_NOMBRE"].ToString();
                        _cp.TRA_NOMBRE = r["TRA_NOMBRE"].ToString();
                        _cp.EXPEDIENTE = r["EXPEDIENTE"].ToString();
                        _cp.NOMBRE_PROYECTO = r["NOMBRE_PROYECTO"].ToString();
                        _cp.MUNICIPIO = r["MUNICIPIO"].ToString();
                        _cp.DEPARTAMENTO = r["DEPARTAMENTO"].ToString();
                        _cp.NOMBRE_COMPLETO = r["NOMBRE_COMPLETO"].ToString();
                        _cp.SOL_ID_SOLICITANTE = r["SOL_ID_SOLICITANTE"].ToString();
                        _cp.ORIGEN = r["ORIGEN"].ToString();
                        _cp.NUM_DOCUMENTO= r["NUMERO_DOCUMENTO"].ToString();
                        ListaRespuesta.Add(_cp);
                    }
                }
                return ListaRespuesta;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroSilpa"></param>
        /// <returns></returns>
        public DataTable ConsultarExpediente(string numeroSilpa)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_EXPEDIENTE_CP");
                db.AddInParameter(cmd, "P_SOL_NUMERO_SILPA", DbType.String, numeroSilpa);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                DataTable dtExpedientes = new DataTable();
                if (dsResultado.Tables[0].Rows.Count > 0)
                    dtExpedientes = db.ExecuteDataSet(cmd).Tables[0];
                return dtExpedientes;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarCampo-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroSilpa"></param>
        /// <returns></returns>
        public DataTable ConsultarExpedienteEIA(string IdSolicitud)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_EXPEDIENTE_EIA_CP");
                db.AddInParameter(cmd, "IDSOLICITUD", DbType.String, IdSolicitud);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                DataTable dtExpedientes = new DataTable();
                if (dsResultado.Tables[0].Rows.Count > 0)
                    dtExpedientes = db.ExecuteDataSet(cmd).Tables[0];
                return dtExpedientes;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "DetalleEIA-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista las publicaciones existententes en la base de datos segun los registros ESPECIFICOS que apliquen.
        /// </summary>
        /// <param name="parametroBusqueda">Parametro de busqueda requerido por el SP separado por comas</param>
        /// <returns>Listado de publicaciones existententes</returns>
        public List<ConsultaPublicaEntity> BuscarCamposEspecifico(string parametroBusqueda, int pagesize, int pageNumber, string tipoBusqueda)
        {
            try
            {
                int numerofilas = 0;
                int numeroCampos = 0;
                List<ConsultaPublicaEntity> ListaRespuesta = new List<ConsultaPublicaEntity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaAnbogwCnx.ToString());
                object[] parametros = new object[] { parametroBusqueda, tipoBusqueda, pagesize, pageNumber, numerofilas, numeroCampos };
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_PUBLICA_MULTIPLE", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    numerofilas = Convert.ToInt32(cmd.Parameters["@PageCount"].Value);
                    numeroCampos = Convert.ToInt32(cmd.Parameters["@RecordCnt"].Value);
                    foreach (DataRow r in dsResultado.Tables[0].Rows)
                    {
                        ConsultaPublicaEntity _cp = new ConsultaPublicaEntity();
                        _cp.ID_CONSULTA_PUBLICA = Convert.ToInt32(r["ID_CONSULTA_PUBLICA"].ToString());
                        _cp.TAR_SOL_ID = Convert.ToInt32(r["TAR_SOL_ID"] is DBNull ? 0 : r["TAR_SOL_ID"]);
                        _cp.SOL_NUM_SILPA = r["SOL_NUM_SILPA"].ToString();
                        _cp.SEC_ID = Convert.ToInt32(r["SEC_ID"] is DBNull ? 0 : r["SEC_ID"]);
                        _cp.SEC_NOMBRE = r["SEC_NOMBRE"].ToString();
                        _cp.SEC_PADRE_ID = Convert.ToInt32(r["SEC_PADRE_ID"] is DBNull ? 0 : r["SEC_PADRE_ID"]);
                        _cp.NOMBRE_SEC_PADRE = r["NOMBRE_SEC_PADRE"].ToString();
                        _cp.AUT_NOMBRE = r["AUT_NOMBRE"].ToString();
                        _cp.TRA_NOMBRE = r["TRA_NOMBRE"].ToString();
                        _cp.EXPEDIENTE = r["EXPEDIENTE"].ToString();
                        _cp.NOMBRE_PROYECTO = r["NOMBRE_PROYECTO"].ToString();
                        _cp.MUNICIPIO = r["MUNICIPIO"].ToString();
                        _cp.DEPARTAMENTO = r["DEPARTAMENTO"].ToString();
                        _cp.NOMBRE_COMPLETO = r["NOMBRE_COMPLETO"].ToString();
                        _cp.SOL_ID_SOLICITANTE = r["SOL_ID_SOLICITANTE"].ToString();
                        _cp.AUT_NOMBRE = r["AUT_NOMBRE"].ToString();
                        _cp.TAR_FECHA_CREACION = r["TAR_FECHA_CREACION"].ToString();
                        _cp.ORIGEN = r["ORIGEN"].ToString();
                        _cp.NUM_DOCUMENTO = r["NUMERO_DOCUMENTO"].ToString();
                        _cp.temporalNumeroPaginas = numerofilas;
                        _cp.temporalNumeroRegistros = numeroCampos;
                        ListaRespuesta.Add(_cp);
                    }
                }
                return ListaRespuesta;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista los tramites existententes en la base de datos segun los registros que apliquen.
        /// </summary>
        /// <param name="parametroBusqueda">Parametro de busqueda requerido por el SP</param>
        /// <param name="pagesize">tamaño de paginas</param>
        /// <param name="pageNumber">numero de pagina a mostrar</param>
        /// <returns>lista de tramites</returns>
        public List<ConsultaPublicaEntity> BuscarCampoPaginado(string parametroBusqueda, int pagesize, int pageNumber, string tipoBusqueda)
        {
            try
            {
                int numerofilas = 0;
                int numeroCampos = 0;
                List<ConsultaPublicaEntity> ListaRespuesta = new List<ConsultaPublicaEntity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaAnbogwCnx.ToString());
                object[] parametros = new object[] { parametroBusqueda, tipoBusqueda, pagesize, pageNumber, numerofilas, numeroCampos };
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_PUBLICA_PG", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    numerofilas = Convert.ToInt32(cmd.Parameters["@PageCount"].Value);
                    numeroCampos = Convert.ToInt32(cmd.Parameters["@RecordCnt"].Value);
                    foreach (DataRow r in dsResultado.Tables[0].Rows)
                    {
                        ConsultaPublicaEntity _cp = new ConsultaPublicaEntity();
                        _cp.ID_CONSULTA_PUBLICA = Convert.ToInt32(r["ID_CONSULTA_PUBLICA"].ToString());
                        _cp.TAR_SOL_ID = Convert.ToInt32(r["TAR_SOL_ID"] is DBNull ? 0 : r["TAR_SOL_ID"]);
                        _cp.SOL_NUM_SILPA = r["SOL_NUM_SILPA"].ToString();
                        _cp.SEC_ID = Convert.ToInt32(r["SEC_ID"] is DBNull ? 0 : r["SEC_ID"]);
                        _cp.SEC_NOMBRE = r["SEC_NOMBRE"].ToString();
                        _cp.SEC_PADRE_ID = Convert.ToInt32(r["SEC_PADRE_ID"] is DBNull ? 0 : r["SEC_PADRE_ID"]);
                        _cp.NOMBRE_SEC_PADRE = r["NOMBRE_SEC_PADRE"].ToString();
                        _cp.AUT_NOMBRE = r["AUT_NOMBRE"].ToString();
                        _cp.TRA_NOMBRE = r["TRA_NOMBRE"].ToString();
                        _cp.EXPEDIENTE = r["EXPEDIENTE"].ToString();
                        _cp.NOMBRE_PROYECTO = r["NOMBRE_PROYECTO"].ToString();
                        _cp.MUNICIPIO = r["MUNICIPIO"].ToString();
                        _cp.DEPARTAMENTO = r["DEPARTAMENTO"].ToString();
                        _cp.NOMBRE_COMPLETO = r["NOMBRE_COMPLETO"].ToString();
                        _cp.SOL_ID_SOLICITANTE = r["SOL_ID_SOLICITANTE"].ToString();
                        _cp.ORIGEN = r["ORIGEN"].ToString();
                        _cp.TAR_FECHA_CREACION = r["TAR_FECHA_CREACION"].ToString();
                        _cp.NUM_DOCUMENTO = r["NUMERO_DOCUMENTO"].ToString();
                        _cp.temporalNumeroPaginas = numerofilas;
                        _cp.temporalNumeroRegistros = numeroCampos;
                        ListaRespuesta.Add(_cp);
                    }
                }

                return ListaRespuesta;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarCampoPaginado-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Verifica si para los parametros dados existe registro almacenado en la bodega de datos de la consulta pública. 
        /// </summary>
        /// <author>FRAMIREZ - 07072020</author>
        /// <param name="talSolId">talSolId</param>
        /// <param name="solNumSilpa">solNumSilpa</param>
        /// <param name="origen">Origen de la información (EIA, PUBLICACION, SILA, SILAMC, VITAL)</param>
        /// <returns>booleano indicando si existe o no el registro</returns>
        public string BuscarSiExisteRegistroEnBodega(int talSolId, string solNumSilpa, string origen)
        {
            try
            {              
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaAnbogwCnx.ToString());
                object[] parametros = new object[] { talSolId, solNumSilpa, origen,0};
                DbCommand cmd = db.GetStoredProcCommand("SP_VERIFICAR_EXISTE_REGISTRO_CONSULTA_PUBLICA", parametros);
                db.ExecuteNonQuery(cmd);

                string resultado = db.GetParameterValue(cmd, "@Existe").ToString();  

                return resultado;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarSiExisteRegistroEnBodega-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Inserta registro en la tabla VS_BOD_DATA_SILA, tabla que alamcena los registros de SILA que serán utilizados por la consulta pública. 
        /// </summary>
        /// <author>FRAMIREZ - 10072020</author>
        /// <param name="registroConsultaPublica">Registro con los datos a insertar</param>
        /// <returns>Entero con el Id del registro insertado en la tabla VS_BOD_DATA_SILA</returns>
        public int InsertarRegistroEnBodegaVsBodDataSila(ConsultaPublicaEntity registroConsultaPublica) 
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaAnbogwCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_TB_BOD_DATA_SILA");
                db.AddInParameter(cmd, "TAR_SOL_ID", DbType.Int32, registroConsultaPublica.TAR_SOL_ID);
                db.AddInParameter(cmd, "SOL_NUM_SILPA", DbType.String, registroConsultaPublica.SOL_NUM_SILPA);
                db.AddInParameter(cmd, "ORIGEN", DbType.String, registroConsultaPublica.ORIGEN);

                if(registroConsultaPublica.SEC_ID != 0){
                    db.AddInParameter(cmd, "SEC_ID", DbType.Int32, registroConsultaPublica.SEC_ID);
                }
                else{
                    db.AddInParameter(cmd, "SEC_ID", DbType.Int32, DBNull.Value);
                }

                if (registroConsultaPublica.SEC_PADRE_ID != 0)
                {
                    db.AddInParameter(cmd, "SEC_PADRE_ID", DbType.Int32, registroConsultaPublica.SEC_PADRE_ID);
                }
                else
                {
                    db.AddInParameter(cmd, "SEC_PADRE_ID", DbType.Int32, DBNull.Value);
                }


                if (int.Parse(registroConsultaPublica.DEPARTAMENTO) != 0)
                {
                    db.AddInParameter(cmd, "DEPARTAMENTO", DbType.String, registroConsultaPublica.DEPARTAMENTO);    
                }
                else
                {
                    db.AddInParameter(cmd, "DEPARTAMENTO", DbType.String, DBNull.Value);
                }


                if (int.Parse(registroConsultaPublica.NUM_DOCUMENTO) != 0)
                {
                    db.AddInParameter(cmd, "NUMERO_DOCUMENTO", DbType.String, registroConsultaPublica.NUM_DOCUMENTO);
                }
                else
                {
                    db.AddInParameter(cmd, "NUMERO_DOCUMENTO", DbType.String, DBNull.Value);
                }
                
                
                db.AddInParameter(cmd, "SEC_NOMBRE", DbType.String, registroConsultaPublica.SEC_NOMBRE);
                db.AddInParameter(cmd, "NOMBRE_SEC_PADRE", DbType.String, registroConsultaPublica.NOMBRE_SEC_PADRE);
                db.AddInParameter(cmd, "AUT_NOMBRE", DbType.String, registroConsultaPublica.AUT_NOMBRE);
                db.AddInParameter(cmd, "TRA_NOMBRE", DbType.String, registroConsultaPublica.TRA_NOMBRE);
                db.AddInParameter(cmd, "EXPEDIENTE", DbType.String, registroConsultaPublica.EXPEDIENTE);
                db.AddInParameter(cmd, "NOMBRE_PROYECTO", DbType.String, registroConsultaPublica.NOMBRE_PROYECTO);
                db.AddInParameter(cmd, "TAR_FECHA_CREACION", DbType.String, registroConsultaPublica.TAR_FECHA_CREACION);
                db.AddInParameter(cmd, "TAR_FECHA_FINALIZACION", DbType.String, registroConsultaPublica.TAR_FECHA_FINALIZACION);
                db.AddInParameter(cmd, "MUNICIPIO", DbType.String, registroConsultaPublica.MUNICIPIO);
                
                db.AddInParameter(cmd, "NOMBRE_COMPLETO", DbType.String, registroConsultaPublica.NOMBRE_COMPLETO);
                db.AddInParameter(cmd, "SOL_ID_SOLICITANTE", DbType.String, registroConsultaPublica.SOL_ID_SOLICITANTE);

                

                var respuesta = db.ExecuteNonQuery(cmd);
                return respuesta;

            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "InsertarRegistroEnBodega-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }
    }
}
