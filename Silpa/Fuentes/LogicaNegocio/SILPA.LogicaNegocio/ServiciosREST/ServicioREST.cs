using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using SILPA.Comun;
using SILPA.LogicaNegocio.ServiciosREST.Entidades;
using SoftManagement.Log;

namespace SILPA.LogicaNegocio.ServiciosREST
{
    public class ServicioREST
    {
        #region Objetos

            
        #endregion


        #region Metodos Privados

            /// <summary>
            /// Retorna si el log de servicios REST se encuentra activo
            /// </summary>
            /// <returns>bool indicando si se encuentra activo</returns>
            private bool LogActivo()
            {
                bool esActivo = false;
                string strValor = "";
                SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;

                //Obtener en que valor se encuentra el log            
                objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
                strValor = objParametrizacion.ObtenerValorParametroGeneral(-1, "SERVICIOS_REST.Log");

                //Verificar que se halla obtenido valor
                if (!string.IsNullOrEmpty(strValor))
                    esActivo = (strValor == "0" ? false : true);

                return esActivo;
            }

            /// <summary>
            /// Serializa un objeto a estructura json
            /// </summary>
            /// <param name="p_objObjeto">Object con el objeto a serializar</param>
            /// <returns>string con el objeto serializado</returns>
            private string ObtenerJsonObjeto(object p_objObjeto)
            {
                JavaScriptSerializer objSerializador = null;
                string strObjetoJson = "";

                //Crear objeto json
                objSerializador = new JavaScriptSerializer();             
                strObjetoJson = objSerializador.Serialize(p_objObjeto);
                
                return strObjetoJson;
            }

            /// <summary>
            /// Serializa un objeto a estructura json
            /// </summary>
            /// <param name="p_objObjeto">string con el objeto json a deserializar</param>
            /// <returns>Object con el objeto serializado</returns>
            private T ObtenerObjeto<T>(string p_strObjetoJson)
            {
                JavaScriptSerializer objSerializador = null;
                T objObjeto;

                //Obtener objeto json
                objSerializador = new JavaScriptSerializer();
                objObjeto = objSerializador.Deserialize<T>(p_strObjetoJson);

                return objObjeto;
            }

            /// <summary>
            /// Serializa un objeto a estructura json
            /// </summary>
            /// <param name="p_objObjeto">string con el objeto json a deserializar</param>
            /// <returns>Object con el objeto serializado</returns>
            private object ObtenerObjeto(string p_strObjetoJson)
            {
                JavaScriptSerializer objSerializador = null;
                object objObjeto;

                //Obtener objeto json
                objSerializador = new JavaScriptSerializer();
                objObjeto = objSerializador.DeserializeObject(p_strObjetoJson);

                return objObjeto;
            }


