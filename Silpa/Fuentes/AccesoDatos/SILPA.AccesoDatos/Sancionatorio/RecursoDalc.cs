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

namespace SILPA.AccesoDatos.Sancionatorio
{
    public class RecursoDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public RecursoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Método que retorna un listado de los recursos sancionatorios
        /// </summary>
        /// <param name="_idRecurso">Identificador del recurso sancionatorio</param>
        /// <returns>Conjunto de Datos: [REC_ID_RECURSO] - [REC_NOMBRE]</returns>
        public DataSet ListarRecursos(Nullable<int> _idRecurso)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { _idRecurso };
                DbCommand cmd = db.GetStoredProcCommand("SAN_LISTA_RECURSO", parametros);
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
