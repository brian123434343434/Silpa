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
    public class PeriodoMuestraDalc
    {
        private Configuracion objConfiguracion;

        public PeriodoMuestraDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<PeriodoMuestraEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                PeriodoMuestraEntity _objDatos;
                List<PeriodoMuestraEntity> listaDatos = new List<PeriodoMuestraEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_LISTA_PER_MUESTREO");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new PeriodoMuestraEntity();
                    _objDatos["EPC_ID"] = dr["ECM_ID"].ToString();
                    _objDatos["ECM_PERIODO_CLIMAT_EST_CAL"] = dr["ECM_PERIODO_CLIMAT_EST_CAL"].ToString();
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
