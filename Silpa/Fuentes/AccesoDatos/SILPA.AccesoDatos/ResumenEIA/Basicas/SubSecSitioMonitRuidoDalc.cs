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
    public class SubSecSitioMonitRuidoDalc
    {
        private Configuracion objConfiguracion;

        public SubSecSitioMonitRuidoDalc()
        {
            objConfiguracion = new Configuracion();
        }


        public List<SubSecSitioMonitRuidoEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                SubSecSitioMonitRuidoEntity _objDatos;
                List<SubSecSitioMonitRuidoEntity> listaDatos = new List<SubSecSitioMonitRuidoEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_SUBSECTOR_SITIO_MONIT_RUIDO");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new SubSecSitioMonitRuidoEntity();
                    _objDatos["ESS_ID"] = dr["ESS_ID"].ToString();
                    _objDatos["ESS_SUBSECTOR_SITIO_MONIT"] = dr["ESS_SUBSECTOR_SITIO_MONIT"].ToString();
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
