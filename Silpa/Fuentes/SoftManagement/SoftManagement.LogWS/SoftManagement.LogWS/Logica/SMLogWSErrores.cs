using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SoftManagement.LogWS.Logica
{
    internal static class SMLogWSErrores
    {
        public static void Escribir(string mensaje)
        {
            if (File.Exists(LogWSConfig.NombreArchivo))
            {
                if (!(String.IsNullOrEmpty(LogWSConfig.NombreArchivo)))
                {
                    using (StreamWriter archivo = new StreamWriter(LogWSConfig.NombreArchivo, true))
                    {
                        archivo.WriteLine(mensaje);
                    }
                }
            }
        }
    }
}
