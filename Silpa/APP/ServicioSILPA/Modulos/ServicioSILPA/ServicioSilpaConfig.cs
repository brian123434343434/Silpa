using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ServicioSILPA
{
    public class ServicioSilpaConfig
    {
        public static int Intervalo
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["INTERVALO"]);
            }
        }
    }
}
