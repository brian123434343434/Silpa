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
    public class VAB_CampoDALC
    {
        private Configuracion objConfiguracion;
        public VAB_CampoDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "VAB_Campo"

            /// <summary>
            /// Listar los registros de la tabla VAB_Campos
            /// </summary>
            /// <param name="strDescripcion">Nombre o Descripcion para buscar</param>
            /// <returns></returns>
            public DataTable Listar_Campo(string strDescripcion)
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] { strDescripcion };
                    DbCommand cmd = db.GetStoredProcCommand("VAB_Listar_CAMPO", parametros);
                    DataSet ds_datos = db.ExecuteDataSet(cmd);
                    return (ds_datos.Tables[0]);
                }
                catch (SqlException sql)
                {
                    throw new Exception(sql.Message);
                }
            }

            /// <summary>
            /// Consulta para alimentar el combo de tipo de dato
            /// </summary>
            /// <returns></returns>
            public DataTable Carga_Combo_Tipo_Dato()
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    DbCommand cmd = db.GetStoredProcCommand("VAB_Cargar_Combo_TDato");
                    DataSet ds_datos = db.ExecuteDataSet(cmd);
                    return (ds_datos.Tables[0]);
                }
                catch (SqlException sql)
                {
                    throw new Exception(sql.Message);
                }
            }

            /// <summary>
            /// Registra un nuevo campo
            /// </summary>
            /// <param name="strId"></param>
            /// <param name="Descripcion"></param>
            /// <param name="iTipoDato"></param>
            public void Insertar_Campo(string strId, string strDescripcion, Int32 iTipoDato)
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] {strId, strDescripcion, iTipoDato};
                    DbCommand cmd = db.GetStoredProcCommand("VAB_Insetar_CAMPO", parametros);
                    db.ExecuteNonQuery(cmd);
                }
                catch (Exception sql)
                {
                    throw new Exception(sql.Message);
                }

            }

            /// <summary>
            /// Actualiza erl registro de la tabla VAB_Campo
            /// </summary>
            /// <param name="strId"></param>
            /// <param name="strDescripcion"></param>
            /// <param name="iTipoDato"></param>
            public void Actualizar_Campo(string strId, string strDescripcion, Int32 iTipoDato)
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] {strId, strDescripcion, iTipoDato};
                    DbCommand cmd = db.GetStoredProcCommand("VAB_Actualizar_CAMPO", parametros);
                    db.ExecuteNonQuery(cmd);
                }
                catch (Exception sql)
                {
                    throw new Exception(sql.Message);
                }
            }

            /// <summary>
            /// Elimina el registro de la tabla VAB_Campo
            /// </summary>
            /// <param name="iID">Id del campo</param>
            public void Eliminar_Campo(string strID)
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] {strID};
                    DbCommand cmd = db.GetStoredProcCommand("VAB_Eliminar_CAMPO", parametros);
                    db.ExecuteNonQuery(cmd);
                }
                catch (Exception sql)
                {
                    throw new Exception(sql.Message);
                }
            }

        #endregion
    }
}
