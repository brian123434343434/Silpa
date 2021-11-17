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
    public class Gen_ConceptoDALC
    {
        private Configuracion objConfiguracion;
        public Gen_ConceptoDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region "GEN_CONCEPTO"

        /// <summary>
        /// Listar la informacion de los tipos de persona
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion</param>
        /// <returns></returns>
        public DataTable Listar_Gen_Concepto(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_GEN_CONCEPTO", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }

            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Registra la descripcion de los tipo de persona
        /// </summary>
        /// <param name="strDescripcion"></param>
        public void Insertar_Gen_Concepto(string strDescripcion, byte byEstado )
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion, byEstado };
                DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_GEN_CONCEPTO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Actualizar los tipo de persona
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        /// <param name="strDescripcion">Descripcion adquisicion</param>
        public void Actualizar_Gen_Concepto(int iID, string strDescripcion, byte byEstado)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strDescripcion, byEstado };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_GEN_CONCEPTO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Eliminar la informacion de los tipos de persona
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        public void Eliminar_Gen_Concepto(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_GEN_CONCEPTO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }
        #endregion
    }
}
