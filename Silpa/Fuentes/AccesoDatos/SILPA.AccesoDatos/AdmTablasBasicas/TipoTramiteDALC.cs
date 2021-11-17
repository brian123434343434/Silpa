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
    public class TipoTramiteDALC
    {
  private Configuracion objConfiguracion;

        public TipoTramiteDALC()
        {
            objConfiguracion = new Configuracion();
        }

        #region metodos
        /// <summary>
        /// Lista la informacion de los tipos de tramite
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable ListarTipoTramite(int intIdPar, string strNombre)
        {         
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intIdPar, strNombre };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_TIPO_TRAMITE", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);           
        }

        /// <summary>
        /// Registra la descripcion de los tipo de tramite
        /// </summary>
        /// <param name="intId"></param>
        public void InsertarTipoTramite(int intIdPar, string strNombre, bool mostrarDocumentos)
        {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strNombre, intIdPar, mostrarDocumentos};
                DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_TIPO_TRAMITE", parametros);
                db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Actualizar los tipo de tramite
        /// </summary>
        public void ActualizarTipoTramite(int intId, int intIdPar, string strNombre,bool bVisible, bool mostrarDocumentos)
        {        
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId, strNombre, intIdPar, bVisible, mostrarDocumentos };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_TIPO_TRAMITE", parametros);
                db.ExecuteNonQuery(cmd);         
        }

        /// <summary>
        /// Eliminar la informacion de los tipos de tramite
        /// </summary>
        public void EliminarTipoTramite(int intId)
        {         
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_TIPO_TRAMITE", parametros);
                db.ExecuteNonQuery(cmd);
        }


        #endregion

        public DataTable ListaCondicionesEspeciales(int? condicion, string codigoCondicion, int? tipoCondicion )
        {
            try
            {
	            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { condicion, codigoCondicion, tipoCondicion };
	            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_CONDICIONES_ESPECIALES", parametros);
	            DataSet ds_datos = new DataSet();
	            ds_datos = db.ExecuteDataSet(cmd);
	            return (ds_datos.Tables[0]);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Listar Condiciones Especiales.";
                throw new Exception(strException, ex);
            }
        }
        public void CrearCondicionesEspeciales(int? condicion, string codigoCondicion, int? tipoCondicion)
        {
            try
            {
	            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { condicion, codigoCondicion, tipoCondicion };
	            DbCommand cmd = db.GetStoredProcCommand("BAS_CREAR_CONDICIONES_ESPECIALES", parametros);
	            db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Crear Condiciones Especiales.";
                throw new Exception(strException, ex);
            }
        }
        public void ActualizarCondicionesEspeciales(int? condicion, string codigoCondicion, int? tipoCondicion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { condicion, codigoCondicion, tipoCondicion };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_CONDICIONES_ESPECIALES", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Crear Condiciones Especiales.";
                throw new Exception(strException, ex);
            }
        }
        public void EliminarCondicionesEspeciales( int? condicion)
        {
            try
            {
	            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
	            object[] parametros = new object[] { condicion };
	            DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_CONDICIONES_ESPECIALES", parametros);
	            db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Eliminar Condiciones Especiales.";
                throw new Exception(strException, ex);
            }
        }
    }
}
