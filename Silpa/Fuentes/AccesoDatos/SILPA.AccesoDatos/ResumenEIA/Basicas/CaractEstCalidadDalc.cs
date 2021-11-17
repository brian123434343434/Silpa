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
    public class CaractEstCalidadDalc
    {
        private Configuracion objConfiguracion;

        public CaractEstCalidadDalc()
        {
            objConfiguracion = new Configuracion();
        }

        
        public List<CaractEstCalidadEntity> Listar(int? idTipoCalc)
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                CaractEstCalidadEntity _objDatos;
                List<CaractEstCalidadEntity> listaDatos = new List<CaractEstCalidadEntity>();
                /*
                 * Se pasan parámetros nulos para obtener todos los departamentos
                 */
                object[] parametros = new object[] { idTipoCalc };
                DbCommand cmd = db.GetStoredProcCommand("EIP_LISTA_PARAM_EST_CAL", parametros);
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new CaractEstCalidadEntity();
                    _objDatos["EPC_ID"] = dr["EPC_ID"].ToString();
                    _objDatos["EPC_PARAMETROS_EST_CALIDAD"] = dr["EPC_PARAMETROS_EST_CALIDAD"].ToString();
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
