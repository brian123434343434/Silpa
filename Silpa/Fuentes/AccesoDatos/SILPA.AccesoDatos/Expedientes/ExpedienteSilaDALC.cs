using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System.Data.Common;
using System.Data;
using SoftManagement.Log;

namespace SILPA.AccesoDatos.Expedientes
{
    public class ExpedienteSilaDALC
    {

        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public ExpedienteSilaDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region SILA

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametroBusqueda"></param>
        /// <returns></returns>
        public List<ExpedienteSilaEntity> ConsultarDetalleExpedienteSILA(string parametroBusqueda)
        {
            List<ExpedienteSilaEntity> ListaRespuesta = new List<ExpedienteSilaEntity>();
            try
            {
                //Llamado a webservice  
                //FABIO RAMIREZ 08-06-2020
                WSBuscador.WSBuscador buscador = new WSBuscador.WSBuscador();
                buscador.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSBuscadorSila");   

                DataTable dtResultado = buscador.ConsultarDetalleExpedienteSila(parametroBusqueda);

                if (dtResultado == null)
                    return ListaRespuesta;

                //SqlDatabase db = new SqlDatabase(objConfiguracion.SilaMinCnx.ToString());
                //object[] parametros = new object[] { parametroBusqueda };
                //DbCommand cmd = db.GetStoredProcCommand("SS_EXP_CON_EXPEDIENTE_CP", parametros);
                //DataSet dsResultado = db.ExecuteDataSet(cmd);

                ExpedienteSilaEntity expSila = new ExpedienteSilaEntity();
                if (dtResultado.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtResultado.Rows)
                    {
                        expSila.exp_id = Convert.ToInt32(dr["EXP_ID"].ToString());
                        expSila.str_exp_codigo = dr["EXP_CODIGO"].ToString();
                        expSila.str_exp_numero_vital = dr["EXP_NUMERO_VITAL"].ToString();
                        if (dr["SEC_ID"].ToString() != string.Empty)
                            expSila.sec_id = (Int32)dr["SEC_ID"];
                        expSila.str_sec_nombre = dr["SEC_NOMBRE"].ToString();
                        expSila.str_expediente_nombre = dr["EXP_NOMBRE"].ToString();
                        expSila.str_exp_descripcion = dr["EXP_DESCRIPCION"].ToString();
                        expSila.str_numero_radicacion = dr["EXP_NUMERO_RADICACION"].ToString();
                        expSila.fecha_creacion = (DateTime)dr["EXP_FECHA_CREACION"];
                        if (dr["EST_ID"].ToString() != string.Empty)
                            expSila.int_est_id = (Int32)dr["EST_ID"];
                        expSila.str_est_nombre = dr["EST_NOMBRE"].ToString();
                        if (dr["TRA_ID"].ToString() != string.Empty)
                            expSila.str_numero_tramite = Convert.ToInt32(dr["TRA_ID"].ToString());
                        expSila.str_nombre_tramite = (dr["TRA_NOMBRE"].ToString());
                        expSila.str_nombre_solicitante = dr["NOMBRE_COMPLETO"].ToString();
                        expSila.str_nombre_ubicacion = dr["DPTO_MUN"].ToString();
                        if (dr["ADM_ID"].ToString() != string.Empty)
                            expSila.str_numero_adm = Convert.ToInt32(dr["ADM_ID"].ToString());
                        expSila.str_nombre_autoridad_ambiental = "ANLA";
                        ListaRespuesta.Add(expSila);
                    }
                }
                return ListaRespuesta;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ConsultarDetalleExpedienteSila-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        #region Documentacion Metodo
        /// <summary>
        /// Lista las instancias de etapas asociadas a un expediente
        /// </summary>
        #endregion
        public DataSet ListarEtapas(string str_exp_id)
        {
            try
            {
                DataSet dsResultado = new DataSet();
                //Llamado a webservice  
                //FABIO RAMIREZ 09-06-2020
                WSBuscador.WSBuscador buscador = new WSBuscador.WSBuscador();
                buscador.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSBuscadorSila");

                DataSet dsListaResultado = buscador.ListarEtapasExpedienteSila(str_exp_id);

                if (dsListaResultado == null)
                    return dsResultado;
                //SqlDatabase db = new SqlDatabase(objConfiguracion.SilaMinCnx.ToString());
                //object[] parametros = new object[] { str_exp_id };
                //DbCommand cmd = db.GetStoredProcCommand("SS_EXP_LST_EXPEDIENTEXETAPAS", parametros);
                //DataSet dsResultado = new DataSet();
                //dsResultado = db.ExecuteDataSet(cmd);
                return dsListaResultado;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "listarEtapas-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista las instancias de uno o varios acto
        /// </summary>
        /// <param name="int_tar_id">Identificador de la tarea</param>
        public DataTable ListarInstanciasActos(string str_exp_id, string str_tar_id, string str_nombre, string str_etapa, string str_tipos_acto)
        {
            try
            {
                DataTable dtTable = new DataTable();

                //Llamado a webservice  
                //FABIO RAMIREZ 10-06-2020
                WSBuscador.WSBuscador buscador = new WSBuscador.WSBuscador();
                buscador.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSBuscadorSila");

                DataTable dtResultado = buscador.ListarInstanciasActosSila(str_exp_id, str_tar_id, str_nombre, str_etapa, str_tipos_acto);

                if (dtResultado == null)
                    return dtTable;

                return dtResultado;

                //DataTable dtTable = new DataTable();
                //DataSet dsResultado = new DataSet();
                //SqlDatabase db = new SqlDatabase(objConfiguracion.SilaMinCnx.ToString());
                //DbCommand cmd = db.GetStoredProcCommand("SS_EXP_LST_INSTANCIAS_ACTOS");
                //db.AddInParameter(cmd, "STR_EXP_ID", DbType.String, str_exp_id);
                //db.AddInParameter(cmd, "STR_TAR_ID", DbType.String, str_tar_id);
                //db.AddInParameter(cmd, "STR_NOMBRE", DbType.String, str_nombre);
                //db.AddInParameter(cmd, "STR_EXE_ID", DbType.String, str_etapa);
                //db.AddInParameter(cmd, "STR_TIPO", DbType.String, str_tipos_acto);
                //dsResultado = db.ExecuteDataSet(cmd);
                //if (dsResultado.Tables.Count > 0)
                //    dtTable = dsResultado.Tables[0];
                //return dtTable;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarCampo-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Nuevo proceso para obtener los documentos por etapas pasasndo por la validacion de notificacion donde se verifica si requiere notificacion o si ya se notifico
        /// </summary>
        /// <param name="str_exp_id"></param>
        /// <param name="str_etapa"></param>
        /// <returns></returns>
        public DataTable ListarInstanciasActosValidaNotificacion(string str_exp_id, string str_etapa)
        {
            try
            {
                DataTable dtTable = new DataTable();

                //Llamado a webservice  
                //FABIO RAMIREZ 10-06-2020
                WSBuscador.WSBuscador buscador = new WSBuscador.WSBuscador();
                buscador.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSBuscadorSila");

                DataTable dtResultado = buscador.ListarInstanciasActosValidaNotificacionSila(str_exp_id,str_etapa);

                if (dtResultado == null)
                    return dtTable;

                //DataSet dsResultado = new DataSet();
                //SqlDatabase db = new SqlDatabase(objConfiguracion.SilaMinCnx.ToString());
                //DbCommand cmd = db.GetStoredProcCommand("SS_EXP_LST_INSTANCIAS_ACTOS_NOTIFICADOS");
                //db.AddInParameter(cmd, "STR_EXP_ID", DbType.String, str_exp_id);
                //db.AddInParameter(cmd, "STR_EXE_ID", DbType.String, str_etapa);
                //dsResultado = db.ExecuteDataSet(cmd);                
                //if (dsResultado.Tables.Count > 0)
                //    dtTable = dsResultado.Tables[0];
                //return dtTable;

                return dtResultado;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarCampo-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista las instancias de uno o varios acto
        /// </summary>
        /// <param name="int_tar_id">Identificador de la tarea</param>
        public DataTable ListarExpedientesAsociados(string str_exp_id)
        {
            try
            {
                DataTable dtTable = new DataTable();

                //Llamado a webservice  
                //FABIO RAMIREZ 08-06-2020
                WSBuscador.WSBuscador buscador = new WSBuscador.WSBuscador();
                buscador.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSBuscadorSila");

                DataTable dtResultado = buscador.ListarExpedientesAsociadosSila(str_exp_id);

                if (dtResultado == null)
                    return dtTable;                
                return dtResultado;
                                
                //DataSet dsResultado = new DataSet();
                //SqlDatabase db = new SqlDatabase(objConfiguracion.SilaMinCnx.ToString());
                //DbCommand cmd = db.GetStoredProcCommand("SS_EXP_LST_EXPEDIENTES_RELACIONADOS");
                //db.AddInParameter(cmd, "INT_EXP_ID", DbType.String, str_exp_id);
                //dsResultado = db.ExecuteDataSet(cmd);
                //if (dsResultado.Tables.Count > 0)
                //    dtTable = dsResultado.Tables[0];
                //return dtTable;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ConsultarDetalleExpedienteSila-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        public DataTable ArchivosForest(string str_exp_id)
        {
            try
            {
                DataTable dtTable = new DataTable();

                //Llamado a webservice  
                //FABIO RAMIREZ 09-06-2020
                WSBuscador.WSBuscador buscador = new WSBuscador.WSBuscador();
                buscador.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSBuscadorSila");

                DataTable dtResultado = buscador.ListarArchivosForestExpedienteSila(str_exp_id);

                if (dtResultado == null)
                    return dtTable;

                //DataSet dsResultado = new DataSet();
                //SqlDatabase db = new SqlDatabase(objConfiguracion.SilaMinCnx.ToString());
                //DbCommand cmd = db.GetStoredProcCommand("SS_EXP_FOREST_LST_ARCHIVO");
                //db.AddInParameter(cmd, "P_EXP_ID", DbType.String, str_exp_id);
                //dsResultado = db.ExecuteDataSet(cmd);
                //if (dsResultado.Tables.Count > 0)
                //    dtTable = dsResultado.Tables[0];
                //return dtTable;
                return dtResultado;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ConsultarDetalleExpedienteSila-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Este metodo se encarga de listar los documentos que han sido asociados a una tarea
        /// </summary>
        /// <param name="int_dird_id">Identificador de la tarea a la que estan asociados los documentos
        /// que se van a listar</param>
        /// <returns></returns>
        public DataTable ListarDocumentos(int int_dird_id)
        {
            try
            {
                DataTable dtTable = new DataTable();

                //Llamado a webservice  
                //FABIO RAMIREZ 12-06-2020
                WSBuscador.WSBuscador buscador = new WSBuscador.WSBuscador();
                buscador.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSBuscadorSila");

                DataTable dtResultado = buscador.listarDocumentosSila(int_dird_id.ToString());

                if (dtResultado == null)
                    return dtTable;
                return dtResultado;

                //DataSet dsResultado = new DataSet();
                //DataTable dtTable = new DataTable();
                //SqlDatabase db = new SqlDatabase(objConfiguracion.SilaMinCnx.ToString());
                //DbCommand cmd = db.GetStoredProcCommand("SS_EXP_LST_DOCUMENTOS_pdf");
                //db.AddInParameter(cmd, "INT_DIRD_ID", DbType.String, int_dird_id);
                //dsResultado = db.ExecuteDataSet(cmd);
                //if (dsResultado.Tables.Count > 0)
                //    dtTable = dsResultado.Tables[0];
                //return dtTable;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ConsultarDetalleExpedienteSila-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Este metodo se encarga de listar los datos de un directorio enviado como parametro
        /// </summary>
        /// <param name="int_dir_id">Identificador del directorio</param>
        /// <returns></returns>
        public DataTable ListarDirectoriosDocumentoSila(int int_dir_id)
        {
            try
            {
                DataTable dtTable = new DataTable();

                //Llamado a webservice  
                //FABIO RAMIREZ 12-06-2020
                WSBuscador.WSBuscador buscador = new WSBuscador.WSBuscador();
                buscador.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSBuscadorSila");

                DataTable dtResultado = buscador.ListarDirectoriosDocumentoSila(int_dir_id.ToString());

                if (dtResultado == null)
                    return dtTable;
                return dtResultado;

                //DataSet dsResultado = new DataSet();
                //DataTable dtTable = new DataTable();
                //SqlDatabase db = new SqlDatabase(objConfiguracion.SilaMinCnx.ToString());
                //DbCommand cmd = db.GetStoredProcCommand("SS_EXP_CON_DIRECTORIO_DOCUMENTOS");
                //db.AddInParameter(cmd, "INT_DIRD_ID", DbType.String, int_dir_id);
                //dsResultado = db.ExecuteDataSet(cmd);
                //if (dsResultado.Tables.Count > 0)
                //    dtTable = dsResultado.Tables[0];
                //return dtTable;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ListarDirectoriosDocumento-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Este metodo se encarga de listar los documentos que han sido asociados a una tarea
        /// </summary>
        /// <param name="int_dird_id">Identificador de la tarea a la que estan asociados los documentos
        /// que se van a listar</param>
        /// <returns></returns>
        public DataSet listarDocumentosDS(int int_dird_id)
        {
            try
            {
                DataSet dsResultado = new DataSet();
                //Llamado a webservice  
                //FABIO RAMIREZ 12-06-2020
                WSBuscador.WSBuscador buscador = new WSBuscador.WSBuscador();
                buscador.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSBuscadorSila");

                DataSet dsListaResultado = buscador.listarDocumentosDSSila(int_dird_id.ToString());

                if (dsListaResultado == null)
                    return dsResultado;
                return dsListaResultado;
                
                //DataSet ds_data = new DataSet();
                //SqlDatabase db = new SqlDatabase(objConfiguracion.SilaMinCnx.ToString());
                //DbCommand cmd = db.GetStoredProcCommand("SS_EXP_LST_DOCUMENTOS_PDF");
                //db.AddInParameter(cmd, "INT_DIRD_ID", DbType.String, int_dird_id);
                //ds_data = db.ExecuteDataSet(cmd);
                //return ds_data;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ConsultarDetalleExpedienteSila-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        public DataTable ListarDirectoriosDocumentoTareaSila(int int_tar_id)
        {
            try
            {
                DataTable dtTable = new DataTable();

                //Llamado a webservice  
                //FABIO RAMIREZ 12-06-2020
                WSBuscador.WSBuscador buscador = new WSBuscador.WSBuscador();
                buscador.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSBuscadorSila");

                DataTable dtResultado = buscador.ListarDirectoriosDocumentoTareaSila(int_tar_id.ToString());

                if (dtResultado == null)
                    return dtTable;
                return dtResultado;
                
                //DataTable dtTable = new DataTable();
                //DataSet dsResultado = new DataSet();
                //SqlDatabase db = new SqlDatabase(objConfiguracion.SilaMinCnx.ToString());
                //DbCommand cmd = db.GetStoredProcCommand("SS_EXP_LST_DIRECTORIO_DOCUMENTOS");
                //db.AddInParameter(cmd, "INT_TAR_ID", DbType.String, int_tar_id);
                //dsResultado = db.ExecuteDataSet(cmd);
                //if (dsResultado.Tables.Count > 0)
                //    dtTable = dsResultado.Tables[0];
                //return dtTable;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ConsultarDetalleExpedienteSila-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        #endregion

        #region SILAMC
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametroBusqueda"></param>
        /// <returns></returns>
        public List<ExpedienteSilaEntity> ConsultarDetalleExpedienteSilaMC(string parametroBusqueda)
        {
            List<ExpedienteSilaEntity> ListaRespuesta = new List<ExpedienteSilaEntity>();
            try
            {

                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                object[] parametros = new object[] { parametroBusqueda };
                DbCommand cmd = db.GetStoredProcCommand("SS_EXP_CON_EXPEDIENTE_MC", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                
                ExpedienteSilaEntity expSila = new ExpedienteSilaEntity();
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsResultado.Tables[0].Rows)
                    {
                        expSila.exp_id = Convert.ToInt32(dr["EXP_ID"].ToString());
                        expSila.str_exp_codigo = dr["EXP_CODIGO"].ToString();
                        expSila.str_exp_numero_vital = dr["EXP_NUMERO_VITAL"].ToString();
                        if (dr["SEC_ID"].ToString() != string.Empty)
                            expSila.sec_id = (Int32)dr["SEC_ID"];
                        expSila.str_sec_nombre = dr["SEC_NOMBRE"].ToString();
                        expSila.str_expediente_nombre = dr["EXP_NOMBRE"].ToString();
                        expSila.str_exp_descripcion = dr["EXP_DESCRIPCION"].ToString();
                        expSila.str_numero_radicacion = dr["EXP_NUMERO_RADICACION"].ToString();
                        expSila.fecha_creacion = (DateTime)dr["EXP_FECHA_CREACION"];
                        if (dr["TRA_ID"].ToString() != string.Empty)
                            expSila.str_numero_tramite = Convert.ToInt32(dr["TRA_ID"].ToString());
                        expSila.str_nombre_tramite = (dr["TRA_NOMBRE"].ToString());
                        expSila.str_nombre_solicitante = dr["NOMBRE_COMPLETO"].ToString();
                        expSila.str_nombre_ubicacion = dr["DPTO_MUN"].ToString();
                        if (dr["SOL_ID_SOLICITANTE"].ToString() != string.Empty)
                            expSila.str_solicitante_id = Convert.ToInt32(dr["SOL_ID_SOLICITANTE"].ToString());
                        expSila.str_nombre_autoridad_ambiental = dr["AUT_NOMBRE"].ToString();
                        ListaRespuesta.Add(expSila);
                    }
                }
                return ListaRespuesta;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ConsultarDetalleExpedienteSilaMC-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista las instancias de etapas asociadas a un expediente
        /// </summary>
        public DataSet ListarEtapasSilaMC(string str_exp_id)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                object[] parametros = new object[] { str_exp_id };
                DbCommand cmd = db.GetStoredProcCommand("SS_EXP_LST_EXPEDIENTEXETAPAS", parametros);
                DataSet dsResultado = new DataSet();
                dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ListarEtapasSilaMC-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista las instancias de uno o varios acto
        /// </summary>
        /// <param name="int_tar_id">Identificador de la tarea</param>
        public DataTable ListarExpedientesAsociadosSilaMC(string str_exp_id)
        {
            try
            {
                DataTable dtTable = new DataTable();
                DataSet dsResultado = new DataSet();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SS_EXP_LST_EXPEDIENTES_RELACIONADOS");
                db.AddInParameter(cmd, "INT_EXP_ID", DbType.String, str_exp_id);
                dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado.Tables.Count > 0)
                    dtTable = dsResultado.Tables[0];
                return dtTable;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ConsultarDetalleExpedienteSila-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista las instancias de uno o varios acto
        /// </summary>
        /// <param name="int_tar_id">Identificador de la tarea</param>
        public DataTable ListarInstanciasActosSilaMC(string str_exp_id, string str_tar_id, string str_nombre, string str_etapa, string str_tipos_acto)
        {
            try
            {
                DataTable dtTable = new DataTable();
                DataSet dsResultado = new DataSet();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SS_EXP_LST_INSTANCIAS_ACTOS");
                db.AddInParameter(cmd, "STR_EXP_ID", DbType.String, str_exp_id);
                db.AddInParameter(cmd, "STR_TAR_ID", DbType.String, str_tar_id);
                db.AddInParameter(cmd, "STR_NOMBRE", DbType.String, str_nombre);
                db.AddInParameter(cmd, "STR_EXE_ID", DbType.String, str_etapa);
                db.AddInParameter(cmd, "STR_TIPO", DbType.String, str_tipos_acto);
                dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado.Tables.Count > 0)
                    dtTable = dsResultado.Tables[0];
                return dtTable;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarCampo-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        public DataTable ListarDocumentosSilaMC(int int_dird_id)
        {
            try
            {
                DataSet dsResultado = new DataSet();
                DataTable dtTable = new DataTable();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SS_EXP_LST_DOCUMENTOS");
                db.AddInParameter(cmd, "INT_DIRD_ID", DbType.String, int_dird_id);
                dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado.Tables.Count > 0)
                    dtTable = dsResultado.Tables[0];
                return dtTable;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ConsultarDetalleExpedienteSila-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Este metodo se encarga de listar los directorios de documentos que han sido asociados a una tarea
        /// </summary>
        /// <param name="int_tar_id">Identificador de la tarea a la que estan asociados los directorios de documentos
        /// que se van a listar</param>
        /// <returns></returns>
        public DataTable ListarDirectoriosDocumentoSilaMC(int int_tar_id)
        {
            try
            {
                DataSet ds_data = new DataSet();
                DataSet dsResultado = new DataSet();
                DataTable dtTable = new DataTable();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SS_EXP_CON_DIRECTORIO_DOCUMENTOS");
                db.AddInParameter(cmd, "INT_DIRD_ID", DbType.String, int_tar_id);
                dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado.Tables.Count > 0)
                    dtTable = dsResultado.Tables[0];
                return dtTable;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ListarDirectoriosDocumento-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Este metodo se encarga de listar los documentos que han sido asociados a una tarea
        /// </summary>
        /// <param name="int_dird_id">Identificador de la tarea a la que estan asociados los documentos
        /// que se van a listar</param>
        /// <returns></returns>
        public DataSet ListarDocumentosDSSilaMC(int int_dird_id)
        {
            try
            {
                DataSet ds_data = new DataSet();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SS_EXP_LST_DOCUMENTOS");
                db.AddInParameter(cmd, "INT_DIRD_ID", DbType.String, int_dird_id);
                ds_data = db.ExecuteDataSet(cmd);
                return ds_data;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ConsultarDetalleExpedienteSila-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        public DataTable ListarDirectoriosDocumentoTareaSILAMC(int int_tar_id)
        {
            try
            {

                DataTable dtTable = new DataTable();
                DataSet dsResultado = new DataSet();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SS_EXP_LST_DIRECTORIO_DOCUMENTOS");
                db.AddInParameter(cmd, "INT_TAR_ID", DbType.String, int_tar_id);
                dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado.Tables.Count > 0)
                    dtTable = dsResultado.Tables[0];
                return dtTable;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ConsultarDetalleExpedienteSila-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        #endregion
    }
}
