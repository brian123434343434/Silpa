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
    public class DocumentoSolicitudREADalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public DocumentoSolicitudREADalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion
            #region  Metodos Publicos

            /// <summary>
            /// Guardar la información del documento
            /// </summary>
            /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
            /// <param name="p_objSolicitud">PreguntaSolicitudCambioMenorEntity con la información de la pregunta</param>
            /// <param name="p_intSolicitudREAID">Solicitud REA ID</param>
            public void InsertarDocumentoSolicitudREA(SqlCommand p_objCommand, DocumentoSolicitudREAEntity p_objDocumentoSolicitudREAEntity, int p_intSolicitudREAID)
            {
                try
                {
                    p_objCommand.CommandText = "REASP_INSERTAR_DOCUMENTO_SOLICITUD_REA";
                    p_objCommand.Parameters.Clear();

                    //Cargar parametros
                    p_objCommand.Parameters.Add("@P_SOLICITUD_EVALUACION_REA_ID", SqlDbType.Int).Value = p_intSolicitudREAID;
                    p_objCommand.Parameters.Add("@P_TIPO_DOCUMENTO_ID", SqlDbType.Int).Value = p_objDocumentoSolicitudREAEntity.TipoDocumentoID;
                    p_objCommand.Parameters.Add("@P_NOMBRE_DOCUMENTO_USUARIO", SqlDbType.Text).Value = p_objDocumentoSolicitudREAEntity.NombreDocumentoUsuario;
                    p_objCommand.Parameters.Add("@P_NOMBRE_DOCUMENTO", SqlDbType.Text).Value = p_objDocumentoSolicitudREAEntity.NombreDocumento;
                    p_objCommand.Parameters.Add("@P_UBICACION", SqlDbType.Text).Value = p_objDocumentoSolicitudREAEntity.Ubicacion;
                    //Ejecuta sentencia
                    p_objCommand.ExecuteNonQuery();
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DocumentoSolicitudREADalc :: InsertarDocumentoSolicitudREA -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DocumentoSolicitudREADalc :: InsertarDocumentoSolicitudREA -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }

            #endregion

            public void ActualizarUbicacionDocumentoSolicitudREA(int p_intDocumentoID, string p_strUbicacion, int p_intSolicitudREAID)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("REASP_ACTUALIZAR_UBICACION_DOCUMENTO_SOLICITUD_EVALUCIAION_REA");
                    objDataBase.AddInParameter(objCommand, "@P_DOCUMENTO_ID", DbType.Int32, p_intDocumentoID);
                    objDataBase.AddInParameter(objCommand, "@P_UBICACION_DOCUMENTO", DbType.String, p_strUbicacion);
                    objDataBase.AddInParameter(objCommand, "@P_SOLICITUD_EVALUACION_REA_ID", DbType.Int32, p_intSolicitudREAID);

                    //Realizar actualización
                    objDataBase.ExecuteNonQuery(objCommand);
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DocumentoSolicitudREADalc :: ActualizarUbicacionDocumentoSolicitudREA -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DocumentoSolicitudREADalc :: ActualizarUbicacionDocumentoSolicitudREA -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }
    }
}
