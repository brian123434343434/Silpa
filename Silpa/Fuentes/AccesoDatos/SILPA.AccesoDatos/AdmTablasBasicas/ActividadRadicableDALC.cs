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
    public class ActividadRadicableDALC
    {
   private Configuracion objConfiguracion;

        public ActividadRadicableDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region metodos
        /// <summary>
        /// Lista la informacion de las actividades radicables
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable ListarActividadRadicable(string strNombre)
        {         
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {strNombre };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_ACTIVIDAD_RADICABLE", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);           
        }

        /// <summary>
        /// Registra la descripcion de las actividades radicables
        /// </summary>
        /// <param name="intId"></param>
        public void InsertarActividadRadicable(int intIdForma, bool bEstado, string strNombre, int intIdActividad)
        {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strNombre, bEstado, intIdForma, intIdActividad };
                DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_ACTIVIDAD_RADICABLE", parametros);
                db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Actualizar las actividades radicables
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        /// <param name="intId">Descripcion adquisicion</param>
        public void ActualizarActividadRadicable(int intId, int intIdForma, bool bEstado, string strNombre, int intIdActividad)
        {        
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId, strNombre, bEstado, intIdForma, intIdActividad };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_ACTIVIDAD_RADICABLE", parametros);
                db.ExecuteNonQuery(cmd);         
        }

        /// <summary>
        /// Eliminar la informacion de  las actividades radicables
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        public void EliminarActividadRadicable(int intId)
        {         
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_ACTIVIDAD_RADICABLE", parametros);
                db.ExecuteNonQuery(cmd);
        }


        #endregion
    }
}
