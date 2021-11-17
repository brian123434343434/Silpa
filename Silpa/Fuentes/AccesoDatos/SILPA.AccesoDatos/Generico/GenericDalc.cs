using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Configuration;
using System.Configuration;
using System.Data.Common;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    
    public static class GenericDalc
    {
        public static SqlDatabase DbSilpa;
        public static SqlDatabase DbSila;
        public static DbCommand cmd;

        /// <summary>
        /// Bases de datos del BPM
        /// </summary>
        public static SqlDatabase DbSecurity;
        public static SqlDatabase DbWorkflow;
        public static SqlDatabase DFormBuilder;
        
        private static Configuracion objConfiguracion;

        
        /// <summary>
        /// Método que inicializa las conexiones con las bases de datos
        /// </summary>
        public static void Init() 
        {
            /// Conexiones Sila  - Silpa
            SqlDatabase DbSilpa = new SqlDatabase(objConfiguracion.SilpaCnx);
            SqlDatabase DbSila = new SqlDatabase(objConfiguracion.SilaCnx);

            /// Conexiones BPM 
            SqlDatabase DbSecurity = new SqlDatabase(objConfiguracion.SecurityCnx);
            SqlDatabase DbWorkflow = new SqlDatabase(objConfiguracion.WorkFlowCnx);
            SqlDatabase DFormBuilder = new SqlDatabase(objConfiguracion.FormBuilderCnx);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataSet Procedimiento(string strProcedimiento, object[] objParams) 
        {
            GenericDalc.Init();
            cmd = DbSilpa.GetStoredProcCommand(strProcedimiento, objParams);
            return DbSilpa.ExecuteDataSet(cmd);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataSet ProcedimientoSila(string strProcedimiento, object[] objParams)
        {
            GenericDalc.Init();
            cmd = DbSila.GetStoredProcCommand(strProcedimiento, objParams);
            return DbSila.ExecuteDataSet(cmd);
        }

        
        public static object ProcedimientoSilpa(string strProcedimiento, object[] objParams)
        {
            GenericDalc.Init();
            cmd = DbSilpa.GetStoredProcCommand(strProcedimiento, objParams);
            return DbSilpa.ExecuteDataSet(cmd);
        }


        //public static void ProcedimientoSilpa(string strProcedimiento, object[] objParams)
        //{
        //    GenericDalc.Init();
        //    cmd = DbSilpa.GetStoredProcCommand(strProcedimiento, objParams);
        //    DbSilpa.ExecuteDataSet(cmd);
        //}


    }
}
