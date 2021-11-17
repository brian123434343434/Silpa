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

namespace SILPA.AccesoDatos.ReporteTramite
{
    public  class ReporteTramiteDalc
    {

         /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public ReporteTramiteDalc()
        {
            objConfiguracion = new Configuracion();
        }


        public DataSet ListarReporteTramite(Nullable<DateTime> dateFechaInicial, 
            Nullable<DateTime> dateFechaFinal,Nullable<int> intTipoTramite,
            Nullable<int> intAutoridadAmbiental, Nullable<int> intIdProcessInstance)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { dateFechaInicial, dateFechaFinal, intTipoTramite, intAutoridadAmbiental, intIdProcessInstance };
                DbCommand cmd = db.GetStoredProcCommand("SS_CON_REPORTE_TRAMITE", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool CrearRelacionExpedienteExpediente(string expedienteId, string expedienteIdRef,string numeroVital)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { expedienteId,expedienteIdRef,numeroVital };
                DbCommand cmd = db.GetStoredProcCommand("SIH_INSERT_MIS_TRAMITES_EXPEDIENTEXEXPEDIENTE", parametros);
                db.ExecuteScalar(cmd);
                return true;

            }
            catch (SqlException ex)
            {
                SMLog.Escribir(Severidad.Critico, ex.ToString());
                return false;
            }
        }

        public bool EliminarRelacionExpedienteExpediente(string expedienteId, string expedienteIdRef)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { expedienteId, expedienteIdRef };
                DbCommand cmd = db.GetStoredProcCommand("SIH_DELETE_MIS_TRAMITES_EXPEDIENTEXEXPEDIENTE", parametros);
                db.ExecuteScalar(cmd);
                return true;

            }
            catch (SqlException ex)
            {
                SMLog.Escribir(Severidad.Critico, ex.ToString());
                return false;
            }
        }

        public bool CrearMisTramites(string numeroVital, DateTime fechaCreacion, string descripcion, string pathDocumento, string idExpediente, string etaNombre, int actReposicion, string addNombre,int? actoEjec,int? actoNot,string tipoDocumento,string descripcionActo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { numeroVital, fechaCreacion,descripcion, pathDocumento,idExpediente,etaNombre,actReposicion,addNombre,actoEjec,actoNot,tipoDocumento,descripcionActo};
                DbCommand cmd = db.GetStoredProcCommand("SIH_INSERTAR_MIS_TRAMITES", parametros);
                db.ExecuteScalar(cmd);
                
                return true;

            }
            catch (SqlException ex)
            {
                SMLog.Escribir(Severidad.Critico, ex.ToString());
                return false;
            }
        }


        /// <summary>
        /// Modificar la informacion de un expediente relacionado a un tramite
        /// </summary>
        /// <param name="p_strExpedienteAnteriorID">string con el identificador del expediente anterior</param>
        /// <param name="p_strExpedienteID">string con el identificador del expediente</param>
        /// <param name="p_intNumeroDocumento">int con el numero de documento</param>
        /// <param name="p_strTipoActo">string con el identificador del tipo de acto administrativo</param>
        /// <param name="strDescripcion">string con la descripcion</param>
        /// <param name="p_strEtapa">string con la etapa</param>
        /// <param name="p_strNombre">string con el nombre</param>
        /// <param name="p_strDescripcionActo">string con la descricion del acto administrativo</param>
        /// <returns>bool indicando si se realizo el tramite</returns>
        public bool ModificarDatosExpedienteTramite(string p_strExpedienteAnteriorID, string p_strExpedienteID, int p_intNumeroDocumento, string p_strTipoActo, string strDescripcion, string p_strEtapa, string p_strNombre, string p_strDescripcionActo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { p_strExpedienteAnteriorID, p_strExpedienteID, p_intNumeroDocumento, p_strTipoActo, strDescripcion, p_strEtapa, p_strNombre, p_strDescripcionActo };
                DbCommand cmd = db.GetStoredProcCommand("SIH_MODIFICAR_EXPEDIENTE_MIS_TRAMITES", parametros);
                db.ExecuteScalar(cmd);

                return true;

            }
            catch (SqlException ex)
            {
                SMLog.Escribir(Severidad.Critico, ex.ToString());
                return false;
            }
        }


        public DataTable Tramites()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());                
                DbCommand cmd = db.GetStoredProcCommand("SS_CON_TRAMITE");
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable Actividades(int traId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { traId };
                DbCommand cmd = db.GetStoredProcCommand("SS_CON_ACTIVIDAD", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertarRelacion(int traId,string nombre, int diaLimite, string caracter,  int actId, int docId,  string colorLetra, bool negrilla, string colorFondo, string tipoCorreo, string idSolicitante, string correoElectronico,int diasInicoAlerta)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {  traId, nombre, diaLimite,caracter,actId,docId,colorLetra,negrilla,colorFondo,tipoCorreo,idSolicitante,correoElectronico,diasInicoAlerta};
                DbCommand cmd = db.GetStoredProcCommand("SS_INSERT_RELACION_ALERTA", parametros);
                db.ExecuteNonQuery(cmd);
                

            }
            catch (SqlException ex)
            {
                SMLog.Escribir(Severidad.Critico, ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public DataTable Documentos(int actId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { actId };
                DbCommand cmd = db.GetStoredProcCommand("SS_CON_DOCUMENTOS_SILA", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ReporteXTramite(int traId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { traId };
                DbCommand cmd = db.GetStoredProcCommand("SS_CONS_RELACION_ALERTA", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarRelacionAlerta(int traId, int actId, int docId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {  traId,actId,docId};
                DbCommand cmd = db.GetStoredProcCommand("SS_DEL_RELACION_ALERTA", parametros);
                db.ExecuteNonQuery(cmd);
                

            }
            catch (SqlException ex)
            {
                SMLog.Escribir(Severidad.Critico, ex.ToString());
                throw new Exception(ex.Message);
            }
        }



        public DataSet ConsultarFechasCalendatios(string numeroVital)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { numeroVital };
                DbCommand cmd = db.GetStoredProcCommand("SS_CON_RELACION_ALERTA_CALENDARIOS", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string CodigoCondicion(int codId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { codId };
                DbCommand cmd = db.GetStoredProcCommand("SS_CON_CODIGO_CONDICION", parametros);
                string res = "";
                res = db.ExecuteScalar(cmd).ToString();
                return res;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ConsultarCondicionesSinActividades()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());                
                DbCommand cmd = db.GetStoredProcCommand("SS_CON_CODIGO_CONDICION_SIN_ACTIVIDAD");
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
