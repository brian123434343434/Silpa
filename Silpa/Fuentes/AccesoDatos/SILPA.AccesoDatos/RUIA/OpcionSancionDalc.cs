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
    public class OpcionSancionDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public OpcionSancionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Método que lista las opciones de un Tipo de Sanción de RUIA
        /// </summary>
        /// <returns>Dataset con los siguientes campos [ID_OPCION_SANCION] - [NOMBRE_OPCION_SANCION]</returns>
        public DataSet ListaOpcionSancion()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { };
                DbCommand cmd = db.GetStoredProcCommand("RUH_LISTA_OPCION_SANCION", parametros);
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
