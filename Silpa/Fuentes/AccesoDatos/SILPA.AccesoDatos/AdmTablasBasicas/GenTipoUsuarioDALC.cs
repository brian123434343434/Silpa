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
    public class GenTipoUsuarioDALC
    {
        private Configuracion objConfiguracion;

        public GenTipoUsuarioDALC()
        {
            objConfiguracion = new Configuracion();
        }
        #region metodos
        /// <summary>
        /// Lista la informacion de los tipos de tramite
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable ListarTipoUsuarioDALC(int idTipoUsuario, int  idUserGroup)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idTipoUsuario, idUserGroup };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_USUARIOS");
            DataSet ds_datos = new DataSet();
            ds_datos = db.ExecuteDataSet(cmd);
            return (ds_datos.Tables[0]);
        }

        public DataTable ListarTipoUsuarioDALC(string strNombre)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { strNombre };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_TIPO_USUARIOS", parametros);
            DataSet ds_datos = new DataSet();
            ds_datos = db.ExecuteDataSet(cmd);
            return (ds_datos.Tables[0]);
        }

        public DataTable ValidarTipoUsuarioDALC(int intIdPar, string strNombre)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intIdPar,strNombre };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_GEN_TIPO_USUARIOS", parametros);
            DataSet ds_datos = new DataSet();
            ds_datos = db.ExecuteDataSet(cmd);
            return (ds_datos.Tables[0]);
        }


        /// <summary>
        /// Actualizar los tipo de tramite
        /// </summary>
        public void ActualizarTipoUsuarioDALC(int intId, int intIdPar, string strNombre)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { strNombre, intIdPar, intId };
            DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_GEN_TIPO_USUARIO", parametros);
            db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// Registra la descripcion de los tipo de usuario
        /// </summary>
        /// <param name="intId"></param>
        public void InsertarTipoUsuarioDALC(int intIdPar, string strNombre)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intIdPar,strNombre };
            DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_GEN_TIPO_USUARIO", parametros);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Eliminar la informacion de los tipos de tramite
        /// </summary>
        public void EliminarTipoUsuarioDALC(int intId)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intId };
            DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_GEN_TIPO_USUARIO", parametros);
            db.ExecuteNonQuery(cmd);
        }

        #endregion
    }

    
}
