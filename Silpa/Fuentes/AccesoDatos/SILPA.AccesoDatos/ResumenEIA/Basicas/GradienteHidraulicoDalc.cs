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
    public class GradienteHidraulicoDalc
    {
        private Configuracion objConfiguracion;

        public GradienteHidraulicoDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<GradienteHidraulicoEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                GradienteHidraulicoEntity _objDatos;
                List<GradienteHidraulicoEntity> listaDatos = new List<GradienteHidraulicoEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_LISTA_GRADIENTE_HIDRAULICO");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new GradienteHidraulicoEntity();
                    _objDatos["EGH_ID"] = dr["EGH_ID"].ToString();
                    _objDatos["EGH_TIPO_GRADIENTE_HIDRA"] = dr["EGH_TIPO_GRADIENTE_HIDRA"].ToString();
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
