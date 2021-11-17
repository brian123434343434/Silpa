using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using SILPA.Comun;
using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Dalc
{
    public class SolicitudREADalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion
        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
        public SolicitudREADalc()
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
        /// <param name="p_objSolicitud">SolicitudREAEntity con la información de la solicitud</param>
        /// <returns>int con el identifcador de la solicitud creada</returns>
        private int GuardarSolicitud(SqlCommand p_objCommand, SolicitudREAEntity p_objSolicitud)
        {
            int intSolicitudID = 0;

            try
            {
                p_objCommand.CommandText = "REASP_INSERTAR_SOLICITUD_REA";
                p_objCommand.Parameters.Clear();

                //Cargar parametros
                p_objCommand.Parameters.Add("@P_SOLICITANTE_ID", SqlDbType.Int).Value = p_objSolicitud.SolicitanteID;
                p_objCommand.Parameters.Add("@P_AUTORIDAD_AMBIENTAL_ID", SqlDbType.Int).Value = p_objSolicitud.AutoridadAmbientalID;
                p_objCommand.Parameters.Add("@P_DURACION_PREMISO", SqlDbType.Int).Value = p_objSolicitud.DuracionPermiso;
                
                //Ejecuta sentencia
                using (IDataReader reader = p_objCommand.ExecuteReader())
                {
                    //Cargar id del certificado
                    if (reader.Read())
                    {
                        intSolicitudID = Convert.ToInt32(reader["SOLICITUD_EVALUACION_REA_ID"]);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudREADalc :: GuardarSolicitud -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudREADalc :: GuardarSolicitud -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
        public void InsertarSolicitudREA(ref SolicitudREAEntity p_objSolicitud)
        {
            InsumoRecoleccionDalc objInsumoRecoleccionDalc = null;
            InsumoPreservacionMovilizacionDalc objInsumoPreservacionMovilizacionDalc = null;
            InsumoProfesionalDalc objInsumoProfesionalDalc = null;
            InsumoCoberturaDalc objInsumoCoberturaDalc = null;
            DocumentoSolicitudREADalc objDocumentoSolicitudREADalc = null;
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
                        objTransaccion = objConnection.BeginTransaction("InsertarSolicitudREA");

                        //Crear comando
                        objCommand = objConnection.CreateCommand();
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.Connection = objConnection;
                        objCommand.Transaction = objTransaccion;

                        //Guardar solicitud
                        p_objSolicitud.SolicitudREAID = this.GuardarSolicitud(objCommand, p_objSolicitud);

                        //Verificar que se obtenga el identificador de la solicitud
                        if (p_objSolicitud.SolicitudREAID > 0)
                        {
                            // guardar la informacion de los insumos por cada grupo biologico

                            if (p_objSolicitud.LstInsumosGrupoBilogocio != null && p_objSolicitud.LstInsumosGrupoBilogocio.Count > 0)
                            {
                                objInsumoRecoleccionDalc = new InsumoRecoleccionDalc();
                                objInsumoPreservacionMovilizacionDalc = new InsumoPreservacionMovilizacionDalc();
                                objInsumoProfesionalDalc = new InsumoProfesionalDalc();
                                
                                foreach (InsumosGrupoBiologicoEntity iInsumosGrupoBiologicoEntity in p_objSolicitud.LstInsumosGrupoBilogocio)
                                {

                                    //guarda la informacion del insumo de recoleccion
                                    foreach (InsumoRecoleccionEntity iInsumoRecoleccionEntity in iInsumosGrupoBiologicoEntity.objLstInsumoRecoleccion)
	                                {
                                        objInsumoRecoleccionDalc.InsertarInsumoRecoleccion(objCommand, iInsumoRecoleccionEntity, p_objSolicitud.SolicitudREAID);
	                                }
                                    //guarda la informacion del insumo de preservacion y movilizacion
                                    foreach (InsumoPreservacionMovilizacionEntity iInsumoPreservacionMovilizacionEntity in iInsumosGrupoBiologicoEntity.objLstInsumoPreservacionMovilizacion)
                                    {
                                        objInsumoPreservacionMovilizacionDalc.InsertarInsumoPreservacionMovilizacion(objCommand, iInsumoPreservacionMovilizacionEntity, p_objSolicitud.SolicitudREAID);
                                    }
                                    //guarda la informacion de insumo de profesionales
                                    foreach (InsumoProfesionalEntity iInsumoProfesionalEntity in iInsumosGrupoBiologicoEntity.ObjLstInsumoProfesional)
                                    {
                                        objInsumoProfesionalDalc.InsertarInsumoProfesional(objCommand, iInsumoProfesionalEntity, p_objSolicitud.SolicitudREAID);
                                    }
                                }
                            }

                            //guardamos la cobertura
                            if (p_objSolicitud.LstIsnumosCobertura != null && p_objSolicitud.LstIsnumosCobertura.Count > 0)
                            {
                                objInsumoCoberturaDalc = new InsumoCoberturaDalc();
                                foreach (InsumoCoberturaEntity iInsumoCoberturaEntity in p_objSolicitud.LstIsnumosCobertura)
                                {
                                    objInsumoCoberturaDalc.InsertarInsumoCobertura(objCommand, iInsumoCoberturaEntity, p_objSolicitud.SolicitudREAID);
                                }
                            }
                            
                            //Guardar la información de archivos
                            if (p_objSolicitud.LstDocumentos != null && p_objSolicitud.LstDocumentos.Count > 0)
                            {
                                //Crear objeto para manejo de datos de documentos
                                objDocumentoSolicitudREADalc = new DocumentoSolicitudREADalc();

                                //Ciclo que almacena información
                                foreach (DocumentoSolicitudREAEntity iDocumentoSolicitudREAEntity in p_objSolicitud.LstDocumentos)
                                {
                                    //Guardar registro
                                    objDocumentoSolicitudREADalc.InsertarDocumentoSolicitudREA(objCommand, iDocumentoSolicitudREAEntity, p_objSolicitud.SolicitudREAID);
                                }
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
                SMLog.Escribir(Severidad.Critico, "SolicitudREADalc :: InsertarSolicitudREA -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudREADalc :: InsertarSolicitudREA -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
                    SMLog.Escribir(Severidad.Critico, "SolicitudREADalc :: InsertarSolicitudREA -> Error bd cerrando conexión: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }
        }

        public void ActualizarNumeroVitalSolicitudREA(int p_intSolicitudREAID, string p_strNumeroVital)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("REASP_ACTUALIZAR_NUMERO_VITAL_SOLICITUD_EVALUCAION_REA");
                objDataBase.AddInParameter(objCommand, "@P_SOLICITUD_EVALUACION_REA_ID", DbType.Int32, p_intSolicitudREAID);
                objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVital);

                //Realizar actualización
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudREADalc :: ActualizarNumeroVitalSolicitudREA -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudREADalc :: ActualizarNumeroVitalSolicitudREA -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
                objCommand = objDataBase.GetStoredProcCommand("REASP_ELIMINAR_SOLICITUD_EVALUACION_REA_ERROR");
                objDataBase.AddInParameter(objCommand, "@P_SOLICITUD_EVALUACION_REA_ID", DbType.Int32, p_intSolicitudID);
                objDataBase.AddInParameter(objCommand, "@P_MENSAJE_ERROR", DbType.String, p_strMensajeError);

                //Realizar actualización
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudREADalc :: MoverSolicitudError -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudREADalc :: MoverSolicitudError -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
        }
        #endregion
    }
}
