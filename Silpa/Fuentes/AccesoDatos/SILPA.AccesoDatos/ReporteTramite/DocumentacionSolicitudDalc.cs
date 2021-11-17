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

namespace SILPA.AccesoDatos.ReporteTramite
{
    public class DocumentacionSolicitudDalc
    {

        
         /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public DocumentacionSolicitudDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Retorna la informacion asociada al numero silpa
        /// </summary>
        /// <param name="strNumSILPA">Numero silpa</param>
        /// <returns> SOL_FECHA_CREACION, SOL_NUMERO_SILPA, IDProcessInstance, IDActivity, 
        /// IDRelated, EntryDataType, EntryData, IDEntryData, URLDataView, DESCRIPCION,
        /// RUTA</returns>
        public DataSet ListarDocumentacionSolicitud(string strNumSILPA)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strNumSILPA };
                DbCommand cmd = db.GetStoredProcCommand("BPM_GENERAR_FORMULARIO", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ListarDocumentacionSolicitudFUS(string strNumSILPA)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strNumSILPA };
                DbCommand cmd = db.GetStoredProcCommand("BPM_GENERAR_INFORMACION_FUS", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ListarDocumentacionSolicitudFUSxPerfil(string strNumSILPA, int idUsuario)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strNumSILPA, idUsuario };
                DbCommand cmd = db.GetStoredProcCommand("BPM_GENERAR_INFORMACION_FUS_X_PERFIL", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }




    }
}
