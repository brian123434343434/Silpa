using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
  

namespace Referenciador
{
    
    public class Proxy: SoapHttpClientProtocol
    {
        public Proxy()
        {
            
            string user = ConfigurationManager.AppSettings["usuario_servicio"].ToString();

            string pass = ConfigurationManager.AppSettings["clave_servicio"].ToString();

            this.Credentials = new System.Net.NetworkCredential(user, pass); 

        }
    }

}
