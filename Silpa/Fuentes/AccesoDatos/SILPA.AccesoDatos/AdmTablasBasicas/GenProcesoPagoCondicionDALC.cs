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
    public class GenProcesoPagoCondicionDALC
    {
        private Configuracion objConfiguracion;

        public GenProcesoPagoCondicionDALC() {
            objConfiguracion = new Configuracion();
        }

        #region metodos
        /// <summary>
        /// Lista la informacion de los procesos de pago condicion
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable ListarProcesoPagoCondicion(int intIdProcCase, string strConPago, string strConImprimir)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intIdProcCase, strConPago, strConImprimir };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_GEN_PROCESO_PAGO_CONDICION", parametros);
            DataSet ds_datos = new DataSet();
            ds_datos = db.ExecuteDataSet(cmd);
            return (ds_datos.Tables[0]);
        }

        /// <summary>
        /// Registra la descripcion de los procesos de pago condicion
        /// </summary>
        /// <param name="intId"></param>
        public void InsertarProcesoPagoCondicion(int intIdProcCase, string strConPago, string strConImprimir)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] {  intIdProcCase,  strConPago,  strConImprimir };
            DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_GEN_PROCESO_PAGO_CONDICION", parametros);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Actualizar las actividades radicables
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        /// <param name="intId">Descripcion adquisicion</param>
        public void ActualizarProcesoPagoCondicion(int intId, int intIdProcCase, string strConPago, string strConImprimir)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intId, intIdProcCase, strConPago, strConImprimir };
            DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_GEN_PROCESO_PAGO_CONDICION", parametros);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Eliminar la informacion de  las actividades radicables
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        public void EliminarProcesoPagoCondicion(int intId)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intId };
            DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_GEN_PROCESO_PAGO_CONDICION", parametros);
            db.ExecuteNonQuery(cmd);
        }

        #endregion

    }
}
