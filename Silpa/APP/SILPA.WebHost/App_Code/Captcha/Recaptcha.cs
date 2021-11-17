using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

/// <summary>
/// Descripción breve de CaptchaEnum
/// </summary>
public class Recaptcha
{
    #region Propiedades

        /// <summary>
        /// LLave publica de conexión a captcha
        /// </summary>
        public static string LlavePublica
        {
            get
            {
                return (ConfigurationManager.AppSettings["recaptcha-publico"] != null ? ConfigurationManager.AppSettings["recaptcha-publico"].ToString() : "");
            }
        }

        /// <summary>
        /// LLave privada de conexión a captcha
        /// </summary>
        public static string LlavePrivada
        {
            get
            {
                return (ConfigurationManager.AppSettings["recaptcha-privado"] != null ? ConfigurationManager.AppSettings["recaptcha-privado"].ToString() : "");
            }
        }

        /// <summary>
        /// URL de ubicación de api de google
        /// </summary>
        public static string URLAPI
        {
            get
            {
                return (ConfigurationManager.AppSettings["recaptcha-url-api"] != null ? ConfigurationManager.AppSettings["recaptcha-url-api"].ToString() : "");
            }
        }

        /// <summary>
        /// URL de conexión para verificación de captcha
        /// </summary>
        public static string URLVerificacion
        {
            get
            {
                return (ConfigurationManager.AppSettings["recaptcha-url-verificacion"] != null ? ConfigurationManager.AppSettings["recaptcha-url-verificacion"].ToString() : "");
            }
        }

    #endregion


    #region Metodos Publicos

        /// <summary>
        /// Verifica que se realice la validación del captcha de manera correcta
        /// </summary>
        /// <returns>Boolean que indica si se verifico de manaera correcta el captcha</returns>
        public static bool Validar(string p_strCaptchaResponse){            
            bool blnValido = false;

            if(!string.IsNullOrWhiteSpace(p_strCaptchaResponse)){

                WebRequest objSolicitud = WebRequest.Create(URLVerificacion + "?secret=" + LlavePrivada + "&response=" + p_strCaptchaResponse);
                using(WebResponse objWebResponse  = objSolicitud.GetResponse())
                {
                    using(StreamReader objReadStream = new StreamReader(objWebResponse.GetResponseStream())){
                        string strJsonResponse = objReadStream.ReadToEnd();
                        JavaScriptSerializer objSerializador = new JavaScriptSerializer();
                        CaptchaResponse objCaptchaResponse = objSerializador.Deserialize<CaptchaResponse>(strJsonResponse);
                        blnValido = Convert.ToBoolean(objCaptchaResponse.success);
                    }
                }
            }

            return blnValido;
        }

    #endregion

}