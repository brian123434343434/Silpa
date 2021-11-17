using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data.SqlClient;
using SILPA.Comun;
using System.Data;

namespace SILPA.AccesoDatos.AdmTablasBasicas
{
    public class AutoridadAmbientalDALC
    {
        private Configuracion objConfiguracion;

        public AutoridadAmbientalDALC()
        {
            objConfiguracion = new Configuracion();
        }
        /// <summary>
        /// Eliminar la informacion de los tipos de falta
        /// </summary>
        public void Eliminar_EXT_Autoridad(int AUT_ID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                object[] parametros = new object[] { AUT_ID };
                DbCommand cmd = db.GetStoredProcCommand("SS_MC_DEL_EXT_AUTORIDAD1", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }


        /// <summary>
        /// Eliminar la informacion de los tipos de falta
        /// </summary>
        public DataSet ListarTodasAutoridadAmbiental()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AUTORIDAD_AMBIENTAL");
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return ds_datos;
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }
    }
}
