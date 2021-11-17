using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;


namespace SoftManagement.Log
{
    public static class LogConfig
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
                return ConfigurationManager.AppSettings["LOG_PROCEDIMIENTO_ALMACENADO"];
            }
        }



        #region jmartinez creo clase para referenciar el log que guarda la informacion de los correos
        public static string ProcedimientoAlmacenadoCorreo
        {
            get
            {
                return ConfigurationManager.AppSettings["LOG_PROCEDIMIENTO_ALMACENADO_CORREO"];
            }
        }

        public static string IngresarDetallesLogCorreo
        {
            get
            {
                return ConfigurationManager.AppSettings["INGRESAR_DETALLES_LOG_CORREO"];
            }
        }

        #endregion

        public static string NombreArchivo
        {   
            get
            {
                return ConfigurationManager.AppSettings["LOG_NOMBRE_ARCHIVO"];
            }
        }
    }
}