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
  public class CasoProcesoPermitidoDALC
    {
    private Configuracion objConfiguracion;

      public CasoProcesoPermitidoDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region metodos

        /// <summary>
        /// Lista la informacion de los casos de proceso permitidos
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable ListarCasoProcesoPermitido(string strNombre, string strfechaIni, string strFechaFin)
        {         
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strfechaIni, strFechaFin, strNombre };
                DbCommand cmd = db.GetStoredProcCommand("WSB_LISTAR_CASO_PROCESO_PERMITIDO", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);           
        }

        /// <summary>
        /// Lista la informacion de los casos de proceso permitidos
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable CargarCboCasoProcesoPermitido(int iIdCaso)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { iIdCaso };
            DbCommand cmd = db.GetStoredProcCommand("WSB_CARGAR_COMBO_CASOS", parametros);
            DataSet ds_datos = new DataSet();
            ds_datos = db.ExecuteDataSet(cmd);
            return (ds_datos.Tables[0]);
        }

        /// <summary>
        /// Registra la descripcion de los casos de proceso permitidos
        /// </summary>
        /// <param name="intId"></param>
        public void InsertarCasoProcesoPermitido(int iIdCaso, string strFecha, bool bEstado, string strNombre, bool bTipoEntidad)
        {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iIdCaso, strNombre, strFecha, bEstado, bTipoEntidad };
                DbCommand cmd = db.GetStoredProcCommand("WSB_INSERTAR_CASO_PROCESO_PERMITIDO", parametros);
                db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Actualizar los casos de proceso permitidos
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        /// <param name="intId">Descripcion adquisicion</param>
      public void ActualizarCasoProcesoPermitido(int intId, int iIdCaso, string strFecha, bool bEstado, string strNombre, bool bTipoEntidad)
        {        
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId, iIdCaso, strNombre, strFecha, bEstado, bTipoEntidad };
                DbCommand cmd = db.GetStoredProcCommand("WSB_ACTUALIZAR_CASO_PROCESO_PERMITIDO", parametros);
                db.ExecuteNonQuery(cmd);         
        }

        /// <summary>
        /// Eliminar la informacion de los casos de proceso permitidos
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        public void EliminarCasoProcesoPermitido(int intId)
        {         
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId };
                DbCommand cmd = db.GetStoredProcCommand("WSB_ELIMINAR_CASO_PROCESO_PERMITIDO", parametros);
                db.ExecuteNonQuery(cmd);
        }

        #endregion
    }
}

