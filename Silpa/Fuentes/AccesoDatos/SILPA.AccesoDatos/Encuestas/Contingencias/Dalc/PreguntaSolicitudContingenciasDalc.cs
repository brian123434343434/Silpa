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
    public class PreguntaSolicitudContingenciasDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public void PreguntaSolicitudCambioMenorDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion


        #region  Metodos Publicos

                /// <summary>
                /// Guardar la información de una pregunta asociada a la solicitud de Contingencias
                /// </summary>
                /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
                /// <param name="p_objPregunta">objeto con la información de la  Pregunta asociada a una Solicitud de ContingenciasEntity </param>
                /// <returns>int con el identifcador de la pregunta creada</returns>
                public int InsertarPreguntaSolicitudContingencias(SqlCommand p_objCommand, PreguntaSolicitudContingenciasEntity p_objPregunta)
                {
                    int intPreguntaSolicitudContingenciasID = 0;

                    try
                    {
                        p_objCommand.CommandText = "ENC_INSERTAR_SOLICITUD_PREGUNTA_CONTINGENCIA";
                        p_objCommand.Parameters.Clear();

                        //Cargar parametros
                        p_objCommand.Parameters.Add("@P_ENCSOLICITUDCONTINGENCIA_ID", SqlDbType.Int).Value = p_objPregunta.PreguntaSolicitudID;
                        p_objCommand.Parameters.Add("@P_ENCPREGUNTA_ID", SqlDbType.Int).Value = p_objPregunta.Pregunta.PreguntaID;

                        //Ejecuta sentencia
                        using (IDataReader reader = p_objCommand.ExecuteReader())
                        {
                            //Cargar id del certificado
                            if (reader.Read())
                            {
                                intPreguntaSolicitudContingenciasID = Convert.ToInt32(reader["ENCSOLICITUDPREGUNTACONTINGENCIA_ID"]);
                            }
                        }
                    }
                    catch (SqlException sqle)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "PreguntaSolicitudContingenciasDalc :: InsertarPreguntaSolicitudContingencias -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                        //Escalar error
                        throw sqle;
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "PreguntaSolicitudContingenciasDalc :: InsertarPreguntaSolicitudContingencias -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw exc;
                    }

                    return intPreguntaSolicitudContingenciasID;
                }

            #endregion
    }
}
    