            /// <summary>
            /// Realiza el consumo del servicio REST especifiado
            /// </summary>
            /// <param name="p_strMetodoConexion">string con el metodo de conexion</param>
            /// <param name="p_strURL">string con la URL</param>
            /// <param name="p_strToken">string con el token de seguridad. Opcional</param>
            /// <param name="p_strParametrosEntrada">string con los parametros de entrada. Opcional</param>
            /// <returns>string con la respuesta del servicio</returns>
            private RespuestaServicioEntity ConsumirServicio(string p_strMetodoConexion, string p_strURL, string p_strToken = "", string p_strParametrosEntrada = "")
            {
                RespuestaServicioEntity objRespuesta = new RespuestaServicioEntity();
                HttpWebRequest objRequest = null;
                HttpWebResponse objResponse = null;
                byte[] objByteArray;
                StreamReader objReader = null;
                string strRespuesta = "";

                try
                {
                    if (this.LogActivo())
                        SMLog.Escribir(Severidad.Informativo, "ServiciosREST :: ConsumirServicio -> SE CARGA PARAMETROS DE CONEXION: p_strMetodoConexion: " + (p_strMetodoConexion ?? "null") + " - p_strURL: " + (p_strURL ?? "null") + " -- p_strParametrosEntrada: " + (p_strParametrosEntrada ?? "null"));
                    
                    try
                    {
                        //Crear objeto de envío de solicitud
                        objRequest = (HttpWebRequest)HttpWebRequest.Create(p_strURL);
                        objRequest.Method = p_strMetodoConexion;
                        objRequest.ContentType = "application/json";

                        //Adicionar TOKEN
                        if (!string.IsNullOrEmpty(p_strToken))
                        {
                            objRequest.PreAuthenticate = true;
                            objRequest.Headers.Add("Authorization", "Bearer " + p_strToken);
                        }

                        //Se crea stream para transmisión de información
                        if (!string.IsNullOrEmpty(p_strParametrosEntrada))
                        {
                            objByteArray = Encoding.UTF8.GetBytes(p_strParametrosEntrada);
                            objRequest.ContentLength = objByteArray.Length;
                            using (Stream dataStream = objRequest.GetRequestStream())
                            {
                                dataStream.Write(objByteArray, 0, objByteArray.Length);
                                dataStream.Close();
                            }
                        }
                        else
                            objRequest.ContentLength = 0;

                        //Cargar respuesta de solicitud
                        if (this.LogActivo())
                            SMLog.Escribir(Severidad.Informativo, "ServiciosREST :: ConsumirServicio -> " + p_strURL + " INICIA CONSUMO SERVICIO SIGPRO");
                        using (objResponse = (HttpWebResponse)objRequest.GetResponse())
                        {
                            if (objResponse.StatusCode == HttpStatusCode.OK)
                            {
                                using (objReader = new StreamReader(objResponse.GetResponseStream()))
                                {
                                    //Cargar respuesta
                                    if (objReader != null)
                                    {
                                        strRespuesta = objReader.ReadToEnd();
                                        objReader.Close();
                                    }
                                }

                                //Cargar respuesta
                                objRespuesta.CodigoRespuesta = CodigosRespuestaREST.OK;
                                objRespuesta.ObjetoRespuesta = strRespuesta;
                            }
                            else if (objResponse.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                objRespuesta.CodigoRespuesta = CodigosRespuestaREST.NO_AUTORIZADO;
                            }
                            else if (objResponse.StatusCode == HttpStatusCode.BadRequest)
                            {
                                objRespuesta.CodigoRespuesta = CodigosRespuestaREST.ERROR;
                            }
                            else if (objResponse.StatusCode == HttpStatusCode.NotFound)
                            {
                                objRespuesta.CodigoRespuesta = CodigosRespuestaREST.NO_EXISTE;
                            }
                            else
                            {
                                throw new Exception("Error durante el consumo del servicio. " + objResponse.StatusCode.ToString());
                            }

                            if (this.LogActivo())
                                SMLog.Escribir(Severidad.Informativo, "ServiciosREST :: ConsumirServicio -> " + p_strURL + " - CodigoRespuesta: " + objRespuesta.CodigoRespuesta.ToString() + " -- Respuesta: " + (strRespuesta ?? "null") + " -- FINALIZ CONSUMO SERVICIO");
                        }

                    }
                    catch (WebException webEx)
                    {
                        SMLog.Escribir(Severidad.Critico, "ServiciosREST :: ConsumirServicio -> " + p_strURL + " - Error: " + webEx.Message + "ERROR SERVICIO REST");
                        throw new Exception(webEx.Message);
                    }
                    catch (Exception ex)
                    {
                        SMLog.Escribir(Severidad.Critico, "ServiciosREST :: ConsumirServicio -> " + p_strURL + " - Error: " + ex.Message + "ERROR SERVICIO REST");
                        throw new Exception(ex.Message);
                    }
                }
                catch (HttpException ex)
                {
                    SMLog.Escribir(Severidad.Critico, "ServiciosREST :: ConsumirServicio -> " + p_strURL + " - Error: " + ex.Message + " HttpException - ERROR SERVICIO REST");
                    if (ex.ErrorCode == 404)
                        throw new Exception("No se encuentra el servicio " + p_strURL);
                    else throw ex;
                }

                return objRespuesta;
            }

