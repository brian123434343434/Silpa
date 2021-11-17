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

namespace SILPA.AccesoDatos.PINES
{
    public class PINESDALC
    {
        private Configuracion objConfiguracion;

        public PINESDALC()
        {
            objConfiguracion = new Configuracion();
        }

        public void InsertarComentarioActividad(ComentarioActividadIdentity pComentarioActividadIdentity, bool blContinuaProcesoAccion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_INSERTAR_PIN_COMETARIOS_ACTIVIDAD");
                db.AddInParameter(cmd, "p_IDProcessInstance",DbType.Int32,pComentarioActividadIdentity.IDProcessInstance);
                db.AddInParameter(cmd, "p_IDActivityInstance",DbType.Int32, pComentarioActividadIdentity.IDActivityInstance);
                db.AddInParameter(cmd, "p_IDActivity",DbType.Int32, pComentarioActividadIdentity.IDActivity);
                db.AddInParameter(cmd, "p_comments",DbType.String, pComentarioActividadIdentity.Comments);
                db.AddInParameter(cmd, "p_field",DbType.String, pComentarioActividadIdentity.Field);
                db.AddInParameter(cmd, "p_Usuario", DbType.String, pComentarioActividadIdentity.Usuario);
                db.AddInParameter(cmd, "P_ActividadRealizada", DbType.Boolean, pComentarioActividadIdentity.ActividadRealizada);
                db.AddInParameter(cmd, "p_AUT_ID", DbType.Int32, pComentarioActividadIdentity.AutoridadId);
                db.AddInParameter(cmd, "p_IDACCION", DbType.Int32, pComentarioActividadIdentity.IDAccion);
                db.AddInParameter(cmd, "p_ES_GERENTE", DbType.Boolean, pComentarioActividadIdentity.EsGerente);
                db.AddInParameter(cmd, "p_FECHA_COMPROMISO", DbType.DateTime, pComentarioActividadIdentity.FechaCompromiso);
                db.AddInParameter(cmd, "p_TIENE_PRORROGA", DbType.Boolean, pComentarioActividadIdentity.TieneProrroga);
                db.AddInParameter(cmd, "p_FECHA_PRORROGA", DbType.DateTime, pComentarioActividadIdentity.FechaProrroga);
                db.AddInParameter(cmd, "P_DETIENE_FLUJO", DbType.Boolean, pComentarioActividadIdentity.DetieneFlujo);
                db.AddInParameter(cmd, "P_CONTINUA_PROCESO_ACCION", DbType.Boolean, blContinuaProcesoAccion);
                db.AddInParameter(cmd, "P_IDPROGRESOACCION", DbType.Int32, pComentarioActividadIdentity.IDProgresoAccion);

                db.ExecuteNonQuery(cmd);
                
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void ConsultarComentarioActividad(ref ComentarioActividadIdentity pComentarioActividadIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTAR_PIN_COMETARIOS_ACTIVIDAD");
                db.AddInParameter(cmd, "p_IDProcessInstance", DbType.Int32, pComentarioActividadIdentity.IDProcessInstance);
                db.AddInParameter(cmd, "p_IDActivityInstance", DbType.Int32, pComentarioActividadIdentity.IDActivityInstance);
                db.AddInParameter(cmd, "p_IDActivity", DbType.Int32, pComentarioActividadIdentity.IDActivity);
                db.AddInParameter(cmd, "p_Usuario", DbType.String, pComentarioActividadIdentity.Usuario);
                db.AddInParameter(cmd, "p_ES_GERENTE", DbType.Boolean, pComentarioActividadIdentity.EsGerente);
                db.AddInParameter(cmd, "p_IDACCION", DbType.Int32, pComentarioActividadIdentity.IDAccion);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        pComentarioActividadIdentity.IDComentario = Convert.ToInt32(reader["IDComentario"]);
                        pComentarioActividadIdentity.Comments = reader["comments"].ToString();
                        pComentarioActividadIdentity.Field = reader["Field"].ToString();
                        pComentarioActividadIdentity.FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]);
                        pComentarioActividadIdentity.ActividadRealizada = Convert.ToBoolean(reader["ActividadRealizada"]);
                        if (reader["AUT_ID"].ToString() != string.Empty)
                            pComentarioActividadIdentity.AutoridadId = Convert.ToInt32(reader["AUT_ID"]);
                        pComentarioActividadIdentity.IDAccion = Convert.ToInt32(reader["IDACCION"]);
                        if (reader["FECHA_COMPROMISO"].ToString() != string.Empty)
                            pComentarioActividadIdentity.FechaCompromiso = Convert.ToDateTime(reader["FECHA_COMPROMISO"]);
                        if (reader["FECHA_VENCIMIENTO"].ToString() != string.Empty)
                            pComentarioActividadIdentity.FechaVencimiento = Convert.ToDateTime(reader["FECHA_VENCIMIENTO"]);
                        if (reader["FECHA_PRORROGA"].ToString() != string.Empty)
                            pComentarioActividadIdentity.FechaProrroga = Convert.ToDateTime(reader["FECHA_PRORROGA"]);
                        pComentarioActividadIdentity.TieneProrroga = Convert.ToBoolean(reader["TIENE_PRORROGA"]);
                        pComentarioActividadIdentity.DetieneFlujo = Convert.ToBoolean(reader["DETIENE_FLUJO"]);
                        pComentarioActividadIdentity.IDProgresoAccion = Convert.ToInt32(reader["IDPROGRESOACCION"]);
                    }
                }
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public List<int> ListaActividadesVariosIntervinientes()
        {
            try
            {
                List<int> LstActividadesVariosIntervinientes = new List<int>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_LISTA_ACTIVIDADES_VARIOS_INTERVINIENTES");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstActividadesVariosIntervinientes.Add(Convert.ToInt32(reader["IDActivity"]));
                    }
                }
                return LstActividadesVariosIntervinientes;

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public List<int> ListaAutoridadesSolicitudID(int IdProcessInstace, int? idField)
        {
            try
            {
                List<int> LstAutoridadesSolicitud = new List<int>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTA_AUTORIDADES_SOLICITUD");
                db.AddInParameter(cmd, "IDProcessInstance", DbType.Int32, IdProcessInstace);
                db.AddInParameter(cmd, "IDfield", DbType.Int32, idField);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstAutoridadesSolicitud.Add(Convert.ToInt32(reader["AUT_ID"]));
                    }
                }
                return LstAutoridadesSolicitud;

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public List<string> ListaAutoridadesSolicitudNombre(int IdProcessInstace, int? idField)
        {
            try
            {
                List<string> LstAutoridadesSolicitud = new List<string>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTA_AUTORIDADES_SOLICITUD");
                db.AddInParameter(cmd, "IDProcessInstance", DbType.Int32, IdProcessInstace);
                db.AddInParameter(cmd, "IDfield", DbType.Int32, idField);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstAutoridadesSolicitud.Add(reader["AUT_NOMBRE"].ToString());
                    }
                }
                return LstAutoridadesSolicitud;

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public List<ComentarioActividadIdentity> ListaComentarioActividad(ComentarioActividadIdentity pComentarioActividadIdentity)
        {
            try
            {
                List<ComentarioActividadIdentity> LstComentariosActividad = new List<ComentarioActividadIdentity>();
                ComentarioActividadIdentity vComentarioActividadIdentity;
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTAR_PIN_COMETARIOS_ACTIVIDAD");
                db.AddInParameter(cmd, "p_IDProcessInstance", DbType.Int32, pComentarioActividadIdentity.IDProcessInstance);
                db.AddInParameter(cmd, "p_IDActivityInstance", DbType.Int32, pComentarioActividadIdentity.IDActivityInstance);
                db.AddInParameter(cmd, "p_IDActivity", DbType.Int32, pComentarioActividadIdentity.IDActivity);
                db.AddInParameter(cmd, "p_Usuario", DbType.String, pComentarioActividadIdentity.Usuario);
                db.AddInParameter(cmd, "p_ES_GERENTE", DbType.Boolean, pComentarioActividadIdentity.EsGerente);
                db.AddInParameter(cmd, "p_AUT_ID", DbType.Int32, pComentarioActividadIdentity.AutoridadId);
                db.AddInParameter(cmd, "p_IDACCION", DbType.Int32, pComentarioActividadIdentity.IDAccion);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        vComentarioActividadIdentity = new ComentarioActividadIdentity();
                        vComentarioActividadIdentity.IDComentario = Convert.ToInt32(reader["IDComentario"]);
                        vComentarioActividadIdentity.Comments = reader["comments"].ToString();
                        vComentarioActividadIdentity.Field = reader["Field"].ToString();
                        vComentarioActividadIdentity.FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]);
                        vComentarioActividadIdentity.ActividadRealizada = Convert.ToBoolean(reader["ActividadRealizada"]);
                        vComentarioActividadIdentity.IDProcessInstance = Convert.ToInt32(reader["IDProcessInstance"]);
                        vComentarioActividadIdentity.IDActivityInstance = Convert.ToInt32(reader["IDActivityInstance"]);
                        vComentarioActividadIdentity.IDActivity = Convert.ToInt32(reader["IDActivity"]);
                        vComentarioActividadIdentity.Usuario = reader["Usuario"].ToString();
                        if (reader["AUT_ID"].ToString() != string.Empty)
                            vComentarioActividadIdentity.AutoridadId = Convert.ToInt32(reader["AUT_ID"]);
                        vComentarioActividadIdentity.IDAccion = Convert.ToInt32(reader["IDACCION"]);
                        vComentarioActividadIdentity.EsGerente = Convert.ToBoolean(reader["ES_GERENTE"]);
                        if (reader["FECHA_COMPROMISO"].ToString() != string.Empty)
                            vComentarioActividadIdentity.FechaCompromiso = Convert.ToDateTime(reader["FECHA_COMPROMISO"]);
                        if (reader["FECHA_VENCIMIENTO"].ToString() != string.Empty)
                            vComentarioActividadIdentity.FechaVencimiento = Convert.ToDateTime(reader["FECHA_VENCIMIENTO"]);
                        if (reader["FECHA_PRORROGA"].ToString() != string.Empty)
                            vComentarioActividadIdentity.FechaProrroga = Convert.ToDateTime(reader["FECHA_PRORROGA"]);
                        vComentarioActividadIdentity.TieneProrroga = Convert.ToBoolean(reader["TIENE_PRORROGA"]);
                        vComentarioActividadIdentity.NombreAccion = reader["NOMBRE_ACCION"].ToString();
                        vComentarioActividadIdentity.DetieneFlujo = Convert.ToBoolean(reader["DETIENE_FLUJO"]);
                        vComentarioActividadIdentity.IDProgresoAccion = Convert.ToInt32(reader["IDPROGRESOACCION"]);
                        vComentarioActividadIdentity.NombreAutoridad = reader["NOMBRE_AUTORIDAD"].ToString();
                        LstComentariosActividad.Add(vComentarioActividadIdentity);
                    }
                }
                return LstComentariosActividad;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DataTable ConsultarVisorProceso(string nroVital, DateTime? fechaIni, DateTime? fechaFin, string departamento, int? autoridadId, string nombreProyecto, string solicitante)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTA_VISOR_PROCESO_PINES");
                db.AddInParameter(cmd, "P_SOL_NUMERO_SILPA", DbType.String, nroVital);
                db.AddInParameter(cmd, "P_FECHA_INICIO", DbType.DateTime, fechaIni);
                db.AddInParameter(cmd, "P_FECHA_FIN", DbType.DateTime, fechaFin);
                db.AddInParameter(cmd, "P_DEPARTAMENTO", DbType.String, departamento);
                db.AddInParameter(cmd, "P_AUT_ID", DbType.Int32, autoridadId);
                db.AddInParameter(cmd, "P_NOMBRE_PROYECTO", DbType.String, nombreProyecto);
                db.AddInParameter(cmd, "P_SOLICITANTE",DbType.String, solicitante);
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DataTable ConsultaAvanceEsperadoHoyProyecto(string nroVital, DateTime? fechaIni, DateTime? fechaFin, string departamento, int? autoridadId, string nombreProyecto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTA_AVANCE_ESPERADO_HOY_PROYECTO");
                db.AddInParameter(cmd, "P_SOL_NUMERO_SILPA", DbType.String, nroVital);
                db.AddInParameter(cmd, "P_FECHA_INICIO", DbType.DateTime, fechaIni);
                db.AddInParameter(cmd, "P_FECHA_FIN", DbType.DateTime, fechaFin);
                db.AddInParameter(cmd, "P_DEPARTAMENTO", DbType.String, departamento);
                db.AddInParameter(cmd, "P_AUT_ID", DbType.Int32, autoridadId);
                db.AddInParameter(cmd, "P_NOMBRE_PROYECTO", DbType.String, nombreProyecto);
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DataTable ParticipantesActividadPendiente(Int64? IdProcessInstance)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTA_RESPONSABLES_ACTIVIDAD_PENDIENTE_IDPROCESSINSTANCE");
                db.AddInParameter(cmd, "P_IDPROCESSINSTANCE", DbType.Int64, IdProcessInstance);
                if (db.ExecuteDataSet(cmd).Tables.Count > 0)
                    return db.ExecuteDataSet(cmd).Tables[0];
                else
                    return new DataTable();

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DataTable ConsultarDatosFormProcessInstance(Int64 IdProcessInstance)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTA_VALORES_IDENTRYDATA_BY_PROCESSINSTANCE");
                db.AddInParameter(cmd, "P_ID_PROCESSINSTANCE", DbType.Int64, IdProcessInstance);
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DataTable ConsultaAccionActividad(Int32 IDActivity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTA_ACCION_ACTIVIDAD_PINES");
                db.AddInParameter(cmd, "p_ID_ACTIVITY", DbType.Int32, IDActivity);
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DataTable ConsultaActividadesWorkFlowPINES()
        {
             try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTA_ACTIVIDADES_PROCESO_PINES");
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void InsertarActividadProcesoUrl(ActividadProcesoURL pActividadProcesoURL)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_INSERTAR_ACTIVIDAD_PROCESO_URL");
                db.AddInParameter(cmd, "P_IDPROCESSINSTANCE", DbType.Int32, pActividadProcesoURL.IdProcessInstance);
                db.AddInParameter(cmd, "P_IDACTIVITYINSSTANCE", DbType.Int32, pActividadProcesoURL.IdActivityInstance);
                db.AddInParameter(cmd, "P_USUARIO", DbType.String, pActividadProcesoURL.Usuario);
                db.AddInParameter(cmd, "P_URL_PROYECTO", DbType.String, pActividadProcesoURL.UrlProyecto);
                db.AddInParameter(cmd, "P_ES_VITAL", DbType.Boolean, pActividadProcesoURL.EsVITAL);
                db.AddInParameter(cmd, "P_NUMERO_VITAL", DbType.String, pActividadProcesoURL.NroVITAL);
                db.ExecuteNonQuery(cmd);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void ConsultarActividadProcesoUrl(ref ActividadProcesoURL pActividadProcesoURL)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTAR_ACTIVIDAD_PROCESO_URL");
                db.AddInParameter(cmd, "P_IDPROCESSINSTANCE", DbType.Int32, pActividadProcesoURL.IdProcessInstance);
                db.AddInParameter(cmd, "P_IDACTIVITYINSSTANCE", DbType.Int32, pActividadProcesoURL.IdActivityInstance);
                db.AddInParameter(cmd, "P_USUARIO", DbType.String, pActividadProcesoURL.Usuario);
                db.AddInParameter(cmd, "P_URL_PROYECTO", DbType.String, pActividadProcesoURL.UrlProyecto);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        pActividadProcesoURL.IdProcessInstance = Convert.ToInt32(reader["IDPROCESSINSTANCE"]);
                        pActividadProcesoURL.IdActivityInstance = Convert.ToInt32(reader["IDACTIVITYINSSTANCE"]);
                        pActividadProcesoURL.Usuario = reader["USUARIO"].ToString();
                        pActividadProcesoURL.UrlProyecto = reader["URL_PROYECTO"].ToString();
                        pActividadProcesoURL.EsVITAL = Convert.ToBoolean(reader["ES_VITAL"]);
                        pActividadProcesoURL.NroVITAL = reader["NUMERO_VITAL"].ToString();
                    }
                }
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public List<ActividadProcesoURL> ListaActividadProcesoUrl(ActividadProcesoURL pActividadProcesoURL)
        {
            try
            {
                List<ActividadProcesoURL> lstActividadProcesoURL = new List<ActividadProcesoURL>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_CONSULTAR_ACTIVIDAD_PROCESO_URL");
                db.AddInParameter(cmd, "P_IDPROCESSINSTANCE", DbType.Int32, pActividadProcesoURL.IdProcessInstance);
                db.AddInParameter(cmd, "P_IDACTIVITYINSSTANCE", DbType.Int32, pActividadProcesoURL.IdActivityInstance);
                db.AddInParameter(cmd, "P_USUARIO", DbType.String, pActividadProcesoURL.Usuario);
                db.AddInParameter(cmd, "P_URL_PROYECTO", DbType.String, pActividadProcesoURL.UrlProyecto);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ActividadProcesoURL vActividadProcesoURL = new ActividadProcesoURL();
                        vActividadProcesoURL.IdProcessInstance = Convert.ToInt32(reader["IDPROCESSINSTANCE"]);
                        vActividadProcesoURL.IdActivityInstance = Convert.ToInt32(reader["IDACTIVITYINSSTANCE"]);
                        vActividadProcesoURL.Usuario = reader["USUARIO"].ToString();
                        vActividadProcesoURL.UrlProyecto = reader["URL_PROYECTO"].ToString();
                        lstActividadProcesoURL.Add(vActividadProcesoURL);
                    }
                }
                return lstActividadProcesoURL;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DateTime CalcularFecha(DateTime vFechaInicial, int vDiasHabiles)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CALCULA_FECHA_FIN_DIAS_HABILES");
                db.AddInParameter(cmd, "P_FECHA_INICIAL", DbType.DateTime, vFechaInicial);
                db.AddInParameter(cmd, "P_DIAS_HABILES", DbType.Int32, vDiasHabiles);
                db.AddOutParameter(cmd, "P_FECHA_CALCULADA", DbType.DateTime, 15);
                db.ExecuteNonQuery(cmd);
                return Convert.ToDateTime(db.GetParameterValue(cmd, "@P_FECHA_CALCULADA"));
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DataTable AccionesNoUsadas(Int32 idActivity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_LISTA_ACCIONES_NO_ASOCIADAS_ACTIVIDAD");
                db.AddInParameter(cmd, "IDACTIVITY", DbType.Int32, idActivity);
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

    }
}

