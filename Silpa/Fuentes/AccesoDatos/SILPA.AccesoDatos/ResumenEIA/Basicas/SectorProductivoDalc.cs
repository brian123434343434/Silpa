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
    public class SectorProductivoDalc
    {
        private Configuracion objConfiguracion;

        public SectorProductivoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista los departamentos
        /// </summary>
        public List<SectorProductivoEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                SectorProductivoEntity _objDatos;
                List<SectorProductivoEntity> listaDatos = new List<SectorProductivoEntity>();

                DbCommand cmd = db.GetStoredProcCommand("EIP_LISTA_SECTOR_PRODUCTIVO");                
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new SectorProductivoEntity();
                    _objDatos["ESP_ID"] = dr["ESP_ID"].ToString();
                    _objDatos["ESP_SECTOR_PRODUCTIVO"] = dr["ESP_SECTOR_PRODUCTIVO"].ToString();
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
