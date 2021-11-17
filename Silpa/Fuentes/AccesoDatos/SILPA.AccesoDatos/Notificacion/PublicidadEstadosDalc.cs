using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Notificacion
{
    public class PublicidadEstadosDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public PublicidadEstadosDalc()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Obtiene los estados publicados de acuerdo a los parametros de busqueda especificados
        /// </summary>
        /// <param name="p_intTipoTramiteID">int con el identificador del tipo de trámite</param>
        /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental</param>
        /// <param name="p_strNumeroVital">string con el numero vital</param>
        /// <param name="p_strExpediente">string con el numero de expediente</param>
        /// <param name="p_intTipoActo">int con el identificador del tipo de actoa dministrativo</param>
        /// <param name="p_strNumeroActo">string con el numero de acto administrativo</param>
        /// <param name="p_strNumeroIdentificacion">string con el número de identificación</param>
        /// <param name="p_blnIncluirHistorico">bool que indica si se incluye publicaciones desfijadas</param>
        /// <param name="p_dteFechaInicio">DataTime con la fecha de inicio de fijación de la publicación</param>
        /// <param name="p_dteFechaFin">DateTime con la fecha de incio de fijación de la publicacion</param>
        /// <returns></returns>
        public DataTable ConsultarInformacionEstadosPublicados(int p_intTipoTramiteID, int p_intAutoridadID, string p_strNumeroVital, string p_strExpediente, int p_intTipoActo,
                                                             string p_strNumeroActo, string p_strNumeroIdentificacion, bool p_blnIncluirHistorico, DateTime p_dteFechaInicio, DateTime p_dteFechaFin)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objInformacion = null;
            DataTable objEstados = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_ESTADOS_NOTIFICACIO_PUBLICADOS");
                if (p_intTipoTramiteID > 0)
                    objDataBase.AddInParameter(objCommand, "@P_TIPO_TRAMITE_ID", DbType.Int32, p_intTipoTramiteID);
                if (p_intAutoridadID > 0)
                    objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_intAutoridadID);
                if (!string.IsNullOrEmpty(p_strNumeroVital))
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVital);
                if (!string.IsNullOrEmpty(p_strExpediente))
                    objDataBase.AddInParameter(objCommand, "@P_EXPEDIENTE", DbType.String, p_strExpediente);
                if (p_intTipoActo > 0)
                    objDataBase.AddInParameter(objCommand, "@P_TIPO_ACTO_ADMINISTRATIVO", DbType.Int32, p_intTipoActo);
                if (!string.IsNullOrEmpty(p_strNumeroActo))
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_ACTO_ADMINISTRATIVO", DbType.String, p_strNumeroActo);
                if (!string.IsNullOrEmpty(p_strNumeroIdentificacion))
                    objDataBase.AddInParameter(objCommand, "@P_IDENTIFICACION_USUARIO", DbType.String, p_strNumeroIdentificacion);
                objDataBase.AddInParameter(objCommand, "@P_INCLUIR_HISTORICO", DbType.Boolean, p_blnIncluirHistorico);
                if (p_dteFechaInicio != default(DateTime))
                    objDataBase.AddInParameter(objCommand, "@P_FECHA_DESDE", DbType.DateTime, p_dteFechaInicio);
                if (p_dteFechaFin != default(DateTime))
                    objDataBase.AddInParameter(objCommand, "@P_FECHA_HASTA", DbType.DateTime, p_dteFechaFin);

                //Obtener informacion
                objInformacion = objDataBase.ExecuteDataSet(objCommand);

                if (objInformacion != null && objInformacion.Tables.Count > 0)
                {
                    objEstados = objInformacion.Tables[0];
                }
                else if (objInformacion != null && objInformacion.Tables.Count == 2)
                {
                    objEstados = null;
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PublicidadEstadosDalc :: ConsultarInformacionEstadosPublicados -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PublicidadEstadosDalc :: ConsultarInformacionEstadosPublicados -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }

            return objEstados;
        }


        /// <summary>
        /// Retorna la información de un estado de publicidad especifico
        /// </summary>
        /// <param name="p_lngPublicacionEstadoPersonaActoID">long con el identificador de publicación</param>
        /// <returns>DataTable con la información de la publicación</returns>
        public DataTable ConsultarInformacionPublicacion(long p_lngPublicacionEstadoPersonaActoID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objInformacion = null;
            DataTable objPublicacion = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_ESTADO_NOTIFICACIOn_PUBLICADO");
                objDataBase.AddInParameter(objCommand, "@P_ID_NOT_PUBLICACION_ESTADO_PERSONA_ACTO", DbType.Int64, p_lngPublicacionEstadoPersonaActoID);

                //Obtener informacion
                objInformacion = objDataBase.ExecuteDataSet(objCommand);

                if (objInformacion != null && objInformacion.Tables.Count > 0)
                {
                    objPublicacion = objInformacion.Tables[0];
                }
                else if (objInformacion != null && objInformacion.Tables.Count == 2)
                {
                    objPublicacion = null;
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PublicidadEstadosDalc :: ConsultarInformacionPublicacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PublicidadEstadosDalc :: ConsultarInformacionPublicacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }

            return objPublicacion;
        }

    }
}
