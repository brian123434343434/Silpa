using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.RUIA
{
    public class TipoSancionDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicaci�n
        /// </summary>
        private Configuracion objConfiguracion;

        public TipoSancionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// M�todo que lista los tipos de Sanci�n
        /// </summary>
        /// <returns>Dataset con los siguientes campos [ID_TIPO_SANCION] - [NOMBRE_TIPO_SANCION]</returns>
        public DataSet ListaTipoSancion()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { };
                DbCommand cmd = db.GetStoredProcCommand("RUH_LISTA_TIPO_SANCION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

    }
}
