using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Encuestas.Contingencias.Entity;
using SoftManagement.Log;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.Encuestas.Contingencias.Dalc
{
    public class RespuestaPreguntaSolicitudContingenciasDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public RespuestaPreguntaSolicitudContingenciasDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion

        #region  Metodos Publicos

        /// <summary>
        /// Guarda la información basica de la Respuesta a una Pregunta de una Solicitud
        /// </summary>
        /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
        /// <param name="p_objRespuestaPreguntaSolicitudContingenciasEntity">RespuestaPreguntaSolicitudContingenciasEntity con la información de la Respuesta a una Pregunta Solicitud</param>
        /// <returns>int con el identificador de la Respuesta creada</returns>
        public int InsertarRespuestaPreguntaSolicitudContingencias(SqlCommand p_objCommand, RespuestaPreguntaSolicitudContingenciasEntity p_objRespuestaPreguntaSolicitudContingenciasEntity)
        {
            int intSolicitudID = 0;

            try
            {
                p_objCommand.CommandText = "[dbo].[ENC_INSERTAR_SOLICITUD_RESPUESTA_PREGUNTA_CONTINGENCIA]";
                p_objCommand.Parameters.Clear();

                //Cargar parametros
                p_objCommand.Parameters.Add("@P_ENCSOLICITUDPREGUNTACONTINGENCIA_ID", SqlDbType.Int).Value = p_objRespuestaPreguntaSolicitudContingenciasEntity.PreguntaSolicitudID;
                p_objCommand.Parameters.Add("@P_ENCSOLICITUDCONTINGENCIA_ID", SqlDbType.Int).Value = p_objRespuestaPreguntaSolicitudContingenciasEntity.SolicitudID;
                p_objCommand.Parameters.Add("@P_ENCPREGUNTA_ID", SqlDbType.Int).Value = p_objRespuestaPreguntaSolicitudContingenciasEntity.Pregunta.PreguntaID;
                p_objCommand.Parameters.Add("@P_RESPUESTA", SqlDbType.VarChar).Value = p_objRespuestaPreguntaSolicitudContingenciasEntity.Respuesta;

                //Ejecuta sentencia
                using (IDataReader reader = p_objCommand.ExecuteReader())
                {
                    //Cargar id del certificado
                    if (reader.Read())
                    {
                        intSolicitudID = Convert.ToInt32(reader["ENCSOLICITUDRESPUESTAPREGUNTACONTINGENCIA_ID"]);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "RespuestaPreguntaSolicitudContingenciasDalc :: GuardarRespuestaPreguntaSolicitudContingencias -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "RespuestaPreguntaSolicitudContingenciasDalc :: GuardarRespuestaPreguntaSolicitudContingencias -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return intSolicitudID;
        }

        #endregion

    }
}
