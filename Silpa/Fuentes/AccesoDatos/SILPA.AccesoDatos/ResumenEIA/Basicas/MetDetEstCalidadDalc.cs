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
    public class MetDetEstCalidadDalc
    {
        private Configuracion objConfiguracion;

        public MetDetEstCalidadDalc()
        {
            objConfiguracion = new Configuracion();
        }


        public List<MetDetEstCalidadEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                MetDetEstCalidadEntity _objDatos;
                List<MetDetEstCalidadEntity> listaDatos = new List<MetDetEstCalidadEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_METODOS_DET_CALIDAD");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new MetDetEstCalidadEntity();
                    _objDatos["EMC_ID"] = dr["EMC_ID"].ToString();
                    _objDatos["EMC_METODOS_DET_CALIDAD"] = dr["EMC_METODOS_DET_CALIDAD"].ToString();
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
