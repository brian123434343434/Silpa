using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SoftManagement.Log;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.AdmTablasBasicas
{
    public class SolicitudesDALC
    {
           /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public SolicitudesDALC()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Metodo que obtiene las actividaes de un tramite por numero vital
        /// </summary>
        /// <param name="numeroExpediente">numero VITAL</param>
        /// <returns>2 tablas 1 con las actividades porcesadas y otra con el total de actividades</returns>
        public List<DataTable> BuscarSolicitud(string numeroExpediente)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_TRAMITE_ACTIVIDADES");
                db.AddInParameter(cmd, "NUMERO_EXPEDIENTE", DbType.String, numeroExpediente);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                List<DataTable> DtResult = new List<DataTable>();
                DataTable dtActividades = new DataTable();
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    dtActividades = db.ExecuteDataSet(cmd).Tables[0];
                    DtResult.Add(dtActividades);
                }

                return DtResult;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        public List<DataTable> BuscarDetalleActividadSolicitud(int idActivity, string numeroVital)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_DETALLE_ACTIVIDAD");
                db.AddInParameter(cmd, "@IDActivity", DbType.Int32, idActivity);
                db.AddInParameter(cmd, "@sol_numero_Silpa", DbType.String, numeroVital);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                List<DataTable> DtResult = new List<DataTable>();
                DataTable dtActividades = new DataTable();
                DataTable dtActividadesDocumentos = new DataTable();
                DtResult.Add(dsResultado.Tables[0]);
                DtResult.Add(dsResultado.Tables[1]);
                //if (dsResultado.Tables[0].Rows.Count > 0)
                //{
                //    dtActividades = db.ExecuteDataSet(cmd).Tables[0];
                //    DtResult.Add(dtActividades);
                //}
                //if (dsResultado.Tables[1].Rows.Count > 0)
                //{
                //    dtActividades = db.ExecuteDataSet(cmd).Tables[0];
                //    DtResult.Add(dtActividades);
                //}
                return DtResult;

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista las publicaciones existententes en la base de datos segun los registros que apliquen.
        /// </summary>
        /// <param name="parametroBusqueda">Parametro de busqueda requerido por el SP</param>
        /// <returns>Listado de  publicaciones existententes</returns>
        public DataTable BuscarSolicitud(string parametroIdenificacion, string  parametroSolicitud)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_SOLICITUDES");
                db.AddInParameter(cmd, "NUMERO_IDENTIFICACION", DbType.String, parametroIdenificacion);
                db.AddInParameter(cmd, "NUMERO_SILPA", DbType.String, parametroSolicitud); 
            
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                DataTable dtExpedientes = new DataTable();
                if (dsResultado.Tables[0].Rows.Count > 0)
                    dtExpedientes = db.ExecuteDataSet(cmd).Tables[0];
                return dtExpedientes;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Obtiene las solicitudes asociadas a un usuario por medio del ID_APPLICATION_USER
        /// </summary>
        /// <param name="pIDapplicationUSer"></param>
        /// <param name="pfechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public DataTable BuscarSolicitudes(string pIDapplicationUSer, string  pfechaInicial, string pfechaFinal)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_SOLICITUDES_PERSONA");
                db.AddInParameter(cmd, "ID_APPLICATION_USER", DbType.String, pIDapplicationUSer);
                db.AddInParameter(cmd, "FECHA_INICIAL", DbType.String, pfechaInicial);
                db.AddInParameter(cmd, "FECHA_FINAL", DbType.String, pfechaFinal); 
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                DataTable dtExpedientes = new DataTable();
                if (dsResultado.Tables[0].Rows.Count > 0)
                    dtExpedientes = db.ExecuteDataSet(cmd).Tables[0];
                return dtExpedientes;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public Boolean ActualizarSolicitud(int id_solicitud, int id_Autoridad)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_SOLICITUDES_ACTUALIZA_AA");
                db.AddInParameter(cmd, "ID_SOLICITUD", DbType.Int32, id_solicitud);
                db.AddInParameter(cmd, "ID_AUTORIDAD", DbType.Int32, id_Autoridad);
                db.ExecuteDataSet(cmd);
                return true;
            }
            catch (SqlException sqle)
            {
                return false;
                throw new Exception(sqle.Message);
            }
        }


        public Boolean EliminarSolicitud(int id_solicitud)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_SOLICITUDES_ELIMINAR");
                db.AddInParameter(cmd, "ID_SOLICITUD", DbType.Int32, id_solicitud);
                db.ExecuteDataSet(cmd);
                return true;
            }
            catch (SqlException sqle)
            {
                return false;
                throw new Exception(sqle.Message);
            }
        }

    }
}
