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
using System.Linq;

namespace SILPA.AccesoDatos.Generico
{
    public class NumeroSilpaDalc
    {
        private Configuracion objConfiguracion;

        public NumeroSilpaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public string NumeroSilpa(int idInstancia)
        {
            string salida = "";
            SqlDatabase db  = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetSqlStringCommand("SELECT dbo.F_NUMERO_VITAL_CREADO("+ idInstancia.ToString()+")");
            salida = Convert.ToString(db.ExecuteScalar(cmd));
            cmd.Dispose();
            return salida;
        }

        public DataTable ConsultarExpediente(string numeroSilpa)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("GEN_SOLICITUD_CONSULTAR_EXPEDIENTE");
            db.AddInParameter(cmd, "P_SOL_NUMERO_SILPA", DbType.String, numeroSilpa);
            DataTable dtExpedientes = db.ExecuteDataSet(cmd).Tables[0];
            if (dtExpedientes.AsEnumerable().Where(x => x.Field<string>("LOCALIZACION").ToString() != string.Empty).Count() > 0)
            {
                return dtExpedientes.AsEnumerable().Where(x => x.Field<string>("LOCALIZACION").ToString() != string.Empty).CopyToDataTable();
            }
            return db.ExecuteDataSet(cmd).Tables[0];

        }


        public DataTable ConsultarExpedienteXCodExo(string codigo_exp)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("GEN_SOLICITUD_CONSULTAR_EXPEDIENTE_X_COD_EXP");
            db.AddInParameter(cmd, "@P_EXP_NUM", DbType.String, codigo_exp);
            return db.ExecuteDataSet(cmd).Tables[0];

        }


        public DataSet ConsultarCoordenadas(string numeroVital,string codigoExpediente)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("GEN_SOLICITUD_CONSULTAR_COORDENADAS");
            db.AddInParameter(cmd, "P_NUMERO_VITAL", DbType.String, numeroVital);
            db.AddInParameter(cmd, "P_CODIGO_EXPEDIENTE", DbType.String, codigoExpediente);
            return db.ExecuteDataSet(cmd);
        }

        


        public string NumeroInstancia(string numeroSilpa)
        {
            string salida = "";
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetSqlStringCommand("SELECT dbo.F_NUMERO_PROCESO('" + numeroSilpa + "')");
            salida = Convert.ToString(db.ExecuteScalar(cmd));
            return salida;
        }


        /// <summary>
        /// Obtiene el número ProcessInstance a partir del número vital completo
        /// </summary>
        /// <param name="numeroVitalCompleto">string: número vital completo</param>
        /// <returns>long : idProcessInstance</returns>
        public string ProcessInstance(string numeroVitalCompleto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { numeroVitalCompleto, string.Empty };
                DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_PROCESSINSTANCE", parametros);
                int i = db.ExecuteNonQuery(cmd);
                string resultado = db.GetParameterValue(cmd, "@SOL_ID_PROCESSINSTANCE").ToString();
                cmd.Dispose();
                return resultado;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al obtener el número ProcessInstance a partir del número VITAL completo.";
                throw new Exception(strException, ex);
            }
        }

        /// <summary>
        /// sobre carga del metodo para permitir hacer la busqueda por ambos códigos
        /// </summary>
        /// <param name="numeroVitalCompleto"></param>
        /// <param name="numeroSilpa"></param>
        /// <returns></returns>
        public string ProcessInstance(string numeroVitalCompleto, string numeroSilpa)
        {
            if (String.IsNullOrEmpty(numeroSilpa)) numeroSilpa = string.Empty;
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numeroVitalCompleto, numeroSilpa };
            DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_PROCESSINSTANCE_POR_SILPA_VITAL", parametros);
            int i = db.ExecuteNonQuery(cmd);
            string resultado = db.GetParameterValue(cmd, "@SOL_ID_PROCESSINSTANCE").ToString();
            cmd.Dispose();
            return resultado;
        }
        /// <summary>
        /// Genera mensaje de solicitud recibida
        /// </summary>
        /// <param name="numeroVITAL">Nro VITAL</param>
        /// <returns></returns>
        public string mensajeSolicitudRecibida(string numeroVITAL)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numeroVITAL, string.Empty };
            DbCommand cmd = db.GetStoredProcCommand("GEN_MENSAJE_SOLICITUD_RECIBIDA", parametros);
            int i = db.ExecuteNonQuery(cmd);
            string resultado = db.GetParameterValue(cmd, "@V_MENSAJE_SOLICITUD_RECIBIDA").ToString();
            cmd.Dispose();
            return resultado;
        }

        /// <summary>
        ///  Genera mensaje de solicitud recibida POR PROCESS INSTANCE
        /// </summary>
        /// <param name="IdProcessInstance"></param>
        /// <returns></returns>
        public string mensajeSolicitudRecibida(int IdProcessInstance, out string str_NumeroVital)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { IdProcessInstance, string.Empty, string.Empty };
            DbCommand cmd = db.GetStoredProcCommand("GEN_MENSAJE_SOL_RAD_RECIBIDA", parametros);
            int i = db.ExecuteNonQuery(cmd);
            string resultado = db.GetParameterValue(cmd, "@V_MENSAJE_SOLICITUD_RECIBIDA").ToString();
            str_NumeroVital = db.GetParameterValue(cmd, "@V_NUMERO_VITAL_OUT").ToString();
            cmd.Dispose();
            return resultado;
        }


        public string mensajeSolicitudRadicada(int id)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { id, string.Empty };
            DbCommand cmd = db.GetStoredProcCommand("GEN_MENSAJE_SOLICITUD_RADICADA", parametros);
            int i = db.ExecuteNonQuery(cmd);
            string resultado = db.GetParameterValue(cmd, "@V_MENSAJE_SOLICITUD_RADICADA").ToString();
            cmd.Dispose();
            return resultado;
        }


    }
}


