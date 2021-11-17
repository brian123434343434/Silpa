using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace SoftManagement.Log.Logica
{
    internal static class SMLogErrores
    {
        public static void Escribir(string mensaje)
        {
            if (!(String.IsNullOrEmpty(LogConfig.NombreArchivo)))
            {
                using (StreamWriter archivo = new StreamWriter(LogConfig.NombreArchivo,true))
                {
                    archivo.WriteLine(mensaje);
                }
            }
        }
    }
}
