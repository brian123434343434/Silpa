using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Reflection; 

namespace IdentidadAutoridad
{
    public class Identidad
    {
        private int _idAutoridadAmbiental;
        public int IdAutoridadAmbiental
        {
            get { return _idAutoridadAmbiental; }
        }

        private string _cadenaCx;
        public string CadenaCx
        {
            get { return _cadenaCx; }
        }

        private Dao.IdentidadAutoridad _identidad;

        public Identidad():this(CadenaValor("CadenaCxBD"))
        {}

        public Identidad(string cadenaCX)
        {
            _idAutoridadAmbiental = 0;
            _cadenaCx = cadenaCX;
            _identidad = new IdentidadAutoridad.Dao.IdentidadAutoridad(_cadenaCx); 
        }

        public Entidades.IdentidadAutoridad IdentidadAutoridad()
        {
            return _identidad.Consultar(_idAutoridadAmbiental);   
        }

        public static string CadenaValor(string llave)
        {
            Assembly ensamblado = typeof(Identidad).Assembly;   
            ResourceManager lector = new ResourceManager("Softmanagement.IdentidadAutoridad.Recurso",ensamblado);
            return lector.GetString(llave);
        }

        public static string EncriptarMensaje(string mensaje)
        {
            return SILPA.Comun.EnDecript.Encriptar(mensaje);  
        }

        public static string DesEncriptarMensaje(string mensaje)
        {
            return SILPA.Comun.EnDecript.Desencriptar(mensaje);  
        }
    }

}
