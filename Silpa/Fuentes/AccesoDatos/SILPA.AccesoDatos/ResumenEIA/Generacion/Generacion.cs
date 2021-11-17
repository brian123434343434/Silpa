using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.Common;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.ResumenEIA.Generacion
{
    public class Generacion
    {
        Configuracion objConfiguracion;
        public Generacion()
        {
            objConfiguracion = new Configuracion();
        }

        public string NumeroVitalEIA(int IdProyecto, string NitCompanhia)
        {
            SqlDatabase db = null;
            DbCommand cmd = null;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                cmd = db.GetSqlStringCommand("SELECT DBO.F_GENERA_NUMERO_VITAL_EIA(" + IdProyecto.ToString() +  ",'" + NitCompanhia + "')");
                return cmd.ExecuteScalar().ToString();  
            }
            finally
            {
                if(db!=null)
                  db = null;
                if(cmd==null)
                    cmd.Dispose();
                cmd = null;
            }
            
        }

        public string GenerarProyectoEIA()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                string IdProyecto = "";
                DbCommand cmd = db.GetStoredProcCommand("EIP_NUEVO_PROYECTO_EIA");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    IdProyecto = dr["IdProyecto"].ToString();
                    
                }
                return IdProyecto;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
            finally
            {
                dr = null;
                db = null;
            }

        }

        public string GenerarNumeroVitalProyectoEIA(int idSolicitante, int idTramite)
        {
            objConfiguracion = new Configuracion();      
         
            try
            {             
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { idSolicitante, idTramite };
                DbCommand cmd = db.GetStoredProcCommand("EIP_NUEVO_NUMERO_VITAL_PROYECTO_EIA", parametros);
                string strResultado = db.ExecuteScalar(cmd).ToString();
                return (strResultado);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }           
        }
    }
}
