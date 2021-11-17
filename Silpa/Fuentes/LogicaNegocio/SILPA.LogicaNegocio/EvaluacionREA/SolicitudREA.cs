using SILPA.AccesoDatos.BPMProcess;
using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using SILPA.LogicaNegocio.BPMProcessL;
using SILPA.LogicaNegocio.Excepciones;
using SILPA.LogicaNegocio.Generico;
using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class SolicitudREA
    {
        #region  Objetos

        private SolicitudREADalc _objSolicitudREADalc;

        #endregion

         #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
        public SolicitudREA()
            {
                //Creary cargar configuración
                this._objSolicitudREADalc = new SolicitudREADalc();
            }

        #endregion

        #region  Metodos Privados

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
        /// <param name="p_objSolicitud">SolicitudCambioMenorEntity con la información de la solicitud</param>
        /// <returns>string con el XML</returns>
        private string CrearXmlVital(SolicitudREAEntity p_objSolicitud)
        {
            List<ValoresIdentity> objValoresList = new List<ValoresIdentity>();
            objValoresList.Add(CargarValores(1, "Bas", p_objSolicitud.AutoridadAmbientalID.ToString(), 1, new Byte[1]));
            objValoresList.Add(CargarValores(2, "Bas", p_objSolicitud.SolicitudREAID.ToString(), 1, new Byte[1]));
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer serializador = new XmlSerializer(typeof(List<ValoresIdentity>));
            serializador.Serialize(memoryStream, objValoresList);
            string xml = System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
            return xml;
        }


        /// <summary>
        /// Realizar el registro de la solicitud en VITAL
        /// </summary>
        /// <param name="p_objSolicitud">SolicitudCambioMenorEntity con la información de la solicitud</param>
        /// <returns>string con el numero vital</returns>
        private string RegistroVital(SolicitudREAEntity p_objSolicitud)
        {
            BpmProcessLn objProceso = new BpmProcessLn();
            string strNumeroVital = "";

            try
            {
                //Crear proceso
                objProceso = new BpmProcessLn();

                //Verificar en pruebas
                strNumeroVital = objProceso.crearProceso(ConfigurationManager.AppSettings["BPMSolicitudREAClientID"].ToString(),
                                                         Convert.ToInt64(ConfigurationManager.AppSettings["FormularioSolicitudREA"].ToString()),
                                                         p_objSolicitud.SolicitanteID,
                                                         this.CrearXmlVital(p_objSolicitud));
                /*strNumeroVital = "980000801507" + (new Random().Next(6000000, 6099999)).ToString(); */

                //Verificar si se obtuvo el número vital
                if (string.IsNullOrEmpty(strNumeroVital) || strNumeroVital.ToLower().Contains("error"))
                {
                    throw new Exception("No se obtuvo número vital para proceso de cambio menor" + (!string.IsNullOrEmpty(strNumeroVital) && strNumeroVital.ToLower().Contains("error") ? " - " + strNumeroVital : ""));
                }
            }
            catch (BPMException exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudREA :: RegistroVital -> Error realizando la radicación: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw new RadicacionSolicitudREAException("SolicitudREA :: RegistroVital -> Error Realizando radicación: " + exc.Message, exc.InnerException);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudREA :: RegistroVital -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw new RadicacionSolicitudREAException("SolicitudREA :: RegistroVital -> Error Inesperado: " + exc.Message, exc.InnerException);
            }

            return strNumeroVital;
        }


        /// <summary>
        /// Mover archivos asociados a la solicitud de carpeta temporal a la carpeta definitiva
        /// </summary>
        /// <param name="p_objSolicitud">SolicitudCambioMenorEntity con la informacion de la solicitud</param>
        private void MoverArchivosCarpetaVital(SolicitudREAEntity p_objSolicitud)
        {
            DocumentoSolicitudREA objDocumentoSolicitudREA = null;
            RadicacionDocumento objRadicacionDocumento = null;
            FileInfo objArchivo = null;
            string strCarpeta = "";

            try
            {
                //Verificar que se tenga documentos para mover
                if (p_objSolicitud.LstDocumentos != null && p_objSolicitud.LstDocumentos.Count > 0)
                {

                    //Obtener carpeta vital
                    objRadicacionDocumento = new RadicacionDocumento();
                    strCarpeta = objRadicacionDocumento.ObtenerPathDocumentosNumeroVital(p_objSolicitud.NumeroVITAL);

                    //Verificar que se halla obtenido path, en caso contrario mover a carpeta traffic ruta numero vital
                    if (string.IsNullOrEmpty(strCarpeta))
                    {
                        strCarpeta = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + string.Format(@"{0}\{1}\", ConfigurationManager.AppSettings["CarpetaTemporalSolicitudREA"].ToString(), p_objSolicitud.NumeroVITAL);
                    }

                    //Verificar que la carpeta exista sino crearla
                    if (!Directory.Exists(strCarpeta))
                    {
                        Directory.CreateDirectory(strCarpeta);
                    }

                    //Crear el objeto manejo de datos
                    objDocumentoSolicitudREA = new DocumentoSolicitudREA();

                    //Ciclo que mueve documentos declaracion de conformidad
                    foreach (DocumentoSolicitudREAEntity objDocumento in p_objSolicitud.LstDocumentos)
                    {
                        //Crear referencia a archivo
                        objArchivo = new FileInfo(objDocumento.Ubicacion + objDocumento.NombreDocumento);

                        //Copiar archivo a ruta
                        objArchivo.CopyTo(strCarpeta + objDocumento.NombreDocumento);

                        //Actualizar la ruta en la base de datos                        
                        objDocumentoSolicitudREA.ActualizarUbicacionDocumentoSolicitudREA(objDocumento.DocumentoID, strCarpeta, p_objSolicitud.SolicitudREAID);
                    }
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudREA :: MoverArchivosCarpetaVital -> Error Inesperado moviendo archivos de la solicitud " + p_objSolicitud.SolicitudREAID + ": " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw new DocumentoSolicitudCambiosMenoresException("SolicitudREA :: MoverArchivosCarpetaVital -> Error Inesperado: " + exc.Message, exc.InnerException);
            }
        }


        /// <summary>
        /// Actualizar el número de vital asociado a una solicitud
        /// </summary>
        /// <param name="p_intSolicitudID">int con el identificador de la solicitud</param>
        /// <param name="p_strNumeroVital">string con el número vital</param>
        private void ActualizarNumeroVitalSolicitudCambiorMenor(int p_intSolicitudID, string p_strNumeroVital)
        {
            this._objSolicitudREADalc.ActualizarNumeroVitalSolicitudREA(p_intSolicitudID, p_strNumeroVital);
        }


        /// <summary>
        /// Mover el registro de solicitud a tablas de error
        /// </summary>
        /// <param name="p_intSolicitudID">int con el identificador de la solicitud</param>
        /// <param name="p_strMensajeError">string con el mensaje de error</param>
        public void MoverSolicitudError(int p_intSolicitudID, string p_strMensajeError)
        {
            this._objSolicitudREADalc.MoverSolicitudError(p_intSolicitudID, p_strMensajeError);
        }

        #endregion


        #region  Metodos Publicos

        /// <summary>
        /// Insertar la solicitud de cambio menor
        /// </summary>
        /// <param name="p_objSolicitud">SolicitudCambioMenorEntity con la información de la solicitud. Ingresa por referencia para cargue de identificadores</param>
        public void InsertarSolicitudREA(ref SolicitudREAEntity p_objSolicitud)
        {
            try
            {
                //Almacenar la información de la solicitud
                this._objSolicitudREADalc.InsertarSolicitudREA(ref p_objSolicitud);

                //Verificar que se haya insertado correctamente la información
                if (p_objSolicitud.SolicitudREAID > 0)
                {
                    //Radicar solicitud en VITAL
                    p_objSolicitud.NumeroVITAL = this.RegistroVital(p_objSolicitud);

                    //Actualizar el número vital en el sistema
                    this.ActualizarNumeroVitalSolicitudCambiorMenor(p_objSolicitud.SolicitudREAID, p_objSolicitud.NumeroVITAL);

                    //Mover archivos de carpeta temporales a carpeta vital
                    this.MoverArchivosCarpetaVital(p_objSolicitud);
                }
                else
                {
                    throw new Exception("No se inserto la información de la solicitud de manera correcta");
                }

            }
            catch (RadicacionSolicitudREAException exc)
            {
                try
                {
                    this.MoverSolicitudError(p_objSolicitud.SolicitudREAID, "Se presento error en el registro y generación de número VITAL. Error: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                catch (Exception e)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "SolicitudREA :: InsertarSolicitudREA -> Error realizando movimiento de registro a tablas de error " + p_objSolicitud.SolicitudREAID.ToString() + " : " + exc.Message + " - " + exc.StackTrace);
                }

                //Escalar excepción
                throw exc;
            }
            catch (DocumentoSolicitudREAException exc)
            {
                //Escalar excepción
                throw exc;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SolicitudREA :: InsertarSolicitudREA -> Error bd: " + exc.Message + " - " + exc.StackTrace);

                //Escalar error
                throw new SolicitudREAException("Se presento un error no esperado durante el proceso de alamcenamiento de la información de la solicitud de evalucación REA. " + exc.Message, exc);
            }
        }


        #endregion
    }
}
