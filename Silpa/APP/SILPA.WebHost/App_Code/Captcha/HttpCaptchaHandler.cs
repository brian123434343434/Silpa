using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using SILPA.LogicaNegocio.Generico;
using SoftManagement.Log;
using SILPA.ComunSeguridad;
using System.Drawing;
using System.Web.SessionState;

/// <summary>
/// Descripción breve de HttpCaptchaHandler
/// </summary>
public class HttpCaptchaHandler : IHttpHandler, IReadOnlySessionState
{
    /// <summary>
    /// Muestra el archivo anexo indicado
    /// </summary>
    /// <param name="context">HttpContext con el contexto del sitio</param>
    public void ProcessRequest(HttpContext context)
    {        
        ImageConverter objConverter = null;
        CaptchaResult objCaptchaResult = null;
        Stream objStream = null;
        Image objImage = null;
        string strCaptchaCode = "";
        byte[] objContenido = null;
        int width = 200;
        int height = 60;

        try
        {
            //Generar captcha
            strCaptchaCode = Captcha.GenerateCaptchaCode();
            objCaptchaResult = Captcha.GenerateCaptchaImage(width, height, strCaptchaCode);

            //Adicionar datos del captcha en sesion
            context.Session.Add("CaptchaCode", objCaptchaResult.CaptchaCode);

            //Generar imagen
            objStream = new MemoryStream(objCaptchaResult.CaptchaByteData);
            objImage = Bitmap.FromStream(objStream);
                        
            //Cargar imagen
            objConverter = new ImageConverter();
            objContenido =  (byte[])objConverter.ConvertTo(objImage, typeof(byte[]));

            //Mostrar archivo
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", ".png"));
            context.Response.ContentType = "image/png";
            context.Response.BinaryWrite(objContenido);
            
        }
        catch (Exception exc)
        {
            //Escribir error
            SMLog.Escribir(Severidad.Critico, "HttpCaptchaHandler :: ProcessRequest -> Error Inesperado: " + exc.Message);

            //Limpiar sesion
            context.Session.Add("CaptchaCode", null);

            //Mostrar error
            context.Response.Clear();
            context.Response.Write("Se genero un error generando captcha. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
        }
        finally
        {
            //Finalizar respuesta
            context.Response.End();
        }
    }

    //Indicador si es reusable
    public bool IsReusable
    {
        get { return false; }
    }
}