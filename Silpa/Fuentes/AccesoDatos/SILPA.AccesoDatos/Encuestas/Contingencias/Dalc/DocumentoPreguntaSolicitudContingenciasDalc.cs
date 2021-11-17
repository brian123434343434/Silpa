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
    public class DocumentoPreguntaSolicitudContingenciasDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public DocumentoPreguntaSolicitudContingenciasDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion

        #region  Metodos Privados

        /// <summary>
        /// Guarda la información del Documento asociado a una Pregunta de una Solicitud de Contingencias.
        /// </summary>
        /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
        /// <param name="p_objDocumentoPreguntaSolicitudContingenciasEntity">Objeto con la informacion de la Pregunta con la información de la Documento asociado a una Pregunta de una Solicitud de Contingencias</param>
        /// <returns>int con el identifcador de la solicitud creada</returns>
        public int InsertarDocumentoPreguntaSolicitudContingencias(SqlCommand p_objCommand, DocumentoPreguntaSolicitudContingenciasEntity p_objDocumentoPreguntaSolicitudContingenciasEntity)
        {
            int intSolicitudID = 0;

            try
            {
                p_objCommand.CommandText = "[dbo].[ENC_INSERTAR_SOLICITUD_DOCUMENTO_PREGUNTA_CONTINGENCIA]";
                p_objCommand.Parameters.Clear();

                //Cargar parametros
                p_objCommand.Parameters.Add("@P_ENCSOLICITUDPREGUNTACONTINGENCIA_ID", SqlDbType.Int).Value = p_objDocumentoPreguntaSolicitudContingenciasEntity.PreguntaSolicitudID;
                p_objCommand.Parameters.Add("@P_ENCSOLICITUDCONTINGENCIA_ID", SqlDbType.Int).Value = p_objDocumentoPreguntaSolicitudContingenciasEntity.SolicitudID;
                p_objCommand.Parameters.Add("@P_ENCPREGUNTA_ID", SqlDbType.Int).Value = p_objDocumentoPreguntaSolicitudContingenciasEntity.Pregunta.PreguntaID;
                p_objCommand.Parameters.Add("@P_UBICACION_DOCUMENTO", SqlDbType.VarChar).Value = p_objDocumentoPreguntaSolicitudContingenciasEntity.Ubicacion;
                p_objCommand.Parameters.Add("@P_NOMBRE_DOCUMENTO_USUARIO", SqlDbType.VarChar).Value = p_objDocumentoPreguntaSolicitudContingenciasEntity.NombreDocumentoUsuario;
                p_objCommand.Parameters.Add("@P_NOMBRE_DOCUMENTO", SqlDbType.VarChar).Value = p_objDocumentoPreguntaSolicitudContingenciasEntity.NombreDocumento;

                //Ejecuta sentencia
                using (IDataReader reader = p_objCommand.ExecuteReader())
                {
                    //Cargar id del certificado
                    if (reader.Read())
                    {
                        intSolicitudID = Convert.ToInt32(reader["ENCSOLICITUDDOCUMENTOPREGUNTACONTINGENCIA_ID"]);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DocumentoPreguntaSolicitudContingenciasDalc :: GuardarDocumentoPreguntaSolicitudContingencias -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DocumentoPreguntaSolicitudContingenciasDalc :: GuardarDocumentoPreguntaSolicitudContingencias -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return intSolicitudID;
        }

        /// <summary>
        /// Actualizar el número de vital asociado a una solicitud
        /// </summary>
        /// <param name="p_intSolicitudID">int con el identificador de la solicitud</param>
        /// <param name="p_strNumeroVital">string con el número vital</param>
        public void ActualizarUbicacionDocumentoSolicitudContingencia(int p_intDocumentoPreguntaContingenciaSolicitudID, string p_strUbicacion)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("ENC_ACTUALIZAR_UBICACION_SOLICITUD_DOCUMENTO_CONTINGENCIA");
                objDataBase.AddInParameter(objCommand, "@P_ENCSOLICITUDDOCUMENTOPREGUNTACONTINGENCIA_ID", DbType.Int32, p_intDocumentoPreguntaContingenciaSolicitudID);
                objDataBase.AddInParameter(objCommand, "@P_UBICACION_DOCUMENTO", DbType.String, p_strUbicacion);

                //Realizar actualización
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DocumentoPreguntaSolicitudContingenciasDalc :: ActualizarUbicacionDocumentoSolicitudContingencia -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DocumentoPreguntaSolicitudContingenciasDalc :: ActualizarUbicacionDocumentoSolicitudContingencia -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
        }

        #endregion
    }
}
