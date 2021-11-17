using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;


namespace AppRadicacion.Comun
{
    class Utilidades
    {
        public static Utilidades utl;
        public string cadena;  
        public static void Init(string valor)
        {
            utl = new Utilidades();
            utl.cadena = valor; 
        }
    }
}
