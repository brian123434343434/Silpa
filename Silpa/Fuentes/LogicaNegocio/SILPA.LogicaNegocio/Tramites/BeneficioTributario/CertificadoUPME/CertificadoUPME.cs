using SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME;
using SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME.Entidades;
using SILPA.LogicaNegocio.BPMProcessL;
using SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades;
using SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Excepciones;
using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;



namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME
{

    public class CertificadoUPME
    {
        public CertificadoUPMEDalc _CertificadoUPMEDalc;

        public CertificadoUPME()
        {
            _CertificadoUPMEDalc = new CertificadoUPMEDalc();
        }
        public List<proyectos> ConsultaCertificado(string p_strNumeroCertificado)
        {
            string strRespuestaServidor = string.Empty;
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;
            // se consulta la url dada por UPME para el consumo del servicio.
            string strUrlServicio = "";

            //Crear objetoobtener parametros
            objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
            List<proyectos> objRespuesta = null;
            //Cargar la URL del servicio de crea el radicado
            strUrlServicio = objParametrizacion.ObtenerValorParametroGeneral(-1, "WS_UPME");
            // verificamo si existe el dato para la URL
            if (!string.IsNullOrEmpty(strUrlServicio))
            {
                // agregamos los valores del filtro a la url
                strUrlServicio = strUrlServicio + "/proyectos/?search=";
                try
                {
                    //Radicar documento
                    strRespuestaServidor = this.GetResponse_GET(strUrlServicio, p_strNumeroCertificado);
                }
                catch (Exception ex)
                {

                    SMLog.Escribir(Severidad.Critico, "CertificadoUPME :: ConsultaCertificado -> ERROR INVOCANDO GetResponse_POST: " + ex.Message + strUrlServicio + p_strNumeroCertificado);
                    throw new UPMEException("CertificadoUPME :: ConsultaCertificado -> Error consultando certificado: " + ex.Message, ex);
                }

                //verificamos que tenga respuesta de servicio
                if (!string.IsNullOrEmpty(strRespuestaServidor))
                {
                    try
                    {
                        //Cargar respuesta
                        //objRespuesta = new proyectos();
                        //objRespuesta = (proyectos)objRespuesta.Deserializar(strRespuestaServidor);
                        objRespuesta = new JavaScriptSerializer().Deserialize<List<proyectos>>(strRespuestaServidor);

                        foreach (var itemRespuesta in objRespuesta)
	                    {
                            //verificamos que se halla obtenido datos
                            if (itemRespuesta.id != string.Empty   /*objRespuesta.certificado != string.Empty*/)
                            {
                                // cargamos los solicitantes secundarios
                                if (itemRespuesta.solicitante_secundario.Count() > 0)
                                {
                                    itemRespuesta.lstSolicitanteSecundario = cargarsolicitantes_secundarios(itemRespuesta.solicitante_secundario);
                                }
                                // cargamos los incentivos
                                if (itemRespuesta.incentivos.Count() > 0)
                                {
                                    itemRespuesta.lstInsentivos = cargarincentivos(itemRespuesta.incentivos);
                                }
                                if (itemRespuesta.bienes.Count() > 0)
                                {
                                    itemRespuesta.lstBienes = cargarbienes(itemRespuesta.bienes);
                                }
                                if (itemRespuesta.servicios.Count() > 0)
                                {
                                    itemRespuesta.lstServicios = cargarservicios(itemRespuesta.servicios);
                                }
                                if (itemRespuesta.municipio != null && itemRespuesta.municipio != string.Empty)
                                {
                                    itemRespuesta.objmunicipio = cargarmunicipios(itemRespuesta.municipio);
                                }
                                if (itemRespuesta.generalidadesproyecto != null && itemRespuesta.generalidadesproyecto != string.Empty)
                                {
                                    itemRespuesta.objGeneralidadesproyecto = cargargeneralidades(itemRespuesta.generalidadesproyecto);
                                }
                                if (itemRespuesta.usuario != null && itemRespuesta.usuario != string.Empty)
                                {
                                    itemRespuesta.objusuario = cargarUsuario(itemRespuesta.usuario);
                                }
                            }
	                    } 
                        
                    }
                    catch (Exception ex)
                    {
                        
                        throw;
                    }
                }
                return objRespuesta;

            }
            else
            {
                throw new Exception("CertificadoUPME :: ConsultaCertificado -> URL de servicio no configurada.");
            }
        }
        public string CrearProcesoCertificado(string ClientId, Int64 FormularioQueja, Int64 UsuarioQueja, string ValoresXml)
        {
            BpmProcessLn objProceso = new BpmProcessLn();
            return objProceso.crearProceso(ClientId, FormularioQueja, UsuarioQueja, ValoresXml);      
        }
        public void GuardarSolicitudCertificado(ref CertificadoUPMEEntity objEntity)
        {
            CertificadoUPMEDalc objCertificadoUPMEDalc = new CertificadoUPMEDalc();
            objCertificadoUPMEDalc.InsertarCertificado(ref objEntity);
            foreach (var bien in objEntity.lstBienes)
            {
                bien.certificadoID = objEntity.certificadoID;
                objCertificadoUPMEDalc.insertarBien(bien);
            }
            foreach (var servicio in objEntity.lstServicios)
            {
                servicio.certificadoID = objEntity.certificadoID;
                objCertificadoUPMEDalc.insertarServicio(servicio);
            }
            foreach (var solicitanteSecundario in objEntity.lstSolicitanteSecundario)
            {
                solicitanteSecundario.certificadoID = objEntity.certificadoID;
                objCertificadoUPMEDalc.insertarSolicitante(solicitanteSecundario);
            }
            objEntity.solicitantePrincial.certificadoID = objEntity.certificadoID;
            objCertificadoUPMEDalc.insertarSolicitante(objEntity.solicitantePrincial);
        }
        public string crearListaSolicitantesSecundatios(List<solicitantes_secundarios> lst)
        {
            string lstDatos = string.Empty;
            foreach (solicitantes_secundarios objSolicitante in lst)
            {
                lstDatos += string.Format("<li>{0} {1} {2}</li>", objSolicitante.nombre, objSolicitante.tipo_identificacion, objSolicitante.identificacion);
            }
            return string.Format("<ul>{0}</ul>", lstDatos);
        }
        public void guardarSoportePago(string ruta, int certificadoID, string numeroReferencia)
        {
            _CertificadoUPMEDalc.guardarSoportePago(ruta, certificadoID, numeroReferencia);
        }
        public void guardarRutaDescripcionProyecto(string ruta, int certificadoID)
        {
            _CertificadoUPMEDalc.guardarRutaDescripcionProyecto(ruta, certificadoID);
        }
        public List<FuenteConvencionalSustituirEntity> listaFuenteConvencional()
        {
            return _CertificadoUPMEDalc.lstFuenteConvencionalSustituir();
        }
        public List<SubFuenteConvencionalSustituirEntity> listaSubfuenteConvencional(int fuenteConvencionalID)
        {
            return _CertificadoUPMEDalc.lstSubFuenteConvencionalSustituir(fuenteConvencionalID);
        }
        public string GenerarPDFSolicitud(int certificadoID, string path)
        {
            Formularios.CrearFormularios clsCrearFormularios = new Formularios.CrearFormularios();
            string nombreArchivo = clsCrearFormularios.GenerarSolicitudCertificadoFNCE(certificadoID, path);
            return nombreArchivo;
        }
        public void GenerarArchivoBienesCSV(List<bienesEntity> lst, string path)
        {

            string csvHeaderRow = String.Join(",", typeof(bienesEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(x => x.Name).ToArray<string>());

            using (var writer = new StreamWriter(path + "bienes.csv",false,Encoding.UTF8))
            {
                writer.WriteLine(csvHeaderRow);

               foreach (var item in lst)
                {
                    writer.WriteLine(item.ToString());
                }
               writer.Flush();
            }
        }
        public void GenerarArchivoServiciosCSV(List<serviciosEntity> lst, string path)
        {

            string csvHeaderRow = String.Join(",", typeof(serviciosEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(x => x.Name).ToArray<string>());

            using (var writer = new StreamWriter(path + "servicios.csv", false, Encoding.UTF8))
            {
                writer.WriteLine(csvHeaderRow);

                foreach (var item in lst)
                {
                    writer.WriteLine(item.ToString());
                }
                writer.Flush();
            }
        }

        #region Metodos privados
        private string GetResponse_GET(string p_strUrl)
        {
            LogUPME _objLogUPME = new LogUPME();
            HttpWebRequest objRequest = null;
            HttpWebResponse objResponse = null;
            StreamReader objReader = null;
            string strRespuesta = "";

            //_objLogUPME.insertarRegistroLog(p_strUrl, "GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, "INICIO SERVICIO UPME");
            try
            {
                // parametros de consulta del certificado
                objRequest = (HttpWebRequest)HttpWebRequest.Create(p_strUrl);
                objRequest.Method = "GET";
                objRequest.ContentType = "application/json; charset=utf-8";
                objRequest.Timeout = 60000;
                //_objLogUPME.insertarRegistroLog(p_strUrl, "GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, "VA A INICIAR EJECUCION SERVICIO UPME");
                objResponse = (HttpWebResponse)objRequest.GetResponse();
                if (objResponse.StatusCode == HttpStatusCode.OK)
                {
                    objReader = new StreamReader(objResponse.GetResponseStream());
                    strRespuesta = objReader.ReadToEnd();
                    objReader.Close();
                    //_objLogUPME.insertarRegistroLog("GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, strRespuesta, "RESPUESTA OK SERVICIO UPME");
                }
                else
                {
                    //_objLogUPME.insertarRegistroLog("GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, strRespuesta, "RESPUESTA NO OK SERVICIO UPME");
                }
            }
            catch (WebException webEx)
            {
                //_objLogUPME.insertarRegistroLog("GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, webEx.Message, "ERROR SERVICIO UPME");
                throw new Exception(webEx.Message);
            }
            catch (System.Web.HttpException ex)
            {
                //_objLogUPME.insertarRegistroLog("GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, ex.Message, "System.Web.HttpException ERROR SERVICIO UPME");
                if (ex.ErrorCode == 404)
                    throw new Exception("No se encuentra el servicio " + p_strUrl);
                else throw ex;
            }
            catch (Exception ex)
            {
                //_objLogUPME.insertarRegistroLog("GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, ex.Message, "ERROR SERVICIO UPME");
                throw new Exception(ex.Message);
            }
            return strRespuesta;
        }
        private string GetResponse_GET(string p_strUrl, string p_strNumeroCertificado)
        {
            LogUPME _objLogUPME = new LogUPME();
            HttpWebRequest objRequest = null;
            HttpWebResponse objResponse = null;
            StreamReader objReader = null;
            string strRespuesta = "";

            //_objLogUPME.insertarRegistroLog(p_strUrl, "GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, "INICIO SERVICIO UPME");
            try
            {
                // parametros de consulta del certificado
                objRequest = (HttpWebRequest)HttpWebRequest.Create(p_strUrl + p_strNumeroCertificado);
                objRequest.Method = "GET";
                objRequest.ContentType = "application/json; charset=utf-8";
                objRequest.Timeout = 60000;
                //_objLogUPME.insertarRegistroLog(p_strUrl, "GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, "VA A INICIAR EJECUCION SERVICIO UPME");
                objResponse = (HttpWebResponse)objRequest.GetResponse();
                if (objResponse.StatusCode == HttpStatusCode.OK)
                {
                    objReader = new StreamReader(objResponse.GetResponseStream());
                    strRespuesta = objReader.ReadToEnd();
                    objReader.Close();
                    //_objLogUPME.insertarRegistroLog("GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, strRespuesta, "RESPUESTA OK SERVICIO UPME");
                }
                else
                {
                    //_objLogUPME.insertarRegistroLog("GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, strRespuesta, "RESPUESTA NO OK SERVICIO UPME");
                }
            }
            catch (WebException webEx)
            {
                //_objLogUPME.insertarRegistroLog("GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, webEx.Message, "ERROR SERVICIO UPME");
                throw new Exception(webEx.Message);
            }
            catch (System.Web.HttpException ex)
            {
                //_objLogUPME.insertarRegistroLog("GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, ex.Message, "System.Web.HttpException ERROR SERVICIO UPME");
                if (ex.ErrorCode == 404)
                    throw new Exception("No se encuentra el servicio " + p_strUrl);
                else throw ex;
            }
            catch (Exception ex)
            {
                //_objLogUPME.insertarRegistroLog("GetResponse_POST::" + p_strUrl + p_strNumeroCertificado, ex.Message, "ERROR SERVICIO UPME");
                throw new Exception(ex.Message);
            }
            return strRespuesta;
        }
        private usuarios cargarUsuario(string dato)
        {
            string strResultado = string.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            strResultado = GetResponse_GET(dato);
            usuarios obj = serializer.Deserialize<usuarios>(strResultado);
            obj.lstSolicitantePrincipal = cargardatosSolicitantePrincipal(obj.solicitante_principal);
            return obj;
        }
        private List<solicitantes_secundarios> cargarsolicitantes_secundarios(List<string> listaDatos)
        {
            string strResultado = string.Empty;
            List<solicitantes_secundarios> lstDatosTipo = new List<solicitantes_secundarios>();
            foreach (var item in listaDatos)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                strResultado = GetResponse_GET(item);
                solicitantes_secundarios obj = serializer.Deserialize<solicitantes_secundarios>(strResultado);
                obj.objCodigoCIIU = cargarCodigoCIIU(obj.codigo_ciiu);
                obj.objMunicipios = cargarmunicipios(obj.municipio);
                lstDatosTipo.Add(obj);
            }
            return lstDatosTipo;
        }
        private List<incentivos> cargarincentivos(List<string> listaDatos)
        {
            string strResultado = string.Empty;
            List<incentivos> lstDatosTipo = new List<incentivos>();
            foreach (var item in listaDatos)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                strResultado = GetResponse_GET(item);
                incentivos obj = serializer.Deserialize<incentivos>(strResultado);
                lstDatosTipo.Add(obj);
            }
            return lstDatosTipo;
        }
        private List<bien> cargarbienes(List<string> listaDatos)
        {
            string strResultado = string.Empty;
            List<bien> lstDatosTipo = new List<bien>();
            foreach (var item in listaDatos)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                strResultado = GetResponse_GET(item);
                bien obj = serializer.Deserialize<bien>(strResultado);
                lstDatosTipo.Add(obj);
            }
            return lstDatosTipo;
        }
        private List<servicios> cargarservicios(List<string> listaDatos)
        {
            string strResultado = string.Empty;
            List<servicios> lstDatosTipo = new List<servicios>();
            foreach (var item in listaDatos)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                strResultado = GetResponse_GET(item);
                servicios obj = serializer.Deserialize<servicios>(strResultado);
                lstDatosTipo.Add(obj);
            }
            return lstDatosTipo;
        }
        private municipios cargarmunicipios(string dato)
        {
            string strResultado = string.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            strResultado = GetResponse_GET(dato);
            municipios obj = serializer.Deserialize<municipios>(strResultado);
            obj.objdepartamento = cargardepartamentos(obj.departamento);
            return obj;
        }
        private departamentos cargardepartamentos(string dato)
        {
            string strResultado = string.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            strResultado = GetResponse_GET(dato);
            departamentos obj = serializer.Deserialize<departamentos>(strResultado);

            return obj;
        }
        private generalidades cargargeneralidades(string dato)
        {
            string strResultado = string.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            strResultado = GetResponse_GET(dato);
            generalidades obj = serializer.Deserialize<generalidades>(strResultado);
            obj._objdatosambientales = cargardatosambientales(obj.datosambientales);
            obj._objUbicaciones = cargarubicaciones(obj.ubicaciongeneralidades);
            obj._objdatostecnicos = cargardatostecnicos(obj.datostecnicos);
            return obj;
        }
        private string obtenerNombreArchivo(string url)
        {
            string archivo = url.Split('/').Last();
            return archivo;
        }
        private datos_ambientales cargardatosambientales(string dato)
        {
            string strResultado = string.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            strResultado = GetResponse_GET(dato);
            datos_ambientales obj = serializer.Deserialize<datos_ambientales>(strResultado);
            return obj;
        }
        private ubicaciones cargarubicaciones(string dato)
        {
            string strResultado = string.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            strResultado = GetResponse_GET(dato);
            ubicaciones obj = serializer.Deserialize<ubicaciones>(strResultado);
            return obj;
        }
        private datostecnicos cargardatostecnicos(string dato)
        {
            string strResultado = string.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            strResultado = GetResponse_GET(dato);
            datostecnicos obj = serializer.Deserialize<datostecnicos>(strResultado);
            return obj;
        }
        private codigo_ciiu cargarCodigoCIIU(string dato)
        {
            string strResultado = string.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            strResultado = GetResponse_GET(dato);
            codigo_ciiu obj = serializer.Deserialize<codigo_ciiu>(strResultado);
            obj.objSectorProductivo = cargarSectorProductivo(obj.sector_productivo);
            return obj;
        }
        private sector_productivo cargarSectorProductivo(string dato)
        {
            string strResultado = string.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            strResultado = GetResponse_GET(dato);
            sector_productivo obj = serializer.Deserialize<sector_productivo>(strResultado);
            return obj;
        }
        private List<solicitantes_principales> cargardatosSolicitantePrincipal(List<string> listaDatos)
        {
            string strResultado = string.Empty;
            List<solicitantes_principales> lstDatosTipo = new List<solicitantes_principales>();
            foreach (var item in listaDatos)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                strResultado = GetResponse_GET(item);
                solicitantes_principales obj = serializer.Deserialize<solicitantes_principales>(strResultado);
                obj.objCodigoCIIU = cargarCodigoCIIU(obj.codigo_ciiu);
                obj.objMunicipios = cargarmunicipios(obj.municipio);
                lstDatosTipo.Add(obj);
            }
            return lstDatosTipo;
        }

        #endregion Metodos privados
    }
}
