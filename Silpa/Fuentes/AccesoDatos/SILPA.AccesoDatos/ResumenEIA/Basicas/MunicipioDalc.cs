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
    public class MunicipioDalc
    {
        private Configuracion objConfiguracion;

        public MunicipioDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista los departamentos
        /// </summary>
        public List<MunicipioEntity> Listar(int idDepto)
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                MunicipioEntity _objDatos;
                List<MunicipioEntity> listaDatos = new List<MunicipioEntity>();
                /*
                 * Se pasan parámetros nulos para obtener todos los departamentos
                 */
                object[] parametros = new object[] { null, idDepto ,null };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_MUNICIPIO", parametros);                
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new MunicipioEntity();
                    _objDatos["MUN_ID"] = dr["MUN_ID"].ToString();
                    _objDatos["MUN_NOMBRE"] = dr["MUN_NOMBRE"].ToString();
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
