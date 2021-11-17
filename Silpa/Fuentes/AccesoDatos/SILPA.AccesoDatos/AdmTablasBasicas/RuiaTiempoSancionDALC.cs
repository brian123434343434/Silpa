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
    public class RuiaTiempoSancionDALC
    {
        private Configuracion objConfiguracion;

        public RuiaTiempoSancionDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region Tipo_Sancion
        /// <summary>
        /// Lista la informacion de los Tipos de Sancion
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable ListarTiempoSancion(int intDias, string strNombre)
        {         
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intDias, strNombre };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_RUIA_TIEMPO_SANCION", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);           
        }

        /// <summary>
        /// Registra la descripcion de los tipo de sancion
        /// </summary>
        /// <param name="intDias"></param>
        public void InsertarTiempoSancion(int intDias, bool bEstado, string strNombre)
        {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intDias, bEstado, strNombre };
                DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_RUIA_TIEMPO_SANCION", parametros);
                db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Actualizar los tipo de falta
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        /// <param name="intDias">Descripcion adquisicion</param>
        public void ActualizarTiempoSancion(int intId, int intDias, bool bEstado, string strNombre)
        {        
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId, intDias, bEstado, strNombre };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_RUIA_TIEMPO_SANCION", parametros);
                db.ExecuteNonQuery(cmd);         
        }

        /// <summary>
        /// Eliminar la informacion de los tiempos de sancion
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        public void EliminarTiempoSancion(int intId)
        {         
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_RUIA_TIEMPO_SANCION", parametros);
                db.ExecuteNonQuery(cmd);
        }


        #endregion
    }
}
