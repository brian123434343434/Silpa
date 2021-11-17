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
    public class DepartamentoDalc
    {
        private Configuracion objConfiguracion;

        public DepartamentoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista los departamentos
        /// </summary>
        public List<DepartamentoEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DepartamentoEntity _objDatos;
                List<DepartamentoEntity> listaDatos = new List<DepartamentoEntity>();
                /*
                 * Se pasan parámetros nulos para obtener todos los departamentos
                 */
                object[] parametros = new object[] { null, null };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DEPARTAMENTO", parametros);                
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new DepartamentoEntity();
                    _objDatos["DEP_ID"] = dr["DEP_ID"].ToString();
                    _objDatos["DEP_NOMBRE"] = dr["DEP_NOMBRE"].ToString();
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
