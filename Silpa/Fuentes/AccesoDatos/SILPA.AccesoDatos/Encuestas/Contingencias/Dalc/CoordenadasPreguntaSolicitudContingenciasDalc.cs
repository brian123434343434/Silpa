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
    public class CoordenadasPreguntaSolicitudContingenciasDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public CoordenadasPreguntaSolicitudContingenciasDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion


        #region  Metodos Publicos

        /// <summary>
        /// Guarda las coordenas de la solicitud
        /// </summary>
        /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
        /// <param name="p_objCoordenadasPreguntaSolicitudContingenciasEntity">objeto con las Coordenadas asociadas en una Pregunta con la información de la solicitud</param>
        /// <returns>int con el identifcador de la solicitud creada</returns>
        public int InsertarCoordenadasPreguntaSolicitudContingencias(SqlCommand p_objCommand, CoordenadasPreguntaSolicitudContingenciasEntity p_objCoordenadasPreguntaSolicitudContingenciasEntity)
        {
            int intSolicitudID = 0;

            try
            {
                p_objCommand.CommandText = "[dbo].[ENC_INSERTAR_COORDENADAS_PREGUNTA_CONTINGENCIA]";
                p_objCommand.Parameters.Clear();

                //Cargar parametros
                p_objCommand.Parameters.Add("@P_ENCSOLICITUDPREGUNTACONTINGENCIA_ID", SqlDbType.Int).Value = p_objCoordenadasPreguntaSolicitudContingenciasEntity.PreguntaSolicitudID;
                p_objCommand.Parameters.Add("@P_ENCSOLICITUDCONTINGENCIA_ID", SqlDbType.Int).Value = p_objCoordenadasPreguntaSolicitudContingenciasEntity.SolicitudID;
                p_objCommand.Parameters.Add("@P_ENCPREGUNTA_ID", SqlDbType.Int).Value = p_objCoordenadasPreguntaSolicitudContingenciasEntity.Pregunta.PreguntaID;
                p_objCommand.Parameters.Add("@P_GRADOS_LONGITUD", SqlDbType.Decimal).Value = p_objCoordenadasPreguntaSolicitudContingenciasEntity.GradosLongitud;
                p_objCommand.Parameters.Add("@P_MINUTOS_LONGITUD", SqlDbType.Decimal).Value = p_objCoordenadasPreguntaSolicitudContingenciasEntity.MinutosLongitud;
                p_objCommand.Parameters.Add("@P_SEGUNDOS_LONGITUD", SqlDbType.Decimal).Value = p_objCoordenadasPreguntaSolicitudContingenciasEntity.SegundosLongitud;
                p_objCommand.Parameters.Add("@P_GRADOS_LATITUD", SqlDbType.Decimal).Value = p_objCoordenadasPreguntaSolicitudContingenciasEntity.GradosLatitud;
                p_objCommand.Parameters.Add("@P_MINUTOS_LATITUD", SqlDbType.Decimal).Value = p_objCoordenadasPreguntaSolicitudContingenciasEntity.MinutosLatitud;
                p_objCommand.Parameters.Add("@P_SEGUNDOS_LATITUD", SqlDbType.Decimal).Value = p_objCoordenadasPreguntaSolicitudContingenciasEntity.SegundosLatitud;

                //Ejecuta sentencia
                using (IDataReader reader = p_objCommand.ExecuteReader())
                {
                    //Cargar id del certificado
                    if (reader.Read())
                    {
                        intSolicitudID = Convert.ToInt32(reader["ENCSOLICITUDCOORDENADASPREGUNTACONTINGENCIA_ID"]);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "CoordenadasPreguntaSolicitudContingenciasDalc :: GuardarCoordenadasPreguntaSolicitudContingencias -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "CoordenadasPreguntaSolicitudContingenciasDalc :: GuardarCoordenadasPreguntaSolicitudContingencias -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return intSolicitudID;
        }

        #endregion

    }
}