        #endregion


        #region Metodos Publicos


            /// <summary>
            /// Realiza la ejecucion de un servicio REST
            /// </summary>
            /// <param name="p_objMetodoConexion">MetodosConexionREST con el metodo de conexion a utilizar</param>
            /// <param name="p_strURL">string con el URL</param>
            /// <param name="p_strToken">string con el token. Opcional</param>
            /// <param name="p_objParametrosEntrada">Object con los parametros de entrada. Opcional</param>
            /// <returns>T con la respuesta del servicio</returns>
            public RespuestaServicioEntity Ejecutar<T>(MetodosConexionREST p_objMetodoConexion, string p_strURL, string p_strToken = "", Object p_objParametrosEntrada = null)
            {
                string strParametros = "";
                RespuestaServicioEntity objRespuesta = null;

                try
                {
                    //Verificar que los parametros se encuentren correctos
                    if (p_objMetodoConexion != null && !string.IsNullOrEmpty(p_strURL))
                    {
                        //Verificar si se especificaron parametros de entrada
                        if (p_objParametrosEntrada != null)
                            strParametros = this.ObtenerJsonObjeto(p_objParametrosEntrada);

                        //Consumir el servicio REST
                        objRespuesta = this.ConsumirServicio(p_objMetodoConexion.ToString(), p_strURL, p_strToken, strParametros);

                        //Realizar deserializacion de respuesta
                        if (objRespuesta.ObjetoRespuesta != null && objRespuesta.CodigoRespuesta == CodigosRespuestaREST.OK && !string.IsNullOrEmpty((string)objRespuesta.ObjetoRespuesta))
                            objRespuesta.ObjetoRespuesta = this.ObtenerObjeto<T>((string)objRespuesta.ObjetoRespuesta);
                    }
                    else
                    {
                        throw new Exception("No se especifico metodo y/o URL de conexion");
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ServiciosREST :: Ejecutar -> Se presento error durante ejecucion de servicio REST: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Escalar error
                    throw exc;
                }

                return objRespuesta;
            }


            /// <summary>
            /// Realiza la ejecucion de un servicio REST
            /// </summary>
            /// <param name="p_objMetodoConexion">MetodosConexionREST con el metodo de conexion a utilizar</param>
            /// <param name="p_strURL">string con el URL</param>
            /// <param name="p_strToken">string con el token. Opcional</param>
            /// <param name="p_objParametrosEntrada">Object con los parametros de entrada. Opcional</param>
            /// <returns>RespuestaServicio con la respuesta del servicio</returns>
            public RespuestaServicioEntity Ejecutar(MetodosConexionREST p_objMetodoConexion, string p_strURL, string p_strToken = "", Object p_objParametrosEntrada = null)
            {
                string strParametros = "";
                RespuestaServicioEntity objRespuesta = null;

                try
                {
                    //Verificar que los parametros se encuentren correctos
                    if (p_objMetodoConexion != null && !string.IsNullOrEmpty(p_strURL))
                    {
                        //Verificar si se especificaron parametros de entrada
                        if (p_objParametrosEntrada != null)
                            strParametros = this.ObtenerJsonObjeto(p_objParametrosEntrada);

                        //Consumir el servicio REST
                        objRespuesta = this.ConsumirServicio(p_objMetodoConexion.ToString(), p_strURL, p_strToken, strParametros);

                        //Realizar deserializacion de respuesta
                        if (objRespuesta.ObjetoRespuesta != null && objRespuesta.CodigoRespuesta == CodigosRespuestaREST.OK && !string.IsNullOrEmpty((string)objRespuesta.ObjetoRespuesta))
                            objRespuesta.ObjetoRespuesta = this.ObtenerObjeto((string)objRespuesta.ObjetoRespuesta);
                    }
                    else
                    {
                        throw new Exception("No se especifico metodo y/o URL de conexion");
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ServiciosREST :: Ejecutar -> Se presento error durante ejecucion de servicio REST: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Escalar error
                    throw exc;
                }

                return objRespuesta;
            }


        #endregion

    }
}
