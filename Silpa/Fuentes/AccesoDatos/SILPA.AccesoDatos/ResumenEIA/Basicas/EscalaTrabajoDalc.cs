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

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class EscalaTrabajoDalc
    {
        private Configuracion objConfiguracion;

        public EscalaTrabajoDalc()
        {
            objConfiguracion = new Configuracion();
        }


        public List<EscalaTrabajoEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                EscalaTrabajoEntity _objDatos;
                List<EscalaTrabajoEntity> listaDatos = new List<EscalaTrabajoEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_ESCALA_TRABAJO");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new EscalaTrabajoEntity();
                    _objDatos["EET_ID"] = dr["EET_ID"].ToString();
                    _objDatos["EET_ESCALA_TRABAJO"] = dr["EET_ESCALA_TRABAJO"].ToString();
                    listaDatos.Add(_objDatos);
                }
                return listaDatos;
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
    }
}
