using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Xml.Serialization;
using SILPA.AccesoDatos.Encuestas.Contingencias.Entity;
using SILPA.AccesoDatos.Encuestas.Contingencias.Dalc;
using SILPA.AccesoDatos.BPMProcess;
using SILPA.LogicaNegocio.Excepciones;
using SILPA.LogicaNegocio.BPMProcessL;
using SILPA.LogicaNegocio.Generico;
using SoftManagement.Log;


namespace SILPA.LogicaNegocio.Encuestas.Contingencias
{
    public class SolicitudContingencias
    {
        #region  Objetos
        SolicitudContingenciasDalc _objSolicitudContingenciasDalc;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public SolicitudContingencias()
        {
            //Creary cargar configuración
            this._objSolicitudContingenciasDalc = new SolicitudContingenciasDalc();
        }

        #endregion

        #region  Metodos Publicos

        /// <summary>
        /// Inserta una solicitud de contingencias
        /// </summary>
        /// <param name="p_objSolicitud">Objeto tipo SolicitudContingenciasEntity</param>

        public string InsertarSolicitudContingencias(ref SolicitudContingenciasEntity p_objSolicitud)
        {
            string numeroVital = string.Empty;
            try
            {
                //Almacenar la información de la solicitud
                this._objSolicitudContingenciasDalc.InsertarSolicitudContingencias(ref p_objSolicitud);

                //Verificar que se haya insertado correctamente la información
                if (p_objSolicitud.SolicitudContingenciasID > 0)
                {
                    //Radicar solicitud en VITAL
                    numeroVital = this.RegistroVital(p_objSolicitud);
                    p_objSolicitud.NumeroVital = numeroVital ;

                    //Actualizar el número vital en el sistema
                    this.ActualizarNumeroVitalSolicitudContingencia(p_objSolicitud.SolicitudContingenciasID, p_objSolicitud.NumeroVital);

                    //Mover archivos de carpeta temporales a carpeta vital
                    this.MoverArchivosCarpetaVital(ref p_objSolicitud);
                }
                else
                {
                    throw new Exception("No se inserto la información de la solicitud de manera correcta");
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudContingencias :: InsertarSolicitudContingencias -> Error: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            return numeroVital;
        }
        #endregion

        #region  Metodos Privados

        /// <summary>
        /// Realizar el registro de la solicitud en VITAL
        /// </summary>
        /// <param name="p_objSolicitud">SolicitudContingenciasEntity con la información de la solicitud</param>
        /// <returns>string con el numero vital</returns>
        private string RegistroVital(SolicitudContingenciasEntity p_objSolicitud)
        {
            BpmProcessLn objProceso = new BpmProcessLn();
            string strNumeroVital = "";

            try
            {
                //Crear proceso
                objProceso = new BpmProcessLn();

                //Verificar en pruebas
                strNumeroVital = objProceso.crearProceso(ConfigurationManager.AppSettings["BPMSolicitudContingenciasClientID"].ToString(),
                                                         Convert.ToInt64(ConfigurationManager.AppSettings["FormularioSolicitudContingencias"].ToString()),
                                                         p_objSolicitud.SolicitanteID,
                                                         this.CrearXmlVital(p_objSolicitud));
                //strNumeroVital = "980000801507" + (new Random().Next(6000000, 6099999)).ToString(); 

                //Verificar si se obtuvo el número vital
                if (string.IsNullOrEmpty(strNumeroVital) || strNumeroVital.ToLower().Contains("error"))
                {
                    throw new Exception("No se obtuvo número vital para proceso de Solicitud Contingencias" + (!string.IsNullOrEmpty(strNumeroVital) && strNumeroVital.ToLower().Contains("error") ? " - " + strNumeroVital : ""));
                }
            }
            catch (BPMException exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudContingencias :: RegistroVital -> Error realizando la radicación: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw new RadicacionSolicitudContingenciaException("SolicitudContingencias :: RegistroVital -> Error Realizando radicación: " + exc.Message, exc.InnerException);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudContingencias :: RegistroVital -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw new RadicacionSolicitudContingenciaException("SolicitudContingencias :: RegistroVital -> Error Inesperado: " + exc.Message, exc.InnerException);
            }

            return strNumeroVital;
        }

        /// <summary>
        /// Crear objeto identity para cargar en XML
        /// </summary>
        /// <param name="pintId">int con el ID</param>
        /// <param name="p_strGrupo">string con el nombre del grupo</param>
        /// <param name="p_strValor">string con el valor</param>
        /// <param name="p_intOrden">int con el orden</param>
        /// <param name="p_objArchivo">Arreglo de bytes con archivo</param>
        /// <returns></returns>
        private ValoresIdentity CargarValores(int pintId, string p_strGrupo, string p_strValor, int p_intOrden, Byte[] p_objArchivo)
        {
            ValoresIdentity objValores = new ValoresIdentity();
            objValores.Id = pintId;
            objValores.Grupo = p_strGrupo;
            objValores.Valor = p_strValor;
            objValores.Orden = p_intOrden;
            objValores.Archivo = p_objArchivo;
            return objValores;
        }


        /// <summary>
        /// Crear el XML requerido para crear formulario en Vital
        /// </summary>
        /// <param name="p_objSolicitud">SolicitudContingenciasEntity con la información de la solicitud</param>
        /// <returns>string con el XML</returns>
        private string CrearXmlVital(SolicitudContingenciasEntity p_objSolicitud)
        {
            List<ValoresIdentity> objValoresList = new List<ValoresIdentity>();
            objValoresList.Add(CargarValores(1, "Bas", p_objSolicitud.AutoridadID.ToString(), 1, new Byte[1]));
            objValoresList.Add(CargarValores(2, "Bas", p_objSolicitud.Expediente.ExpedienteCodigo, 1, new Byte[1]));
            objValoresList.Add(CargarValores(3, "Bas", p_objSolicitud.SolicitudContingenciasID.ToString(), 1, new Byte[1]));
            objValoresList.Add(CargarValores(4, "Bas", p_objSolicitud.NivelEmergenciaContingenciaID.ToString(), 1, new Byte[1]));
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer serializador = new XmlSerializer(typeof(List<ValoresIdentity>));
            serializador.Serialize(memoryStream, objValoresList);
            string xml = System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
            return xml;
        }

        /// <summary>
        /// Actualizar el número de vital asociado a una solicitud
        /// </summary>
        /// <param name="p_intSolicitudID">int con el identificador de la solicitud</param>
        /// <param name="p_strNumeroVital">string con el número vital</param>
        private void ActualizarNumeroVitalSolicitudContingencia(int p_intSolicitudID, string p_strNumeroVital)
        {
            this._objSolicitudContingenciasDalc.ActualizarNumeroVitalSolicitudContingencias(p_intSolicitudID, p_strNumeroVital);
        }

        /// <summary>
        /// Mover archivos asociados a la solicitud de carpeta temporal a la carpeta definitiva
        /// </summary>
        /// <param name="p_objSolicitud">SolicitudContingenciasEntity con la informacion de la solicitud</param>
        private void MoverArchivosCarpetaVital(ref SolicitudContingenciasEntity p_objSolicitud)
        {
            DocumentoSolicitudContingencias DocumentoSolicitudContingencias = null;
            RadicacionDocumento objRadicacionDocumento = null;
            FileInfo objArchivo = null;
            string strCarpeta = "";

            try
            {
                //Verificar que se tenga documentos para mover
                if (p_objSolicitud.Preguntas != null && p_objSolicitud.Preguntas.Count > 0)
                {

                    //Obtener carpeta vital
                    objRadicacionDocumento = new RadicacionDocumento();
                    strCarpeta = objRadicacionDocumento.ObtenerPathDocumentosNumeroVital(p_objSolicitud.NumeroVital);

                    //Verificar que se halla obtenido path, en caso contrario mover a carpeta traffic ruta numero vital
                    if (string.IsNullOrEmpty(strCarpeta))
                    {
                        strCarpeta = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + string.Format(@"{0}\{1}\", ConfigurationManager.AppSettings["CarpetaTemporalSolicitudContingenciasError"].ToString(), p_objSolicitud.NumeroVital);
                    }

                    //Verificar que la carpeta exista sino crearla
                    if (!Directory.Exists(strCarpeta))
                    {
                        Directory.CreateDirectory(strCarpeta);
                    }

                    //Crear el objeto manejo de datos
                    DocumentoSolicitudContingencias = new DocumentoSolicitudContingencias();

                    //Ciclo que mueve documentos declaracion de conformidad
                     foreach (PreguntaSolicitudContingenciasEntity objPregunta in p_objSolicitud.Preguntas)
                     {
                         foreach (DocumentoPreguntaSolicitudContingenciasEntity objDocumentoPreguntaSolicitudContingenciasEntity in objPregunta.DocumentosPregunta)
                         {
                             //Crear referencia a archivo
                             objArchivo = new FileInfo(objDocumentoPreguntaSolicitudContingenciasEntity.Ubicacion + objDocumentoPreguntaSolicitudContingenciasEntity.NombreDocumento);
                             //Copiar archivo a ruta
                             objArchivo.CopyTo(strCarpeta + objDocumentoPreguntaSolicitudContingenciasEntity.NombreDocumento);

                             //Actualizar la ruta en la base de datos                        
                             DocumentoSolicitudContingencias.ActualizarUbicacionDocumentoSolicitudContingencia(objDocumentoPreguntaSolicitudContingenciasEntity.DocumentoPreguntaSolicitudID, strCarpeta);
                         }
                    }
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudContingencias :: MoverArchivosCarpetaVital -> Error Inesperado moviendo archivos de la solicitud " + p_objSolicitud.SolicitudContingenciasID + ": " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw new DocumentoSolicitudContingenciaException("SolicitudContingencias :: MoverArchivosCarpetaVital -> Error Inesperado: " + exc.Message, exc.InnerException);
            }
        }
        #endregion

    }
}
