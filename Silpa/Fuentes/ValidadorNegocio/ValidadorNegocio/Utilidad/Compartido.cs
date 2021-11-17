using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.ValidadorNegocio.Utilidad
{
    class Compartido
    {
        public string cadenaSQL;
        public static Compartido _compartido;
 
        public static void Init(string sql)
        {
            _compartido = new Compartido();
            _compartido.cadenaSQL = sql;
        }
    }
}
