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
    public class EstrucVertDomDalc
    {
        private Configuracion objConfiguracion;

        public EstrucVertDomDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<EstrucVertDomEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                EstrucVertDomEntity _objDatos;
                List<EstrucVertDomEntity> listaDatos = new List<EstrucVertDomEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_TIPO_ESTRUC_VERT_DOM_ECOTERR");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new EstrucVertDomEntity();
                    _objDatos["ETE_ID"] = dr["ETE_ID"].ToString();
                    _objDatos["ETE_TIPO_ESTRUC_VERT_DOM"] = dr["ETE_TIPO_ESTRUC_VERT_DOM"].ToString();
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
