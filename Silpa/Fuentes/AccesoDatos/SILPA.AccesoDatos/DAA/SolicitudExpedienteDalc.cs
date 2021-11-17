using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SoftManagement.Log;

namespace SILPA.AccesoDatos.DAA
{
    public class SolicitudExpedienteDalc
    {
        private string silpaConnection;

        /// <summary>
        /// Contructor de  la clases
        /// </summary>
        public SolicitudExpedienteDalc()
        { 
            silpaConnection = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString;
        }


        public void Insertar(int autoridadId, string numeroExpediente, string tipoAsociacion, string numeroSilpa)
        {
            try
            {
	            SqlDatabase db = new SqlDatabase(silpaConnection);
	
	            object[] parametros = new object[] { autoridadId, numeroExpediente, tipoAsociacion, numeroSilpa};
	
	            DbCommand cmd = db.GetStoredProcCommand("GEN_INSERTAR_SOLICITUD_EXPEDIENTE", parametros);
	
	            db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Insertar Solicitud de Expediente.";
                throw new Exception(strException, ex);
            }
        }

        public void Eliminar(int autoridadId, string numeroExpediente, string tipoAsociacion)
        {
            try
            {
	            SqlDatabase db = new SqlDatabase(silpaConnection);
	
	            object[] parametros = new object[] { autoridadId, numeroExpediente, tipoAsociacion };
	
	            DbCommand cmd = db.GetStoredProcCommand("GEN_ELIMINAR_SOLICITUD_EXPEDIENTE", parametros);
	
	            db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Eliminar Solicitud de Expediente.";
                throw new Exception(strException, ex);
            }
        }



        public void InsertarInfoExp(string codigoExpediente, string numeroVital,string nombreExpediente,string descripcionExpediente,int secId,string ubicacion,string localizacion)
        {
            try
            {
	            SqlDatabase db = new SqlDatabase(silpaConnection);
	
	            object[] parametros = new object[] { codigoExpediente, numeroVital, nombreExpediente, descripcionExpediente, secId, ubicacion, localizacion };
	
	            DbCommand cmd = db.GetStoredProcCommand("GEN_INSERTAR_INF_EXPEDIENTE", parametros);
	
	            db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarInfoExp(string codigoExpediente, string numeroVital)
        {
            try
            {
	            SqlDatabase db = new SqlDatabase(silpaConnection);
	
	            object[] parametros = new object[] { codigoExpediente, numeroVital };
	
	            DbCommand cmd = db.GetStoredProcCommand("GEN_ELIMINAR_INFO_EXPEDIENTE", parametros);
	
	            db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que activa el expediente y numero silpa
        /// </summary>
        /// <param name="autoridadId"></param>
        /// <param name="numeroExpediente"></param>
        public void InsertarActivar(int autoridadId, string numeroExpediente)
        {
            try
            {
	            SqlDatabase db = new SqlDatabase(silpaConnection);
	
	            object[] parametros = new object[] { autoridadId, numeroExpediente };
	
	            DbCommand cmd = db.GetStoredProcCommand("GEN_INSERTAR_SOLICITUD_EXPEDIENTE_ACTIVO", parametros);
	
	            db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lista los terceros intervinientes asociados a un expediente
        /// </summary>
        /// <param name="int_exp_id">Identificador del expediente para el que se desean listar los terceros intervinientes</param>
        /// <returns>Retorna una lista de terceros intervinientes asociada al identificador de expediente suminstrado</returns>
        public void CrearSolicitudManual(int AutoridadID, int ExpedienteID, string CodigoExpediente, int SectorID, int PersonaID, string NumeroVITAL, string NumeroRadicado, int TramiteID, string NombreProyecto)
        {

            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(silpaConnection);

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("DAA_INSERT_SOLICITUD_MANUAL");
                objDataBase.AddInParameter(objCommand, "@P_SOL_ID_SECTOR", DbType.Int32, SectorID);
                objDataBase.AddInParameter(objCommand, "@P_EXP_ID", DbType.Int32, ExpedienteID);
                objDataBase.AddInParameter(objCommand, "@P_SOL_ID_AA", DbType.Int32, AutoridadID);
                objDataBase.AddInParameter(objCommand, "@P_SOL_ID", DbType.Int32, PersonaID);
                objDataBase.AddInParameter(objCommand, "@P_SOL_ID_RADICACION", DbType.String, NumeroRadicado);
                objDataBase.AddInParameter(objCommand, "@P_GTT_ID", DbType.Int32, TramiteID);
                objDataBase.AddInParameter(objCommand, "@P_NUMERO_SILPA", DbType.String, NumeroVITAL);
                objDataBase.AddInParameter(objCommand, "@P_CODIGO_EXPEDIENTE", DbType.String, CodigoExpediente);
                objDataBase.AddInParameter(objCommand, "@P_NOMBRE_PROYECTO", DbType.String, NombreProyecto);
                objDataBase.AddInParameter(objCommand, "@SOL_ID", DbType.Int32, 0);
                objDataBase.ExecuteNonQuery(objCommand);
                
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudExpedienteDalc :: CrearSolicitudManual -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

        }


        /// <summary>
        /// Obtener el numero vital padre de una solicitud
        /// </summary>
        /// <param name="strNumeroVital">string con el numero vital de la solicitud</param>
        /// <returns>string con el numero vital de la solicitud</returns>
        public string ObtenerNumeroVITALPadreSolicitud(string strNumeroVital)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objDatos = null;
            string strNumeroVitalPadre = "";

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(silpaConnection);

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("GEN_CONSULTAR_NUMERO_VITAL_PADRE_TRAMITE");
                objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.Int32, strNumeroVital);
                objDatos = objDataBase.ExecuteDataSet(objCommand);

                //Cargar el numero vital
                if (objDatos != null && objDatos.Tables.Count > 0 && objDatos.Tables[0].Rows.Count > 0)
                    strNumeroVitalPadre = objDatos.Tables[0].Rows[0]["NUMERO_VITAL_PADRE"].ToString();

            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudExpedienteDalc :: CrearSolicitudManual -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return strNumeroVitalPadre;
        }

    }
}
