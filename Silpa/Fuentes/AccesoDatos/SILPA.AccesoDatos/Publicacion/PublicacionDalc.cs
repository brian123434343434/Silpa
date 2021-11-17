using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.SqlClient;
using SoftManagement.Log;  

namespace SILPA.AccesoDatos.Publicacion
{
    public class PublicacionDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public PublicacionDalc()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Lista las publicaciones existententes en la base de datos segun los registros que apliquen.
        /// </summary>
        /// <param name="intIdPublicacion">Identificador de la publicacion</param>
        /// <param name="intIdTramite">Identificador del tramite</param>
        /// <param name="intIdAutAmbiental">Identificador de la autoridad ambiental</param>
        /// <param name="intIdSector">Identificador del sector</param>
        /// <param name="strNombreExpediente">Nombre del expediente</param>        
        /// <param name="intIdTipoActoAdmin">Identificador del tipo de acto administrativo</param>
        /// <param name="intIdActoAdministrativo">Identificador del acto administrativo</param>
        /// <param name="strNumDocumento">Numero del documento</param>
        /// <param name="intIdExpediente">Identificador del expediente</param>
        /// <param name="strFechaDesde">Fecha inicial de consulta</param>
        /// <param name="strFechaHasta">Fecha final de consulta</param>
        /// <param name="intPublicacionID">Identificador publicacion</param>
        /// <returns>DataSet con los registros y las siguientes columnas: 
        /// ID_PUBLICACION - RUTA_PUB - TITULO_PUB - VIGENCIA_PUB - ID_TRAMITE - TIPO_TRAMITE - AUT_ID - AUTORIDAD_AMBIENTAL - 
        /// SEC_ID - SECTOR_NOMBRE - EXP_ID - EXP_NOMBRE - TAAD_ID - TIPO_ACTO_ADM - AAD_ID - ACTO_ADMINISTRATIVO - 
        /// NUM_DOCUMENTO - FECHA_FIJACION -  FECHA_DESFIJACION -  TIPO_FIJACION - TIPO_PUBLICACION - FECHA_EXPEDICION - DESCRIPCION_PUB - ID_PUB_AA  
        /// </returns>
        public DataSet ListarPublicacion(Nullable<Int64> intIdPublicacion, Nullable<int> intIdTramite, Nullable<int> intIdAutAmbiental,
            Nullable<int> intIdSector, string strNombreExpediente, Nullable<int> intIdTipoActoAdmin,
            string strNumDocumento, string strIdExpediente,
            string strFechaDesde,string strFechaHasta, Nullable<int> intPublicacionID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intIdPublicacion, intIdTramite, intIdAutAmbiental, intIdSector,   
                    strNombreExpediente,intIdTipoActoAdmin, strNumDocumento,strIdExpediente, 
                    strFechaDesde, strFechaHasta, intPublicacionID };
                DbCommand cmd = db.GetStoredProcCommand("SIH_LISTA_PUBLICACION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Consultar una publicación especifica
        /// </summary>
        /// <param name="p_lngIdPublicacion">long con el id de la publicación</param>
        /// <returns>DataSet con la información de la publicación</returns>
        public DataSet ConsultarPublicacion(long p_lngIdPublicacion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { p_lngIdPublicacion };
                DbCommand cmd = db.GetStoredProcCommand("SIH_CONSULTAR_PUBLICACION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Lista los comentarios existententes en la base de datos segun los registros que apliquen.
        /// </summary>
        /// <param name="intIdComentario">Identificador del comentario</param>
        /// <param name="intIdPublicacion">Identificador de la publicacion</param>
        /// <returns>DataSet con los registros y las siguientes columnas: 
        /// ID_COMENTARIO - TEXTO_COMENTARIO - FECHA_COMENTARIO
        /// </returns>
        public DataSet ListarComentario(Nullable<int> intIdComentario, Nullable<Int64> intIdPublicacion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intIdComentario, intIdPublicacion };
                DbCommand cmd = db.GetStoredProcCommand("SIH_LISTA_COMENTARIO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Inserta una publicación en la base de datos
        /// </summary>
        /// <param name="objIdentity">Identidad de la publicacion</param>
        public long InsertarPublicacion(ref PublicacionIdentity objIdentity)
        {
            long lngPublicacionID = -1;
            DataSet objInformacion;

            try
            {
	            /*
	             * @P_RUTA_PUB VARCHAR(2000), 
					@P_TITULO_PUB NVARCHAR(500),
					@P_ID_TRAMITE NUMERIC, 
					@P_AUT_ID NUMERIC, 
					@P_EXP_NOMBRE NVARCHAR(800), 
					@P_TAA_ID NUMERIC,
					 @P_AAD_ID NUMERIC, 
					@P_NUM_DOCUMENTO NVARCHAR(10)='',
					 @P_EXP_ID NUMERIC, 
					@P_FECHA_DES NVARCHAR(10)='', 
					@P_TPU_ID NUMERIC,
					@P_DESCRIPCION_PUB NVARCHAR(800), 
					@P_NOTIFICACION NVARCHAR,
					@P_ID_RELACIONA_PUBLICACION BIGINT = NULL,
					@P_EXP_CODIGO NVARCHAR(75) = ''
	             */
	
	            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
	            object[] parametros = new object[]
	                       {
	                           objIdentity.RutaPublicacion, 
	                           objIdentity.TitutloPublicacion, 
	                           objIdentity.IdTramite, 
	                           objIdentity.IdAutoridad,
	                           objIdentity.NombreExpediente, //cambiado
	                           objIdentity.IdTipoActoAdministrativo, //cambiado
	                           objIdentity.IdActoAdministrativo, //cambiado
	                           objIdentity.NumeroDocumento,	 // cambiado
	                           objIdentity.IdExpediente,
	                           objIdentity.FechaDesfijacion,  //ESTA ES LA FECHA DE DESFIJACION 
	                           objIdentity.IdTipoPublicacion,
	                           objIdentity.DescripcionPublicacion,
	                           objIdentity.Notificacion,
	                           objIdentity.IdRelacionaPublicacion,
	                           objIdentity.CodigoExpediente
	                        };
	
	            DbCommand cmd = db.GetStoredProcCommand("SIH_ADICIONAR_PUBLICACION", parametros);
                objInformacion = db.ExecuteDataSet(cmd);

                if (objInformacion != null && objInformacion.Tables.Count > 0 && objInformacion.Tables[0].Rows.Count > 0)
                    lngPublicacionID = Convert.ToInt64(objInformacion.Tables[0].Rows[0]["ID_PUBLICACION"]);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Insertar Publicación.";
                throw new Exception(strException, ex);
            }

            return lngPublicacionID;
        }


        /// <summary>
        /// Elimina una publicación, la inactiva en la base de datos
        /// </summary>
        /// <param name="idPublicacion">Identificador de la publicacion</param>
        public void EliminarPublicacion(long idPublicacion)
        {         
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                       {
                           idPublicacion
                        };

                DbCommand cmd = db.GetStoredProcCommand("SIH_ELIMINAR_PUBLICACION", parametros);
                db.ExecuteNonQuery(cmd);           
        }


        /// <summary>
        /// Actualiza una publicación que se encuentra no publicada porque espera respuesta desde notificación
        /// Electrónica
        /// </summary>
        public void ActualizarPublicacionPorNotificacion(Decimal Id_Acto_Notificacion) 
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { Id_Acto_Notificacion };
            DbCommand cmd = db.GetStoredProcCommand("PUB_ACTUALIZAR_PUBLICACION", parametros);
            db.ExecuteNonQuery(cmd);   
        }

        /// <summary>
        /// Actualiza la fecha de desfijacion de la pubi=licacion actual
        /// </summary>
        /// <param name="p_lngPublicacionID">long con el identificador de la publicacion</param>
        /// <param name="p_objFechaDesfijacion">DateTime con la fecha de desfijacion</param>
        public void ActualizarDesfijarPublicacion(long p_lngPublicacionID, DateTime p_objFechaDesfijacion)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_lngPublicacionID, p_objFechaDesfijacion };
            DbCommand cmd = db.GetStoredProcCommand("PUB_ACTUALIZAR_FECHA_DESFIJACION_PUBLICACION", parametros);
            db.ExecuteNonQuery(cmd);
        }
    }
}
