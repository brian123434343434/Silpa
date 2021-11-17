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
    public class GenTipoUsuarioGrupoDALC
    {
        private Configuracion objConfiguracion;


        public GenTipoUsuarioGrupoDALC()
        {
            objConfiguracion = new Configuracion();
        }
        #region metodos
        /// <summary>
        /// Lista la informacion de los tipos de usuario grupo
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable ListarTipoUsuarioGrupo(int tipoUsuario, int userGroup)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { tipoUsuario, userGroup };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_TIPO_USUARIO_GRUPO",parametros);
            DataSet ds_datos = new DataSet();
            ds_datos = db.ExecuteDataSet(cmd);
            return (ds_datos.Tables[0]);
        }

        /// <summary>
        
        /// Eliminar la informacion de los tipos de Usuario Grupo
        /// </summary>
        public void EliminarTipoUsuarioGrupo(int strIdTipoUsuario, int strIdUserGroup)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { strIdTipoUsuario, strIdUserGroup };
            DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_TIPO_USUARIO_GRUPO", parametros);
            db.ExecuteNonQuery(cmd);
        }

        public void ActualizarTipoUsuarioGrupo(int intIdTipoUsuarioNvo, int intIdUserGroupNvo, int intIdTipoUsuarioAnt, int intIdUserGroupAnt)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intIdTipoUsuarioNvo, intIdUserGroupNvo, intIdTipoUsuarioAnt, intIdUserGroupAnt };
            DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_TIPO_USUARIO_GRUPO", parametros);
            db.ExecuteNonQuery(cmd);
        }

        public void InsertarTipoUsuarioGrupo(int intIdTipoUsuario, int intIdUserGroup)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intIdTipoUsuario, intIdUserGroup };
            DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_TIPO_USUARIO_GRUPO", parametros);
            db.ExecuteNonQuery(cmd);
        }
        #endregion
    }
}