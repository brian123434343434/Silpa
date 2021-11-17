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
    public class SolicitudContingenciasDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public SolicitudContingenciasDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion


        #region  Metodos Privados

        /// <summary>
        /// Guarda la información basica de la solicitud
        /// </summary>
        /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
        /// <param name="p_objSolicitud">SolicitudContingenciasEntity con la información de la solicitud</param>
        /// <returns>int con el identifcador de la solicitud creada</returns>
        private int GuardarSolicitudContingencias(SqlCommand p_objCommand, SolicitudContingenciasEntity p_objSolicitud)
        {
            int intSolicitudID = 0;

            try
            {
                p_objCommand.CommandText = "[dbo].[ENC_INSERTAR_SOLICITUD_CONTINGENCIA]";
                p_objCommand.Parameters.Clear();

                //Cargar parametros
                p_objCommand.Parameters.Add("@P_ENCFORMULARIO_ID", SqlDbType.Int).Value = p_objSolicitud.FormularioID;
                p_objCommand.Parameters.Add("@AUT_ID", SqlDbType.Int).Value = p_objSolicitud.AutoridadID;
                p_objCommand.Parameters.Add("@P_SOLICITANTE_ID", SqlDbType.Int).Value = p_objSolicitud.SolicitanteID;
                if (!string.IsNullOrEmpty(p_objSolicitud.Expediente.ExpedienteCodigo))
                    p_objCommand.Parameters.Add("@P_CODIGO_EXPEDIENTE", SqlDbType.VarChar).Value = p_objSolicitud.Expediente.ExpedienteCodigo;
                p_objCommand.Parameters.Add("@P_ENCSECTOR_ID", SqlDbType.VarChar).Value = p_objSolicitud.SectorID;
                if (!string.IsNullOrEmpty(p_objSolicitud.Expediente.ExpedienteNombre))
                    p_objCommand.Parameters.Add("@P_NOMBRE_PROYECTO", SqlDbType.VarChar).Value = p_objSolicitud.Expediente.ExpedienteNombre;
                p_objCommand.Parameters.Add("@P_NUMERO_VITAL", SqlDbType.VarChar).Value = p_objSolicitud.NumeroVital;
                p_objCommand.Parameters.Add("@P_NUMERO_VITAL_PADRE", SqlDbType.VarChar).Value = p_objSolicitud.NumeroVital;
                p_objCommand.Parameters.Add("@P_NOMBRE_RESPONSABLE", SqlDbType.VarChar).Value = p_objSolicitud.NombreResponsable;
                p_objCommand.Parameters.Add("@P_TELEFONO_RESPONSABLE", SqlDbType.VarChar).Value = p_objSolicitud.NumeroTelefonicoResponsable;
                p_objCommand.Parameters.Add("@P_EMAIL_RESPONSABLE", SqlDbType.VarChar).Value = p_objSolicitud.EmailResponsable;
                p_objCommand.Parameters.Add("@P_ENCETAPA_PROYECTO_CONTINGENCIA_ID", SqlDbType.Int).Value = p_objSolicitud.EtapaProyecto.EtapaProyectoID;
                p_objCommand.Parameters.Add("@P_ETAPA_PROYECTO_OTRO", SqlDbType.VarChar).Value = p_objSolicitud.EtapaProyectoOtro;
                p_objCommand.Parameters.Add("@P_ENCNIVELEMERGENCIACONTINGENCIA_ID", SqlDbType.Int).Value = p_objSolicitud.NivelEmergenciaContingenciaID;

                //Ejecuta sentencia
                using (IDataReader reader = p_objCommand.ExecuteReader())
                {
                    //Cargar id del certificado
                    if (reader.Read())
                    {
                        intSolicitudID = Convert.ToInt32(reader["ENCSOLICITUDCONTINGENCIA_ID"]);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudCambioMenorDalc :: GuardarSolicitud -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudCambioMenorDalc :: GuardarSolicitud -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return intSolicitudID;
        }



        #endregion


        #region  Metodos Publicos


        /// <summary>
        /// Insertar la solicitud de cambio menor
        /// </summary>
        /// <param name="p_objSolicitud">SolicitudCambioMenorEntity con la información de la solicitud. Ingresa por referencia para cargue de identificadores</param>
        public void InsertarSolicitudContingencias(ref SolicitudContingenciasEntity p_objSolicitud)
        {
            PreguntaSolicitudContingenciasDalc objPreguntaSolicitudContingenciasEntity = null;
            OpcionPreguntaSolicitudContingenciasDalc objOpcionPreguntaSolicitudContingenciasDalc = null;
            CoordenadasPreguntaSolicitudContingenciasDalc objCoordenadasPreguntaSolicitudContingenciasDalc = null;
            LocalizacionPreguntaSolicitudContingenciasDalc objLocalizacionPreguntaSolicitudContingenciasDalc = null;
            DocumentoPreguntaSolicitudContingenciasDalc objDocumentoPreguntaSolicitudContingenciasDalc = null;
            RespuestaPreguntaSolicitudContingenciasDalc objRespuestaPreguntaSolicitudContingenciasDalc = null;

            SqlConnection objConnection = null;
            SqlTransaction objTransaccion = null;
            SqlCommand objCommand = null;

            try
            {
                //Cargar conexion
                objConnection = new SqlConnection(this._objConfiguracion.SilpaCnx.ToString());

                using (objConnection)
                {
                    //Abrir conexion
                    objConnection.Open();

                    try
                    {
                        //Comenzar transaccion
                        objTransaccion = objConnection.BeginTransaction("InsertarSolicitudContingencias");

                        //Crear comando
                        objCommand = objConnection.CreateCommand();
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.Connection = objConnection;
                        objCommand.Transaction = objTransaccion;

                        //Guardar solicitud
                        p_objSolicitud.SolicitudContingenciasID = this.GuardarSolicitudContingencias(objCommand, p_objSolicitud);

                        //Verificar que se obtenga el identificador de la solicitud
                        if (p_objSolicitud.SolicitudContingenciasID > 0)
                        {
                            //Guardar el listado de preguntas
                            if (p_objSolicitud.Preguntas != null && p_objSolicitud.Preguntas.Count > 0)
                            {
                                //Crear objeto para manejo de datos de preguntas
                                objPreguntaSolicitudContingenciasEntity = new PreguntaSolicitudContingenciasDalc();

                                int preguntaSolicitudContingenciasID;

                                //Ciclo que almacena información
                                foreach (PreguntaSolicitudContingenciasEntity objPregunta in p_objSolicitud.Preguntas)
                                {
                                    //Asignar identificador
                                    objPregunta.PreguntaSolicitudID = p_objSolicitud.SolicitudContingenciasID;

                                    //Guardar registro coordenadas                                       
                                    preguntaSolicitudContingenciasID = objPreguntaSolicitudContingenciasEntity.InsertarPreguntaSolicitudContingencias(objCommand, objPregunta);

                                    if (preguntaSolicitudContingenciasID > 0)
                                    {
                                        foreach (OpcionPreguntaSolicitudContingenciasEntity objOpcionPreguntaSolicitudContingencias in objPregunta.OpcionesPregunta)
                                        {
                                            objOpcionPreguntaSolicitudContingenciasDalc = new OpcionPreguntaSolicitudContingenciasDalc();
                                            objOpcionPreguntaSolicitudContingencias.SolicitudID = p_objSolicitud.SolicitudContingenciasID;
                                            objOpcionPreguntaSolicitudContingencias.PreguntaSolicitudID = preguntaSolicitudContingenciasID;
                                            objOpcionPreguntaSolicitudContingenciasDalc.InsertarOpcionPreguntaSolicitudContingencias(objCommand, objOpcionPreguntaSolicitudContingencias);
                                        }

                                        foreach (CoordenadasPreguntaSolicitudContingenciasEntity objCoordenadasPreguntaSolicitudContingenciasEntity in objPregunta.CoordenadasPregunta)
                                        {
                                            objCoordenadasPreguntaSolicitudContingenciasDalc = new CoordenadasPreguntaSolicitudContingenciasDalc();
                                            objCoordenadasPreguntaSolicitudContingenciasEntity.SolicitudID = p_objSolicitud.SolicitudContingenciasID;
                                            objCoordenadasPreguntaSolicitudContingenciasEntity.PreguntaSolicitudID = preguntaSolicitudContingenciasID;
                                            objCoordenadasPreguntaSolicitudContingenciasDalc.InsertarCoordenadasPreguntaSolicitudContingencias(objCommand, objCoordenadasPreguntaSolicitudContingenciasEntity);
                                        }

                                        foreach (LocalizacionPreguntaSolicitudContingenciasEntity objLocalizacionPreguntaSolicitudContingenciasEntity in objPregunta.LocalizacionesPregunta)
                                        {
                                            objLocalizacionPreguntaSolicitudContingenciasDalc = new LocalizacionPreguntaSolicitudContingenciasDalc();
                                            objLocalizacionPreguntaSolicitudContingenciasEntity.SolicitudID = p_objSolicitud.SolicitudContingenciasID;
                                            objLocalizacionPreguntaSolicitudContingenciasEntity.PreguntaSolicitudID = preguntaSolicitudContingenciasID;
                                            objLocalizacionPreguntaSolicitudContingenciasDalc.InsertarLocalizacionPreguntaSolicitudContingencias(objCommand, objLocalizacionPreguntaSolicitudContingenciasEntity);
                                        }

                                        foreach (DocumentoPreguntaSolicitudContingenciasEntity objDocumentoPreguntaSolicitudContingenciasEntity in objPregunta.DocumentosPregunta)
                                        {
                                            objDocumentoPreguntaSolicitudContingenciasDalc = new DocumentoPreguntaSolicitudContingenciasDalc();
                                            objDocumentoPreguntaSolicitudContingenciasEntity.SolicitudID = p_objSolicitud.SolicitudContingenciasID;
                                            objDocumentoPreguntaSolicitudContingenciasEntity.PreguntaSolicitudID = preguntaSolicitudContingenciasID;
                                            objDocumentoPreguntaSolicitudContingenciasDalc.InsertarDocumentoPreguntaSolicitudContingencias(objCommand, objDocumentoPreguntaSolicitudContingenciasEntity);
                                        }


                                        foreach (RespuestaPreguntaSolicitudContingenciasEntity objRespuestaPreguntaSolicitudContingenciasEntity in objPregunta.RespuestasPregunta)
                                        {
                                            objRespuestaPreguntaSolicitudContingenciasDalc = new RespuestaPreguntaSolicitudContingenciasDalc();
                                            objRespuestaPreguntaSolicitudContingenciasEntity.SolicitudID = p_objSolicitud.SolicitudContingenciasID;
                                            objRespuestaPreguntaSolicitudContingenciasEntity.PreguntaSolicitudID = preguntaSolicitudContingenciasID;
                                            objRespuestaPreguntaSolicitudContingenciasDalc.InsertarRespuestaPreguntaSolicitudContingencias(objCommand, objRespuestaPreguntaSolicitudContingenciasEntity);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("No se encontro preguntas en el custionario para ser almacenadas");
                            }
                        }
                        else
                        {
                            throw new Exception("No se obtuvo el identificador de la solicitud");
                        }

                        //Realizar Commit de la transaccion
                        objTransaccion.Commit();
                    }
                    catch (SqlException sqle)
                    {
                        //Realizar rollback
                        objTransaccion.Rollback();

                        //Escalar exc
                        throw sqle;
                    }
                    catch (Exception exc)
                    {
                        //Realizar rollback
                        objTransaccion.Rollback();

                        //Escalar exc
                        throw exc;
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudContingenciasDalc :: InsertarSolicitudContingencias -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudContingenciasDalc :: InsertarSolicitudContingencias -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                try
                {
                    objConnection.Close();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "SolicitudContingenciasDalc :: InsertarSolicitudContingencias -> Error bd cerrando conexión: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }
        }

        /// <summary>
        /// Actualizar el número de vital asociado a una solicitud
        /// </summary>
        /// <param name="p_intSolicitudID">int con el identificador de la solicitud</param>
        /// <param name="p_strNumeroVital">string con el número vital</param>
        public void ActualizarNumeroVitalSolicitudContingencias(int p_intSolicitudID, string p_strNumeroVital)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("ENC_ACTUALIZAR_NUMERO_VITAL_SOLICITUD_CONTINGENCIA");
                objDataBase.AddInParameter(objCommand, "@P_ENCSOLICITUDCONTINGENCIA_ID", DbType.Int32, p_intSolicitudID);
                objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVital);

                //Realizar actualización
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudContingenciasDalc :: ActualizarNumeroVitalSolicitudContingencias -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudContingenciasDalc :: ActualizarNumeroVitalSolicitudContingencias -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
        }



        /// <summary>
        /// Mover el registro de solicitud a tablas de error
        /// </summary>
        /// <param name="p_intSolicitudID">int con el identificador de la solicitud</param>
        /// <param name="p_strMensajeError">string con el mensaje de error</param>
        public void MoverSolicitudError(int p_intSolicitudID, string p_strMensajeError)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("CM_ELIMINAR_SOLICITUD_CAMBIO_MENOR_ERROR");
                objDataBase.AddInParameter(objCommand, "@P_CMSOLICITUDCAMBIOMENOR_ID", DbType.Int32, p_intSolicitudID);
                objDataBase.AddInParameter(objCommand, "@P_MENSAJE_ERROR", DbType.String, p_strMensajeError);

                //Realizar actualización
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudCambioMenorDalc :: MoverSolicitudError -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudCambioMenorDalc :: MoverSolicitudError -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
        }


        #endregion

    }
}
