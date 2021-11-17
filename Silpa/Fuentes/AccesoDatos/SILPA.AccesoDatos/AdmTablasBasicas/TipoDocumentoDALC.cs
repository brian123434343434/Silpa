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
    public class TipoDocumentoDALC
    {
   private Configuracion objConfiguracion;

        public TipoDocumentoDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region metodos
        /// <summary>
        /// Lista la informacion de los tipos de documentos
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable ListarTipoDocumento(int intIdPar, string strNombre)
        {         
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intIdPar, strNombre };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_TIPOS_DOCUMENTO", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);           
        }

        /// <summary>
        /// Registra la descripcion de los tipo de documentos
        /// </summary>
        /// <param name="intId"></param>
        public void InsertarTipoDocumento(int intIdPar, bool bEstado, string strNombre, string strCodConvenio, int? intIdFlujoNotificacion, int? condicionEspecialID)
        {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strNombre, intIdPar, bEstado, strCodConvenio, (intIdFlujoNotificacion == 0) ? null : intIdFlujoNotificacion, condicionEspecialID };
                DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_TIPO_DOCUMENTO", parametros);
                db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Actualizar los tipo de documentos
        /// </summary>
        public void ActualizarTipoDocumento(int intId, int intIdPar, bool bEstado, string strNombre, string strCodConvenio, int? intIdFlujoNotificacion, int? condicionEspecialID)
        {        
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId, strNombre, intIdPar, bEstado, strCodConvenio, (intIdFlujoNotificacion == 0) ? null : intIdFlujoNotificacion, condicionEspecialID };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_TIPO_DOCUMENTO", parametros);
                db.ExecuteNonQuery(cmd);         
        }

        /// <summary>
        /// Eliminar la informacion de los tipos de documento
        /// </summary>
        public void EliminarTipoDocumento(int intId)
        {         
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_TIPO_DOCUMENTO", parametros);
                db.ExecuteNonQuery(cmd);
        }


        #endregion
    }
}
