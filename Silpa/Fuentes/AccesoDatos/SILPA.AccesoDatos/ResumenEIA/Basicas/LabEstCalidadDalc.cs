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
    public class LabEstCalidadDalc
    {
        private Configuracion objConfiguracion;

        public LabEstCalidadDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<LabEstCalidadEntity> Listar(int? idTipoLab)
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                LabEstCalidadEntity _objDatos;
                List<LabEstCalidadEntity> listaDatos = new List<LabEstCalidadEntity>();
                /*
                * Se pasan parámetros nulos para obtener todos los departamentos
                */
                object[] parametros = new object[] { idTipoLab};
                DbCommand cmd = db.GetStoredProcCommand("EIP_LISTA_LABORATORIO_EST_CAL", parametros);  
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new LabEstCalidadEntity();
                    _objDatos["ELC_ID"] = dr["ELC_ID"].ToString();
                    _objDatos["ELC_LABORATORIO_EST_CALIDAD"] = dr["ELC_LABORATORIO_EST_CALIDAD"].ToString();
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
