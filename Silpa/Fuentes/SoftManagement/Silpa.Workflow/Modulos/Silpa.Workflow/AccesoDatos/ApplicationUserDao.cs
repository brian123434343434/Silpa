using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Silpa.Workflow.Entidades;

namespace Silpa.Workflow.AccesoDatos
{
    public class ApplicationUserDao
    {
        public static long ObtenerIdUsuario(string usuario)
        {
            DataTable datos;
            Database db;
            DbCommand command;
            db = DatabaseFactory.CreateDatabase(ServicioWorkflowConfig.ConexionSilpa);
            command = db.GetStoredProcCommand("GEN_WORFLOW_APPLICATION_USER_CONSULTAR");
            db.AddInParameter(command, "P_USUARIO", DbType.String, usuario);
            datos= db.ExecuteDataSet(command).Tables[0];
            if (datos.Rows.Count == 0)
                throw new Exception("No existe el usuario " + usuario);
            else
                return long.Parse(datos.Rows[0]["ID"].ToString());
         
        }

        public static long ObtenerIdParticipante(string usuario)
        {
            DataTable datos;
            Database db;
            DbCommand command;
            db = DatabaseFactory.CreateDatabase(ServicioWorkflowConfig.ConexionSilpa);
            command = db.GetStoredProcCommand("GEN_WORFLOW_PARTICIPANT_CONSULTAR");
            db.AddInParameter(command, "P_USUARIO", DbType.String, usuario);
            datos = db.ExecuteDataSet(command).Tables[0];
            if (datos.Rows.Count == 0)
                throw new Exception("No existe el usuario " + usuario);
            else
                //return (long) datos.Rows[0]["ID"];
                return long.Parse(datos.Rows[0]["ID"].ToString());

        }
        public static Int32 ObtenerAplicationUser(string usuario)
        {
            DataTable datos;
            Database db;
            DbCommand command;
            db = DatabaseFactory.CreateDatabase(ServicioWorkflowConfig.ConexionSilpa);
            command = db.GetStoredProcCommand("GEN_WORFLOW_APLICATION_USER_ID_CONSULTAR");
            db.AddInParameter(command, "P_USUARIO", DbType.String, usuario);
            datos = db.ExecuteDataSet(command).Tables[0];
            if (datos.Rows.Count == 0)
                throw new Exception("No existe el usuario " + usuario);
            else
                //return (long) datos.Rows[0]["ID"];
                return Int32.Parse(datos.Rows[0]["ID"].ToString());

        }

        public static List<string> ObtenerEmpresaUsuario(string usuario)
        {
            
            Database db;
            DbCommand command;
            List<string> lstEmpresa = new List<string>();
            db = DatabaseFactory.CreateDatabase(ServicioWorkflowConfig.ConexionSilpa);
            command = db.GetStoredProcCommand("GEN_WORKFLOW_COMPANY_USUARIO");
            db.AddInParameter(command, "P_USUARIO", DbType.String, usuario);
            using (IDataReader reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    lstEmpresa.Add(reader["ShortName"].ToString());
                }
            }
            return lstEmpresa;
        }


        public static DatosUsuario ConsultarDatosUsuario(string usuario)
        {
            Database db;
            DbCommand command;
            db = DatabaseFactory.CreateDatabase(ServicioWorkflowConfig.ConexionSilpa);
            command = db.GetStoredProcCommand("GEN_DATOS_USUARIO_CONSULTAR");
            db.AddInParameter(command, "P_USUARIO", DbType.String, usuario);
            db.AddOutParameter(command, "P_NOMBRE_USUARIO", DbType.String, 140);
            db.AddOutParameter(command, "P_ULTIMO_LOGIN", DbType.String, 100);
            db.AddOutParameter(command, "P_GRUPOS_USUARIO", DbType.String, 100);
            db.ExecuteNonQuery(command);

            return new DatosUsuario(db.GetParameterValue(command, "P_NOMBRE_USUARIO").ToString(), db.GetParameterValue(command, "P_ULTIMO_LOGIN").ToString(), db.GetParameterValue(command, "P_GRUPOS_USUARIO").ToString());
        
        }
    }
}
