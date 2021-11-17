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
    public class UserGroupDALC
    {
        private Configuracion objConfiguracion;


        public UserGroupDALC()
        {
            objConfiguracion = new Configuracion();
        }
        #region metodos
        /// <summary>
        /// Lista la informacion de los tipos de usuario grupo
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable ListarUserGroupDALC(int idUserGroup)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idUserGroup };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_USER_GROUP");
            DataSet ds_datos = new DataSet();
            ds_datos = db.ExecuteDataSet(cmd);
            return (ds_datos.Tables[0]);
        }        
#endregion
    }
}
