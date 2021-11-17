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

namespace SILPA.AccesoDatos.AdmTablasBasicas
{
    public class GenParticipantDALC
    {
        private Configuracion objConfiguracion;

        public GenParticipantDALC() {
            objConfiguracion = new Configuracion();
        }

        #region metodos

        /// <summary>
        /// Lista la informacion de los participantes permitidos para cargar
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable ListarParticipantes()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            DbCommand cmd = db.GetStoredProcCommand("[BAS_LISTAR_GEN_PARTICIPANTES]");
            DataSet ds_datos = new DataSet();
            ds_datos = db.ExecuteDataSet(cmd);
            return (ds_datos.Tables[0]);
        }
        #endregion
    }
}
