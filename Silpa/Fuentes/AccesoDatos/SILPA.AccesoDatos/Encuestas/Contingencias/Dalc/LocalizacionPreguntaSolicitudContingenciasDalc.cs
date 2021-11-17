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
    public class LocalizacionPreguntaSolicitudContingenciasDalc
    {

        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public LocalizacionPreguntaSolicitudContingenciasDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion

        #region  Metodos Publicos

        /// <summary>
        /// Guarda la información basica de la solicitud
        /// </summary>
        /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
        /// <param name="p_objLocalizacionPreguntaSolicitudContingenciasEntity">Objeto con la informacion de la Localizacion de una Pregunta asoiciada a uno Solicitud Contingencias </param>
        /// <returns>int con el identifcador de la solicitud creada</returns>
        public int InsertarLocalizacionPreguntaSolicitudContingencias(SqlCommand p_objCommand, LocalizacionPreguntaSolicitudContingenciasEntity p_objLocalizacionPreguntaSolicitudContingenciasEntity)
        {
            int intSolicitudID = 0;

            try
            {
                p_objCommand.CommandText = "[dbo].[ENC_INSERTAR_SOLICITUD_LOCALIZACION_PREGUNTA_CONTINGENCIA]";
                p_objCommand.Parameters.Clear();

                //Cargar parametros
                p_objCommand.Parameters.Add("@P_ENCSOLICITUDPREGUNTACONTINGENCIA_ID", SqlDbType.Int).Value =    p_objLocalizacionPreguntaSolicitudContingenciasEntity.PreguntaSolicitudID;
                p_objCommand.Parameters.Add("@P_ENCSOLICITUDCONTINGENCIA_ID", SqlDbType.Int).Value =            p_objLocalizacionPreguntaSolicitudContingenciasEntity.SolicitudID;
                p_objCommand.Parameters.Add("@P_ENCPREGUNTA_ID", SqlDbType.Int).Value =                         p_objLocalizacionPreguntaSolicitudContingenciasEntity.Pregunta.PreguntaID;
                p_objCommand.Parameters.Add("@P_DEPARTAMENTO", SqlDbType.VarChar).Value =                       p_objLocalizacionPreguntaSolicitudContingenciasEntity.Departamento;
                p_objCommand.Parameters.Add("@P_CIUDAD", SqlDbType.VarChar).Value =                             p_objLocalizacionPreguntaSolicitudContingenciasEntity.Ciudad;

                //Ejecuta sentencia
                using (IDataReader reader = p_objCommand.ExecuteReader())
                {
                    //Cargar id del certificado
                    if (reader.Read())
                    {
                        intSolicitudID = Convert.ToInt32(reader["ENCSOLICITUDLOCALIZACIONPREGUNTACONTINGENCIA_ID"]);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "LocalizacionPreguntaSolicitudContingenciasDalc :: GuardarLocalizacionPreguntaSolicitudContingencias -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "LocalizacionPreguntaSolicitudContingenciasDalc :: GuardarLocalizacionPreguntaSolicitudContingencias -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return intSolicitudID;
        }

        #endregion

    }
}
