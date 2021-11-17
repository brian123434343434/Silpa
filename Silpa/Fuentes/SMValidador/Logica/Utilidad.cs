using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.Validador
{
    public class Utilidad
    {
        public string _cadena = "";
        public enum TipoMotor { SQLSERVER, ORACLE, MYSQL }
        public TipoMotor _tipoMotor; 
        public static Utilidad _utilidad;
        

        public static void CargaValores(string cadena, TipoMotor motor)
        {
            _utilidad = new Utilidad();
            _utilidad._cadena = cadena;
            _utilidad._tipoMotor = motor;
        }
    }
}
