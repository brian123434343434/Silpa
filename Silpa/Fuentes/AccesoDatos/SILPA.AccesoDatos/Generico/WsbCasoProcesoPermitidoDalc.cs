using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.SqlClient;



namespace SILPA.AccesoDatos.Generico
{
    public class WsbCasoProcesoPermitidoDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public WsbCasoProcesoPermitidoDalc()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Lista los Web Services Caso Proceso Permitido Dalc en la BD o uno en particular.
        /// </summary>
        /// <param name="intId" >Valor del identificador del caso proceso, se le valor es null lista todos</param>
        /// <returns>DataSet con los registros y las siguientes columnas: PRO_ID_CASO_PROCESO, PRO_FECHA_REGISTRO ,PRO_ACTIVO/returns>
        public DataSet ListarTipoEstadoProceso(Nullable<int> intId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId };
                DbCommand cmd = db.GetStoredProcCommand("WSB_LISTA_CASO_PROCESO", parametros);
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
