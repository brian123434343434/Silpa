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
    public class OpcionPreguntaSolicitudContingenciasDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public OpcionPreguntaSolicitudContingenciasDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion

        #region  Metodos Publicos

        /// <summary>
        /// Guarda la información basica de la opción de una Pregunta de una Solicitud
        /// </summary>
        /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
        /// <param name="p_objRespuestaPreguntaSolicitudContingenciasEntity">OpcionPreguntaSolicitudContingenciasEntity con la información de la opcion a una Pregunta Solicitud</param>
        /// <returns>int con el identificador de la opcion creada</returns>
        public int InsertarOpcionPreguntaSolicitudContingencias(SqlCommand p_objCommand, OpcionPreguntaSolicitudContingenciasEntity p_objOpcionPreguntaSolicitudContingenciasEntity)
        {
            int intSolicitudID = 0;

            try
            {
                p_objCommand.CommandText = "[dbo].[ENC_INSERTAR_SOLICITUD_OPCION_PREGUNTA_CONTINGENCIA]";
                p_objCommand.Parameters.Clear();

                //Cargar parametros
                p_objCommand.Parameters.Add("@P_ENCSOLICITUDPREGUNTACONTINGENCIA_ID", SqlDbType.Int).Value = p_objOpcionPreguntaSolicitudContingenciasEntity.PreguntaSolicitudID;
                p_objCommand.Parameters.Add("@P_ENCSOLICITUDCONTINGENCIA_ID", SqlDbType.Int).Value = p_objOpcionPreguntaSolicitudContingenciasEntity.SolicitudID;
                p_objCommand.Parameters.Add("@P_ENCPREGUNTA_ID", SqlDbType.Int).Value = p_objOpcionPreguntaSolicitudContingenciasEntity.Pregunta.PreguntaID;
                p_objCommand.Parameters.Add("@P_ENCOPCION_PREGUNTA_ID", SqlDbType.Int).Value = p_objOpcionPreguntaSolicitudContingenciasEntity.OpcionPregunta.OpcionPreguntaID;
                if (p_objOpcionPreguntaSolicitudContingenciasEntity.Selecciono != null)
                    p_objCommand.Parameters.Add("@P_SELECCIONO", SqlDbType.Bit).Value = p_objOpcionPreguntaSolicitudContingenciasEntity.Selecciono.Value;
                if (!string.IsNullOrEmpty(p_objOpcionPreguntaSolicitudContingenciasEntity.RespuestaOpcion))
                    p_objCommand.Parameters.Add("@P_RESPUESTA_OPCION", SqlDbType.VarChar).Value = p_objOpcionPreguntaSolicitudContingenciasEntity.RespuestaOpcion;

                //Ejecuta sentencia
                using (IDataReader reader = p_objCommand.ExecuteReader())
                {
                    //Cargar id del certificado
                    if (reader.Read())
                    {
                        intSolicitudID = Convert.ToInt32(reader["ENCSOLICITUDOPCIONPREGUNTACONTINGENCIA_ID"]);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "OpcionPreguntaSolicitudContingenciasDalc :: InsertarOpcionPreguntaSolicitudContingencias -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "OpcionPreguntaSolicitudContingenciasDalc :: InsertarOpcionPreguntaSolicitudContingencias -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return intSolicitudID;
        }

        #endregion

    }
}
