using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using System.Collections; 
using System.Data.SqlClient;
using System.Data;
using SILPA.LogicaNegocio.AudienciaPublica;
using SILPA.Comun;



namespace SILPA.LogicaNegocio.AudienciaPublica
{
    public class AudienciaPublica
    {
        AccesoDatos.AudienciaPublica.AudienciaPublicaDalc audiencia = new AccesoDatos.AudienciaPublica.AudienciaPublicaDalc();




        /// <summary>
        /// Listar audiencias publicas
        /// </summary>
        /// <param name="strNumeroSilpa"></param>
        /// <param name="intAutoridadAmbiental"></param>
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

            try
            {
                return audiencia.ListarAudienciasPublicas(
                    strNumeroSilpa,
                    intAutoridadAmbiental,
                    strNombreProyecto,
                    strNumeroExpediente,
                    DateFechaReunionInformativa, DateFechaCelebracionAudiencia);
            }
            finally
            {
                audiencia = null;
            }
        }



        /// <summary>
        /// Crear inscripción a audiencia pública
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
                                                       string strSegundoApellido, int intID_TipoDocumento, string strNumeroIdentificacion,
                                                       string strLugarExpedicion, string strCorreoElectronico, string strEntidadComunidad,
                                                       string strDocumentoAdjuntos)
        {


            try
            {
                return audiencia.IngresarInscripcionAudiencia(intID_AudienciaPublica,
                                                                strNumeroSILPAInscripcion, strPrimerNombre,
                                                                strSegundoNombre, strPrimerApellido, strSegundoApellido,
                                                                intID_TipoDocumento,
                                                                strNumeroIdentificacion, strLugarExpedicion,
                                                                strCorreoElectronico, strEntidadComunidad, strDocumentoAdjuntos);
            }
            finally
            {
                audiencia = null;
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
                AccesoDatos.AudienciaPublica.AudienciaPublicaDalc objDatos = new SILPA.AccesoDatos.AudienciaPublica.AudienciaPublicaDalc();
                return objDatos.consultarCorreoAA(intAutoridadAmbiental); ;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }

}
