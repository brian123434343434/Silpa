using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.SqlClient;
using SILPA.AccesoDatos.AudienciaPublica;

namespace SILPA.AccesoDatos.AudienciaPublica
{
    public class AudienciaPublicaDalc
    {

        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        public Configuracion objConfiguracion;


        public AudienciaPublicaDalc()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Listar audiencias publicas 
        /// </summary>
        /// <param name="strNumeroSilpa"></param>
        /// <param name="strAutoridadAmbiental"></param>
        /// <param name="strNombreProyecto"></param>
        /// <param name="strNumeroExpediente"></param>
        /// <param name="DateFechaReunionInformativa"></param>
        /// <param name="DateFechaCelebracionAudiencia"></param>
        /// <returns></returns>
        public DataTable ListarAudienciasPublicas(string strNumeroSilpa,
                                                    Nullable<int> intAutoridadAmbiental,
                                                    string strNombreProyecto,
                                                    string strNumeroExpediente,
                                                    Nullable<DateTime> DateFechaReunionInformativa,
                                                    Nullable<DateTime> DateFechaCelebracionAudiencia)
        {
            AudienciaPublicaIdentity objIdentity = new AudienciaPublicaIdentity();

            try
            {

                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { DateFechaReunionInformativa, DateFechaCelebracionAudiencia, strNumeroSilpa, intAutoridadAmbiental, strNombreProyecto, strNumeroExpediente };
                DbCommand cmd = db.GetStoredProcCommand("AUD_CONSULTAR_AUDIENCIA_PUBLICA", parametros);
                DataSet dsResultado = new DataSet();
                dsResultado = db.ExecuteDataSet(cmd);

                if (dsResultado != null)
                {
                    if (dsResultado.Tables[0].Rows.Count > 0)
                    {
                        objIdentity = new AudienciaPublicaIdentity();
                        objIdentity.IDAudienciaPublica = int.Parse(dsResultado.Tables[0].Rows[0]["ID_AUDIENCIA_PUBLICA"].ToString());
                        objIdentity.NumeroSilpa = Convert.ToString(dsResultado.Tables[0].Rows[0]["NUMERO_SILPA"]);
                        objIdentity.AutoridadAmbiental = Convert.ToString(dsResultado.Tables[0].Rows[0]["AUTORIDAD_AMBIENTAL"]);
                        objIdentity.NombreProyecto = Convert.ToString(dsResultado.Tables[0].Rows[0]["NOMBRE_PROYECTO"]);
                        objIdentity.NumeroExpediente = Convert.ToString(dsResultado.Tables[0].Rows[0]["NUMERO_EXPEDIENTE"]);
                        objIdentity.FechaReunionInformativa = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FECHA_HORA_REUNION_INFORMATIVA"]);
                        objIdentity.FechaCelebracionAudiencia = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FECHA_HORA_CELEBRACION_AUDIENCIA"]);
                    }
                }
                return dsResultado.Tables[0];
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }



        /// <summary>
        /// Ingresar datos de incripción a una audiencia publica
        /// </summary>
        /// <param name="intID_AudienciaPublica"></param>
        /// <param name="strNumeroSILPASolicitud"></param>
        /// <param name="strNumeroSILPAInscripcion"></param>
        /// <param name="strPrimerNombre"></param>
        /// <param name="strSegundoNombre"></param>
        /// <param name="strPrimerApellido"></param>
        /// <param name="strSegundoApellido"></param>
        /// <param name="intID_TipoDocumento"></param>
        /// <param name="strNumeroIdentificacion"></param>
        /// <param name="strLugarExpedicion"></param>
        /// <param name="strCorreoElectronico"></param>
        /// <param name="strEntidadComunidad"></param>
        /// <param name="strDocumentoAdjuntos"></param>
        /// <returns></returns>
        public DataTable IngresarInscripcionAudiencia(int intID_AudienciaPublica, string strNumeroSILPAInscripcion,
                                                    string strPrimerNombre, string strSegundoNombre, string strPrimerApellido,
                                                    string strSegundoApellido, int intID_TipoDocumento,
                                                    string strNumeroIdentificacion, string strLugarExpedicion,
                                                    string strCorreoElectronico, string strEntidadComunidad, string strDocumentoAdjuntos)
        {

            try
            {

                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] 
                {
                     intID_AudienciaPublica,strNumeroSILPAInscripcion,
                     strPrimerNombre, strSegundoNombre, strPrimerApellido,
                     strSegundoApellido,    intID_TipoDocumento,strNumeroIdentificacion,
                     strLugarExpedicion,  strCorreoElectronico,  strEntidadComunidad, 
                     strDocumentoAdjuntos
                
                };
                DbCommand cmd = db.GetStoredProcCommand("AUD_CREAR_INSCRIPCION_AUDIENCIA_PUBLICA", parametros);
                DataSet dsResultado = new DataSet();
                dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado.Tables[0];

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Consultar correo autoridad ambiental
        /// </summary>
        /// <param name="intAutoridadAmbiental"></param>
        /// <returns></returns>
        public DataTable consultarCorreoAA(int intAutoridadAmbiental)
        {            
            try
            {

                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {intAutoridadAmbiental};
                DbCommand cmd = db.GetStoredProcCommand("AUD_CONSULTAR_CORREO_AA", parametros);
                DataSet dsResultado = new DataSet();
                dsResultado = db.ExecuteDataSet(cmd);               
                return dsResultado.Tables[0];
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}