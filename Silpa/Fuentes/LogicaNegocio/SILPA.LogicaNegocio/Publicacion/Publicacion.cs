using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Publicacion;
using System.Data;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.LogicaNegocio.Publicacion
{
    public class Publicacion
    {

        public ComentarioIdentity ComIdentity;
        private ComentarioDalc ComDalc;
        public PublicacionIdentity PubIdentity;
        private PublicacionDalc PubDalc;

        /// <summary>
        /// Objeto configuración
        /// </summary>
        private Configuracion _objConfiguracion;

        /// <summary>
        /// Constructor
        /// </summary>
        public Publicacion()
        {
            _objConfiguracion = new Configuracion();
            PubDalc=new PublicacionDalc();
            PubIdentity = new PublicacionIdentity();
            ComDalc = new ComentarioDalc();
            ComIdentity = new ComentarioIdentity();
        }

        /// <summary>
        /// Constructor que inicializa los datos para insertar un comentario
        /// </summary>
        /// <param name="intIdPublicacion">Identificador de la publicacion</param>
        /// <param name="strComentario">Texto del comentario</param>
        public Publicacion(Int64 intIdPublicacion, string strComentario)
        {
            _objConfiguracion = new Configuracion();
            ComDalc = new ComentarioDalc();
            ComIdentity = new ComentarioIdentity();
            ComIdentity.IdPublicacion = intIdPublicacion;
            ComIdentity.TexComentario = strComentario;
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
        /// [ID_PUBLICACION - RUTA_PUB - TITULO_PUB - VIGENCIA_PUB - ID_TRAMITE - TIPO_TRAMITE - AUT_ID - AUTORIDAD_AMBIENTAL - 
        /// SEC_ID - SECTOR_NOMBRE - EXP_ID - EXP_NOMBRE - TAAD_ID - TIPO_ACTO_ADM - AAD_ID - ACTO_ADMINISTRATIVO - 
        /// NUM_DOCUMENTO - FECHA_FIJACION -  FECHA_DESFIJACION -  TIPO_FIJACION - TIPO_PUBLICACION - FECHA_EXPEDICION - DESCRIPCION_PUB - ID_PUB_AA]
        /// </returns>
        public DataSet ListarPublicacion(Nullable<Int64> intIdPublicacion, Nullable<int> intIdTramite, Nullable<int> intIdAutAmbiental,
            Nullable<int> intIdSector, string strNombreExpediente, Nullable<int> intIdTipoActoAdmin,
            string strNumDocumento, string strIdExpediente,
            string strFechaDesde,string strFechaHasta, Nullable<int> intPublicacionID)
        {
            PublicacionDalc obj = new PublicacionDalc();
            return obj.ListarPublicacion(intIdPublicacion, intIdTramite, intIdAutAmbiental, intIdSector, strNombreExpediente,
                    intIdTipoActoAdmin, strNumDocumento, strIdExpediente, strFechaDesde, strFechaHasta, intPublicacionID);
        }


        /// <summary>
        /// Consultar una publicación especifica
        /// </summary>
        /// <param name="p_lngIdPublicacion">long con el id de la publicación</param>
        /// <returns>DataSet con la información de la publicación</returns>
        public DataSet ConsultarPublicacion(long p_lngIdPublicacion)
        {
            PublicacionDalc obj = new PublicacionDalc();
            return obj.ConsultarPublicacion(p_lngIdPublicacion);
        }

        //public DataSet ListarPublicacion(Nullable<Int64> intIdPublicacion, Nullable<int> intIdTramite, Nullable<int> intIdAutAmbiental,
        //   Nullable<int> intIdSector, string strNombreExpediente, Nullable<int> intIdTipoActoAdmin,
        //   Nullable<int> intIdActoAdministrativo, string strNumDocumento, Nullable<Int64> intIdExpediente,
        //   string strFechaDesde, string strFechaHasta, Nullable<int> intPublicacionID)
        //{
        //    PublicacionDalc obj = new PublicacionDalc();
        //    return obj.ListarPublicacion(intIdPublicacion, intIdTramite, intIdAutAmbiental, intIdSector, strNombreExpediente,
        //            intIdTipoActoAdmin, intIdActoAdministrativo, strNumDocumento, intIdExpediente, strFechaDesde, strFechaHasta, intPublicacionID);
        //}



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
            return ComDalc.ListarComentario(intIdComentario,intIdPublicacion);
        }

        /// <summary>
        /// Inserta un comentario en la tabla respectiva.
        /// </summary>        
        public void InsertarComentario()
        {
            ComDalc.InsertarComentario(ref ComIdentity);            
        }

        /// <summary>
        /// Inserta una publicacion en la base de datos.
        /// </summary>
        /// <param name="strXmlDatos">string en formato XML para la insercion de una publicacion</param>
        /// <returns>El identity de Publicacion serializado</returns>
        public long InsertarPublicacion(PublicacionIdentity strXmlDatos)
        {
            long lngPublicacionID = -1;

            try
            {
	            PubIdentity = strXmlDatos;
	
	            List<string> lsNombres = new List<string>();
	            List<byte[]> lsBytes = new List<byte[]>();
	
	            if (PubIdentity.ListaDocumentoAdjuntoType.ListaDocumento != null)
	            {
					this.PubIdentity.RutaPublicacion = this._objConfiguracion.FileTraffic;
					
	                for (int i = 0; i < PubIdentity.ListaDocumentoAdjuntoType.ListaDocumento.Length; i++)
	                {
	                    documentoAdjuntoType doc = (documentoAdjuntoType)PubIdentity.ListaDocumentoAdjuntoType.ListaDocumento[i];
	                    lsNombres.Add(doc.nombreArchivo);
	                    lsBytes.Add(doc.bytes);
	                }
					
	                TraficoDocumento tf = new TraficoDocumento();
	                string rutaPublicacion = string.Empty;
					
	                string fechaArchivo = DateTime.Now.ToString("yyyyMMddmmss");
					
	                tf.RecibirDocumento
	                        (PubIdentity.IdExpediente,
	                            fechaArchivo,
	                            lsBytes,
	                            ref lsNombres,
	                            ref rutaPublicacion
	                         );
	                PubIdentity.RutaPublicacion = rutaPublicacion;
	            }

                lngPublicacionID = PubDalc.InsertarPublicacion(ref PubIdentity);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Insertar Publicación.";
                throw new Exception(strException, ex);
            }

            return lngPublicacionID;
        }

        
        /// <summary>
        /// Método que inserta la publicacion 
        /// </summary>
        /// <param name="strXmlDatos">datos de la publicacion </param>
        /// <param name="xmlListaDocumentos">Listado de arvhivos en formato xml</param>
        /// <returns> XmlRespuesta: con el estado de exito fracaso de la inserción </returns>
        public void InsertarPublicacion(string strXmlDatos, string xmlListaDocumentos)
         {            
            PubIdentity = (PublicacionIdentity)PubIdentity.Deserializar(strXmlDatos);

            ListaDocumentoAdjuntoType lstDoc =  new ListaDocumentoAdjuntoType();
            lstDoc = (ListaDocumentoAdjuntoType)lstDoc.Deserializar(xmlListaDocumentos);

            PubIdentity.ListaDocumentoAdjuntoType = lstDoc;

            List<string> lsNombres      =  new List<string>();
            List<byte[]> lsBytes = new List<byte[]>();


            if (PubIdentity.ListaDocumentoAdjuntoType.ListaDocumento != null)
            {

                this.PubIdentity.RutaPublicacion = this._objConfiguracion.FileTraffic;

                foreach (documentoAdjuntoType doc in lstDoc.ListaDocumento)
                {
                    lsNombres.Add(doc.nombreArchivo);
                    lsBytes.Add(doc.bytes);
                }
                TraficoDocumento tf = new TraficoDocumento();

                string rutaPublicacion = string.Empty;
                
                
                string fechaArchivo = DateTime.Now.ToString("yyyyMMddmmss");

                tf.RecibirDocumento
                        (   PubIdentity.IdExpediente, 
                            fechaArchivo,
                            lsBytes, 
                            ref lsNombres, 
                            ref rutaPublicacion
                         );

                PubIdentity.RutaPublicacion = rutaPublicacion;

            }

            PubDalc.InsertarPublicacion(ref PubIdentity);

            //WSRespuesta result = new WSRespuesta(PubIdentity.IdPublicacion.ToString(), PubIdentity.IdPublicacionAA.ToString(), "001", "Publicación generada exitosamente", true);
           //return result.GetXml();
        }       

        /// <summary>
        ///  Elimina una publicación, la inactiva en la base de datos
        /// </summary>
        /// <param name="idPublicacion">Identificador de la publicacion</param>
         public void EliminarPublicacion(long idPublicacion)
        {
            PublicacionDalc obj = new PublicacionDalc();
            obj.EliminarPublicacion(idPublicacion);           
        }


         /// <summary>
         /// Actualiza la fecha de desfijacion de la pubi=licacion actual
         /// </summary>
         /// <param name="p_lngPublicacionID">long con el identificador de la publicacion</param>
         /// <param name="p_objFechaDesfijacion">DateTime con la fecha de desfijacion</param>
         public void ActualizarDesfijarPublicacion(long p_lngPublicacionID, DateTime p_objFechaDesfijacion)
         {
             PublicacionDalc obj = new PublicacionDalc();
             obj.ActualizarDesfijarPublicacion(p_lngPublicacionID, p_objFechaDesfijacion);
         }

    }
}
