<%@ WebHandler Language="C#" Class="Manejador" %>

using System;
using System.Web;
using System.Drawing;
using System.IO;
using SoftManagement.Log;
using System.Configuration;
using iTextSharp.text.pdf;

public class Manejador : IHttpHandler
{
    #region Propiedades

        #region Heredadas

            public bool IsReusable
            {
                get
                {
                    return false;
                }
            }
    
        #endregion
        
    #endregion


    #region Metodos Privados

        /// <summary>
        /// Generar imagen de acuerdo a código proporcionado
        /// </summary>
        /// <param name="p_strCodigo">string con el codigo de barras</param>
        /// <returns>Arreglo de bytes con la imagen. null en caso de no generarse</returns>
        private static Bitmap GenerarImagen(string p_strCodigo, int p_fltAncho, int p_fltAlto)
        {
            Barcode128 objGenerador = null;
            Bitmap objImagenCodigo = null;
            Image objImage = null;

            //Cargar parametros del codigo de barras
            objGenerador = new Barcode128();
            objGenerador.CodeType = iTextSharp.text.pdf.Barcode.CODE128_UCC;
            objGenerador.BarHeight = p_fltAlto;
            objGenerador.ChecksumText = true;
            objGenerador.GenerateChecksum = true;
            objGenerador.StartStopText = false;
            objGenerador.Code = p_strCodigo;
            objImage = objGenerador.CreateDrawingImage(Color.Black, System.Drawing.Color.White);
            objImagenCodigo = new Bitmap(objImage, objImage.Width, objImage.Height);
            
            return objImagenCodigo;
        }
    
    #endregion
    

    #region Evento

        /// <summary>
        /// Evento que genera imagen de codigo de barras
        /// </summary>
        public void ProcessRequest(HttpContext context)
        {
            System.ComponentModel.TypeConverter objTypeConverter = null;
            string strCodigo = "";
            string strAlto = "";
            string strAncho = "";
            Bitmap objBitmap = null;
            FileStream objFileStream = null;
            byte[] objImagenCodigoBarras = null;
            string strPathImagenError = "";
                                
            try
            {
                //Obtener parametros request
                strCodigo = context.Request.QueryString.Get("code");
                strAncho = context.Request.QueryString.Get("width");
                strAlto = context.Request.QueryString.Get("height");
                
                //Verificar que se especifique el codigo que se quiere obtener como codigo de barras
                if (!string.IsNullOrEmpty(strCodigo) && !string.IsNullOrEmpty(strAlto) && !string.IsNullOrEmpty(strAncho))
                {
                    objBitmap = GenerarImagen(strCodigo, Convert.ToInt32(strAncho), Convert.ToInt32(strAlto));                    
                }
                else
                {
                    throw new Exception("No se especifico código para generación de imagen");
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Manejador - Generador Codigo Barras :: ProcessRequest -> Error Inesperado generando el código de barras con los siguientes datos: \n" +
                                                  "strCode: " + (!string.IsNullOrWhiteSpace(strCodigo) ? strCodigo : "null") + "\n" +
                                                  "Error: " + exc.Message + " - " + exc.StackTrace.ToString());
                try
                {
                    //Cargar el path de imagen de error
                    strPathImagenError = context.Server.MapPath("~/images/") + "Error_Barras.jpg";

                    //Leer imagen
                    objFileStream = new FileStream(strPathImagenError, FileMode.OpenOrCreate, FileAccess.Read);
                    objImagenCodigoBarras = new byte[objFileStream.Length];
                    objFileStream.Read(objImagenCodigoBarras, 0, Convert.ToInt32(objFileStream.Length));                    

                    //Cargar imagen
                    objTypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(Bitmap));
                    objBitmap = (Bitmap)objTypeConverter.ConvertFrom(objImagenCodigoBarras);
                }
                catch (Exception ex)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Manejador - Generador Codigo Barras :: ProcessRequest -> Error cargando imagen default de error: \n" +
                                                      "strCode: " + (!string.IsNullOrWhiteSpace(strCodigo) ? strCodigo : "null") + "\n" +
                                                      "Error: " + ex.Message + " - " + ex.StackTrace.ToString());
                }
                finally
                {
                    //Cerrar filestream
                    if (objFileStream != null)
                        objFileStream.Close();
                }
                                
            }
            finally
            {
                try
                {
                    //Mostrar imagen en pantalla
                    context.Response.ContentType = "image/jpeg";
                    objBitmap.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    objBitmap.Dispose();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Manejador - Generador Codigo Barras :: ProcessRequest -> Error mostrando imagen en pantalla: \n" +
                                                        "strCode: " + (!string.IsNullOrWhiteSpace(strCodigo) ? strCodigo : "null") + "\n" +
                                                        "Error: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Mostrar mensaje de error en pantalla 
                    context.Response.Write("Error durante cargue de imagen. Por favor comunicarse con el administrador del sistema");
                }
                
                //Eliminar objetos
                objTypeConverter = null;
                strCodigo = "";
                objBitmap = null;
                objFileStream = null;
                objImagenCodigoBarras = null;
                strPathImagenError = "";                
            }
        }
    

    #endregion

}