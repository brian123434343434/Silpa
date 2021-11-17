using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace SoftManagement.LogWS
{
    public static class LogWSConfig
    {
        public static string Conexion
        {
            get
            {
                return ConfigurationManager.AppSettings["LOG_CONEXION"];
            }
        }

        public static string ProcedimientoAlmacenado
        {
            get
            {
                return ConfigurationManager.AppSettings["LOGWS_PROCEDIMIENTO_ALMACENADO"];
            }
        }

        public static string NombreArchivo
        {
            get
            {
                return ConfigurationManager.AppSettings["LOGWS_NOMBRE_ARCHIVO"];
            }
        }
    }
}
