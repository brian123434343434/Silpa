using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.Comun
{ 
    public class DireccionamientoWS
    {
        public static string UrlWS(string llave)
        {
            string salida = System.Configuration.ConfigurationManager.AppSettings[llave];

            if (salida.Equals(string.Empty))
                return "";
            return salida;
        }

        /// <summary>
        /// HAVA:05-ABR-2011
        /// Crea las credenciales para los servicios.
        /// </summary>
        /// <param name="user">usuario</param>
        /// <param name="password">clave</param>
        /// <returns></returns>
        public static System.Net.NetworkCredential Credenciales()
        {
            string user = System.Configuration.ConfigurationManager.AppSettings["usuario_servicio"].ToString();
            string pass = System.Configuration.ConfigurationManager.AppSettings["clave_servicio"].ToString();
            System.Net.NetworkCredential credencial = new System.Net.NetworkCredential(user, pass);
            return credencial;
        }

    }
}
