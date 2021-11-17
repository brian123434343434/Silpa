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
    public class UnidadPolAdminDalc
    {
        private Configuracion objConfiguracion;

        public UnidadPolAdminDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<UnidadPolAdminEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                UnidadPolAdminEntity _objDatos;
                List<UnidadPolAdminEntity> listaDatos = new List<UnidadPolAdminEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_UNIDAD_POL_ADMIN");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new UnidadPolAdminEntity();
                    _objDatos["EPA_ID"] = dr["EPA_ID"].ToString();
                    _objDatos["EPA_UNIDAD_POL_ADMIN"] = dr["EPA_UNIDAD_POL_ADMIN"].ToString();
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
