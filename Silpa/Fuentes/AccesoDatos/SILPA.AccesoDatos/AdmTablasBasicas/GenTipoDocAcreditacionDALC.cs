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
    public class GenTipoDocAcreditacionDALC
    {
        private Configuracion objConfiguracion;

        public GenTipoDocAcreditacionDALC() {
            objConfiguracion = new Configuracion();
        }

         #region metodos
        /// <summary>
        /// Lista la informacion de los tipos de documento acreditacion
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable ListarTipoDocAcreditacion(string strNombre)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] {strNombre};
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_TIPO_DOCUMENTO_ACREDITACION", parametros);
            DataSet ds_datos = new DataSet();
            ds_datos = db.ExecuteDataSet(cmd);
            return (ds_datos.Tables[0]);
        }

        /// <summary>
        /// Registra la descripcion de los tipo de documento de acreditacion
        /// </summary>
        /// <param name="intId"></param>
        public void InsertarTipoDocAcreditacion(string strNombre)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] {strNombre};
            DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_TIPO_DOCUMENTO_ACREDITACION", parametros);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Actualizar los tipo de documentos de acreditacion
        /// </summary>
        public void ActualizarTipoDocAcreditacion(int intId, string strNombre)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intId, strNombre };
            DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_TIPO_DOCUMENTO_ACREDITACION", parametros);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Eliminar la informacion de los tipos de documentos de acreditaciòn
        /// </summary>
        public void EliminarTipoTipoDocAcreditacion(int intId)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intId };
            DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_TIPO_DOCUMENTO_ACREDITACION", parametros);
            db.ExecuteNonQuery(cmd);
        }
         #endregion
    }
}
